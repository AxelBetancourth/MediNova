using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.Medico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Recepcionista
{
    public class NCitas : IDisposable
    {
        private DCitas dCitas;
        private NDoctor nDoctor;
        private NPacientes nPaciente;

        public NCitas()
        {
            dCitas = new DCitas();
            nDoctor = new NDoctor();
            nPaciente = new NPacientes();
        }

        public List<TCita> ObtenerCitasPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaFin < fechaInicio)
                throw new Exception("La fecha de fin no puede ser anterior a la fecha de inicio.");

            return dCitas.ListadoPorRangoFechas(fechaInicio, fechaFin);
        }

        public TCita ObtenerCitaPorId(int citaId)
        {
            if (citaId <= 0) return null;
            return dCitas.BuscarPorId(citaId);
        }

        public int GuardarCita(TCita cita)
        {
            // --- 1. VALIDACIONES BÁSICAS ---
            if (cita.PacienteId <= 0)
                throw new Exception("Debe seleccionar un paciente.");
            if (cita.DoctorId <= 0)
                throw new Exception("Debe seleccionar un doctor.");
            if (string.IsNullOrWhiteSpace(cita.Asunto))
                throw new Exception("El 'Asunto' de la cita no puede estar vacío.");

            if (!cita.TodoElDia && cita.FechaHoraFin <= cita.FechaHoraInicio)
                throw new Exception("La fecha/hora de fin debe ser posterior a la fecha/hora de inicio.");

            // Asumimos que una cita no puede durar más de un día
            if (cita.FechaHoraInicio.Date != cita.FechaHoraFin.Date)
                throw new Exception("La cita debe empezar y terminar el mismo día.");

            // --- 2. VALIDACIÓN DE DISPONIBILIDAD Y HORARIO ---
            var doctor = nDoctor.BuscarPorId(cita.DoctorId);
            if (doctor == null)
                throw new Exception("El doctor seleccionado no existe.");
            if (!doctor.Disponible)
                throw new Exception(string.Format("El doctor {0} no se encuentra disponible.", doctor.NombreCompleto));

            if (nPaciente.BuscarPorId(cita.PacienteId) == null)
                throw new Exception("El paciente seleccionado no existe.");

            // --- 🚀 NUEVA VALIDACIÓN DE HORARIO 🚀 ---
            if (!cita.TodoElDia)
            {
                if (!EsHorarioLaboralValido(doctor, cita.FechaHoraInicio, cita.FechaHoraFin))
                {
                    throw new Exception("La hora seleccionada está fuera del horario laboral del doctor.");
                }
            }

            // --- 3. VALIDACIÓN DE CONFLICTOS DE CITAS ---
            if (HayConflictoDeCitas(cita))
            {
                throw new Exception("Conflicto de horario: El doctor o el paciente ya tienen una cita en ese rango.");
            }

            return dCitas.Guardar(cita);
        }

        public int EliminarCita(int citaId)
        {
            if (citaId <= 0)
                throw new Exception("ID de cita no válido.");

            return dCitas.Eliminar(citaId);
        }

        public int CancelarCita(int citaId)
        {
            if (citaId <= 0)
                throw new Exception("ID de cita no válido.");

            var cita = dCitas.BuscarPorId(citaId);
            if (cita == null)
                throw new Exception("La cita no existe.");

            if (cita.Estado.ToLower() == "cancelada")
                throw new Exception("La cita ya está cancelada.");

            if (cita.Estado.ToLower() == "completada")
                throw new Exception("No se puede cancelar una cita completada.");

            cita.Estado = "Cancelada";
            return dCitas.Guardar(cita);
        }

        /// <summary>
        /// NUEVO: Valida si la cita cae dentro del horario de trabajo del doctor.
        /// </summary>
        private bool EsHorarioLaboralValido(TDoctor doctor, DateTime inicioCita, DateTime finCita)
        {
            // Si el doctor no tiene horarios definidos, no puede aceptar citas
            if (doctor.Horarios == null || !doctor.Horarios.Any())
            {
                return false;
            }

            DayOfWeek diaDeCita = inicioCita.DayOfWeek;
            TimeSpan horaInicioCita = inicioCita.TimeOfDay;
            TimeSpan horaFinCita = finCita.TimeOfDay;

            // Buscar el horario para ESE día de la semana
            var horarioDelDia = doctor.Horarios
                                    .FirstOrDefault(h => h.DiaSemana == diaDeCita);

            if (horarioDelDia == null)
            {
                return false; // El doctor no trabaja ese día.
            }

            // Validar que la cita esté COMPLETAMENTE DENTRO del horario laboral
            bool dentroDeHorario = (horaInicioCita >= horarioDelDia.HoraInicio &&
                                    horaFinCita <= horarioDelDia.HoraFin);

            return dentroDeHorario;
        }

        /// Validar si existen OTRAS citas para el mismo doctor o paciente.
        private bool HayConflictoDeCitas(TCita nuevaCita)
        {
            // Obtenemos citas que se solapan en tiempo, excluyendo la cita actual (si se edita)
            var citasEnRango = dCitas.ListadoPorRangoFechas(nuevaCita.FechaHoraInicio, nuevaCita.FechaHoraFin)
                                     .Where(c => c.CitaId != nuevaCita.CitaId && !c.Eliminado)
                                     .ToList();

            // 1. Revisar si el DOCTOR está ocupado
            bool doctorOcupado = citasEnRango.Any(c => c.DoctorId == nuevaCita.DoctorId);
            if (doctorOcupado) return true;

            // 2. Revisar si el PACIENTE está ocupado
            bool pacienteOcupado = citasEnRango.Any(c => c.PacienteId == nuevaCita.PacienteId);
            if (pacienteOcupado) return true;

            return false;
        }

        public void Dispose()
        {
            if (dCitas != null)
            {
                dCitas.Dispose();
            }
            if (nDoctor != null)
            {
                nDoctor.Dispose();
            }
            if (nPaciente != null)
            {
                nPaciente.Dispose();
            }
        }
    }
}
