using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using FluentValidation;

namespace BuscAcoes.Dominio.Validacoes.Acao
{
    public class AcaoValidacao : AbstractValidator<AcaoInputDTO>
    {
        private readonly IAcaoRepositorio _acaoRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public AcaoValidacao(IAcaoRepositorio acaoRepositorio, IEmpresaRepositorio empresaRepositorio)
        {
            _acaoRepositorio = acaoRepositorio;
            _empresaRepositorio = empresaRepositorio;

            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.CodigoNegociacao)
                .MinimumLength(4)
                .Must((dto,codNegociacao) => {
                    bool acaoJaExiste = VerificarSeAcaoJaExiste(dto.Id, codNegociacao);
                    bool acaoValida = (acaoJaExiste == false);

                    return acaoValida;
                })
                .WithMessage("Essa Ação já existe.")
                .NotEmpty();

            RuleFor(x => x.EmpresaId)
                .NotEmpty()
                .Must((Id) => EmpresaEncontrada(Id))
                .WithMessage("Empresa não encontrada.");
        }

        private bool EmpresaEncontrada(int id)
        {
            var empresaNoBanco = _empresaRepositorio.SelecionarPorId(id);
            return empresaNoBanco != null;
        }

        private bool VerificarSeAcaoJaExiste(int id, string codigoNegociacao)
        {
            var acaoExistente = _acaoRepositorio.SelecionarPorCodigoNegociacao(codigoNegociacao);

            return acaoExistente != null && acaoExistente.Id != id;
        }
    }
}
