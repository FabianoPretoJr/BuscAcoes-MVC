using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Dominio.Servicos
{
    public class AcaoServico : IAcaoServico
    {
        private readonly IAcaoRepositorio _acaoRepositorio;
        private readonly IClienteAcaoRepositorio _clienteAcaoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public AcaoServico(
            IAcaoRepositorio acaoRepositorio, 
            IClienteAcaoRepositorio clienteAcaoRepositorio,
            IClienteRepositorio clienteRepositorio)
        {
            _acaoRepositorio = acaoRepositorio;
            _clienteAcaoRepositorio = clienteAcaoRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }

        public void Salvar(AcaoInputDTO dto)
        {
            Acao acao = _acaoRepositorio.SelecionarPorId(dto.Id);
            if (acao == null)
                throw new EntidadeNaoEncontradaException("Ação não encontrada.");

            var acaoParaAtualizar = new Acao(acao.Id, dto.CodigoNegociacao.Replace(" ","").ToUpper(), dto.EmpresaId);

            _acaoRepositorio.Atualizar(acaoParaAtualizar);
        }

        public void Desativar(int id)
        {
            try
            {
                //Validações -> se a ação é valida, etc
                var acao = _acaoRepositorio.SelecionarPorId(id, asNoTracking: false);
                if (acao == null) throw new EntidadeNaoEncontradaException("Ação não encontrada.");

                acao.Desativar();
                _acaoRepositorio.Atualizar(acao);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<AcaoOutputDTO> ListarAcoes()
        {
            var acoes = _acaoRepositorio.SelecionarTodos();

            var dtos = acoes.Select(a => new AcaoOutputDTO()
            {
                Ativo = a.Ativo,
                CodigoNegociacao = a.CodigoNegociacao,
                NomeEmpresa = a.Empresa.Nome,
                Id = a.Id,
                Monitorada = true
            }).ToList();

            return dtos;
        }

        public List<AcaoOutputDTO> ListarAcoes(int id)
        {
            var cliente = _clienteRepositorio.SelecionarPorIdUsuario(id);
            var acoesMonitoradas = _clienteAcaoRepositorio.ListarAcoesMonitoradas(cliente.Id);
            var acoes = _acaoRepositorio.SelecionarTodos().Where(a => a.Ativo == true).ToList();

            var dtos = acoes.Select(a => new AcaoOutputDTO()
            {
                Ativo = a.Ativo,
                CodigoNegociacao = a.CodigoNegociacao,
                NomeEmpresa = a.Empresa.Nome,
                Id = a.Id,
                Monitorada = acoesMonitoradas.Any(am => am.AcaoId == a.Id)
            }).ToList();

            return dtos;
        }

        public AcaoInputDTO SelecionarPorId(int id)
        {
            var acao = _acaoRepositorio.SelecionarPorId(id);
            if (acao == null) throw new EntidadeNaoEncontradaException("Ação não encontrada.");

            var dto = new AcaoInputDTO()
            {
                Id = acao.Id,
                CodigoNegociacao = acao.CodigoNegociacao,
                EmpresaId = acao.Empresa.Id,
                CnpjEmpresa = acao.Empresa.Cnpj,
                SetorEmpresa = acao.Empresa.Setor.Nome
            };

            return dto;
        }

        public void Ativar(int id)
        {
            try
            {
                var acao = _acaoRepositorio.SelecionarPorId(id, asNoTracking: false);
                if (acao == null) 
                    throw new EntidadeNaoEncontradaException("Ação não encontrada.");
                if (!acao.Empresa.Ativo)
                    throw new EntidadeInvalidaException("Empresa desta ação está desativada, ative-a para poder ativar está ação!");

                acao.Ativar();
                _acaoRepositorio.Atualizar(acao);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Acao> SelecionarAcoesPorIdEmpresa(int idEmpresa)
        {
            return _acaoRepositorio.SelecionarAcoesPorIdEmpresa(idEmpresa);
        }
    }
}
