using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.DTOs.Monitoramento;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Interfaces
{
    public interface IGeracaoDeAlerta
    {
        Task VerificarGeracaoDeAlerta();
    }
}
