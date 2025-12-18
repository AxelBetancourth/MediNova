using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Farmacia;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CapaPresentacion.Farmacia
{
    public partial class FormVentaLibre : Form
    {
        public TMedicamento MedicamentoSeleccionado { get; private set; }
        public int Cantidad { get; private set; }

        // Importación de la función de la API de Windows
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public FormVentaLibre()
        {
            InitializeComponent();
            this.Load += FormVentaLibre_Load;

            // Aplica el redondeo en el constructor
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); // Radio de 20
        }

        private void FormVentaLibre_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid();
            numCantidad.Value = 1;
            CargarMedicamentos(); // Cargar productos al inicio
        }

        private void ConfigurarDataGrid()
        {
            dgvMedicamentos.AutoGenerateColumns = false;
            dgvMedicamentos.Columns.Clear();

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MedicamentoId",
                DataPropertyName = "MedicamentoId",
                Visible = false
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Dosis",
                HeaderText = "Dosis",
                DataPropertyName = "Dosis",
                Width = 120
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Proveedor",
                HeaderText = "Proveedor",
                DataPropertyName = "Proveedor",
                Width = 150
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Presentacion",
                HeaderText = "Presentación",
                DataPropertyName = "Presentacion",
                Width = 120
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 80
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "PrecioUnitario",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" }
            });
        }

        private void CargarMedicamentos(string filtro = "")
        {
            try
            {
                using (var nMedicamento = new NMedicamento())
                {
                    var medicamentos = nMedicamento.ListarMedicamentos();

                    if (!string.IsNullOrWhiteSpace(filtro))
                    {
                        medicamentos = medicamentos.Where(m =>
                            m.Nombre.ToUpper().Contains(filtro.ToUpper()) ||
                            (m.Presentacion != null && m.Presentacion.ToUpper().Contains(filtro.ToUpper())) ||
                            (m.Dosis != null && m.Dosis.ToUpper().Contains(filtro.ToUpper())) ||
                            (m.Proveedor != null && m.Proveedor.ToUpper().Contains(filtro.ToUpper()))
                        ).ToList();
                    }

                    // Filtrar solo medicamentos con stock disponible
                    medicamentos = medicamentos.Where(m => m.Stock > 0).ToList();

                    dgvMedicamentos.DataSource = medicamentos;
                    lblResultados.Text = string.Format("📊 {0} medicamento(s) disponible(s)", medicamentos.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar medicamentos: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarMedicamentos(txtBuscar.Text);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un medicamento", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var medicamento = dgvMedicamentos.SelectedRows[0].DataBoundItem as TMedicamento;
            if (medicamento == null) return;

            // Validar cantidad vs stock
            if (numCantidad.Value > medicamento.Stock)
            {
                MessageBox.Show(string.Format("La cantidad solicitada ({0}) supera el stock disponible ({1})", numCantidad.Value, medicamento.Stock),
                    "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MedicamentoSeleccionado = medicamento;
            Cantidad = (int)numCantidad.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DgvMedicamentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnAgregar_Click(sender, e);
            }
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            numCantidad.Value = 1;
            CargarMedicamentos();
        }

        private void DgvMedicamentos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMedicamentos.SelectedRows.Count > 0)
            {
                var medicamento = dgvMedicamentos.SelectedRows[0].DataBoundItem as TMedicamento;
                if (medicamento != null)
                {
                    // Actualizar el máximo del NumericUpDown según el stock
                    numCantidad.Maximum = Math.Max(1, medicamento.Stock);

                    // Si la cantidad actual es mayor al stock, ajustarla
                    if (numCantidad.Value > medicamento.Stock)
                    {
                        numCantidad.Value = Math.Min(numCantidad.Value, medicamento.Stock);
                    }
                }
            }
        }

        private void FormVentaLibre_Load_1(object sender, EventArgs e)
        {

        }

        private void panelPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
