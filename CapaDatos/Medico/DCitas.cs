using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CapaDatos.Medico
{
    public class DCitas : IDisposable
    {
        UnitOfWork _unitOfWork;

        public DCitas()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<TCita> ListadoPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return _unitOfWork.Repository<TCita>().Consulta()
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => !c.Eliminado &&
                             c.FechaHoraInicio < fechaFin && 
                             c.FechaHoraFin > fechaInicio)
                .ToList();
        }

        public List<TCita> Listado()
        {
            return _unitOfWork.Repository<TCita>().Consulta()
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => !c.Eliminado)
                .OrderByDescending(c => c.FechaHoraInicio)
                .ToList();
        }

        public TCita BuscarPorId(int citaId)
        {
            return _unitOfWork.Repository<TCita>().Consulta()
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .FirstOrDefault(c => c.CitaId == citaId);
        }

        public int Guardar(TCita cita)
        {
            try
            {
                if (cita.CitaId == 0)
                {
                    // Nueva cita
                    cita.Paciente = null;
                    cita.Doctor = null;
                    _unitOfWork.Repository<TCita>().Agregar(cita);
                }
                else
                {
                    // Actualizar cita
                    var citaEnDb = _unitOfWork.Repository<TCita>().Consulta()
                                .FirstOrDefault(c => c.CitaId == cita.CitaId);

                    if (citaEnDb != null)
                    {
                        citaEnDb.PacienteId = cita.PacienteId;
                        citaEnDb.DoctorId = cita.DoctorId;
                        citaEnDb.Asunto = cita.Asunto;
                        citaEnDb.FechaHoraInicio = cita.FechaHoraInicio;
                        citaEnDb.FechaHoraFin = cita.FechaHoraFin;
                        citaEnDb.TodoElDia = cita.TodoElDia;
                        citaEnDb.Ubicacion = cita.Ubicacion;
                        citaEnDb.Estado = cita.Estado;
                        citaEnDb.ReglaRecurrencia = cita.ReglaRecurrencia;
                        citaEnDb.IdCitaPadre = cita.IdCitaPadre;

                        _unitOfWork.Repository<TCita>().Editar(citaEnDb);
                    }
                }
                return _unitOfWork.Guardar();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Eliminar(int citaId)
        {
            var cita = _unitOfWork.Repository<TCita>().Consulta()
                            .FirstOrDefault(c => c.CitaId == citaId);
            if (cita != null)
            {
                cita.Eliminado = true;
                _unitOfWork.Repository<TCita>().Editar(cita);
                return _unitOfWork.Guardar();
            }
            return 0;
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
