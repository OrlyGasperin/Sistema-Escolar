using Microsoft.AspNetCore.Authorization;
using SistemaEscolarCompleto.Models;

namespace SistemaEscolarCompleto.Authorization;

public static class AuthorizationExtensions
{
    public static void AdicionarPoliticasDoSistema(this AuthorizationOptions options)
    {
        string claimType = ClaimsTipoUsuario.ClaimType;

        options.AddPolicy(Politicas.GerenciarPedagogos, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString()));

        options.AddPolicy(Politicas.GerenciarProfessores, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));

        options.AddPolicy(Politicas.ExcluirProfessor, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString()));

        options.AddPolicy(Politicas.GerenciarTurmasAlunos, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));

        options.AddPolicy(Politicas.GerenciarMaterias, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));

        options.AddPolicy(Politicas.LancarFalta, p =>
            p.RequireClaim(claimType,
                TipoUsuario.Admin.ToString(),
                TipoUsuario.Pedagogo.ToString(),
                TipoUsuario.Professor.ToString()));

        options.AddPolicy(Politicas.EditarFaltaDireto, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));

        options.AddPolicy(Politicas.AprovarSolicitacaoFalta, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));

        options.AddPolicy(Politicas.GerenciarAvisos, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));

        options.AddPolicy(Politicas.GerenciarCalendario, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));

        options.AddPolicy(Politicas.VerHistorico, p =>
            p.RequireClaim(claimType, TipoUsuario.Admin.ToString(), TipoUsuario.Pedagogo.ToString()));
    }
}