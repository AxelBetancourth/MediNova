using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Administrador
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void AbrirFormularioEnPanel(Form FormularioHijo)
        {
            this.PanelMain.Controls.Clear();

            FormularioHijo.TopLevel = false;
            FormularioHijo.FormBorderStyle = FormBorderStyle.None;
            FormularioHijo.Dock = DockStyle.Fill;

            this.PanelMain.Controls.Add(FormularioHijo);
            this.PanelMain.Tag = FormularioHijo;

            FormularioHijo.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CargarMenuInicio();
        }

        private void CargarMenuInicio()
        {
            var formRegistroUsuario = new CapaPresentacion.Administrador.Inicio();
            AbrirFormularioEnPanel(formRegistroUsuario);
        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnInicio_Click(object sender, EventArgs e)
        {
            var formRegistroUsuario = new CapaPresentacion.Administrador.Inicio();
            AbrirFormularioEnPanel(formRegistroUsuario);
        }

        private void BtnRecuperar_Click(object sender, EventArgs e)
        {
            var formRegistroUsuario = new CapaPresentacion.Administrador.PMedico();
            AbrirFormularioEnPanel(formRegistroUsuario);
        }

        private void BtnRol_Click(object sender, EventArgs e)
        {
            var form = new CapaPresentacion.Administrador.Reporte();
            AbrirFormularioEnPanel(form);
        }

        private void BtnUsuario_Click(object sender, EventArgs e)
        {
            var formRegistroUsuario = new CapaPresentacion.ModuloLogin.Registrar();
            AbrirFormularioEnPanel(formRegistroUsuario);
        }

        private void BtnEnfermedades_Click(object sender, EventArgs e)
        {
            var formEnfermedades = new CapaPresentacion.Medico.GestionEnfermedades();
            AbrirFormularioEnPanel(formEnfermedades);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnEmpresa_Click(object sender, EventArgs e)
        {
            var formRegistroEmpresa = new CapaPresentacion.Administrador.Empresa();
            AbrirFormularioEnPanel(formRegistroEmpresa);
        }
    }
}
