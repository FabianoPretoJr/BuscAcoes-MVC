using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Data.Seeds
{
    public static class EmpresaSeeds
    {
        public static void Empresas(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().HasData(
                new Empresa(1, "PETROBRAS", "33.000.167/0001-01", 8),
                new Empresa(2, "PORTO SEGURO", "02.149.205/0001-69", 5),
                new Empresa(3, "POSITIVO TEC", "81.243.735/0001-48", 10),
                new Empresa(4, "PETZ", "18.328.118/0001-09", 3),
                new Empresa(5, "MAESTROLOC", "08.795.211/0001-70", 3),
                new Empresa(6, "MAGAZ LUIZA", "47.960.950/0001-21", 3),
                new Empresa(7, "LOJAS MARISA", "61.189.288/0001-89", 3),
                new Empresa(8, "MELIUZ", "14.110.585/0001-07", 10),
                new Empresa(9, "MUNDIAL", "88.610.191/0001-54", 3),
                new Empresa(10, "MRV", "08.343.492/0001-20", 3),
                new Empresa(11, "MULTIPLAN", "07.816.890/0001-53", 5),
                new Empresa(12, "MRS LOGIST", "01.417.222/0001-77", 1),
                new Empresa(13, "B2W DIGITAL", "00.776.574/0001-56", 3),
                new Empresa(14, "B3", "09.346.601/0001-25", 5),
                new Empresa(15, "BAHEMA", "45.987.245/0001-92", 3),
                new Empresa(16, "BANCO BMG", "61.186.680/0001-74", 5),
                new Empresa(17, "BANCO INTER", "00.416.968/0001-01", 5),
                new Empresa(18, "BAUMER", "61.374.161/0001-30", 9),
                new Empresa(19, "BBSEGURIDADE", "17.344.597/0001-94", 5),
                new Empresa(20, "ABC BRASIL", "28.195.667/0001-06", 5),
                new Empresa(21, "BTGP BANCO", "30.306.294/0001-45", 5),
                new Empresa(22, "BRASIL", "00.000.000/0001-91", 5),
                new Empresa(23, "BRADESCO", "60.746.948/0001-12", 5),
                new Empresa(24, "BK BRASIL", "13.574.594/0001-96", 3),
                new Empresa(25, "BRQ", "36.542.025/0001-64", 10),
                new Empresa(26, "VALE", "33.592.510/0001-54", 6),
                new Empresa(27, "VALID", "33.113.309/0001-47", 1),
                new Empresa(28, "VIAVAREJO", "33.041.260/0652-90", 3),
                new Empresa(29, "VIVARA S.A.", "33.839.910/0001-11", 3),
                new Empresa(30, "OI", "76.535.764/0001-43", 2),
                new Empresa(31, "TELEBRAS", "00.336.701/0001-04", 2),
                new Empresa(32, "TIM", "02.421.421/0001-11", 2),
                new Empresa(33, "CAMIL", "64.904.295/0001-03", 4),
                new Empresa(34, "ABEV3", "07.526.557/0001-00", 4),
                new Empresa(35, "CARREFOUR BR", "75.315.333/0001-09", 4),
                new Empresa(36, "UNIPAR", "33.958.695/0001-78", 6),
                new Empresa(37, "PAGUE MENOS", "06.626.253/0001-51", 9),
                new Empresa(38, "RAIADROGASIL", "61.585.865/0001-51", 9),
                new Empresa(39, "LINX", "06.948.969/0001-75", 10),
                new Empresa(40, "MOBLY", "31.553.627/0001-01", 10),
                new Empresa(41, "QUALITY SOFT", "35.791.391/0001-94", 10),
                new Empresa(42, "TOTVS", "53.113.791/0001-22", 10),
                new Empresa(43, "ELETROPAR", "01.104.937/0001-70", 11),
                new Empresa(44, "ELEKTRO", "02.328.280/0001-97", 11),
                new Empresa(45, "ATOMPAR", "00.359.742/0001-08", 7),
                new Empresa(46, "GAMA PART", "02.796.775/0001-40", 7),
                new Empresa(47, "INVEST BEMGE", "01.548.981/0001-79", 7),
                new Empresa(48, "SPTURIS", "62.002.886/0001-60", 3),
                new Empresa(49, "EMBRAER", "07.689.002/0001-89", 1),
                new Empresa(50, "TAURUS ARMAS", "92.781.335/0001-02", 1)
            );
        }
    }
}
