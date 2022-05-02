using System.Collections.Generic;

namespace BuscAcoes.Dominio.Entidades
{
    public class Perfil : EntidadeBase
    {
        public Perfil(int id, string descricao)
            : base(id)
        {
            Descricao = descricao;
        }

        private Perfil() : base() { }

        public string Descricao { get; private set; }

        #region Navegação

        public virtual ICollection<Usuario> Usuarios { get; private set; }
        
        #endregion
    }
}
