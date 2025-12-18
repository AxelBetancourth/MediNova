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

namespace CapaPresentacion.Farmacia
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
            CargarNombreUsuario();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CargarNombreUsuario()
        {
            try
            {
                if (SesionUsuario.UsuarioActual != null)
                {
                    string nombreUsuario = SesionUsuario.UsuarioActual.NombreUsuario;
                    string rolUsuario = SesionUsuario.UsuarioActual.Rol != null ? SesionUsuario.UsuarioActual.Rol.Nombre : "Usuario";

                    // Cambiar el texto del label
                    guna2HtmlLabel1.Text = string.Format("Hola {0}", nombreUsuario);

                }
                else
                {
                    guna2HtmlLabel1.Text = "Hola Usuario";
                }
            }
            catch (Exception ex)
            {
                guna2HtmlLabel1.Text = "Hola Usuario";
                // Opcional: Registrar el error
                Console.WriteLine(string.Format("Error al cargar nombre de usuario: {0}", ex.Message));
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
            CargarNombreUsuario();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void cerrarSesiónToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
            "¿Desea cerrar sesión?",
            "Confirmar cierre de sesión",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                SesionUsuario.CerrarSesion();

                Form mainForm = this.ParentForm;

                if (mainForm == null)
                {
                    Control parent = this.Parent;
                    while (parent != null && !(parent is Form))
                    {
                        parent = parent.Parent;
                    }
                    mainForm = parent as Form;
                }

                var login = new CapaPresentacion.ModuloLogin.Login();
                login.StartPosition = FormStartPosition.CenterScreen;
                login.Show();

                if (mainForm != null)
                {
                    mainForm.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error al cerrar sesión: {0}", ex.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void pictureBoxUsuario_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int margenVertical = 10;

                int nuevaPosicionY = pictureBoxUsuario.Height + margenVertical;

                ContextMenuCS.Show(pictureBoxUsuario, new Point(0, nuevaPosicionY));
            }
        }
    }
}
