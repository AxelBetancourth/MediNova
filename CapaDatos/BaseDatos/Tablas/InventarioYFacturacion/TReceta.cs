using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.InventarioYFacturacion
{
    public class TReceta
    {
        [Key]
        public int RecetaId { get; set; }

        [Required]
        public int PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public TPaciente Paciente { get; set; }

        [Required]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public TDoctor Doctor { get; set; }

        public int? CitaId { get; set; }
        [ForeignKey("CitaId")]
        public TCita Cita { get; set; }

        // Relación con Consulta (la consulta médica donde se recetó)
        public int? ConsultaId { get; set; }
        [ForeignKey("ConsultaId")]
        public TConsulta Consulta { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroReceta { get; set; } // RX-2025-001

        [Required]
        public DateTime FechaEmision { get; set; }

        public DateTime? FechaVencimiento { get; set; } // Las recetas pueden vencer

        [MaxLength(500)]
        public string Diagnostico { get; set; }

        [MaxLength(1000)]
        public string IndicacionesGenerales { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // Pendiente, Surtida, Parcial, Vencida, Cancelada

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TDetalleReceta> DetallesReceta { get; set; }
        public ICollection<TVenta> Ventas { get; set; }
    }
}
