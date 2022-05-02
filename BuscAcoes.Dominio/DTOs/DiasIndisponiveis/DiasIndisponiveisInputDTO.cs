using System;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.DiasIndisponiveis
{
    public class DiasIndisponiveisInputDTO
    {
        public bool ParaEdicao { get => this.Id > 0; }

        public int Id { get; set; }

        public DateTime Data { get; set; } = DateTime.Now.AddDays(1);

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
