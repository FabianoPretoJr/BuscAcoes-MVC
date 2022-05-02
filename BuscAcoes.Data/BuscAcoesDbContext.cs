using BuscAcoes.Data.Seeds;
using BuscAcoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscAcoes.Data
{
    public class BuscAcoesDbContext : DbContext
    {
        public BuscAcoesDbContext()
        {
        }

        public BuscAcoesDbContext(DbContextOptions<BuscAcoesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Acao> Acoes { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteAcao> ClienteAcoes { get; set; }
        public DbSet<DiasIndisponiveis> DiasIndisponiveis { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Monitoramento> Monitoramentos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Perfil> Perfils { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=GFT-ZAFFLGAYFYP;Database=BuscAcoes;Trusted_Connection=True;"); //Mudar Aqui .\SQLEXPRESS
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parametro>(entity =>
            {
                entity.HasKey(x => x.Nome);
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Login).IsUnique();
            });
            modelBuilder.Entity<Acao>(entity =>
            {
                entity.HasIndex(a => a.CodigoNegociacao).IsUnique();
            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(c => c.Documento).IsUnique();
                entity.HasIndex(c => c.Email).IsUnique();
            });
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasIndex(e => e.Cnpj).IsUnique();
            });
            modelBuilder.Entity<DiasIndisponiveis>(entity =>
            {
                entity.HasIndex(di => di.Data).IsUnique();
            });
            modelBuilder.Entity<Setor>(entity =>
            {
                entity.HasIndex(s => s.Nome).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
            PerfilSeeds.Perfils(modelBuilder);
            UsuarioSeeds.Usuarios(modelBuilder);
            ClienteSeeds.Clientes(modelBuilder);
            ParametroSeeds.Parametros(modelBuilder);
            SetorSeeds.Setores(modelBuilder);
            EmpresaSeeds.Empresas(modelBuilder);
            AcaoSeeds.Acoes(modelBuilder);
        }
        
    }
}
