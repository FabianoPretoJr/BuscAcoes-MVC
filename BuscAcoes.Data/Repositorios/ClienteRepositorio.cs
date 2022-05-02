using BuscAcoes.Dominio.DTOs.Cliente;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BuscAcoes.Data.Repositorios
{
    public class ClienteRepositorio : RepositorioBase<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public Cliente SelecionarPorDocumento(string documento)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Documento == documento);
        }

        public Cliente SelecionarPorEmail(string email)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }

        public Cliente SelecionarPorIdUsuario(int id)
        {
            return _dbSet.AsNoTracking().Include(c => c.Usuario).FirstOrDefault(e => e.UsuarioId == id);
        }


    }
}
