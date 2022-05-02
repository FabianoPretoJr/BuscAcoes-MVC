using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.Entidades
{
    public class Acao : EntidadeBase
    {
        public Acao(int id, string codigoNegociacao, int empresaId)
            : base(id)
        {
            CodigoNegociacao = codigoNegociacao;
            EmpresaId = empresaId;
            Ativo = true;
        }

        public Acao(string codigoNegociacao, int empresaId)
        {
            CodigoNegociacao = codigoNegociacao;
            EmpresaId = empresaId;
            Ativo = true;
        }

        public Acao(int id, string codigoNegociacao, Empresa empresa)
            : base(id)
        {
            CodigoNegociacao = codigoNegociacao;
            Empresa = empresa;
            Ativo = true;
        }

        private Acao() : base() { }

        public string CodigoNegociacao { get; private set; }
        public int EmpresaId { get; private set; }
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

        public virtual Empresa Empresa { get; private set; }
        public virtual ICollection<ClienteAcao> ClienteAcoes { get; private set; }

        #endregion
    }
}
