namespace SistemaEscolarCompleto.Models;

public class SolicitacaoAlteracaoFalta
{
    public int Id { get; set; }

    public int FaltaId { get; set; }
    public Falta? Falta { get; set; }

    public string ProfessorSolicitanteId { get; set; } = string.Empty;
    public ApplicationUser? ProfessorSolicitante { get; set; }

    public string Motivo { get; set; } = string.Empty;
    public StatusFalta StatusDesejado { get; set; } // Ativa ou Cancelada
    public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;

    public StatusSolicitacao Status { get; set; } = StatusSolicitacao.Pendente;

    public string? PedagogoRespondeuId { get; set; }
    public ApplicationUser? PedagogoRespondeu { get; set; }
    public string? RespostaPedagogo { get; set; }
    public DateTime? DataResposta { get; set; }
}