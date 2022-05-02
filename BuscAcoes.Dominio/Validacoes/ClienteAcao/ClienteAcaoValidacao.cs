using BuscAcoes.Dominio.DTOs.ClienteAcao;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using FluentValidation;
using System;

namespace BuscAcoes.Dominio.Validacoes.ClienteAcao
{
    public class ClienteAcaoValidacao : AbstractValidator<ClienteAcaoInputDTO>
    {
        private readonly IClienteAcaoRepositorio _clienteAcaoRepositorio;

        public ClienteAcaoValidacao(IClienteAcaoRepositorio clienteAcaoRepositorio)
        {
            _clienteAcaoRepositorio = clienteAcaoRepositorio;

            RuleFor(x => x.Id)
                .Must((Id) =>
                {
                    return AcaoMonitoradaEncontrada(Id);
                }).WithMessage("Ação monitorada não encontrada.");

            RuleFor(x => x.Tolerancia)
                .NotEmpty()
                .Matches(@"^\d{1,6}(\,\d{1,2})?$").WithMessage("Use somente números positivos, com duas casas decimais separados por vírgula");
        }

        private bool AcaoMonitoradaEncontrada(int id)
        {
            if (id == 0)
                return true;
            else
            {
                var clienteAcaoNoBanco = _clienteAcaoRepositorio.SelecionarPorId(id);
                return id > 0 && clienteAcaoNoBanco != null ? true : false;
            }
        }
    }
}
