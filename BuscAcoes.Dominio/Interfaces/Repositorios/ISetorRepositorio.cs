using BuscAcoes.Dominio.Entidades;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface ISetorRepositorio : IRepositorioBase<Setor>
    {
        Setor SelecionarPorNome(string nome);
    }
}
