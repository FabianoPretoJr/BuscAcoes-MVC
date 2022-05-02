using BuscAcoes.Dominio.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Usuario
{
    public class UsuarioOutputDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        [Display(Name = "Data criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Perfil")]
        public EPerfil Perfil { get; set; }
        public bool Ativo { get; set; }
    }
}
