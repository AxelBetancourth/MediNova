using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Farmacia;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Farmacia
{
    public class NVenta : IDisposable
    {
        private DVenta dVenta;

        public NVenta()
        {
            dVenta = new DVenta();
        }

        public List<TVenta> ListarVentas()
        {
            try
            {
                return dVenta.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar ventas: {0}", ex.Message), ex);
            }
        }

        public TVenta BuscarPorId(int ventaId)
        {
            try
            {
                if (ventaId <= 0)
                    throw new Exception("ID de venta no válido.");

                return dVenta.BuscarPorId(ventaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar venta: {0}", ex.Message), ex);
            }
        }

        public TVenta BuscarPorNumero(string numeroVenta)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroVenta))
                    throw new Exception("El número de venta no puede estar vacío.");

                return dVenta.BuscarPorNumero(numeroVenta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar venta: {0}", ex.Message), ex);
            }
        }

        public List<TVenta> BuscarPorReceta(int recetaId)
        {
            try
            {
                if (recetaId <= 0)
                    throw new Exception("ID de receta no válido.");

                return dVenta.BuscarPorReceta(recetaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar ventas por receta: {0}", ex.Message), ex);
            }
        }

        public List<TVenta> BuscarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (fechaInicio > fechaFin)
                    throw new Exception("La fecha de inicio no puede ser mayor a la fecha fin.");

                return dVenta.BuscarPorFecha(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar ventas por fecha: {0}", ex.Message), ex);
            }
        }

        public int RegistrarVenta(TVenta venta)
        {
            try
            {
                ValidarDatosVenta(venta);

                venta.NumeroVenta = dVenta.GenerarNumeroVenta();
                venta.FechaVenta = DateTime.Now;
                venta.Estado = "Completada";
                venta.Eliminado = false;

                return dVenta.Agregar(venta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar venta: {0}", ex.Message), ex);
            }
        }

        public int EditarVenta(TVenta venta)
        {
            try
            {
                if (venta.VentaId <= 0)
                    throw new Exception("ID de venta no válido.");

                ValidarDatosVenta(venta);

                var ventaExistente = dVenta.BuscarPorId(venta.VentaId);
                if (ventaExistente == null)
                    throw new Exception("Venta no encontrada.");

                return dVenta.Editar(venta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar venta: {0}", ex.Message), ex);
            }
        }

        public int CancelarVenta(int ventaId)
        {
            try
            {
                if (ventaId <= 0)
                    throw new Exception("ID de venta no válido.");

                var venta = dVenta.BuscarPorId(ventaId);
                if (venta == null)
                    throw new Exception("Venta no encontrada.");

                return dVenta.Eliminar(ventaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al cancelar venta: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosVenta(TVenta venta)
        {
            if (venta == null)
                throw new Exception("La venta no puede ser nula.");

            if (venta.UsuarioVentaId <= 0)
                throw new Exception("Debe especificar el usuario que realiza la venta.");

            if (venta.Subtotal < 0)
                throw new Exception("El subtotal no puede ser negativo.");

            if (venta.Descuento < 0)
                throw new Exception("El descuento no puede ser negativo.");

            if (venta.Impuesto < 0)
                throw new Exception("El impuesto no puede ser negativo.");

            if (venta.Total <= 0)
                throw new Exception("El total debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(venta.TipoVenta))
                throw new Exception("El tipo de venta es obligatorio.");

            if (string.IsNullOrWhiteSpace(venta.Estado))
                throw new Exception("El estado de la venta es obligatorio.");
        }

        public void GuardarVenta(TVenta venta, List<TDetalleVenta> detalles)
        {
            try
            {
                ValidarDatosVenta(venta);

                if (detalles == null || detalles.Count == 0)
                    throw new Exception("Debe agregar al menos un medicamento a la venta.");

                venta.NumeroVenta = dVenta.GenerarNumeroVenta();
                venta.FechaVenta = DateTime.Now;
                venta.Estado = "Completada";
                venta.Eliminado = false;

                dVenta.GuardarVenta(venta, detalles);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al guardar venta: {0}", ex.Message), ex);
            }
        }

        public List<TVenta> BuscarPorPaciente(int pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                    throw new Exception("ID de paciente no válido.");

                return dVenta.BuscarPorPaciente(pacienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar ventas del paciente: {0}", ex.Message), ex);
            }
        }

        public List<TDetalleVenta> ObtenerDetallesVenta(int ventaId)
        {
            try
            {
                if (ventaId <= 0)
                    throw new Exception("ID de venta no válido.");

                return dVenta.ObtenerDetallesVenta(ventaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener detalles de venta: {0}", ex.Message), ex);
            }
        }


        public void Dispose()
        {
            if (dVenta != null)
            {
                dVenta.Dispose();
            }
        }
    }
}
