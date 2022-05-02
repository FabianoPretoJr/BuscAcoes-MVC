using BuscAcoes.Dominio.DTOs.Setor;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Dominio.Servicos
{
    public class SetorServico : ISetorServico
    {
        private readonly ISetorRepositorio _setorRepositorio;
        private readonly IEmpresaServico _empresaServico;

        public SetorServico(ISetorRepositorio setorRepositorio, IEmpresaServico empresaServico)
        {
            _setorRepositorio = setorRepositorio;
            _empresaServico = empresaServico;
        }

        public IList<SetorOutputDTO> ListarSetores(bool somenteAtivos = false)
        {
            IList<Setor> setores = _setorRepositorio.SelecionarTodos();
            if (somenteAtivos) setores = setores.Where(s => s.Ativo == true).ToList();

            var dtos = setores.Select(s => new SetorOutputDTO()
            {
                Id = s.Id,
                Nome = s.Nome,
                Ativo = s.Ativo
            }).ToList();

            return dtos;
        }

        public SetorInputDTO SelecionarPorId(int id)
        {
            Setor setor = _setorRepositorio.SelecionarPorId(id);

            return new SetorInputDTO()
            {
                Id = setor.Id,
                Nome = setor.Nome
            };
        }

        public void Salvar(SetorInputDTO dto)
        {
            if (dto.ParaEdicao)
            {
                var setorParaAtualizar = new Setor(dto.Id, dto.Nome);
                _setorRepositorio.Atualizar(setorParaAtualizar);
            }
            else
            {
                var setorParaInserir = new Setor(dto.Id, dto.Nome);
                _setorRepositorio.Inserir(setorParaInserir);
            }
        }

        public void Ativar(int id)
        {
            var setor = _setorRepositorio.SelecionarPorId(id, asNoTracking: false);
            if (setor == null)
                throw new EntidadeNaoEncontradaException("Setor não encontrado.");

            setor.Ativar();
            _setorRepositorio.Atualizar(setor);
        }

        public void Desativar(int id)
        {
            var setor = _setorRepositorio.SelecionarPorId(id, asNoTracking: false);
            if (setor == null) throw new EntidadeNaoEncontradaException("Setor não encontrado.");

            setor.Desativar();
            _setorRepositorio.Atualizar(setor);

            this.DesativarEmpresasPorIdSetor(id);
        }

        private void DesativarEmpresasPorIdSetor(int idSetor)
        {
            var empresasDesseSetor = _empresaServico.SelecionarEmpresasPorIdSetor(idSetor);
            foreach (var empresa in empresasDesseSetor)
            {
                _empresaServico.Desativar(empresa.Id);
            }
        }
    }
}
