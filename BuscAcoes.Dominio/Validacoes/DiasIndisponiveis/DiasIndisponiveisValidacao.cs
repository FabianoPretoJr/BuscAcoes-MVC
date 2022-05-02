using BuscAcoes.Dominio.DTOs.DiasIndisponiveis;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.Validacoes.DiasIndisponiveis
{
    public class DiasIndisponiveisValidacao : AbstractValidator<DiasIndisponiveisInputDTO>
    {
        private readonly IDiasIndisponiveisRepositorio _diasIndisponiveisRepositorio;

        public DiasIndisponiveisValidacao(IDiasIndisponiveisRepositorio diasIndisponiveisRepositorio)
        {
            _diasIndisponiveisRepositorio = diasIndisponiveisRepositorio;

            RuleFor(x => x.Id)
                .Must((Id) =>
                {
                    return DataEncontrada(Id);
                }).WithMessage("Registro de data não encontrado.");

            RuleFor(x => x.Data)
                .NotEmpty();

            RuleFor(x => x.Descricao)
                .NotEmpty();
        }

        private bool DataEncontrada(int id)
        {
            if (id == 0)
                return true;
            else
            {
                var dataNoBanco = _diasIndisponiveisRepositorio.SelecionarPorId(id);
                return dataNoBanco != null && id > 0 ? true : false;
            }
        }
    }
}
