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
    public class TVenta
    {
        [Key]
        public int VentaId { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroVenta { get; set; } // VTA-2025-001

        [Required]
        public DateTime FechaVenta { get; set; }

        // Relación con receta (OPCIONAL - puede ser venta libre)
        public int? RecetaId { get; set; }
        [ForeignKey("RecetaId")]
        public TReceta Receta { get; set; }

        // Si tiene receta, heredamos estos datos, sino son opcionales
        public int? PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public TPaciente Paciente { get; set; }

        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public TDoctor Doctor { get; set; }

        [Required]
        public int UsuarioVentaId { get; set; }
        [ForeignKey("UsuarioVentaId")]
        public TUsuario UsuarioVenta { get; set; } // Farmacéutico que atendió

        [Required]
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }

        public decimal Impuesto { get; set; }

        [Required]
        public decimal Total { get; set; }

        // Métodos de pago
        [MaxLength(20)]
        public string MetodoPago1 { get; set; } // Efectivo, Tarjeta

        public decimal? MontoPago1 { get; set; }

        [MaxLength(20)]
        public string MetodoPago2 { get; set; } // Segundo método (opcional para pagos mixtos)

        public decimal? MontoPago2 { get; set; }

        public decimal? MontoPagado { get; set; } // Total pagado por el cliente (para calcular cambio)

        public decimal? Cambio { get; set; } // Cambio a devolver

        [Required]
        [MaxLength(20)]
        public string TipoVenta { get; set; } // Receta, Libre, Mixta

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // Completada, Cancelada, Pendiente

        [MaxLength(500)]
        public string Observaciones { get; set; }

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TDetalleVenta> DetallesVenta { get; set; }
        public TFactura Factura { get; set; }
    }
}
