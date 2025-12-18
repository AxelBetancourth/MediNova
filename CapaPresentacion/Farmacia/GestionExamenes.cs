using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Medico;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Farmacia
{
    public partial class GestionExamenes : Form
    {
        private List<TExamen> examenes;

        public GestionExamenes()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CargarExamenes();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Gestión de Costos de Exámenes";
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvExamenes.AutoGenerateColumns = false;
            dgvExamenes.Columns.Clear();
            dgvExamenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExamenes.MultiSelect = false;
            dgvExamenes.ReadOnly = false;
            dgvExamenes.AllowUserToAddRows = false;
            dgvExamenes.AllowUserToDeleteRows = false;
            dgvExamenes.RowHeadersVisible = false;

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExamenId",
                HeaderText = "ID",
                DataPropertyName = "ExamenId",
                Width = 60,
                Visible = false,
                ReadOnly = true
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre del Examen",
                DataPropertyName = "Nombre",
                Width = 250,
                ReadOnly = true
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                Width = 150,
                ReadOnly = true
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 120,
                ReadOnly = true
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Costo",
                HeaderText = "Costo (L)",
                DataPropertyName = "Costo",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" },
                ReadOnly = false  // Este campo SÍ es editable
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaSolicitud",
                HeaderText = "Fecha Solicitud",
                DataPropertyName = "FechaSolicitud",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                ReadOnly = true
            });

            dgvExamenes.CellEndEdit += DgvExamenes_CellEndEdit;
        }

        private void CargarExamenes()
        {
            try
            {
                using (var nExamen = new NExamen())
                {
                    examenes = nExamen.ListarExamenes();
                    FiltrarYMostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar exámenes: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarYMostrar()
        {
            if (examenes == null) return;

            var filtrados = examenes.AsEnumerable();

            // Filtrar por búsqueda
            string busqueda = txtBuscar.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                filtrados = filtrados.Where(e =>
                    (e.Nombre != null && e.Nombre.ToLower().Contains(busqueda)) ||
                    (e.Tipo != null && e.Tipo.ToLower().Contains(busqueda))
                );
            }

            dgvExamenes.DataSource = filtrados.ToList();
            lblTotal.Text = string.Format("Total: {0} exámenes", filtrados.Count());
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarYMostrar();
        }

        private void DgvExamenes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Solo procesar si se editó la columna de Costo
            if (dgvExamenes.Columns[e.ColumnIndex].Name == "Costo")
            {
                try
                {
                    var examen = dgvExamenes.Rows[e.RowIndex].DataBoundItem as TExamen;
                    if (examen != null)
                    {
                        var nuevoCostoStr = dgvExamenes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null ? dgvExamenes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() : null;

                        decimal nuevoCosto = 0;
                        if (decimal.TryParse(nuevoCostoStr, out nuevoCosto) && nuevoCosto >= 0)
                        {
                            examen.Costo = nuevoCosto;

                            using (var nExamen = new NExamen())
                            {
                                nExamen.EditarExamen(examen);
                            }

                            MessageBox.Show("Costo actualizado correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("El costo debe ser un número válido mayor o igual a 0.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // Revertir cambio
                            dgvExamenes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = examen.Costo;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al actualizar costo: {0}", ex.Message),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Recargar para revertir cambios
                    CargarExamenes();
                }
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            CargarExamenes();
        }
    }
}
