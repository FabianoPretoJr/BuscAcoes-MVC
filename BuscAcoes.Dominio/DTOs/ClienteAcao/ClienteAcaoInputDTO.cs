using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BuscAcoes.Dominio.DTOs.ClienteAcao
{
    public class ClienteAcaoInputDTO
    {
        public bool ParaEdicao { get => this.Id > 0; }

        public int Id { get; set; }

        public int IdAcao { get; set; }

        public int IdCliente { get; set; }

        public int IdUsuario { get; set; }

        [Display(Name = "Tolerância")]
        //[RegularExpression(@"^\d{1,6}(\,\d{1,2})?$", ErrorMessage = "Use somente números positivos, com duas casas decimais separados por vírgula")]
        public string Tolerancia { get; set; } = "0";

        public string CodigoNegociacao { get; set; }
        public string NomeEmpresa { get; set; }
        public string CnpjEmpresa { get; set; }
        public string Setorempresa { get; set; }
        public bool ReceberEmail { get; set; }
    }
}
