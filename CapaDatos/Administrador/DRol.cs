using CapaDatos.BaseDatos;
using CapaDatos.BaseDatos.Tablas.Login;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Administrador
{
    public class DRol
    {
        // Obtiene lista de roles activos de la base de datos
        public List<TRol> Listado()
        {
            try
            {
                using (var context = new MediNovaContext())
                {
                    return context.TRols
                                  .Where(r => !r.Eliminado)
                                  .ToList();
                }
            }
            catch
            {
                return new List<TRol>();
            }
        }

        // Obtiene todos los roles incluyendo los eliminados
        public List<TRol> ListadoCompleto()
        {
            try
            {
                using (var context = new MediNovaContext())
                {
                    return context.TRols.ToList();
                }
            }
            catch
            {
                return new List<TRol>();
            }
        }

        // Busca un rol por su identificador
        public TRol BuscarPorId(int id)
        {
            try
            {
                using (var context = new MediNovaContext())
                {
                    return context.TRols.FirstOrDefault(r => r.RolId == id);
                }
            }
            catch
            {
                return null;
            }
        }

        // Guarda un nuevo rol o actualiza uno existente
        public int Guardar(TRol rol)
        {
            using (var context = new MediNovaContext())
            {
                if (rol.RolId == 0)
                {
                    context.TRols.Add(rol);
                }
                else
                {
                    var rolExistente = context.TRols.FirstOrDefault(r => r.RolId == rol.RolId);
                    if (rolExistente != null)
                    {
                        rolExistente.Nombre = rol.Nombre;
                        rolExistente.Eliminado = rol.Eliminado;
                    }
                }
                return context.SaveChanges();
            }
        }

        // Realiza eliminación lógica de un rol
        public int Eliminar(int rolId)
        {
            using (var context = new MediNovaContext())
            {
                var rol = context.TRols.FirstOrDefault(r => r.RolId == rolId);
                if (rol != null)
                {
                    rol.Eliminado = true;
                    return context.SaveChanges();
                }
                return 0;
            }
        }
    }
}
