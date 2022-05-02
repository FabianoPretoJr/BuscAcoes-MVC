using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.DTOs.Monitoramento
{
    public class MonitoramentoInputDTO
    {
        public int Id { get; set; }
        public int IdAcao { get; set; }
        public double PrecoAcao { get; set; }
        public double PercentualVariacao { get; set; }
        public double PercentualVariacaoCalculada { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
