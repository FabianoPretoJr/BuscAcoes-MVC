using System;

namespace BuscAcoes.Dominio.Entidades
{
    public class Monitoramento : EntidadeBase
    {
        public Monitoramento(int id, double precoAcao, double percentualVariacao, int acaoId, DateTime atualizadoEm, double percentualVariacaoCalculada)
            : base(id)
        {
            PrecoAcao = precoAcao;
            PercentualVariacao = percentualVariacao;
            AcaoId = acaoId;
            DataCriacao = DateTime.Now;
            AtualizadoEm = atualizadoEm;
            PercentualVariacaoCalculada = percentualVariacaoCalculada;
        }
        private Monitoramento() : base() { }

        public double PrecoAcao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public double PercentualVariacao { get; private set; }
        public DateTime AtualizadoEm { get; private set; }
        public double PercentualVariacaoCalculada { get; private set; }
        public int AcaoId { get; private set; }

        #region Navegação

        public virtual Acao Acao { get; private set; }

        #endregion
    }
}
