using Microsoft.AspNetCore.Mvc;

namespace TiendaOnline.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View( );
        }
    }
}
