using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuscAcoes.Data.Migrations
{
    public partial class AdicionandoAtivoEmSetor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Setores",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Setores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataNasc",
                value: new DateTime(2021, 5, 17, 13, 23, 48, 569, DateTimeKind.Local).AddTicks(2035));

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 4,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 5,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 6,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 7,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 8,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 9,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 10,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 11,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 17, 13, 23, 48, 566, DateTimeKind.Local).AddTicks(8300));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 17, 13, 23, 48, 568, DateTimeKind.Local).AddTicks(9042));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 17, 13, 23, 48, 568, DateTimeKind.Local).AddTicks(9113));

            migrationBuilder.CreateIndex(
                name: "IX_Setores_Nome",
                table: "Setores",
                column: "Nome",
                unique: true,
                filter: "[Nome] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Setores_Nome",
                table: "Setores");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Setores");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Setores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataNasc",
                value: new DateTime(2021, 5, 10, 15, 48, 41, 406, DateTimeKind.Local).AddTicks(1695));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 10, 15, 48, 41, 404, DateTimeKind.Local).AddTicks(1525));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 10, 15, 48, 41, 405, DateTimeKind.Local).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 10, 15, 48, 41, 405, DateTimeKind.Local).AddTicks(8453));
        }
    }
}
