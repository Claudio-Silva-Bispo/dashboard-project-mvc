using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelfosMachine.Migrations
{
    /// <inheritdoc />
    public partial class RotinaCuidado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RotinaCuidado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    HistoricoMedico = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FrequenciaEscovacao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FrequenciaFioDental = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FrequenciaEnxaguante = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SintomasAtuais = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HabitosAlimentares = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FrequenciaVisitasDentista = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CuidadosEspecificos = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotinaCuidado", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RotinaCuidado");
        }
    }
}
