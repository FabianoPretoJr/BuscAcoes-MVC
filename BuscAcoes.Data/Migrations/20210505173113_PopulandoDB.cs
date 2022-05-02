using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuscAcoes.Data.Migrations
{
    public partial class PopulandoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Parametros",
                columns: new[] { "Nome", "Valor" },
                values: new object[,]
                {
                    { "HorarioAbertura", "10:00:00" },
                    { "HorarioFechamento", "17:00:00" },
                    { "MonitoramentoMinutos", "00:10:00" },
                    { "EndpointApi", "https://api.hgbrasil.com/finance/stock_price?fields=only_results,price,change_percent,updated_at&symbol=" },
                    { "ChaveApi1", "78c45fea" }
                });

            migrationBuilder.InsertData(
                table: "Perfils",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Operador" },
                    { 3, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "Setores",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 9, "Tecnologia da Informação" },
                    { 8, "Saúde" },
                    { 7, "Petróleo. Gás e Biocombustíveis" },
                    { 6, "Outros" },
                    { 2, "Comunicações" },
                    { 4, "Consumo não Cíclico" },
                    { 3, "Consumo Cíclico" },
                    { 10, "Utilidade Pública" },
                    { 1, "Bens Industriais" },
                    { 5, "Financeiro" },
                    { 11, "Materiais Básicos" }
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Ativo", "Cnpj", "Nome", "SetorId" },
                values: new object[,]
                {
                    { 11, true, "07.816.890/0001-53", "MULTIPLAN", 5 },
                    { 16, true, "61.186.680/0001-74", "BANCO BMG", 5 },
                    { 17, true, "00.416.968/0001-01", "BANCO INTER", 5 },
                    { 19, true, "17.344.597/0001-94", "BBSEGURIDADE", 5 },
                    { 20, true, "28.195.667/0001-06", "ABC BRASIL", 5 },
                    { 21, true, "30.306.294/0001-45", "BTGP BANCO", 5 },
                    { 22, true, "00.000.000/0001-91", "BRASIL", 5 },
                    { 23, true, "60.746.948/0001-12", "BRADESCO", 5 },
                    { 26, true, "33.592.510/0001-54", "VALE", 6 },
                    { 36, true, "33.958.695/0001-78", "UNIPAR", 6 },
                    { 45, true, "00.359.742/0001-08", "ATOMPAR", 7 },
                    { 46, true, "02.796.775/0001-40", "GAMA PART", 7 },
                    { 47, true, "01.548.981/0001-79", "INVEST BEMGE", 7 },
                    { 1, true, "33.000.167/0001-01", "PETROBRAS", 8 },
                    { 18, true, "61.374.161/0001-30", "BAUMER", 9 },
                    { 37, true, "06.626.253/0001-51", "PAGUE MENOS", 9 },
                    { 38, true, "61.585.865/0001-51", "RAIADROGASIL", 9 },
                    { 3, true, "81.243.735/0001-48", "POSITIVO TEC", 10 },
                    { 8, true, "14.110.585/0001-07", "MELIUZ", 10 },
                    { 25, true, "36.542.025/0001-64", "BRQ", 10 },
                    { 39, true, "06.948.969/0001-75", "LINX", 10 },
                    { 40, true, "31.553.627/0001-01", "MOBLY", 10 },
                    { 41, true, "35.791.391/0001-94", "QUALITY SOFT", 10 },
                    { 42, true, "53.113.791/0001-22", "TOTVS", 10 },
                    { 14, true, "09.346.601/0001-25", "B3", 5 },
                    { 43, true, "01.104.937/0001-70", "ELETROPAR", 11 },
                    { 44, true, "02.328.280/0001-97", "ELEKTRO", 11 },
                    { 35, true, "75.315.333/0001-09", "CARREFOUR BR", 4 },
                    { 12, true, "01.417.222/0001-77", "MRS LOGIST", 1 },
                    { 27, true, "33.113.309/0001-47", "VALID", 1 },
                    { 49, true, "07.689.002/0001-89", "EMBRAER", 1 },
                    { 50, true, "92.781.335/0001-02", "TAURUS ARMAS", 1 },
                    { 30, true, "76.535.764/0001-43", "OI", 2 },
                    { 31, true, "00.336.701/0001-04", "TELEBRAS", 2 },
                    { 32, true, "02.421.421/0001-11", "TIM", 2 },
                    { 4, true, "18.328.118/0001-09", "PETZ", 3 },
                    { 5, true, "08.795.211/0001-70", "MAESTROLOC", 3 },
                    { 2, true, "02.149.205/0001-69", "PORTO SEGURO", 5 },
                    { 6, true, "47.960.950/0001-21", "MAGAZ LUIZA", 3 },
                    { 9, true, "88.610.191/0001-54", "MUNDIAL", 3 },
                    { 10, true, "08.343.492/0001-20", "MRV", 3 },
                    { 13, true, "00.776.574/0001-56", "B2W DIGITAL", 3 },
                    { 15, true, "45.987.245/0001-92", "BAHEMA", 3 },
                    { 24, true, "13.574.594/0001-96", "BK BRASIL", 3 },
                    { 28, true, "33.041.260/0652-90", "VIAVAREJO", 3 },
                    { 29, true, "33.839.910/0001-11", "VIVARA S.A.", 3 },
                    { 48, true, "62.002.886/0001-60", "SPTURIS", 3 },
                    { 33, true, "64.904.295/0001-03", "CAMIL", 4 },
                    { 34, true, "07.526.557/0001-00", "ABEV3", 4 },
                    { 7, true, "61.189.288/0001-89", "LOJAS MARISA", 3 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "DataCriacao", "Login", "Nome", "PerfilId", "Senha" },
                values: new object[,]
                {
                    { 3, true, new DateTime(2021, 5, 5, 14, 31, 12, 967, DateTimeKind.Local).AddTicks(4523), "cliente", "Cliente", 3, "123456" },
                    { 2, true, new DateTime(2021, 5, 5, 14, 31, 12, 967, DateTimeKind.Local).AddTicks(4485), "opera", "Operador", 2, "123456" },
                    { 1, true, new DateTime(2021, 5, 5, 14, 31, 12, 966, DateTimeKind.Local).AddTicks(983), "Admin", "Administrador", 1, "123456" }
                });

            migrationBuilder.InsertData(
                table: "Acoes",
                columns: new[] { "Id", "Ativo", "CodigoNegociacao", "EmpresaId" },
                values: new object[,]
                {
                    { 21, true, "BIDI3", 17 },
                    { 26, true, "ABCB4", 20 },
                    { 27, true, "BPAC11", 21 },
                    { 28, true, "BPAC3", 21 },
                    { 29, true, "BPAC5", 21 },
                    { 30, true, "BBAS11", 22 },
                    { 31, true, "BBAS12", 22 },
                    { 32, true, "BBAS3", 22 },
                    { 33, true, "BBDC3", 23 },
                    { 34, true, "BBDC4", 23 },
                    { 37, true, "VALE3", 26 },
                    { 49, true, "UNIP3", 36 },
                    { 50, true, "UNIP5", 36 },
                    { 51, true, "UNIP6", 36 },
                    { 61, true, "ATOM3", 45 },
                    { 62, true, "OPGM3B", 46 },
                    { 63, true, "FIGE3", 47 },
                    { 64, true, "FIGE4", 47 },
                    { 58, true, "LIPR3", 43 },
                    { 57, true, "TOTS3", 42 },
                    { 56, true, "QUSW3", 41 },
                    { 55, true, "MBLY3", 40 },
                    { 54, true, "LINX3", 39 },
                    { 36, true, "BRQB3", 25 },
                    { 25, true, "BBSE3", 19 },
                    { 9, true, "CASH3", 8 },
                    { 53, true, "RADL3", 38 },
                    { 52, true, "PGMN3", 37 },
                    { 24, true, "BALM4", 18 },
                    { 23, true, "BALM3", 18 },
                    { 2, true, "PETR4", 1 },
                    { 1, true, "PETR3", 1 },
                    { 4, true, "POSI3", 3 },
                    { 22, true, "BIDI4", 17 },
                    { 60, true, "EKTR4", 44 },
                    { 20, true, "BIDI11", 17 },
                    { 6, true, "MSRO3", 5 },
                    { 5, true, "PETZ3", 4 },
                    { 45, true, "TIMS3", 32 },
                    { 44, true, "TELB4", 31 },
                    { 43, true, "TELB3", 31 },
                    { 42, true, "OIBR4", 30 },
                    { 7, true, "MGLU3", 6 },
                    { 41, true, "OIBR3", 30 },
                    { 69, true, "TASA3", 50 },
                    { 68, true, "EMBR3", 49 },
                    { 38, true, "VLID3", 27 },
                    { 15, true, "MRSA6B", 12 },
                    { 14, true, "MRSA5B", 12 },
                    { 13, true, "MRSA3B", 12 },
                    { 70, true, "TASA4", 50 },
                    { 59, true, "EKTR3", 44 },
                    { 8, true, "AMAR3", 7 },
                    { 11, true, "MRVE3", 10 },
                    { 19, true, "BMGB4", 16 },
                    { 17, true, "B3SA3", 14 },
                    { 12, true, "MULT3", 11 },
                    { 3, true, "PSSA3", 2 },
                    { 47, true, "ABEV3", 34 },
                    { 46, true, "CAML3", 33 },
                    { 10, true, "MNDL3", 9 },
                    { 67, true, "AHEB6", 48 },
                    { 65, true, "AHEB3", 48 },
                    { 40, true, "VIVA3", 29 },
                    { 39, true, "VVAR3", 28 },
                    { 35, true, "BKBR3", 24 },
                    { 18, true, "BAHI3", 15 },
                    { 16, true, "BTOW3", 13 },
                    { 66, true, "AHEB5", 48 }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "DataNasc", "Documento", "Email", "Nome", "UsuarioId" },
                values: new object[] { 1, new DateTime(2021, 5, 5, 14, 31, 12, 967, DateTimeKind.Local).AddTicks(6845), "450.129.340-32", "claudio@gmail.com", "Claudio", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Acoes",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Nome",
                keyValue: "ChaveApi1");

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Nome",
                keyValue: "EndpointApi");

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Nome",
                keyValue: "HorarioAbertura");

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Nome",
                keyValue: "HorarioFechamento");

            migrationBuilder.DeleteData(
                table: "Parametros",
                keyColumn: "Nome",
                keyValue: "MonitoramentoMinutos");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Perfils",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Perfils",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Perfils",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Setores",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
