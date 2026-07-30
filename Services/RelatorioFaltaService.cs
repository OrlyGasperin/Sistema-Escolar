using Microsoft.EntityFrameworkCore;
using SistemaEscolarCompleto.Data;
using SistemaEscolarCompleto.Models;

namespace SistemaEscolarCompleto.Services;

public class RelatorioFaltaService : IRelatorioFaltaService
{
    private readonly ApplicationDbContext _context;

    public RelatorioFaltaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultadoRelatorioFalta> CalcularPercentualFaltasAsync(
        PeriodoRelatorio periodo,
        DateTime dataReferencia,
        int? turmaId = null,
        int? alunoId = null,
        int? materiaId = null,
        string? professorId = null)
    {
        var (inicio, fim) = ObterIntervalo(periodo, dataReferencia);

        // Busca faltas ativas no período
        var faltasQuery = _context.Faltas
            .Where(f => f.Status == StatusFalta.Ativa && f.Data >= inicio && f.Data <= fim);

        if (turmaId.HasValue)
            faltasQuery = faltasQuery.Where(f => f.TurmaId == turmaId.Value);
        if (alunoId.HasValue)
            faltasQuery = faltasQuery.Where(f => f.AlunoId == alunoId.Value);
        if (materiaId.HasValue)
            faltasQuery = faltasQuery.Where(f => f.MateriaId == materiaId.Value);
        if (professorId is not null)
            faltasQuery = faltasQuery.Where(f => f.ProfessorLancouId == professorId);

        // Busca aulas do calendário no período (base para total de aulas)
        var aulasQuery = _context.AulasCalendario
            .Where(a => a.Data >= inicio && a.Data <= fim);

        if (turmaId.HasValue)
            aulasQuery = aulasQuery.Where(a => a.TurmaId == turmaId.Value);
        if (materiaId.HasValue)
            aulasQuery = aulasQuery.Where(a => a.MateriaId == materiaId.Value);
        if (professorId is not null)
            aulasQuery = aulasQuery.Where(a => a.ProfessorId == professorId);

        int totalAulas = await aulasQuery.CountAsync();
        int totalFaltas = await faltasQuery.CountAsync();

        // Se não houver aulas no calendário, usa o total de faltas como base mínima
        // para ainda mostrar algo útil no gráfico
        int baseCalculo = totalAulas > 0 ? totalAulas : totalFaltas;

        // Busca quantos alunos distintos estão na seleção para calcular presenças
        int totalAlunos = 1;
        if (alunoId.HasValue)
        {
            totalAlunos = 1;
        }
        else if (turmaId.HasValue)
        {
            totalAlunos = await _context.Alunos
                .CountAsync(a => a.TurmaId == turmaId.Value && a.Ativo);
        }

        // Calcula presenças possíveis = aulas × alunos
        int presencasPossiveis = totalAulas > 0
            ? totalAulas * (alunoId.HasValue ? 1 : totalAlunos)
            : totalFaltas + 1; // fallback quando não há calendário

        int presencas = Math.Max(presencasPossiveis - totalFaltas, 0);

        double percentualFaltas = presencasPossiveis == 0 ? 0
            : Math.Round((double)totalFaltas / presencasPossiveis * 100, 1);
        double percentualPresencas = Math.Round(100 - percentualFaltas, 1);

        return new ResultadoRelatorioFalta
        {
            TotalAulasNoPeriodo = totalAulas,
            TotalFaltasNoPeriodo = totalFaltas,
            Itens = new List<ItemRelatorioFalta>
            {
                new() { Rotulo = "Presenças", Quantidade = presencas, Percentual = percentualPresencas },
                new() { Rotulo = "Faltas", Quantidade = totalFaltas, Percentual = percentualFaltas }
            }
        };
    }

    private static (DateTime inicio, DateTime fim) ObterIntervalo(PeriodoRelatorio periodo, DateTime dataReferencia)
    {
        dataReferencia = DateTime.SpecifyKind(dataReferencia, DateTimeKind.Utc);

        return periodo switch
        {
            PeriodoRelatorio.Dia => (
                dataReferencia.Date,
                dataReferencia.Date.AddDays(1).AddTicks(-1)),

            PeriodoRelatorio.Mes => (
                new DateTime(dataReferencia.Year, dataReferencia.Month, 1, 0, 0, 0, DateTimeKind.Utc),
                new DateTime(dataReferencia.Year, dataReferencia.Month, 1, 0, 0, 0, DateTimeKind.Utc).AddMonths(1).AddTicks(-1)),

            PeriodoRelatorio.Semestre => (
                new DateTime(dataReferencia.Year, dataReferencia.Month <= 6 ? 1 : 7, 1, 0, 0, 0, DateTimeKind.Utc),
                new DateTime(dataReferencia.Year, dataReferencia.Month <= 6 ? 1 : 7, 1, 0, 0, 0, DateTimeKind.Utc).AddMonths(6).AddTicks(-1)),

            PeriodoRelatorio.Ano => (
                new DateTime(dataReferencia.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                new DateTime(dataReferencia.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddYears(1).AddTicks(-1)),

            _ => throw new ArgumentOutOfRangeException(nameof(periodo))
        };
    }
}