namespace SNDClinico.Web.Data;

public class MovimentacaoEstoque
{
    public int Id { get; set; }
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public int? LoteEstoqueId { get; set; }
    public LoteEstoque? LoteEstoque { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public string Tipo { get; set; } = string.Empty;
    public decimal Quantidade { get; set; }
    public string Unidade { get; set; } = string.Empty;
    public string Sinal { get; set; } = string.Empty;
    public string Responsavel { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public string? UsuarioId { get; set; }
}
