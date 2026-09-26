using BibliotecaAspNet.Data;
using BibliotecaAspNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAspNet.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await GetByIdAsync(id);
        if (usuario is not null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Usuarios.AnyAsync(u => u.Id == id);
    }
}
