using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.ClienteAcao
{
    public class ClienteAcaoOutputDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdAcao { get; set; }

        [Display(Name = "Código negociação")]
        public string CodigoNegociacao { get; set; }
        [Display(Name = "Última variação")]
        public double UltimaVariacao { get; set; }
        [Display(Name = "Último preço")]
        public double UltimoPreco { get; set; }

        [Display(Name = "Tolerância")]
        public double Tolerancia { get; set; }
        public bool AcaoAtiva { get; set; }
    }
}
