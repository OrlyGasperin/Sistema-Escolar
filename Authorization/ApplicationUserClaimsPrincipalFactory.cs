using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SistemaEscolarCompleto.Models;
using System.Security.Claims;


namespace SistemaEscolarCompleto.Authorization;

// Garante que o claim "TipoUsuario" sempre seja incluído no cookie de autenticação,
// para que as políticas baseadas em RequireClaim funcionem.
public class ApplicationUserClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<ApplicationUser>
{
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor)
    {
    }

    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        var identity = (ClaimsIdentity)principal.Identity!;

        if (!identity.HasClaim(c => c.Type == ClaimsTipoUsuario.ClaimType))
        {
            identity.AddClaim(new Claim(ClaimsTipoUsuario.ClaimType, user.TipoUsuario.ToString()));
        }

        identity.AddClaim(new Claim("NomeCompleto", user.NomeCompleto));

        return principal;
    }
}