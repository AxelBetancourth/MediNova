using CapaDatos.BaseDatos.Tablas;
using CapaDatos.BaseDatos.Tablas.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.ModuloLogin
{
    public static class SesionUsuario
    {
        public static TUsuario UsuarioActual { get; set; }
        public static TDoctor DoctorActual { get; set; }

        // Propiedad de conveniencia para acceder directamente al DoctorId
        public static int DoctorId
        {
            get
            {
                if (DoctorActual == null)
                    throw new InvalidOperationException("No hay un doctor asociado a la sesión actual.");
                return DoctorActual.DoctorId;
            }
        }

        public static void IniciarSesion(TUsuario usuario, TDoctor doctor = null)
        {
            UsuarioActual = usuario;
            DoctorActual = doctor;
        }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
            DoctorActual = null;
        }

        public static bool EsMedico()
        {
            return UsuarioActual != null && UsuarioActual.Rol != null && UsuarioActual.Rol.Nombre == "Medico";
        }

        public static bool TieneDoctorAsociado()
        {
            return DoctorActual != null;
        }
    }
}
