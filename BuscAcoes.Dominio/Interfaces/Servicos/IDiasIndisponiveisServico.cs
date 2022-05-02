using BuscAcoes.Dominio.DTOs.DiasIndisponiveis;
using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IDiasIndisponiveisServico
    {
        Task<List<DiasIndisponiveisOutputDTO>> ListarDiasIndisponiveis();
        DiasIndisponiveisInputDTO SelecionarDiaIndisponivelPorId(int id);
        DiasIndisponiveis SalvarDiaIndisponivel(DiasIndisponiveisInputDTO dto);
        void DeletarDiaIndisponivel(int id);
        Task<List<DiasIndisponiveisOutputDTO>> SelecionarDiasIndisponiveisApartirDoAtual();

    }
}
