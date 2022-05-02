using BuscAcoes.Dominio.DTOs.Usuario;
using BuscAcoes.Dominio.Validacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Cliente
{
    public class ClientePerfilInputDTO
    {
        public int IdCliente { get; set; }

        [Display(Name = "CPF")]
        public string Documento { get; set; }

        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; } = DateTime.Now.AddYears(-18);


        public int IdUsuario { get; set; }
        public UsuarioInputDTO Usuario { get; set; }
    }
}
