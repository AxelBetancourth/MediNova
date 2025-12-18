using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Medico
{
    /// <summary>
    /// Capa de Datos para Consultas Médicas
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DConsulta : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DConsulta()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Obtener todas las consultas activas
        /// </summary>
        public List<TConsulta> Listado()
        {
            return _unitOfWork.Repository<TConsulta>().Consulta()
                .Where(c => !c.Eliminado)
                .OrderByDescending(c => c.FechaConsulta)
                .ToList();
        }

        /// <summary>
        /// Obtener consultas con todas las relaciones cargadas
        /// </summary>
        public List<TConsulta> ListadoConRelaciones()
        {
            return _unitOfWork.Repository<TConsulta>().Consulta()
                .AsNoTracking()
                .Include(c => c.Expediente)
                .Include(c => c.Expediente.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Cita)
                .Include(c => c.Enfermedad)
                .Include(c => c.Recetas)
                .Include(c => c.Diagnosticos)
                .Where(c => !c.Eliminado)
                .OrderByDescending(c => c.FechaConsulta)
                .ToList();
        }

        /// <summary>
        /// Buscar consulta por ID
        /// </summary>
        public TConsulta BuscarPorId(int consultaId)
        {
            return _unitOfWork.Repository<TConsulta>().Consulta()
                .AsNoTracking()
                .Include(c => c.Expediente)
                .Include(c => c.Expediente.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Cita)
                .Include(c => c.Enfermedad)
                .Include(c => c.Recetas)
                .Include(c => c.Diagnosticos)
                .FirstOrDefault(c => c.ConsultaId == consultaId && !c.Eliminado);
        }

        /// <summary>
        /// Obtener todas las consultas de un expediente específico
        /// </summary>
        public List<TConsulta> BuscarPorExpedienteId(int expedienteId)
        {
            return _unitOfWork.Repository<TConsulta>().Consulta()
                .AsNoTracking()
                .Include(c => c.Doctor)
                .Include(c => c.Cita)
                .Include(c => c.Enfermedad)
                .Include(c => c.Recetas)
                .Include(c => c.Diagnosticos)
                .Where(c => c.ExpedienteId == expedienteId && !c.Eliminado)
                .OrderByDescending(c => c.FechaConsulta)
                .ToList();
        }

        /// <summary>
        /// Obtener consulta asociada a una cita
        /// </summary>
        public TConsulta BuscarPorCitaId(int citaId)
        {
            return _unitOfWork.Repository<TConsulta>().Consulta()
                .AsNoTracking()
                .Include(c => c.Expediente)
                .Include(c => c.Expediente.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Enfermedad)
                .FirstOrDefault(c => c.CitaId == citaId && !c.Eliminado);
        }

        /// <summary>
        /// Obtener consultas de un paciente específico
        /// </summary>
        public List<TConsulta> BuscarPorPacienteId(int pacienteId)
        {
            return _unitOfWork.Repository<TConsulta>().Consulta()
                .AsNoTracking()
                .Include(c => c.Doctor)
                .Include(c => c.Cita)
                .Include(c => c.Enfermedad)
                .Where(c => c.Expediente.PacienteId == pacienteId && !c.Eliminado)
                .OrderByDescending(c => c.FechaConsulta)
                .ToList();
        }

        /// <summary>
        /// Obtener consultas por rango de fechas
        /// </summary>
        public List<TConsulta> BuscarPorRangoFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            return _unitOfWork.Repository<TConsulta>().Consulta()
                .AsNoTracking()
                .Include(c => c.Expediente.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.FechaConsulta >= fechaInicio &&
                           c.FechaConsulta <= fechaFin &&
                           !c.Eliminado)
                .OrderBy(c => c.FechaConsulta)
                .ToList();
        }

        /// <summary>
        /// Generar número de consulta automático
        /// </summary>
        public string GenerarNumeroConsulta()
        {
            var year = DateTime.Now.Year;
            // Construir el prefijo ANTES de la consulta LINQ
            string prefijo = "CON-" + year;

            var ultimaConsulta = _unitOfWork.Repository<TConsulta>().Consulta()
                .Where(c => c.NumeroConsulta.StartsWith(prefijo))
                .OrderByDescending(c => c.ConsultaId)
                .FirstOrDefault();

            int nuevoNumero = 1;
            if (ultimaConsulta != null)
            {
                var partes = ultimaConsulta.NumeroConsulta.Split('-');
                int numero;
                if (partes.Length == 3 && int.TryParse(partes[2], out numero))
                {
                    nuevoNumero = numero + 1;
                }
            }

            return string.Format("CON-{0}-{1:D4}", year, nuevoNumero);
        }

        /// <summary>
        /// Agregar nueva consulta
        /// </summary>
        public int Agregar(TConsulta consulta)
        {
            try
            {
                consulta.Eliminado = false;
                consulta.FechaConsulta = DateTime.Now;

                // Generar número de consulta si no está asignado
                if (string.IsNullOrWhiteSpace(consulta.NumeroConsulta))
                {
                    consulta.NumeroConsulta = GenerarNumeroConsulta();
                }

                // Asegurar que campos requeridos tengan valores por defecto
                if (string.IsNullOrWhiteSpace(consulta.Estado))
                {
                    consulta.Estado = "EnProgreso";
                }

                if (string.IsNullOrWhiteSpace(consulta.EstadoPago))
                {
                    consulta.EstadoPago = "Pendiente";
                }

                _unitOfWork.Repository<TConsulta>().Agregar(consulta);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DConsulta.Agregar: {0}", ex.Message));
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Inner Exception: {0}", ex.InnerException.Message));
                }
                throw;
            }
        }

        /// <summary>
        /// Editar consulta existente
        /// </summary>
        public int Editar(TConsulta consulta)
        {
            try
            {
                var consultaExistente = _unitOfWork.Repository<TConsulta>().Consulta()
                    .FirstOrDefault(c => c.ConsultaId == consulta.ConsultaId);

                if (consultaExistente == null) return 0;

                // Actualizar campos
                consultaExistente.MotivoConsulta = consulta.MotivoConsulta;
                consultaExistente.Sintomas = consulta.Sintomas;
                consultaExistente.PresionArterial = consulta.PresionArterial;
                consultaExistente.Temperatura = consulta.Temperatura;
                consultaExistente.FrecuenciaCardiaca = consulta.FrecuenciaCardiaca;
                consultaExistente.FrecuenciaRespiratoria = consulta.FrecuenciaRespiratoria;
                consultaExistente.Peso = consulta.Peso;
                consultaExistente.Altura = consulta.Altura;
                consultaExistente.IMC = consulta.IMC;
                consultaExistente.Saturacion = consulta.Saturacion;
                consultaExistente.Diagnostico = consulta.Diagnostico;
                consultaExistente.EnfermedadId = consulta.EnfermedadId;
                consultaExistente.Tratamiento = consulta.Tratamiento;
                consultaExistente.Observaciones = consulta.Observaciones;
                consultaExistente.IndicacionesMedicas = consulta.IndicacionesMedicas;
                consultaExistente.CostoConsulta = consulta.CostoConsulta;
                consultaExistente.EstadoPago = consulta.EstadoPago;
                consultaExistente.FechaPago = consulta.FechaPago;
                consultaExistente.VentaId = consulta.VentaId;
                consultaExistente.ProximaCita = consulta.ProximaCita;
                consultaExistente.NotasProximaCita = consulta.NotasProximaCita;
                consultaExistente.Estado = consulta.Estado;

                _unitOfWork.Repository<TConsulta>().Editar(consultaExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DConsulta.Editar: {0}", ex.Message));
                throw;
            }
        }

        /// <summary>
        /// Eliminar consulta (soft delete)
        /// </summary>
        public int Eliminar(int consultaId)
        {
            try
            {
                var consulta = _unitOfWork.Repository<TConsulta>().Consulta()
                    .FirstOrDefault(c => c.ConsultaId == consultaId && !c.Eliminado);

                if (consulta != null)
                {
                    consulta.Eliminado = true;
                    _unitOfWork.Repository<TConsulta>().Editar(consulta);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DConsulta.Eliminar: {0}", ex.Message));
                return 0;
            }
        }

        /// <summary>
        /// Finalizar consulta
        /// </summary>
        public int FinalizarConsulta(int consultaId)
        {
            try
            {
                var consulta = _unitOfWork.Repository<TConsulta>().Consulta()
                    .FirstOrDefault(c => c.ConsultaId == consultaId && !c.Eliminado);

                if (consulta != null)
                {
                    consulta.Estado = "Finalizada";
                    _unitOfWork.Repository<TConsulta>().Editar(consulta);

                    // Si la consulta tiene una cita asociada, marcarla como Completada
                    if (consulta.CitaId.HasValue)
                    {
                        var cita = _unitOfWork.Repository<CapaDatos.BaseDatos.Tablas.ControlCitas.TCita>()
                            .Consulta()
                            .FirstOrDefault(c => c.CitaId == consulta.CitaId.Value && !c.Eliminado);

                        if (cita != null)
                        {
                            cita.Estado = "Completada";
                            _unitOfWork.Repository<CapaDatos.BaseDatos.Tablas.ControlCitas.TCita>().Editar(cita);
                        }
                    }

                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DConsulta.FinalizarConsulta: {0}", ex.Message));
                return 0;
            }
        }

        /// <summary>
        /// Registrar pago de consulta
        /// </summary>
        public int RegistrarPago(int consultaId)
        {
            try
            {
                var consulta = _unitOfWork.Repository<TConsulta>().Consulta()
                    .FirstOrDefault(c => c.ConsultaId == consultaId && !c.Eliminado);

                if (consulta != null)
                {
                    consulta.EstadoPago = "Pagado";
                    consulta.FechaPago = DateTime.Now;
                    _unitOfWork.Repository<TConsulta>().Editar(consulta);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DConsulta.RegistrarPago: {0}", ex.Message));
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
