using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.DTOs.Empresa;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Dominio.Servicos
{
    public class EmpresaServico : IEmpresaServico
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IAcaoRepositorio _acaoRepositorio;
        private readonly IAcaoServico _acaoServico;

        public EmpresaServico(IEmpresaRepositorio empresaRepositorio, IAcaoServico acaoServico, IAcaoRepositorio acaoRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
            _acaoRepositorio = acaoRepositorio;
            _acaoServico = acaoServico;
        }

        public void Ativar(int id)
        {
            try
            {
                var empresa = _empresaRepositorio.SelecionarPorId(id, asNoTracking: false);
                if (empresa == null)
                    throw new EntidadeNaoEncontradaException("Empresa não encontrada.");
                if (!empresa.Setor.Ativo)
                    throw new EntidadeInvalidaException("Setor desta empresa está desativado, ative-o para poder ativar está empresa!");

                empresa.Ativar();
                _empresaRepositorio.Atualizar(empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Desativar(int id)
        {
            try
            {
                var empresa = _empresaRepositorio.SelecionarPorId(id, asNoTracking: false);
                if (empresa == null)
                    throw new EntidadeNaoEncontradaException("Empresa não encontrada.");

                empresa.Desativar();
                _empresaRepositorio.Atualizar(empresa);

                this.DesativarAcoesPorIdEmpresa(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmpresaOutputDTO> ListarEmpresas(bool somenteAtivas = false)
        {
            IList<Empresa> listaEmpresas = _empresaRepositorio.SelecionarTodos();
            if (somenteAtivas)
                listaEmpresas = listaEmpresas.Where(e => e.Ativo == true).ToList();

            return listaEmpresas.Select(e =>
            {
                return new EmpresaOutputDTO()
                {
                    Id = e.Id,
                    Nome = e.Nome,
                    Cnpj = e.Cnpj,
                    Ativo = e.Ativo,
                    SetorId = e.SetorId,
                    SetorNome = e.Setor.Nome
                };
            }).OrderBy(e => e.Nome).ToList();
        }

        public void Salvar(EmpresaInputDTO dto)
        {
            if (dto.ParaEdicao)
            {
                var empresaAtualizada = new Empresa(dto.Id, dto.Nome, dto.Cnpj, dto.SetorId);
                _empresaRepositorio.Atualizar(empresaAtualizada);
            }
            else
            {
                var empresaNova = new Empresa(dto.Id, dto.Nome, dto.Cnpj, dto.SetorId);
                _empresaRepositorio.Inserir(empresaNova);
            }
        }

        public EmpresaInputDTO SelecionarPorId(int id)
        {
            try
            {
                var empresa = _empresaRepositorio.SelecionarPorId(id);
                if (empresa == null)
                    throw new EntidadeNaoEncontradaException("Empresa não encontrada.");

                return new EmpresaInputDTO()
                {
                    Id = empresa.Id,
                    Nome = empresa.Nome,
                    Cnpj = empresa.Cnpj,
                    SetorId = empresa.SetorId,
                    NomeSetor = empresa.Setor.Nome
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empresa> SelecionarEmpresasPorIdSetor(int idSetor)
        {
            return _empresaRepositorio.SelecionarEmpresasPorIdSetor(idSetor);
        }

        private void DesativarAcoesPorIdEmpresa(int idEmpresa)
        {
            var acoesDessaEmpresa = _acaoServico.SelecionarAcoesPorIdEmpresa(idEmpresa);
            foreach (var acao in acoesDessaEmpresa)
            {
                _acaoServico.Desativar(acao.Id);
            }
        }

        public EmpresaAcoesInputDTO ObterEmpresaComAcoes(int id)
        {
            var empresa = _empresaRepositorio.SelecionarPorIdComAcoes(id);
            if (empresa == null)
                throw new EntidadeNaoEncontradaException("Empresa não encontrada.");



            return new EmpresaAcoesInputDTO()
            {
                EmpresaId = empresa.Id,
                CnpjEmpresa = empresa.Cnpj,
                SetorEmpresa = empresa.Setor.Nome,
                Acoes = empresa.Acoes.Select(a => new AcaoOutputDTO()
                {
                    CodigoNegociacao = a.CodigoNegociacao,
                    Id = a.Id,
                    Ativo = a.Ativo
                }).ToList(),
                CodigosNegociacao = empresa.Acoes.Select(a => a.CodigoNegociacao).ToList(),

            };
        }

        public void SalvarAcoesDaEmpresa(EmpresaAcoesInputDTO dto)
        {
            var acoes = dto.Acoes.Select(a => new Acao(a.Id, a.CodigoNegociacao, a.EmpresaId)).ToList();
            _acaoRepositorio.AtualizarAcoes(dto.EmpresaId, acoes);
        }

        public List<AcaoOutputDTO> ArrumarAcoes(EmpresaAcoesInputDTO dto)
        {
            var acoesArrumadas = new List<AcaoOutputDTO>();
            var codigosNegociacao = new List<string>();
            codigosNegociacao.AddRange(dto.CodigosNegociacao);

            if (dto.EmpresaId > 0)
            {
                List<Acao> acoesNoBanco = _acaoServico.SelecionarAcoesPorIdEmpresa(dto.EmpresaId)?.ToList();

                List<AcaoOutputDTO> acoesParaArrumar = codigosNegociacao
                                                            .Select(codNegociacao =>
                                                            new AcaoOutputDTO
                                                            {
                                                                CodigoNegociacao = codNegociacao,
                                                                EmpresaId = dto.EmpresaId
                                                            })
                                                            .ToList();

                foreach (var acao in acoesNoBanco)
                {
                    if (codigosNegociacao.Contains(acao.CodigoNegociacao))
                    {
                        acoesArrumadas.Add(new AcaoOutputDTO
                        {
                            Id = acao.Id,
                            CodigoNegociacao = acao.CodigoNegociacao,
                            EmpresaId = acao.EmpresaId,
                            Ativo = acao.Ativo
                        });
                        codigosNegociacao.Remove(acao.CodigoNegociacao);
                    }
                }
            }

            foreach (var codigoNegociacao in codigosNegociacao)
            {
                acoesArrumadas.Add(new AcaoOutputDTO
                {
                    CodigoNegociacao = codigoNegociacao,
                    EmpresaId = dto.EmpresaId,
                    Ativo = true
                });
            }

            return acoesArrumadas;
        }

    }
}
