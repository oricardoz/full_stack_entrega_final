using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFullStack.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarGalpao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GalpaoId",
                table: "Produtos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Galpoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galpoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_GalpaoId",
                table: "Produtos",
                column: "GalpaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Galpoes_GalpaoId",
                table: "Produtos",
                column: "GalpaoId",
                principalTable: "Galpoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Galpoes_GalpaoId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Galpoes");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_GalpaoId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "GalpaoId",
                table: "Produtos");
        }
    }
}
