using BibliotecaAspNet.Models;

namespace BibliotecaAspNet.Repositories;

public interface IEmprestimoRepository
{
    Task<List<Emprestimo>> GetAllAsync();
    Task<Emprestimo?> GetByIdAsync(int id);
    Task AddAsync(Emprestimo emprestimo);
    Task UpdateAsync(Emprestimo emprestimo);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
