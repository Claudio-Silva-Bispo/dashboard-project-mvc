using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelfosMachine.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Genero = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataNasc = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnderecoPreferencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CEP = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Rua = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Complemento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoPreferencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenciaDia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DiaSemana = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciaDia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenciaHorario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Horario = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciaHorario", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Turno = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenciasClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdEndereco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdTurno = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdHorario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdDiaSemana = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenciasClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreferenciasClientes_EnderecoPreferencia_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "EnderecoPreferencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenciasClientes_PreferenciaDia_IdDiaSemana",
                        column: x => x.IdDiaSemana,
                        principalTable: "PreferenciaDia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenciasClientes_PreferenciaHorario_IdHorario",
                        column: x => x.IdHorario,
                        principalTable: "PreferenciaHorario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenciasClientes_Turno_IdTurno",
                        column: x => x.IdTurno,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasClientes_IdDiaSemana",
                table: "PreferenciasClientes",
                column: "IdDiaSemana");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasClientes_IdEndereco",
                table: "PreferenciasClientes",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasClientes_IdHorario",
                table: "PreferenciasClientes",
                column: "IdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenciasClientes_IdTurno",
                table: "PreferenciasClientes",
                column: "IdTurno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PreferenciasClientes");

            migrationBuilder.DropTable(
                name: "RotinaCuidado");

            migrationBuilder.DropTable(
                name: "EnderecoPreferencia");

            migrationBuilder.DropTable(
                name: "PreferenciaDia");

            migrationBuilder.DropTable(
                name: "PreferenciaHorario");

            migrationBuilder.DropTable(
                name: "Turno");
        }
    }
}
