using BuscAcoes.Dominio.DTOs.Usuario;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using FluentValidation;

namespace BuscAcoes.Dominio.Validacoes.Usuario
{
    public class UsuarioValidacao : AbstractValidator<UsuarioInputDTO>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioValidacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;

            RuleFor(x => x.IdUsuario)
                .Must((IdUsuario) =>
                {
                    return UsuarioEncontrado(IdUsuario);
                }).WithMessage("Usuario não encontrado.");

            RuleFor(x => x.Nome)
                .NotEmpty();

            RuleFor(x => x.Senha)
                .NotEmpty().When(x => x.ParaEdicao == false);

            RuleFor(x => x.Login)
                .NotEmpty()
                .Must((dto, Login) =>
                {
                    return UsuarioExiste(dto.IdUsuario, Login);
                }).WithMessage("Já existe um usuário com esse login.");
        }

        private bool UsuarioEncontrado(int id)
        {
            if (id <= 0)
                return true;
            else
            {
                var usuarioNoBanco = _usuarioRepositorio.SelecionarPorId(id);
                return usuarioNoBanco != null;
            }
        }

        private bool UsuarioExiste(int id, string login)
        {
            var usuarioExistente = _usuarioRepositorio.SelecionarPorUsuario(login);
            return usuarioExistente == null || usuarioExistente.Id == id;
        }
    }
}
