namespace SistemaEscolarCompleto.Authorization;

// Nomes centralizados das políticas de autorização.
// Usar estas constantes em vez de strings soltas evita erro de digitação
// e facilita encontrar todo lugar que usa uma política específica.
public static class Politicas
{
    public const string GerenciarPedagogos = "GerenciarPedagogos";       // só Admin
    public const string GerenciarProfessores = "GerenciarProfessores";   // Admin + Pedagogo (cadastrar); exclusão só Admin (checada manualmente)
    public const string ExcluirProfessor = "ExcluirProfessor";           // só Admin
    public const string GerenciarTurmasAlunos = "GerenciarTurmasAlunos"; // Admin + Pedagogo
    public const string GerenciarMaterias = "GerenciarMaterias";         // Admin + Pedagogo
    public const string LancarFalta = "LancarFalta";                     // Admin + Pedagogo + Professor
    public const string EditarFaltaDireto = "EditarFaltaDireto";         // Admin + Pedagogo
    public const string AprovarSolicitacaoFalta = "AprovarSolicitacaoFalta"; // Admin + Pedagogo
    public const string GerenciarAvisos = "GerenciarAvisos";             // Admin + Pedagogo
    public const string GerenciarCalendario = "GerenciarCalendario";     // Admin + Pedagogo
    public const string VerHistorico = "VerHistorico";                   // Admin + Pedagogo
}