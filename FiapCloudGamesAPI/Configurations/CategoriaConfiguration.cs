using FiapCloudGamesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FiapCloudGamesAPI.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("BIGINT").ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(p => p.Descricao).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME2").IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.CriadoPor).HasColumnType("VARCHAR(100)").Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(p => p.DataAtualizacao).HasColumnType("DATETIME2");
            builder.Property(p => p.AtualizadoPor).HasColumnType("VARCHAR(100)");

            builder.HasData(
                new Categoria("Ação", "system") { Id = 1, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Aventura", "system") { Id = 2, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("RPG", "system") { Id = 3, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Estratégia", "system") { Id = 4, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Simulação", "system") { Id = 5, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Esporte", "system") { Id = 6, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Corrida", "system") { Id = 7, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Puzzle", "system") { Id = 8, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Tiro", "system") { Id = 9, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Luta", "system") { Id = 10, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Plataforma", "system") { Id = 11, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Horror", "system") { Id = 12, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Multiplayer", "system") { Id = 13, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Indie", "system") { Id = 14, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Casual", "system") { Id = 15, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Aventura Gráfica", "system") { Id = 16, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Roguelike", "system") { Id = 17, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Battle Royale", "system") { Id = 18, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("MOBA", "system") { Id = 19, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Card Game", "system") { Id = 20, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Visual Novel", "system") { Id = 21, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Sandbox", "system") { Id = 22, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Metroidvania", "system") { Id = 23, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Survival", "system") { Id = 24, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Open World", "system") { Id = 25, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Hacking", "system") { Id = 26, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Stealth", "system") { Id = 27, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Narrativa", "system") { Id = 28, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Co-op", "system") { Id = 29, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("VR", "system") { Id = 30, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("AR", "system") { Id =31, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Educational", "system") { Id = 32, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Fitness", "system") { Id = 33, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Music", "system") { Id = 34, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Party", "system") { Id = 35, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Trivia", "system") { Id = 36, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Card", "system") { Id = 37, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") },
                new Categoria("Board Game", "system") { Id = 38, DataCriacao = DateTime.Parse("2025-01-01 00:00:00") }
            );
        }
    }
}
