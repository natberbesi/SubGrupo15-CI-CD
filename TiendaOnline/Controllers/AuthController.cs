using TiendaOnline.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace TiendaOnline.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _connectionString = "Server=mysql_container;Port=3306;Database=ecommerce_db;Uid=poligrangrupo15;Pwd=poli@/87**;";

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UsuarioRol") == "admin")
                return RedirectToAction("Index", "Admin");

            if (HttpContext.Session.GetString("UsuarioRol") == "cliente")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string query = @"SELECT USU_NID, USU_CNOMBRE, USU_CCORREO, USU_CCONTRASEÑA, USU_CROL 
                             FROM TBL_RUSUARIO 
                             WHERE USU_CCORREO = @correo";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@correo", modelo.Correo);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string hashGuardado = reader["USU_CCONTRASEÑA"].ToString();
                if (VerificarPassword(modelo.Clave, hashGuardado))
                {
                    string nombre = reader["USU_CNOMBRE"].ToString();
                    string rol = reader["USU_CROL"].ToString();

                    HttpContext.Session.SetString("UsuarioNombre", nombre);
                    HttpContext.Session.SetString("UsuarioRol", rol);

                    return rol == "admin"
                        ? RedirectToAction("Index", "Admin")
                        : RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Correo o contraseña incorrectos.");
            return View(modelo);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string checkQuery = "SELECT COUNT(*) FROM TBL_RUSUARIO WHERE USU_CCORREO = @correo";
            using (var checkCmd = new MySqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@correo", modelo.Correo);
                long count = (long)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    ViewBag.Error = "Ya existe un usuario con ese correo.";
                    return View(modelo);
                }
            }

            string insertQuery = @"INSERT INTO TBL_RUSUARIO (USU_CNOMBRE, USU_CCORREO, USU_CCONTRASEÑA, USU_CROL)
                                   VALUES (@nombre, @correo, @contraseña, 'cliente')";

            using (var cmd = new MySqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("@correo", modelo.Correo);
                cmd.Parameters.AddWithValue("@contraseña", HashPassword(modelo.Clave));
                cmd.ExecuteNonQuery();
            }

            HttpContext.Session.SetString("UsuarioNombre", modelo.Nombre);
            HttpContext.Session.SetString("UsuarioRol", "cliente");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CrearAdmin()
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            string checkQuery = "SELECT COUNT(*) FROM TBL_RUSUARIO WHERE USU_CCORREO = 'admin@admin.com'";
            using (var cmd = new MySqlCommand(checkQuery, conn))
            {
                long count = (long)cmd.ExecuteScalar();
                if (count > 0)
                    return Content("El usuario administrador ya existe.");
            }

            string insertQuery = @"INSERT INTO TBL_RUSUARIO (USU_CNOMBRE, USU_CCORREO, USU_CCONTRASEÑA, USU_CROL)
                                   VALUES ('Administrador', 'admin@admin.com', @contraseña, 'admin')";
            using (var cmd = new MySqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@contraseña", HashPassword("admin123"));
                cmd.ExecuteNonQuery();
            }

            return Content("Administrador creado: admin@admin.com / admin123");
        }

        private string HashPassword(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerificarPassword(string claveIngresada, string claveHasheada)
        {
            var claveIngresadaHasheada = HashPassword(claveIngresada);
            return claveHasheada == claveIngresadaHasheada;
        }
    }
}
