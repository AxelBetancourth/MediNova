using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Compartido;
using CapaDatos.Medico;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Medico
{
    public class NDiagnostico : IDisposable
    {
        private DDiagnostico dDiagnostico;

        public NDiagnostico()
        {
            dDiagnostico = new DDiagnostico();
        }

        public List<TDiagnostico> ListarDiagnosticos()
        {
            try
            {
                return dDiagnostico.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar diagnósticos: {0}", ex.Message), ex);
            }
        }

        public TDiagnostico BuscarPorId(int diagnosticoId)
        {
            try
            {
                if (diagnosticoId <= 0)
                    throw new Exception("ID de diagnóstico no válido.");

                return dDiagnostico.BuscarPorId(diagnosticoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar diagnóstico: {0}", ex.Message), ex);
            }
        }

        public List<TDiagnostico> BuscarPorExpedienteId(int expedienteId)
        {
            try
            {
                if (expedienteId <= 0)
                    throw new Exception("ID de expediente no válido.");

                return dDiagnostico.BuscarPorExpedienteId(expedienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar diagnósticos del expediente: {0}", ex.Message), ex);
            }
        }

        public List<TDiagnostico> BuscarPorConsultaId(int consultaId)
        {
            try
            {
                if (consultaId <= 0)
                    throw new Exception("ID de consulta no válido.");

                return dDiagnostico.BuscarPorConsultaId(consultaId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar diagnósticos de la consulta: {0}", ex.Message), ex);
            }
        }

        public int RegistrarDiagnostico(TDiagnostico diagnostico)
        {
            try
            {
                ValidarDatosDiagnostico(diagnostico);

                diagnostico.FechaDiagnostico = DateTime.Now;
                diagnostico.Estado = "Activo";
                diagnostico.Eliminado = false;

                return dDiagnostico.Agregar(diagnostico);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar diagnóstico: {0}", ex.Message), ex);
            }
        }

        public int EditarDiagnostico(TDiagnostico diagnostico)
        {
            try
            {
                if (diagnostico.DiagnosticoId <= 0)
                    throw new Exception("ID de diagnóstico no válido.");

                ValidarDatosDiagnostico(diagnostico);

                var diagnosticoExistente = dDiagnostico.BuscarPorId(diagnostico.DiagnosticoId);
                if (diagnosticoExistente == null)
                    throw new Exception("Diagnóstico no encontrado.");

                return dDiagnostico.Editar(diagnostico);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar diagnóstico: {0}", ex.Message), ex);
            }
        }

        public int EliminarDiagnostico(int diagnosticoId)
        {
            try
            {
                if (diagnosticoId <= 0)
                    throw new Exception("ID de diagnóstico no válido.");

                var diagnostico = dDiagnostico.BuscarPorId(diagnosticoId);
                if (diagnostico == null)
                    throw new Exception("Diagnóstico no encontrado.");

                return dDiagnostico.Eliminar(diagnosticoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar diagnóstico: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosDiagnostico(TDiagnostico diagnostico)
        {
            if (diagnostico == null)
                throw new Exception("El diagnóstico no puede ser nulo.");

            if (diagnostico.ExpedienteId <= 0)
                throw new Exception("Debe asociar el diagnóstico a un expediente.");

            if (diagnostico.EnfermedadId <= 0)
                throw new Exception("Debe especificar una enfermedad.");

            if (string.IsNullOrWhiteSpace(diagnostico.Estado))
                diagnostico.Estado = "Activo";
        }

        public void Dispose()
        {
            if (dDiagnostico != null)
            {
                dDiagnostico.Dispose();
            }
        }
    }
}
