using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.Catalogos
{
    public class TEnfermedad
    {
        [Key]
        public int EnfermedadId { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(1000)]
        public string Sintomas { get; set; }

        [MaxLength(1000)]
        public string Tratamiento { get; set; }

        [MaxLength(100)]
        public string Tipo { get; set; }

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TDiagnostico> Diagnosticos { get; set; }
    }
}
