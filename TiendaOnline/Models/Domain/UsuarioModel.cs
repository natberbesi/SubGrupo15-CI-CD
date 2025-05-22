using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.Domian
{
    public class UsuarioModel
    {
        [Key]
        public int UsuarioId { get; set; }

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
        public string Rol { get; set; } // "admin" o "cliente"

        public ICollection<VentaModel>? Ventas { get; set; }
        public ICollection<CarritoModel>? Carrito { get; set; }
    }
}
