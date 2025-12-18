using CapaDatos.BaseDatos;
using CapaDatos.BaseDatos.Tablas;
using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.Login;
using CapaDatos.Compartido;
using CapaDatos.Administrador;
using CapaNegocio.Recepcionista;
using DPFP;
using DPFP.Verification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Administrador
{
    public class NLogin
    {
        private DUsuario dusuario;
        private DRol drol;
        private DDoctor ddoctor;

        public NLogin()
        {
            dusuario = new DUsuario();
            drol = new DRol();
            ddoctor = new DDoctor();
        }

        // Obtiene la lista de usuarios activos
        public List<TUsuario> ListadoUsuarios()
        {
            return dusuario.Listado();
        }

        // Obtiene la lista de usuarios eliminados
        public List<TUsuario> ListadoUsuariosEliminado()
        {
            return dusuario.ListadoEliminado();
        }

        // Registra un nuevo usuario en el sistema
        public int RegistrarUsuario(string nombreUsuario, string contrasena, int rolId, byte[] huellaDactilar)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("El nombre de usuario no puede estar vacío");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("La contraseña no puede estar vacía");

            if (rolId <= 0)
                throw new Exception("Debe seleccionar un rol válido");

            var usuarioExistente = dusuario.BuscarPorNombre(nombreUsuario);
            if (usuarioExistente != null && !usuarioExistente.Eliminado)
                throw new Exception(string.Format("El usuario '{0}' ya existe", nombreUsuario));

            var nuevoUsuario = new TUsuario
            {
                NombreUsuario = nombreUsuario,
                Contrasena = GenerarHash(contrasena),
                RolId = rolId,
                HuellaDactilar = huellaDactilar,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Eliminado = false,
                UltimoAcceso = DateTime.Now,
                Rol = null
            };

            return dusuario.Guardar(nuevoUsuario);
        }

        // Edita un usuario existente
        public int EditarUsuario(int usuarioId, string nombreUsuario, string contrasena, int rolId, byte[] huellaDactilar)
        {
            if (usuarioId <= 0)
                throw new Exception("ID de usuario no válido");

            var usuarioExistente = dusuario.BuscarPorNombre(nombreUsuario);
            if (usuarioExistente == null || usuarioExistente.UsuarioId != usuarioId)
            {
                usuarioExistente = dusuario.Listado().FirstOrDefault(u => u.UsuarioId == usuarioId);
                if (usuarioExistente == null)
                    throw new Exception("Usuario no encontrado");
            }

            usuarioExistente.NombreUsuario = nombreUsuario;
            usuarioExistente.RolId = rolId;
            usuarioExistente.HuellaDactilar = huellaDactilar;

            if (!string.IsNullOrWhiteSpace(contrasena))
                usuarioExistente.Contrasena = GenerarHash(contrasena);

            return dusuario.Guardar(usuarioExistente);
        }

        // Elimina un usuario (marcado como eliminado)
        public int EliminarUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new Exception("ID de usuario no válido");

            return dusuario.Eliminar(usuarioId);
        }

        // Recupera un usuario previamente eliminado
        public int RecuperarUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new Exception("ID de usuario no válido");

            return dusuario.Recuperar(usuarioId);
        }

        // Busca un usuario por su nombre de usuario
        public TUsuario BuscarPorNombre(string nombreUsuario)
        {
            return dusuario.BuscarPorNombre(nombreUsuario);
        }

        // Busca un usuario por su ID
        public TUsuario BuscarPorId(int usuarioId)
        {
            var todosUsuarios = dusuario.Listado();
            todosUsuarios.AddRange(dusuario.ListadoEliminado());
            return todosUsuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
        }

        // Obtiene usuarios en formato para DataGrid incluyendo eliminados
        public List<object> ObtenerUsuariosGrid()
        {
            try
            {
                var usuarios = dusuario.ListadoEliminado();

                if (usuarios == null || usuarios.Count == 0)
                    return new List<object>();

                var resultado = usuarios.Select(u => new
                {
                    Id = u.UsuarioId,
                    Nombre_Usuario = u.NombreUsuario,
                    Rol = u.Rol != null ? u.Rol.Nombre : "Sin rol",
                    Fecha_Registro = u.FechaRegistro,
                    Ultima_Sesion = u.UltimoAcceso,
                    Estado = u.Estado ? "Activo" : "Inactivo",
                    Huella = u.HuellaDactilar != null && u.HuellaDactilar.Length > 0 ? "Agregada" : "Sin agregar",
                    Eliminado = u.Eliminado ? "Sí" : "No"
                }).ToList();

                return resultado.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener usuarios: {0}", ex.Message));
            }
        }

        // Obtiene usuarios activos en formato para DataGrid
        public List<object> ObtenerUsuariosActivosGrid()
        {
            try
            {
                var usuarios = dusuario.Listado();

                if (usuarios == null || usuarios.Count == 0)
                    return new List<object>();

                var resultado = usuarios.Select(u => new
                {
                    Id = u.UsuarioId,
                    Nombre_Usuario = u.NombreUsuario,
                    Rol = u.Rol != null ? u.Rol.Nombre : "Sin rol",
                    Fecha_Registro = u.FechaRegistro,
                    Ultima_Sesion = u.UltimoAcceso,
                    Estado = u.Estado ? "Activo" : "Inactivo",
                    Huella = u.HuellaDactilar != null && u.HuellaDactilar.Length > 0 ? "Agregada" : "Sin agregar"
                }).ToList();

                return resultado.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener usuarios: {0}", ex.Message));
            }
        }

        // Obtiene todos los roles activos
        public List<TRol> TodosLosRoles()
        {
            return drol.Listado();
        }

        // Obtiene todos los roles incluyendo eliminados
        public List<TRol> TodosLosRolesCompleto()
        {
            return drol.ListadoCompleto();
        }

        // Obtiene los roles en formato para ComboBox
        public List<object> ObtenerRolesParaCombo()
        {
            try
            {
                var roles = drol.Listado();

                if (roles == null || roles.Count == 0)
                    return new List<object>();

                var resultado = roles.Select(r => new
                {
                    r.RolId,
                    r.Nombre
                }).ToList();

                return resultado.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al obtener roles: {0}", ex.Message));
            }
        }

        // Autentica un usuario con nombre de usuario y contraseña
        public TUsuario Login(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("El nombre de usuario no puede estar vacío");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new Exception("La contraseña no puede estar vacía");

            var usuario = dusuario.BuscarPorNombre(nombreUsuario);

            if (usuario == null || usuario.Eliminado || !usuario.Estado)
                return null;

            if (VerificarHash(contrasena, usuario.Contrasena))
            {
                usuario.UltimoAcceso = DateTime.Now;
                dusuario.Guardar(usuario);
                return usuario;
            }

            return null;
        }

        // Autentica un usuario mediante huella dactilar
        public TUsuario LoginConHuella(byte[] huella)
        {
            if (huella == null || huella.Length == 0)
                throw new Exception("No se proporcionó una huella válida");

            return dusuario.Listado()
                           .FirstOrDefault(u => u.HuellaDactilar != null &&
                                                u.HuellaDactilar.SequenceEqual(huella) &&
                                                !u.Eliminado &&
                                                u.Estado);
        }

        private string GenerarHash(string contrasena)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        private bool VerificarHash(string contrasena, string hashGuardado)
        {
            string hashIngresado = GenerarHash(contrasena);
            return hashIngresado.Equals(hashGuardado, StringComparison.OrdinalIgnoreCase);
        }

        // Actualiza la fecha y hora del último acceso del usuario
        public void ActualizarUltimoAcceso(int usuarioId)
        {
            try
            {
                var usuario = dusuario.Listado().FirstOrDefault(u => u.UsuarioId == usuarioId);
                if (usuario != null)
                {
                    usuario.UltimoAcceso = DateTime.Now;
                    dusuario.Guardar(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al actualizar último acceso: {0}", ex.Message));
            }
        }

        // Inicializa los roles básicos del sistema si no existen
        public void InicializarRoles()
        {
            try
            {
                var rolesExistentes = drol.Listado();

                if (rolesExistentes != null && rolesExistentes.Count > 0)
                    return;

                var rolesBasicos = new[]
                {
            new TRol { Nombre = "Administrador", Eliminado = false },
            new TRol { Nombre = "Medico", Eliminado = false },
            new TRol { Nombre = "Recepcionista", Eliminado = false },
            new TRol { Nombre = "Farmaceutico", Eliminado = false }
        };

                foreach (var rol in rolesBasicos)
                {
                    drol.Guardar(rol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al inicializar roles: {0}", ex.Message));
            }
        }

        // Crea un usuario administrador por defecto si no existe ninguno
        public void CrearAdministradorPorDefecto()
        {
            try
            {
                var usuariosActivos = dusuario.Listado();
                var usuariosEliminados = dusuario.ListadoEliminado();
                var todosLosUsuarios = usuariosActivos.Concat(usuariosEliminados).ToList();

                var roles = drol.Listado();
                var rolAdministrador = roles.FirstOrDefault(r => r.Nombre == "Administrador");

                if (rolAdministrador == null)
                {
                    throw new Exception("El rol Administrador no existe. Debe inicializar los roles primero.");
                }

                var adminExistente = todosLosUsuarios.Any(u => u.RolId == rolAdministrador.RolId);

                if (adminExistente)
                {
                    return;
                }

                var usuarioAdmin = new TUsuario
                {
                    NombreUsuario = "admin",
                    Contrasena = GenerarHash("password"),
                    RolId = rolAdministrador.RolId,
                    HuellaDactilar = null,
                    FechaRegistro = DateTime.Now,
                    Estado = true,
                    Eliminado = false,
                    UltimoAcceso = DateTime.Now,
                    Rol = null
                };

                dusuario.Guardar(usuarioAdmin);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al crear administrador por defecto: {0}", ex.Message));
            }
        }

        // Inicializa el sistema creando roles y usuario administrador por defecto
        public void InicializarSistema()
        {
            try
            {
                InicializarRoles();

                CrearAdministradorPorDefecto();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al inicializar el sistema: {0}", ex.Message));
            }
        }

        // Obtiene el doctor asociado a un usuario médico
        public TDoctor ObtenerDoctorDelUsuario(int usuarioId)
        {
            try
            {
                var usuario = BuscarPorId(usuarioId);

                if (usuario == null || usuario.Rol == null || usuario.Rol.Nombre != "Medico")
                    return null;

                return ddoctor.BuscarPorUsuarioId(usuarioId);
            }
            catch
            {
                return null;
            }
        }

        // Asocia un doctor a un usuario con rol de médico
        public int AsociarDoctorAUsuario(int doctorId, int usuarioId)
        {
            try
            {
                var usuario = BuscarPorId(usuarioId);

                if (usuario == null)
                    throw new Exception("Usuario no encontrado");

                if (usuario.Rol == null || usuario.Rol.Nombre != "Medico")
                    throw new Exception("Solo los usuarios con rol 'Medico' pueden asociarse a un doctor");

                return ddoctor.AsociarUsuario(doctorId, usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al asociar doctor: {0}", ex.Message));
            }
        }

        // Desasocia un doctor de su usuario
        public int DesasociarDoctorDeUsuario(int doctorId)
        {
            try
            {
                return ddoctor.DesasociarUsuario(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al desasociar doctor: {0}", ex.Message));
            }
        }

        // Lista los usuarios médicos que no están asociados a ningún doctor
        public List<TUsuario> ListarMedicosDisponiblesParaAsociar()
        {
            try
            {
                var rolMedico = drol.Listado().FirstOrDefault(r => r.Nombre == "Medico");
                if (rolMedico == null)
                    throw new Exception("Rol 'Medico' no encontrado.");

                var todosLosMedicos = dusuario.Listado()
                                        .Where(u => u.RolId == rolMedico.RolId && u.Estado && !u.Eliminado)
                                        .ToList();

                var idsUsuariosAsociados = ddoctor.ListadoConUsuarios()
                                            .Where(d => d.UsuarioId.HasValue && !d.Eliminado)
                                            .Select(d => d.UsuarioId.Value)
                                            .ToList();

                var medicosDisponibles = todosLosMedicos
                                        .Where(u => !idsUsuariosAsociados.Contains(u.UsuarioId))
                                        .ToList();

                return medicosDisponibles;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al listar médicos disponibles: {0}", ex.Message));
            }
        }

    }

}