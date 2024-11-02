using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelfosMachine.Migrations
{
    /// <inheritdoc />
    public partial class Preferencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FatoCliente_Clientes_IdCliente",
                table: "FatoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_FatoCliente_PreferenciasClientes_IdPreferenciaCliente",
                table: "FatoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_FatoCliente_RotinaCuidado_IdRotinaCuidadoCliente",
                table: "FatoCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FatoCliente",
                table: "FatoCliente");

            migrationBuilder.RenameTable(
                name: "FatoCliente",
                newName: "DadosCadastrais");

            migrationBuilder.RenameIndex(
                name: "IX_FatoCliente_IdRotinaCuidadoCliente",
                table: "DadosCadastrais",
                newName: "IX_DadosCadastrais_IdRotinaCuidadoCliente");

            migrationBuilder.RenameIndex(
                name: "IX_FatoCliente_IdPreferenciaCliente",
                table: "DadosCadastrais",
                newName: "IX_DadosCadastrais_IdPreferenciaCliente");

            migrationBuilder.RenameIndex(
                name: "IX_FatoCliente_IdCliente",
                table: "DadosCadastrais",
                newName: "IX_DadosCadastrais_IdCliente");

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Turno",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "PreferenciaHorario",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "PreferenciaDia",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "EnderecoPreferencia",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DadosCadastrais",
                table: "DadosCadastrais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCadastrais_Clientes_IdCliente",
                table: "DadosCadastrais",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCadastrais_PreferenciasClientes_IdPreferenciaCliente",
                table: "DadosCadastrais",
                column: "IdPreferenciaCliente",
                principalTable: "PreferenciasClientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DadosCadastrais_RotinaCuidado_IdRotinaCuidadoCliente",
                table: "DadosCadastrais",
                column: "IdRotinaCuidadoCliente",
                principalTable: "RotinaCuidado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DadosCadastrais_Clientes_IdCliente",
                table: "DadosCadastrais");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosCadastrais_PreferenciasClientes_IdPreferenciaCliente",
                table: "DadosCadastrais");

            migrationBuilder.DropForeignKey(
                name: "FK_DadosCadastrais_RotinaCuidado_IdRotinaCuidadoCliente",
                table: "DadosCadastrais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DadosCadastrais",
                table: "DadosCadastrais");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "PreferenciaHorario");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "PreferenciaDia");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "EnderecoPreferencia");

            migrationBuilder.RenameTable(
                name: "DadosCadastrais",
                newName: "FatoCliente");

            migrationBuilder.RenameIndex(
                name: "IX_DadosCadastrais_IdRotinaCuidadoCliente",
                table: "FatoCliente",
                newName: "IX_FatoCliente_IdRotinaCuidadoCliente");

            migrationBuilder.RenameIndex(
                name: "IX_DadosCadastrais_IdPreferenciaCliente",
                table: "FatoCliente",
                newName: "IX_FatoCliente_IdPreferenciaCliente");

            migrationBuilder.RenameIndex(
                name: "IX_DadosCadastrais_IdCliente",
                table: "FatoCliente",
                newName: "IX_FatoCliente_IdCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FatoCliente",
                table: "FatoCliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FatoCliente_Clientes_IdCliente",
                table: "FatoCliente",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FatoCliente_PreferenciasClientes_IdPreferenciaCliente",
                table: "FatoCliente",
                column: "IdPreferenciaCliente",
                principalTable: "PreferenciasClientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FatoCliente_RotinaCuidado_IdRotinaCuidadoCliente",
                table: "FatoCliente",
                column: "IdRotinaCuidadoCliente",
                principalTable: "RotinaCuidado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
