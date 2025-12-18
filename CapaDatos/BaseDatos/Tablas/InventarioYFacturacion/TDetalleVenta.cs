using CapaDatos.BaseDatos.Tablas.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.InventarioYFacturacion
{
    public class TDetalleVenta
    {
        [Key]
        public int DetalleVentaId { get; set; }

        [Required]
        public int VentaId { get; set; }
        [ForeignKey("VentaId")]
        public TVenta Venta { get; set; }

        [Required]
        public int MedicamentoId { get; set; }
        [ForeignKey("MedicamentoId")]
        public TMedicamento Medicamento { get; set; }

        // Referencia al detalle de receta (si aplica)
        public int? DetalleRecetaId { get; set; }
        [ForeignKey("DetalleRecetaId")]
        public TDetalleReceta DetalleReceta { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        public decimal Descuento { get; set; }

        [Required]
        public decimal Subtotal { get; set; } // (PrecioUnitario * Cantidad) - Descuento

        public bool Eliminado { get; set; }
    }
}
