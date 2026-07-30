namespace SistemaEscolarCompleto.Services;

public interface IAuditoriaService
{
    Task RegistrarAsync(string usuarioId, string usuarioNome, string acao, string entidade,
        string? entidadeId = null, string? detalhesAntes = null, string? detalhesDepois = null);
}