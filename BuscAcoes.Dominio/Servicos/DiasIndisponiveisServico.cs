using BuscAcoes.Dominio.DTOs.DiasIndisponiveis;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Servicos
{
    public class DiasIndisponiveisServico : IDiasIndisponiveisServico
    {
        private readonly IDiasIndisponiveisRepositorio _diasIndisponiveisRepositorio;

        public DiasIndisponiveisServico(IDiasIndisponiveisRepositorio diasIndisponiveisRepositorio)
        {
            _diasIndisponiveisRepositorio = diasIndisponiveisRepositorio;
        }

        public async Task<List<DiasIndisponiveisOutputDTO>> ListarDiasIndisponiveis()
        {
            var diasIndisponiveis = await _diasIndisponiveisRepositorio.SelecionarTodosAsync();

            var dtos = diasIndisponiveis.Select(d => new DiasIndisponiveisOutputDTO()
            {
                Id = d.Id,
                Data = d.Data,
                Descricao = d.Descricao
            }).ToList();

            return dtos;
        }

        public DiasIndisponiveis SalvarDiaIndisponivel(DiasIndisponiveisInputDTO dto)
        {
            if (dto.ParaEdicao)
            {
                var diaIndisponivelAtualizado = new DiasIndisponiveis(dto.Id, dto.Data, dto.Descricao);
                _diasIndisponiveisRepositorio.Atualizar(diaIndisponivelAtualizado);
                return diaIndisponivelAtualizado;
            }
            else
            {
                var diaIndisponivel = new DiasIndisponiveis(dto.Id, dto.Data, dto.Descricao);
                _diasIndisponiveisRepositorio.Inserir(diaIndisponivel);
                return diaIndisponivel;
            }
        }

        public void DeletarDiaIndisponivel(int id)
        {
            try
            {
                _diasIndisponiveisRepositorio.Deletar(id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public DiasIndisponiveisInputDTO SelecionarDiaIndisponivelPorId(int id)
        {
            var diaIndisponivel = _diasIndisponiveisRepositorio.SelecionarPorId(id);
            if (diaIndisponivel == null) 
                throw new EntidadeNaoEncontradaException("Reegistro de data não encontrado.");

            var dto = new DiasIndisponiveisInputDTO()
            {
                Id = diaIndisponivel.Id,
                Data = diaIndisponivel.Data,
                Descricao = diaIndisponivel.Descricao
            };

            return dto;
        }

        public async Task<List<DiasIndisponiveisOutputDTO>> SelecionarDiasIndisponiveisApartirDoAtual()
        {
            var dias = await _diasIndisponiveisRepositorio.SelecionarDiasIndisponiveisApartirDoAtual();

            var dtos = dias.Select(d => new DiasIndisponiveisOutputDTO()
            {
                Id = d.Id,
                Data = d.Data,
                Descricao = d.Descricao
            }).ToList();

            return dtos;
        }
    }
}
