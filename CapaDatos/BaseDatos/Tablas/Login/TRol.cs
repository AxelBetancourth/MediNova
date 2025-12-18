using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.Login
{
    public class TRol
    {
        [Key]
        public int RolId { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }

        // Navegación
        public ICollection<TUsuario> Usuarios { get; set; }
    }
}
