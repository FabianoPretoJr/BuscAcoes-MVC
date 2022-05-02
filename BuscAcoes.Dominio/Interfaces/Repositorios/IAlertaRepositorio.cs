using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Interfaces.Repositorios
{
    public interface IAlertaRepositorio : IRepositorioBase<Alerta>
    {
        List<Alerta> SelecionarPorUsuario(int usuarioId, bool apenasNaoVisualizados = true, bool comUsuario = false);
        void VisualizarTodosAlertas(int usuarioId);
    }
}
