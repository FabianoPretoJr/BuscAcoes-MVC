namespace BuscAcoes.Dominio.Entidades
{
    public class Parametro
    {
        public Parametro(string nome, string valor)
        {
            Nome = nome;
            Valor = valor;
        }

        private Parametro() { }

        public string Nome { get; private set; }
        public string Valor { get; private set; }
    }
}
