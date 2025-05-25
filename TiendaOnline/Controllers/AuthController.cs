using Microsoft.AspNetCore.Mvc;
using TiendaOnline.Data;
using TiendaOnline.Models.Domian;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace TiendaOnline.Controllers
{
    public class AuthController : Controller
    {
        private readonly TiendaOnlineContext _context;

        public AuthController(TiendaOnlineContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "admin")
                return RedirectToAction("Index", "Admin");

            if (HttpContext.Session.GetString("UsuarioRol") == "cliente")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string clave)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                ViewBag.Error = "Correo y contraseña son requeridos.";
                return View();
            }

            string hashedPassword = HashPassword(clave);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == hashedPassword);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

            return usuario.Rol == "admin"
                ? RedirectToAction("Index", "Admin")
                : RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ==== NUEVO: Registro ====

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string correo, string clave, string confirmarClave)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(clave) || string.IsNullOrWhiteSpace(confirmarClave))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            if (clave != confirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            if (await _context.Usuarios.AnyAsync(u => u.Correo == correo))
            {
                ViewBag.Error = "Ya existe un usuario con ese correo.";
                return View();
            }

            var usuario = new UsuarioModel
            {
                Nombre = nombre,
                Correo = correo,
                Clave = HashPassword(clave),
                Rol = "cliente"
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

            return RedirectToAction("Index", "Home");
        }

        // ==== HashPassword ====
        private string HashPassword(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        [HttpGet]
        public async Task<IActionResult> CrearAdmin()
        {
            // Verifica si ya existe un admin
            var existe = await _context.Usuarios
                .AnyAsync(u => u.Correo == "admin@admin.com");

            if (existe)
                return Content("El usuario administrador ya existe.");

            // Crea el usuario admin
            var admin = new UsuarioModel
            {
                Nombre = "Administrador",
                Correo = "admin@admin.com",
                Clave = HashPassword("admin123"), // Contraseña de ejemplo
                Rol = "admin"
            };

            _context.Usuarios.Add(admin);
            await _context.SaveChangesAsync();

            return Content("Administrador creado: admin@admin.com / admin123");
        }

    }
    
}

