using BuscAcoes.Dominio.DTOs.Empresa;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Util;
using FluentValidation;

namespace BuscAcoes.Dominio.Validacoes.Empresas
{
    public class EmpresaValidacao : AbstractValidator<EmpresaInputDTO>
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresaValidacao(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;

            RuleFor(x => x.Id)
                .Must((dto, Id) =>
                {
                    return EmpresaEncontrada(Id, dto.Cnpj);
                }).WithMessage("Empresa não encontrada.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Cnpj)
                .NotEmpty()
                .Must((Cnpj) =>
                {
                    return CNPJValidator.IsCnpj(Cnpj);
                }).WithMessage("CNPJ inválido.")
                .Must((dto, Cnpj) =>
                {
                    return CNPJExiste(dto.Id, Cnpj);
                }).WithMessage("CNPJ já cadastrado no sistema.");

            RuleFor(x => x.SetorId)
                .NotEmpty()
                .GreaterThan(0);
        }

        private bool EmpresaEncontrada(int id, string cnpj)
        {
            if (id == 0)
                return true;
            else
            { 
                var empresaNoBanco = _empresaRepositorio.SelecionarPorCNPJ(cnpj);
                return empresaNoBanco != null && id > 0  ? true : false;
            }
        }

        private bool CNPJExiste(int id, string cnpj)
        {
            var cnpjExistente = _empresaRepositorio.SelecionarPorCNPJ(cnpj);
            return cnpjExistente != null && cnpjExistente.Cnpj == cnpj && id == 0 ? false : true;
        }
    }
}
