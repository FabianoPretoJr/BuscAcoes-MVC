using BuscAcoes.Dominio.DTOs.Alerta;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Dominio.Servicos
{
    public class AlertaServico : IAlertaServico
    {
        private readonly IAlertaRepositorio _alertaRepositorio;

        public AlertaServico(IAlertaRepositorio alertaRepositorio)
        {
            _alertaRepositorio = alertaRepositorio;
        }

        public void RegistrarAlerta(AlertaDTO dto)
        {
            var alerta = new Alerta(dto.Id, dto.ClienteId, dto.AcaoId, dto.Tolerancia, false);
            _alertaRepositorio.Inserir(alerta);
        }
    }
}
