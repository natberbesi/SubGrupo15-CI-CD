using System.ComponentModel.DataAnnotations;
namespace TiendaOnline.Models.Domian
{

    public class ProductoModel
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        [Range(0.01, 99999)]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Stock { get; set; }

        public string? ImagenUrl { get; set; }

        public ICollection<DetalleVentaModel>? DetallesVenta { get; set; }
        public ICollection<CarritoModel>? Carritos { get; set; }
    }
    
}
