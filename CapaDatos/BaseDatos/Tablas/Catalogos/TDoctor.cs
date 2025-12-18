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
    public class TDoctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required, MaxLength(100)]
        public string NombreCompleto { get; set; }

        [Required, MaxLength(50)]
        public string Especialidad { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(100)]
        public string Correo { get; set; }

        public bool Disponible { get; set; }

        public bool Eliminado { get; set; }
        // 🔹 Nueva relación opcional con Usuario (solo para rol Medico)
        public int? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public TUsuario Usuario { get; set; }

        // Navegación
        public ICollection<TCita> Citas { get; set; }
        public ICollection<TExpediente> Expedientes { get; set; }
        public ICollection<THorario> Horarios { get; set; }
    }
}
