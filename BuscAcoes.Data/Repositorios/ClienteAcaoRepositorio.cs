using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Data.Repositorios
{
    public class ClienteAcaoRepositorio : RepositorioBase<ClienteAcao>, IClienteAcaoRepositorio
    {
        public ClienteAcaoRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public List<ClienteAcao> ListarAcoesMonitoradas(int id)
        {
            return _dbSet.Where(c => c.ClienteId == id).Include(c => c.Acao).ToList();
        }

        public override ClienteAcao SelecionarPorId(int id, bool asNoTracking = true)
        {
            IQueryable<ClienteAcao> query = _dbSet;
            if (asNoTracking)
                query = query.AsNoTracking();

            return query.Where(c => c.Id == id).Include(c => c.Acao).ThenInclude(a => a.Empresa).ThenInclude(e => e.Setor).FirstOrDefault();
        }

        public async Task<List<ClienteAcao>> ObterAcoesParaMonitorar(bool obterDadosCliente = false)
        {
            IQueryable<ClienteAcao> query = _dbSet;
            if (obterDadosCliente)
                query = query.Include(ca => ca.Cliente).ThenInclude(c => c.Usuario);

            return await query.Include(ca => ca.Acao)
                               .Where(ca => ca.Acao.Ativo && ca.Cliente.Usuario.Ativo)
                               .AsNoTracking()
                               .ToListAsync();
        }
    }
}