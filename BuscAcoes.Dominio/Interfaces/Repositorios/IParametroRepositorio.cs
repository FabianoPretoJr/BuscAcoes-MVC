using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IParametroRepositorio
    {
        Task<List<Parametro>> SelecionarHorarios();
        void AtualizarParametro(Parametro parametro);
        Parametro SelecionarIntervaloMonitoramento();
        Task<string> ObterLinkEndpointApi();
        Task<string> ObterChaveApi();
    }
}
