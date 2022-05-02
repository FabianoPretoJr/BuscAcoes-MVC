using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IAcaoRepositorio : IRepositorioBase<Acao>
    {
        List<Acao> SelecionarAcoesPorCliente(int clienteId);
        Acao ObterDadosAcao(int id);
        List<Acao> SelecionarAcoesPorIdEmpresa(int idEmpresa);
        void AtualizarAcoes(int idEmpresa, List<Acao> acoes);
        Acao SelecionarPorCodigoNegociacao(string codigoNegociacao);
    }
}
