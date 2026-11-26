namespace SNDClinico.Web.Data;

public class LoteEstoque
{
    public int Id { get; set; }
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public int? ItemRecebimentoId { get; set; }
    public ItemRecebimento? ItemRecebimento { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public DateTime? Fabricacao { get; set; }
    public DateTime Validade { get; set; }
    public decimal QuantidadeInicial { get; set; }
    public decimal QuantidadeAtual { get; set; }
    public string Unidade { get; set; } = "kg";
    public decimal CustoUnitario { get; set; }
    public string Local { get; set; } = "Despensa";
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = new List<MovimentacaoEstoque>();
    public ICollection<PerdaEstoque> Perdas { get; set; } = new List<PerdaEstoque>();
}
