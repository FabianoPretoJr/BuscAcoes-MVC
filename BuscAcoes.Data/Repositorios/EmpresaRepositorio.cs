using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Data.Repositorios
{
    public class EmpresaRepositorio : RepositorioBase<Empresa>, IEmpresaRepositorio
    {
        public EmpresaRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public Empresa SelecionarPorCNPJ(string cnpj, bool asNoTracking = true)
        {
            IQueryable<Empresa> query = _dbSet;
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault(e => e.Cnpj == cnpj);
        }

        public override IList<Empresa> SelecionarTodos()
        {
            return _dbSet.Include(e => e.Setor).ToList();
        }

        public override Empresa SelecionarPorId(int id, bool asNoTracking = true)
        {
            IQueryable<Empresa> query = _dbSet;
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.Where(e => e.Id == id).Include(e => e.Setor).FirstOrDefault();
        }

        public Empresa SelecionarPorIdComAcoes(int id, bool asNoTracking = true)
        {
            IQueryable<Empresa> query = _dbSet;
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.Where(e => e.Id == id).Include(e => e.Acoes).Include(e => e.Setor).FirstOrDefault();
        }

        public List<Empresa> SelecionarEmpresasPorIdSetor(int idSetor)
        {
            return _dbSet.Where(a => a.SetorId == idSetor).ToList();
        }
    }
}
