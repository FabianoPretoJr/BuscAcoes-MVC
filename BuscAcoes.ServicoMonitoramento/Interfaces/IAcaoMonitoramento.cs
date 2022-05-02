using System;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Interfaces
{
    public interface IAcaoMonitoramento
    {
        Task Monitorar(DateTime data);
    }
}
