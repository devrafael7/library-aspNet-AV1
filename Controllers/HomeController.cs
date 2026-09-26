using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAspNet.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
