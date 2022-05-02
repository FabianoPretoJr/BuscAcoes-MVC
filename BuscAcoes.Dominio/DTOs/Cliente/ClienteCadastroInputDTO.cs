using BuscAcoes.Dominio.Validacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BuscAcoes.Dominio.DTOs.Cliente
{
    public class ClienteCadastroInputDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }
        public string Senha { get; set; }

        [Display(Name = "CPF")]       
        public string Documento { get; set; }

        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; } = DateTime.Now.AddYears(-18);
    }
}
