using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Data.Seeds
{
    public static class UsuarioSeeds
    {
        
        public static void Usuarios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
            new Usuario(1, "Administrador", "Admin", "e10adc3949ba59abbe56e057f20f883e", 1),
            new Usuario(2, "Operador", "opera", "e10adc3949ba59abbe56e057f20f883e", 2),
            new Usuario(3, "Cliente", "cliente", "e10adc3949ba59abbe56e057f20f883e", 3)
            );
        }
    }
}
