using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIProject.Migrations
{
    /// <inheritdoc />
    public partial class ConceitoDetalhes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ConceitoDetalhes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subtitulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConceitoId = table.Column<int>(type: "int", nullable: false),
                    ConceptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceitoDetalhes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConceitoDetalhes_Conceito_ConceptId",
                        column: x => x.ConceptId,
                        principalTable: "Conceito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConceitoDetalhes_ConceptId",
                table: "ConceitoDetalhes",
                column: "ConceptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceitoDetalhes");

            migrationBuilder.DropTable(
                name: "Conceito");
        }
    }
}
