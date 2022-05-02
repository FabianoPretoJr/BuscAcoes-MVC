using BuscAcoes.Dominio.DTOs.Cliente;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Util;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.Validacoes.Cliente
{
    public class ClientePerfilValidacao : AbstractValidator<ClientePerfilInputDTO>
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClientePerfilValidacao(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;

            RuleFor(x => x.Documento)
                .NotEmpty()
                .Must((Documento) =>
                {
                    return CPFValidator.IsCpf(Documento);
                }).WithMessage("CPF inválido.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must((dto, Email) =>
                {
                    return EmailExiste(dto.IdCliente, Email);
                }).WithMessage("Email já cadastrado no sistema.");

            RuleFor(x => x.DataNasc)
                .NotEmpty();
        }

        private bool EmailExiste(int id, string email)
        {
            var validaEmail = _clienteRepositorio.SelecionarPorEmail(email);
            return validaEmail != null && id != validaEmail.Id ? false : true;
        }
    }
}
