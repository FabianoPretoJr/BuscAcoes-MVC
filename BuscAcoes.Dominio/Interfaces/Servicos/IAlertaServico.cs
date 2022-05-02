using BuscAcoes.Dominio.DTOs.Alerta;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IAlertaServico
    {
        void RegistrarAlerta(AlertaDTO dto);
    }
}
