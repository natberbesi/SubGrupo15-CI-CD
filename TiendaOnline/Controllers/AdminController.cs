using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TiendaOnline.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            if (rol != "admin")
                return RedirectToAction("Login", "Auth");

            return View();
        }
    }
}