using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuscAcoes.Dominio.DTOs.Alerta
{
    public class AlertaOutputDTO
    {
        public int Id { get; set; }

        [Display(Name = "Tolerância")]
        public double Tolerancia { get; set; }

        public bool Visualizado { get; set; }

        [Display(Name = "Data criação")]
        public DateTime DataCriacao { get; set; }

        public int ClienteId { get; set; }

        public int AcaoId { get; set; }

        [Display(Name = "Código negociação")]
        public string CodigoNegociacao { get; set; }
    }
}
