using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.Medico;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Medico
{
    public class NExpediente : IDisposable
    {
        private DExpediente dExpediente;

        public NExpediente()
        {
            dExpediente = new DExpediente();
        }

        public List<TExpediente> ListarExpedientes()
        {
            try
            {
                return dExpediente.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar expedientes: {0}", ex.Message), ex);
            }
        }

        public TExpediente BuscarPorId(int expedienteId)
        {
            try
            {
                if (expedienteId <= 0)
                    throw new Exception("ID de expediente no válido.");

                return dExpediente.BuscarPorId(expedienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar expediente: {0}", ex.Message), ex);
            }
        }

        public TExpediente BuscarPorPacienteId(int pacienteId)
        {
            try
            {
                if (pacienteId <= 0)
                    throw new Exception("ID de paciente no válido.");

                return dExpediente.BuscarPorPacienteId(pacienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar expediente del paciente: {0}", ex.Message), ex);
            }
        }

        public int RegistrarExpediente(TExpediente expediente)
        {
            try
            {
                ValidarDatosExpediente(expediente);

                expediente.FechaApertura = DateTime.Now;
                expediente.Eliminado = false;

                // Generar número de expediente
                expediente.NumeroExpediente = GenerarNumeroExpediente();

                return dExpediente.Agregar(expediente);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar expediente: {0}", ex.Message), ex);
            }
        }

        public int EditarExpediente(TExpediente expediente)
        {
            try
            {
                if (expediente.PacienteId <= 0)
                    throw new Exception("ID de expediente no válido.");

                ValidarDatosExpediente(expediente);

                var expedienteExistente = dExpediente.BuscarPorId(expediente.PacienteId);
                if (expedienteExistente == null)
                    throw new Exception("Expediente no encontrado.");

                return dExpediente.Editar(expediente);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar expediente: {0}", ex.Message), ex);
            }
        }

        public int EliminarExpediente(int expedienteId)
        {
            try
            {
                if (expedienteId <= 0)
                    throw new Exception("ID de expediente no válido.");

                var expediente = dExpediente.BuscarPorId(expedienteId);
                if (expediente == null)
                    throw new Exception("Expediente no encontrado.");

                return dExpediente.Eliminar(expedienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar expediente: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosExpediente(TExpediente expediente)
        {
            if (expediente == null)
                throw new Exception("El expediente no puede ser nulo.");

            if (expediente.PacienteId <= 0)
                throw new Exception("Debe asociar el expediente a un paciente.");
        }

        private string GenerarNumeroExpediente()
        {
            return string.Format("EXP-{0}-{1}", DateTime.Now.Year, Guid.NewGuid().ToString().Substring(0, 6).ToUpper());
        }

        public void Dispose()
        {
            if (dExpediente != null)
            {
                dExpediente.Dispose();
            }
        }
    }
}
