using BuscAcoes.Data;
using BuscAcoes.Data.Repositorios;
using BuscAcoes.Dominio.Enum;
using BuscAcoes.Dominio.Interfaces.Repositorios;
using BuscAcoes.Dominio.Interfaces.Servicos;
using BuscAcoes.Dominio.Servicos;
using BuscAcoes.Dominio.Validacoes.Empresas;
using BuscAcoes.Dominio.Validacoes.Setores;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;

namespace BuscAcoes.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMvc()
                .AddFluentValidation(fv => {
                    fv.RegisterValidatorsFromAssemblyContaining<SetorValidacao>();
                    fv.RegisterValidatorsFromAssemblyContaining<EmpresaValidacao>();
                });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            #region Autenticacao com Cookie

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Cookies";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });

            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, EPerfil.Administrador.ToString()));

                    options.AddPolicy("Operador", policy => policy.RequireClaim(ClaimTypes.Role, EPerfil.Operador.ToString(),
                                                                                                 EPerfil.Administrador.ToString()));

                    options.AddPolicy("Cliente", policy => policy.RequireClaim(ClaimTypes.Role, EPerfil.Cliente.ToString(),
                                                                                                 EPerfil.Administrador.ToString()));
                })
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                 options =>
                 {
                     options.LoginPath = new PathString("/Home/Login");
                     options.AccessDeniedPath = new PathString("/Home");
                 });

            #endregion

            #region Configuracao Culture Info
            services.AddMvc(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa se preeenchido.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisições não esteja valido");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numerico.");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O campo deve ser numerico.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor preenchido é invalido para este campo.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numerico.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este capo precisa ser preenchido.");
            });
            #endregion


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<BuscAcoesDbContext>();

            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            services.AddScoped(typeof(IAcaoRepositorio), typeof(AcaoRepositorio));
            services.AddScoped(typeof(IEmpresaRepositorio), typeof(EmpresaRepositorio));
            services.AddScoped(typeof(ISetorRepositorio), typeof(SetorRepositorio));
            services.AddScoped(typeof(IUsuarioRepositorio), typeof(UsuarioRepositorio));
            services.AddScoped(typeof(IParametroRepositorio), typeof(ParametroRepositorio));
            services.AddScoped(typeof(IDiasIndisponiveisRepositorio), typeof(DiasIndisponiveisRepositorio));
            services.AddScoped(typeof(IMonitoramentoRepositorio), typeof(MonitoramentoRepositorio));
            services.AddScoped(typeof(IClienteAcaoRepositorio), typeof(ClienteAcaoRepositorio));
            services.AddScoped(typeof(IClienteRepositorio), typeof(ClienteRepositorio));
            services.AddScoped(typeof(IAlertaRepositorio), typeof(AlertaRepositorio));

            services.AddTransient(typeof(IAcaoServico), typeof(AcaoServico));
            services.AddTransient(typeof(IUsuarioServico), typeof(UsuarioServico));
            services.AddTransient(typeof(ILoginServico), typeof(LoginServico));
            services.AddTransient(typeof(IParametroServico), typeof(ParametroServico));
            services.AddTransient(typeof(IDiasIndisponiveisServico), typeof(DiasIndisponiveisServico));
            services.AddTransient(typeof(IMonitoramentoServico), typeof(MonitoramentoServico));
            services.AddTransient(typeof(IClienteAcaoServico), typeof(ClienteAcaoServico));
            services.AddTransient(typeof(ISetorServico), typeof(SetorServico));
            services.AddTransient(typeof(IEmpresaServico), typeof(EmpresaServico));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var defalCulture = new CultureInfo("pt-BR");
            var localizationOpitions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defalCulture),
                SupportedCultures = new List<CultureInfo> { defalCulture },
                SupportedUICultures = new List<CultureInfo> { defalCulture }
            };

            app.UseRequestLocalization(localizationOpitions);

            app.UseRouting();

            app.UseSession();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
