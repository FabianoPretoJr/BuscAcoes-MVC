using BuscAcoes.Dominio.DTOs.Setor;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(policy: "Admin")]
    public class SetorController : Controller
    {
        public readonly ISetorServico _setorServico;

        public SetorController(ISetorServico setorServico)
        {
            _setorServico = setorServico;
        }

        public ActionResult Index()
        {
            var setores = _setorServico.ListarSetores();
            return View(setores);
        }

        [HttpPost("Editar/{id:int}")]
        public ActionResult Editar(int id)
        {
            SetorInputDTO dto = _setorServico.SelecionarPorId(id);
            return View("Setor",dto);
        }

        [HttpGet("Cadastrar")]
        public ActionResult Cadastrar()
        {
            return View("Setor",new SetorInputDTO());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(SetorInputDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return View("Setor", dto);

                _setorServico.Salvar(dto);

                return RedirectToAction("Index");
            }
            catch (EntidadeInvalidaException e)
            {
                ModelState.AddModelError(e.Campo, e.Message);
                return View("Setor", dto);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Setor", dto);
            }
        }

        [HttpPost("Desativar/{id:int}")]
        public IActionResult Desativar(int id)
        {
            _setorServico.Desativar(id);

            return RedirectToAction("Index");
        }

        [HttpPost("Ativar/{id:int}")]
        public IActionResult Ativar(int id)
        {
            _setorServico.Ativar(id);

            return RedirectToAction("Index");
        }

    }
}
