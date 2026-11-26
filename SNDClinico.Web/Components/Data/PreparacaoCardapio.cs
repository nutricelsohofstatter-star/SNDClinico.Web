namespace SNDClinico.Web.Data;

public class PreparacaoCardapio
{
    public int Id { get; set; }
    public int RefeicaoCardapioId { get; set; }
    public RefeicaoCardapio RefeicaoCardapio { get; set; } = null!;
    public int FichaTecnicaId { get; set; }
    public FichaTecnica FichaTecnica { get; set; } = null!;
    public string NomeSnapshot { get; set; } = string.Empty;
    public int Porcoes { get; set; }
    public decimal PesoPorPorcao { get; set; }
    public bool Herdada { get; set; }
    public int? PreparacaoOrigemId { get; set; }
    public PreparacaoCardapio? PreparacaoOrigem { get; set; }
    public ICollection<PreparacaoCardapio> Variacoes { get; set; } = new List<PreparacaoCardapio>();
    public int Ordem { get; set; }
    public ICollection<IngredienteDoadoPreparacao> IngredientesDoados { get; set; } = new List<IngredienteDoadoPreparacao>();
}
