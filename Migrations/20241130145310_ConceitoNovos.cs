using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIProject.Migrations
{
    /// <inheritdoc />
    public partial class ConceitoNovos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConceitoDetalhes_Conceito_ConceptId",
                table: "ConceitoDetalhes");

            migrationBuilder.DropTable(
                name: "Conceito");

            migrationBuilder.RenameColumn(
                name: "ConceptId",
                table: "ConceitoDetalhes",
                newName: "ConceptTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_ConceitoDetalhes_ConceptId",
                table: "ConceitoDetalhes",
                newName: "IX_ConceitoDetalhes_ConceptTitleId");

            migrationBuilder.CreateTable(
                name: "ConceitoTitulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceitoTitulo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConceitoDetalhes_ConceitoTitulo_ConceptTitleId",
                table: "ConceitoDetalhes",
                column: "ConceptTitleId",
                principalTable: "ConceitoTitulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConceitoDetalhes_ConceitoTitulo_ConceptTitleId",
                table: "ConceitoDetalhes");

            migrationBuilder.DropTable(
                name: "ConceitoTitulo");

            migrationBuilder.RenameColumn(
                name: "ConceptTitleId",
                table: "ConceitoDetalhes",
                newName: "ConceptId");

            migrationBuilder.RenameIndex(
                name: "IX_ConceitoDetalhes_ConceptTitleId",
                table: "ConceitoDetalhes",
                newName: "IX_ConceitoDetalhes_ConceptId");

            migrationBuilder.CreateTable(
                name: "Conceito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conceito", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConceitoDetalhes_Conceito_ConceptId",
                table: "ConceitoDetalhes",
                column: "ConceptId",
                principalTable: "Conceito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
