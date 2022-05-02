using System;

namespace BuscAcoes.Dominio.Entidades
{
    public class DiasIndisponiveis : EntidadeBase
    {
        public DiasIndisponiveis(int id, DateTime data, string descricao)
            : base(id)
        {
            Data = data;
            Descricao = descricao;
        }
        private DiasIndisponiveis() : base() { }

        public DateTime Data { get; set; }
        public string Descricao { get; private set; }
    }
}
