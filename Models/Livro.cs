using System.ComponentModel.DataAnnotations;

namespace BibliotecaAspNet.Models;

public class Livro
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Autor { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string ISBN { get; set; } = string.Empty;

    [Range(1, 2100)]
    public int AnoPublicacao { get; set; }

    [Required]
    [StringLength(80)]
    public string Categoria { get; set; } = string.Empty;
}
