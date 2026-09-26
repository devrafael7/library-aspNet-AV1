using BibliotecaAspNet.Models;

namespace BibliotecaAspNet.Repositories;

public interface ILivroRepository
{
    Task<List<Livro>> GetAllAsync();
    Task<Livro?> GetByIdAsync(int id);
    Task AddAsync(Livro livro);
    Task UpdateAsync(Livro livro);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
