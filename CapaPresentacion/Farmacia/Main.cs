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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {

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

        private void BtnInicio_Click(object sender, EventArgs e)
        {
            var formInicio = new CapaPresentacion.Farmacia.Inicio();
            AbrirFormularioEnPanel(formInicio);
        }

        private void BtnVenta_Click(object sender, EventArgs e)
        {
            var formVenta = new CapaPresentacion.Farmacia.Ventas();
            AbrirFormularioEnPanel(formVenta);
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            var formInventario = new CapaPresentacion.Farmacia.Inventario();
            AbrirFormularioEnPanel(formInventario);
        }

        private void BtnFactura_Click(object sender, EventArgs e)
        {
            var formFactura = new CapaPresentacion.Farmacia.Facturas();
            AbrirFormularioEnPanel(formFactura);
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            CargarMenuInicio();
        }

        private void CargarMenuInicio()
        {
            var formRegistroUsuario = new CapaPresentacion.Farmacia.Inicio();
            AbrirFormularioEnPanel(formRegistroUsuario);
        }
    }
}
