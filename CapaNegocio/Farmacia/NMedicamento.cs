using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Compartido;
using System;
using System.Collections.Generic;

namespace CapaNegocio.Farmacia
{
    public class NMedicamento : IDisposable
    {
        private DMedicamento dMedicamento;

        public NMedicamento()
        {
            dMedicamento = new DMedicamento();
        }

        // Lista todos los medicamentos
        public List<TMedicamento> ListarMedicamentos()
        {
            try
            {
                return dMedicamento.Listado();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar medicamentos: {0}", ex.Message), ex);
            }
        }

        // Lista medicamentos disponibles en stock
        public List<TMedicamento> ListarMedicamentosDisponibles()
        {
            try
            {
                return dMedicamento.ListadoDisponibles();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar medicamentos disponibles: {0}", ex.Message), ex);
            }
        }

        // Lista medicamentos próximos a vencer
        public List<TMedicamento> ListarMedicamentosPorVencer(int dias = 30)
        {
            try
            {
                return dMedicamento.ListadoPorVencer(dias);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar medicamentos por vencer: {0}", ex.Message), ex);
            }
        }

        // Lista medicamentos con stock bajo
        public List<TMedicamento> ListarMedicamentosBajoStock(int stockMinimo = 10)
        {
            try
            {
                return dMedicamento.ListadoBajoStock(stockMinimo);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar medicamentos con bajo stock: {0}", ex.Message), ex);
            }
        }

        // Busca un medicamento por su ID
        public TMedicamento BuscarPorId(int medicamentoId)
        {
            try
            {
                if (medicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                return dMedicamento.BuscarPorId(medicamentoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar medicamento: {0}", ex.Message), ex);
            }
        }

        // Busca medicamentos por nombre
        public List<TMedicamento> BuscarPorNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new Exception("El nombre no puede estar vacío.");

                return dMedicamento.BuscarPorNombre(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al buscar medicamentos: {0}", ex.Message), ex);
            }
        }

        // Registra un nuevo medicamento
        public int RegistrarMedicamento(TMedicamento medicamento)
        {
            try
            {
                ValidarDatosMedicamento(medicamento);

                medicamento.Eliminado = false;

                return dMedicamento.Agregar(medicamento);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al registrar medicamento: {0}", ex.Message), ex);
            }
        }

        // Edita un medicamento existente
        public int EditarMedicamento(TMedicamento medicamento)
        {
            try
            {
                if (medicamento.MedicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                ValidarDatosMedicamento(medicamento);

                var medicamentoExistente = dMedicamento.BuscarPorId(medicamento.MedicamentoId);
                if (medicamentoExistente == null)
                    throw new Exception("Medicamento no encontrado.");

                return dMedicamento.Editar(medicamento);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al editar medicamento: {0}", ex.Message), ex);
            }
        }

        // Actualiza el stock de un medicamento
        public int ActualizarStock(int medicamentoId, int cantidad)
        {
            try
            {
                if (medicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                var medicamento = dMedicamento.BuscarPorId(medicamentoId);
                if (medicamento == null)
                    throw new Exception("Medicamento no encontrado.");

                return dMedicamento.ActualizarStock(medicamentoId, cantidad);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al actualizar stock: {0}", ex.Message), ex);
            }
        }

        // Elimina un medicamento (marcado como eliminado)
        public int EliminarMedicamento(int medicamentoId)
        {
            try
            {
                if (medicamentoId <= 0)
                    throw new Exception("ID de medicamento no válido.");

                var medicamento = dMedicamento.BuscarPorId(medicamentoId);
                if (medicamento == null)
                    throw new Exception("Medicamento no encontrado.");

                return dMedicamento.Eliminar(medicamentoId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al eliminar medicamento: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosMedicamento(TMedicamento medicamento)
        {
            if (medicamento == null)
                throw new Exception("El medicamento no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(medicamento.Nombre))
                throw new Exception("El nombre del medicamento es obligatorio.");

            if (medicamento.Nombre.Length > 100)
                throw new Exception("El nombre del medicamento no puede exceder 100 caracteres.");

            if (medicamento.FechaVencimiento <= DateTime.Now)
                throw new Exception("La fecha de vencimiento debe ser futura.");

            if (medicamento.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");

            if (medicamento.PrecioUnitario <= 0)
                throw new Exception("El precio unitario debe ser mayor a cero.");
        }

        public void Dispose()
        {
            if (dMedicamento != null)
            {
                dMedicamento.Dispose();
            }
        }
    }
}
