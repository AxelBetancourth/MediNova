using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Compartido
{
    /// <summary>
    /// Capa de Datos para Exámenes Médicos
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DExamen : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DExamen()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TExamen> Listado()
        {
            return _unitOfWork.Repository<TExamen>().Consulta()
                .Where(e => !e.Eliminado)
                .OrderByDescending(e => e.FechaSolicitud)
                .ToList();
        }

        public List<TExamen> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TExamen>().Consulta()
                .AsNoTracking()
                .Include(e => e.Expediente.Paciente)
                .Include(e => e.Cita)
                .Where(e => !e.Eliminado)
                .OrderByDescending(e => e.FechaSolicitud)
                .ToList();
        }

        public TExamen BuscarPorId(int examenId)
        {
            return _unitOfWork.Repository<TExamen>().Consulta()
                .AsNoTracking()
                .Include(e => e.Expediente.Paciente)
                .Include(e => e.Cita)
                .FirstOrDefault(e => e.ExamenId == examenId && !e.Eliminado);
        }

        public List<TExamen> BuscarPorExpedienteId(int expedienteId)
        {
            return _unitOfWork.Repository<TExamen>().Consulta()
                .AsNoTracking()
                .Include(e => e.Cita)
                .Where(e => e.ExpedienteId == expedienteId && !e.Eliminado)
                .OrderByDescending(e => e.FechaSolicitud)
                .ToList();
        }

        public List<TExamen> BuscarPorEstado(string estado)
        {
            return _unitOfWork.Repository<TExamen>().Consulta()
                .AsNoTracking()
                .Include(e => e.Expediente.Paciente)
                .Where(e => e.Estado == estado && !e.Eliminado)
                .OrderByDescending(e => e.FechaSolicitud)
                .ToList();
        }

        public int Agregar(TExamen examen)
        {
            try
            {
                examen.Eliminado = false;
                examen.FechaSolicitud = DateTime.Now;
                examen.Estado = "Pendiente";
                examen.ExamenId = 0;

                _unitOfWork.Repository<TExamen>().Agregar(examen);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DExamen.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TExamen examen)
        {
            try
            {
                var examenExistente = _unitOfWork.Repository<TExamen>().Consulta()
                    .FirstOrDefault(e => e.ExamenId == examen.ExamenId);

                if (examenExistente == null) return 0;

                examenExistente.Nombre = examen.Nombre;
                examenExistente.Tipo = examen.Tipo;
                examenExistente.Costo = examen.Costo;
                examenExistente.FechaResultado = examen.FechaResultado;
                examenExistente.Resultado = examen.Resultado;
                examenExistente.Estado = examen.Estado;
                examenExistente.VentaId = examen.VentaId;

                _unitOfWork.Repository<TExamen>().Editar(examenExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DExamen.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int examenId)
        {
            try
            {
                var examen = _unitOfWork.Repository<TExamen>().Consulta()
                    .FirstOrDefault(e => e.ExamenId == examenId && !e.Eliminado);

                if (examen != null)
                {
                    examen.Eliminado = true;
                    _unitOfWork.Repository<TExamen>().Editar(examen);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DExamen.Eliminar: {0}", ex.Message));
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
