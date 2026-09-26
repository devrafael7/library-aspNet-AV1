using BibliotecaAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAspNet.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Livro> Livros => Set<Livro>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Emprestimo> Emprestimos => Set<Emprestimo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Livro)
            .WithMany()
            .HasForeignKey(e => e.LivroId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Usuario)
            .WithMany()
            .HasForeignKey(e => e.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
