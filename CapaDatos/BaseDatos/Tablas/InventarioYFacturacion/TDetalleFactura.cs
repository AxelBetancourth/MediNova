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
    public class TDetalleFactura
    {
        [Key]
        public int DetalleId { get; set; }

        [Required]
        public int FacturaId { get; set; }  // Ahora referencia a VentaId

        [ForeignKey("FacturaId")]
        public TFactura Factura { get; set; }

        // MedicamentoId es nullable para soportar consultas y exámenes
        public int? MedicamentoId { get; set; }
        [ForeignKey("MedicamentoId")]
        public TMedicamento Medicamento { get; set; }

        // Descripción para servicios que no son medicamentos (consultas, exámenes)
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        public bool Eliminado { get; set; }
    }
}
