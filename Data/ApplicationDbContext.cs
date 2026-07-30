using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarCompleto.Models;

namespace SistemaEscolarCompleto.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Turma> Turmas => Set<Turma>();
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Materia> Materias => Set<Materia>();
    public DbSet<ProfessorMateriaTurma> ProfessorMateriaTurmas => Set<ProfessorMateriaTurma>();
    public DbSet<AulaCalendario> AulasCalendario => Set<AulaCalendario>();
    public DbSet<Falta> Faltas => Set<Falta>();
    public DbSet<SolicitacaoAlteracaoFalta> SolicitacoesAlteracaoFalta => Set<SolicitacaoAlteracaoFalta>();
    public DbSet<Aviso> Avisos => Set<Aviso>();
    public DbSet<LogAuditoria> LogsAuditoria => Set<LogAuditoria>();
    public DbSet<HorarioAula> HorariosAula => Set<HorarioAula>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<HorarioAula>()
    .HasOne(h => h.Turma)
    .WithMany()
    .HasForeignKey(h => h.TurmaId)
    .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<HorarioAula>()
            .HasOne(h => h.Materia)
            .WithMany()
            .HasForeignKey(h => h.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<HorarioAula>()
            .HasOne(h => h.Professor)
            .WithMany()
            .HasForeignKey(h => h.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<HorarioAula>()
            .HasIndex(h => new { h.TurmaId, h.DiaSemana, h.NumeroAula })
            .IsUnique();
        // ---- Aluno -> Turma (N:1) ----
        builder.Entity<Aluno>()
            .HasOne(a => a.Turma)
            .WithMany(t => t.Alunos)
            .HasForeignKey(a => a.TurmaId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- ProfessorMateriaTurma (N:N: Professor + Materia + Turma) ----
        builder.Entity<ProfessorMateriaTurma>()
            .HasOne(p => p.Professor)
            .WithMany(u => u.Vinculos)
            .HasForeignKey(p => p.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ProfessorMateriaTurma>()
            .HasOne(p => p.Materia)
            .WithMany(m => m.Vinculos)
            .HasForeignKey(p => p.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ProfessorMateriaTurma>()
            .HasOne(p => p.Turma)
            .WithMany(t => t.Vinculos)
            .HasForeignKey(p => p.TurmaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Evita vínculo duplicado: mesmo professor + mesma matéria + mesma turma
        builder.Entity<ProfessorMateriaTurma>()
            .HasIndex(p => new { p.ProfessorId, p.MateriaId, p.TurmaId })
            .IsUnique();

        // ---- AulaCalendario ----
        builder.Entity<AulaCalendario>()
            .HasOne(a => a.Turma)
            .WithMany(t => t.Aulas)
            .HasForeignKey(a => a.TurmaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AulaCalendario>()
            .HasOne(a => a.Materia)
            .WithMany(m => m.Aulas)
            .HasForeignKey(a => a.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AulaCalendario>()
            .HasOne(a => a.Professor)
            .WithMany()
            .HasForeignKey(a => a.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- Falta ----
        builder.Entity<Falta>()
            .HasOne(f => f.Aluno)
            .WithMany(a => a.Faltas)
            .HasForeignKey(f => f.AlunoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Falta>()
            .HasOne(f => f.Materia)
            .WithMany()
            .HasForeignKey(f => f.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Falta>()
            .HasOne(f => f.Turma)
            .WithMany()
            .HasForeignKey(f => f.TurmaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Falta>()
            .HasOne(f => f.ProfessorLancou)
            .WithMany()
            .HasForeignKey(f => f.ProfessorLancouId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- SolicitacaoAlteracaoFalta ----
        builder.Entity<SolicitacaoAlteracaoFalta>()
            .HasOne(s => s.Falta)
            .WithMany(f => f.Solicitacoes)
            .HasForeignKey(s => s.FaltaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SolicitacaoAlteracaoFalta>()
            .HasOne(s => s.ProfessorSolicitante)
            .WithMany()
            .HasForeignKey(s => s.ProfessorSolicitanteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SolicitacaoAlteracaoFalta>()
            .HasOne(s => s.PedagogoRespondeu)
            .WithMany()
            .HasForeignKey(s => s.PedagogoRespondeuId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        // ---- Aviso ----
        builder.Entity<Aviso>()
            .HasOne(a => a.Autor)
            .WithMany()
            .HasForeignKey(a => a.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // ---- LogAuditoria: sem FK de navegação obrigatória (snapshot) ----
        builder.Entity<LogAuditoria>()
            .HasIndex(l => l.DataHora);
    }
}