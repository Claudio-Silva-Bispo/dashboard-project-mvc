using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIProject.Migrations
{
    /// <inheritdoc />
    public partial class SeparacaoTitulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConceitoDetalhes_ConceitoTitulo_ConceptTitleId",
                table: "ConceitoDetalhes");

            migrationBuilder.DropIndex(
                name: "IX_ConceitoDetalhes_ConceptTitleId",
                table: "ConceitoDetalhes");

            migrationBuilder.DropColumn(
                name: "ConceitoId",
                table: "ConceitoDetalhes");

            migrationBuilder.RenameColumn(
                name: "ConceptTitleId",
                table: "ConceitoDetalhes",
                newName: "IdTitulo");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "ConceitoDetalhes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "ConceitoDetalhes");

            migrationBuilder.RenameColumn(
                name: "IdTitulo",
                table: "ConceitoDetalhes",
                newName: "ConceptTitleId");

            migrationBuilder.AddColumn<int>(
                name: "ConceitoId",
                table: "ConceitoDetalhes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConceitoDetalhes_ConceptTitleId",
                table: "ConceitoDetalhes",
                column: "ConceptTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConceitoDetalhes_ConceitoTitulo_ConceptTitleId",
                table: "ConceitoDetalhes",
                column: "ConceptTitleId",
                principalTable: "ConceitoTitulo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
