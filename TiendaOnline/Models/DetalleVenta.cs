using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models
{
    public class DetalleVenta
    {
        [Key]
    public int DetalleVentaId { get; set; }

    [Required]
    public int VentaId { get; set; }

    [ForeignKey("VentaId")]
    public Venta? Venta { get; set; }

    [Required]
    public int ProductoId { get; set; }

    [ForeignKey("ProductoId")]
    public Producto? Producto { get; set; }

    [Required]
    [Range(1, 1000)]
    public int Cantidad { get; set; }

    [Required]
    [Range(0.01, 99999)]
    public decimal PrecioUnitario { get; set; }
    }
}
