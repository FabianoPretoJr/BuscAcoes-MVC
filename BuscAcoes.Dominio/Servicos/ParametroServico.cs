using BuscAcoes.Dominio.DTOs.Parametro;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Servicos
{
    public class ParametroServico : IParametroServico
    {
        private readonly IParametroRepositorio _parametroRepositorio;

        public ParametroServico(IParametroRepositorio parametroRepositorio)
        {
            _parametroRepositorio = parametroRepositorio;
        }

        public void AtualizarHorarios(HorarioInputDTO dto)
        {
            var parametroAbertura = new Parametro("HorarioAbertura", dto.HoraAbertura.ToString());
            var parametroFechamento = new Parametro("HorarioFechamento", dto.HoraFechamento.ToString());

            _parametroRepositorio.AtualizarParametro(parametroAbertura);
            _parametroRepositorio.AtualizarParametro(parametroFechamento);
        }

        public async Task<HorarioInputDTO> ObterHorarios()
        {
            var parametros = await _parametroRepositorio.SelecionarHorarios();
            var dtos = new HorarioInputDTO()
            {
                HoraAbertura = TimeSpan.Parse(parametros[0].Valor),
                HoraFechamento = TimeSpan.Parse(parametros[1].Valor),
            };

            return dtos;
        }
        public int ConsultarIntervaloMonitoramentoEmMinutos()
        {
            var parametro = _parametroRepositorio.SelecionarIntervaloMonitoramento();
            if (parametro == null) throw new EntidadeNaoEncontradaException("Intervalo não encontrado.");

            var intervalo = parametro.Valor.Split(":").Select(x => Convert.ToInt32(x)).ToArray();

            var horas = intervalo[0];
            var minutos = intervalo[1];
            var segundos = intervalo[2];

            minutos += horas * 60;
            minutos += segundos / 60;

            return minutos;
        }

        public async Task<string> ObterLinkEndpointApi()
        {
            var parametro = await _parametroRepositorio.ObterLinkEndpointApi();
            if (parametro == null) 
                throw new EntidadeNaoEncontradaException("Link não encontrado.");

            return parametro;
        }

        public async Task<string> ObterChaveApi()
        {
            var parametro = await _parametroRepositorio.ObterChaveApi();
            if (parametro == null)
                throw new EntidadeNaoEncontradaException("Chave não encontrada.");

            return parametro;
        }

        public async Task<ParametroInputDTO> ObterParametros()
        {
            var parametrosDTO = new ParametroInputDTO()
            {
                ChaveApi = await this.ObterChaveApi(),
                LinkApi = await this.ObterLinkEndpointApi(),
                MonitoramentoMinutos = this.ConsultarIntervaloMonitoramentoEmMinutos()
            };

            return parametrosDTO;
        }

        public void SalvarConfiguracoes(ParametroInputDTO dto)
        {
            var parametroChaveAPI = new Parametro("ChaveApi1", dto.ChaveApi);
            var parametroLinkAPI = new Parametro("EndpointApi", dto.LinkApi);
            var time = new DateTime(1, 1, 1, 0, dto.MonitoramentoMinutos, 0);
            var parametroIntervaloMonitoramento = new Parametro("MonitoramentoMinutos", time.TimeOfDay.ToString());

            _parametroRepositorio.AtualizarParametro(parametroChaveAPI);
            _parametroRepositorio.AtualizarParametro(parametroLinkAPI);
            _parametroRepositorio.AtualizarParametro(parametroIntervaloMonitoramento);
        }
    }
}
