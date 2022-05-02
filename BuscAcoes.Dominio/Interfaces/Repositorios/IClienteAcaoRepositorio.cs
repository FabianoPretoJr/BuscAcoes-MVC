using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IClienteAcaoRepositorio : IRepositorioBase<ClienteAcao>
    {
        List<ClienteAcao> ListarAcoesMonitoradas(int id);
        Task<List<ClienteAcao>> ObterAcoesParaMonitorar(bool obterDadosCliente = false);
    }
}
