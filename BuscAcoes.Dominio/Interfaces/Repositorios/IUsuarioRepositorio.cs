using BuscAcoes.Dominio.Entidades;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Usuario SelecionarPorUsuarioESenha(string usuario, string senha);
        Usuario SelecionarPorUsuario(string usuario);
    }
}
