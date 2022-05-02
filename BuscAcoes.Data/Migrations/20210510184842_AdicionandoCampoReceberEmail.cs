using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuscAcoes.Data.Migrations
{
    public partial class AdicionandoCampoReceberEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReceberEmail",
                table: "ClienteAcoes",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceberEmail",
                table: "ClienteAcoes");

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataNasc",
                value: new DateTime(2021, 5, 5, 15, 37, 19, 230, DateTimeKind.Local).AddTicks(4232));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 15, 37, 19, 228, DateTimeKind.Local).AddTicks(6714));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 15, 37, 19, 230, DateTimeKind.Local).AddTicks(1579));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 15, 37, 19, 230, DateTimeKind.Local).AddTicks(1622));
        }
    }
}
