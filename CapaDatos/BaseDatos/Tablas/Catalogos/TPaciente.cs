using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.Catalogos
{
    public class TPaciente
    {
        [Key]
        public int PacienteId { get; set; }

        [Required, MaxLength(15)]
        public string DNI { get; set; }

        [Required, MaxLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(10)]
        public string Sexo { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(200)]
        public string Direccion { get; set; }

        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TCita> Citas { get; set; }
        public ICollection<TFactura> Facturas { get; set; }


        // 🔹 Relación 1:1 con Expediente
        public TExpediente Expediente { get; set; }
    }
}
