using System.Collections.Generic;

namespace BuscAcoes.Dominio.Entidades
{
    public class Empresa : EntidadeBase
    {
        public Empresa(int id, string nome, string cnpj, int setorId)
            : base(id)
        {
            Nome = nome;
            Cnpj = cnpj;
            SetorId = setorId;
            Ativo = true;
        }

        private Empresa() : base() { }

        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public bool Ativo { get; private set; }
        public int SetorId { get; private set; }

        public void Desativar()
        {
            this.Ativo = false;
        }

        public void Ativar()
        {
            this.Ativo = true;
        }


        #region Navegação

        public virtual ICollection<Acao> Acoes { get; private set; }
        public virtual Setor Setor { get; private set; }

        #endregion
    }
}
