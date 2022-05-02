using BuscAcoes.Dominio.DTOs.Empresa;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Dominio.Validacoes.Empresas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaServico _servicoEmpresa;
        private readonly ISetorServico _servicoSetor;

        public EmpresaController(IEmpresaServico servicoEmpresa, ISetorServico servicoSetor)
        {
            _servicoEmpresa = servicoEmpresa;
            _servicoSetor = servicoSetor;
        }

        [Authorize(policy: "Admin")]
        public IActionResult Index()
        {
            var dto = _servicoEmpresa.ListarEmpresas();
            return View(dto);
        }

        [HttpGet("Cadastrar")]
        [Authorize(policy: "Admin")]
        public IActionResult Cadastrar()
        {
            var dto = new EmpresaInputDTO();
            ViewBag.setor = _servicoSetor.ListarSetores(true);
            return View("Empresa", dto);
        }

        [HttpPost("Editar/{id:int}")]
        [Authorize(policy: "Admin")]
        public IActionResult Editar(int id)
        {
            var dto = _servicoEmpresa.SelecionarPorId(id);
            ViewBag.setor = _servicoSetor.ListarSetores(true);
            return View("Empresa", dto);
        }

        [HttpGet("Acoes")]
        [Authorize(policy: "Operador")]
        public IActionResult Acoes()
        {
            ViewBag.Empresas = CriarListaDeEmpresas();

            var dto = new EmpresaAcoesInputDTO();
            return View(dto);
        }

        [HttpPost("Acoes/{id:int}")]
        [Authorize(policy: "Admin")]
        public IActionResult Acoes(int id)
        {
            var dto = _servicoEmpresa.ObterEmpresaComAcoes(id);
            ViewBag.Empresas = CriarListaDeEmpresas(dto.EmpresaId);
            dto.ParaEdicao = true;

            return View(dto);
        }

        [HttpPost("SalvarAcoes")]
        [ValidateAntiForgeryToken]
        [Authorize(policy: "Operador")]
        public IActionResult SalvarAcoes(EmpresaAcoesInputDTO dto)
        {
            try
            {
                dto.Acoes = _servicoEmpresa.ArrumarAcoes(dto);

                if (!ModelState.IsValid)
                {
                    ViewBag.Empresas = CriarListaDeEmpresas(dto.EmpresaId);
                    return View("Acoes", dto);
                }

                _servicoEmpresa.SalvarAcoesDaEmpresa(dto);

                if (dto.ParaEdicao)
                    return RedirectToAction("Index");

                return RedirectToAction(actionName: "Index", controllerName: "Acao");
            }
            catch (EntidadeInvalidaException e)
            {
                ModelState.AddModelError(e.Campo, e.Message);
                ViewBag.Empresas = CriarListaDeEmpresas(dto.EmpresaId);
                return View("Acoes", dto);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Empresas = CriarListaDeEmpresas(dto.EmpresaId);

                return View("Acoes", dto);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(policy: "Admin")]
        public IActionResult Salvar(EmpresaInputDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.setor = _servicoSetor.ListarSetores(true);
                    return View("Empresa", dto);
                }

                _servicoEmpresa.Salvar(dto);

                return RedirectToAction("Index");
            }
            catch (EntidadeInvalidaException e)
            {
                ModelState.AddModelError(e.Campo, e.Message);
                ViewBag.setor = _servicoSetor.ListarSetores(true);
                return View("Empresa", dto);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.setor = _servicoSetor.ListarSetores(true);
                return View("Empresa", dto);
            }
        }

        [HttpPost("Desativar/{id:int}")]
        [Authorize(policy: "Admin")]
        public IActionResult Desativar(int id)
        {
            _servicoEmpresa.Desativar(id);
            return RedirectToAction("Index");
        }

        [HttpPost("Ativar/{id:int}")]
        [Authorize(policy: "Admin")]
        public IActionResult Ativar(int id)
        {                    
            try
            {
                _servicoEmpresa.Ativar(id);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }
            return RedirectToAction("Index");
        }



        private SelectList CriarListaDeEmpresas(int? idSelecionado = null)
        {
            if (idSelecionado.HasValue)
                return new SelectList(_servicoEmpresa.ListarEmpresas(somenteAtivas: true), "Id", "Nome", idSelecionado);
            else
                return new SelectList(_servicoEmpresa.ListarEmpresas(somenteAtivas: true), "Id", "Nome");

        }
    }
}
