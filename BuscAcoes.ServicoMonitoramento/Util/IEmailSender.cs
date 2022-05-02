using BuscAcoes.Dominio.DTOs.Alerta;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Util
{
    public interface IEmailSender
    {
        Task EnviarAlertaPorEmailAsync(AlertaDTO alerta);
    }
}
