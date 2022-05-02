using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IEmpresaRepositorio : IRepositorioBase<Empresa>
    {
        Empresa SelecionarPorCNPJ(string cnpj, bool asNoTracking = true);
        List<Empresa> SelecionarEmpresasPorIdSetor(int idSetor);
        Empresa SelecionarPorIdComAcoes(int id, bool asNoTracking = true);
    }
}
