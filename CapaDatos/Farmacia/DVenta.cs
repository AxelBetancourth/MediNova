using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Farmacia
{
    /// <summary>
    /// Capa de Datos para Ventas
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DVenta : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DVenta()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TVenta> Listado()
        {
            return _unitOfWork.Repository<TVenta>().Consulta()
                .Where(v => !v.Eliminado)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public List<TVenta> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TVenta>().Consulta()
                .AsNoTracking()
                .Include(v => v.Paciente)
                .Include(v => v.Doctor)
                .Include(v => v.UsuarioVenta)
                .Include(v => v.Receta)
                .Include(v => v.DetallesVenta)
                .Include(v => v.Factura)
                .Where(v => !v.Eliminado)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public TVenta BuscarPorId(int ventaId)
        {
            return _unitOfWork.Repository<TVenta>().Consulta()
                .AsNoTracking()
                .Include(v => v.Paciente)
                .Include(v => v.Doctor)
                .Include(v => v.UsuarioVenta)
                .Include(v => v.Receta)
                .Include(v => v.DetallesVenta)
                .Include(v => v.Factura)
                .FirstOrDefault(v => v.VentaId == ventaId && !v.Eliminado);
        }

        public TVenta BuscarPorNumero(string numeroVenta)
        {
            return _unitOfWork.Repository<TVenta>().Consulta()
                .AsNoTracking()
                .Include(v => v.DetallesVenta)
                .FirstOrDefault(v => v.NumeroVenta == numeroVenta && !v.Eliminado);
        }

        public List<TVenta> BuscarPorReceta(int recetaId)
        {
            return _unitOfWork.Repository<TVenta>().Consulta()
                .AsNoTracking()
                .Include(v => v.DetallesVenta)
                .Where(v => v.RecetaId == recetaId && !v.Eliminado)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public List<TVenta> BuscarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return _unitOfWork.Repository<TVenta>().Consulta()
                .AsNoTracking()
                .Include(v => v.Paciente)
                .Include(v => v.UsuarioVenta)
                .Include(v => v.DetallesVenta)
                .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin && !v.Eliminado)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public int Agregar(TVenta venta)
        {
            try
            {
                venta.Eliminado = false;
                venta.FechaVenta = DateTime.Now;
                venta.VentaId = 0;

                _unitOfWork.Repository<TVenta>().Agregar(venta);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DVenta.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TVenta venta)
        {
            try
            {
                var ventaExistente = _unitOfWork.Repository<TVenta>().Consulta()
                    .FirstOrDefault(v => v.VentaId == venta.VentaId);

                if (ventaExistente == null) return 0;

                ventaExistente.Subtotal = venta.Subtotal;
                ventaExistente.Descuento = venta.Descuento;
                ventaExistente.Impuesto = venta.Impuesto;
                ventaExistente.Total = venta.Total;
                ventaExistente.Estado = venta.Estado;
                ventaExistente.Observaciones = venta.Observaciones;

                _unitOfWork.Repository<TVenta>().Editar(ventaExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DVenta.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int ventaId)
        {
            try
            {
                var venta = _unitOfWork.Repository<TVenta>().Consulta()
                    .FirstOrDefault(v => v.VentaId == ventaId && !v.Eliminado);

                if (venta != null)
                {
                    venta.Eliminado = true;
                    venta.Estado = "Cancelada";
                    _unitOfWork.Repository<TVenta>().Editar(venta);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DVenta.Eliminar: {0}", ex.Message));
                return 0;
            }
        }

        public string GenerarNumeroVenta()
        {
            try
            {
                var ultimaVenta = _unitOfWork.Repository<TVenta>().Consulta()
                    .OrderByDescending(v => v.VentaId)
                    .FirstOrDefault();

                int numeroConsecutivo = 1;
                if (ultimaVenta != null && !string.IsNullOrEmpty(ultimaVenta.NumeroVenta))
                {
                    string[] partes = ultimaVenta.NumeroVenta.Split('-');
                    if (partes.Length == 3)
                    {
                        int.TryParse(partes[2], out numeroConsecutivo);
                        numeroConsecutivo++;
                    }
                }

                return string.Format("VTA-{0}-{1:D3}", DateTime.Now.Year, numeroConsecutivo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DVenta.GenerarNumeroVenta: {0}", ex.Message));
                return string.Format("VTA-{0}-001", DateTime.Now.Year);
            }
        }

        public void GuardarVenta(TVenta venta, List<TDetalleVenta> detalles)
        {
            _unitOfWork.ComenzarTransaccion();
            try
            {
                // Validar que hay stock suficiente antes de procesar la venta
                ValidarStockDisponible(detalles);

                // Guardar la venta primero
                venta.Eliminado = false;
                venta.FechaVenta = DateTime.Now;
                venta.VentaId = 0;

                _unitOfWork.Repository<TVenta>().Agregar(venta);
                _unitOfWork.Guardar();

                // Ahora guardar los detalles con el VentaId generado
                foreach (var detalle in detalles)
                {
                    detalle.VentaId = venta.VentaId;
                    detalle.Eliminado = false;
                    _unitOfWork.Repository<TDetalleVenta>().Agregar(detalle);
                }

                _unitOfWork.Guardar();

                // Actualizar inventario y registrar movimientos
                ActualizarInventarioPorVenta(venta, detalles);

                // Generar factura automáticamente
                GenerarFacturaParaVenta(venta, detalles);

                _unitOfWork.ConfirmarTransaccion();
            }
            catch
            {
                _unitOfWork.ReversarTransaccion();
                throw;
            }
        }

        private void GenerarFacturaParaVenta(TVenta venta, List<TDetalleVenta> detalles)
        {
            // Generar número de factura
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

            string numeroFactura = string.Format("FAC-{0}-{1:D3}", DateTime.Now.Year, numeroConsecutivo);

            // Determinar el método de pago basado en los métodos de la venta
            string metodoPago = venta.MetodoPago1;
            if (!string.IsNullOrEmpty(venta.MetodoPago2))
            {
                metodoPago = venta.MetodoPago1 + "/" + venta.MetodoPago2;
            }

            // Crear la factura (ahora soporta ventas sin paciente)
            var factura = new TFactura
            {
                VentaId = venta.VentaId,
                NumeroFactura = numeroFactura,
                Fecha = DateTime.Now,
                PacienteId = venta.PacienteId, // Puede ser null para ventas libres
                Subtotal = venta.Subtotal,
                Descuento = venta.Descuento,
                Impuesto = venta.Impuesto,
                Total = venta.Total,
                MetodoPago = metodoPago,
                Observaciones = venta.Observaciones ?? (venta.PacienteId.HasValue ? null : "Venta Libre"),
                Eliminado = false
            };

            _unitOfWork.Repository<TFactura>().Agregar(factura);
            _unitOfWork.Guardar();

            // Crear los detalles de factura a partir de los detalles de venta (medicamentos)
            foreach (var detalleVenta in detalles)
            {
                var detalleFactura = new TDetalleFactura
                {
                    FacturaId = factura.VentaId,
                    MedicamentoId = detalleVenta.MedicamentoId,
                    Cantidad = detalleVenta.Cantidad,
                    Precio = detalleVenta.PrecioUnitario,
                    Subtotal = detalleVenta.Subtotal,
                    Eliminado = false
                };

                _unitOfWork.Repository<TDetalleFactura>().Agregar(detalleFactura);
            }

            // Agregar consultas pagadas en esta venta a los detalles de factura
            var consultas = _unitOfWork.Repository<CapaDatos.BaseDatos.Tablas.ExpedienteClinico.TConsulta>()
                .Consulta()
                .Where(c => c.VentaId == venta.VentaId && !c.Eliminado)
                .ToList();

            foreach (var consulta in consultas)
            {
                var detalleFactura = new TDetalleFactura
                {
                    FacturaId = factura.VentaId,
                    MedicamentoId = null, // No es medicamento
                    Cantidad = 1,
                    Precio = consulta.CostoConsulta,
                    Subtotal = consulta.CostoConsulta,
                    Descripcion = "Consulta Médica - " + consulta.NumeroConsulta,
                    Eliminado = false
                };

                _unitOfWork.Repository<TDetalleFactura>().Agregar(detalleFactura);
            }

            // Agregar exámenes pagados en esta venta a los detalles de factura
            var examenes = _unitOfWork.Repository<CapaDatos.BaseDatos.Tablas.Catalogos.TExamen>()
                .Consulta()
                .Where(ex => ex.VentaId == venta.VentaId && !ex.Eliminado)
                .ToList();

            foreach (var examen in examenes)
            {
                var detalleFactura = new TDetalleFactura
                {
                    FacturaId = factura.VentaId,
                    MedicamentoId = null, // No es medicamento
                    Cantidad = 1,
                    Precio = examen.Costo,
                    Subtotal = examen.Costo,
                    Descripcion = "Examen: " + examen.Nombre + " (" + examen.Tipo + ")",
                    Eliminado = false
                };

                _unitOfWork.Repository<TDetalleFactura>().Agregar(detalleFactura);
            }

            _unitOfWork.Guardar();
        }

        public List<TVenta> BuscarPorPaciente(int pacienteId)
        {
            return _unitOfWork.Repository<TVenta>().Consulta()
                .AsNoTracking()
                .Include(v => v.DetallesVenta)
                .Include(v => v.UsuarioVenta)
                .Where(v => v.PacienteId == pacienteId && !v.Eliminado)
                .OrderByDescending(v => v.FechaVenta)
                .ToList();
        }

        public List<TDetalleVenta> ObtenerDetallesVenta(int ventaId)
        {
            return _unitOfWork.Repository<TDetalleVenta>().Consulta()
                .AsNoTracking()
                .Include(d => d.Medicamento)
                .Where(d => d.VentaId == ventaId && !d.Eliminado)
                .ToList();
        }


        private void ValidarStockDisponible(List<TDetalleVenta> detalles)
        {
            foreach (var detalle in detalles)
            {
                var medicamento = _unitOfWork.Repository<TMedicamento>().Consulta()
                    .FirstOrDefault(m => m.MedicamentoId == detalle.MedicamentoId && !m.Eliminado);

                if (medicamento == null)
                {
                    throw new Exception(string.Format("El medicamento con ID {0} no existe.", detalle.MedicamentoId));
                }

                if (medicamento.Stock < detalle.Cantidad)
                {
                    throw new Exception(string.Format("Stock insuficiente para {0}. Disponible: {1}, Solicitado: {2}", medicamento.Nombre, medicamento.Stock, detalle.Cantidad));
                }
            }
        }

        private void ActualizarInventarioPorVenta(TVenta venta, List<TDetalleVenta> detalles)
        {
            foreach (var detalle in detalles)
            {
                // Obtener el medicamento actual
                var medicamento = _unitOfWork.Repository<TMedicamento>().Consulta()
                    .FirstOrDefault(m => m.MedicamentoId == detalle.MedicamentoId && !m.Eliminado);

                if (medicamento == null)
                {
                    throw new Exception(string.Format("El medicamento con ID {0} no existe.", detalle.MedicamentoId));
                }

                int stockAnterior = medicamento.Stock;
                int stockNuevo = stockAnterior - detalle.Cantidad;

                // Actualizar el stock del medicamento
                medicamento.Stock = stockNuevo;
                _unitOfWork.Repository<TMedicamento>().Editar(medicamento);

                // Registrar el movimiento de inventario
                var movimiento = new TInventarioMovimiento
                {
                    MedicamentoId = medicamento.MedicamentoId,
                    Fecha = DateTime.Now,
                    Cantidad = detalle.Cantidad,
                    TipoMovimiento = "Salida",
                    StockAnterior = stockAnterior,
                    StockNuevo = stockNuevo,
                    Motivo = "Venta " + venta.NumeroVenta,
                    UsuarioRegistro = venta.UsuarioVenta != null ? venta.UsuarioVenta.NombreUsuario : "Sistema",
                    Eliminado = false
                };

                _unitOfWork.Repository<TInventarioMovimiento>().Agregar(movimiento);
            }

            _unitOfWork.Guardar();
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
