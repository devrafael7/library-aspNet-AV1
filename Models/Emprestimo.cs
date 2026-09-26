using System.ComponentModel.DataAnnotations;

namespace BibliotecaAspNet.Models;

public class Emprestimo
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Livro")]
    public int LivroId { get; set; }

    [Required]
    [Display(Name = "Usuário")]
    public int UsuarioId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data do Empréstimo")]
    public DateTime DataEmprestimo { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Devolução")]
    public DateTime? DataDevolucao { get; set; }

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Ativo";

    public Livro? Livro { get; set; }
    public Usuario? Usuario { get; set; }
}
