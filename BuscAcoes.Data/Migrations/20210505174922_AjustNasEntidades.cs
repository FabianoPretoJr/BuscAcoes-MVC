using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuscAcoes.Data.Migrations
{
    public partial class AjustNasEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ToleranciaMaxima",
                table: "ClienteAcoes");

            migrationBuilder.DropColumn(
                name: "ToleranciaMinima",
                table: "ClienteAcoes");

            migrationBuilder.DropColumn(
                name: "ToleranciaMaxima",
                table: "Alertas");

            migrationBuilder.DropColumn(
                name: "ToleranciaMinima",
                table: "Alertas");

            migrationBuilder.AddColumn<double>(
                name: "PercentualVariacaoCalculada",
                table: "Monitoramentos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Tolerancia",
                table: "ClienteAcoes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Tolerencia",
                table: "Alertas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataNasc",
                value: new DateTime(2021, 5, 5, 14, 49, 21, 194, DateTimeKind.Local).AddTicks(9102));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 14, 49, 21, 193, DateTimeKind.Local).AddTicks(2058));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 14, 49, 21, 194, DateTimeKind.Local).AddTicks(6743));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 14, 49, 21, 194, DateTimeKind.Local).AddTicks(6784));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentualVariacaoCalculada",
                table: "Monitoramentos");

            migrationBuilder.DropColumn(
                name: "Tolerancia",
                table: "ClienteAcoes");

            migrationBuilder.DropColumn(
                name: "Tolerencia",
                table: "Alertas");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ToleranciaMaxima",
                table: "ClienteAcoes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ToleranciaMinima",
                table: "ClienteAcoes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ToleranciaMaxima",
                table: "Alertas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ToleranciaMinima",
                table: "Alertas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataNasc", "Nome" },
                values: new object[] { new DateTime(2021, 5, 5, 14, 31, 12, 967, DateTimeKind.Local).AddTicks(6845), "Claudio" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 14, 31, 12, 966, DateTimeKind.Local).AddTicks(983));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 14, 31, 12, 967, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2021, 5, 5, 14, 31, 12, 967, DateTimeKind.Local).AddTicks(4523));
        }
    }
}
