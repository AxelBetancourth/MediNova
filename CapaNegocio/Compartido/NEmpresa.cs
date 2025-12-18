using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.Compartido;
using System;

namespace CapaNegocio.Compartido
{
    public class NEmpresa : IDisposable
    {
        private DEmpresa dEmpresa;

        public NEmpresa()
        {
            dEmpresa = new DEmpresa();
        }

        // Obtiene la información de la empresa
        public TEmpresa ObtenerInformacion()
        {
            try
            {
                return dEmpresa.ObtenerInformacion();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener información de la empresa: {0}", ex.Message), ex);
            }
        }

        // Guarda o actualiza la información de la empresa
        public int GuardarInformacion(TEmpresa empresa)
        {
            try
            {
                ValidarDatosEmpresa(empresa);
                return dEmpresa.GuardarInformacion(empresa);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al guardar información de la empresa: {0}", ex.Message), ex);
            }
        }

        private void ValidarDatosEmpresa(TEmpresa empresa)
        {
            if (empresa == null)
                throw new Exception("La información de la empresa no puede ser nula.");

            if (string.IsNullOrWhiteSpace(empresa.NombreEmpresa))
                throw new Exception("El nombre de la empresa es obligatorio.");

            if (empresa.NombreEmpresa.Length > 200)
                throw new Exception("El nombre de la empresa no puede exceder 200 caracteres.");

            if (!string.IsNullOrWhiteSpace(empresa.RTN) && empresa.RTN.Length > 20)
                throw new Exception("El RTN no puede exceder 20 caracteres.");

            if (!string.IsNullOrWhiteSpace(empresa.Telefono) && empresa.Telefono.Length > 50)
                throw new Exception("El teléfono no puede exceder 50 caracteres.");

            if (!string.IsNullOrWhiteSpace(empresa.Email))
            {
                if (empresa.Email.Length > 100)
                    throw new Exception("El email no puede exceder 100 caracteres.");

                if (!empresa.Email.Contains("@"))
                    throw new Exception("El email no tiene un formato válido.");
            }
        }

        public void Dispose()
        {
            if (dEmpresa != null)
            {
                dEmpresa.Dispose();
            }
        }
    }
}
