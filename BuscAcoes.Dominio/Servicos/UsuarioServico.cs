using BuscAcoes.Dominio.DTOs.Cliente;
using BuscAcoes.Dominio.DTOs.Usuario;
using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Enum;
using BuscAcoes.Dominio.Excecoes;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Dominio.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscAcoes.Dominio.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, IClienteRepositorio clienteRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }

        public List<UsuarioOutputDTO> ListarUsuarios()
        {
            var usuarios = _usuarioRepositorio.SelecionarTodos().Where(u => u.PerfilId != (int)EPerfil.Administrador);

            var dtos = usuarios.Select(u =>
            {
                return new UsuarioOutputDTO()
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Login = u.Login,
                    DataCriacao = u.DataCriacao,
                    Perfil = (EPerfil)u.PerfilId,
                    Ativo = u.Ativo
                };
            }
            ).ToList();

            return dtos;
        }

        public Usuario Salvar(UsuarioInputDTO dto)
        {
            if (dto.ParaEdicao)
            {
                Usuario usuarioNoBanco = _usuarioRepositorio.SelecionarPorId(dto.IdUsuario);

                if (string.IsNullOrEmpty(dto.Senha))
                    dto.Senha = usuarioNoBanco.Senha;
                else
                    dto.Senha = Criptografia.getMdIHash(dto.Senha);

                var usuarioParaAtualizar = new Usuario(usuarioNoBanco.Id, dto.Nome, dto.Login, dto.Senha, dto.IdPerfil);
                _usuarioRepositorio.Atualizar(usuarioParaAtualizar);

                return usuarioParaAtualizar;
            }

            var usuarioParaInserir = new Usuario(dto.IdUsuario, dto.Nome, dto.Login, Criptografia.getMdIHash(dto.Senha), dto.IdPerfil);
            _usuarioRepositorio.Inserir(usuarioParaInserir);

            return usuarioParaInserir;

        }

        public UsuarioInputDTO ConsultarUsuarioPorId(int id)
        {
            var usuario = _usuarioRepositorio.SelecionarPorId(id);
            if (usuario == null) throw new EntidadeNaoEncontradaException("Usuário não encontrado.");

            var dto = new UsuarioInputDTO()
            {
                IdUsuario = usuario.Id,
                Nome = usuario.Nome,
                Login = usuario.Login,
                Senha = usuario.Senha,
                DataCriacao = usuario.DataCriacao,
                IdPerfil = usuario.PerfilId,
                Ativo = usuario.Ativo
            };

            return dto;
        }

        public ClientePerfilInputDTO ConsultarClientePorUsuarioId(int usuarioId)
        {
            var cliente = _clienteRepositorio.SelecionarPorIdUsuario(usuarioId);
            var usuario = ConsultarUsuarioPorId(cliente.UsuarioId);

            return new ClientePerfilInputDTO()
            {
                Usuario = usuario,
                DataNasc = cliente.DataNasc,
                Documento = cliente.Documento,
                Email = cliente.Email,
                IdCliente = cliente.Id,
                IdUsuario = cliente.UsuarioId
            };
        }

        public void DesativarUsuario(int id)
        {
            try
            {
                var usuario = _usuarioRepositorio.SelecionarPorId(id, asNoTracking: false);
                if (usuario == null) throw new EntidadeNaoEncontradaException("Usuário não encontrado.");

                usuario.Desativar();
                _usuarioRepositorio.Atualizar(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarCliente(ClientePerfilInputDTO dto)
        {
            Cliente clienteNoBanco = _clienteRepositorio.SelecionarPorId(dto.IdCliente);

            #region Validação
            if (clienteNoBanco == null) throw new EntidadeNaoEncontradaException("Cliente não encontrado.");
            #endregion

            var clienteParaAtualizar = new Cliente(dto.IdCliente, dto.Email, dto.Documento, dto.DataNasc, dto.IdUsuario);
            _clienteRepositorio.Atualizar(clienteParaAtualizar);

        }

        public void CadastrarCliente(ClienteCadastroInputDTO dto)
        {
            var usuarioSalvo = this.Salvar(new UsuarioInputDTO()
            {
                IdUsuario = dto.Id,
                Login = dto.Login,
                Nome = dto.Nome,
                Senha = dto.Senha,
                IdPerfil = (int)EPerfil.Cliente
            });

            var clienteParaInserir = new Cliente(dto.Id, dto.Email, dto.Documento, dto.DataNasc, usuarioSalvo.Id);
            _clienteRepositorio.Inserir(clienteParaInserir);
        }

        public void AtivarUsuario(int id)
        {
            try
            {
                var usuario = _usuarioRepositorio.SelecionarPorId(id, asNoTracking: false);
                if (usuario == null)
                    throw new EntidadeNaoEncontradaException("Usuário não encontrado.");

                usuario.Ativar();
                _usuarioRepositorio.Atualizar(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
