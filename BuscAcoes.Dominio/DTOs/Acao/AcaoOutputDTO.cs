using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Acao
{
    public class AcaoOutputDTO
    {
        public int Id { get; set; }
        [Display(Name = "Código de negociação")]
        public string CodigoNegociacao { get; set; }
        public int EmpresaId { get; set; }
        [Display(Name = "Empresa")]
        public string NomeEmpresa { get; set; }
        public bool Ativo { get; set; }
        public bool Monitorada { get; set; }
    }
}
