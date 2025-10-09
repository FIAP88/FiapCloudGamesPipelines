using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fiapcloudgames.usuario.Infrastructure.Migrations.ReadModelDb
{
    /// <inheritdoc />
    public partial class CreateUserAuthReadModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioLogin",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    HashSenha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogin", x => x.UsuarioId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogin_Email",
                table: "UsuarioLogin",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioLogin");
        }
    }
}
