using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIProject.Migrations
{
    /// <inheritdoc />
    public partial class LoginHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Usuario_UsuarioId",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Login");

            migrationBuilder.RenameColumn(
                name: "IdLogin",
                table: "Login",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Login",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Usuario_UsuarioId",
                table: "Login",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Usuario_UsuarioId",
                table: "Login");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Login",
                newName: "IdLogin");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Login",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Usuario_UsuarioId",
                table: "Login",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
