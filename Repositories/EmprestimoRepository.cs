using BibliotecaAspNet.Data;
using BibliotecaAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAspNet.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly AppDbContext _context;

    public EmprestimoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Emprestimo>> GetAllAsync()
    {
        return await _context.Emprestimos
            .Include(e => e.Livro)
            .Include(e => e.Usuario)
            .OrderByDescending(e => e.DataEmprestimo)
            .ToListAsync();
    }

    public async Task<Emprestimo?> GetByIdAsync(int id)
    {
        return await _context.Emprestimos
            .Include(e => e.Livro)
            .Include(e => e.Usuario)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Emprestimo emprestimo)
    {
        _context.Emprestimos.Add(emprestimo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Emprestimo emprestimo)
    {
        _context.Emprestimos.Update(emprestimo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var emprestimo = await GetByIdAsync(id);
        if (emprestimo is not null)
        {
            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Emprestimos.AnyAsync(e => e.Id == id);
    }
}
