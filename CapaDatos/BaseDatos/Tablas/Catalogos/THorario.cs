using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.Catalogos
{
    public class THorario
    {
        [Key]
        public int HorarioDoctorId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public TDoctor Doctor { get; set; }

        [Required]
        public DayOfWeek DiaSemana { get; set; } // Lunes = 1, Domingo = 0

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }
    }
}
