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
    public class TInventarioMovimiento
    {
        [Key]
        public int MovimientoId { get; set; }

        [Required]
        public int MedicamentoId { get; set; }

        [ForeignKey("MedicamentoId")]
        public TMedicamento Medicamento { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required, MaxLength(20)]
        public string TipoMovimiento { get; set; }

        public int StockAnterior { get; set; }

        public int StockNuevo { get; set; }

        [MaxLength(200)]
        public string Motivo { get; set; }

        [MaxLength(100)]
        public string UsuarioRegistro { get; set; }

        public bool Eliminado { get; set; }
    }
}
