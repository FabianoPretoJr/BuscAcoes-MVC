using System.Collections.Generic;

namespace BuscAcoes.Dominio.Entidades
{
    public class Setor : EntidadeBase
    {
        public Setor(int id,string nome)
            : base(id)
        {
            Nome = nome;
            Ativo = true;
        }

        private Setor() : base() { }

        public string Nome { get; private set; }
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

        public virtual ICollection<Empresa> Empresas { get; private set; }

        #endregion
    }
}
