using CapaPresentacion.ModuloLogin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class Main : Form
    {

        private bool sesionValida = false;

        public Main()
        {
            InitializeComponent();
        }

        private bool VerificarSesion()
        {
            try
            {
                // 🔹 Verificar que la sesión esté iniciada
                if (SesionUsuario.UsuarioActual == null)
                {
                    MessageBox.Show(
                        "No hay sesión activa. Debe iniciar sesión.",
                        "Error de Sesión",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    MostrarLoginYCerrar();
                    return false;
                }

                // 🔹 Verificar que el usuario sea médico
                if (!SesionUsuario.EsMedico())
                {
                    string rolNombre = (SesionUsuario.UsuarioActual.Rol != null) ? SesionUsuario.UsuarioActual.Rol.Nombre : "Desconocido";
                    MessageBox.Show(
                        string.Format("Este módulo es solo para médicos.\nSu rol es: {0}", rolNombre),
                        "Acceso Denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    MostrarLoginYCerrar();
                    return false;
                }

                // 🔹 CRÍTICO: Verificar si tiene doctor asociado
                if (!SesionUsuario.TieneDoctorAsociado())
                {
                    MessageBox.Show(
                        "Su usuario no tiene un doctor asociado en el sistema.\n\n" +
                        "Por favor, contacte al administrador para que le asocie un perfil de doctor.\n\n" +
                        "No podrá acceder al módulo de médicos hasta que esto se resuelva.",
                        "Doctor No Asociado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    MostrarLoginYCerrar();
                    return false;
                }

                // ✅ Todo correcto
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error al verificar la sesión:\n{0}", ex.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                MostrarLoginYCerrar();
                return false;
            }
        }

        private void MostrarLoginYCerrar()
        {
            try
            {
                // Limpiar la sesión
                SesionUsuario.CerrarSesion();

                // Abrir el formulario de Login
                var login = new CapaPresentacion.ModuloLogin.Login();
                login.StartPosition = FormStartPosition.CenterScreen;
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error al retornar al login:\n{0}", ex.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Si la sesión no era válida, asegurarse de que el login esté visible
            if (!sesionValida)
            {
                // Buscar si ya hay un login abierto
                var loginExistente = Application.OpenForms.OfType<Login>().FirstOrDefault();

                if (loginExistente != null)
                {
                    loginExistente.Show();
                    loginExistente.BringToFront();
                }
            }
        }

        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AbrirFormularioEnPanel(Form FormularioHijo)
        {
            this.panel_main.Controls.Clear();

            FormularioHijo.TopLevel = false;
            FormularioHijo.FormBorderStyle = FormBorderStyle.None;
            FormularioHijo.Dock = DockStyle.Fill;

            this.panel_main.Controls.Add(FormularioHijo);
            this.panel_main.Tag = FormularioHijo;

            FormularioHijo.Show();
        }

        private void BtnInicio_Click(object sender, EventArgs e)
        {
            var formInicio = new CapaPresentacion.Medico.Inicio();
            AbrirFormularioEnPanel(formInicio);
        }

        private void BtnConsulta_Click(object sender, EventArgs e)
        {
            var formConsulta = new CapaPresentacion.Medico.Consultas();
            AbrirFormularioEnPanel(formConsulta);
        }

        private void BtnExpediente_Click(object sender, EventArgs e)
        {
            var formExpediente = new CapaPresentacion.Medico.Calendario();
            AbrirFormularioEnPanel(formExpediente);
        }

        private void BtnPrescripcion_Click(object sender, EventArgs e)
        {
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Verificar sesión al cargar el formulario
            if (!VerificarSesion())
            {
                // Si la verificación falla, cerrar este formulario
                this.Close();
                return;
            }

            // Si llegamos aquí, la sesión es válida
            sesionValida = true;

            CargarMenuInicio();
        }

        private void CargarMenuInicio()
        {
            var formRegistroUsuario = new CapaPresentacion.Medico.Inicio();
            AbrirFormularioEnPanel(formRegistroUsuario);
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnEnfermedad_Click(object sender, EventArgs e)
        {
            var formEnfermedades = new CapaPresentacion.Medico.GestionEnfermedades();
            AbrirFormularioEnPanel(formEnfermedades);
        }

        private void BtnExamenes_Click(object sender, EventArgs e)
        {
            var formExamenes = new CapaPresentacion.Medico.Examenes();
            AbrirFormularioEnPanel(formExamenes);
        }
    }
}
