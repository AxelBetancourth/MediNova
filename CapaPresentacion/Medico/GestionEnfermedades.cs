using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Compartido;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class GestionEnfermedades : Form
    {
        private List<TEnfermedad> enfermedades;

        public GestionEnfermedades()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CargarEnfermedades();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Gestión de Enfermedades";
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvEnfermedades.AutoGenerateColumns = false;
            dgvEnfermedades.Columns.Clear();
            dgvEnfermedades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEnfermedades.MultiSelect = false;
            dgvEnfermedades.ReadOnly = true;
            dgvEnfermedades.AllowUserToAddRows = false;
            dgvEnfermedades.AllowUserToDeleteRows = false;
            dgvEnfermedades.RowHeadersVisible = false;
            dgvEnfermedades.BackgroundColor = Color.White;
            dgvEnfermedades.BorderStyle = BorderStyle.None;
            dgvEnfermedades.RowTemplate.Height = 40; // Aumentar altura de filas
            dgvEnfermedades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 245, 244); // Color verde claro para selección
            dgvEnfermedades.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvEnfermedades.DefaultCellStyle.BackColor = Color.White;
            dgvEnfermedades.DefaultCellStyle.Font = new Font("Arial", 10F);
            dgvEnfermedades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136); // Color del botón Examenes
            dgvEnfermedades.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEnfermedades.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            dgvEnfermedades.ColumnHeadersHeight = 45;
            dgvEnfermedades.EnableHeadersVisualStyles = false;

            dgvEnfermedades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EnfermedadId",
                HeaderText = "ID",
                DataPropertyName = "EnfermedadId",
                Width = 60,
                Visible = false
            });

            dgvEnfermedades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            });

            dgvEnfermedades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                Width = 150
            });

            dgvEnfermedades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sintomas",
                HeaderText = "Síntomas",
                DataPropertyName = "Sintomas",
                Width = 300
            });

            dgvEnfermedades.CellDoubleClick += DgvEnfermedades_CellDoubleClick;
        }

        private void CargarEnfermedades()
        {
            try
            {
                using (var nEnfermedad = new NEnfermedad())
                {
                    enfermedades = nEnfermedad.ListarEnfermedades();
                    FiltrarYMostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar enfermedades: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarYMostrar()
        {
            if (enfermedades == null) return;

            var filtradas = enfermedades.AsEnumerable();

            // Filtrar por búsqueda
            string busqueda = txtBuscar.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                filtradas = filtradas.Where(e =>
                    (e.Nombre != null && e.Nombre.ToLower().Contains(busqueda)) ||
                    (e.Tipo != null && e.Tipo.ToLower().Contains(busqueda)) ||
                    (e.Sintomas != null && e.Sintomas.ToLower().Contains(busqueda))
                );
            }

            dgvEnfermedades.DataSource = filtradas.ToList();
            lblTotal.Text = string.Format("Total: {0} enfermedades", filtradas.Count());
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarYMostrar();
        }

        private void BtnNueva_Click(object sender, EventArgs e)
        {
            MostrarFormularioEnfermedad(null);
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEnfermedades.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una enfermedad para editar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var enfermedad = dgvEnfermedades.SelectedRows[0].DataBoundItem as TEnfermedad;
            if (enfermedad != null)
            {
                MostrarFormularioEnfermedad(enfermedad);
            }
        }

        private void DgvEnfermedades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var enfermedad = dgvEnfermedades.Rows[e.RowIndex].DataBoundItem as TEnfermedad;
            if (enfermedad != null)
            {
                MostrarFormularioEnfermedad(enfermedad);
            }
        }

        private void MostrarFormularioEnfermedad(TEnfermedad enfermedad)
        {
            Form formEnfermedad;

            if (enfermedad == null)
            {
                // Nueva enfermedad
                formEnfermedad = new Enfermedad();
            }
            else
            {
                // Editar enfermedad existente
                formEnfermedad = new Enfermedad(enfermedad.EnfermedadId);
            }

            if (formEnfermedad.ShowDialog() == DialogResult.OK)
            {
                CargarEnfermedades();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvEnfermedades.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una enfermedad para eliminar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var enfermedad = dgvEnfermedades.SelectedRows[0].DataBoundItem as TEnfermedad;
            if (enfermedad != null)
            {
                var resultado = MessageBox.Show(
                    string.Format("¿Está seguro de eliminar la enfermedad '{0}'?", enfermedad.Nombre),
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (var nEnfermedad = new NEnfermedad())
                        {
                            nEnfermedad.EliminarEnfermedad(enfermedad.EnfermedadId);
                        }

                        MessageBox.Show("Enfermedad eliminada correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarEnfermedades();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al eliminar enfermedad: {0}", ex.Message),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
