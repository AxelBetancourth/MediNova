using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Farmacia
{
    /// <summary>
    /// Capa de Datos para Recetas Médicas
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DReceta : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DReceta()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TReceta> Listado()
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .Where(r => !r.Eliminado)
                .OrderByDescending(r => r.FechaEmision)
                .ToList();
        }

        public List<TReceta> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .AsNoTracking()
                .Include(r => r.Paciente)
                .Include(r => r.Doctor)
                .Include(r => r.Cita)
                .Include(r => r.DetallesReceta)
                .Where(r => !r.Eliminado)
                .OrderByDescending(r => r.FechaEmision)
                .ToList();
        }

        public TReceta BuscarPorId(int recetaId)
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .AsNoTracking()
                .Include(r => r.Paciente)
                .Include(r => r.Doctor)
                .Include(r => r.Cita)
                .Include(r => r.DetallesReceta.Select(d => d.Medicamento))
                .FirstOrDefault(r => r.RecetaId == recetaId && !r.Eliminado);
        }

        public TReceta BuscarPorNumero(string numeroReceta)
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .AsNoTracking()
                .Include(r => r.Paciente)
                .Include(r => r.Doctor)
                .Include(r => r.DetallesReceta)
                .FirstOrDefault(r => r.NumeroReceta == numeroReceta && !r.Eliminado);
        }

        public List<TReceta> BuscarPorPaciente(int pacienteId)
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .AsNoTracking()
                .Include(r => r.Doctor)
                .Include(r => r.DetallesReceta)
                .Where(r => r.PacienteId == pacienteId && !r.Eliminado)
                .OrderByDescending(r => r.FechaEmision)
                .ToList();
        }

        public List<TReceta> BuscarPorDoctor(int doctorId)
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .AsNoTracking()
                .Include(r => r.Paciente)
                .Include(r => r.DetallesReceta)
                .Where(r => r.DoctorId == doctorId && !r.Eliminado)
                .OrderByDescending(r => r.FechaEmision)
                .ToList();
        }

        public List<TReceta> BuscarPorEstado(string estado)
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .AsNoTracking()
                .Include(r => r.Paciente)
                .Include(r => r.Doctor)
                .Include(r => r.DetallesReceta)
                .Where(r => r.Estado == estado && !r.Eliminado)
                .OrderByDescending(r => r.FechaEmision)
                .ToList();
        }

        public List<TReceta> BuscarPorConsulta(int consultaId)
        {
            return _unitOfWork.Repository<TReceta>().Consulta()
                .AsNoTracking()
                .Include(r => r.Paciente)
                .Include(r => r.Doctor)
                .Include(r => r.DetallesReceta.Select(d => d.Medicamento))
                .Where(r => r.ConsultaId == consultaId && !r.Eliminado)
                .OrderByDescending(r => r.FechaEmision)
                .ToList();
        }

        public int Agregar(TReceta receta)
        {
            try
            {
                receta.Eliminado = false;
                receta.FechaEmision = DateTime.Now;
                receta.Estado = "Pendiente";
                receta.RecetaId = 0;

                _unitOfWork.Repository<TReceta>().Agregar(receta);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DReceta.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TReceta receta)
        {
            try
            {
                var recetaExistente = _unitOfWork.Repository<TReceta>().Consulta()
                    .FirstOrDefault(r => r.RecetaId == receta.RecetaId);

                if (recetaExistente == null) return 0;

                recetaExistente.FechaVencimiento = receta.FechaVencimiento;
                recetaExistente.Diagnostico = receta.Diagnostico;
                recetaExistente.IndicacionesGenerales = receta.IndicacionesGenerales;
                recetaExistente.Estado = receta.Estado;

                _unitOfWork.Repository<TReceta>().Editar(recetaExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DReceta.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int CambiarEstado(int recetaId, string nuevoEstado)
        {
            try
            {
                var receta = _unitOfWork.Repository<TReceta>().Consulta()
                    .FirstOrDefault(r => r.RecetaId == recetaId);

                if (receta == null) return 0;

                receta.Estado = nuevoEstado;

                _unitOfWork.Repository<TReceta>().Editar(receta);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DReceta.CambiarEstado: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int recetaId)
        {
            try
            {
                var receta = _unitOfWork.Repository<TReceta>().Consulta()
                    .FirstOrDefault(r => r.RecetaId == recetaId && !r.Eliminado);

                if (receta != null)
                {
                    receta.Eliminado = true;
                    receta.Estado = "Cancelada";
                    _unitOfWork.Repository<TReceta>().Editar(receta);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DReceta.Eliminar: {0}", ex.Message));
                return 0;
            }
        }

        public string GenerarNumeroReceta()
        {
            try
            {
                int añoActual = DateTime.Now.Year;

                // Obtener todas las recetas del año actual (incluyendo eliminadas)
                string prefijo = "RX-" + añoActual + "-";
                var recetasDelAño = _unitOfWork.Repository<TReceta>().Consulta()
                    .Where(r => r.NumeroReceta.StartsWith(prefijo))
                    .Select(r => r.NumeroReceta)
                    .ToList();

                if (recetasDelAño.Count == 0)
                {
                    return string.Format("RX-{0}-001", añoActual);
                }

                // Extraer los números consecutivos existentes
                var numerosExistentes = new HashSet<int>();
                foreach (var numeroReceta in recetasDelAño)
                {
                    string[] partes = numeroReceta.Split('-');
                    int numero;
                    if (partes.Length == 3 && int.TryParse(partes[2], out numero))
                    {
                        numerosExistentes.Add(numero);
                    }
                }

                // Buscar el primer hueco disponible
                int numeroConsecutivo = 1;
                while (numerosExistentes.Contains(numeroConsecutivo))
                {
                    numeroConsecutivo++;
                }

                return string.Format("RX-{0}-{1:D3}", añoActual, numeroConsecutivo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DReceta.GenerarNumeroReceta: {0}", ex.Message));
                return string.Format("RX-{0}-001", DateTime.Now.Year);
            }
        }

        public void GuardarConDetalles(TReceta receta, List<TDetalleReceta> detalles)
        {
            _unitOfWork.ComenzarTransaccion();
            try
            {
                // Guardar la receta primero
                receta.Eliminado = false;
                receta.FechaEmision = DateTime.Now;
                receta.Estado = "Pendiente";
                receta.RecetaId = 0;

                _unitOfWork.Repository<TReceta>().Agregar(receta);
                _unitOfWork.Guardar();

                // Ahora guardar los detalles con el RecetaId generado
                foreach (var detalle in detalles)
                {
                    detalle.RecetaId = receta.RecetaId;
                    detalle.Eliminado = false;
                    detalle.CantidadSurtida = 0;
                    _unitOfWork.Repository<TDetalleReceta>().Agregar(detalle);
                }

                _unitOfWork.Guardar();
                _unitOfWork.ConfirmarTransaccion();
            }
            catch
            {
                _unitOfWork.ReversarTransaccion();
                throw;
            }
        }

        public List<TDetalleReceta> ObtenerDetallesReceta(int recetaId)
        {
            return _unitOfWork.Repository<TDetalleReceta>().Consulta()
                .AsNoTracking()
                .Include(d => d.Medicamento)
                .Where(d => d.RecetaId == recetaId && !d.Eliminado)
                .ToList();
        }

        public int ActualizarCantidadSurtida(int detalleRecetaId, int cantidadSurtida)
        {
            try
            {
                var detalle = _unitOfWork.Repository<TDetalleReceta>().Consulta()
                    .FirstOrDefault(d => d.DetalleRecetaId == detalleRecetaId);

                if (detalle == null) return 0;

                detalle.CantidadSurtida += cantidadSurtida;

                // Si ya se surtió toda la cantidad, actualizar estado de la receta
                if (detalle.CantidadSurtida >= detalle.CantidadPrescrita)
                {
                    var receta = _unitOfWork.Repository<TReceta>().Consulta()
                        .Include(r => r.DetallesReceta)
                        .FirstOrDefault(r => r.RecetaId == detalle.RecetaId);

                    if (receta != null)
                    {
                        bool todosSurtidos = receta.DetallesReceta
                            .All(d => d.CantidadSurtida >= d.CantidadPrescrita);

                        if (todosSurtidos)
                            receta.Estado = "Completada";
                        else
                            receta.Estado = "Parcial";

                        _unitOfWork.Repository<TReceta>().Editar(receta);
                    }
                }

                _unitOfWork.Repository<TDetalleReceta>().Editar(detalle);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DReceta.ActualizarCantidadSurtida: {0}", ex.Message));
                throw;
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
