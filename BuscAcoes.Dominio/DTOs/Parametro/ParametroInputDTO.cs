using System;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Parametro
{
    public class ParametroInputDTO
    {
        public string ChaveApi { get; set; }

        public string LinkApi { get; set; }

        public int MonitoramentoMinutos { get; set; }
    }
}
