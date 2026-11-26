namespace SNDClinico.Web.Data;

public class Cardapio
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Escopo { get; set; } = "Geral";
    public int? ModalidadeCardapioId { get; set; }
    public ModalidadeCardapio? ModalidadeCardapio { get; set; }
    public int? CardapioBaseId { get; set; }
    public Cardapio? CardapioBase { get; set; }
    public ICollection<Cardapio> CardapiosDerivados { get; set; } = new List<Cardapio>();
    public DateOnly DataInicio { get; set; }
    public DateOnly DataFim { get; set; }
    public string? Observacoes { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public string? CriadoPorUsuarioId { get; set; }
    public ICollection<RefeicaoCardapio> Refeicoes { get; set; } = new List<RefeicaoCardapio>();
}
