namespace SistemaEscolarCompleto.Authorization;

// Nome do Claim usado para guardar o TipoUsuario (Admin/Pedagogo/Professor) no cookie de autenticação.
public static class ClaimsTipoUsuario
{
    public const string ClaimType = "TipoUsuario";
}