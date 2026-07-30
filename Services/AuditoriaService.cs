using SistemaEscolarCompleto.Data;
using SistemaEscolarCompleto.Models;

namespace SistemaEscolarCompleto.Services;

public class AuditoriaService : IAuditoriaService
{
    private readonly ApplicationDbContext _context;

    public AuditoriaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAsync(string usuarioId, string usuarioNome, string acao, string entidade,
        string? entidadeId = null, string? detalhesAntes = null, string? detalhesDepois = null)
    {
        var log = new LogAuditoria
        {
            UsuarioId = usuarioId,
            UsuarioNome = usuarioNome,
            Acao = acao,
            Entidade = entidade,
            EntidadeId = entidadeId,
            DetalhesAntes = detalhesAntes,
            DetalhesDepois = detalhesDepois,
            DataHora = DateTime.UtcNow
        };

        _context.LogsAuditoria.Add(log);
        await _context.SaveChangesAsync();
    }
}