using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Empresa
{
    public class EmpresaOutputDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "CPNJ")]
        public string Cnpj { get; set; }

        public bool Ativo { get; set; }

        public int SetorId { get; set; }

        [Display(Name = "Setor")]
        public string SetorNome { get; set; }
    }
}
