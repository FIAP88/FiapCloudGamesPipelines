using FiapCloudGamesAPI.Enums;
using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class PerfilPermissaoConfiguration : IEntityTypeConfiguration<PerfilPermissao>
    {
        public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
        {
           
            builder.ToTable("PerfilPermissao");
            builder.HasKey(p => new { p.IdPerfil, p.IdPermissao });
            builder.Property(p => p.IdPerfil).HasColumnType("BIGINT").IsRequired();
            builder.Property(p => p.IdPermissao).HasColumnType("BIGINT").IsRequired();

            builder.HasOne(p => p.Perfil)
            .WithMany(p => p.PerfilPermissoes)
            .HasForeignKey(p => p.IdPerfil);

            builder.HasOne(p => p.Permissao)
            .WithMany(p => p.PerfilPermissoes)
            .HasForeignKey(p => p.IdPermissao);

            var perfilAdminPermissoes = Enum.GetValues(typeof(PermissoesEnum)).Cast<PermissoesEnum>().Select(
               p => new PerfilPermissao(1, (long)p))
            .ToList();

            var perfilJogadorPermissoes = new List<PerfilPermissao>
            {
                new PerfilPermissao(2, (long)PermissoesEnum.AvaliarJogos),
                new PerfilPermissao(2, (long)PermissoesEnum.DeletarAvaliacao),
                new PerfilPermissao(2, (long)PermissoesEnum.BuscarAvaliacoes),
                new PerfilPermissao(2, (long)PermissoesEnum.CriarAvaliacao),
                new PerfilPermissao(2, (long)PermissoesEnum.AtualizarAvaliacao),
                new PerfilPermissao(2, (long)PermissoesEnum.BuscarJogos),
                new PerfilPermissao(2, (long)PermissoesEnum.BuscarJogoPorId),
                new PerfilPermissao(2, (long)PermissoesEnum.MeusJogos),
                new PerfilPermissao(2, (long)PermissoesEnum.AdicionarJogo)
            };

            builder.HasData(perfilAdminPermissoes);
            builder.HasData(perfilJogadorPermissoes);
        }
    }
}
