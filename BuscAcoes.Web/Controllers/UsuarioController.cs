using BuscAcoes.Dominio.DTOs.Usuario;
using BuscAcoes.Dominio.Enum;
using BuscAcoes.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuscAcoes.Web.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServico _servicoUsuario;
        private readonly ILoginServico _loginServico;

        public UsuarioController(IUsuarioServico servicoUsuario, ILoginServico loginServico)
        {
            _servicoUsuario = servicoUsuario;
            _loginServico = loginServico;
        }

        [Authorize(policy: "Admin")]
        public IActionResult Index()
        {
            var usuarios = _servicoUsuario.ListarUsuarios();
            return View(usuarios);
        }

        [HttpGet("Cadastrar")]
        [Authorize(policy: "Admin")]
        public IActionResult Cadastrar()
        {
            TempData["ReturnUrl"] = Url.Action(action: "Index", controller: "Usuario");

            var dto = new UsuarioInputDTO() { IdPerfil = (int)EPerfil.Operador };
            return View("Usuario", dto);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salvar(UsuarioInputDTO dto, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                    return RetornarErroParaView(dto);

                _servicoUsuario.Salvar(dto);

                await _loginServico.DefinirCookie(dto, HttpContext);

                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return RetornarErroParaView(dto);
            }
        }

        private IActionResult RetornarErroParaView(UsuarioInputDTO dto)
        {
            var Perfil = Enum.Parse<EPerfil>(User.FindFirst(ClaimTypes.Role)?.Value);

            if (Perfil != EPerfil.Cliente)
                return View("Usuario", dto);

            var dtoClientePerfil = _servicoUsuario.ConsultarClientePorUsuarioId(dto.IdUsuario);
            dtoClientePerfil.Usuario = dto;
            TempData["geralAtivado"] = false;

            return View("../Home/Perfil", dtoClientePerfil);
        }

        [HttpGet("Perfil/{id:int}")]
        [Authorize(policy: "Operador")]
        public IActionResult Perfil(int id)
        {
            UsuarioInputDTO dto = _servicoUsuario.ConsultarUsuarioPorId(id);

            TempData["PaginaPerfil"] = true;

            return View("Usuario", dto);
        }

        [HttpPost("Editar/{id:int}")]
        [Authorize(policy: "Admin")]
        public IActionResult Editar(int id)
        {
            UsuarioInputDTO dto = _servicoUsuario.ConsultarUsuarioPorId(id);

            TempData["ReturnUrl"] = Url.Action(action: "Index", controller: "Usuario");

            return View("Usuario", dto);
        }

        [HttpPost("Desativar/{id:int}")]
        [Authorize(policy: "Admin")]
        public IActionResult Desativar(int id)
        {
            _servicoUsuario.DesativarUsuario(id);

            return RedirectToAction("Index");
        }

        [HttpPost("Ativar/{id:int}")]
        [Authorize(policy: "Admin")]
        public IActionResult Ativar(int id)
        {
            _servicoUsuario.AtivarUsuario(id);

            return RedirectToAction("Index");
        }
    }
}
