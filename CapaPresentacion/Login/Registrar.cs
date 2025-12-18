using CapaNegocio.Administrador;
using DPFP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CapaPresentacion.ModuloLogin
{
    public partial class Registrar : Form
    {
        private byte[] huellaCapturada;
        private NLogin nlogin;
        private int? usuarioSeleccionadoId = null;

        public Registrar()
        {
            InitializeComponent();
            nlogin = new NLogin();
        }

        private void Registro_Load(object sender, EventArgs e)
        {
            CargarRoles();
            LimpiarFormulario();
            BtnRecuperar.Visible = false;

            DtgUsuarios.AutoGenerateColumns = true;
            DtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DtgUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DtgUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DtgUsuarios.MultiSelect = false;
            DtgUsuarios.AllowUserToAddRows = false;
            DtgUsuarios.ReadOnly = true;

            DtgUsuarios.DataBindingComplete -= DtgUsuarios_DataBindingComplete;
            DtgUsuarios.DataBindingComplete += DtgUsuarios_DataBindingComplete;

            CargarDatos();
        }

        void CargarDatos()
        {
            if (!string.IsNullOrWhiteSpace(BotonBuscar.Text))
            {
                BotonBuscar_TextChanged(this, EventArgs.Empty);
                return;
            }

            // Cuando está marcado mostramos la lista usando ObtenerUsuariosGrid
            if (CheckEliminados != null && CheckEliminados.Checked)
                DtgUsuarios.DataSource = nlogin.ObtenerUsuariosGrid();
            else
                DtgUsuarios.DataSource = nlogin.ObtenerUsuariosActivosGrid();

            DtgUsuarios.ClearSelection();
            usuarioSeleccionadoId = null;
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnRecuperar.Enabled = false;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                var capturarHuella = new CaptureForm();

                // Suscribir al evento de captura de huella
                capturarHuella.OnHuellaCapturada += (huella) =>
                {
                    this.huellaCapturada = huella;
                    ActualizarEstadoHuella();
                };

                // Mostrar el formulario
                if (this.MdiParent != null)
                    capturarHuella.MdiParent = this.MdiParent;

                capturarHuella.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir captura de huella:\n{0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos básicos
                if (!ValidarCampos(out string nombreUsuario, out string contrasena, out int rolId))
                    return;

                // Validar huella
                if (!ValidarHuella())
                    return;

                int resultado;

                if (usuarioSeleccionadoId == null)
                {
                    // Nuevo usuario
                    resultado = nlogin.RegistrarUsuario(nombreUsuario, contrasena, rolId, huellaCapturada);
                }
                else
                {
                    // Editar usuario
                    resultado = nlogin.EditarUsuario(usuarioSeleccionadoId.Value, nombreUsuario, contrasena, rolId, huellaCapturada);
                }

                if (resultado > 0)
                {
                    string mensaje = usuarioSeleccionadoId == null
                        ? string.Format("Usuario '{0}' registrado correctamente", nombreUsuario)
                        : string.Format("Usuario '{0}' editado correctamente", nombreUsuario);

                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarDatos();

                    // Mostrar botón de registrar nuevamente
                    BtnLogin.Visible = true;
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el usuario. Intente nuevamente.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar el usuario:\n{0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var res = nlogin.EliminarUsuario(usuarioSeleccionadoId.Value);
                if (res > 0)
                {
                    MessageBox.Show("Usuario eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null)
            {
                MessageBox.Show("Seleccione un usuario para editar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ejecutamos la misma logica de guardar
            BtnLogin_Click(sender, e);
        }

        private void BtnRecuperar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoId == null)
            {
                MessageBox.Show("Seleccione un usuario para recuperar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("¿Está seguro que desea recuperar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var res = nlogin.RecuperarUsuario(usuarioSeleccionadoId.Value);
                if (res > 0)
                {
                    MessageBox.Show("Usuario recuperado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("No se pudo recuperar el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al recuperar: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckEliminados_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CargarDatos();

            if (CheckEliminados.Checked)
            {
                // Modo: Ver usuarios eliminados
                BtnRecuperar.Visible = true;
                BtnEliminar.Visible = false;
                BtnEditar.Visible = false;
                BtnLogin.Visible = false;
            }
            else
            {
                // Modo: Ver usuarios activos
                BtnRecuperar.Visible = false;
                BtnEliminar.Visible = true;
                BtnEditar.Visible = true;
                BtnLogin.Visible = true;
            }
        }

        private void DtgUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = DtgUsuarios.Rows[e.RowIndex];
            if (row == null) return;

            // Obtener el ID
            var cellId = row.Cells.Cast<DataGridViewCell>()
                .FirstOrDefault(c => c.OwningColumn.Name == "Id" ||
                                     c.OwningColumn.Name == "UsuarioId")?.Value;

            if (cellId == null) return;

            if (!int.TryParse(cellId.ToString(), out int id)) return;

            // Si estamos en modo eliminados, no cargar datos en los controles
            if (CheckEliminados.Checked)
            {
                usuarioSeleccionadoId = id;
                BtnRecuperar.Enabled = true;
                return;
            }

            // Cargar datos en controles para edición
            var usuario = nlogin.ListadoUsuarios().FirstOrDefault(u => u.UsuarioId == id);
            if (usuario == null)
            {
                LimpiarFormulario();
                return;
            }

            usuarioSeleccionadoId = usuario.UsuarioId;
            TxtUsuario.Text = usuario.NombreUsuario;
            TxtPassword.Text = string.Empty;
            if (CBRol.DataSource != null) CBRol.SelectedValue = usuario.RolId;

            huellaCapturada = usuario.HuellaDactilar;
            Estado.Text = (huellaCapturada != null && huellaCapturada.Length > 0) ? "Status: Agregada" : "Status: Sin agregar";

            BtnEditar.Enabled = true;
            BtnEliminar.Enabled = true;

            BtnLogin.Visible = false;
        }

        private void DtgUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // No hacer nada en modo eliminados
            if (CheckEliminados.Checked)
                return;

            // Carga el usuario y ocultar botón registrar
            DtgUsuarios_CellClick(sender, e);
        }

        private void BotonBuscar_TextChanged(object sender, EventArgs e)
        {
            var texto = BotonBuscar.Text?.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                CargarDatos();
                return;
            }

            List<object> resultado;

            if (CheckEliminados.Checked)
            {
                // Buscar en usuarios eliminados
                var usuariosEncontrados = nlogin.ListadoUsuariosEliminado()
                    .Where(u => u.NombreUsuario.ToLower().Contains(texto.ToLower()))
                    .Select(u => new
                    {
                        Id = u.UsuarioId,
                        Nombre_Usuario = u.NombreUsuario,
                        Rol = u.Rol?.Nombre ?? "Sin rol",
                        Fecha_Registro = u.FechaRegistro,
                        Ultima_Sesion = u.UltimoAcceso,
                        Estado = u.Estado ? "Activo" : "Inactivo",
                        Huella = u.HuellaDactilar != null && u.HuellaDactilar.Length > 0 ? "Agregada" : "Sin agregar",
                        Eliminado = "Sí"
                    })
                    .Cast<object>()
                    .ToList();

                resultado = usuariosEncontrados;
            }
            else
            {
                // Buscar en usuarios activos
                var usuariosEncontrados = nlogin.ListadoUsuarios()
                    .Where(u => u.NombreUsuario.ToLower().Contains(texto.ToLower()))
                    .Select(u => new
                    {
                        Id = u.UsuarioId,
                        Nombre_Usuario = u.NombreUsuario,
                        Rol = u.Rol?.Nombre ?? "Sin rol",
                        Fecha_Registro = u.FechaRegistro,
                        Ultima_Sesion = u.UltimoAcceso,
                        Estado = u.Estado ? "Activo" : "Inactivo",
                        Huella = u.HuellaDactilar != null && u.HuellaDactilar.Length > 0 ? "Agregada" : "Sin agregar"
                    })
                    .Cast<object>()
                    .ToList();

                resultado = usuariosEncontrados;
            }

            // Mostrar resultados
            if (resultado.Count == 0)
            {
                DtgUsuarios.DataSource = new List<object>();
                usuarioSeleccionadoId = null;
                BtnEditar.Enabled = false;
                BtnEliminar.Enabled = false;
                BtnRecuperar.Enabled = false;
                return;
            }

            DtgUsuarios.DataSource = resultado;
            DtgUsuarios.ClearSelection();
            usuarioSeleccionadoId = null;
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnRecuperar.Enabled = false;
        }

        private void DtgUsuarios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in DtgUsuarios.Columns)
            {
                switch (col.Name)
                {
                    case "Id":
                    case "UsuarioId":
                        col.FillWeight = 8;
                        col.HeaderText = "ID";
                        break;
                    case "Nombre_Usuario":
                    case "NombreUsuario":
                        col.FillWeight = 30;
                        col.HeaderText = "Usuario";
                        break;
                    case "Rol":
                        col.FillWeight = 20;
                        break;
                    case "Fecha_Registro":
                    case "FechaRegistro":
                        col.FillWeight = 18;
                        col.HeaderText = "Fecha Registro";
                        col.DefaultCellStyle.Format = "g";
                        break;
                    case "Ultima_Sesion":
                    case "UltimoAcceso":
                        col.FillWeight = 14;
                        col.HeaderText = "Última Sesión";
                        col.DefaultCellStyle.Format = "g";
                        break;
                    case "Estado":
                        col.FillWeight = 10;
                        break;
                    case "Huella":
                    case "TieneHuella":
                        col.FillWeight = 10;
                        col.HeaderText = "Huella";
                        break;
                    case "Eliminado":
                        col.FillWeight = 10;
                        col.HeaderText = "Eliminado";
                        break;
                    default:
                        col.FillWeight = 10;
                        break;
                }
            }

            DtgUsuarios.ClearSelection();
        }
        private void CargarRoles()
        {
            try
            {
                var roles = nlogin.ObtenerRolesParaCombo();

                if (roles == null || roles.Count == 0)
                {
                    return;
                }

                CBRol.DataSource = roles;
                CBRol.DisplayMember = "Nombre";
                CBRol.ValueMember = "RolId";
                CBRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los roles:\n{0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            TxtUsuario.Clear();
            TxtPassword.Clear();
            CBRol.SelectedIndex = -1;
            huellaCapturada = null;
            usuarioSeleccionadoId = null;
            TxtUsuario.Focus();
            Estado.Text = "Status: Sin agregar";
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnRecuperar.Enabled = false;

            if (!CheckEliminados.Checked)
                BtnLogin.Visible = true;
        }

        private void ActualizarEstadoHuella()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(ActualizarEstadoHuella));
                return;
            }

            Estado.Text = (huellaCapturada != null && huellaCapturada.Length > 0)
                ? "Status: Agregada"
                : "Status: Sin agregar";
        }

        private bool ValidarCampos(out string nombreUsuario, out string contrasena, out int rolId)
        {
            nombreUsuario = TxtUsuario.Text.Trim();
            contrasena = TxtPassword.Text;
            rolId = CBRol.SelectedValue != null ? Convert.ToInt32(CBRol.SelectedValue) : 0;

            // Validar nombre de usuario
            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                MessageBox.Show("Por favor, ingrese un nombre de usuario",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtUsuario.Focus();
                return false;
            }

            // Validar rol
            if (rolId == 0)
            {
                MessageBox.Show("Por favor, seleccione un rol",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CBRol.Focus();
                return false;
            }

            // Validar contraseña según sea nuevo o edición
            if (usuarioSeleccionadoId == null)
            {
                if (string.IsNullOrWhiteSpace(contrasena))
                {
                    MessageBox.Show("Por favor, ingrese una contraseña",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtPassword.Focus();
                    return false;
                }

                if (contrasena.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtPassword.Focus();
                    return false;
                }
            }
            else
            {
                // Validar longitud
                if (!string.IsNullOrWhiteSpace(contrasena) && contrasena.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtPassword.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool ValidarHuella()
        {
            if (huellaCapturada == null || huellaCapturada.Length == 0)
            {
                var resultado = MessageBox.Show(
                    "No se ha capturado ninguna huella dactilar.\n¿Desea continuar sin huella?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                return resultado == DialogResult.Yes;
            }

            return true;
        }
        private void CBRol_SelectedIndexChanged(object sender, EventArgs e) { }
        private void TxtUsuario_TextChanged(object sender, EventArgs e) { }
        private void TxtPassword_TextChanged(object sender, EventArgs e) { }
        private void Estado_Click(object sender, EventArgs e) { }
        private void DtgUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void Status_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}