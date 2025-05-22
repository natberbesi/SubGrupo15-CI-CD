using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using TiendaOnline.Models.Domian;
namespace TiendaOnline.Data
{
    public class TiendaOnlineContext: DbContext
    {
        public TiendaOnlineContext(DbContextOptions<TiendaOnlineContext> options)
            : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProductoModel> Productos { get; set; }
        public DbSet<VentaModel> Ventas { get; set; }
        public DbSet<DetalleVentaModel> DetallesVenta { get; set; }
        public DbSet<CarritoModel> Carritos { get; set; }
    }
}
