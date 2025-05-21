using Microsoft.AspNetCore.Mvc;

namespace TiendaOnline.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View( );
        }
    }
}
