using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.ControlCitas
{
    public class TCita
    {
        [Key]
        public int CitaId { get; set; }
        [Required]
        public int PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public TPaciente Paciente { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public TDoctor Doctor { get; set; }

        [Required]
        [MaxLength(255)]
        public string Asunto { get; set; }

        [Required]
        public DateTime FechaHoraInicio { get; set; }

        [Required]
        public DateTime FechaHoraFin { get; set; }

        public bool TodoElDia { get; set; }

        [MaxLength(100)]
        public string Ubicacion { get; set; }

        [Required, MaxLength(20)]
        public string Estado { get; set; }


        public bool Eliminado { get; set; }

        [MaxLength(500)]
        public string ReglaRecurrencia { get; set; }

        public int? IdCitaPadre { get; set; }

        // Navegación
        public ICollection<TExamen> Examenes { get; set; }
    }
}
