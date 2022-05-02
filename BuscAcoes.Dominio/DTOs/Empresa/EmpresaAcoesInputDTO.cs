using BuscAcoes.Dominio.DTOs.Acao;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Empresa
{
    public class EmpresaAcoesInputDTO
    {
        public bool ParaEdicao { get; set; }
        public int EmpresaId { get; set; }
        public string SetorEmpresa { get; set; }
        public string CnpjEmpresa { get; set; }

        [Display(Name = "Código de negociação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public List<string> CodigosNegociacao { get; set; } = new List<string>();
        public List<AcaoOutputDTO> Acoes { get; set; } = new List<AcaoOutputDTO>();
    }
}
