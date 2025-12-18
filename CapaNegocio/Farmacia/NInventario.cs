using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Farmacia;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Farmacia
{
    public class NInventario : IDisposable
    {
        private DInventarioMovimiento dInventarioMovimiento;

        public NInventario()
        {
            dInventarioMovimiento = new DInventarioMovimiento();
        }

        public List<TInventarioMovimiento> ListarMovimientos()
        {
            try
            {
                return dInventarioMovimiento.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar movimientos de inventario: {0}", ex.Message), ex);
            }
        }

        public TInventarioMovimiento BuscarPorId(int movimientoId)
        {
            try
            {
                if (movimientoId <= 0)
                    throw new Exception("ID de movimiento no válido.");

                return dInventarioMovimiento.BuscarPorId(movimientoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar movimiento: {0}", ex.Message), ex);
            }
        }

        public List<TInventarioMovimiento> BuscarPorMedicamento(int medicamentoId)
        {
            try
            {
                if (medicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                return dInventarioMovimiento.BuscarPorMedicamento(medicamentoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar movimientos del medicamento: {0}", ex.Message), ex);
            }
        }

        public List<TInventarioMovimiento> BuscarPorTipo(string tipoMovimiento)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipoMovimiento))
                    throw new Exception("El tipo de movimiento no puede estar vacío.");

                return dInventarioMovimiento.BuscarPorTipo(tipoMovimiento);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar movimientos por tipo: {0}", ex.Message), ex);
            }
        }

        public List<TInventarioMovimiento> BuscarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (fechaInicio > fechaFin)
                    throw new Exception("La fecha de inicio no puede ser mayor a la fecha fin.");

                return dInventarioMovimiento.BuscarPorFecha(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar movimientos por fecha: {0}", ex.Message), ex);
            }
        }

        public int RegistrarEntrada(int medicamentoId, int cantidad, int stockAnterior, int stockNuevo, string motivo, string usuarioRegistro)
        {
            try
            {
                if (medicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                if (cantidad <= 0)
                    throw new Exception("La cantidad debe ser mayor a cero.");

                var movimiento = new TInventarioMovimiento
                {
                    MedicamentoId = medicamentoId,
                    Cantidad = cantidad,
                    TipoMovimiento = "Entrada",
                    StockAnterior = stockAnterior,
                    StockNuevo = stockNuevo,
                    Motivo = motivo,
                    UsuarioRegistro = usuarioRegistro,
                    Fecha = DateTime.Now,
                    Eliminado = false
                };

                return dInventarioMovimiento.Agregar(movimiento);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar entrada de inventario: {0}", ex.Message), ex);
            }
        }

        public int RegistrarSalida(int medicamentoId, int cantidad, int stockAnterior, int stockNuevo, string motivo, string usuarioRegistro)
        {
            try
            {
                if (medicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                if (cantidad <= 0)
                    throw new Exception("La cantidad debe ser mayor a cero.");

                var movimiento = new TInventarioMovimiento
                {
                    MedicamentoId = medicamentoId,
                    Cantidad = cantidad,
                    TipoMovimiento = "Salida",
                    StockAnterior = stockAnterior,
                    StockNuevo = stockNuevo,
                    Motivo = motivo,
                    UsuarioRegistro = usuarioRegistro,
                    Fecha = DateTime.Now,
                    Eliminado = false
                };

                return dInventarioMovimiento.Agregar(movimiento);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar salida de inventario: {0}", ex.Message), ex);
            }
        }

        public int RegistrarAjuste(int medicamentoId, int cantidad, string motivo, string usuarioRegistro)
        {
            try
            {
                if (medicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                if (cantidad == 0)
                    throw new Exception("La cantidad de ajuste no puede ser cero.");

                var movimiento = new TInventarioMovimiento
                {
                    MedicamentoId = medicamentoId,
                    Cantidad = cantidad,
                    TipoMovimiento = "Ajuste",
                    Motivo = motivo ?? "Ajuste de inventario",
                    UsuarioRegistro = usuarioRegistro,
                    Fecha = DateTime.Now,
                    Eliminado = false
                };

                return dInventarioMovimiento.Agregar(movimiento);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar ajuste de inventario: {0}", ex.Message), ex);
            }
        }

        public int EliminarMovimiento(int movimientoId)
        {
            try
            {
                if (movimientoId <= 0)
                    throw new Exception("ID de movimiento no válido.");

                var movimiento = dInventarioMovimiento.BuscarPorId(movimientoId);
                if (movimiento == null)
                    throw new Exception("Movimiento no encontrado.");

                return dInventarioMovimiento.Eliminar(movimientoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar movimiento: {0}", ex.Message), ex);
            }
        }

        public void Dispose()
        {
            if (dInventarioMovimiento != null)
            {
                dInventarioMovimiento.Dispose();
            }
        }
    }
}
