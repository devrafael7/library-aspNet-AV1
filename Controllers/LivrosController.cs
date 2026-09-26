using BibliotecaAspNet.Models;
using BibliotecaAspNet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAspNet.Controllers;

public class LivrosController : Controller
{
    private readonly ILivroRepository _repository;

    public LivrosController(ILivroRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _repository.GetAllAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var livro = await _repository.GetByIdAsync(id.Value);
        if (livro is null) return NotFound();

        return View(livro);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Livro livro)
    {
        if (!ModelState.IsValid) return View(livro);

        await _repository.AddAsync(livro);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var livro = await _repository.GetByIdAsync(id.Value);
        if (livro is null) return NotFound();

        return View(livro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Livro livro)
    {
        if (id != livro.Id) return NotFound();

        if (!ModelState.IsValid) return View(livro);

        if (!await _repository.ExistsAsync(id)) return NotFound();

        await _repository.UpdateAsync(livro);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var livro = await _repository.GetByIdAsync(id.Value);
        if (livro is null) return NotFound();

        return View(livro);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
