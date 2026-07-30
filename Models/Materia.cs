namespace SistemaEscolarCompleto.Models;

public class Materia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public bool Ativa { get; set; } = true;

    public ICollection<ProfessorMateriaTurma> Vinculos { get; set; } = new List<ProfessorMateriaTurma>();
    public ICollection<AulaCalendario> Aulas { get; set; } = new List<AulaCalendario>();
}