namespace SistemaEscolarCompleto.Models;

public class HorarioAula
{
    public int Id { get; set; }

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public int MateriaId { get; set; }
    public Materia? Materia { get; set; }

    public string ProfessorId { get; set; } = string.Empty;
    public ApplicationUser? Professor { get; set; }

    // 1=Segunda, 2=Terça, 3=Quarta, 4=Quinta, 5=Sexta
    public int DiaSemana { get; set; }

    // 1 a 9
    public int NumeroAula { get; set; }

    public bool Ativo { get; set; } = true;
}