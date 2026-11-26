namespace SNDClinico.Web.Data;

public class HistoricoAuditoria
{
    public long Id { get; set; }
    public string Entidade { get; set; } = string.Empty;
    public string EntidadeId { get; set; } = string.Empty;
    public string Acao { get; set; } = string.Empty;
    public string? DadosAnteriores { get; set; }
    public string? DadosNovos { get; set; }
    public DateTime DataHora { get; set; } = DateTime.UtcNow;
    public string? UsuarioId { get; set; }
    public string? UsuarioNome { get; set; }
}
