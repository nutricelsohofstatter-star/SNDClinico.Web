using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SNDClinico.Web.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaCompletaProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracoesProducao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chave = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    AtualizadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesProducao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FichasTecnicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    RendimentoFinalGramas = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Porcoes = table.Column<int>(type: "integer", nullable: false),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false),
                    CriadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichasTecnicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Documento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Contato = table.Column<string>(type: "text", nullable: true),
                    Telefone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Endereco = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoAuditoria",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Entidade = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    EntidadeId = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Acao = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    DadosAnteriores = table.Column<string>(type: "text", nullable: true),
                    DadosNovos = table.Column<string>(type: "text", nullable: true),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: true),
                    UsuarioNome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoAuditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPrecosAlimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    PrecoAnterior = table.Column<decimal>(type: "numeric(12,4)", precision: 12, scale: 4, nullable: false),
                    PrecoNovo = table.Column<decimal>(type: "numeric(12,4)", precision: 12, scale: 4, nullable: false),
                    Unidade = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    VigenciaEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPrecosAlimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoPrecosAlimentos_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventariosEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Responsavel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    FinalizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventariosEstoque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModalidadesCardapio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModalidadesCardapio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientesFicha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FichaTecnicaId = table.Column<int>(type: "integer", nullable: false),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    NomeAlimentoSnapshot = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Unidade = table.Column<string>(type: "text", nullable: false),
                    QuantidadeLiquida = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    QuantidadeBruta = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    FatorCorrecao = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false),
                    PrecoSnapshot = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    UnidadePrecoSnapshot = table.Column<string>(type: "text", nullable: false),
                    CaloriasSnapshot = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    CarboidratosSnapshot = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    ProteinasSnapshot = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    LipidiosSnapshot = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    FibrasSnapshot = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    SodioSnapshot = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    UnidadeNutricional = table.Column<string>(type: "text", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientesFicha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientesFicha_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientesFicha_FichasTecnicas_FichaTecnicaId",
                        column: x => x.FichaTecnicaId,
                        principalTable: "FichasTecnicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recebimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Documento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataRecebimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FornecedorId = table.Column<int>(type: "integer", nullable: true),
                    Responsavel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recebimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recebimentos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContagensInventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventarioEstoqueId = table.Column<int>(type: "integer", nullable: false),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeSistema = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    QuantidadeContada = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Diferenca = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Unidade = table.Column<string>(type: "text", nullable: false),
                    Justificativa = table.Column<string>(type: "text", nullable: true),
                    AjusteAplicado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContagensInventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContagensInventario_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContagensInventario_InventariosEstoque_InventarioEstoqueId",
                        column: x => x.InventarioEstoqueId,
                        principalTable: "InventariosEstoque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cardapios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Escopo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ModalidadeCardapioId = table.Column<int>(type: "integer", nullable: true),
                    CardapioBaseId = table.Column<int>(type: "integer", nullable: true),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CriadoPorUsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardapios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cardapios_Cardapios_CardapioBaseId",
                        column: x => x.CardapioBaseId,
                        principalTable: "Cardapios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cardapios_ModalidadesCardapio_ModalidadeCardapioId",
                        column: x => x.ModalidadeCardapioId,
                        principalTable: "ModalidadesCardapio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensRecebimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecebimentoId = table.Column<int>(type: "integer", nullable: false),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    CodigoLote = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Fabricacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Validade = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuantidadeEntregue = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    QuantidadeAceita = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    QuantidadeRecusada = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Unidade = table.Column<string>(type: "text", nullable: false),
                    CustoUnitario = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensRecebimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensRecebimento_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensRecebimento_Recebimentos_RecebimentoId",
                        column: x => x.RecebimentoId,
                        principalTable: "Recebimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefeicoesCardapio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardapioId = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    QuantidadePrevista = table.Column<int>(type: "integer", nullable: false),
                    Observacoes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefeicoesCardapio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefeicoesCardapio_Cardapios_CardapioId",
                        column: x => x.CardapioId,
                        principalTable: "Cardapios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotesEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    ItemRecebimentoId = table.Column<int>(type: "integer", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Fabricacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Validade = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QuantidadeInicial = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    QuantidadeAtual = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Unidade = table.Column<string>(type: "text", nullable: false),
                    CustoUnitario = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    Local = table.Column<string>(type: "text", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotesEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotesEstoque_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LotesEstoque_ItensRecebimento_ItemRecebimentoId",
                        column: x => x.ItemRecebimentoId,
                        principalTable: "ItensRecebimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreparacoesCardapio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RefeicaoCardapioId = table.Column<int>(type: "integer", nullable: false),
                    FichaTecnicaId = table.Column<int>(type: "integer", nullable: false),
                    NomeSnapshot = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Porcoes = table.Column<int>(type: "integer", nullable: false),
                    PesoPorPorcao = table.Column<decimal>(type: "numeric(12,3)", precision: 12, scale: 3, nullable: false),
                    Herdada = table.Column<bool>(type: "boolean", nullable: false),
                    PreparacaoOrigemId = table.Column<int>(type: "integer", nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparacoesCardapio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreparacoesCardapio_FichasTecnicas_FichaTecnicaId",
                        column: x => x.FichaTecnicaId,
                        principalTable: "FichasTecnicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreparacoesCardapio_PreparacoesCardapio_PreparacaoOrigemId",
                        column: x => x.PreparacaoOrigemId,
                        principalTable: "PreparacoesCardapio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreparacoesCardapio_RefeicoesCardapio_RefeicaoCardapioId",
                        column: x => x.RefeicaoCardapioId,
                        principalTable: "RefeicoesCardapio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AjustesInventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContagemInventarioId = table.Column<int>(type: "integer", nullable: false),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    LoteEstoqueId = table.Column<int>(type: "integer", nullable: true),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Motivo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Responsavel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AjustesInventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AjustesInventario_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AjustesInventario_ContagensInventario_ContagemInventarioId",
                        column: x => x.ContagemInventarioId,
                        principalTable: "ContagensInventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AjustesInventario_LotesEstoque_LoteEstoqueId",
                        column: x => x.LoteEstoqueId,
                        principalTable: "LotesEstoque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    LoteEstoqueId = table.Column<int>(type: "integer", nullable: true),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Unidade = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Sinal = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Responsavel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_LotesEstoque_LoteEstoqueId",
                        column: x => x.LoteEstoqueId,
                        principalTable: "LotesEstoque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerdasEstoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false),
                    LoteEstoqueId = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(14,3)", precision: 14, scale: 3, nullable: false),
                    Unidade = table.Column<string>(type: "text", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Responsavel = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CustoTotal = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerdasEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerdasEstoque_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerdasEstoque_LotesEstoque_LoteEstoqueId",
                        column: x => x.LoteEstoqueId,
                        principalTable: "LotesEstoque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientesDoadosPreparacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PreparacaoCardapioId = table.Column<int>(type: "integer", nullable: false),
                    AlimentoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientesDoadosPreparacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientesDoadosPreparacao_Alimentos_AlimentoId",
                        column: x => x.AlimentoId,
                        principalTable: "Alimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientesDoadosPreparacao_PreparacoesCardapio_Preparacao~",
                        column: x => x.PreparacaoCardapioId,
                        principalTable: "PreparacoesCardapio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AjustesInventario_AlimentoId",
                table: "AjustesInventario",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesInventario_ContagemInventarioId",
                table: "AjustesInventario",
                column: "ContagemInventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AjustesInventario_LoteEstoqueId",
                table: "AjustesInventario",
                column: "LoteEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Cardapios_CardapioBaseId",
                table: "Cardapios",
                column: "CardapioBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Cardapios_DataInicio_DataFim_Escopo",
                table: "Cardapios",
                columns: new[] { "DataInicio", "DataFim", "Escopo" });

            migrationBuilder.CreateIndex(
                name: "IX_Cardapios_ModalidadeCardapioId",
                table: "Cardapios",
                column: "ModalidadeCardapioId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesProducao_Chave",
                table: "ConfiguracoesProducao",
                column: "Chave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContagensInventario_AlimentoId",
                table: "ContagensInventario",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContagensInventario_InventarioEstoqueId_AlimentoId",
                table: "ContagensInventario",
                columns: new[] { "InventarioEstoqueId", "AlimentoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FichasTecnicas_Nome",
                table: "FichasTecnicas",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_Documento",
                table: "Fornecedores",
                column: "Documento");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAuditoria_Entidade_EntidadeId_DataHora",
                table: "HistoricoAuditoria",
                columns: new[] { "Entidade", "EntidadeId", "DataHora" });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoPrecosAlimentos_AlimentoId_VigenciaEm",
                table: "HistoricoPrecosAlimentos",
                columns: new[] { "AlimentoId", "VigenciaEm" });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientesDoadosPreparacao_AlimentoId",
                table: "IngredientesDoadosPreparacao",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientesDoadosPreparacao_PreparacaoCardapioId_AlimentoId",
                table: "IngredientesDoadosPreparacao",
                columns: new[] { "PreparacaoCardapioId", "AlimentoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientesFicha_AlimentoId",
                table: "IngredientesFicha",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientesFicha_FichaTecnicaId_Ordem",
                table: "IngredientesFicha",
                columns: new[] { "FichaTecnicaId", "Ordem" });

            migrationBuilder.CreateIndex(
                name: "IX_ItensRecebimento_AlimentoId",
                table: "ItensRecebimento",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensRecebimento_RecebimentoId",
                table: "ItensRecebimento",
                column: "RecebimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_LotesEstoque_AlimentoId_Codigo_Validade",
                table: "LotesEstoque",
                columns: new[] { "AlimentoId", "Codigo", "Validade" });

            migrationBuilder.CreateIndex(
                name: "IX_LotesEstoque_ItemRecebimentoId",
                table: "LotesEstoque",
                column: "ItemRecebimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_AlimentoId",
                table: "MovimentacoesEstoque",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_Data",
                table: "MovimentacoesEstoque",
                column: "Data");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_LoteEstoqueId",
                table: "MovimentacoesEstoque",
                column: "LoteEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PerdasEstoque_AlimentoId",
                table: "PerdasEstoque",
                column: "AlimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PerdasEstoque_LoteEstoqueId",
                table: "PerdasEstoque",
                column: "LoteEstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparacoesCardapio_FichaTecnicaId",
                table: "PreparacoesCardapio",
                column: "FichaTecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparacoesCardapio_PreparacaoOrigemId",
                table: "PreparacoesCardapio",
                column: "PreparacaoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparacoesCardapio_RefeicaoCardapioId_Ordem",
                table: "PreparacoesCardapio",
                columns: new[] { "RefeicaoCardapioId", "Ordem" });

            migrationBuilder.CreateIndex(
                name: "IX_Recebimentos_Codigo",
                table: "Recebimentos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recebimentos_FornecedorId",
                table: "Recebimentos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefeicoesCardapio_CardapioId_Data_Ordem",
                table: "RefeicoesCardapio",
                columns: new[] { "CardapioId", "Data", "Ordem" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AjustesInventario");

            migrationBuilder.DropTable(
                name: "ConfiguracoesProducao");

            migrationBuilder.DropTable(
                name: "HistoricoAuditoria");

            migrationBuilder.DropTable(
                name: "HistoricoPrecosAlimentos");

            migrationBuilder.DropTable(
                name: "IngredientesDoadosPreparacao");

            migrationBuilder.DropTable(
                name: "IngredientesFicha");

            migrationBuilder.DropTable(
                name: "MovimentacoesEstoque");

            migrationBuilder.DropTable(
                name: "PerdasEstoque");

            migrationBuilder.DropTable(
                name: "ContagensInventario");

            migrationBuilder.DropTable(
                name: "PreparacoesCardapio");

            migrationBuilder.DropTable(
                name: "LotesEstoque");

            migrationBuilder.DropTable(
                name: "InventariosEstoque");

            migrationBuilder.DropTable(
                name: "FichasTecnicas");

            migrationBuilder.DropTable(
                name: "RefeicoesCardapio");

            migrationBuilder.DropTable(
                name: "ItensRecebimento");

            migrationBuilder.DropTable(
                name: "Cardapios");

            migrationBuilder.DropTable(
                name: "Recebimentos");

            migrationBuilder.DropTable(
                name: "ModalidadesCardapio");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
