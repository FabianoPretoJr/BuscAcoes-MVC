using System;

namespace BuscAcoes.Dominio.Excecoes
{
    public class EntidadeNaoEncontradaException : ArgumentException
    {
        public EntidadeNaoEncontradaException(string message) : base(message)
        {
        }

        public EntidadeNaoEncontradaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
