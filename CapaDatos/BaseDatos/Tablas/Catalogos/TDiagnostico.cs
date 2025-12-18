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
    public class TDiagnostico
    {
        [Key]
        public int DiagnosticoId { get; set; }

        [Required]
        public int ExpedienteId { get; set; }
        [ForeignKey("ExpedienteId")]
        public TExpediente Expediente { get; set; }

        [Required]
        public int EnfermedadId { get; set; }
        [ForeignKey("EnfermedadId")]
        public TEnfermedad Enfermedad { get; set; }

        public int? ConsultaId { get; set; } // Consulta donde se diagnosticó
        [ForeignKey("ConsultaId")]
        public TConsulta Consulta { get; set; }

        [Required]
        public DateTime FechaDiagnostico { get; set; }

        public string Observaciones { get; set; }

        [MaxLength(20)]
        public string Estado { get; set; } // Activo, Controlado, Curado

        public bool Eliminado { get; set; }
    }
}
