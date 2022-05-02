using BuscAcoes.Dominio.DTOs.Cliente;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Util;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.Validacoes.Cliente
{
    public class ClienteCadastroValidacao : AbstractValidator<ClienteCadastroInputDTO>
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteCadastroValidacao(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;

            RuleFor(x => x.Nome)
                .NotEmpty();

            RuleFor(x => x.Login)
                .NotEmpty();

            RuleFor(x => x.Senha)
                .NotEmpty();

            RuleFor(x => x.Documento)
                .NotEmpty()
                .Must((Documento) =>
                {
                    return CPFValidator.IsCpf(Documento);
                }).WithMessage("CPF inválido.")
                .Must((dto, Documento) =>
                {
                    return CpfExiste(Documento);
                }).WithMessage("CPF já cadastrado no sistema.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must((Email) => 
                {
                    return EmailExiste(Email);
                }).WithMessage("Email já cadastrado no sistema.");

            RuleFor(x => x.DataNasc)
                .NotEmpty();
        }

        private bool EmailExiste(string email)
        {
            var validaEmail = _clienteRepositorio.SelecionarPorEmail(email);
            return validaEmail != null ? false : true;
        }

        private bool CpfExiste(string documento)
        {
            var validaDocumento = _clienteRepositorio.SelecionarPorDocumento(documento);
            return validaDocumento != null ? false : true;
        }
    }
}
