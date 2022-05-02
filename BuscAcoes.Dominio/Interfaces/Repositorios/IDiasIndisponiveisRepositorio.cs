using BuscAcoes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IDiasIndisponiveisRepositorio : IRepositorioBase<DiasIndisponiveis>
    {
        bool VerificarDiaRegistrado(DateTime data);
        Task<List<DiasIndisponiveis>> SelecionarDiasIndisponiveisApartirDoAtual();
    }
}
