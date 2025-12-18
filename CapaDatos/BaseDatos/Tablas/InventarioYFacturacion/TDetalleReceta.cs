using CapaDatos.BaseDatos.Tablas.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos.Tablas.InventarioYFacturacion
{
    public class TDetalleReceta
    {
        [Key]
        public int DetalleRecetaId { get; set; }

        [Required]
        public int RecetaId { get; set; }
        [ForeignKey("RecetaId")]
        public TReceta Receta { get; set; }

        // Nullable para permitir medicamentos externos (no en inventario)
        public int? MedicamentoId { get; set; }
        [ForeignKey("MedicamentoId")]
        public TMedicamento Medicamento { get; set; }

        // Nombre del medicamento externo (cuando MedicamentoId es null)
        [MaxLength(300)]
        public string NombreMedicamentoExterno { get; set; }

        [Required]
        public int CantidadPrescrita { get; set; }

        [Required]
        [MaxLength(200)]
        public string Dosis { get; set; } // "1 tableta cada 8 horas"

        [Required]
        public int DuracionDias { get; set; } // Duración del tratamiento

        [MaxLength(500)]
        public string Indicaciones { get; set; } // "Tomar con alimentos"

        public int CantidadSurtida { get; set; } // Cantidad ya entregada

        public bool Eliminado { get; set; }
    }
}
