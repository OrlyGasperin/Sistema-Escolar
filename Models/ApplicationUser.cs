using Microsoft.AspNetCore.Identity;

namespace SistemaEscolarCompleto.Models;

public class ApplicationUser : IdentityUser
{
    public string NomeCompleto { get; set; } = string.Empty;
    public TipoUsuario TipoUsuario { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // Navegação
    public ICollection<ProfessorMateriaTurma> Vinculos { get; set; } = new List<ProfessorMateriaTurma>();
}