
namespace SNDClinico.Web.Data;

public class Alimento
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Unidade { get; set; } = "kg";

    public string Categoria { get; set; } = "Outros";

    public bool Ativo { get; set; } = true;

    public decimal Preco { get; set; }

    public decimal Calorias { get; set; }

    public decimal Carboidratos { get; set; }

    public decimal Proteinas { get; set; }

    public decimal Lipidios { get; set; }

    public decimal Fibras { get; set; }

    public decimal Sodio { get; set; }

    public decimal FatorCorrecao { get; set; } = 1m;
}
