using CapaDatos.BaseDatos;
using CapaDatos.BaseDatos.Tablas;
using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.Login;
using CapaDatos.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Administrador
{
    public class DUsuario
    {

        UnitOfWork _unitOfWork;

        public DUsuario()
        {
            _unitOfWork = new UnitOfWork();
        }

        // Obtiene lista de usuarios activos con sus roles
        public List<TUsuario> Listado()
        {
            return _unitOfWork.Repository<TUsuario>().Consulta()
                                         .Include(u => u.Rol)
                                         .Where(u => !u.Eliminado)
                                         .ToList();
        }

        // Obtiene lista de usuarios eliminados con sus roles
        public List<TUsuario> ListadoEliminado()
        {
            return _unitOfWork.Repository<TUsuario>().Consulta()
                                         .Include(u => u.Rol)
                                         .Where(u => u.Eliminado)
                                         .ToList();
        }

        // Busca un usuario por su nombre de usuario
        public TUsuario BuscarPorNombre(string nombreUsuario)
        {
            try
            {
                using (var context = new MediNovaContext())
                {
                    return context.TUsuarios
                                  .Include(u => u.Rol)
                                  .FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
                }
            }
            catch
            {
                return null;
            }
        }

        // Guarda un nuevo usuario o actualiza uno existente
        public int Guardar(TUsuario usuario)
        {
            try
            {
                if (usuario.UsuarioId == 0)
                {
                    usuario.Rol = null;
                    _unitOfWork.Repository<TUsuario>().Agregar(usuario);
                }
                else
                {
                    var userInDb = _unitOfWork.Repository<TUsuario>().Consulta()
                                          .FirstOrDefault(u => u.UsuarioId == usuario.UsuarioId);
                    if (userInDb != null)
                    {
                        userInDb.NombreUsuario = usuario.NombreUsuario;
                        if (!string.IsNullOrEmpty(usuario.Contrasena))
                            userInDb.Contrasena = usuario.Contrasena;
                        userInDb.RolId = usuario.RolId;
                        userInDb.Estado = usuario.Estado;
                        userInDb.HuellaDactilar = usuario.HuellaDactilar;
                        userInDb.UltimoAcceso = usuario.UltimoAcceso;

                        _unitOfWork.Repository<TUsuario>().Editar(userInDb);
                    }
                }

                return _unitOfWork.Guardar();
            }
            catch
            {
                return 0;
            }
        }

        // Realiza eliminación lógica de un usuario
        public int Eliminar(int usuarioId)
        {
            var usuario = _unitOfWork.Repository<TUsuario>().Consulta()
                                .FirstOrDefault(u => u.UsuarioId == usuarioId);
            if (usuario != null)
            {
                usuario.Eliminado = true;
                _unitOfWork.Repository<TUsuario>().Editar(usuario);
                return _unitOfWork.Guardar();
            }
            return 0;
        }

        // Recupera un usuario eliminado previamente
        public int Recuperar(int usuarioId)
        {
            try
            {
                var usuario = _unitOfWork.Repository<TUsuario>().Consulta()
                                    .FirstOrDefault(u => u.UsuarioId == usuarioId);
                if (usuario != null)
                {
                    usuario.Eliminado = false;
                    _unitOfWork.Repository<TUsuario>().Editar(usuario);
                    return _unitOfWork.Guardar();
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
