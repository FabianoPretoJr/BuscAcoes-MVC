using BuscAcoes.Dominio.DTOs;
using BuscAcoes.Dominio.DTOs.Usuario;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Enum;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Dominio.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Servicos
{
    public class LoginServico : ILoginServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<bool> Logar(LoginInputDTO loginDTO, HttpContext httpContext)
        {
            var usuario = _usuarioRepositorio.SelecionarPorUsuarioESenha(loginDTO.Usuario, Criptografia.getMdIHash(loginDTO.Senha));
            if (usuario == null) throw new EntidadeNaoEncontradaException("Usuario e/ou Senha inválidos!");

            return await DefinirCookie(usuario, httpContext);
        }

        public Task Deslogar(HttpContext httpContext)
        {
            return httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool VerificarUsuarioValido(HttpContext httpContext)
        {
            string claimId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUsuarioLogado = Convert.ToInt32(claimId);

            var usuario = _usuarioRepositorio.SelecionarPorId(idUsuarioLogado);

            return usuario != null && usuario.Ativo != false;
        }

        private async Task<bool> DefinirCookie(Usuario usuario, HttpContext httpContext)
        {
            var perfil = (EPerfil)usuario.PerfilId;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, perfil.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var loginClaim = new ClaimsPrincipal(claimsIdentity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, loginClaim);

            return true;
        }

        public async Task DefinirCookie(UsuarioInputDTO dto, HttpContext httpContext)
        {
            string claimId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int idUsuarioLogado = Convert.ToInt32(claimId);

            var usuario = new Usuario(dto.IdUsuario, dto.Nome, dto.Login, dto.Senha, dto.IdPerfil);

            if(dto.IdUsuario == idUsuarioLogado) await DefinirCookie(usuario, httpContext);
        }
    }
}
