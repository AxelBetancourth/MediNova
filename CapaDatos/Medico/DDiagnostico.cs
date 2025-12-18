using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Medico
{
    /// <summary>
    /// Capa de Datos para Diagnósticos
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DDiagnostico : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DDiagnostico()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TDiagnostico> Listado()
        {
            return _unitOfWork.Repository<TDiagnostico>().Consulta()
                .Where(d => !d.Eliminado)
                .OrderByDescending(d => d.FechaDiagnostico)
                .ToList();
        }

        public List<TDiagnostico> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TDiagnostico>().Consulta()
                .AsNoTracking()
                .Include(d => d.Expediente.Paciente)
                .Include(d => d.Enfermedad)
                .Include(d => d.Consulta)
                .Where(d => !d.Eliminado)
                .OrderByDescending(d => d.FechaDiagnostico)
                .ToList();
        }

        public TDiagnostico BuscarPorId(int diagnosticoId)
        {
            return _unitOfWork.Repository<TDiagnostico>().Consulta()
                .AsNoTracking()
                .Include(d => d.Expediente.Paciente)
                .Include(d => d.Enfermedad)
                .Include(d => d.Consulta)
                .FirstOrDefault(d => d.DiagnosticoId == diagnosticoId && !d.Eliminado);
        }

        public List<TDiagnostico> BuscarPorExpedienteId(int expedienteId)
        {
            return _unitOfWork.Repository<TDiagnostico>().Consulta()
                .AsNoTracking()
                .Include(d => d.Enfermedad)
                .Include(d => d.Consulta)
                .Where(d => d.ExpedienteId == expedienteId && !d.Eliminado)
                .OrderByDescending(d => d.FechaDiagnostico)
                .ToList();
        }

        public List<TDiagnostico> BuscarPorConsultaId(int consultaId)
        {
            return _unitOfWork.Repository<TDiagnostico>().Consulta()
                .AsNoTracking()
                .Include(d => d.Enfermedad)
                .Where(d => d.ConsultaId == consultaId && !d.Eliminado)
                .ToList();
        }

        public int Agregar(TDiagnostico diagnostico)
        {
            try
            {
                diagnostico.Eliminado = false;
                diagnostico.FechaDiagnostico = DateTime.Now;
                diagnostico.DiagnosticoId = 0;

                _unitOfWork.Repository<TDiagnostico>().Agregar(diagnostico);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDiagnostico.Agregar: {0}", ex.Message));
                throw;
            }
        }

        public int Editar(TDiagnostico diagnostico)
        {
            try
            {
                var diagnosticoExistente = _unitOfWork.Repository<TDiagnostico>().Consulta()
                    .FirstOrDefault(d => d.DiagnosticoId == diagnostico.DiagnosticoId);

                if (diagnosticoExistente == null) return 0;

                diagnosticoExistente.EnfermedadId = diagnostico.EnfermedadId;
                diagnosticoExistente.Observaciones = diagnostico.Observaciones;
                diagnosticoExistente.Estado = diagnostico.Estado;

                _unitOfWork.Repository<TDiagnostico>().Editar(diagnosticoExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDiagnostico.Editar: {0}", ex.Message));
                throw;
            }
        }

        public int Eliminar(int diagnosticoId)
        {
            try
            {
                var diagnostico = _unitOfWork.Repository<TDiagnostico>().Consulta()
                    .FirstOrDefault(d => d.DiagnosticoId == diagnosticoId && !d.Eliminado);

                if (diagnostico != null)
                {
                    diagnostico.Eliminado = true;
                    _unitOfWork.Repository<TDiagnostico>().Editar(diagnostico);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDiagnostico.Eliminar: {0}", ex.Message));
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
