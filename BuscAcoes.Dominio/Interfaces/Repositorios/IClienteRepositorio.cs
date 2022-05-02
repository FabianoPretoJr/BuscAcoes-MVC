using BuscAcoes.Dominio.DTOs.Cliente;
using BuscAcoes.Dominio.Entidades;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IClienteRepositorio : IRepositorioBase<Cliente>
    {
        Cliente SelecionarPorIdUsuario(int id);
        Cliente SelecionarPorDocumento(string documento);
        Cliente SelecionarPorEmail(string email);
    }
}
