using BuscAcoes.Dominio.DTOs.Calendario;
using BuscAcoes.Dominio.DTOs.DiasIndisponiveis;
using BuscAcoes.Dominio.Enum;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(policy: "Operador")]
    public class CalendarioController : Controller
    {
        private readonly IDiasIndisponiveisServico _servicoDiasIndisponiveis;
        private readonly IParametroServico _servicoParametro;

        public CalendarioController(IDiasIndisponiveisServico servicoDiasIndisponiveis, IParametroServico servicoParametro)
        {
            _servicoDiasIndisponiveis = servicoDiasIndisponiveis;
            _servicoParametro = servicoParametro;
        }

        public async Task<IActionResult> Index()
        {
            var partialCalendarioDto = new PartialCalendarioDTO()
            {
                DiasIndisponiveis = await _servicoDiasIndisponiveis.ListarDiasIndisponiveis(),
                Perfil = Enum.Parse<EPerfil>(User.FindFirst(ClaimTypes.Role)?.Value)
            };

            var dto = new CalendarioDTO()
            {
                PartialCalendarioDTO = partialCalendarioDto,
                HorarioDTO = await _servicoParametro.ObterHorarios()
            };

            return View(dto);
        }

        
        [HttpPost("Cadastrar")]
        public IActionResult Cadastrar(DateTime dataEscolhida)
        {
            var dto = new DiasIndisponiveisInputDTO()
            {
                Data = dataEscolhida
            };
            return PartialView("_Dia", dto);
        }

        [HttpPost("Editar/{id:int}")]
        public IActionResult Editar(int id)
        {
            DiasIndisponiveisInputDTO dto = _servicoDiasIndisponiveis.SelecionarDiaIndisponivelPorId(id);
            return PartialView("_Dia", dto);
        }

        [HttpPost("Deletar/{id:int}")]
        public IActionResult Deletar(int id)
        {
            _servicoDiasIndisponiveis.DeletarDiaIndisponivel(id);
            return RedirectToAction("Index");
        }

        [HttpPost("Dia")]
        [ValidateAntiForgeryToken]
        public IActionResult SalvarDia(DiasIndisponiveisInputDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var dia = _servicoDiasIndisponiveis.SalvarDiaIndisponivel(dto);
            return Json(dia);
        }

        [HttpPost("Horario")]
        public IActionResult SalvarHorario(CalendarioDTO dto)
        {
            if (ModelState.IsValid)
            {
                _servicoParametro.AtualizarHorarios(dto.HorarioDTO);
                TempData["Sucesso"] = "O horário foi salvo com sucesso!";
            }
            else
            {
                var errors = ModelStateHelper.GetErrorsFromModelState(ModelState);
                TempData["Erro"] = string.Join(", ", errors.Values);
            }

            return RedirectToAction("Index");
        }
    }
}
