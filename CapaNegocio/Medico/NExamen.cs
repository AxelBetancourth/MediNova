using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Compartido;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Medico
{
    public class NExamen : IDisposable
    {
        private DExamen dExamen;

        public NExamen()
        {
            dExamen = new DExamen();
        }

        public List<TExamen> ListarExamenes()
        {
            try
            {
                return dExamen.ListadoConRelaciones();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar exámenes: {0}", ex.Message), ex);
            }
        }

        public TExamen BuscarPorId(int examenId)
        {
            try
            {
                if (examenId <= 0)
                    throw new Exception("ID de examen no válido.");

                return dExamen.BuscarPorId(examenId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar examen: {0}", ex.Message), ex);
            }
        }

        public List<TExamen> BuscarPorExpedienteId(int expedienteId)
        {
            try
            {
                if (expedienteId <= 0)
                    throw new Exception("ID de expediente no válido.");

                return dExamen.BuscarPorExpedienteId(expedienteId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar exámenes del expediente: {0}", ex.Message), ex);
            }
        }

        public List<TExamen> BuscarPorEstado(string estado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estado))
                    throw new Exception("El estado no puede estar vacío.");

                return dExamen.BuscarPorEstado(estado);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar exámenes por estado: {0}", ex.Message), ex);
            }
        }

        public int SolicitarExamen(TExamen examen)
        {
            try
            {
                ValidarDatosExamen(examen);

                examen.FechaSolicitud = DateTime.Now;
                examen.Estado = "Solicitado";
                examen.Eliminado = false;

                return dExamen.Agregar(examen);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al solicitar examen: {0}", ex.Message), ex);
            }
        }

        public int EditarExamen(TExamen examen)
        {
            try
            {
                if (examen.ExamenId <= 0)
                    throw new Exception("ID de examen no válido.");

                ValidarDatosExamen(examen);

                var examenExistente = dExamen.BuscarPorId(examen.ExamenId);
                if (examenExistente == null)
                    throw new Exception("Examen no encontrado.");

                return dExamen.Editar(examen);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar examen: {0}", ex.Message), ex);
            }
        }

        public int RegistrarResultado(int examenId, string resultado)
        {
            try
            {
                if (examenId <= 0)
                    throw new Exception("ID de examen no válido.");

                if (string.IsNullOrWhiteSpace(resultado))
                    throw new Exception("El resultado no puede estar vacío.");

                var examen = dExamen.BuscarPorId(examenId);
                if (examen == null)
                    throw new Exception("Examen no encontrado.");

                examen.Resultado = resultado;
                examen.FechaResultado = DateTime.Now;
                examen.Estado = "Completado";

                return dExamen.Editar(examen);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar resultado del examen: {0}", ex.Message), ex);
            }
        }

        public int EliminarExamen(int examenId)
        {
            try
            {
                if (examenId <= 0)
                    throw new Exception("ID de examen no válido.");

                var examen = dExamen.BuscarPorId(examenId);
                if (examen == null)
                    throw new Exception("Examen no encontrado.");

                return dExamen.Eliminar(examenId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar examen: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosExamen(TExamen examen)
        {
            if (examen == null)
                throw new Exception("El examen no puede ser nulo.");

            if (examen.ExpedienteId <= 0)
                throw new Exception("Debe asociar el examen a un expediente.");

            if (string.IsNullOrWhiteSpace(examen.Nombre))
                throw new Exception("El nombre del examen es obligatorio.");

            if (string.IsNullOrWhiteSpace(examen.Tipo))
                throw new Exception("El tipo de examen es obligatorio.");

            if (examen.Costo < 0)
                throw new Exception("El costo del examen no puede ser negativo.");
        }

        public void Dispose()
        {
            if (dExamen != null)
            {
                dExamen.Dispose();
            }
        }
    }
}
