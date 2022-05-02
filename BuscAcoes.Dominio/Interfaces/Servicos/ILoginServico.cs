using BuscAcoes.Dominio.DTOs;
using BuscAcoes.Dominio.DTOs.Usuario;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface ILoginServico
    {
        Task<bool> Logar(LoginInputDTO loginDTO, HttpContext httpContext);
        Task DefinirCookie(UsuarioInputDTO dto, HttpContext httpContext);
        Task Deslogar(HttpContext httpContext);
        bool VerificarUsuarioValido(HttpContext httpContext);
    }
}
