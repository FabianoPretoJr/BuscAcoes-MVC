using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Data.Seeds
{
    public static class ParametroSeeds
    {
        public static void Parametros(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parametro>().HasData(
                new Parametro("HorarioAbertura", "10:00:00"),
                new Parametro("HorarioFechamento", "17:00:00"),
                new Parametro("MonitoramentoMinutos", "00:10:00"),
                new Parametro("EndpointApi", "https://api.hgbrasil.com/finance/stock_price?fields=only_results,price,change_percent,updated_at&symbol="),
                new Parametro("ChaveApi1", "78c45fea")
                );
        }
    }
}
