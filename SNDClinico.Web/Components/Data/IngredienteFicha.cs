namespace SNDClinico.Web.Data;

public class IngredienteFicha
{
    public int Id { get; set; }
    public int FichaTecnicaId { get; set; }
    public FichaTecnica FichaTecnica { get; set; } = null!;
    public int AlimentoId { get; set; }
    public Alimento Alimento { get; set; } = null!;
    public string NomeAlimentoSnapshot { get; set; } = string.Empty;
    public string Unidade { get; set; } = "g";
    public decimal QuantidadeLiquida { get; set; }
    public decimal QuantidadeBruta { get; set; }
    public decimal FatorCorrecao { get; set; } = 1m;
    public decimal PrecoSnapshot { get; set; }
    public string UnidadePrecoSnapshot { get; set; } = "kg";
    public decimal CaloriasSnapshot { get; set; }
    public decimal CarboidratosSnapshot { get; set; }
    public decimal ProteinasSnapshot { get; set; }
    public decimal LipidiosSnapshot { get; set; }
    public decimal FibrasSnapshot { get; set; }
    public decimal SodioSnapshot { get; set; }
    public string UnidadeNutricional { get; set; } = "g";
    public int Ordem { get; set; }
}
