using BuscAcoes.Dominio.DTOs;
using BuscAcoes.Dominio.DTOs.Usuario;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BuscAcoes.Dominio.Excecoes;
using System.Security.Claims;
using BuscAcoes.Dominio.DTOs.Calendario;
using BuscAcoes.Dominio.Enum;
using BuscAcoes.Dominio.DTOs.Cliente;

namespace BuscAcoes.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILoginServico _loginServico;
        private readonly IUsuarioServico _servicoUsuario;
        private readonly IClienteAcaoServico _clienteAcaoServico;
        private readonly IDiasIndisponiveisServico _servicoDiasIndisponiveis;
        private readonly IMonitoramentoServico _servicoMonitoramento;

        public HomeController(ILoginServico loginServico, IUsuarioServico usuarioServico,
            IClienteAcaoServico clienteAcaoServico, IDiasIndisponiveisServico servicoDiasIndisponiveis,
            IMonitoramentoServico servicoMonitoramento)
        {
            _loginServico = loginServico;
            _servicoUsuario = usuarioServico;
            _clienteAcaoServico = clienteAcaoServico;
            _servicoDiasIndisponiveis = servicoDiasIndisponiveis;
            _servicoMonitoramento = servicoMonitoramento;
        }

        #region Sem autenticação

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInputDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                string returnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();
                if (string.IsNullOrEmpty(returnUrl)) returnUrl = "/";


                await _loginServico.Logar(loginDTO, HttpContext);

                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }

        [AllowAnonymous]
        [HttpGet("Cadastro")]
        public IActionResult CadastrarCliente()
        {
            return View(new ClienteCadastroInputDTO());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(ClienteCadastroInputDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return View("CadastrarCliente", dto);

                _servicoUsuario.CadastrarCliente(dto);

                return RedirectToAction("Login");
            }
            catch (EntidadeInvalidaException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("CadastrarCliente", dto);
            }
        }
        public async Task<IActionResult> Deslogar()
        {
            await _loginServico.Deslogar(HttpContext);
            return RedirectToAction("Login");
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            CarregarAlertas();

            bool usuarioValido = _loginServico.VerificarUsuarioValido(HttpContext);
            if (usuarioValido == false) return await Deslogar();

            var perfilUsuarioLogado = Enum.Parse<EPerfil>(User.FindFirst(ClaimTypes.Role)?.Value);

            var partialCalendarioDto = new PartialCalendarioDTO()
            {
                DiasIndisponiveis = await _servicoDiasIndisponiveis.ListarDiasIndisponiveis(),
                Perfil = perfilUsuarioLogado
            };

            var dto = new HomeDTO()
            {
                PartialCalendarioDTO = partialCalendarioDto,
                ExibeCalendario = perfilUsuarioLogado == EPerfil.Cliente
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SalvarPerfil(ClientePerfilInputDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    dto.Usuario = _servicoUsuario.ConsultarUsuarioPorId(dto.IdUsuario);

                    TempData["geralAtivado"] = true;

                    return View("Perfil", dto);
                }

                _servicoUsuario.EditarCliente(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                dto.Usuario = _servicoUsuario.ConsultarUsuarioPorId(dto.IdUsuario);

                TempData["geralAtivado"] = true;

                return View("Perfil", dto);
            }
        }

        [HttpGet("[controller]/Perfil/{id:int}")]
        public IActionResult Perfil(int id)
        {
            ClientePerfilInputDTO dto = _servicoUsuario.ConsultarClientePorUsuarioId(id);

            TempData["geralAtivado"] = false;

            return View("Perfil", dto);
        }

        [HttpGet("[controller]/Grafico")]
        public IActionResult ObterDadosGrafico()
        {
            var Perfil = Enum.Parse<EPerfil>(User.FindFirst(ClaimTypes.Role)?.Value);
            if (Perfil == EPerfil.Cliente)
            {
                int idUsuario = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(_servicoMonitoramento.ObterUltimoMonitoramentoPorCliente(idUsuario));
            }

            return Ok(_servicoMonitoramento.ObterTodosUltimoMonitoramento());
        }

        public IActionResult MeusAlertas()
        {
            string claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUsuarioLogado = Convert.ToInt32(claimId);
            var dto = _clienteAcaoServico.BuscarTodosAlertas(idUsuarioLogado);
            return View(dto);
        }

        [HttpPost]
        public IActionResult VisualizarTodosAlertas()
        {
            string claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUsuarioLogado = Convert.ToInt32(claimId);

            _clienteAcaoServico.VisualizarTodosAlertas(HttpContext, idUsuarioLogado);

            return RedirectToAction("MeusAlertas");
        }

        [HttpPost]
        public IActionResult MarcarComoVisualizado([FromBody] int idAlerta)
        {
            _clienteAcaoServico.AtualizarVisualizacaoAlerta(idAlerta);

            string claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUsuarioLogado = Convert.ToInt32(claimId);

            _clienteAcaoServico.CarregarAlertasNaSessao(HttpContext, idUsuarioLogado);

            return RedirectToAction("MeusAlertas");
        }


        private void CarregarAlertas()
        {
            string claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUsuarioLogado = Convert.ToInt32(claimId);

            _clienteAcaoServico.CarregarAlertasNaSessao(HttpContext, idUsuarioLogado);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
