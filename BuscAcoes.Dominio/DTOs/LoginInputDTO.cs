using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs
{
    public class LoginInputDTO
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
