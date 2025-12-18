using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Medico
{
    /// <summary>
    /// Capa de Datos para Expedientes Clínicos
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DExpediente : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DExpediente()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TExpediente> Listado()
        {
            return _unitOfWork.Repository<TExpediente>().Consulta()
                .Where(e => !e.Eliminado)
                .OrderByDescending(e => e.FechaApertura)
                .ToList();
        }

        public List<TExpediente> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TExpediente>().Consulta()
                .AsNoTracking()
                .Include(e => e.Paciente)
                .Include(e => e.Consultas)
                .Include(e => e.Diagnosticos)
                .Include(e => e.Examenes)
                .Where(e => !e.Eliminado)
                .OrderByDescending(e => e.FechaApertura)
                .ToList();
        }

        public TExpediente BuscarPorId(int expedienteId)
        {
            return _unitOfWork.Repository<TExpediente>().Consulta()
                .AsNoTracking()
                .Include(e => e.Paciente)
                .Include(e => e.Consultas)
                .Include(e => e.Diagnosticos)
                .Include(e => e.Examenes)
                .FirstOrDefault(e => e.PacienteId == expedienteId && !e.Eliminado);
        }

        public TExpediente BuscarPorPacienteId(int pacienteId)
        {
            return _unitOfWork.Repository<TExpediente>().Consulta()
                .AsNoTracking()
                .Include(e => e.Paciente)
                .Include(e => e.Consultas.Select(c => c.Doctor))
                .Include(e => e.Consultas.Select(c => c.Enfermedad))
                .Include(e => e.Diagnosticos.Select(d => d.Enfermedad))
                .Include(e => e.Examenes)
                .FirstOrDefault(e => e.PacienteId == pacienteId && !e.Eliminado);
        }

        public int Agregar(TExpediente expediente)
        {
            try
            {
                expediente.Eliminado = false;
                expediente.FechaApertura = DateTime.Now;

                _unitOfWork.Repository<TExpediente>().Agregar(expediente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DExpediente.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TExpediente expediente)
        {
            try
            {
                var expedienteExistente = _unitOfWork.Repository<TExpediente>().Consulta()
                    .FirstOrDefault(e => e.PacienteId == expediente.PacienteId);

                if (expedienteExistente == null) return 0;

                expedienteExistente.Alergias = expediente.Alergias;
                expedienteExistente.AntecedentesFamiliares = expediente.AntecedentesFamiliares;
                expedienteExistente.AntecedentesPersonales = expediente.AntecedentesPersonales;
                expedienteExistente.TipoSangre = expediente.TipoSangre;
                expedienteExistente.NotasGenerales = expediente.NotasGenerales;
                expedienteExistente.ContactoEmergencia = expediente.ContactoEmergencia;
                expedienteExistente.TelefonoEmergencia = expediente.TelefonoEmergencia;

                _unitOfWork.Repository<TExpediente>().Editar(expedienteExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DExpediente.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int expedienteId)
        {
            try
            {
                var expediente = _unitOfWork.Repository<TExpediente>().Consulta()
                    .FirstOrDefault(e => e.PacienteId == expedienteId && !e.Eliminado);

                if (expediente != null)
                {
                    expediente.Eliminado = true;
                    _unitOfWork.Repository<TExpediente>().Editar(expediente);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DExpediente.Eliminar: {0}", ex.Message));
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
