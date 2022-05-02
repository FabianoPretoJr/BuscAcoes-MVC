using BuscAcoes.Dominio.DTOs.Monitoramento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IMonitoramentoServico
    {
        void RegistrarMonitoramento(MonitoramentoInputDTO dto);
        List<MonitoramentoDTO> ObterMonitoramentosAcoes(MonitoramentoOutputDTO dto = null);
        Task<double?> ObterUltimoPreco(int acaoId);
        List<MonitoramentoRecenteDTO> ObterUltimoMonitoramentoPorCliente(int id);
        List<MonitoramentoRecenteDTO> ObterTodosUltimoMonitoramento();
    }
}
