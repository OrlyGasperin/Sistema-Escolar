namespace SistemaEscolarCompleto.Models;

public class LogAuditoria
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; } = DateTime.UtcNow;

    public string UsuarioId { get; set; } = string.Empty;
    public string UsuarioNome { get; set; } = string.Empty; // snapshot, evita join se usuário for excluído

    public string Acao { get; set; } = string.Empty; // Criação, Edição, Exclusão
    public string Entidade { get; set; } = string.Empty; // Ex: "Aluno", "Turma", "Falta"
    public string? EntidadeId { get; set; }
    public string? DetalhesAntes { get; set; }
    public string? DetalhesDepois { get; set; }
}