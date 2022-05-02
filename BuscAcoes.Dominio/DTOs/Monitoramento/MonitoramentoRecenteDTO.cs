namespace BuscAcoes.Dominio.DTOs.Monitoramento
{
    public class MonitoramentoRecenteDTO
    {
        public int AcaoId { get; set; }
        public string CodigoNegociacao { get; set; }
        public double Preco { get; set; }
        public double PercentualVariacaoCalculada { get; set; }
    }
}
