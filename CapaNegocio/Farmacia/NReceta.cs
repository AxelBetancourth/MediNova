using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.Farmacia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaNegocio.Farmacia
{
    public class NReceta : IDisposable
    {
        private DReceta dReceta;

        public NReceta()
        {
            dReceta = new DReceta();
        }

        public List<TReceta> ListarRecetas()
        {
            try
            {
                return dReceta.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar recetas: {0}", ex.Message), ex);
            }
        }

        public TReceta BuscarPorId(int recetaId)
        {
            try
            {
                if (recetaId <= 0)
                    throw new Exception("ID de receta no válido.");

                return dReceta.BuscarPorId(recetaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar receta: {0}", ex.Message), ex);
            }
        }

        public TReceta BuscarPorNumero(string numeroReceta)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroReceta))
                    throw new Exception("El número de receta no puede estar vacío.");

                return dReceta.BuscarPorNumero(numeroReceta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar receta: {0}", ex.Message), ex);
            }
        }

        public List<TReceta> BuscarPorPaciente(int pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                    throw new Exception("ID de paciente no válido.");

                return dReceta.BuscarPorPaciente(pacienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar recetas del paciente: {0}", ex.Message), ex);
            }
        }

        public List<TReceta> BuscarPorDoctor(int doctorId)
        {
            try
            {
                if (doctorId <= 0)
                    throw new Exception("ID de doctor no válido.");

                return dReceta.BuscarPorDoctor(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar recetas del doctor: {0}", ex.Message), ex);
            }
        }

        public List<TReceta> BuscarPorEstado(string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estado))
                    throw new Exception("El estado no puede estar vacío.");

                return dReceta.BuscarPorEstado(estado);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar recetas por estado: {0}", ex.Message), ex);
            }
        }

        public List<TReceta> BuscarRecetasPendientesPorPaciente(int pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                    throw new Exception("ID de paciente no válido.");

                // Solo mostrar recetas que realmente tienen medicamentos INTERNOS pendientes de surtir
                // Excluir las que ya están completamente surtidas, canceladas, vencidas o marcadas como no surtidas
                var recetasCandidatas = dReceta.BuscarPorPaciente(pacienteId)
                    .Where(r => r.Estado == "Pendiente" || r.Estado == "Parcial")
                    .Where(r => r.Estado != "Surtida" && r.Estado != "Cancelada" && r.Estado != "Vencida" && r.Estado != "NoSurtida")
                    .ToList();

                // Filtrar solo las recetas que tienen medicamentos INTERNOS (del inventario) pendientes
                var recetasConPendientesInternos = recetasCandidatas.Where(r =>
                {
                    var detalles = r.DetallesReceta ?? ObtenerDetallesReceta(r.RecetaId);
                    // Verificar si hay medicamentos internos con cantidad pendiente
                    return detalles.Any(d =>
                        d.MedicamentoId.HasValue && // Solo internos
                        (d.CantidadPrescrita - d.CantidadSurtida) > 0 // Con cantidad pendiente
                    );
                }).OrderByDescending(r => r.FechaEmision)
                  .ToList();

                return recetasConPendientesInternos;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar recetas pendientes: {0}", ex.Message), ex);
            }
        }

        public List<TReceta> BuscarPorConsulta(int consultaId)
        {
            try
            {
                if (consultaId <= 0)
                    throw new Exception("ID de consulta no válido.");

                return dReceta.BuscarPorConsulta(consultaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar recetas de la consulta: {0}", ex.Message), ex);
            }
        }

        public int EmitirReceta(TReceta receta)
        {
            try
            {
                ValidarDatosReceta(receta);

                receta.NumeroReceta = dReceta.GenerarNumeroReceta();
                receta.FechaEmision = DateTime.Now;
                receta.Estado = "Pendiente";
                receta.Eliminado = false;

                // Por defecto, las recetas vencen en 30 días
                if (!receta.FechaVencimiento.HasValue)
                    receta.FechaVencimiento = DateTime.Now.AddDays(30);

                return dReceta.Agregar(receta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al emitir receta: {0}", ex.Message), ex);
            }
        }

        public void GuardarReceta(TReceta receta, List<TDetalleReceta> detalles)
        {
            try
            {
                ValidarDatosReceta(receta);

                if (detalles == null || detalles.Count == 0)
                    throw new Exception("Debe agregar al menos un medicamento a la receta.");

                // Generar número de receta
                receta.NumeroReceta = dReceta.GenerarNumeroReceta();
                receta.FechaEmision = DateTime.Now;
                receta.Estado = "Pendiente";
                receta.Eliminado = false;

                // Guardar receta con detalles
                dReceta.GuardarConDetalles(receta, detalles);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al guardar receta: {0}", ex.Message), ex);
            }
        }

        public int EditarReceta(TReceta receta)
        {
            try
            {
                if (receta.RecetaId <= 0)
                    throw new Exception("ID de receta no válido.");

                ValidarDatosReceta(receta);

                var recetaExistente = dReceta.BuscarPorId(receta.RecetaId);
                if (recetaExistente == null)
                    throw new Exception("Receta no encontrada.");

                return dReceta.Editar(receta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar receta: {0}", ex.Message), ex);
            }
        }

        public int CambiarEstadoReceta(int recetaId, string nuevoEstado)
        {
            try
            {
                if (recetaId <= 0)
                    throw new Exception("ID de receta no válido.");

                if (string.IsNullOrWhiteSpace(nuevoEstado))
                    throw new Exception("El nuevo estado no puede estar vacío.");

                var receta = dReceta.BuscarPorId(recetaId);
                if (receta == null)
                    throw new Exception("Receta no encontrada.");

                return dReceta.CambiarEstado(recetaId, nuevoEstado);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al cambiar estado de receta: {0}", ex.Message), ex);
            }
        }

        public int CancelarReceta(int recetaId)
        {
            try
            {
                if (recetaId <= 0)
                    throw new Exception("ID de receta no válido.");

                var receta = dReceta.BuscarPorId(recetaId);
                if (receta == null)
                    throw new Exception("Receta no encontrada.");

                return dReceta.Eliminar(recetaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al cancelar receta: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosReceta(TReceta receta)
        {
            if (receta == null)
                throw new Exception("La receta no puede ser nula.");

            if (receta.PacienteId <= 0)
                throw new Exception("Debe asociar la receta a un paciente.");

            if (receta.DoctorId <= 0)
                throw new Exception("Debe asociar la receta a un doctor.");

            if (receta.FechaVencimiento.HasValue && receta.FechaVencimiento <= DateTime.Now)
                throw new Exception("La fecha de vencimiento debe ser futura.");
        }

        public List<TDetalleReceta> ObtenerDetallesReceta(int recetaId)
        {
            try
            {
                if (recetaId <= 0)
                    throw new Exception("ID de receta no válido.");

                return dReceta.ObtenerDetallesReceta(recetaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener detalles de receta: {0}", ex.Message), ex);
            }
        }

        public int ActualizarCantidadSurtida(int detalleRecetaId, int cantidadSurtida)
        {
            try
            {
                if (detalleRecetaId <= 0)
                    throw new Exception("ID de detalle de receta no válido.");

                if (cantidadSurtida <= 0)
                    throw new Exception("La cantidad surtida debe ser mayor a cero.");

                return dReceta.ActualizarCantidadSurtida(detalleRecetaId, cantidadSurtida);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al actualizar cantidad surtida: {0}", ex.Message), ex);
            }
        }


        public void Dispose()
        {
            if (dReceta != null)
            {
                dReceta.Dispose();
            }
        }
    }
}
