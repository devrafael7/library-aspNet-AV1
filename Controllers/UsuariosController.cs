using BibliotecaAspNet.Models;
using BibliotecaAspNet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAspNet.Controllers;

public class UsuariosController : Controller
{
    private readonly IUsuarioRepository _repository;

    public UsuariosController(IUsuarioRepository repository)
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

        var usuario = await _repository.GetByIdAsync(id.Value);
        if (usuario is null) return NotFound();

        return View(usuario);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Usuario usuario)
    {
        if (!ModelState.IsValid) return View(usuario);

        await _repository.AddAsync(usuario);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var usuario = await _repository.GetByIdAsync(id.Value);
        if (usuario is null) return NotFound();

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Usuario usuario)
    {
        if (id != usuario.Id) return NotFound();

        if (!ModelState.IsValid) return View(usuario);

        if (!await _repository.ExistsAsync(id)) return NotFound();

        await _repository.UpdateAsync(usuario);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var usuario = await _repository.GetByIdAsync(id.Value);
        if (usuario is null) return NotFound();

        return View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
