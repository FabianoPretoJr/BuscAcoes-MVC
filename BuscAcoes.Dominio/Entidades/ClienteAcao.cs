namespace BuscAcoes.Dominio.Entidades
{
    public class ClienteAcao : EntidadeBase
    {
        public ClienteAcao(int id, int clienteId, int acaoId, double tolerancia, bool receberEmail)
            : base(id)
        {
            ClienteId = clienteId;
            AcaoId = acaoId;
            Tolerancia = tolerancia;
            ReceberEmail = receberEmail;
        }

        private ClienteAcao() : base() { }

        public int ClienteId { get; private set; }
        public int AcaoId { get; private set; }
        public double Tolerancia { get; set; }
        public bool ReceberEmail { get; set; }

        #region Navegação

        public virtual Cliente Cliente { get; private set; }
        public virtual Acao Acao { get; private set; }

        #endregion
    }
}
