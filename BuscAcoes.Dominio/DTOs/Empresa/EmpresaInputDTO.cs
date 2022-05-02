using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Empresa
{
    public class EmpresaInputDTO
    {
        public bool ParaEdicao { get => this.Id > 0; }
        public int Id { get; set; }

        [Display(Name = "Nome da empresa")]
        public string Nome { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "Setor da empresa")]
        public int SetorId { get; set; }

        public string NomeSetor { get; set; }
    }
}
