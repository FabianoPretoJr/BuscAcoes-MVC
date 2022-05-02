using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.DTOs.Monitoramento;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Servicos
{
    public class MonitoramentoServico : IMonitoramentoServico
    {
        private readonly IMonitoramentoRepositorio _monitoramentoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IClienteAcaoRepositorio _clienteAcaoRepositorio;

        public MonitoramentoServico(IMonitoramentoRepositorio monitoramentoRepositorio, IClienteRepositorio clienteRepositorio,
                                    IClienteAcaoRepositorio clienteAcaoRepositorio)
        {
            _monitoramentoRepositorio = monitoramentoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _clienteAcaoRepositorio = clienteAcaoRepositorio;
        }

        public void RegistrarMonitoramento(MonitoramentoInputDTO dto)
        {
            var monitoramento = new Monitoramento(dto.Id, 
                                                  dto.PrecoAcao, 
                                                  dto.PercentualVariacao, 
                                                  dto.IdAcao, 
                                                  dto.AtualizadoEm, 
                                                  Convert.ToDouble(dto.PercentualVariacaoCalculada.ToString("F"))
                                                 );

            _monitoramentoRepositorio.Inserir(monitoramento);
        }

        public List<MonitoramentoDTO> ObterMonitoramentosAcoes(MonitoramentoOutputDTO dto = null)
        {
            var query = _monitoramentoRepositorio.SelecionarTodos().AsQueryable();

            if(dto?.Filtros?.CodigoAcao != null)
            {
                query = query.Where(m => m.Acao.CodigoNegociacao == dto.Filtros.CodigoAcao);
            }
            if(dto?.Filtros?.DataInicio != null)
            {
                query = query.Where(m => m.DataCriacao.Date >= dto.Filtros.DataInicio);
            }
            if (dto?.Filtros?.DataFim != null)
            {
                query = query.Where(m => m.DataCriacao.Date <= dto.Filtros.DataFim);
            }
            if (dto?.Filtros?.TodosRegistros == false)
            {
                query = query.Where(m=>m.PercentualVariacaoCalculada != 0);
            }

            return query.ToList().Select(m => {
                return new MonitoramentoDTO()
                {
                    Id = m.Id,
                    CodigoNegociacao = m.Acao.CodigoNegociacao,
                    PrecoAcao = m.PrecoAcao,
                    PercentualVariacaoCalculada = m.PercentualVariacaoCalculada,
                    PercentualVariacao = m.PercentualVariacao,
                    DataCriacao = m.DataCriacao,
                    AtualizadoEm = m.AtualizadoEm
                };
            }).ToList();
        }

        public async Task<double?> ObterUltimoPreco(int acaoId)
        {
            var monitoramento = await _monitoramentoRepositorio.SelecionarUltimoMonitoramento(acaoId);
            
            return monitoramento?.PrecoAcao;
        }

        public List<MonitoramentoRecenteDTO> ObterUltimoMonitoramentoPorCliente(int id)
        {
            var cliente = _clienteRepositorio.SelecionarPorIdUsuario(id);
            var clienteAcoes = _clienteAcaoRepositorio.ListarAcoesMonitoradas(cliente.Id);

            int[] acoesIds = clienteAcoes.Select(c => c.AcaoId).ToArray();

            var monitoramentos = _monitoramentoRepositorio
                                                        .SelecionarUltimoMonitoramentoPorCliente(acoesIds);

            return monitoramentos.Select(m =>
            {
                return new MonitoramentoRecenteDTO()
                {
                    CodigoNegociacao = m.Acao.CodigoNegociacao,
                    Preco = m.PrecoAcao
                };
            }).ToList();
        }

        public List<MonitoramentoRecenteDTO> ObterTodosUltimoMonitoramento()
        {
            var todosClienteAcoes = _clienteAcaoRepositorio.SelecionarTodos();
            var monitoramentos = _monitoramentoRepositorio.SelecionarUltimoMonitoramentoPorCliente(todosClienteAcoes.Select(ca => ca.AcaoId).Distinct().ToArray());

            return monitoramentos.Select(m =>
            {
                return new MonitoramentoRecenteDTO()
                {
                    CodigoNegociacao = m.Acao.CodigoNegociacao,
                    Preco = m.PrecoAcao,
                    PercentualVariacaoCalculada = m.PercentualVariacaoCalculada,
                    AcaoId = m.AcaoId
                };
            }).ToList();
        }
    }
}
