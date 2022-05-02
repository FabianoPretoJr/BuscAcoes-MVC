using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Acao
{
    public class AcaoInputDTO
    {
        public int Id { get; set; }
        [Display(Name = "Código de negociação")]
        public string CodigoNegociacao { get; set; }
        public int EmpresaId { get; set; }
        public string SetorEmpresa { get; set; }
        public string CnpjEmpresa { get; set; }
    }
}
