namespace SNDClinico.Web.Data;

public class AjusteInventario
{
    public int Id { get; set; }
    public int ContagemInventarioId { get; set; }
    public ContagemInventario ContagemInventario { get; set; } = null!;
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public int? LoteEstoqueId { get; set; }
    public LoteEstoque? LoteEstoque { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public decimal Quantidade { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string Responsavel { get; set; } = string.Empty;
    public string? UsuarioId { get; set; }
}
