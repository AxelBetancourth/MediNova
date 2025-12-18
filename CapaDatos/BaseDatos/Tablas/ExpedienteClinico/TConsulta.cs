using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.ExpedienteClinico
{
    public class TConsulta
    {
        [Key]
        public int ConsultaId { get; set; }

        // Relación con Expediente (cada consulta pertenece a un expediente)
        [Required]
        public int ExpedienteId { get; set; }
        [ForeignKey("ExpedienteId")]
        public TExpediente Expediente { get; set; }

        // Relación con Cita (puede o no estar asociada a una cita agendada)
        public int? CitaId { get; set; }
        [ForeignKey("CitaId")]
        public TCita Cita { get; set; }

        // Doctor que atendió esta consulta específica
        [Required]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public TDoctor Doctor { get; set; }

        // Información básica de la consulta
        [Required]
        [MaxLength(50)]
        public string NumeroConsulta { get; set; } // CON-2025-001

        [Required]
        public DateTime FechaConsulta { get; set; }

        [Required]
        [MaxLength(500)]
        public string MotivoConsulta { get; set; }

        [MaxLength(2000)]
        public string Sintomas { get; set; }

        // Signos vitales
        [MaxLength(20)]
        public string PresionArterial { get; set; } // 120/80

        public decimal? Temperatura { get; set; } // °C

        public decimal? FrecuenciaCardiaca { get; set; } // ppm

        public decimal? FrecuenciaRespiratoria { get; set; } // rpm

        public decimal? Peso { get; set; } // kg

        public decimal? Altura { get; set; } // cm

        public decimal? IMC { get; set; } // Índice de Masa Corporal

        [MaxLength(20)]
        public string Saturacion { get; set; } // % SpO2

        // Diagnóstico y tratamiento
        [MaxLength(2000)]
        public string Diagnostico { get; set; }

        // Relación con Enfermedad (opcional, si se identifica una enfermedad específica)
        public int? EnfermedadId { get; set; }
        [ForeignKey("EnfermedadId")]
        public TEnfermedad Enfermedad { get; set; }

        [MaxLength(2000)]
        public string Tratamiento { get; set; }

        [MaxLength(2000)]
        public string Observaciones { get; set; }

        [MaxLength(2000)]
        public string IndicacionesMedicas { get; set; }

        // Costo de la consulta
        [Required]
        public decimal CostoConsulta { get; set; }

        [Required]
        [MaxLength(20)]
        public string EstadoPago { get; set; } // Pendiente, Pagado, Cancelado

        public DateTime? FechaPago { get; set; }

        // Relación con Venta (cuando se paga la consulta)
        public int? VentaId { get; set; }
        [ForeignKey("VentaId")]
        public TVenta Venta { get; set; }

        // Próxima cita (seguimiento)
        public DateTime? ProximaCita { get; set; }

        [MaxLength(500)]
        public string NotasProximaCita { get; set; }

        // Estado de la consulta
        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // EnProgreso, Finalizada, Cancelada

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TReceta> Recetas { get; set; }
        public ICollection<TDiagnostico> Diagnosticos { get; set; }
    }
}
