using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.DTOs.Empresa;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using FluentValidation;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Validacoes.Empresas
{
    public class EmpresaAcoesValidacao : AbstractValidator<EmpresaAcoesInputDTO>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IAcaoRepositorio _acaoRepositorio;

        private readonly IEmpresaServico _empresaServico;
        public EmpresaAcoesValidacao(IEmpresaRepositorio empresaRepositorio, IAcaoRepositorio acaoRepositorio, IEmpresaServico empresaServico)
        {
            _empresaRepositorio = empresaRepositorio;
            _acaoRepositorio = acaoRepositorio;
            _empresaServico = empresaServico;

            RuleFor(x => x.EmpresaId)
                .Must((dto, Id) => EmpresaEncontrada(Id))
                .WithMessage("Empresa não encontrada.");

            RuleFor(x => x.CodigosNegociacao)
                .Must((dto, _, context) => ValidarAcoes(dto, context))
                .WithMessage("Ações já existentes: {AcoesInvalidas}")
                .NotEmpty();
        }

        private bool EmpresaEncontrada(int id)
        {
            if (id == 0)
                return true;
            else
            {
                var empresaNoBanco = _empresaRepositorio.SelecionarPorId(id);
                return empresaNoBanco != null && id > 0 ? true : false;
            }
        }

        private bool ValidarAcoes(EmpresaAcoesInputDTO dto, ValidationContext<EmpresaAcoesInputDTO> context)
        {
            bool valido = true;
            List<string> codigosNegociacoesInvalidas = new List<string>();

            List<AcaoOutputDTO> acoes = _empresaServico.ArrumarAcoes(dto);
            foreach (var acao in acoes)
            {
                bool acaoJaExiste = VerificarSeAcaoJaExiste(acao.Id, acao.CodigoNegociacao);

                if (acaoJaExiste)
                {
                    codigosNegociacoesInvalidas.Add(acao.CodigoNegociacao);
                    valido = false;
                }
            }

            if (!valido)
            {
                string mensagemAcoesInvalidas = string.Join(",", codigosNegociacoesInvalidas);
                context.MessageFormatter.AppendArgument("AcoesInvalidas", mensagemAcoesInvalidas);
            }

            return valido;
        }

        private bool VerificarSeAcaoJaExiste(int id, string codigoNegociacao)
        {
            var acaoExistente = _acaoRepositorio.SelecionarPorCodigoNegociacao(codigoNegociacao);

            return acaoExistente != null && acaoExistente.Id != id;
        }

    }
}
