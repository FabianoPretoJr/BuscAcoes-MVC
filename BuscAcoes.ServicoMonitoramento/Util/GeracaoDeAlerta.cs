using BuscAcoes.Dominio.DTOs.Alerta;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.ServicoMonitoramento.Interfaces;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Util
{
    public class GeracaoDeAlerta : IGeracaoDeAlerta
    {
        private readonly IEmailSender _emailSender;
        private readonly IAlertaServico _alertaServico;
        private readonly IMonitoramentoServico _monitoramentoServico;
        private readonly IClienteAcaoServico _clienteAcaoServico;

        public GeracaoDeAlerta(IEmailSender emailSender, IAlertaServico alertaServico, 
            IMonitoramentoServico monitoramentoServico, IClienteAcaoServico clienteAcaoServico)
        {
            _emailSender = emailSender;
            _alertaServico = alertaServico;
            _monitoramentoServico = monitoramentoServico;
            _clienteAcaoServico = clienteAcaoServico;
        }

        public async Task VerificarGeracaoDeAlerta()
        {
            var monitoramentos = _monitoramentoServico.ObterTodosUltimoMonitoramento();
            var clienteAcoes = await _clienteAcaoServico.ObterDadosMonitoramentoClientesAcoes();

            try
            {
                Log.Information("Rodando ciclo de alerta...");

                foreach (var monitoramento in monitoramentos)
                {
                    var percentualAbsoluto = Math.Abs(monitoramento.PercentualVariacaoCalculada);

                    var acoesParaAlertar = clienteAcoes.Where(acaoDoCliente =>
                                                                acaoDoCliente.AcaoId == monitoramento.AcaoId &&
                                                                AcaoVariou(percentualAbsoluto, acaoDoCliente.Tolerancia)).ToList();

                    bool existeAcaoParaAlertar = acoesParaAlertar.Count() != 0;
                    if (existeAcaoParaAlertar)
                    {
                        foreach (var acaoParaAlertar in acoesParaAlertar)
                        {
                            try
                            {
                                var alerta = new AlertaDTO()
                                {
                                    ClienteId = acaoParaAlertar.ClienteId,
                                    ClienteNome = acaoParaAlertar.ClienteNome,
                                    ClienteEmail = acaoParaAlertar.ClienteEmail,

                                    AcaoId = acaoParaAlertar.AcaoId,
                                    CodigoNegociacao = acaoParaAlertar.CodigoNegociacao,
                                    AcaoPercentual = monitoramento.PercentualVariacaoCalculada,
                                    AcaoPreco = monitoramento.Preco,

                                    Tolerancia = acaoParaAlertar.Tolerancia
                                };

                                if (acaoParaAlertar.ReceberEmail)
                                    await _emailSender.EnviarAlertaPorEmailAsync(alerta);

                                _alertaServico.RegistrarAlerta(alerta);
                                Log.Debug($"Alerta gerado para o cliente '{acaoParaAlertar.ClienteNome}' da ação '{monitoramento.CodigoNegociacao}'");
                            }
                            catch (Exception ex)
                            {
                                Log.Error($"Ocorreu um erro na geração de alerta da ação '{monitoramento.CodigoNegociacao}': '{ex.Message}', em {ex.StackTrace}");
                            }
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
                Log.Information("Parando ciclo de alerta");
            }
        }

        private bool AcaoVariou(double percentualAcao, double tolerancia)
        {
            if (tolerancia == 0) return false;

            return percentualAcao >= tolerancia;
        }
    }
}
