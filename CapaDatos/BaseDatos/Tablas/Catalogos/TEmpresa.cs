using System;
using System.ComponentModel.DataAnnotations;

namespace CapaDatos.BaseDatos.Tablas.Catalogos
{
    public class TEmpresa
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required, MaxLength(200)]
        public string NombreEmpresa { get; set; }

        [MaxLength(20)]
        public string RTN { get; set; } // Registro Tributario Nacional (Honduras)

        [MaxLength(300)]
        public string Direccion { get; set; }

        [MaxLength(50)]
        public string Telefono { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string SitioWeb { get; set; }

        public byte[] Logo { get; set; } // Logo de la empresa en formato de imagen

        [MaxLength(500)]
        public string Slogan { get; set; }

        [MaxLength(100)]
        public string RepresentanteLegal { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public bool Eliminado { get; set; }
    }
}
