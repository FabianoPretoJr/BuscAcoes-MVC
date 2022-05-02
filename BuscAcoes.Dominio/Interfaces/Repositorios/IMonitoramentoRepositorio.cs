using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IMonitoramentoRepositorio : IRepositorioBase<Monitoramento>
    {
        Task<Monitoramento> SelecionarUltimoMonitoramento(int acaoId);
        List<Monitoramento> SelecionarUltimoMonitoramentoPorCliente(int[] idAcoes, bool mostrarSomenteSemVariacao = false);
    }
}
