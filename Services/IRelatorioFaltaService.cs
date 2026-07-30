namespace SistemaEscolarCompleto.Services;

public enum PeriodoRelatorio
{
    Dia = 1,
    Mes = 2,
    Semestre = 3,
    Ano = 4
}

public class ItemRelatorioFalta
{
    public string Rotulo { get; set; } = string.Empty; // Ex: "Faltou", "Presente"
    public int Quantidade { get; set; }
    public double Percentual { get; set; }
}

public class ResultadoRelatorioFalta
{
    public List<ItemRelatorioFalta> Itens { get; set; } = new();
    public int TotalAulasNoPeriodo { get; set; }
    public int TotalFaltasNoPeriodo { get; set; }
}

public interface IRelatorioFaltaService
{
    // turmaId/materiaId/professorId nulos = sem filtro (todos)
    Task<ResultadoRelatorioFalta> CalcularPercentualFaltasAsync(
        PeriodoRelatorio periodo,
        DateTime dataReferencia,
        int? turmaId = null,
        int? alunoId = null,
        int? materiaId = null,
        string? professorId = null);
}