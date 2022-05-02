using BuscAcoes.Dominio.DTOs.Monitoramento;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.ServicoMonitoramento.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Util
{
    public class AcaoMonitoramento : IAcaoMonitoramento
    {
        private readonly IParametroServico _parametroServico;
        private readonly IMonitoramentoServico _monitoramentoServico;
        private readonly IValidacaoMonitoramento _validacaoMonitoramento;
        private readonly IClienteAcaoServico _clienteAcaoServico;

        public AcaoMonitoramento(IParametroServico parametroServico, IMonitoramentoServico monitoramentoServico,
            IValidacaoMonitoramento validacaoMonitoramento, IClienteAcaoServico clienteAcaoServico)
        {
            _parametroServico = parametroServico;
            _monitoramentoServico = monitoramentoServico;
            _validacaoMonitoramento = validacaoMonitoramento;
            _clienteAcaoServico = clienteAcaoServico;
        }

        public async Task Monitorar(DateTime data)
        {
            try
            {
                Log.Information("Rodando ciclo de monitoramento...");

                if (await _validacaoMonitoramento.Validar(data))
                {
                    var linkEndpointApi = await _parametroServico.ObterLinkEndpointApi();
                    var chaveApi = await _parametroServico.ObterChaveApi();
                    var acoesMonitoradas = await _clienteAcaoServico.ObterAcoesParaMonitorar();

                    foreach (var acao in acoesMonitoradas)
                    {
                        try
                        {
                            var monitoramento = await API.Requisitar(linkEndpointApi, chaveApi, acao.Codigo_Negociacao, acao.AcaoId);
                            monitoramento.PercentualVariacaoCalculada = await this.CalcularPercentual(monitoramento);

                            _monitoramentoServico.RegistrarMonitoramento(monitoramento);

                            Log.Debug($"Um evento de monitoramento da ação '{acao.Codigo_Negociacao}' ocorreu com sucesso");
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"Ocorreu um erro no monitoramento da ação '{acao.Codigo_Negociacao}': '{ex.Message}', em {ex.StackTrace}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Ocorreu um erro inesperado: '{ex.Message}', em {ex.StackTrace}");
            }
            finally
            {
                Log.Information("Parando ciclo de monitoramento");
            }
        }

        private async Task<double> CalcularPercentual(MonitoramentoInputDTO monitoramento)
        {
            var ultimoPreco = await _monitoramentoServico.ObterUltimoPreco(monitoramento.IdAcao);
            if (!ultimoPreco.HasValue) return 0;

            return 100 * (monitoramento.PrecoAcao - ultimoPreco.GetValueOrDefault()) / ultimoPreco.GetValueOrDefault();
        }
    }
}
