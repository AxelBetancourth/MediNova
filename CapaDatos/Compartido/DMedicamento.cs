using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapaDatos.Compartido
{
    /// <summary>
    /// Capa de Datos para Medicamentos
    /// Usa UnitOfWork para todas las operaciones
    /// </summary>
    public class DMedicamento : IDisposable
    {
        private UnitOfWork _unitOfWork;

        public DMedicamento()
        {
            _unitOfWork = new UnitOfWork();
        }

        // Obtiene lista de medicamentos activos ordenados por nombre
        public List<TMedicamento> Listado()
        {
            return _unitOfWork.Repository<TMedicamento>().Consulta()
                .Where(m => !m.Eliminado)
                .OrderBy(m => m.Nombre)
                .ToList();
        }

        // Obtiene medicamentos disponibles con stock y no vencidos
        public List<TMedicamento> ListadoDisponibles()
        {
            return _unitOfWork.Repository<TMedicamento>().Consulta()
                .Where(m => !m.Eliminado && m.Stock > 0 && m.FechaVencimiento > DateTime.Now)
                .OrderBy(m => m.Nombre)
                .ToList();
        }

        // Obtiene medicamentos próximos a vencer según días especificados
        public List<TMedicamento> ListadoPorVencer(int dias = 30)
        {
            DateTime fechaLimite = DateTime.Now.AddDays(dias);
            return _unitOfWork.Repository<TMedicamento>().Consulta()
                .Where(m => !m.Eliminado && m.FechaVencimiento <= fechaLimite && m.FechaVencimiento > DateTime.Now)
                .OrderBy(m => m.FechaVencimiento)
                .ToList();
        }

        // Obtiene medicamentos con stock bajo según mínimo especificado
        public List<TMedicamento> ListadoBajoStock(int stockMinimo = 10)
        {
            return _unitOfWork.Repository<TMedicamento>().Consulta()
                .Where(m => !m.Eliminado && m.Stock <= stockMinimo && m.Stock > 0)
                .OrderBy(m => m.Stock)
                .ToList();
        }

        // Busca un medicamento por su identificador
        public TMedicamento BuscarPorId(int medicamentoId)
        {
            return _unitOfWork.Repository<TMedicamento>().Consulta()
                .FirstOrDefault(m => m.MedicamentoId == medicamentoId && !m.Eliminado);
        }

        // Busca medicamentos que contengan el nombre especificado
        public List<TMedicamento> BuscarPorNombre(string nombre)
        {
            return _unitOfWork.Repository<TMedicamento>().Consulta()
                .Where(m => m.Nombre.Contains(nombre) && !m.Eliminado)
                .OrderBy(m => m.Nombre)
                .ToList();
        }

        // Agrega un nuevo medicamento a la base de datos
        public int Agregar(TMedicamento medicamento)
        {
            try
            {
                medicamento.Eliminado = false;
                medicamento.MedicamentoId = 0;

                _unitOfWork.Repository<TMedicamento>().Agregar(medicamento);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DMedicamento.Agregar: {0}", ex.Message));
                throw;
            }
        }

        // Edita un medicamento existente
        public int Editar(TMedicamento medicamento)
        {
            try
            {
                var medicamentoExistente = _unitOfWork.Repository<TMedicamento>().Consulta()
                    .FirstOrDefault(m => m.MedicamentoId == medicamento.MedicamentoId);

                if (medicamentoExistente == null) return 0;

                medicamentoExistente.Nombre = medicamento.Nombre;
                medicamentoExistente.Presentacion = medicamento.Presentacion;
                medicamentoExistente.Dosis = medicamento.Dosis;
                medicamentoExistente.Proveedor = medicamento.Proveedor;
                medicamentoExistente.FechaVencimiento = medicamento.FechaVencimiento;
                medicamentoExistente.Stock = medicamento.Stock;
                medicamentoExistente.PrecioUnitario = medicamento.PrecioUnitario;

                _unitOfWork.Repository<TMedicamento>().Editar(medicamentoExistente);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DMedicamento.Editar: {0}", ex.Message));
                throw;
            }
        }

        // Actualiza el stock de un medicamento con la cantidad especificada
        public int ActualizarStock(int medicamentoId, int cantidad)
        {
            try
            {
                var medicamento = _unitOfWork.Repository<TMedicamento>().Consulta()
                    .FirstOrDefault(m => m.MedicamentoId == medicamentoId);

                if (medicamento == null) return 0;

                medicamento.Stock += cantidad;

                if (medicamento.Stock < 0)
                    throw new Exception("El stock no puede ser negativo.");

                _unitOfWork.Repository<TMedicamento>().Editar(medicamento);
                return _unitOfWork.Guardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DMedicamento.ActualizarStock: {0}", ex.Message));
                throw;
            }
        }

        // Realiza eliminación lógica de un medicamento
        public int Eliminar(int medicamentoId)
        {
            try
            {
                var medicamento = _unitOfWork.Repository<TMedicamento>().Consulta()
                    .FirstOrDefault(m => m.MedicamentoId == medicamentoId && !m.Eliminado);

                if (medicamento != null)
                {
                    medicamento.Eliminado = true;
                    _unitOfWork.Repository<TMedicamento>().Editar(medicamento);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error en DMedicamento.Eliminar: {0}", ex.Message));
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
