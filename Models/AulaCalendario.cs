namespace SistemaEscolarCompleto.Models;

// Define os dias em que há aula de determinada matéria para determinada turma.
// Usado como base para o cálculo de percentual de faltas.
public class AulaCalendario
{
    public int Id { get; set; }
    public DateTime Data { get; set; }

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public int MateriaId { get; set; }
    public Materia? Materia { get; set; }

    public string ProfessorId { get; set; } = string.Empty;
    public ApplicationUser? Professor { get; set; }

    public string? Observacao { get; set; }
}