using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.DTOs.Alerta;
using BuscAcoes.Dominio.DTOs.ClienteAcao;
using BuscAcoes.Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IClienteAcaoServico
    {
        List<ClienteAcaoOutputDTO> ListarAcoesMonitoradas(int id);
        void Salvar(ClienteAcaoInputDTO dto);
        ClienteAcaoInputDTO ObterDadosAcao(int id);
        ClienteAcaoInputDTO ObterDadosClienteAcao(int id);
        void Deletar(int id);
        void CarregarAlertasNaSessao(HttpContext httpContext, int usuarioId);
        void VisualizarTodosAlertas(HttpContext httpContext, int usuarioId);
        Alerta AtualizarVisualizacaoAlerta(int idAlerta);
        List<AlertaOutputDTO> BuscarTodosAlertas(int usuarioId);
        Task<List<AcaoMonitoradaDTO>> ObterAcoesParaMonitorar();
        Task<List<ClienteAcaoMonitoramentoDTO>> ObterDadosMonitoramentoClientesAcoes();
    }
}
