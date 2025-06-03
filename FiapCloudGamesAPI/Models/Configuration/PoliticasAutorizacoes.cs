using Microsoft.AspNetCore.Authorization;

namespace FiapCloudGamesAPI.Models.Configuration
{
    public static class PoliticasAutorizacoes
    {
        public static void PoliticasCustomizadas(this AuthorizationOptions options)
        {

            CriarPoliticaUsuario(options);
            CriarPoliticaPermissoes(options);
            CriarPoliticaPerfil(options);
            CriarPoliticaJogos(options);
            CriarPoliticaEmpresasFornecedoras(options);
            CriarPoliticaCategoria(options);
            CriarPoliticaAvaliacao(options);
        }

        #region CriarPoliticaUsuario
        private static void CriarPoliticaUsuario(AuthorizationOptions options)
        {
            options.AddPolicy("GerenciarUsuarios", policy =>
                policy.RequireClaim("permission", "GerenciarUsuarios"));
        }
        #endregion

        #region CriarPoliticaPermissoes

        private static void CriarPoliticaPermissoes(AuthorizationOptions options)
        {
            options.AddPolicy("GerenciarPermissoes", policy =>
                policy.RequireClaim("permission", "GerenciarPermissoes"));
        }
        #endregion

        #region CriarPoliticaPerfil

        private static void CriarPoliticaPerfil(AuthorizationOptions options)
        {
            options.AddPolicy("GerenciarPerfil", policy =>
                policy.RequireClaim("permission", "GerenciarPerfil"));
        }
        #endregion

        #region CriarPoliticaJogos

        private static void CriarPoliticaJogos(AuthorizationOptions options)
        {
            options.AddPolicy("CriarJogos", policy =>
                policy.RequireClaim("permission", "CriarJogos"));

            options.AddPolicy("DeletarJogo", policy =>
                policy.RequireClaim("permission", "DeletarJogo"));

            options.AddPolicy("AtualizarJogo", policy =>
                policy.RequireClaim("permission", "AtualizarJogo"));

            options.AddPolicy("BuscarJogos", policy =>
                policy.RequireClaim("permission", "BuscarJogos"));

            options.AddPolicy("BuscarJogoPorId", policy =>
                policy.RequireClaim("permission", "BuscarJogoPorId"));

            options.AddPolicy("MeusJogos", policy =>
                policy.RequireClaim("permission", "MeusJogos"));

            options.AddPolicy("AdicionarJogo", policy =>
                policy.RequireClaim("permission", "AdicionarJogo"));
        }
        #endregion

        #region CriarPoliticaEmpresasFornecedoras
        private static void CriarPoliticaEmpresasFornecedoras(AuthorizationOptions options)
        {
            options.AddPolicy("BuscarEmpresasFornecedoras", policy =>
                policy.RequireClaim("permission", "BuscarEmpresasFornecedoras"));

            options.AddPolicy("CriarEmpresasFornecedoras", policy =>
                policy.RequireClaim("permission", "CriarEmpresasFornecedoras"));

            options.AddPolicy("BuscarEmpresasFornecedorasPorId", policy =>
                policy.RequireClaim("permission", "BuscarEmpresasFornecedorasPorId"));

            options.AddPolicy("AtualizarEmpresasFornecedoras", policy =>
                policy.RequireClaim("permission", "AtualizarEmpresasFornecedoras"));

            options.AddPolicy("DeletarEmpresasFornecedoras", policy =>
                policy.RequireClaim("permission", "DeletarEmpresasFornecedoras"));
        }
        #endregion

        #region CriarPoliticaCategoria
        private static void CriarPoliticaCategoria(AuthorizationOptions options)
        {
            options.AddPolicy("BuscarCategorias", policy =>
                policy.RequireClaim("permission", "BuscarCategorias"));

            options.AddPolicy("CriarCategoria", policy =>
                policy.RequireClaim("permission", "CriarCategoria"));

            options.AddPolicy("BuscarCategoriaPorId", policy =>
                policy.RequireClaim("permission", "BuscarCategoriaPorId"));

            options.AddPolicy("AtualizarCategoria", policy =>
                policy.RequireClaim("permission", "AtualizarCategoria"));

            options.AddPolicy("DeletarCategoria", policy =>
                policy.RequireClaim("permission", "DeletarCategoria"));
        }
        #endregion

        #region CriarPoliticaAvaliacao
        private static void CriarPoliticaAvaliacao(AuthorizationOptions options)
        {
            options.AddPolicy("BuscarAvaliacoes", policy =>
                policy.RequireClaim("permission", "BuscarAvaliacoes"));

            options.AddPolicy("BuscarAvaliacaoPorId", policy =>
                policy.RequireClaim("permission", "BuscarAvaliacaoPorId"));

            options.AddPolicy("CriarAvaliacao", policy =>
                policy.RequireClaim("permission", "CriarAvaliacao"));

            options.AddPolicy("AtualizarAvaliacao", policy =>
                policy.RequireClaim("permission", "AtualizarAvaliacao"));

            options.AddPolicy("DeletarAvaliacao", policy =>
                policy.RequireClaim("permission", "DeletarAvaliacao"));
        }
        #endregion

    }
}
