using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.ServicoMonitoramento.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace BuscAcoes.ServicoMonitoramento
{
    public class App
    {
        private readonly IGeracaoDeAlerta _geracaoDeAlerta;
        private readonly IAcaoMonitoramento _acaoMonitoramento;
        private readonly IParametroServico _parametroServico;

        public App(IGeracaoDeAlerta geracaoDeAlerta, IAcaoMonitoramento acaoMonitoramento, IParametroServico parametroServico)
        {
            _geracaoDeAlerta = geracaoDeAlerta;
            _acaoMonitoramento = acaoMonitoramento;
            _parametroServico = parametroServico;
        }

        public async Task RunAsync()
        {
            var intervaloEmMinutos = _parametroServico.ConsultarIntervaloMonitoramentoEmMinutos();

            Log.Information($"-- Intervalo definido: {intervaloEmMinutos} minuto(s) --");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Pressione a tecla Enter para desligar o programa...\n ");
            Console.ResetColor();

            await _acaoMonitoramento.Monitorar(DateTime.Now);
            await _geracaoDeAlerta.VerificarGeracaoDeAlerta();

            IniciarTimer(intervaloEmMinutos * 60 * 1000);

            Console.ReadLine();
        }

        private void IniciarTimer(int intervalo)
        {
            var aTimer = new Timer
            {
                Interval = intervalo,
                AutoReset = true
            };

            aTimer.Elapsed += OnTimedEvent;

            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _acaoMonitoramento.Monitorar(e.SignalTime).Wait();
            _geracaoDeAlerta.VerificarGeracaoDeAlerta().Wait();
        }  
    }
}