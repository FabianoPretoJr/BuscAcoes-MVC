using BuscAcoes.Dominio.DTOs.Monitoramento;
using BuscAcoes.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(policy: "Admin")]
    public class MonitoramentoController : Controller
    {
        private readonly IMonitoramentoServico _servicoMonitoramento;

        public MonitoramentoController(IMonitoramentoServico servicoMonitoramento)
        {
            _servicoMonitoramento = servicoMonitoramento;
        }

        public IActionResult Index()
        {
            var dto = new MonitoramentoOutputDTO()
            {
                Monitoramentos = _servicoMonitoramento.ObterMonitoramentosAcoes()
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult Filtrar(MonitoramentoOutputDTO dto)
        {
            dto.Filtros.TodosRegistros = true;

            var monitoramentos = _servicoMonitoramento.ObterMonitoramentosAcoes(dto);
            dto.Monitoramentos = monitoramentos;

            return View("Index", dto);
        }
    }
}
