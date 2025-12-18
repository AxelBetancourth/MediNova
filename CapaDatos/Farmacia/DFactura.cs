using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Farmacia
{
    /// <summary>
    /// Capa de Datos para Facturas
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DFactura : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DFactura()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TFactura> Listado()
        {
            return _unitOfWork.Repository<TFactura>().Consulta()
                .Where(f => !f.Eliminado)
                .OrderByDescending(f => f.Fecha)
                .ToList();
        }

        public List<TFactura> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TFactura>().Consulta()
                .AsNoTracking()
                .Include(f => f.Paciente)
                .Include(f => f.Venta)
                .Include(f => f.DetallesFactura)
                .Include("DetallesFactura.Medicamento")
                .Where(f => !f.Eliminado)
                .OrderByDescending(f => f.Fecha)
                .ToList();
        }

        public TFactura BuscarPorId(int ventaId)
        {
            return _unitOfWork.Repository<TFactura>().Consulta()
                .AsNoTracking()
                .Include(f => f.Paciente)
                .Include(f => f.Venta)
                .Include(f => f.DetallesFactura)
                .Include("DetallesFactura.Medicamento")
                .FirstOrDefault(f => f.VentaId == ventaId && !f.Eliminado);
        }

        public TFactura BuscarPorNumero(string numeroFactura)
        {
            return _unitOfWork.Repository<TFactura>().Consulta()
                .AsNoTracking()
                .Include(f => f.Paciente)
                .Include(f => f.DetallesFactura)
                .Include("DetallesFactura.Medicamento")
                .FirstOrDefault(f => f.NumeroFactura == numeroFactura && !f.Eliminado);
        }

        public List<TFactura> BuscarPorPaciente(int pacienteId)
        {
            return _unitOfWork.Repository<TFactura>().Consulta()
                .AsNoTracking()
                .Include(f => f.DetallesFactura)
                .Include("DetallesFactura.Medicamento")
                .Where(f => f.PacienteId == pacienteId && !f.Eliminado)
                .OrderByDescending(f => f.Fecha)
                .ToList();
        }

        public List<TFactura> BuscarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return _unitOfWork.Repository<TFactura>().Consulta()
                .AsNoTracking()
                .Include(f => f.Paciente)
                .Include(f => f.DetallesFactura)
                .Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin && !f.Eliminado)
                .OrderByDescending(f => f.Fecha)
                .ToList();
        }

        public int Agregar(TFactura factura)
        {
            try
            {
                factura.Eliminado = false;
                factura.Fecha = DateTime.Now;

                _unitOfWork.Repository<TFactura>().Agregar(factura);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DFactura.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TFactura factura)
        {
            try
            {
                var facturaExistente = _unitOfWork.Repository<TFactura>().Consulta()
                    .FirstOrDefault(f => f.VentaId == factura.VentaId);

                if (facturaExistente == null) return 0;

                facturaExistente.Subtotal = factura.Subtotal;
                facturaExistente.Descuento = factura.Descuento;
                facturaExistente.Impuesto = factura.Impuesto;
                facturaExistente.Total = factura.Total;
                facturaExistente.MetodoPago = factura.MetodoPago;
                facturaExistente.Observaciones = factura.Observaciones;

                _unitOfWork.Repository<TFactura>().Editar(facturaExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DFactura.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int ventaId)
        {
            try
            {
                var factura = _unitOfWork.Repository<TFactura>().Consulta()
                    .FirstOrDefault(f => f.VentaId == ventaId && !f.Eliminado);

                if (factura != null)
                {
                    factura.Eliminado = true;
                    _unitOfWork.Repository<TFactura>().Editar(factura);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DFactura.Eliminar: {0}", ex.Message));
                return 0;
            }
        }

        public string GenerarNumeroFactura()
        {
            try
            {
                var ultimaFactura = _unitOfWork.Repository<TFactura>().Consulta()
                    .OrderByDescending(f => f.VentaId)
                    .FirstOrDefault();

                int numeroConsecutivo = 1;
                if (ultimaFactura != null && !string.IsNullOrEmpty(ultimaFactura.NumeroFactura))
                {
                    string[] partes = ultimaFactura.NumeroFactura.Split('-');
                    if (partes.Length == 3)
                    {
                        int.TryParse(partes[2], out numeroConsecutivo);
                        numeroConsecutivo++;
                    }
                }

                return string.Format("FAC-{0}-{1:D3}", DateTime.Now.Year, numeroConsecutivo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DFactura.GenerarNumeroFactura: {0}", ex.Message));
                return string.Format("FAC-{0}-001", DateTime.Now.Year);
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
