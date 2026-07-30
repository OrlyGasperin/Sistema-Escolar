using SistemaEscolarCompleto.Models;

namespace SistemaEscolarCompleto.Services;

public class ResultadoOperacao
{
    public bool Sucesso { get; set; }
    public string? MensagemErro { get; set; }

    public static ResultadoOperacao Ok() => new() { Sucesso = true };
    public static ResultadoOperacao Falha(string mensagem) => new() { Sucesso = false, MensagemErro = mensagem };
}

public interface IFaltaService
{
    Task<ResultadoOperacao> LancarFaltaAsync(int alunoId, int materiaId, int turmaId, DateTime data,
        string professorId, string professorNome, string? observacao);

    Task<ResultadoOperacao> EditarFaltaDiretoAsync(int faltaId, StatusFalta novoStatus,
        string usuarioId, string usuarioNome, string? observacao);

    Task<ResultadoOperacao> SolicitarAlteracaoAsync(int faltaId, string professorId, string professorNome,
        StatusFalta statusDesejado, string motivo);

    Task<ResultadoOperacao> ResponderSolicitacaoAsync(int solicitacaoId, bool aprovar,
        string pedagogoId, string pedagogoNome, string resposta);
}