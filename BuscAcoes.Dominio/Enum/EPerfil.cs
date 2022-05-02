using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.Enum
{
    public enum EPerfil
    {
        [Display(Name = "Administrador")]
        Administrador = 1,
        [Display(Name = "Operador")]
        Operador = 2,
        [Display(Name = "Cliente")]
        Cliente = 3
    }
}
