using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BuscAcoes.Data.Repositorios
{
    public class SetorRepositorio : RepositorioBase<Setor>, ISetorRepositorio
    {
        public SetorRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public Setor SelecionarPorNome(string nome)
        {
            if (nome == null) return null;

            return _dbSet
                        .AsNoTracking()
                        .FirstOrDefault(s => s.Nome.ToLower().Trim() == nome.ToLower().Trim());
        }
    }
}
