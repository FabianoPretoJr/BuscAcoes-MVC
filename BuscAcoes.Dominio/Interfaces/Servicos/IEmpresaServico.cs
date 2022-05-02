using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.DTOs.Empresa;
using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IEmpresaServico
    {
        List<EmpresaOutputDTO> ListarEmpresas(bool somenteAtivas = false);
        EmpresaInputDTO SelecionarPorId(int id);
        void Salvar(EmpresaInputDTO dto);
        void Desativar(int id);
        void Ativar(int id);
        List<Empresa> SelecionarEmpresasPorIdSetor(int idSetor);
        EmpresaAcoesInputDTO ObterEmpresaComAcoes(int id);
        void SalvarAcoesDaEmpresa(EmpresaAcoesInputDTO dto);
        List<AcaoOutputDTO> ArrumarAcoes(EmpresaAcoesInputDTO dto);
    }
}
