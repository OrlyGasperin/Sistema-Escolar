namespace SistemaEscolarCompleto.Models;

public class Turma
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string AnoLetivo { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty; // Manhã, Tarde, Noite
    public bool Ativa { get; set; } = true;

    public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    public ICollection<ProfessorMateriaTurma> Vinculos { get; set; } = new List<ProfessorMateriaTurma>();
    public ICollection<AulaCalendario> Aulas { get; set; } = new List<AulaCalendario>();
}