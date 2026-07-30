using Microsoft.AspNetCore.Identity;
using SistemaEscolarCompleto.Models;

namespace SistemaEscolarCompleto.Data;

public static class DbInitializer
{
    // Credenciais do admin inicial. Troque a senha no primeiro acesso.
    private const string AdminEmail = "admin@escola.com";
    private const string AdminSenha = "Admin@123";

    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var dbContext = services.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.EnsureCreatedAsync();

        var admin = await userManager.FindByEmailAsync(AdminEmail);
        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true,
                NomeCompleto = "Administrador do Sistema",
                TipoUsuario = TipoUsuario.Admin,
                Ativo = true
            };

            var resultado = await userManager.CreateAsync(admin, AdminSenha);
            if (!resultado.Succeeded)
            {
                throw new Exception(
                    "Falha ao criar usuário admin: " +
                    string.Join(", ", resultado.Errors.Select(e => e.Description)));
            }
        }
    }
}