using System;

namespace BuscAcoes.Dominio.Entidades
{
    public class Alerta : EntidadeBase
    {
        public Alerta(int id, int clienteId, int acaoId, double tolerancia, bool visualizado)
            : base(id)
        {
            ClienteId = clienteId;
            AcaoId = acaoId;
            Tolerencia = tolerancia;
            Visualizado = visualizado;
            DataCriacao = DateTime.Now;
        }

        private Alerta() : base() { }

        public int ClienteId { get; private set; }
        public int AcaoId { get; private set; }
        public double Tolerencia { get; private set; }
        public bool Visualizado { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public void Visualizar()
        {
            this.Visualizado = true;
        }

        #region Navegação

        public virtual Cliente Cliente { get; private set; }
        public virtual Acao Acao { get; private set; }

        #endregion
    }
}
