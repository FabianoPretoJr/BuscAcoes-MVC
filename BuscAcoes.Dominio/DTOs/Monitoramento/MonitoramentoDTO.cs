using System;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Monitoramento
{
    public class MonitoramentoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Código negociação")]
        public string CodigoNegociacao { get; set; }

        [Display(Name = "Preço da ação")]
        public double PrecoAcao { get; set; }
        [Display(Name = "Percentual de variação da API")]
        public double PercentualVariacao { get; set; }

        [Display(Name = "Percentual de variação")]
        public double PercentualVariacaoCalculada { get; set; }

        [Display(Name = "Data do registro")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Última atualização da API")]
        public DateTime AtualizadoEm { get; set; }
    }
}
