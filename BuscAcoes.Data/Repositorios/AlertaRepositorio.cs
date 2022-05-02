using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Data.Repositorios
{
    public class AlertaRepositorio : RepositorioBase<Alerta>, IAlertaRepositorio
    {
        public AlertaRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public override Alerta SelecionarPorId(int id, bool asNoTracking = true)
        {
            IQueryable<Alerta> query = _dbSet.Include(a => a.Acao);
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault(e => e.Id == id);
        }

        public List<Alerta> SelecionarPorUsuario(int usuarioId, bool apenasNaoVisualizados = true, bool comUsuario = false)
        {
            IQueryable<Alerta> query = _dbSet.Include(a => a.Acao);

            if (comUsuario)
                query = query.Include(a => a.Cliente).ThenInclude(c => c.Usuario);

            if (apenasNaoVisualizados)
                query = query.Where(a => a.Cliente.UsuarioId == usuarioId && a.Visualizado == false);
            else
                query = query.Where(a => a.Cliente.UsuarioId == usuarioId);

            return query.AsNoTracking().ToList();
        }

        public void VisualizarTodosAlertas(int usuarioId)
        {
            List<Alerta> alertas = _dbSet.Include(a => a.Cliente)
                                         .Where(a => a.Cliente.UsuarioId == usuarioId && a.Visualizado == false)
                                         .ToList();
            alertas.ForEach(alerta => alerta.Visualizar());

            _dbSet.UpdateRange(alertas);
            _context.SaveChanges();
        }
    }
}
