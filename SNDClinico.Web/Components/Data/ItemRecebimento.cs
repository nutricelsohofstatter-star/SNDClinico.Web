namespace SNDClinico.Web.Data;

public class ItemRecebimento
{
    public int Id { get; set; }
    public int RecebimentoId { get; set; }
    public Recebimento Recebimento { get; set; } = null!;
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public string CodigoLote { get; set; } = string.Empty;
    public DateTime? Fabricacao { get; set; }
    public DateTime Validade { get; set; }
    public decimal QuantidadeEntregue { get; set; }
    public decimal QuantidadeAceita { get; set; }
    public decimal QuantidadeRecusada { get; set; }
    public string Unidade { get; set; } = "kg";
    public decimal CustoUnitario { get; set; }
    public string? Observacao { get; set; }
    public ICollection<LoteEstoque> LotesGerados { get; set; } = new List<LoteEstoque>();
}
