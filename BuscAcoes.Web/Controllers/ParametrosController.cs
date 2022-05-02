using BuscAcoes.Dominio.DTOs.Parametro;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Web.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    [Authorize(policy: "Admin")]
    public class ParametrosController : Controller
    {
        private readonly IParametroServico _servicoParametro;

        public ParametrosController(IParametroServico servicoParametro)
        {
            _servicoParametro = servicoParametro;
        }

        public async Task<IActionResult> Index()
        {
            var dto = await _servicoParametro.ObterParametros();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Salvar(ParametroInputDTO dto)
        {                   
            if (ModelState.IsValid)
            {
                _servicoParametro.SalvarConfiguracoes(dto);
                TempData["Sucesso"] = "As configurações foram salvas com sucesso!";
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
