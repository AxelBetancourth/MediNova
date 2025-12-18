using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.ExpedienteClinico
{
    public class TExpediente
    {
        [Key]
        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }
        public TPaciente Paciente { get; set; }

        [Required]
        public DateTime FechaApertura { get; set; }

        [MaxLength(50)]
        public string NumeroExpediente { get; set; } // EXP-2024-001

        // Datos generales del paciente (encabezado del expediente)
        public string Alergias { get; set; }

        public string AntecedentesFamiliares { get; set; }

        public string AntecedentesPersonales { get; set; }

        [MaxLength(20)]
        public string TipoSangre { get; set; }

        public string NotasGenerales { get; set; }

        [MaxLength(100)]
        public string ContactoEmergencia { get; set; }

        [MaxLength(20)]
        public string TelefonoEmergencia { get; set; }

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TConsulta> Consultas { get; set; }
        public ICollection<TDiagnostico> Diagnosticos { get; set; }
        public ICollection<TExamen> Examenes { get; set; }
    }
}
