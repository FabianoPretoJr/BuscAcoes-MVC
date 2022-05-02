using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Data.Repositorios
{
    public class AcaoRepositorio : RepositorioBase<Acao>, IAcaoRepositorio
    {
        public AcaoRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public override IList<Acao> SelecionarTodos()
        {
            return _dbSet
                        .Include(a => a.Empresa)
                        .OrderByDescending(a => a.Ativo)
                        .ThenByDescending(a => a.Id)
                        .ToList();
        }

        public override Acao SelecionarPorId(int id, bool asNoTracking = true)
        {
            IQueryable<Acao> query = _dbSet.Include(a => a.Empresa).ThenInclude(e => e.Setor);

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault(a => a.Id == id);
        }

        public List<Acao> SelecionarAcoesPorCliente(int clienteId)
        {
            throw new NotImplementedException();
        }

        public Acao ObterDadosAcao(int id)
        {
            return _dbSet.Where(a => a.Id == id).Include(a => a.Empresa).ThenInclude(e => e.Setor).FirstOrDefault();
        }

        public List<Acao> SelecionarAcoesPorIdEmpresa(int idEmpresa)
        {
            return _dbSet.Where(a => a.EmpresaId == idEmpresa).ToList();
        }

        public void AtualizarAcoes(int idEmpresa, List<Acao> acoes)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var acoesParaInserir = acoes.Where(p => p.Id == 0).ToList();

                _dbSet.AddRange(acoesParaInserir);

                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public Acao SelecionarPorCodigoNegociacao(string codigoNegociacao)
        {
            return _dbSet.FirstOrDefault(a => a.CodigoNegociacao == codigoNegociacao);
        }
    }
}
