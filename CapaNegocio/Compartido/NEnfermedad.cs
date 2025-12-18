using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Compartido;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Compartido
{
    public class NEnfermedad : IDisposable
    {
        private DEnfermedad dEnfermedad;

        public NEnfermedad()
        {
            dEnfermedad = new DEnfermedad();
        }

        // Lista todas las enfermedades disponibles
        public List<TEnfermedad> ListarEnfermedades()
        {
            try
            {
                return dEnfermedad.Listado();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar enfermedades: {0}", ex.Message), ex);
            }
        }

        // Busca una enfermedad por su ID
        public TEnfermedad BuscarPorId(int enfermedadId)
        {
            try
            {
                if (enfermedadId <= 0)
                    throw new Exception("ID de enfermedad no válido.");

                return dEnfermedad.BuscarPorId(enfermedadId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar enfermedad: {0}", ex.Message), ex);
            }
        }

        // Busca enfermedades por nombre
        public List<TEnfermedad> BuscarPorNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new Exception("El nombre no puede estar vacío.");

                return dEnfermedad.BuscarPorNombre(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar enfermedades: {0}", ex.Message), ex);
            }
        }

        // Busca enfermedades por tipo
        public List<TEnfermedad> BuscarPorTipo(string tipo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipo))
                    throw new Exception("El tipo no puede estar vacío.");

                return dEnfermedad.BuscarPorTipo(tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar enfermedades por tipo: {0}", ex.Message), ex);
            }
        }

        // Registra una nueva enfermedad
        public int RegistrarEnfermedad(TEnfermedad enfermedad)
        {
            try
            {
                ValidarDatosEnfermedad(enfermedad);

                enfermedad.Eliminado = false;

                return dEnfermedad.Agregar(enfermedad);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar enfermedad: {0}", ex.Message), ex);
            }
        }

        // Edita una enfermedad existente
        public int EditarEnfermedad(TEnfermedad enfermedad)
        {
            try
            {
                if (enfermedad.EnfermedadId <= 0)
                    throw new Exception("ID de enfermedad no válido.");

                ValidarDatosEnfermedad(enfermedad);

                var enfermedadExistente = dEnfermedad.BuscarPorId(enfermedad.EnfermedadId);
                if (enfermedadExistente == null)
                    throw new Exception("Enfermedad no encontrada.");

                return dEnfermedad.Editar(enfermedad);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar enfermedad: {0}", ex.Message), ex);
            }
        }

        // Elimina una enfermedad (marcada como eliminada)
        public int EliminarEnfermedad(int enfermedadId)
        {
            try
            {
                if (enfermedadId <= 0)
                    throw new Exception("ID de enfermedad no válido.");

                var enfermedad = dEnfermedad.BuscarPorId(enfermedadId);
                if (enfermedad == null)
                    throw new Exception("Enfermedad no encontrada.");

                return dEnfermedad.Eliminar(enfermedadId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar enfermedad: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosEnfermedad(TEnfermedad enfermedad)
        {
            if (enfermedad == null)
                throw new Exception("La enfermedad no puede ser nula.");

            if (string.IsNullOrWhiteSpace(enfermedad.Nombre))
                throw new Exception("El nombre de la enfermedad es obligatorio.");

            if (enfermedad.Nombre.Length > 100)
                throw new Exception("El nombre de la enfermedad no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(enfermedad.Sintomas) && enfermedad.Sintomas.Length > 1000)
                throw new Exception("Los síntomas no pueden exceder 1000 caracteres.");

            if (!string.IsNullOrWhiteSpace(enfermedad.Tratamiento) && enfermedad.Tratamiento.Length > 1000)
                throw new Exception("El tratamiento no puede exceder 1000 caracteres.");
        }

        public void Dispose()
        {
            if (dEnfermedad != null)
            {
                dEnfermedad.Dispose();
            }
        }
    }
}
