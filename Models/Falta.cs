namespace SistemaEscolarCompleto.Models;

public class Falta
{
    public int Id { get; set; }
    public DateTime Data { get; set; }

    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    public int MateriaId { get; set; }
    public Materia? Materia { get; set; }

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public string ProfessorLancouId { get; set; } = string.Empty;
    public ApplicationUser? ProfessorLancou { get; set; }

    public StatusFalta Status { get; set; } = StatusFalta.Ativa;
    public DateTime DataLancamento { get; set; } = DateTime.Now;
    public string? Observacao { get; set; }

    public ICollection<SolicitacaoAlteracaoFalta> Solicitacoes { get; set; } = new List<SolicitacaoAlteracaoFalta>();
}