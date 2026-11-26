namespace SNDClinico.Web.Data;

public class IngredienteDoadoPreparacao
{
    public int Id { get; set; }
    public int PreparacaoCardapioId { get; set; }
    public PreparacaoCardapio PreparacaoCardapio { get; set; } = null!;
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
}
