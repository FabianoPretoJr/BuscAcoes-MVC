using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.ServicoMonitoramento.Interfaces;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento.Util
{
    public class ValidacaoMonitoramento : IValidacaoMonitoramento
    {
        private readonly IParametroServico _parametroServico;
        private readonly IDiasIndisponiveisServico _diasIndisponiveisServico;

        public ValidacaoMonitoramento(IParametroServico parametroServico, IDiasIndisponiveisServico diasIndisponiveisServico)
        {
            _parametroServico = parametroServico;
            _diasIndisponiveisServico = diasIndisponiveisServico;
        }

        public async Task<bool> Validar(DateTime data)
        {
            if (await ValidarHorarioAtual(data))
            {
                if (await ValidarDiaIndisponivel(data)) return true;
                else Log.Warning("Bolsa de valores fechada. A data selecionada não é dia útil.");
            }
            else Log.Warning("Bolsa de valores fechada.");

            return false;
        }

        private async Task<bool> ValidarHorarioAtual(DateTime data)
        {
            var horarioAtual = await _parametroServico.ObterHorarios();
            return data.TimeOfDay >= horarioAtual.HoraAbertura && data.TimeOfDay <= horarioAtual.HoraFechamento;
        }

        private async Task<bool> ValidarDiaIndisponivel(DateTime data)
        {
            var diasIndisponiveis = await _diasIndisponiveisServico.SelecionarDiasIndisponiveisApartirDoAtual();
            return diasIndisponiveis.Count() == 0 || diasIndisponiveis.Where(d => d.Data.Date == data.Date).Count() == 0;
        }
    }
}
