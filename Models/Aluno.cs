namespace SistemaEscolarCompleto.Models;

public class Aluno
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string? NomeResponsavel { get; set; }
    public string? TelefoneResponsavel { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataMatricula { get; set; } = DateTime.UtcNow;

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public ICollection<Falta> Faltas { get; set; } = new List<Falta>();
}