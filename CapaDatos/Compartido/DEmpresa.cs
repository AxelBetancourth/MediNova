using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Core;
using System;
using System.Data.Entity;
using System.Linq;

namespace CapaDatos.Compartido
{
    public class DEmpresa : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public DEmpresa()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Obtener la información de la empresa (siempre debe haber solo un registro)
        /// </summary>
        public TEmpresa ObtenerInformacion()
        {
            return _unitOfWork.Repository<TEmpresa>()
                .Consulta()
                .AsNoTracking()
                .FirstOrDefault(e => !e.Eliminado);
        }

        /// <summary>
        /// Guardar o actualizar información de la empresa
        /// </summary>
        public int GuardarInformacion(TEmpresa empresa)
        {
            var empresaExistente = _unitOfWork.Repository<TEmpresa>()
                .Consulta()
                .FirstOrDefault(e => !e.Eliminado);

            if (empresaExistente != null)
            {
                // Actualizar registro existente
                empresaExistente.NombreEmpresa = empresa.NombreEmpresa;
                empresaExistente.RTN = empresa.RTN;
                empresaExistente.Direccion = empresa.Direccion;
                empresaExistente.Telefono = empresa.Telefono;
                empresaExistente.Email = empresa.Email;
                empresaExistente.SitioWeb = empresa.SitioWeb;
                empresaExistente.Slogan = empresa.Slogan;
                empresaExistente.RepresentanteLegal = empresa.RepresentanteLegal;
                empresaExistente.FechaActualizacion = DateTime.Now;

                // Solo actualizar logo si se proporciona uno nuevo
                if (empresa.Logo != null && empresa.Logo.Length > 0)
                {
                    empresaExistente.Logo = empresa.Logo;
                }

                _unitOfWork.Guardar();
                return empresaExistente.EmpresaId;
            }
            else
            {
                // Crear nuevo registro
                empresa.FechaActualizacion = DateTime.Now;
                _unitOfWork.Repository<TEmpresa>().Agregar(empresa);
                _unitOfWork.Guardar();
                return empresa.EmpresaId;
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
