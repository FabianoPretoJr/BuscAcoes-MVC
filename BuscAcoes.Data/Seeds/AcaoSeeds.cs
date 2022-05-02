using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Data.Seeds
{
    public static class AcaoSeeds
    {
        public static void Acoes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acao>().HasData(
                new Acao(1, "PETR3",1),
                new Acao(2, "PETR4", 1),
                new Acao(3, "PSSA3", 2),
                new Acao(4, "POSI3", 3),
                new Acao(5, "PETZ3", 4),
                new Acao(6, "MSRO3", 5),
                new Acao(7, "MGLU3", 6),
                new Acao(8, "AMAR3", 7),
                new Acao(9, "CASH3", 8),
                new Acao(10, "MNDL3", 9),
                new Acao(11, "MRVE3", 10),
                new Acao(12, "MULT3", 11),
                new Acao(13, "MRSA3B", 12),
                new Acao(14, "MRSA5B", 12),
                new Acao(15, "MRSA6B", 12),
                new Acao(16, "BTOW3", 13),
                new Acao(17, "B3SA3", 14),
                new Acao(18, "BAHI3", 15),
                new Acao(19, "BMGB4", 16),
                new Acao(20, "BIDI11", 17),
                new Acao(21, "BIDI3", 17),
                new Acao(22, "BIDI4", 17),
                new Acao(23, "BALM3", 18),
                new Acao(24, "BALM4", 18),
                new Acao(25, "BBSE3", 19),
                new Acao(26, "ABCB4", 20),
                new Acao(27, "BPAC11", 21),
                new Acao(28, "BPAC3", 21),
                new Acao(29, "BPAC5", 21),
                new Acao(30, "BBAS11", 22),
                new Acao(31, "BBAS12", 22),
                new Acao(32, "BBAS3", 22),
                new Acao(33, "BBDC3", 23),
                new Acao(34, "BBDC4", 23),
                new Acao(35, "BKBR3", 24),
                new Acao(36, "BRQB3", 25),
                new Acao(37, "VALE3", 26),
                new Acao(38, "VLID3", 27),
                new Acao(39, "VVAR3", 28),
                new Acao(40, "VIVA3", 29),
                new Acao(41, "OIBR3", 30),
                new Acao(42, "OIBR4", 30),
                new Acao(43, "TELB3", 31),
                new Acao(44, "TELB4", 31),
                new Acao(45, "TIMS3", 32),
                new Acao(46, "CAML3", 33),
                new Acao(47, "ABEV3", 34),
                new Acao(49, "UNIP3", 36),
                new Acao(50, "UNIP5", 36),
                new Acao(51, "UNIP6", 36),
                new Acao(52, "PGMN3", 37),
                new Acao(53, "RADL3", 38),
                new Acao(54, "LINX3", 39),
                new Acao(55, "MBLY3", 40),
                new Acao(56, "QUSW3", 41),
                new Acao(57, "TOTS3", 42),
                new Acao(58, "LIPR3", 43),
                new Acao(59, "EKTR3", 44),
                new Acao(60, "EKTR4", 44),
                new Acao(61, "ATOM3", 45),
                new Acao(62, "OPGM3B", 46),
                new Acao(63, "FIGE3", 47),
                new Acao(64, "FIGE4", 47),
                new Acao(65, "AHEB3", 48),
                new Acao(66, "AHEB5", 48),
                new Acao(67, "AHEB6", 48),
                new Acao(68, "EMBR3", 49),
                new Acao(69, "TASA3", 50),
                new Acao(70, "TASA4", 50)
                );
        }
    }
}
