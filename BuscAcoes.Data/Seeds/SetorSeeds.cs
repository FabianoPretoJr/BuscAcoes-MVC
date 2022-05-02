using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Data.Seeds
{
    public static class SetorSeeds
    {
        public static void Setores(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setor>().HasData(
                new Setor(1, "Bens Industriais"),
                new Setor(2, "Comunicações"),
                new Setor(3, "Consumo Cíclico"),
                new Setor(4, "Consumo não Cíclico"),
                new Setor(5, "Financeiro"),
                new Setor(6, "Outros"),
                new Setor(7, "Petróleo. Gás e Biocombustíveis"),
                new Setor(8, "Saúde"),
                new Setor(9, "Tecnologia da Informação"),
                new Setor(10, "Utilidade Pública"),
                new Setor(11, "Materiais Básicos")
                );
        }
    }
}
