using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuscAcoes.Data.Migrations
{
    public partial class AjuteDSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2021, 5, 5, 15, 37, 19, 228, DateTimeKind.Local).AddTicks(6714), "e10adc3949ba59abbe56e057f20f883e" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2021, 5, 5, 15, 37, 19, 230, DateTimeKind.Local).AddTicks(1579), "e10adc3949ba59abbe56e057f20f883e" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2021, 5, 5, 15, 37, 19, 230, DateTimeKind.Local).AddTicks(1622), "e10adc3949ba59abbe56e057f20f883e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2021, 5, 5, 14, 49, 21, 193, DateTimeKind.Local).AddTicks(2058), "123456" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2021, 5, 5, 14, 49, 21, 194, DateTimeKind.Local).AddTicks(6743), "123456" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DataCriacao", "Senha" },
                values: new object[] { new DateTime(2021, 5, 5, 14, 49, 21, 194, DateTimeKind.Local).AddTicks(6784), "123456" });
        }
    }
}
