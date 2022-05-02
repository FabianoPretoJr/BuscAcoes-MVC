using BuscAcoes.Dominio.DTOs.ClienteAcao;
using BuscAcoes.Dominio.DTOs.Monitoramento;
using BuscAcoes.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(policy: "Cliente")]
    public class ClienteAcaoController : Controller
    {
        private readonly IClienteAcaoServico _servicoAcaoCliente;
        private readonly IMonitoramentoServico _servicoMonitoramento;

        public ClienteAcaoController(IClienteAcaoServico servicoAcaoCliente, IMonitoramentoServico servicoMonitoramento)
        {
            _servicoAcaoCliente = servicoAcaoCliente;
            _servicoMonitoramento = servicoMonitoramento;
        }

        public IActionResult Acoes()
        {
            CarregarAlertas();

            var id = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            var clienteAcoes = _servicoAcaoCliente.ListarAcoesMonitoradas(Convert.ToInt32(id.Value));

            return View(clienteAcoes);
        }

        [HttpPost("Cadastrar/{id:int}")]
        public IActionResult Cadastrar(int id)
        {
            var dto = _servicoAcaoCliente.ObterDadosAcao(id);
            dto.IdUsuario = Convert.ToInt32(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return View("Alerta", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(ClienteAcaoInputDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Alerta", dto);

                _servicoAcaoCliente.Salvar(dto);
                return RedirectToAction("Acoes");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("Editar/{id:int}")]
        public IActionResult Editar(int id)
        {
            var dto = _servicoAcaoCliente.ObterDadosClienteAcao(id);

            return View("Alerta", dto);
        }

        [HttpPost("Deletar/{id:int}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _servicoAcaoCliente.Deletar(id);

                return RedirectToAction("Acoes");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("Historico/{cod}")]
        public IActionResult Historico(string cod, string returnUrl = null, string view = null, MonitoramentoOutputDTO dto = null)
        {
            CarregarAlertas();

            if (dto == null) dto = new MonitoramentoOutputDTO();

            dto.Filtros.CodigoAcao = cod;

            var monitoramentos = _servicoMonitoramento.ObterMonitoramentosAcoes(dto);
            dto.Monitoramentos = monitoramentos;

            if (returnUrl != null) TempData["ReturnUrl"] = returnUrl;
            if (view != null) TempData["view"] = view;

            return View("Historico", dto);
        }

        [HttpGet("Alerta/{id:int}")]
        public IActionResult VisualizarAlerta(int id)
        {
            var alerta = _servicoAcaoCliente.AtualizarVisualizacaoAlerta(id);

            int idUsuarioLogado = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            _servicoAcaoCliente.CarregarAlertasNaSessao(HttpContext, idUsuarioLogado);

            return Historico(alerta.Acao.CodigoNegociacao);
        }

        private void CarregarAlertas()
        {
            string claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUsuarioLogado = Convert.ToInt32(claimId);

            _servicoAcaoCliente.CarregarAlertasNaSessao(HttpContext, idUsuarioLogado);
        }
    }
}
