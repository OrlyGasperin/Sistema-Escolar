using Microsoft.EntityFrameworkCore;
using SistemaEscolarCompleto.Data;
using SistemaEscolarCompleto.Models;

namespace SistemaEscolarCompleto.Services;

public class FaltaService : IFaltaService
{
    private readonly ApplicationDbContext _context;
    private readonly IAuditoriaService _auditoria;

    public FaltaService(ApplicationDbContext context, IAuditoriaService auditoria)
    {
        _context = context;
        _auditoria = auditoria;
    }

    public async Task<ResultadoOperacao> LancarFaltaAsync(int alunoId, int materiaId, int turmaId, DateTime data,
        string professorId, string professorNome, string? observacao)
    {
        bool jaExiste = await _context.Faltas.AnyAsync(f =>
            f.AlunoId == alunoId && f.MateriaId == materiaId && f.Data.Date == data.Date &&
            f.Status == StatusFalta.Ativa);

        if (jaExiste)
            return ResultadoOperacao.Falha("Já existe uma falta lançada para este aluno, nesta matéria, nesta data.");

        var falta = new Falta
        {
            AlunoId = alunoId,
            MateriaId = materiaId,
            TurmaId = turmaId,
            Data = data.Date,
            ProfessorLancouId = professorId,
            Status = StatusFalta.Ativa,
            Observacao = observacao,
            DataLancamento = DateTime.UtcNow
        };

        _context.Faltas.Add(falta);
        await _context.SaveChangesAsync();

        await _auditoria.RegistrarAsync(professorId, professorNome, "Criação", "Falta", falta.Id.ToString(),
            null, $"Falta lançada para aluno {alunoId}, matéria {materiaId}, data {data:dd/MM/yyyy}");

        return ResultadoOperacao.Ok();
    }

    public async Task<ResultadoOperacao> EditarFaltaDiretoAsync(int faltaId, StatusFalta novoStatus,
        string usuarioId, string usuarioNome, string? observacao)
    {
        var falta = await _context.Faltas.FindAsync(faltaId);
        if (falta is null)
            return ResultadoOperacao.Falha("Falta não encontrada.");

        string antes = $"Status: {falta.Status}, Observação: {falta.Observacao}";

        falta.Status = novoStatus;
        if (observacao is not null)
            falta.Observacao = observacao;

        await _context.SaveChangesAsync();

        string depois = $"Status: {falta.Status}, Observação: {falta.Observacao}";

        await _auditoria.RegistrarAsync(usuarioId, usuarioNome, "Edição", "Falta", falta.Id.ToString(), antes, depois);

        return ResultadoOperacao.Ok();
    }

    public async Task<ResultadoOperacao> SolicitarAlteracaoAsync(int faltaId, string professorId, string professorNome,
        StatusFalta statusDesejado, string motivo)
    {
        var falta = await _context.Faltas.FindAsync(faltaId);
        if (falta is null)
            return ResultadoOperacao.Falha("Falta não encontrada.");

        if (falta.ProfessorLancouId != professorId)
            return ResultadoOperacao.Falha("Você só pode solicitar alteração de faltas que você mesmo lançou.");

        bool jaTemPendente = await _context.SolicitacoesAlteracaoFalta
            .AnyAsync(s => s.FaltaId == faltaId && s.Status == StatusSolicitacao.Pendente);

        if (jaTemPendente)
            return ResultadoOperacao.Falha("Já existe uma solicitação pendente para esta falta.");

        var solicitacao = new SolicitacaoAlteracaoFalta
        {
            FaltaId = faltaId,
            ProfessorSolicitanteId = professorId,
            Motivo = motivo,
            StatusDesejado = statusDesejado,
            Status = StatusSolicitacao.Pendente,
            DataSolicitacao = DateTime.UtcNow
        };

        _context.SolicitacoesAlteracaoFalta.Add(solicitacao);
        await _context.SaveChangesAsync();

        await _auditoria.RegistrarAsync(professorId, professorNome, "Criação", "SolicitacaoAlteracaoFalta",
            solicitacao.Id.ToString(), null, $"Solicitação para falta {faltaId}, motivo: {motivo}");

        return ResultadoOperacao.Ok();
    }

    public async Task<ResultadoOperacao> ResponderSolicitacaoAsync(int solicitacaoId, bool aprovar,
        string pedagogoId, string pedagogoNome, string resposta)
    {
        var solicitacao = await _context.SolicitacoesAlteracaoFalta
            .Include(s => s.Falta)
            .FirstOrDefaultAsync(s => s.Id == solicitacaoId);

        if (solicitacao is null)
            return ResultadoOperacao.Falha("Solicitação não encontrada.");

        if (solicitacao.Status != StatusSolicitacao.Pendente)
            return ResultadoOperacao.Falha("Esta solicitação já foi respondida.");

        solicitacao.Status = aprovar ? StatusSolicitacao.Aprovada : StatusSolicitacao.Rejeitada;
        solicitacao.PedagogoRespondeuId = pedagogoId;
        solicitacao.RespostaPedagogo = resposta;
        solicitacao.DataResposta = DateTime.UtcNow;

        if (aprovar && solicitacao.Falta is not null)
        {
            solicitacao.Falta.Status = solicitacao.StatusDesejado;
        }

        await _context.SaveChangesAsync();

        await _auditoria.RegistrarAsync(pedagogoId, pedagogoNome,
            aprovar ? "Aprovação" : "Rejeição", "SolicitacaoAlteracaoFalta", solicitacao.Id.ToString(),
            null, $"Resposta: {resposta}");

        return ResultadoOperacao.Ok();
    }
}