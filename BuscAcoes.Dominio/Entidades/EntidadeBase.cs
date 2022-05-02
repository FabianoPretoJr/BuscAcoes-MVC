namespace BuscAcoes.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        protected EntidadeBase(int id)
        {
            Id = id;
        }

        protected EntidadeBase()
        {
        }

        public int Id { get; private set; }
    }
}
