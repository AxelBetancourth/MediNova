using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace CapaDatos.Compartido
{
    public class DDoctor : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DDoctor()
        {
            _unitOfWork = new UnitOfWork();
        }

        // Obtiene lista de doctores activos ordenados por nombre
        public List<TDoctor> Listado()
        {
            return _unitOfWork.Repository<TDoctor>().Consulta()
                .AsNoTracking()
                .Where(d => !d.Eliminado)
                .OrderBy(d => d.NombreCompleto)
                .ToList();
        }

        // Obtiene doctores con sus usuarios, roles y horarios incluidos
        public List<TDoctor> ListadoConUsuarios()
        {
            return _unitOfWork.Repository<TDoctor>().Consulta()
                .AsNoTracking()
                .Include(d => d.Usuario)
                .Include(d => d.Usuario.Rol)
                .Include(d => d.Horarios)
                .Where(d => !d.Eliminado)
                .OrderBy(d => d.NombreCompleto)
                .ToList();
        }

        // Busca un doctor por su identificador con horarios y usuario
        public TDoctor BuscarPorId(int doctorId)
        {
            return _unitOfWork.Repository<TDoctor>().Consulta()
                .AsNoTracking()
                .Include(d => d.Horarios)
                .Include(d => d.Usuario)
                .FirstOrDefault(d => d.DoctorId == doctorId && !d.Eliminado);
        }

        // Busca un doctor por su identificador de usuario
        public TDoctor BuscarPorUsuarioId(int usuarioId)
        {
            return _unitOfWork.Repository<TDoctor>().Consulta()
                .AsNoTracking()
                .Include(d => d.Usuario)
                .Include(d => d.Horarios)
                .FirstOrDefault(d => d.UsuarioId == usuarioId && !d.Eliminado);
        }

        // Asocia un usuario existente a un doctor
        public int AsociarUsuario(int doctorId, int usuarioId)
        {
            try
            {
                var doctor = _unitOfWork.Repository<TDoctor>().Consulta()
                    .FirstOrDefault(d => d.DoctorId == doctorId && !d.Eliminado);

                if (doctor != null)
                {
                    doctor.UsuarioId = usuarioId;
                    _unitOfWork.Repository<TDoctor>().Editar(doctor);
                    return _unitOfWork.Guardar();
                }

                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en AsociarUsuario: {0}", ex.Message));
                return 0;
            }
        }

        // Desasocia el usuario de un doctor
        public int DesasociarUsuario(int doctorId)
        {
            try
            {
                var doctor = _unitOfWork.Repository<TDoctor>().Consulta()
                    .FirstOrDefault(d => d.DoctorId == doctorId && !d.Eliminado);

                if (doctor != null)
                {
                    doctor.UsuarioId = null;
                    _unitOfWork.Repository<TDoctor>().Editar(doctor);
                    return _unitOfWork.Guardar();
                }

                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DesasociarUsuario: {0}", ex.Message));
                return 0;
            }
        }

        // Agrega un nuevo doctor con sus horarios
        public int Agregar(TDoctor doctor)
        {
            try
            {
                doctor.Eliminado = false;
                doctor.DoctorId = 0;

                if (doctor.Horarios != null)
                {
                    var nuevos = doctor.Horarios.Select(h => new THorario
                    {
                        DoctorId = 0,
                        DiaSemana = h.DiaSemana,
                        HoraInicio = h.HoraInicio,
                        HoraFin = h.HoraFin
                    }).ToList();

                    doctor.Horarios = nuevos;
                }

                _unitOfWork.Repository<TDoctor>().Agregar(doctor);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDoctor.Agregar: {0}", ex.Message));
                throw;
            }
        }

        // Actualiza las propiedades básicas de un doctor
        public int ActualizarPropiedades(TDoctor doctor)
        {
            try
            {
                var doctorExistente = _unitOfWork.Repository<TDoctor>().Consulta()
                    .FirstOrDefault(d => d.DoctorId == doctor.DoctorId);

                if (doctorExistente == null) return 0;

                doctorExistente.NombreCompleto = doctor.NombreCompleto;
                doctorExistente.Especialidad = doctor.Especialidad;
                doctorExistente.Telefono = doctor.Telefono;
                doctorExistente.Correo = doctor.Correo;
                doctorExistente.Disponible = doctor.Disponible;

                if (doctor.UsuarioId.HasValue)
                {
                    doctorExistente.UsuarioId = doctor.UsuarioId;
                }

                _unitOfWork.Repository<TDoctor>().Editar(doctorExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDoctor.ActualizarPropiedades: {0}", ex.Message));
                throw;
            }
        }

        // Edita un doctor y reemplaza completamente sus horarios
        public int EditarConHorarios(TDoctor doctorActualizado, List<THorario> nuevosHorarios)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    unitOfWork.ComenzarTransaccion();

                    var doctorExistente = unitOfWork.Repository<TDoctor>().Consulta()
                        .FirstOrDefault(d => d.DoctorId == doctorActualizado.DoctorId);

                    if (doctorExistente == null)
                        throw new Exception("Doctor no encontrado.");

                    doctorExistente.NombreCompleto = doctorActualizado.NombreCompleto;
                    doctorExistente.Especialidad = doctorActualizado.Especialidad;
                    doctorExistente.Telefono = doctorActualizado.Telefono;
                    doctorExistente.Correo = doctorActualizado.Correo;
                    doctorExistente.Disponible = doctorActualizado.Disponible;
                    doctorExistente.UsuarioId = doctorActualizado.UsuarioId;

                    unitOfWork.Repository<TDoctor>().Editar(doctorExistente);
                    unitOfWork.Guardar();

                    var horariosViejos = unitOfWork.Repository<THorario>().Consulta()
                        .Where(h => h.DoctorId == doctorActualizado.DoctorId)
                        .ToList();

                    foreach (var horario in horariosViejos)
                    {
                        unitOfWork.Repository<THorario>().Eliminar(horario);
                    }
                    unitOfWork.Guardar();

                    foreach (var horario in nuevosHorarios)
                    {
                        var nueva = new THorario
                        {
                            DoctorId = doctorActualizado.DoctorId,
                            DiaSemana = horario.DiaSemana,
                            HoraInicio = horario.HoraInicio,
                            HoraFin = horario.HoraFin
                        };
                        unitOfWork.Repository<THorario>().Agregar(nueva);
                    }
                    unitOfWork.Guardar();

                    unitOfWork.ConfirmarTransaccion();

                    return 1;
                }
                catch (Exception ex)
                {
                    unitOfWork.ReversarTransaccion();
                    System.Diagnostics.Debug.WriteLine(string.Format("Error en EditarConHorarios: {0}", ex.Message));
                    throw;
                }
            }
        }

        // Elimina todos los horarios de un doctor
        public int EliminarHorarios(int doctorId)
        {
            try
            {
                var horarios = _unitOfWork.Repository<THorario>().Consulta()
                    .Where(h => h.DoctorId == doctorId)
                    .ToList();

                foreach (var horario in horarios)
                {
                    _unitOfWork.Repository<THorario>().Eliminar(horario);
                }

                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDoctor.EliminarHorarios: {0}", ex.Message));
                throw;
            }
        }

        // Agrega múltiples horarios a un doctor
        public int AgregarHorarios(List<THorario> horarios)
        {
            try
            {
                foreach (var horario in horarios)
                {
                    var nueva = new THorario
                    {
                        DoctorId = horario.DoctorId,
                        DiaSemana = horario.DiaSemana,
                        HoraInicio = horario.HoraInicio,
                        HoraFin = horario.HoraFin
                    };
                    _unitOfWork.Repository<THorario>().Agregar(nueva);
                }

                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDoctor.AgregarHorarios: {0}", ex.Message));
                throw;
            }
        }

        // Realiza eliminación lógica de un doctor
        public int Eliminar(int doctorId)
        {
            try
            {
                var doctor = _unitOfWork.Repository<TDoctor>().Consulta()
                    .FirstOrDefault(d => d.DoctorId == doctorId && !d.Eliminado);

                if (doctor != null)
                {
                    doctor.Eliminado = true;
                    doctor.Disponible = false;
                    _unitOfWork.Repository<TDoctor>().Editar(doctor);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DDoctor.Eliminar: {0}", ex.Message));
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