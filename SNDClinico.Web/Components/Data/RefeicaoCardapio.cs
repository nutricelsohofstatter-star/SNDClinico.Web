namespace SNDClinico.Web.Data;

public class RefeicaoCardapio
{
    public int Id { get; set; }
    public int CardapioId { get; set; }
    public Cardapio Cardapio { get; set; } = null!;
    public DateOnly Data { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int QuantidadePrevista { get; set; }
    public string? Observacoes { get; set; }
    public int Ordem { get; set; }
    public ICollection<PreparacaoCardapio> Preparacoes { get; set; } = new List<PreparacaoCardapio>();
}
