namespace SistemaEscolarCompleto.Models;

public class Aviso
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Mensagem { get; set; } = string.Empty;
    public DateTime DataPublicacao { get; set; } = DateTime.Now;
    public bool Ativo { get; set; } = true;

    public string AutorId { get; set; } = string.Empty;
    public ApplicationUser? Autor { get; set; }
}