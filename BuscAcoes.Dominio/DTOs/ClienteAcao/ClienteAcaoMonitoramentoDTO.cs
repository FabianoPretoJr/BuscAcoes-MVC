namespace BuscAcoes.Dominio.DTOs.ClienteAcao
{
    public class ClienteAcaoMonitoramentoDTO
    {
        public string CodigoNegociacao { get; set; }
        public int AcaoId { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public double Tolerancia { get; set; }
        public bool ReceberEmail { get; set; }
    }
}
