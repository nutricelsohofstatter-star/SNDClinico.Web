namespace SNDClinico.Web.Data;

public class ModalidadeCardapio
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Ativa { get; set; } = true;
    public ICollection<Cardapio> Cardapios { get; set; } = new List<Cardapio>();
}
