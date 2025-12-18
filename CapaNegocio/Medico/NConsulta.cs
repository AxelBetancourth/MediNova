using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.Medico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaNegocio.Medico
{
    /// <summary>
    /// Capa de Negocio para Consultas Médicas
    /// </summary>
    public class NConsulta : IDisposable
    {
        private DConsulta dConsulta;
        private DExpediente dExpediente;

        public NConsulta()
        {
            dConsulta = new DConsulta();
            dExpediente = new DExpediente();
        }

        /// <summary>
        /// Listar todas las consultas
        /// </summary>
        public List<TConsulta> ListarConsultas()
        {
            try
            {
                return dConsulta.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar consultas: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Buscar consulta por ID
        /// </summary>
        public TConsulta BuscarPorId(int consultaId)
        {
            try
            {
                if (consultaId <= 0)
                    throw new Exception("ID de consulta no válido.");

                return dConsulta.BuscarPorId(consultaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar consulta: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Obtener consultas de un expediente
        /// </summary>
        public List<TConsulta> ObtenerConsultasPorExpediente(int expedienteId)
        {
            try
            {
                if (expedienteId <= 0)
                    throw new Exception("ID de expediente no válido.");

                return dConsulta.BuscarPorExpedienteId(expedienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener consultas del expediente: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Obtener consultas de un paciente
        /// </summary>
        public List<TConsulta> ObtenerConsultasPorPaciente(int pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                    throw new Exception("ID de paciente no válido.");

                return dConsulta.BuscarPorPacienteId(pacienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener consultas del paciente: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Obtener consulta asociada a una cita
        /// </summary>
        public TConsulta ObtenerConsultaPorCita(int citaId)
        {
            try
            {
                if (citaId <= 0)
                    throw new Exception("ID de cita no válido.");

                return dConsulta.BuscarPorCitaId(citaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener consulta de la cita: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Registrar nueva consulta
        /// </summary>
        public int RegistrarConsulta(TConsulta consulta)
        {
            try
            {
                ValidarDatosConsulta(consulta);

                // Verificar que el expediente existe
                var expediente = dExpediente.BuscarPorId(consulta.ExpedienteId);
                if (expediente == null)
                    throw new Exception("El expediente no existe.");

                consulta.FechaConsulta = DateTime.Now;
                consulta.Eliminado = false;
                consulta.Estado = "EnProgreso";

                // Asegurar que EstadoPago tenga un valor
                if (string.IsNullOrWhiteSpace(consulta.EstadoPago))
                {
                    consulta.EstadoPago = "Pendiente";
                }

                // Generar número de consulta si no existe
                if (string.IsNullOrWhiteSpace(consulta.NumeroConsulta))
                {
                    consulta.NumeroConsulta = dConsulta.GenerarNumeroConsulta();
                }

                // Calcular IMC si hay peso y altura
                if (consulta.Peso.HasValue && consulta.Altura.HasValue && consulta.Altura.Value > 0)
                {
                    decimal alturaMts = consulta.Altura.Value / 100; // Convertir cm a metros
                    consulta.IMC = consulta.Peso.Value / (alturaMts * alturaMts);
                }

                return dConsulta.Agregar(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar consulta: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Editar consulta existente
        /// </summary>
        public int EditarConsulta(TConsulta consulta)
        {
            try
            {
                if (consulta.ConsultaId <= 0)
                    throw new Exception("ID de consulta no válido.");

                ValidarDatosConsulta(consulta);

                var consultaExistente = dConsulta.BuscarPorId(consulta.ConsultaId);
                if (consultaExistente == null)
                    throw new Exception("Consulta no encontrada.");

                // No permitir editar consultas finalizadas
                if (consultaExistente.Estado == "Finalizada")
                    throw new Exception("No se puede editar una consulta finalizada.");

                // Calcular IMC si hay peso y altura
                if (consulta.Peso.HasValue && consulta.Altura.HasValue && consulta.Altura.Value > 0)
                {
                    decimal alturaMts = consulta.Altura.Value / 100;
                    consulta.IMC = consulta.Peso.Value / (alturaMts * alturaMts);
                }

                return dConsulta.Editar(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar consulta: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Finalizar consulta
        /// </summary>
        public int FinalizarConsulta(int consultaId)
        {
            try
            {
                if (consultaId <= 0)
                    throw new Exception("ID de consulta no válido.");

                var consulta = dConsulta.BuscarPorId(consultaId);
                if (consulta == null)
                    throw new Exception("Consulta no encontrada.");

                if (consulta.Estado == "Finalizada")
                    throw new Exception("La consulta ya está finalizada.");

                // Validar que tenga los datos mínimos
                if (string.IsNullOrWhiteSpace(consulta.Diagnostico))
                    throw new Exception("La consulta debe tener un diagnóstico antes de finalizarse.");

                return dConsulta.FinalizarConsulta(consultaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al finalizar consulta: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Registrar pago de consulta
        /// </summary>
        public int RegistrarPago(int consultaId, int? ventaId = null)
        {
            try
            {
                if (consultaId <= 0)
                    throw new Exception("ID de consulta no válido.");

                var consulta = dConsulta.BuscarPorId(consultaId);
                if (consulta == null)
                    throw new Exception("Consulta no encontrada.");

                if (consulta.EstadoPago == "Pagado")
                    throw new Exception("La consulta ya está pagada.");

                // Asignar VentaId primero si se proporciona
                if (ventaId.HasValue)
                {
                    consulta.VentaId = ventaId.Value;
                }

                // Actualizar estado de pago
                consulta.EstadoPago = "Pagado";
                consulta.FechaPago = DateTime.Now;

                // Guardar todos los cambios en una sola operación
                return dConsulta.Editar(consulta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar pago: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Obtener consultas pendientes de pago por paciente
        /// </summary>
        public List<TConsulta> ObtenerConsultasPendientesPagoPorPaciente(int pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                    throw new Exception("ID de paciente no válido.");

                return dConsulta.BuscarPorPacienteId(pacienteId)
                    .Where(c => c.EstadoPago == "Pendiente" && c.CostoConsulta > 0)
                    .OrderBy(c => c.FechaConsulta)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener consultas pendientes de pago: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Eliminar consulta (soft delete)
        /// </summary>
        public int EliminarConsulta(int consultaId)
        {
            try
            {
                if (consultaId <= 0)
                    throw new Exception("ID de consulta no válido.");

                var consulta = dConsulta.BuscarPorId(consultaId);
                if (consulta == null)
                    throw new Exception("Consulta no encontrada.");

                // No permitir eliminar consultas finalizadas
                if (consulta.Estado == "Finalizada")
                    throw new Exception("No se puede eliminar una consulta finalizada.");

                return dConsulta.Eliminar(consultaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar consulta: {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Validar datos de la consulta
        /// </summary>
        private void ValidarDatosConsulta(TConsulta consulta)
        {
            if (consulta == null)
                throw new Exception("La consulta no puede ser nula.");

            if (consulta.ExpedienteId <= 0)
                throw new Exception("Debe asociar la consulta a un expediente.");

            if (consulta.DoctorId <= 0)
                throw new Exception("Debe asociar la consulta a un doctor.");

            if (string.IsNullOrWhiteSpace(consulta.MotivoConsulta))
                throw new Exception("El motivo de consulta es obligatorio.");

            if (consulta.CostoConsulta < 0)
                throw new Exception("El costo de la consulta no puede ser negativo.");

            // Validar signos vitales si se proporcionan
            if (consulta.Temperatura.HasValue && (consulta.Temperatura.Value < 30 || consulta.Temperatura.Value > 45))
                throw new Exception("La temperatura debe estar entre 30°C y 45°C.");

            if (consulta.FrecuenciaCardiaca.HasValue && (consulta.FrecuenciaCardiaca.Value < 40 || consulta.FrecuenciaCardiaca.Value > 200))
                throw new Exception("La frecuencia cardíaca debe estar entre 40 y 200 ppm.");

            if (consulta.Peso.HasValue && consulta.Peso.Value <= 0)
                throw new Exception("El peso debe ser mayor a 0.");

            if (consulta.Altura.HasValue && consulta.Altura.Value <= 0)
                throw new Exception("La altura debe ser mayor a 0.");
        }

        public void Dispose()
        {
            if (dConsulta != null)
            {
                dConsulta.Dispose();
            }
            if (dExpediente != null)
            {
                dExpediente.Dispose();
            }
        }
    }
}
