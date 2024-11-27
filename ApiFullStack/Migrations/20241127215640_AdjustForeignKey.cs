using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFullStack.Migrations
{
    /// <inheritdoc />
    public partial class AdjustForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Galpoes_GalpaoId",
                table: "Produtos");

            migrationBuilder.AlterColumn<long>(
                name: "GalpaoId",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Galpoes_GalpaoId",
                table: "Produtos",
                column: "GalpaoId",
                principalTable: "Galpoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Galpoes_GalpaoId",
                table: "Produtos");

            migrationBuilder.AlterColumn<long>(
                name: "GalpaoId",
                table: "Produtos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Galpoes_GalpaoId",
                table: "Produtos",
                column: "GalpaoId",
                principalTable: "Galpoes",
                principalColumn: "Id");
        }
    }
}
