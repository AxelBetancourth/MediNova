using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas
{
    public class TUsuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required, MaxLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        public string Contrasena { get; set; }

        public byte[] HuellaDactilar { get; set; }

        [Required]
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public TRol Rol { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        public bool Estado { get; set; }
        public bool Eliminado { get; set; }

    }

}
