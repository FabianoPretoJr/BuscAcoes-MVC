using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BuscAcoes.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public Usuario SelecionarPorUsuarioESenha(string usuario, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Login == usuario && u.Senha == senha && u.Ativo == true);
        }

        public Usuario SelecionarPorUsuario(string usuario)
        {
            return _context.Usuarios.AsNoTracking().FirstOrDefault(u => u.Login == usuario);
        }
    }
}
