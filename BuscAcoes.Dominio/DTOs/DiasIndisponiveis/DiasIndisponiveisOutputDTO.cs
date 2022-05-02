using System;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.DiasIndisponiveis
{
    public class DiasIndisponiveisOutputDTO
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public bool PodeEditar { get => this.Data.Date > DateTime.Now.Date; }
    }
}
