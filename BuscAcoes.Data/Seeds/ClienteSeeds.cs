using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Data.Seeds
{
    public static class ClienteSeeds
    {
        public static void Clientes(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Cliente>().HasData(
            new Cliente(1, "claudio@gmail.com", "450.129.340-32", DateTime.Now, 3)
            );
        }
        
    }
}
