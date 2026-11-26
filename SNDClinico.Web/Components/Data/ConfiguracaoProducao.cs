namespace SNDClinico.Web.Data;

public class ConfiguracaoProducao
{
    public int Id { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime AtualizadaEm { get; set; } = DateTime.UtcNow;
    public string? UsuarioId { get; set; }
}
