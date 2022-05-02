using BuscAcoes.Dominio.Entidades;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuscAcoes.Data.Repositorios
{
    public class ParametroRepositorio : IParametroRepositorio
    {
        private readonly BuscAcoesDbContext _buscAcoesDbContext;
        public ParametroRepositorio(BuscAcoesDbContext context)
        {
            _buscAcoesDbContext = context;
        }

        public void AtualizarParametro(Parametro parametro)
        {
            _buscAcoesDbContext.Update(parametro);
            _buscAcoesDbContext.SaveChanges();
        }

        public async Task<List<Parametro>> SelecionarHorarios()
        {
            return await _buscAcoesDbContext.Parametros
                                                        .Where(p => p.Nome == "HorarioAbertura" || p.Nome == "HorarioFechamento")
                                                        .AsNoTracking()
                                                        .ToListAsync();
        }

        public Parametro SelecionarIntervaloMonitoramento()
        {
            return _buscAcoesDbContext.Parametros.FirstOrDefault(p => p.Nome == "MonitoramentoMinutos");
        }

        public async Task<string> ObterLinkEndpointApi()
        {
            var link = await _buscAcoesDbContext.Parametros
                                                        .AsNoTracking()
                                                        .FirstOrDefaultAsync(p => p.Nome == "EndpointApi");
            return link.Valor;
        }

        public async Task<string> ObterChaveApi()
        {
            var chave = await _buscAcoesDbContext.Parametros
                                                        .AsNoTracking()
                                                        .FirstOrDefaultAsync(p => p.Nome == "ChaveApi1");
            return chave.Valor;
        }
    }
}
