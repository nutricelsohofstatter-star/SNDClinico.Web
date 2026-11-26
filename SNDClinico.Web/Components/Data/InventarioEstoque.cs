namespace SNDClinico.Web.Data;

public class InventarioEstoque
{
    public int Id { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public string Tipo { get; set; } = "Extraordinário";
    public string Status { get; set; } = "Em andamento";
    public string Responsavel { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public DateTime? FinalizadoEm { get; set; }
    public string? UsuarioId { get; set; }
    public ICollection<ContagemInventario> Contagens { get; set; } = new List<ContagemInventario>();
}
