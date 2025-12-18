using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.Catalogos
{
    public class TMedicamento
    {
        [Key]
        public int MedicamentoId { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string Presentacion { get; set; }

        [MaxLength(100)]
        public string Dosis { get; set; }

        [MaxLength(100)]
        public string Proveedor { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TDetalleFactura> DetalleFacturas { get; set; }
        public ICollection<TInventarioMovimiento> MovimientosInventario { get; set; }
    }
}
