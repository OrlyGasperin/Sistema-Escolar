namespace SistemaEscolarCompleto.Models;

// Tabela associativa N:N: define quem (Professor) ensina o quê (Materia) para qual turma (Turma)
public class ProfessorMateriaTurma
{
    public int Id { get; set; }

    public string ProfessorId { get; set; } = string.Empty;
    public ApplicationUser? Professor { get; set; }

    public int MateriaId { get; set; }
    public Materia? Materia { get; set; }

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public bool Ativo { get; set; } = true;
}