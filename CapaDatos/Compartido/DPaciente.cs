using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Compartido
{
    public class DPaciente : IDisposable
    {
        UnitOfWork _unitOfWork;

        public DPaciente()
        {
            _unitOfWork = new UnitOfWork();
        }

        // Obtiene lista de pacientes activos ordenados por nombre
        public List<TPaciente> Listado()
        {
            return _unitOfWork.Repository<TPaciente>().Consulta()
                .Where(p => !p.Eliminado)
                .OrderBy(p => p.NombreCompleto)
                .ToList();
        }

        // Busca un paciente por su identificador
        public TPaciente BuscarPorId(int pacienteId)
        {
            return _unitOfWork.Repository<TPaciente>().Consulta()
                .FirstOrDefault(p => p.PacienteId == pacienteId && !p.Eliminado);
        }

        // Guarda un nuevo paciente en la base de datos
        public void Guardar(TPaciente paciente)
        {
            _unitOfWork.Repository<TPaciente>().Agregar(paciente);
            _unitOfWork.Guardar();
        }

        // Edita un paciente existente
        public void Editar(TPaciente paciente)
        {
            _unitOfWork.Repository<TPaciente>().Editar(paciente);
            _unitOfWork.Guardar();
        }

        // Realiza eliminación lógica de un paciente
        public void Eliminar(int pacienteId)
        {
            var paciente = BuscarPorId(pacienteId);
            if (paciente != null)
            {
                paciente.Eliminado = true;
                Editar(paciente);
            }
        }

        // Busca pacientes por nombre completo o DNI
        public List<TPaciente> BuscarPorNombreODNI(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return new List<TPaciente>();

            termino = termino.ToLower().Trim();
            string terminoSinGuiones = termino.Replace("-", "").Replace(" ", "");

            return _unitOfWork.Repository<TPaciente>().Consulta()
                .Where(p => !p.Eliminado &&
                       (p.NombreCompleto.ToLower().Contains(termino) ||
                        p.DNI.Contains(termino) ||
                        p.DNI.Replace("-", "").Replace(" ", "").Contains(terminoSinGuiones)))
                .OrderBy(p => p.NombreCompleto)
                .ToList();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
