using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SNDClinico.Web.Data;

public class SndDbContext
    : IdentityDbContext<SndIdentityUser>, IDataProtectionKeyContext
{
    public SndDbContext(DbContextOptions<SndDbContext> options)
        : base(options)
    {
    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = default!;

    // Cadastros
    public DbSet<Alimento> Alimentos => Set<Alimento>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();
    public DbSet<HistoricoPrecoAlimento> HistoricoPrecosAlimentos => Set<HistoricoPrecoAlimento>();

    // Fichas técnicas
    public DbSet<FichaTecnica> FichasTecnicas => Set<FichaTecnica>();
    public DbSet<IngredienteFicha> IngredientesFicha => Set<IngredienteFicha>();

    // Planejamento de cardápios
    public DbSet<ModalidadeCardapio> ModalidadesCardapio => Set<ModalidadeCardapio>();
    public DbSet<Cardapio> Cardapios => Set<Cardapio>();
    public DbSet<RefeicaoCardapio> RefeicoesCardapio => Set<RefeicaoCardapio>();
    public DbSet<PreparacaoCardapio> PreparacoesCardapio => Set<PreparacaoCardapio>();
    public DbSet<IngredienteDoadoPreparacao> IngredientesDoadosPreparacao => Set<IngredienteDoadoPreparacao>();

    // Estoque
    public DbSet<Recebimento> Recebimentos => Set<Recebimento>();
    public DbSet<ItemRecebimento> ItensRecebimento => Set<ItemRecebimento>();
    public DbSet<LoteEstoque> LotesEstoque => Set<LoteEstoque>();
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque => Set<MovimentacaoEstoque>();
    public DbSet<PerdaEstoque> PerdasEstoque => Set<PerdaEstoque>();

    // Inventário
    public DbSet<InventarioEstoque> InventariosEstoque => Set<InventarioEstoque>();
    public DbSet<ContagemInventario> ContagensInventario => Set<ContagemInventario>();
    public DbSet<AjusteInventario> AjustesInventario => Set<AjusteInventario>();

    // Gestão
    public DbSet<ConfiguracaoProducao> ConfiguracoesProducao => Set<ConfiguracaoProducao>();
    public DbSet<HistoricoAuditoria> HistoricoAuditoria => Set<HistoricoAuditoria>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SndIdentityUser>(entity =>
        {
            entity.Property(u => u.NomeCompleto).HasMaxLength(150);
        });

        builder.Entity<Alimento>(entity =>
        {
            entity.ToTable("Alimentos");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Nome).HasMaxLength(200).IsRequired();
            entity.Property(a => a.Unidade).HasMaxLength(20).IsRequired();
            entity.Property(a => a.Categoria).HasMaxLength(100).IsRequired();
            entity.Property(a => a.Preco).HasPrecision(12, 4);
            entity.Property(a => a.Calorias).HasPrecision(12, 3);
            entity.Property(a => a.Carboidratos).HasPrecision(12, 3);
            entity.Property(a => a.Proteinas).HasPrecision(12, 3);
            entity.Property(a => a.Lipidios).HasPrecision(12, 3);
            entity.Property(a => a.Fibras).HasPrecision(12, 3);
            entity.Property(a => a.Sodio).HasPrecision(12, 3);
            entity.Property(a => a.FatorCorrecao).HasPrecision(8, 4);
            entity.HasIndex(a => a.Nome);
        });

        builder.Entity<HistoricoPrecoAlimento>(entity =>
        {
            entity.ToTable("HistoricoPrecosAlimentos");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.PrecoAnterior).HasPrecision(12, 4);
            entity.Property(x => x.PrecoNovo).HasPrecision(12, 4);
            entity.Property(x => x.Unidade).HasMaxLength(20).IsRequired();
            entity.HasOne(x => x.Alimento).WithMany().HasForeignKey(x => x.AlimentoId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.AlimentoId, x.VigenciaEm });
        });

        builder.Entity<Fornecedor>(entity =>
        {
            entity.ToTable("Fornecedores");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Documento).HasMaxLength(30);
            entity.HasIndex(x => x.Documento);
        });

        builder.Entity<FichaTecnica>(entity =>
        {
            entity.ToTable("FichasTecnicas");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Categoria).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Tipo).HasMaxLength(100).IsRequired();
            entity.Property(x => x.RendimentoFinalGramas).HasPrecision(14, 3);
            entity.HasIndex(x => x.Nome);
        });

        builder.Entity<IngredienteFicha>(entity =>
        {
            entity.ToTable("IngredientesFicha");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.NomeAlimentoSnapshot).HasMaxLength(200).IsRequired();
            entity.Property(x => x.QuantidadeLiquida).HasPrecision(14, 3);
            entity.Property(x => x.QuantidadeBruta).HasPrecision(14, 3);
            entity.Property(x => x.FatorCorrecao).HasPrecision(10, 4);
            entity.Property(x => x.PrecoSnapshot).HasPrecision(14, 4);
            entity.Property(x => x.CaloriasSnapshot).HasPrecision(14, 3);
            entity.Property(x => x.CarboidratosSnapshot).HasPrecision(14, 3);
            entity.Property(x => x.ProteinasSnapshot).HasPrecision(14, 3);
            entity.Property(x => x.LipidiosSnapshot).HasPrecision(14, 3);
            entity.Property(x => x.FibrasSnapshot).HasPrecision(14, 3);
            entity.Property(x => x.SodioSnapshot).HasPrecision(14, 3);
            entity.HasOne(x => x.FichaTecnica).WithMany(x => x.Ingredientes)
                .HasForeignKey(x => x.FichaTecnicaId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.FichaTecnicaId, x.Ordem });
        });

        builder.Entity<ModalidadeCardapio>(entity =>
        {
            entity.ToTable("ModalidadesCardapio");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).HasMaxLength(120).IsRequired();
        });

        builder.Entity<Cardapio>(entity =>
        {
            entity.ToTable("Cardapios");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Escopo).HasMaxLength(30).IsRequired();
            entity.HasOne(x => x.ModalidadeCardapio).WithMany(x => x.Cardapios)
                .HasForeignKey(x => x.ModalidadeCardapioId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.CardapioBase).WithMany(x => x.CardapiosDerivados)
                .HasForeignKey(x => x.CardapioBaseId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.DataInicio, x.DataFim, x.Escopo });
        });

        builder.Entity<RefeicaoCardapio>(entity =>
        {
            entity.ToTable("RefeicoesCardapio");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nome).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Observacoes).HasMaxLength(1000);
            entity.HasOne(x => x.Cardapio).WithMany(x => x.Refeicoes)
                .HasForeignKey(x => x.CardapioId).OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(x => new { x.CardapioId, x.Data, x.Ordem });
        });

        builder.Entity<PreparacaoCardapio>(entity =>
        {
            entity.ToTable("PreparacoesCardapio");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.NomeSnapshot).HasMaxLength(200).IsRequired();
            entity.Property(x => x.PesoPorPorcao).HasPrecision(12, 3);
            entity.HasOne(x => x.RefeicaoCardapio).WithMany(x => x.Preparacoes)
                .HasForeignKey(x => x.RefeicaoCardapioId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.FichaTecnica).WithMany()
                .HasForeignKey(x => x.FichaTecnicaId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.PreparacaoOrigem).WithMany(x => x.Variacoes)
                .HasForeignKey(x => x.PreparacaoOrigemId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.RefeicaoCardapioId, x.Ordem });
        });

        builder.Entity<IngredienteDoadoPreparacao>(entity =>
        {
            entity.ToTable("IngredientesDoadosPreparacao");
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.PreparacaoCardapio).WithMany(x => x.IngredientesDoados)
                .HasForeignKey(x => x.PreparacaoCardapioId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.PreparacaoCardapioId, x.AlimentoId }).IsUnique();
        });

        builder.Entity<Recebimento>(entity =>
        {
            entity.ToTable("Recebimentos");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Codigo).HasMaxLength(40).IsRequired();
            entity.Property(x => x.Documento).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Responsavel).HasMaxLength(150).IsRequired();
            entity.HasOne(x => x.Fornecedor).WithMany(x => x.Recebimentos)
                .HasForeignKey(x => x.FornecedorId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => x.Codigo).IsUnique();
        });

        builder.Entity<ItemRecebimento>(entity =>
        {
            entity.ToTable("ItensRecebimento");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.CodigoLote).HasMaxLength(100).IsRequired();
            entity.Property(x => x.QuantidadeEntregue).HasPrecision(14, 3);
            entity.Property(x => x.QuantidadeAceita).HasPrecision(14, 3);
            entity.Property(x => x.QuantidadeRecusada).HasPrecision(14, 3);
            entity.Property(x => x.CustoUnitario).HasPrecision(14, 4);
            entity.HasOne(x => x.Recebimento).WithMany(x => x.Itens)
                .HasForeignKey(x => x.RecebimentoId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<LoteEstoque>(entity =>
        {
            entity.ToTable("LotesEstoque");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Codigo).HasMaxLength(100).IsRequired();
            entity.Property(x => x.QuantidadeInicial).HasPrecision(14, 3);
            entity.Property(x => x.QuantidadeAtual).HasPrecision(14, 3);
            entity.Property(x => x.CustoUnitario).HasPrecision(14, 4);
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.ItemRecebimento).WithMany(x => x.LotesGerados)
                .HasForeignKey(x => x.ItemRecebimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.AlimentoId, x.Codigo, x.Validade });
        });

        builder.Entity<MovimentacaoEstoque>(entity =>
        {
            entity.ToTable("MovimentacoesEstoque");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Tipo).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Quantidade).HasPrecision(14, 3);
            entity.Property(x => x.Unidade).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Sinal).HasMaxLength(3).IsRequired();
            entity.Property(x => x.Responsavel).HasMaxLength(150).IsRequired();
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.LoteEstoque).WithMany(x => x.Movimentacoes)
                .HasForeignKey(x => x.LoteEstoqueId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => x.Data);
        });

        builder.Entity<PerdaEstoque>(entity =>
        {
            entity.ToTable("PerdasEstoque");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Quantidade).HasPrecision(14, 3);
            entity.Property(x => x.CustoTotal).HasPrecision(14, 4);
            entity.Property(x => x.Motivo).HasMaxLength(120).IsRequired();
            entity.Property(x => x.Responsavel).HasMaxLength(150).IsRequired();
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.LoteEstoque).WithMany(x => x.Perdas)
                .HasForeignKey(x => x.LoteEstoqueId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<InventarioEstoque>(entity =>
        {
            entity.ToTable("InventariosEstoque");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Tipo).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Status).HasMaxLength(40).IsRequired();
            entity.Property(x => x.Responsavel).HasMaxLength(150).IsRequired();
        });

        builder.Entity<ContagemInventario>(entity =>
        {
            entity.ToTable("ContagensInventario");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.QuantidadeSistema).HasPrecision(14, 3);
            entity.Property(x => x.QuantidadeContada).HasPrecision(14, 3);
            entity.Property(x => x.Diferenca).HasPrecision(14, 3);
            entity.HasOne(x => x.InventarioEstoque).WithMany(x => x.Contagens)
                .HasForeignKey(x => x.InventarioEstoqueId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => new { x.InventarioEstoqueId, x.AlimentoId }).IsUnique();
        });

        builder.Entity<AjusteInventario>(entity =>
        {
            entity.ToTable("AjustesInventario");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Quantidade).HasPrecision(14, 3);
            entity.Property(x => x.Motivo).HasMaxLength(500).IsRequired();
            entity.Property(x => x.Responsavel).HasMaxLength(150).IsRequired();
            entity.HasOne(x => x.ContagemInventario).WithMany()
                .HasForeignKey(x => x.ContagemInventarioId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Alimento).WithMany()
                .HasForeignKey(x => x.AlimentoId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.LoteEstoque).WithMany()
                .HasForeignKey(x => x.LoteEstoqueId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ConfiguracaoProducao>(entity =>
        {
            entity.ToTable("ConfiguracoesProducao");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Chave).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Valor).HasMaxLength(2000).IsRequired();
            entity.HasIndex(x => x.Chave).IsUnique();
        });

        builder.Entity<HistoricoAuditoria>(entity =>
        {
            entity.ToTable("HistoricoAuditoria");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Entidade).HasMaxLength(120).IsRequired();
            entity.Property(x => x.EntidadeId).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Acao).HasMaxLength(40).IsRequired();
            entity.Property(x => x.UsuarioNome).HasMaxLength(150);
            entity.HasIndex(x => new { x.Entidade, x.EntidadeId, x.DataHora });
        });
    }
}
