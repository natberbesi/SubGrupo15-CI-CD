using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.Domian
{
    public class CarritoModel
    {
        [Key]
        public int CarritoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioModel? Usuario { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public ProductoModel? Producto { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Cantidad { get; set; }
    }
}
