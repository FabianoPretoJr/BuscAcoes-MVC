using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        public Usuario(int id,string nome, string login, string senha, int perfilId)
            : base(id)
        {
            Nome = nome;
            Login = login;
            Senha = senha;

            PerfilId = perfilId;

            DataCriacao = DateTime.Now;
            Ativo = true;
        }

        private Usuario() : base() { }

        public string Nome { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int PerfilId { get; private set; }
        public bool Ativo { get; private set; }

        public void Desativar()
        {
            this.Ativo = false;
        }

        public void Ativar()
        {
            this.Ativo = true;
        }


        #region Navegação

        public virtual Perfil Perfil { get; private set; }
        //public virtual Cliente Cliente { get; private set; }

        #endregion
    }
}
