using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Data.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        protected readonly BuscAcoesDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositorioBase(BuscAcoesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual void Atualizar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            var entity = SelecionarPorId(id);
            _dbSet.Remove(entity);

            _context.SaveChanges();
        }

        public virtual void Inserir(T entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public virtual T SelecionarPorId(int id, bool asNoTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            
            return query.FirstOrDefault(e => e.Id == id);
        }

        public virtual IList<T> SelecionarTodos()
        {
            return _dbSet.ToList();
        }

        public async Task<IList<T>> SelecionarTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
