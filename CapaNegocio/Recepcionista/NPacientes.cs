using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.Compartido;
using CapaDatos.Medico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Recepcionista
{
    public class NPacientes : IDisposable
    {
        private DPaciente dPaciente;
        private DExpediente dExpediente;

        public NPacientes()
        {
            dPaciente = new DPaciente();
            dExpediente = new DExpediente();
        }

        public List<TPaciente> ListadoActivos()
        {
            return dPaciente.Listado();
        }

        public TPaciente BuscarPorId(int pacienteId)
        {
            return dPaciente.BuscarPorId(pacienteId);
        }

        public void GuardarPaciente(TPaciente paciente)
        {
            try
            {
                // Guardar el paciente
                dPaciente.Guardar(paciente);

                // Crear expediente automáticamente para el nuevo paciente
                var expediente = new TExpediente
                {
                    PacienteId = paciente.PacienteId,
                    FechaApertura = DateTime.Now,
                    NumeroExpediente = GenerarNumeroExpediente(),
                    Eliminado = false
                };

                dExpediente.Agregar(expediente);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al guardar paciente y crear expediente: {0}", ex.Message), ex);
            }
        }

        private string GenerarNumeroExpediente()
        {
            return string.Format("EXP-{0}-{1}", DateTime.Now.Year, Guid.NewGuid().ToString().Substring(0, 6).ToUpper());
        }

        public void EditarPaciente(TPaciente paciente)
        {
            dPaciente.Editar(paciente);
        }

        public void EliminarPaciente(int pacienteId)
        {
            dPaciente.Eliminar(pacienteId);
        }

        public List<TPaciente> BuscarPorNombreODNI(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                throw new Exception("Debe ingresar un término de búsqueda.");

            return dPaciente.BuscarPorNombreODNI(termino);
        }

        /// <summary>
        /// Alias para BuscarPorNombreODNI - Buscar pacientes por nombre o DNI
        /// </summary>
        public List<TPaciente> BuscarPacientes(string termino)
        {
            return BuscarPorNombreODNI(termino);
        }

        public void Dispose()
        {
            if (dPaciente != null)
            {
                dPaciente.Dispose();
            }
            if (dExpediente != null)
            {
                dExpediente.Dispose();
            }
        }
    }
}
