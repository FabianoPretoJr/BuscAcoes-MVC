using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuscAcoes.Data.Migrations
{
    public partial class correcoes_historia_82 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feriado",
                table: "DiasIndisponiveis");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuarios",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Monitoramentos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresas",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Clientes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoNegociacao",
                table: "Acoes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true,
                filter: "[Login] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Cnpj",
                table: "Empresas",
                column: "Cnpj",
                unique: true,
                filter: "[Cnpj] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DiasIndisponiveis_Data",
                table: "DiasIndisponiveis",
                column: "Data",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Documento",
                table: "Clientes",
                column: "Documento",
                unique: true,
                filter: "[Documento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Email",
                table: "Clientes",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Acoes_CodigoNegociacao",
                table: "Acoes",
                column: "CodigoNegociacao",
                unique: true,
                filter: "[CodigoNegociacao] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_Cnpj",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_DiasIndisponiveis_Data",
                table: "DiasIndisponiveis");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_Documento",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_Email",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Acoes_CodigoNegociacao",
                table: "Acoes");

            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Monitoramentos");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Feriado",
                table: "DiasIndisponiveis",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoNegociacao",
                table: "Acoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
