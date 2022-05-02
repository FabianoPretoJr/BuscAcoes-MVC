using System;
using System.Collections.Generic;

namespace BuscAcoes.Dominio.Excecoes
{
    public class EntidadeInvalidaException : ArgumentException
    {
        public IList<string> Errors { get; private set; }
        public string Campo { get; private set; } = "";
        public EntidadeInvalidaException(string message) : base(message)
        {
            this.Errors = new List<string> { message };
        }

        public EntidadeInvalidaException(string message, string campo) : base(message)
        {
            this.Errors = new List<string> { message };
            Campo = campo;
        }

        public EntidadeInvalidaException(string[] errors) : base(string.Join("\n", errors))
        {
            this.Errors = errors;
        }

        public EntidadeInvalidaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
