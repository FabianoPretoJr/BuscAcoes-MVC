using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Data.Repositorios
{
    public class DiasIndisponiveisRepositorio : RepositorioBase<DiasIndisponiveis>, IDiasIndisponiveisRepositorio
    {
        public DiasIndisponiveisRepositorio(BuscAcoesDbContext context) : base(context)
        {
        }

        public bool VerificarDiaRegistrado(DateTime data)
        {
            return _dbSet.Any(d => d.Data.Date == data.Date);
        }
        public async Task<List<DiasIndisponiveis>> SelecionarDiasIndisponiveisApartirDoAtual()
        {
            return await _dbSet.Where(d => d.Data >= DateTime.Now.Date).ToListAsync();
        }
    }
}
