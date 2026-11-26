namespace SNDClinico.Web.Data;

public class Recebimento
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public DateTime DataRecebimento { get; set; } = DateTime.UtcNow;
    public int? FornecedorId { get; set; }
    public Fornecedor? Fornecedor { get; set; }
    public string Responsavel { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public string? UsuarioId { get; set; }
    public ICollection<ItemRecebimento> Itens { get; set; } = new List<ItemRecebimento>();
}
