using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFullStack.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Produtos",
                newName: "ValorUnitario");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Produtos",
                newName: "DataCadastro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorUnitario",
                table: "Produtos",
                newName: "Preco");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "Produtos",
                newName: "Descricao");
        }
    }
}
