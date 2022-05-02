using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Interfaces
{
    public interface IValidacaoMonitoramento
    {
        Task<bool> Validar(DateTime data);
    }
}
