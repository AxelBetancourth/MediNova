using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos.Compartido
{
    /// <summary>
    /// Capa de Datos para Catálogo de Enfermedades
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DEnfermedad : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DEnfermedad()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TEnfermedad> Listado()
        {
            return _unitOfWork.Repository<TEnfermedad>().Consulta()
                .Where(e => !e.Eliminado)
                .OrderBy(e => e.Nombre)
                .ToList();
        }

        public List<TEnfermedad> BuscarPorTipo(string tipo)
        {
            return _unitOfWork.Repository<TEnfermedad>().Consulta()
                .Where(e => e.Tipo == tipo && !e.Eliminado)
                .OrderBy(e => e.Nombre)
                .ToList();
        }

        public TEnfermedad BuscarPorId(int enfermedadId)
        {
            return _unitOfWork.Repository<TEnfermedad>().Consulta()
                .FirstOrDefault(e => e.EnfermedadId == enfermedadId && !e.Eliminado);
        }

        public List<TEnfermedad> BuscarPorNombre(string nombre)
        {
            return _unitOfWork.Repository<TEnfermedad>().Consulta()
                .Where(e => e.Nombre.Contains(nombre) && !e.Eliminado)
                .OrderBy(e => e.Nombre)
                .ToList();
        }

        public int Agregar(TEnfermedad enfermedad)
        {
            try
            {
                enfermedad.Eliminado = false;
                enfermedad.EnfermedadId = 0;

                _unitOfWork.Repository<TEnfermedad>().Agregar(enfermedad);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DEnfermedad.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TEnfermedad enfermedad)
        {
            try
            {
                var enfermedadExistente = _unitOfWork.Repository<TEnfermedad>().Consulta()
                    .FirstOrDefault(e => e.EnfermedadId == enfermedad.EnfermedadId);

                if (enfermedadExistente == null) return 0;

                enfermedadExistente.Nombre = enfermedad.Nombre;
                enfermedadExistente.Sintomas = enfermedad.Sintomas;
                enfermedadExistente.Tratamiento = enfermedad.Tratamiento;
                enfermedadExistente.Tipo = enfermedad.Tipo;

                _unitOfWork.Repository<TEnfermedad>().Editar(enfermedadExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DEnfermedad.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int enfermedadId)
        {
            try
            {
                var enfermedad = _unitOfWork.Repository<TEnfermedad>().Consulta()
                    .FirstOrDefault(e => e.EnfermedadId == enfermedadId && !e.Eliminado);

                if (enfermedad != null)
                {
                    enfermedad.Eliminado = true;
                    _unitOfWork.Repository<TEnfermedad>().Editar(enfermedad);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DEnfermedad.Eliminar: {0}", ex.Message));
                return 0;
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
