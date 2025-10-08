using fiapcloudgames.usuario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fiapcloudgames.usuario.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.PrimeiroNome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Sobrenome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Apelido)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.DataNascimento)
                .IsRequired();

            builder.Property(c => c.HashSenha)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.DataCadastro)
                .IsRequired();

            builder.Property(c => c.DataAtualizacao);

            builder.Property(c => c.AtualizadoPor);

            //builder.HasData(
            //    new Usuario("João", "Silva", "joaos", "joao@email.com", "31999999999", DateTime.Parse("2000-01-01 00:00:00"), "7+D7gmaWMXRYtMBOLDAtRSgnqJoQ5H62L1setgRLRCx68knp71V1pdUZV6KfWoiT", DateTime.UtcNow, null, "system"),
            //    new Usuario("Gabriel", "Silva", "gabriel", "gabriel@email.com", "31999999999", DateTime.Parse("2000-01-01 00:00:00"), "7+D7gmaWMXRYtMBOLDAtRSgnqJoQ5H62L1setgRLRCx68knp71V1pdUZV6KfWoiT", DateTime.UtcNow, null, "system")
            //);
        }
    }
}
