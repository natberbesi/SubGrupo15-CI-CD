using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.ViewModel
{
    public class RegistroViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        [Required]
        [Compare("Clave", ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        public string ConfirmarClave { get; set; }
    }
}
