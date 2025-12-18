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
    public class TFactura
    {
        [Key]
        [ForeignKey("Venta")]
        public int VentaId { get; set; }  // Ahora es PK y FK al mismo tiempo

        [ForeignKey("VentaId")]
        public TVenta Venta { get; set; }

        [Required]
        [MaxLength(100)]
        public string NumeroFactura { get; set; } // FAC-2025-001

        [Required]
        public DateTime Fecha { get; set; }

        public int? PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public TPaciente Paciente { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        public decimal Descuento { get; set; }

        public decimal Impuesto { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(50)]
        public string MetodoPago { get; set; } // Efectivo, Tarjeta, Transferencia

        [MaxLength(500)]
        public string Observaciones { get; set; }

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TDetalleFactura> DetallesFactura { get; set; }
    }
}
