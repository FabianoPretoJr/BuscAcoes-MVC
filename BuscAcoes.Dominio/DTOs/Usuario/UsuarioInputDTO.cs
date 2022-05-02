using System;

namespace BuscAcoes.Dominio.DTOs.Usuario
{
    public class UsuarioInputDTO
    {
        public bool ParaEdicao { get => this.IdUsuario > 0; }
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
