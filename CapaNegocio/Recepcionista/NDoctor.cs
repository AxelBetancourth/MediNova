using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Compartido;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaNegocio.Recepcionista
{
    public class NDoctor : IDisposable
    {
        private DDoctor dDoctor;

        public NDoctor()
        {
            dDoctor = new DDoctor();
        }

        public List<TDoctor> ListarDoctores()
        {
            try
            {
                return dDoctor.ListadoConUsuarios();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar doctores: {0}", ex.Message), ex);
            }
        }

        public TDoctor BuscarPorId(int doctorId)
        {
            try
            {
                if (doctorId <= 0)
                    throw new Exception("ID de doctor no válido.");

                return dDoctor.BuscarPorId(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar doctor: {0}", ex.Message), ex);
            }
        }

        public int RegistrarDoctor(TDoctor doctor, List<THorario> horarios)
        {
            try
            {
                ValidarDatosDoctor(doctor);
                ValidarHorarios(horarios);

                doctor.Horarios = horarios;
                doctor.Eliminado = false;
                doctor.DoctorId = 0;

                return dDoctor.Agregar(doctor);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar doctor: {0}", ex.Message), ex);
            }
        }

        public int EditarDoctor(TDoctor doctorActualizado, List<THorario> nuevosHorarios)
        {
            try
            {
                if (doctorActualizado.DoctorId <= 0)
                    throw new Exception("ID de doctor no válido para editar.");

                ValidarDatosDoctor(doctorActualizado);
                ValidarHorarios(nuevosHorarios);

                var doctorExistente = dDoctor.BuscarPorId(doctorActualizado.DoctorId);
                if (doctorExistente == null)
                    throw new Exception("Doctor no encontrado en la base de datos.");

                if (!doctorActualizado.UsuarioId.HasValue && doctorExistente.UsuarioId.HasValue)
                {
                    doctorActualizado.UsuarioId = doctorExistente.UsuarioId;
                }

                // Delegar a la capa de datos que realiza la operación en una sola transacción
                int resultado = dDoctor.EditarConHorarios(doctorActualizado, nuevosHorarios);

                if (resultado == 0)
                    throw new Exception("No se pudieron guardar los cambios del doctor y sus horarios.");

                return resultado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("========== ERROR EN NDoctor.EditarDoctor ==========");
                System.Diagnostics.Debug.WriteLine(string.Format("Mensaje: {0}", ex.Message));
                System.Diagnostics.Debug.WriteLine(string.Format("StackTrace: {0}", ex.StackTrace));

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("InnerException: {0}", ex.InnerException.Message));
                }

                throw new Exception(string.Format("Error al editar doctor: {0}", ex.Message), ex);
            }
        }

        public int EliminarDoctor(int doctorId)
        {
            try
            {
                if (doctorId <= 0)
                    throw new Exception("ID de doctor no válido.");
                var doctor = dDoctor.BuscarPorId(doctorId);
                if (doctor == null)
                    throw new Exception("Doctor no encontrado.");

                return dDoctor.Eliminar(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar doctor: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosDoctor(TDoctor doctor)
        {
            if (doctor == null)
                throw new Exception("El objeto doctor no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(doctor.NombreCompleto))
                throw new Exception("El nombre del doctor es obligatorio.");

            if (doctor.NombreCompleto.Length > 100)
                throw new Exception("El nombre del doctor no puede exceder 100 caracteres.");

            if (string.IsNullOrWhiteSpace(doctor.Especialidad))
                throw new Exception("La especialidad es obligatoria.");

            if (doctor.Especialidad.Length > 50)
                throw new Exception("La especialidad no puede exceder 50 caracteres.");

            if (!string.IsNullOrWhiteSpace(doctor.Telefono))
            {
                if (doctor.Telefono.Length > 20)
                    throw new Exception("El teléfono no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(doctor.Correo))
            {
                if (doctor.Correo.Length > 100)
                    throw new Exception("El correo no puede exceder 100 caracteres.");

                if (!doctor.Correo.Contains("@"))
                    throw new Exception("El formato del correo electrónico no es válido.");
            }
        }


        private void ValidarHorarios(List<THorario> horarios)
        {
            if (horarios == null || !horarios.Any())
                throw new Exception("Debe agregar al menos un horario para el doctor.");

            foreach (var horario in horarios)
            {
                if (horario.HoraFin <= horario.HoraInicio)
                    throw new Exception(string.Format("El horario del {0} tiene una hora final menor o igual a la inicial.", TraducirDia(horario.DiaSemana)));

                if (horario.HoraInicio.TotalHours >= 24 || horario.HoraFin.TotalHours > 24)
                    throw new Exception(string.Format("El horario del {0} contiene horas inválidas.", TraducirDia(horario.DiaSemana)));
            }

            var horariosPorDia = horarios.GroupBy(h => h.DiaSemana);
            foreach (var grupo in horariosPorDia)
            {
                var horariosDelDia = grupo.OrderBy(h => h.HoraInicio).ToList();

                for (int i = 0; i < horariosDelDia.Count - 1; i++)
                {
                    if (horariosDelDia[i].HoraFin > horariosDelDia[i + 1].HoraInicio)
                    {
                        throw new Exception(string.Format("Los horarios del {0} se solapan entre sí.", TraducirDia(grupo.Key)));
                    }
                }
            }
        }

        private string TraducirDia(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábado";
                case DayOfWeek.Sunday: return "Domingo";
                default: return dia.ToString();
            }
        }

        public void Dispose()
        {
            if (dDoctor != null)
            {
                dDoctor.Dispose();
            }
        }
    }
}