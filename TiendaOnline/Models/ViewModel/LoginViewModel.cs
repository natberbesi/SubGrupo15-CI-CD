using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; } = string.Empty;
    }
}



