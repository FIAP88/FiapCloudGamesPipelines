using FiapCloudGamesAPI.Models;
using Nest;

namespace FCG_API_Jogos.Services
{
    public interface IJogoElasticService
    {
        Task IndexarAsync(Jogo jogo);
        Task<IEnumerable<Jogo>> BuscarAsync(string termo);
        Task RemoverAsync(string id);
        Task AtualizarAsync(Jogo jogo);

        Task<IEnumerable<Jogo>> SugerirBaseadoNoHistoricoAsync(IEnumerable<string> historicoTags);
        Task<IDictionary<string, long>> ObterJogosMaisPopularesAsync(int topN = 5);

    }

    public class JogoElasticService : IJogoElasticService
    {
        private readonly IElasticClient _client;

        public JogoElasticService(IElasticClient client)
        {
            _client = client;
        }

        public async Task IndexarAsync(Jogo jogo)
        {
            await _client.IndexDocumentAsync(jogo);
        }

        public async Task<IEnumerable<Jogo>> BuscarAsync(string termo)
        {
            var response = await _client.SearchAsync<Jogo>(s => s
                .Query(q => q
                    .MultiMatch(mm => mm
                        .Query(termo)
                        .Fields(f => f
                            .Field(ff => ff.Nome)
                            .Field(ff => ff.Descricao)
                            .Field(ff => ff.Tags)
                            .Field(ff => ff.Categoria.Descricao)
                        )
                    )
                )
                .Size(20)
            );

            return response.Documents;
        }

        public async Task RemoverAsync(string id)
        {
            var response = await _client.DeleteAsync<Jogo>(id);

            if (!response.IsValid)
            {
                throw new Exception($"Erro ao remover jogo do Elasticsearch: {response.ServerError?.Error?.Reason}");
            }
        }

        public async Task AtualizarAsync(Jogo jogo)
        {
            var response = await _client.UpdateAsync<Jogo>(jogo.Id, u => u
                .Doc(jogo)               // substitui o documento pelos novos dados
                .DocAsUpsert(true)       // se não existir, cria
                .RetryOnConflict(3)      // tenta novamente em caso de conflito
            );

            if (!response.IsValid)
            {
                throw new Exception($"Erro ao atualizar jogo no Elasticsearch: {response.ServerError?.Error?.Reason}");
            }
        }

        // 5️⃣ Consulta avançada — sugerir jogos baseados no histórico do usuário
        public async Task<IEnumerable<Jogo>> SugerirBaseadoNoHistoricoAsync(IEnumerable<string> historicoTags)
        {
            var response = await _client.SearchAsync<Jogo>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            bs => bs.Terms(t => t.Field(f => f.Tags).Terms(historicoTags)),
                            bs => bs.Match(m => m.Field(f => f.Descricao).Query(string.Join(" ", historicoTags)))
                        )
                    )
                )
                .Sort(ss => ss.Descending(f => f.NumeroVendas))
                .Size(10)
            );

            return response.Documents;
        }

        // 6️⃣ Agregações — métricas de popularidade
        public async Task<IDictionary<string, long>> ObterJogosMaisPopularesAsync(int topN = 5)
        {
            var response = await _client.SearchAsync<Jogo>(s => s
                .Size(0) // sem resultados de documentos, apenas agregações
                .Aggregations(a => a
                    .Terms("mais_populares", t => t
                        .Field(f => f.Categoria.Descricao.Suffix("keyword"))
                        .Order(o => o.Descending("sum_popularidade"))
                        .Aggregations(aa => aa
                            .Sum("sum_popularidade", sa => sa.Field(f => f.NumeroVendas))
                        )
                    )
                )
            );

            var bucket = response.Aggregations.Terms("mais_populares");
            return bucket?.Buckets
                .Take(topN)
                .ToDictionary(b => b.Key, b => (long)b.Sum("sum_popularidade")?.Value);
        }
    }
}
