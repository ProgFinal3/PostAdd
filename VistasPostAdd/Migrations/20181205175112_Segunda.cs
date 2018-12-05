using Microsoft.EntityFrameworkCore.Migrations;

namespace VistasPostAdd.Migrations
{
    public partial class Segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Anuncio_AnuncioId",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_AnuncioId",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "AnuncioId",
                table: "Categoria");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Anuncio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_CategoriaId",
                table: "Anuncio",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anuncio_Categoria_CategoriaId",
                table: "Anuncio",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anuncio_Categoria_CategoriaId",
                table: "Anuncio");

            migrationBuilder.DropIndex(
                name: "IX_Anuncio_CategoriaId",
                table: "Anuncio");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Anuncio");

            migrationBuilder.AddColumn<int>(
                name: "AnuncioId",
                table: "Categoria",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_AnuncioId",
                table: "Categoria",
                column: "AnuncioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Anuncio_AnuncioId",
                table: "Categoria",
                column: "AnuncioId",
                principalTable: "Anuncio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
