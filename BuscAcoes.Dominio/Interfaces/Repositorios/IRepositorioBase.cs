using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        T SelecionarPorId(int id, bool asNoTracking = true);
        IList<T> SelecionarTodos();
        Task<IList<T>> SelecionarTodosAsync();
        void Inserir(T entidade);
        void Atualizar(T entidade);
        void Deletar(int id);
    }
}
