using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.DTOs.Alerta;
using BuscAcoes.Dominio.DTOs.ClienteAcao;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Dominio.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Servicos
{
    public class ClienteAcaoServico : IClienteAcaoServico
    {
        private readonly IClienteAcaoRepositorio _clienteAcaoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IAcaoRepositorio _acaoRepositorio;
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly IMonitoramentoRepositorio _monitoramentoRepositorio;

        public ClienteAcaoServico(
            IClienteAcaoRepositorio clienteAcaoRepositorio,
            IClienteRepositorio clienteRepositorio,
            IAcaoRepositorio acaoRepositorio,
            IAlertaRepositorio alertaRepositorio,
            IMonitoramentoRepositorio monitoramentoRepositorio)
        {
            _clienteAcaoRepositorio = clienteAcaoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _acaoRepositorio = acaoRepositorio;
            _alertaRepositorio = alertaRepositorio;
            _monitoramentoRepositorio = monitoramentoRepositorio;
        }

        public List<ClienteAcaoOutputDTO> ListarAcoesMonitoradas(int id)
        {
            var cliente = _clienteRepositorio.SelecionarPorIdUsuario(id);
            var clienteAcoes = _clienteAcaoRepositorio.ListarAcoesMonitoradas(cliente.Id);

            int[] acoesIds = clienteAcoes.Select(c => c.AcaoId).ToArray();

            var ultimosMonitoramentos = _monitoramentoRepositorio
                                                        .SelecionarUltimoMonitoramentoPorCliente(acoesIds, mostrarSomenteSemVariacao: true);

            var dto = clienteAcoes.Select(ca =>
            {
                Monitoramento ultimoMonitoramento = ultimosMonitoramentos
                                                                .FirstOrDefault(m => m.AcaoId == ca.AcaoId);

                double ultimaVariacao = ultimoMonitoramento == null ? 0 : ultimoMonitoramento.PercentualVariacaoCalculada;
                double ultimoPreco = ultimoMonitoramento == null ? 0 : ultimoMonitoramento.PrecoAcao;

                return new ClienteAcaoOutputDTO()
                {
                    Id = ca.Id,
                    IdCliente = ca.ClienteId,
                    IdAcao = ca.AcaoId,
                    CodigoNegociacao = ca.Acao.CodigoNegociacao,
                    Tolerancia = ca.Tolerancia,
                    AcaoAtiva = ca.Acao.Ativo,
                    UltimaVariacao = ultimaVariacao,
                    UltimoPreco = ultimoPreco
                };
            }).ToList();

            return dto;
        }

        public void Salvar(ClienteAcaoInputDTO dto)
        {
            if (dto.ParaEdicao)
            {
                var clienteAcaoAtualizado = new ClienteAcao(dto.Id, dto.IdCliente, dto.IdAcao, Convert.ToDouble(dto.Tolerancia), dto.ReceberEmail);
                _clienteAcaoRepositorio.Atualizar(clienteAcaoAtualizado);
            }
            else
            {
                var cliente = _clienteRepositorio.SelecionarPorIdUsuario(dto.IdUsuario);
                var clienteAcao = new ClienteAcao(dto.Id, cliente.Id, dto.IdAcao, Convert.ToDouble(dto.Tolerancia), dto.ReceberEmail);
                _clienteAcaoRepositorio.Inserir(clienteAcao);
            }
        }

        public ClienteAcaoInputDTO ObterDadosAcao(int id)
        {
            var acao = _acaoRepositorio.ObterDadosAcao(id);

            var dto = new ClienteAcaoInputDTO()
            {
                IdAcao = acao.Id,
                CodigoNegociacao = acao.CodigoNegociacao,
                NomeEmpresa = acao.Empresa.Nome,
                CnpjEmpresa = acao.Empresa.Cnpj,
                Setorempresa = acao.Empresa.Setor.Nome
            };

            return dto;
        }

        public ClienteAcaoInputDTO ObterDadosClienteAcao(int id)
        {
            var clienteAcao = _clienteAcaoRepositorio.SelecionarPorId(id);

            var dto = new ClienteAcaoInputDTO()
            {
                Id = clienteAcao.Id,
                IdAcao = clienteAcao.AcaoId,
                IdCliente = clienteAcao.ClienteId,
                CodigoNegociacao = clienteAcao.Acao.CodigoNegociacao,
                NomeEmpresa = clienteAcao.Acao.Empresa.Nome,
                CnpjEmpresa = clienteAcao.Acao.Empresa.Cnpj,
                Setorempresa = clienteAcao.Acao.Empresa.Setor.Nome,
                Tolerancia = clienteAcao.Tolerancia.ToString(),
                ReceberEmail = clienteAcao.ReceberEmail
            };

            return dto;
        }

        public void Deletar(int id)
        {
            var clienteAcao = _clienteAcaoRepositorio.SelecionarPorId(id);
            if (clienteAcao == null)
                throw new EntidadeNaoEncontradaException("Ação monitorada não encontrada.");

            _clienteAcaoRepositorio.Deletar(clienteAcao.Id);
        }

        public void CarregarAlertasNaSessao(HttpContext httpContext, int usuarioId)
        {
            var alertasEmJson = JsonSerializer.Serialize(BuscarAlertasNaoVisualizados(usuarioId));

            httpContext.Session.SetString("Alertas", alertasEmJson);
        }

        private List<AlertaDTO> BuscarAlertasNaoVisualizados(int usuarioId)
        {
            var alertasNoBanco = _alertaRepositorio.SelecionarPorUsuario(usuarioId);

            return alertasNoBanco.Select(alerta => new AlertaDTO()
            {
                Id = alerta.Id,
                CodigoNegociacao = alerta.Acao.CodigoNegociacao,
                AcaoId = alerta.AcaoId,
                Tolerancia = alerta.Tolerencia,
                ClienteId = alerta.ClienteId,
                DataCriacao = alerta.DataCriacao
            }).ToList();
        }

        public Alerta AtualizarVisualizacaoAlerta(int idAlerta)
        {
            var alerta = _alertaRepositorio.SelecionarPorId(idAlerta);
            alerta.Visualizar();

            _alertaRepositorio.Atualizar(alerta);

            return alerta;
        }

        public List<AlertaOutputDTO> BuscarTodosAlertas(int usuarioId)
        {
            var alertasNoBanco = _alertaRepositorio.SelecionarPorUsuario(usuarioId, apenasNaoVisualizados: false);

            return alertasNoBanco.Select(alerta => new AlertaOutputDTO()
            {
                Id = alerta.Id,
                CodigoNegociacao = alerta.Acao.CodigoNegociacao,
                AcaoId = alerta.AcaoId,
                Tolerancia = alerta.Tolerencia,
                ClienteId = alerta.ClienteId,
                DataCriacao = alerta.DataCriacao,
                Visualizado = alerta.Visualizado
            }).OrderBy(a => a.Visualizado).ToList();
        }

        public void VisualizarTodosAlertas(HttpContext httpContext, int usuarioId)
        {
            _alertaRepositorio.VisualizarTodosAlertas(usuarioId);
            this.CarregarAlertasNaSessao(httpContext, usuarioId);
        }

        public async Task<List<AcaoMonitoradaDTO>> ObterAcoesParaMonitorar()
        {
            var clienteAcao = await _clienteAcaoRepositorio.ObterAcoesParaMonitorar();

            return clienteAcao.Select(ca => {
                return new AcaoMonitoradaDTO()
                {
                    Codigo_Negociacao = ca.Acao.CodigoNegociacao,
                    AcaoId = ca.Acao.Id
                };
            }).Distinct(new CompararAcoes()).ToList();
        }

        public async Task<List<ClienteAcaoMonitoramentoDTO>> ObterDadosMonitoramentoClientesAcoes()
        {
            var clientesAcoes = await _clienteAcaoRepositorio.ObterAcoesParaMonitorar(true);

            return clientesAcoes.Select(ca =>
            {
                return new ClienteAcaoMonitoramentoDTO()
                {
                    AcaoId = ca.Acao.Id,
                    CodigoNegociacao = ca.Acao.CodigoNegociacao,
                    ClienteId = ca.Cliente.Id,
                    ClienteEmail = ca.Cliente.Email,
                    ClienteNome = ca.Cliente.Usuario.Nome,
                    ReceberEmail = ca.ReceberEmail,
                    Tolerancia = ca.Tolerancia
                };
            }).ToList();
        }
    }
}

