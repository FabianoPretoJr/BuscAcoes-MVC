using BuscAcoes.Dominio.DTOs.Parametro;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IParametroServico
    {
        Task<HorarioInputDTO> ObterHorarios();
        void AtualizarHorarios(HorarioInputDTO dto);
        int ConsultarIntervaloMonitoramentoEmMinutos();
        Task<string> ObterLinkEndpointApi();
        Task<string> ObterChaveApi();
        Task<ParametroInputDTO> ObterParametros();
        void SalvarConfiguracoes(ParametroInputDTO dto);
    }
}
