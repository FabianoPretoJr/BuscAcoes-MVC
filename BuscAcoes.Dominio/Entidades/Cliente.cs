using System;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public Cliente(int id, string email, string documento, DateTime dataNasc, int usuarioId)
            : base(id)
        {
            
            Email = email;
            Documento = documento;
            DataNasc = dataNasc;
            UsuarioId = usuarioId;
        }

        private Cliente() : base() { }

        public string Documento { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNasc { get; private set; }
        public int UsuarioId { get; private set; }

        #region Navegação

        public virtual Usuario Usuario { get; private set; }
        public virtual ICollection<ClienteAcao> ClienteAcoes { get; private set; }

        #endregion

    }
}
