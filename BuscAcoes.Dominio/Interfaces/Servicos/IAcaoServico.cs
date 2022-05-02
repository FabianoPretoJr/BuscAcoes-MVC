using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IAcaoServico
    {
        List<AcaoOutputDTO> ListarAcoes();
        List<AcaoOutputDTO> ListarAcoes(int id);
        AcaoInputDTO SelecionarPorId(int id);
        void Salvar(AcaoInputDTO dto);
        void Desativar(int id); 
        void Ativar(int id);
        List<Acao> SelecionarAcoesPorIdEmpresa(int idEmpresa);
    }
}
