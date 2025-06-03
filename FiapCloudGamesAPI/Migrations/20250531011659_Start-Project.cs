using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGamesAPI.Migrations
{
    /// <inheritdoc />
    public partial class StartProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaFornecedora",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    CNPJ = table.Column<string>(type: "CHAR(50)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaFornecedora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mensagem = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    Tamanho = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    Preco = table.Column<int>(type: "INT", nullable: false),
                    IdCategoria = table.Column<long>(type: "BIGINT", nullable: false),
                    IdadeMinima = table.Column<int>(type: "INT", nullable: false),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    IdFornecedor = table.Column<long>(type: "BIGINT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogo_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jogo_EmpresaFornecedora_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "EmpresaFornecedora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Sobrenome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Apelido = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    HashSenha = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    PerfilId = table.Column<long>(type: "BIGINT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Perfil_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilPermissao",
                columns: table => new
                {
                    IdPerfil = table.Column<long>(type: "BIGINT", nullable: false),
                    IdPermissao = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilPermissao", x => new { x.IdPerfil, x.IdPermissao });
                    table.ForeignKey(
                        name: "FK_PerfilPermissao_Perfil_IdPerfil",
                        column: x => x.IdPerfil,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilPermissao_Permissao_IdPermissao",
                        column: x => x.IdPermissao,
                        principalTable: "Permissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<long>(type: "BIGINT", nullable: false),
                    IdJogo = table.Column<long>(type: "BIGINT", nullable: false),
                    Nota = table.Column<int>(type: "INT", nullable: false),
                    Comentario = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Jogo_IdJogo",
                        column: x => x.IdJogo,
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BibliotecaDoJogador",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<long>(type: "BIGINT", nullable: false),
                    IdJogo = table.Column<long>(type: "BIGINT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CriadoPor = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecaDoJogador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BibliotecaDoJogador_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BibliotecaDoJogadorJogo",
                columns: table => new
                {
                    BibliotecasId = table.Column<long>(type: "BIGINT", nullable: false),
                    JogosId = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecaDoJogadorJogo", x => new { x.BibliotecasId, x.JogosId });
                    table.ForeignKey(
                        name: "FK_BibliotecaDoJogadorJogo_BibliotecaDoJogador_BibliotecasId",
                        column: x => x.BibliotecasId,
                        principalTable: "BibliotecaDoJogador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibliotecaDoJogadorJogo_Jogo_JogosId",
                        column: x => x.JogosId,
                        principalTable: "Jogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_IdJogo",
                table: "Avaliacao",
                column: "IdJogo");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_IdUsuario",
                table: "Avaliacao",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaDoJogador_IdUsuario",
                table: "BibliotecaDoJogador",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaDoJogadorJogo_JogosId",
                table: "BibliotecaDoJogadorJogo",
                column: "JogosId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_IdCategoria",
                table: "Jogo",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_IdFornecedor",
                table: "Jogo",
                column: "IdFornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilPermissao_IdPermissao",
                table: "PerfilPermissao",
                column: "IdPermissao");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PerfilId",
                table: "Usuario",
                column: "PerfilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "BibliotecaDoJogadorJogo");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "PerfilPermissao");

            migrationBuilder.DropTable(
                name: "BibliotecaDoJogador");

            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "EmpresaFornecedora");

            migrationBuilder.DropTable(
                name: "Perfil");
        }
    }
}
