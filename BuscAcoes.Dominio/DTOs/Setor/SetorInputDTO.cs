using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Setor
{
    public class SetorInputDTO
    {
        public bool ParaEdicao { get => this.Id > 0; }
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
