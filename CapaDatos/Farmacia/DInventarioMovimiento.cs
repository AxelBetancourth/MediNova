using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Farmacia
{
    /// <summary>
    /// Capa de Datos para Movimientos de Inventario
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DInventarioMovimiento : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DInventarioMovimiento()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TInventarioMovimiento> Listado()
        {
            return _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                .Where(m => !m.Eliminado)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        public List<TInventarioMovimiento> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                .AsNoTracking()
                .Include(m => m.Medicamento)
                .Where(m => !m.Eliminado)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        public TInventarioMovimiento BuscarPorId(int movimientoId)
        {
            return _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                .AsNoTracking()
                .Include(m => m.Medicamento)
                .FirstOrDefault(m => m.MovimientoId == movimientoId && !m.Eliminado);
        }

        public List<TInventarioMovimiento> BuscarPorMedicamento(int medicamentoId)
        {
            return _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                .AsNoTracking()
                .Where(m => m.MedicamentoId == medicamentoId && !m.Eliminado)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        public List<TInventarioMovimiento> BuscarPorTipo(string tipoMovimiento)
        {
            return _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                .AsNoTracking()
                .Include(m => m.Medicamento)
                .Where(m => m.TipoMovimiento == tipoMovimiento && !m.Eliminado)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        public List<TInventarioMovimiento> BuscarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                .AsNoTracking()
                .Include(m => m.Medicamento)
                .Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFin && !m.Eliminado)
                .OrderByDescending(m => m.Fecha)
                .ToList();
        }

        public int Agregar(TInventarioMovimiento movimiento)
        {
            try
            {
                movimiento.Eliminado = false;
                movimiento.Fecha = DateTime.Now;
                movimiento.MovimientoId = 0;

                _unitOfWork.Repository<TInventarioMovimiento>().Agregar(movimiento);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DInventarioMovimiento.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TInventarioMovimiento movimiento)
        {
            try
            {
                var movimientoExistente = _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                    .FirstOrDefault(m => m.MovimientoId == movimiento.MovimientoId);

                if (movimientoExistente == null) return 0;

                movimientoExistente.Cantidad = movimiento.Cantidad;
                movimientoExistente.TipoMovimiento = movimiento.TipoMovimiento;
                movimientoExistente.Motivo = movimiento.Motivo;
                movimientoExistente.UsuarioRegistro = movimiento.UsuarioRegistro;

                _unitOfWork.Repository<TInventarioMovimiento>().Editar(movimientoExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DInventarioMovimiento.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int movimientoId)
        {
            try
            {
                var movimiento = _unitOfWork.Repository<TInventarioMovimiento>().Consulta()
                    .FirstOrDefault(m => m.MovimientoId == movimientoId && !m.Eliminado);

                if (movimiento != null)
                {
                    movimiento.Eliminado = true;
                    _unitOfWork.Repository<TInventarioMovimiento>().Editar(movimiento);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DInventarioMovimiento.Eliminar: {0}", ex.Message));
                return 0;
            }
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
