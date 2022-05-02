using System;

namespace BuscAcoes.Dominio.DTOs.Monitoramento
{
    public class Filtros
    {
        public string CodigoAcao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public bool TodosRegistros { get; set; }
    }
}
