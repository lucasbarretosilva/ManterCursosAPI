using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManterCursosAPI.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Categoria_CategoriaId",
                table: "Curso");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Curso",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Categoria_CategoriaId",
                table: "Curso",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Categoria_CategoriaId",
                table: "Curso");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Curso",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Categoria_CategoriaId",
                table: "Curso",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
