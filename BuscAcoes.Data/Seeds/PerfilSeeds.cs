using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BuscAcoes.Data.Seeds
{
    public static class PerfilSeeds
    {
        public static void Perfils(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfil>().HasData(
            new Perfil(1, "Administrador"),
            new Perfil(2, "Operador"),
            new Perfil(3, "Cliente"));
        }
    }
}
