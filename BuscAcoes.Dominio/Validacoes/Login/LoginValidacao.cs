using BuscAcoes.Dominio.DTOs;
using FluentValidation;

namespace BuscAcoes.Dominio.Validacoes.Login
{
    public class LoginValidacao : AbstractValidator<LoginInputDTO>
    {
        public LoginValidacao()
        {
            RuleFor(x => x.Usuario)
                .NotEmpty();

            RuleFor(x => x.Senha)
                .NotEmpty();
        }
    }
}
