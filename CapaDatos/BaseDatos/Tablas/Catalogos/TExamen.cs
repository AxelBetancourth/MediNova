using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.Catalogos
{
    public class TExamen
    {
        [Key]
        public int ExamenId { get; set; }
        [Required]
        public int ExpedienteId { get; set; }
        [ForeignKey("ExpedienteId")]
        public TExpediente Expediente { get; set; }
        public int? CitaId { get; set; }
        [ForeignKey("CitaId")]
        public TCita Cita { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; }
        [Required, MaxLength(100)]
        public string Tipo { get; set; } // Sangre, Orina, Rayos X, etc.
        [Required]
        public decimal Costo { get; set; }
        [Required]
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaResultado { get; set; }
        public string Resultado { get; set; }
        [MaxLength(50)]
        public string Estado { get; set; } // Solicitado, EnProceso, Completado, Pagado
        public bool EsExterno { get; set; } // Si se realiza fuera de MediNova
        [MaxLength(200)]
        public string LugarRealizacion { get; set; } // Laboratorio o lugar donde se realizará

        // Relación con Venta (cuando se paga el examen)
        public int? VentaId { get; set; }
        [ForeignKey("VentaId")]
        public CapaDatos.BaseDatos.Tablas.InventarioYFacturacion.TVenta Venta { get; set; }

        public bool Eliminado { get; set; }
    }
}
