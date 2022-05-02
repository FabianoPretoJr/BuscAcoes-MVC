using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuscAcoes.Dominio.DTOs.Monitoramento
{
    public class MonitoramentoOutputDTO
    {
        public List<MonitoramentoDTO> Monitoramentos { get; set; } = new List<MonitoramentoDTO>();

        public Filtros Filtros { get; set; } = new Filtros();
    }
}
