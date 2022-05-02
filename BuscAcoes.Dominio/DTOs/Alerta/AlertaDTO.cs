using System;

namespace BuscAcoes.Dominio.DTOs.Alerta
{
    public class AlertaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        
        public double Tolerancia { get; set; }

        public int AcaoId { get; set; }
        public string CodigoNegociacao { get; set; }
        public double AcaoPercentual { get; set; }
        public double AcaoPreco { get; set; }


        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
