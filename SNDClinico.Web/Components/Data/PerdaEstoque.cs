namespace SNDClinico.Web.Data;

public class PerdaEstoque
{
    public int Id { get; set; }
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public int LoteEstoqueId { get; set; }
    public LoteEstoque LoteEstoque { get; set; } = null!;
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public decimal Quantidade { get; set; }
    public string Unidade { get; set; } = string.Empty;
    public string Motivo { get; set; } = string.Empty;
    public string Responsavel { get; set; } = string.Empty;
    public decimal CustoTotal { get; set; }
    public string? Observacao { get; set; }
    public string? UsuarioId { get; set; }
}
