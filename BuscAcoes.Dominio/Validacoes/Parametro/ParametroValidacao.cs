using BuscAcoes.Dominio.DTOs.Parametro;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.Validacoes.Parametro
{
    public class ParametroValidacao : AbstractValidator<ParametroInputDTO>
    {
        private readonly IParametroRepositorio _parametroRepositorio;

        public ParametroValidacao(IParametroRepositorio parametroRepositorio)
        {
            _parametroRepositorio = parametroRepositorio;

            RuleFor(x => x.ChaveApi)
                .NotEmpty();

            RuleFor(x => x.LinkApi)
                .NotEmpty()
                .Must(LinkApi => 
                    Uri.TryCreate(LinkApi, UriKind.Absolute, out _)).WithMessage("Url inválida.");

            RuleFor(x => x.MonitoramentoMinutos)
                .NotEmpty()
                .InclusiveBetween(1, 59);
        }
    }
}
