using System.ComponentModel.DataAnnotations;

namespace BibliotecaAspNet.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(14)]
    public string CPF { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Telefone { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }
}
