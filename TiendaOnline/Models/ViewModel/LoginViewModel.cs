using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}
