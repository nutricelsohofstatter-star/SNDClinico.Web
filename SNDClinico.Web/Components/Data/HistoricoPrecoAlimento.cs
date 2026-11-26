namespace SNDClinico.Web.Data;

public class HistoricoPrecoAlimento
{
    public int Id { get; set; }
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public decimal PrecoAnterior { get; set; }
    public decimal PrecoNovo { get; set; }
    public string Unidade { get; set; } = "kg";
    public DateTime VigenciaEm { get; set; } = DateTime.UtcNow;
    public string? Observacao { get; set; }
    public string? UsuarioId { get; set; }
}
