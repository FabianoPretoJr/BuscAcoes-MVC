using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BuscAcoes.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiasIndisponiveis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Feriado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiasIndisponiveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    Nome = table.Column<string>(nullable: false),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.Nome);
                });

            migrationBuilder.CreateTable(
                name: "Perfils",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    PerfilId = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfils_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    SetorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Documento = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DataNasc = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoNegociacao = table.Column<string>(nullable: true),
                    EmpresaId = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acoes_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(nullable: false),
                    AcaoId = table.Column<int>(nullable: false),
                    ToleranciaMaxima = table.Column<double>(nullable: false),
                    ToleranciaMinima = table.Column<double>(nullable: false),
                    Visualizado = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Acoes_AcaoId",
                        column: x => x.AcaoId,
                        principalTable: "Acoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alertas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteAcoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(nullable: false),
                    AcaoId = table.Column<int>(nullable: false),
                    ToleranciaMaxima = table.Column<double>(nullable: false),
                    ToleranciaMinima = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteAcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteAcoes_Acoes_AcaoId",
                        column: x => x.AcaoId,
                        principalTable: "Acoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteAcoes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monitoramentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecoAcao = table.Column<double>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    PercentualVariacao = table.Column<double>(nullable: false),
                    AcaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitoramentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monitoramentos_Acoes_AcaoId",
                        column: x => x.AcaoId,
                        principalTable: "Acoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acoes_EmpresaId",
                table: "Acoes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_AcaoId",
                table: "Alertas",
                column: "AcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_ClienteId",
                table: "Alertas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteAcoes_AcaoId",
                table: "ClienteAcoes",
                column: "AcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteAcoes_ClienteId",
                table: "ClienteAcoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_SetorId",
                table: "Empresas",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Monitoramentos_AcaoId",
                table: "Monitoramentos",
                column: "AcaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PerfilId",
                table: "Usuarios",
                column: "PerfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "ClienteAcoes");

            migrationBuilder.DropTable(
                name: "DiasIndisponiveis");

            migrationBuilder.DropTable(
                name: "Monitoramentos");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Acoes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Perfils");

            migrationBuilder.DropTable(
                name: "Setores");
        }
    }
}
