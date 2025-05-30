﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models.Domian
{
    public class VentaModel
    {
        [Key]
        public int VentaId { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        public decimal Total { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuarioModel? Usuario { get; set; }

        public ICollection<DetalleVentaModel>? Detalles { get; set; }
    }

}
