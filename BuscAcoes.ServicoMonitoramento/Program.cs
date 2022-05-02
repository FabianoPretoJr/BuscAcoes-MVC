using BuscAcoes.Data;
using BuscAcoes.Data.Repositorios;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Dominio.Servicos;
using BuscAcoes.ServicoMonitoramento.Interfaces;
using BuscAcoes.ServicoMonitoramento.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BuscAcoes.ServicoMonitoramento
{
    public class Program
    {
        public static IConfigurationRoot configuration;

        static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                                                  .MinimumLevel.Debug()
                                                  .WriteTo.Console()
                                                  .WriteTo.File(LogHelper.ObterArquivoLog(), rollingInterval: RollingInterval.Day)
                                                  .CreateLogger();

            try
            {
                // Start!
                MainAsync(args).Wait();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        static async Task MainAsync(string[] args)
        {
            try
            {
                Log.Information("Criando coleção de serviços");

                ServiceCollection serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                Log.Information("Construindo provedor de serviços");

                IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

                Log.Information("Iniciando serviço...");

                await serviceProvider.GetService<App>().RunAsync();

                Log.Information("Fim do serviço");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw ex;
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging();

            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            services.AddSingleton(configuration);

            services.Configure<EmailSettings>(options => configuration.GetSection("EmailSettings").Bind(options));

            services.AddTransient<IEmailSender, AlertaEmailSender>();

            services.AddDbContext<BuscAcoesDbContext>(ServiceLifetime.Transient);

            services.AddTransient(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            services.AddTransient(typeof(IParametroRepositorio), typeof(ParametroRepositorio));
            services.AddTransient(typeof(IDiasIndisponiveisRepositorio), typeof(DiasIndisponiveisRepositorio));
            services.AddTransient(typeof(IMonitoramentoRepositorio), typeof(MonitoramentoRepositorio));
            services.AddTransient(typeof(IClienteRepositorio), typeof(ClienteRepositorio));
            services.AddTransient(typeof(IClienteAcaoRepositorio), typeof(ClienteAcaoRepositorio));
            services.AddTransient(typeof(IAcaoRepositorio), typeof(AcaoRepositorio));
            services.AddTransient(typeof(IAlertaRepositorio), typeof(AlertaRepositorio));

            services.AddTransient(typeof(IParametroServico), typeof(ParametroServico));
            services.AddTransient(typeof(IDiasIndisponiveisServico), typeof(DiasIndisponiveisServico));
            services.AddTransient(typeof(IMonitoramentoServico), typeof(MonitoramentoServico));
            services.AddTransient(typeof(IClienteAcaoServico), typeof(ClienteAcaoServico));
            services.AddTransient(typeof(IAlertaServico), typeof(AlertaServico));

            services.AddTransient(typeof(IValidacaoMonitoramento), typeof(ValidacaoMonitoramento));
            services.AddTransient(typeof(IGeracaoDeAlerta), typeof(GeracaoDeAlerta));
            services.AddTransient(typeof(IAcaoMonitoramento), typeof(AcaoMonitoramento));

            services.AddSingleton(Log.Logger);

            // Add app
            services.AddTransient<App>();
        }

    }
}

