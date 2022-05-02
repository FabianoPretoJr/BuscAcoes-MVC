using BuscAcoes.Dominio.DTOs.Setor;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using FluentValidation;

namespace BuscAcoes.Dominio.Validacoes.Setores
{
    public class SetorValidacao : AbstractValidator<SetorInputDTO>
    {
        private readonly ISetorRepositorio _setorRepositorio;

        public SetorValidacao(ISetorRepositorio setorRepositorio)
        {
            _setorRepositorio = setorRepositorio;

            RuleFor(x => x.Id)
                .Must((Id) =>
                {
                    return SetorEncontrado(Id);
                }).WithMessage("Setor não encontrado.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .Must((dto, Nome) =>
                {
                    return SetorExiste(dto.Id, Nome);
                }).WithMessage("Já existe um setor com esse nome.");
        }

        private bool SetorEncontrado(int id)
        {
            if (id == 0)
                return true;
            else
            { 
                var setorNoBanco = _setorRepositorio.SelecionarPorId(id);            
                return setorNoBanco != null && id > 0 ? true : false; 
            }
        }

        private bool SetorExiste(int id, string nome)
        {
            var setorExistente = _setorRepositorio.SelecionarPorNome(nome);
            return setorExistente != null && setorExistente.Id != id ? false : true;
        }
    }
}
