namespace SNDClinicoWeb;

// DEMONSTRAÇÃO: dados apenas em memória; não armazena prontuários reais.
public sealed class HospitalStore
{
    private readonly object sync = new();
    private readonly List<PacienteInternado> pacientes = new();
    private readonly List<MapaDieta> mapas = new();
    private int proximoId = 1;

    public IReadOnlyList<PacienteInternado> Pacientes() { lock(sync) return pacientes.Select(p => p with { }).ToList(); }
    public IReadOnlyList<MapaDieta> Mapas() { lock(sync) return mapas.Select(m => m with { Itens = m.Itens.Select(i => i with { }).ToList() }).ToList(); }
    public void AdicionarPaciente(string nome, int idade, string leito, string unidade)
    {
        if (string.IsNullOrWhiteSpace(nome) || idade < 0 || idade > 125 || string.IsNullOrWhiteSpace(leito))
            throw new ArgumentException("Informe nome, idade e leito válidos.");
        lock(sync) pacientes.Add(new PacienteInternado(proximoId++, nome.Trim(), idade, leito.Trim(), unidade.Trim(), "Pendente", "Não avaliado"));
    }
    public void RegistrarTriagem(int id, string resultado, string classificacao)
    {
        lock(sync) { var idx = pacientes.FindIndex(p => p.Id == id); if(idx >= 0) pacientes[idx] = pacientes[idx] with { Triagem = resultado, EstadoNutricional = classificacao }; }
    }
    public MapaDieta ObterOuCriarMapa(DateTime data)
    {
        lock(sync)
        {
            var mapa = mapas.FirstOrDefault(m => m.Data.Date == data.Date);
            if (mapa is null)
            {
                var anterior = mapas.Where(m=>m.Data.Date<data.Date).OrderByDescending(m=>m.Data).FirstOrDefault();
                mapa = new MapaDieta { Data = data.Date, Itens = anterior is null ? new() : anterior.Itens.Select(x => x with { }).ToList() };
                mapas.Add(mapa);
            }
            return mapa with { Itens = mapa.Itens.Select(x => x with { }).ToList() };
        }
    }
    public void SalvarMapa(MapaDieta mapa)
    {
        lock(sync)
        {
            var idx=mapas.FindIndex(m=>m.Data.Date==mapa.Data.Date);
            var copia=mapa with { Itens=mapa.Itens.Select(x=>x with { }).ToList() };
            if(idx >= 0) mapas[idx]=copia; else mapas.Add(copia);
        }
    }
}
public record PacienteInternado(int Id, string Nome, int Idade, string Leito, string Unidade, string Triagem, string EstadoNutricional);
public record MapaItem(int PacienteId, string Nome, string Leito, string TipoDieta, string Suplemento, string Observacoes);
public record MapaDieta { public DateTime Data { get; init; } public List<MapaItem> Itens { get; set; } = new(); }
