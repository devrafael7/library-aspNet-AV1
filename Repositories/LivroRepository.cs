using BibliotecaAspNet.Data;
using BibliotecaAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAspNet.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;

    public LivroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Livro>> GetAllAsync()
    {
        return await _context.Livros
            .OrderBy(l => l.Titulo)
            .ToListAsync();
    }

    public async Task<Livro?> GetByIdAsync(int id)
    {
        return await _context.Livros.FindAsync(id);
    }

    public async Task AddAsync(Livro livro)
    {
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Livro livro)
    {
        _context.Livros.Update(livro);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var livro = await GetByIdAsync(id);
        if (livro is not null)
        {
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Livros.AnyAsync(l => l.Id == id);
    }
}
