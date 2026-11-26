namespace SNDClinico.Web.Data;

public class FichaTecnica
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string? Observacoes { get; set; }
    public decimal RendimentoFinalGramas { get; set; }
    public int Porcoes { get; set; } = 1;
    public bool Ativa { get; set; } = true;
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
    public DateTime? AtualizadaEm { get; set; }
    public ICollection<IngredienteFicha> Ingredientes { get; set; } = new List<IngredienteFicha>();
}
