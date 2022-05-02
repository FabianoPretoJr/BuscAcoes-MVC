using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Data.Repositorios
{
    public class MonitoramentoRepositorio : RepositorioBase<Monitoramento>, IMonitoramentoRepositorio
    {
        public MonitoramentoRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public override IList<Monitoramento> SelecionarTodos()
        {
            return _dbSet.Include(m => m.Acao).OrderByDescending(m => m.DataCriacao).ToList();
        }
        public async Task<Monitoramento> SelecionarUltimoMonitoramento(int acaoId)
        {
            return await _dbSet.OrderByDescending(p => p.Id)
                               .Where(p => p.AcaoId == acaoId)
                               .AsNoTracking()
                               .FirstOrDefaultAsync();

        }

        public List<Monitoramento> SelecionarUltimoMonitoramentoPorCliente(int[] idAcoes, bool mostrarSomenteSemVariacao = false)
        {
            var monitoramentos = new List<Monitoramento>();

            foreach (var idAcao in idAcoes)
            {
                IQueryable<Monitoramento> query = _dbSet.Where(m => m.AcaoId == idAcao);

                if (mostrarSomenteSemVariacao) query = query.Where(m => m.PercentualVariacaoCalculada != 0);

                Monitoramento monitoramento = query
                                                .OrderByDescending(m => m.Id)
                                                .Include(m => m.Acao)
                                                .FirstOrDefault();

                if (monitoramento != null) monitoramentos.Add(monitoramento);
            }

            return monitoramentos;
        }
    }
}
