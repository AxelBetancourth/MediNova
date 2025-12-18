using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Farmacia;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Farmacia
{
    public class NFactura : IDisposable
    {
        private DFactura dFactura;

        public NFactura()
        {
            dFactura = new DFactura();
        }

        public List<TFactura> ListarFacturas()
        {
            try
            {
                return dFactura.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar facturas: {0}", ex.Message), ex);
            }
        }

        public TFactura BuscarPorId(int ventaId)
        {
            try
            {
                if (ventaId <= 0)
                    throw new Exception("ID de factura no válido.");

                return dFactura.BuscarPorId(ventaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar factura: {0}", ex.Message), ex);
            }
        }

        public TFactura BuscarPorNumero(string numeroFactura)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroFactura))
                    throw new Exception("El número de factura no puede estar vacío.");

                return dFactura.BuscarPorNumero(numeroFactura);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar factura: {0}", ex.Message), ex);
            }
        }

        public List<TFactura> BuscarPorPaciente(int pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                    throw new Exception("ID de paciente no válido.");

                return dFactura.BuscarPorPaciente(pacienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar facturas del paciente: {0}", ex.Message), ex);
            }
        }

        public List<TFactura> BuscarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (fechaInicio > fechaFin)
                    throw new Exception("La fecha de inicio no puede ser mayor a la fecha fin.");

                return dFactura.BuscarPorFecha(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar facturas por fecha: {0}", ex.Message), ex);
            }
        }

        public int GenerarFactura(TFactura factura)
        {
            try
            {
                ValidarDatosFactura(factura);

                factura.NumeroFactura = dFactura.GenerarNumeroFactura();
                factura.Fecha = DateTime.Now;
                factura.Eliminado = false;

                return dFactura.Agregar(factura);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al generar factura: {0}", ex.Message), ex);
            }
        }

        public int EditarFactura(TFactura factura)
        {
            try
            {
                if (factura.VentaId <= 0)
                    throw new Exception("ID de factura no válido.");

                ValidarDatosFactura(factura);

                var facturaExistente = dFactura.BuscarPorId(factura.VentaId);
                if (facturaExistente == null)
                    throw new Exception("Factura no encontrada.");

                return dFactura.Editar(factura);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar factura: {0}", ex.Message), ex);
            }
        }

        public int EliminarFactura(int ventaId)
        {
            try
            {
                if (ventaId <= 0)
                    throw new Exception("ID de factura no válido.");

                var factura = dFactura.BuscarPorId(ventaId);
                if (factura == null)
                    throw new Exception("Factura no encontrada.");

                return dFactura.Eliminar(ventaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar factura: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosFactura(TFactura factura)
        {
            if (factura == null)
                throw new Exception("La factura no puede ser nula.");

            if (factura.PacienteId <= 0)
                throw new Exception("Debe asociar la factura a un paciente.");

            if (factura.Subtotal < 0)
                throw new Exception("El subtotal no puede ser negativo.");

            if (factura.Descuento < 0)
                throw new Exception("El descuento no puede ser negativo.");

            if (factura.Impuesto < 0)
                throw new Exception("El impuesto no puede ser negativo.");

            if (factura.Total <= 0)
                throw new Exception("El total debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(factura.MetodoPago))
                throw new Exception("El método de pago es obligatorio.");
        }

        public void Dispose()
        {
            if (dFactura != null)
            {
                dFactura.Dispose();
            }
        }
    }
}
