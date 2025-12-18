using CapaDatos.BaseDatos.Tablas;
using CapaDatos.BaseDatos.Tablas.Catalogos;
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

namespace CapaPresentacion.Recepcionista
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CargarMenuInicio();
        }
        private void CargarMenuInicio()
        {
            var formInicio = new CapaPresentacion.Recepcionista.Inicio();
            AbrirFormularioEnPanel(formInicio);
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

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnPaciente_Click(object sender, EventArgs e)
        {
            var formPacientes = new CapaPresentacion.Recepcionista.Pacientes();
            AbrirFormularioEnPanel(formPacientes);
        }

        private void BtnCita_Click(object sender, EventArgs e)
        {
            var formCitas = new CapaPresentacion.Recepcionista.Calendario();
            AbrirFormularioEnPanel(formCitas);
        }

        private void BtnInicio_Click(object sender, EventArgs e)
        {
            var formInicio = new CapaPresentacion.Recepcionista.Inicio();
            AbrirFormularioEnPanel(formInicio);
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
