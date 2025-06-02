using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FiapCloudGamesAPI.Migrations
{
    /// <inheritdoc />
    public partial class StartInsertdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Usuario",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Usuario",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Usuario",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Permissao",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Permissao",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Perfil",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Perfil",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Log",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Jogo",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Jogo",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "EmpresaFornecedora",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "EmpresaFornecedora",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "EmpresaFornecedora",
                type: "VARCHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(50)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Categoria",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Categoria",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "BibliotecaDoJogador",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "BibliotecaDoJogador",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "AtualizadoPor",
                table: "BibliotecaDoJogador",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Avaliacao",
                type: "DATETIME2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Avaliacao",
                type: "DATETIME2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "AtualizadoPor",
                table: "Avaliacao",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "AtualizadoPor", "CriadoPor", "DataAtualizacao", "DataCriacao", "Descricao" },
                values: new object[,]
                {
                    { 1L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ação" },
                    { 2L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aventura" },
                    { 3L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RPG" },
                    { 4L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Estratégia" },
                    { 5L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulação" },
                    { 6L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Esporte" },
                    { 7L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corrida" },
                    { 8L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Puzzle" },
                    { 9L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tiro" },
                    { 10L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luta" },
                    { 11L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plataforma" },
                    { 12L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horror" },
                    { 13L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiplayer" },
                    { 14L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indie" },
                    { 15L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual" },
                    { 16L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aventura Gráfica" },
                    { 17L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roguelike" },
                    { 18L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Battle Royale" },
                    { 19L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MOBA" },
                    { 20L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Card Game" },
                    { 21L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Visual Novel" },
                    { 22L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sandbox" },
                    { 23L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Metroidvania" },
                    { 24L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Survival" },
                    { 25L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Open World" },
                    { 26L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hacking" },
                    { 27L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stealth" },
                    { 28L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Narrativa" },
                    { 29L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Co-op" },
                    { 30L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "VR" },
                    { 31L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AR" },
                    { 32L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Educational" },
                    { 33L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fitness" },
                    { 34L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Music" },
                    { 35L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Party" },
                    { 36L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trivia" },
                    { 37L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Card" },
                    { 38L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Board Game" }
                });

            migrationBuilder.InsertData(
                table: "EmpresaFornecedora",
                columns: new[] { "Id", "AtualizadoPor", "CNPJ", "CriadoPor", "DataAtualizacao", "DataCriacao", "Nome" },
                values: new object[,]
                {
                    { 1L, "", "12345678000190", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GameX Studios" },
                    { 2L, "", "98765432000191", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AlphaGames Ltda" }
                });

            migrationBuilder.InsertData(
                table: "Perfil",
                columns: new[] { "Id", "AtualizadoPor", "CriadoPor", "DataAtualizacao", "DataCriacao", "Descricao" },
                values: new object[,]
                {
                    { 1L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrador" },
                    { 2L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jogador" }
                });

            migrationBuilder.InsertData(
                table: "Permissao",
                columns: new[] { "Id", "AtualizadoPor", "CriadoPor", "DataAtualizacao", "DataCriacao", "Descricao" },
                values: new object[,]
                {
                    { 1L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GerenciarJogos" },
                    { 2L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AvaliarJogos" },
                    { 3L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CriarJogos" },
                    { 4L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ListarUsuarios" },
                    { 5L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CriarUsuarios" },
                    { 6L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AtualizarUsuarios" },
                    { 7L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DeletarUsuarios" },
                    { 8L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GerenciarUsuarios" },
                    { 9L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GerenciarPermissoes" },
                    { 10L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GerenciarPerfil" },
                    { 11L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DeletarJogo" },
                    { 12L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AtualizarJogo" },
                    { 13L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarJogos" },
                    { 14L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarJogoPorId" },
                    { 15L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarEmpresasFornecedoras" },
                    { 16L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CriarEmpresasFornecedoras" },
                    { 17L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarEmpresasFornecedorasPorId" },
                    { 18L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AtualizarEmpresasFornecedoras" },
                    { 19L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DeletarEmpresasFornecedoras" },
                    { 20L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DeletarCategoria" },
                    { 21L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarCategorias" },
                    { 22L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CriarCategoria" },
                    { 23L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarCategoriaPorId" },
                    { 24L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AtualizarCategoria" },
                    { 25L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DeletarBibliotecaDoJogador" },
                    { 26L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarBibliotecasDoJogador" },
                    { 27L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarBibliotecaDoJogadorPorId" },
                    { 28L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CriarBibliotecaDoJogador" },
                    { 29L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarBibliotecaDoJogador" },
                    { 30L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AtualizarBibliotecaDoJogador" },
                    { 31L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DeletarAvaliacao" },
                    { 32L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarAvaliacoes" },
                    { 33L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BuscarAvaliacaoPorId" },
                    { 34L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CriarAvaliacao" },
                    { 35L, "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AtualizarAvaliacao" }
                });

            migrationBuilder.InsertData(
                table: "PerfilPermissao",
                columns: new[] { "IdPerfil", "IdPermissao" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 1L, 2L },
                    { 1L, 3L },
                    { 1L, 4L },
                    { 1L, 5L },
                    { 1L, 6L },
                    { 1L, 7L },
                    { 1L, 8L },
                    { 1L, 9L },
                    { 1L, 10L },
                    { 1L, 11L },
                    { 1L, 12L },
                    { 1L, 13L },
                    { 1L, 14L },
                    { 1L, 15L },
                    { 1L, 16L },
                    { 1L, 17L },
                    { 1L, 18L },
                    { 1L, 19L },
                    { 1L, 20L },
                    { 1L, 21L },
                    { 1L, 22L },
                    { 1L, 23L },
                    { 1L, 24L },
                    { 1L, 25L },
                    { 1L, 26L },
                    { 1L, 27L },
                    { 1L, 28L },
                    { 1L, 29L },
                    { 1L, 30L },
                    { 1L, 31L },
                    { 1L, 32L },
                    { 1L, 33L },
                    { 1L, 34L },
                    { 1L, 35L },
                    { 2L, 2L },
                    { 2L, 27L },
                    { 2L, 28L },
                    { 2L, 29L },
                    { 2L, 30L },
                    { 2L, 31L },
                    { 2L, 32L },
                    { 2L, 34L },
                    { 2L, 35L }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Apelido", "AtualizadoPor", "CriadoPor", "DataAtualizacao", "DataCriacao", "DataNascimento", "Email", "HashSenha", "Nome", "PerfilId", "Sobrenome" },
                values: new object[] { 1L, "joaos", "", "system", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao@email.com", "7+D7gmaWMXRYtMBOLDAtRSgnqJoQ5H62L1setgRLRCx68knp71V1pdUZV6KfWoiT", "João", 1L, "Silva" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "EmpresaFornecedora",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "EmpresaFornecedora",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 2L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 3L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 4L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 5L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 6L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 7L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 8L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 9L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 10L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 11L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 12L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 13L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 14L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 15L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 16L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 17L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 18L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 19L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 20L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 21L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 22L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 23L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 24L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 25L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 26L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 27L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 28L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 29L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 30L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 31L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 32L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 33L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 34L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 1L, 35L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 2L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 27L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 28L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 29L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 30L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 31L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 32L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 34L });

            migrationBuilder.DeleteData(
                table: "PerfilPermissao",
                keyColumns: new[] { "IdPerfil", "IdPermissao" },
                keyValues: new object[] { 2L, 35L });

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Usuario",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Usuario",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Usuario",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Permissao",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Permissao",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Perfil",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Perfil",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Log",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Jogo",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Jogo",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "EmpresaFornecedora",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "EmpresaFornecedora",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "EmpresaFornecedora",
                type: "CHAR(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Categoria",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Categoria",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "BibliotecaDoJogador",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "BibliotecaDoJogador",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AtualizadoPor",
                table: "BibliotecaDoJogador",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Avaliacao",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Avaliacao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AtualizadoPor",
                table: "Avaliacao",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");
        }
    }
}
