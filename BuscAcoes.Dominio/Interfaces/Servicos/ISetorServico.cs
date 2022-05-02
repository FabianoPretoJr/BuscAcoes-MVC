using BuscAcoes.Dominio.DTOs.Setor;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface ISetorServico
    {
        IList<SetorOutputDTO> ListarSetores(bool somenteAtivos = false);
        SetorInputDTO SelecionarPorId(int id);
        void Salvar(SetorInputDTO dto);
        void Desativar(int id);
        void Ativar(int id);
    }
}
