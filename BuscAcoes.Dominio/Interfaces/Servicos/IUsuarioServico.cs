using BuscAcoes.Dominio.DTOs.Cliente;
using BuscAcoes.Dominio.DTOs.Usuario;
using BuscAcoes.Dominio.Entidades;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Interfaces.Servicos
{
    public interface IUsuarioServico
    {
        List<UsuarioOutputDTO> ListarUsuarios();
        Usuario Salvar(UsuarioInputDTO dto);
        UsuarioInputDTO ConsultarUsuarioPorId(int id);
        ClientePerfilInputDTO ConsultarClientePorUsuarioId(int usuarioId);
        void DesativarUsuario(int id);
        void AtivarUsuario(int id);

        void EditarCliente(ClientePerfilInputDTO dto);
        void CadastrarCliente(ClienteCadastroInputDTO dto);
        
    }
}
