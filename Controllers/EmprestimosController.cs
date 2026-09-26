using BibliotecaAspNet.Data;
using BibliotecaAspNet.Models;
using BibliotecaAspNet.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAspNet.Controllers;

public class EmprestimosController : Controller
{
    private readonly IEmprestimoRepository _repository;
    private readonly AppDbContext _context;

    public EmprestimosController(
        IEmprestimoRepository repository,
        AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _repository.GetAllAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var emprestimo = await _repository.GetByIdAsync(id.Value);
        if (emprestimo is null) return NotFound();

        return View(emprestimo);
    }

    public async Task<IActionResult> Create()
    {
        await CarregarListas();
        return View(new Emprestimo
        {
            DataEmprestimo = DateTime.Today,
            Status = "Ativo"
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Emprestimo emprestimo)
    {
        if (!ModelState.IsValid)
        {
            await CarregarListas(emprestimo.LivroId, emprestimo.UsuarioId);
            return View(emprestimo);
        }

        await _repository.AddAsync(emprestimo);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var emprestimo = await _repository.GetByIdAsync(id.Value);
        if (emprestimo is null) return NotFound();

        await CarregarListas(emprestimo.LivroId, emprestimo.UsuarioId);
        return View(emprestimo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Emprestimo emprestimo)
    {
        if (id != emprestimo.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await CarregarListas(emprestimo.LivroId, emprestimo.UsuarioId);
            return View(emprestimo);
        }

        if (!await _repository.ExistsAsync(id)) return NotFound();

        await _repository.UpdateAsync(emprestimo);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var emprestimo = await _repository.GetByIdAsync(id.Value);
        if (emprestimo is null) return NotFound();

        return View(emprestimo);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task CarregarListas(int? livroId = null, int? usuarioId = null)
    {
        var livros = await _context.Livros
            .OrderBy(l => l.Titulo)
            .ToListAsync();

        var usuarios = await _context.Usuarios
            .OrderBy(u => u.Nome)
            .ToListAsync();

        ViewBag.LivroId = new SelectList(livros, "Id", "Titulo", livroId);
        ViewBag.UsuarioId = new SelectList(usuarios, "Id", "Nome", usuarioId);
    }
}
