namespace SNDClinico.Web.Data;

public class ContagemInventario
{
    public int Id { get; set; }
    public int InventarioEstoqueId { get; set; }
    public InventarioEstoque InventarioEstoque { get; set; } = null!;
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public decimal QuantidadeSistema { get; set; }
    public decimal QuantidadeContada { get; set; }
    public decimal Diferenca { get; set; }
    public string Unidade { get; set; } = string.Empty;
    public string? Justificativa { get; set; }
    public bool AjusteAplicado { get; set; }
}
