using BuscAcoes.Dominio.DTOs.Acao;
using BuscAcoes.Dominio.Enum;
using BuscAcoes.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    public class AcaoController : Controller
    {
        private readonly IAcaoServico _servicoAcao;
        private readonly IClienteAcaoServico _servicoClienteAcao;
        private readonly IEmpresaServico _servicoEmpresa;

        public AcaoController(IAcaoServico servicoAcao, IClienteAcaoServico servicoClienteAcao,
            IEmpresaServico servicoEmpresa)
        {
            _servicoAcao = servicoAcao;
            _servicoClienteAcao = servicoClienteAcao;
            _servicoEmpresa = servicoEmpresa;
        }

        [Authorize]
        public IActionResult Index()
        {
            var perfilLogadoCliente = Enum.Parse<EPerfil>(User.FindFirst(ClaimTypes.Role)?.Value);
            if (perfilLogadoCliente == EPerfil.Cliente)
            {
                string claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int idUsuarioLogado = Convert.ToInt32(claimId);

                _servicoClienteAcao.CarregarAlertasNaSessao(HttpContext, idUsuarioLogado);

                var acoesCliente = _servicoAcao.ListarAcoes(idUsuarioLogado);
                return View(acoesCliente);
            }

            var acoes = _servicoAcao.ListarAcoes();
            return View(acoes);
        }

        [HttpPost("Editar/{id:int}")]
        [Authorize(policy: "Operador")]
        public IActionResult Editar(int id)
        {
            AcaoInputDTO dto = _servicoAcao.SelecionarPorId(id);
            ViewBag.Empresas = new SelectList(_servicoEmpresa.ListarEmpresas(somenteAtivas: true), "Id", "Nome");

            return View("Acao", dto);
        }

        [HttpPost("Desativar/{id:int}")]
        [Authorize(policy: "Operador")]
        public IActionResult Desativar(int id)
        {
            _servicoAcao.Desativar(id);

            return RedirectToAction("Index");
        }

        [HttpPost("Ativar/{id:int}")]
        [Authorize(policy: "Operador")]
        public IActionResult Ativar(int id)
        {
            try
            {
                _servicoAcao.Ativar(id);
            }
            catch(Exception ex)
            {
                TempData["Erro"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(policy: "Operador")]
        public IActionResult Salvar(AcaoInputDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Empresas = new SelectList(_servicoEmpresa.ListarEmpresas(somenteAtivas: true), "Id", "Nome", selectedValue: dto.EmpresaId);
                return View("Acao", dto);
            }

            _servicoAcao.Salvar(dto);
            return RedirectToAction("Index");
        }

        [HttpGet("ObterDadosEmpresa/{id:int}")]
        [Authorize(policy: "Operador")]
        public IActionResult ObterDadosEmpresa(int id)
        {
            var empresa = _servicoEmpresa.ObterEmpresaComAcoes(id);
            return Ok(empresa);
        }
    }
}
