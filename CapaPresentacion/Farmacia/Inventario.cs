using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Farmacia;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Farmacia
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
            this.Load += Inventario_Load;
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            btnNuevo.Click += BtnNuevo_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnRegistrarEntrada.Click += BtnRegistrarEntrada_Click;
            btnRegistrarSalida.Click += BtnRegistrarSalida_Click;
            btnRefrescar.Click += BtnRefrescar_Click;
            btnVerHistorial.Click += BtnVerHistorial_Click;
            txtBuscar.TextChanged += TxtBuscar_TextChanged;
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarMedicamentos();
        }

        private void ConfigurarDataGridView()
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
                HeaderText = "Medicamento",
                DataPropertyName = "Nombre",
                Width = 250
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Presentacion",
                HeaderText = "Presentación",
                DataPropertyName = "Presentacion",
                Width = 150
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
                Name = "PrecioUnitario",
                HeaderText = "Precio",
                DataPropertyName = "PrecioUnitario",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" }
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaVencimiento",
                HeaderText = "Vencimiento",
                DataPropertyName = "FechaVencimiento",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Dosis",
                HeaderText = "Dosis",
                DataPropertyName = "Dosis",
                Width = 100
            });

            dgvMedicamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Proveedor",
                HeaderText = "Proveedor",
                DataPropertyName = "Proveedor",
                Width = 150
            });
        }

        private void CargarMedicamentos()
        {
            try
            {
                using (var nMedicamento = new NMedicamento())
                {
                    var medicamentos = nMedicamento.ListarMedicamentos();
                    dgvMedicamentos.DataSource = medicamentos;

                    // Resaltar medicamentos con bajo stock o por vencer
                    foreach (DataGridViewRow row in dgvMedicamentos.Rows)
                    {
                        var medicamento = row.DataBoundItem as TMedicamento;
                        if (medicamento != null)
                        {
                            // Stock bajo
                            if (medicamento.Stock < 10)
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 235, 238);
                            }

                            // Por vencer en 30 días
                            if (medicamento.FechaVencimiento <= DateTime.Now.AddDays(30))
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
                            }
                        }
                    }
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
            try
            {
                string filtro = txtBuscar.Text.Trim();

                if (string.IsNullOrEmpty(filtro))
                {
                    CargarMedicamentos();
                    return;
                }

                using (var nMedicamento = new NMedicamento())
                {
                    var medicamentos = nMedicamento.BuscarPorNombre(filtro);
                    dgvMedicamentos.DataSource = medicamentos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var formMedicamento = new FormMedicamento())
                {
                    if (formMedicamento.ShowDialog() == DialogResult.OK)
                    {
                        CargarMedicamentos();
                        MessageBox.Show("Medicamento agregado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMedicamentos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un medicamento para editar.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var medicamento = dgvMedicamentos.SelectedRows[0].DataBoundItem as TMedicamento;
                if (medicamento == null) return;

                using (var formMedicamento = new FormMedicamento(medicamento.MedicamentoId))
                {
                    if (formMedicamento.ShowDialog() == DialogResult.OK)
                    {
                        CargarMedicamentos();
                        MessageBox.Show("Medicamento actualizado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMedicamentos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un medicamento para eliminar.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var medicamento = dgvMedicamentos.SelectedRows[0].DataBoundItem as TMedicamento;
                if (medicamento == null) return;

                var resultado = MessageBox.Show(
                    string.Format("¿Está seguro de eliminar el medicamento '{0}'?\n\nEsta acción no se puede deshacer.", medicamento.Nombre),
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    using (var nMedicamento = new NMedicamento())
                    {
                        nMedicamento.EliminarMedicamento(medicamento.MedicamentoId);
                        CargarMedicamentos();
                        MessageBox.Show("Medicamento eliminado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegistrarEntrada_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMedicamentos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un medicamento para registrar una entrada.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var medicamento = dgvMedicamentos.SelectedRows[0].DataBoundItem as TMedicamento;
                if (medicamento == null) return;

                using (var formMovimiento = new FormMovimientoInventario(medicamento, "Entrada"))
                {
                    if (formMovimiento.ShowDialog() == DialogResult.OK)
                    {
                        CargarMedicamentos();
                        MessageBox.Show("Entrada registrada exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegistrarSalida_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMedicamentos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un medicamento para registrar una salida.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var medicamento = dgvMedicamentos.SelectedRows[0].DataBoundItem as TMedicamento;
                if (medicamento == null) return;

                using (var formMovimiento = new FormMovimientoInventario(medicamento, "Salida"))
                {
                    if (formMovimiento.ShowDialog() == DialogResult.OK)
                    {
                        CargarMedicamentos();
                        MessageBox.Show("Salida registrada exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarMedicamentos();
        }

        private void BtnVerHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                using (var formHistorial = new FormHistorialMovimientos())
                {
                    formHistorial.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al mostrar historial: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Formulario para mostrar historial de movimientos
    public partial class FormHistorialMovimientos : Form
    {
        public FormHistorialMovimientos()
        {
            InitializeComponent();
            this.Load += FormHistorialMovimientos_Load;
        }

        private void InitializeComponent()
        {
            this.Text = "Historial de Movimientos de Inventario";
            this.Size = new System.Drawing.Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            var panelForm = new Guna.UI2.WinForms.Guna2Panel();
            panelForm.Dock = DockStyle.Fill;
            panelForm.FillColor = System.Drawing.Color.FromArgb(240, 244, 247);
            panelForm.Padding = new Padding(20);

            var lblTitulo = new Label
            {
                Text = "Historial de Movimientos",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(76, 175, 80)
            };

            dgvMovimientos = new Guna.UI2.WinForms.Guna2DataGridView
            {
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(940, 420),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };

            btnCerrar = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "Cerrar",
                Location = new System.Drawing.Point(820, 500),
                Size = new System.Drawing.Size(140, 40),
                BorderRadius = 8,
                FillColor = System.Drawing.Color.FromArgb(244, 67, 54),
                Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White
            };
            btnCerrar.Click += (s, e) => { this.Close(); };

            panelForm.Controls.AddRange(new Control[] { lblTitulo, dgvMovimientos, btnCerrar });
            this.Controls.Add(panelForm);
        }

        private Guna.UI2.WinForms.Guna2DataGridView dgvMovimientos;
        private Guna.UI2.WinForms.Guna2Button btnCerrar;

        private void FormHistorialMovimientos_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarHistorial();
        }

        private void ConfigurarDataGridView()
        {
            dgvMovimientos.AutoGenerateColumns = false;
            dgvMovimientos.Columns.Clear();

            dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoMovimiento",
                HeaderText = "Tipo",
                DataPropertyName = "TipoMovimiento",
                Width = 100
            });

            dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreMedicamento",
                HeaderText = "Medicamento",
                Width = 250
            });

            dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 100
            });

            dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockAnterior",
                HeaderText = "Stock Anterior",
                DataPropertyName = "StockAnterior",
                Width = 120
            });

            dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StockNuevo",
                HeaderText = "Stock Nuevo",
                DataPropertyName = "StockNuevo",
                Width = 120
            });

            dgvMovimientos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Motivo",
                HeaderText = "Motivo",
                DataPropertyName = "Motivo",
                Width = 200
            });
        }

        private void CargarHistorial()
        {
            try
            {
                using (var nInventario = new NInventario())
                {
                    var movimientos = nInventario.ListarMovimientos();
                    dgvMovimientos.DataSource = movimientos;

                    // Llenar manualmente la columna de nombre de medicamento y colorear filas
                    foreach (DataGridViewRow row in dgvMovimientos.Rows)
                    {
                        var movimiento = row.DataBoundItem as CapaDatos.BaseDatos.Tablas.InventarioYFacturacion.TInventarioMovimiento;
                        if (movimiento != null)
                        {
                            // Llenar nombre del medicamento
                            row.Cells["NombreMedicamento"].Value = movimiento.Medicamento != null ? movimiento.Medicamento.Nombre : "Desconocido";

                            // Colorear según tipo de movimiento
                            var tipoMovimiento = movimiento.TipoMovimiento;
                            if (tipoMovimiento == "Entrada")
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(27, 94, 32);
                            }
                            else if (tipoMovimiento == "Salida")
                            {
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(230, 81, 0);
                            }
                        }
                    }

                    // Mostrar mensaje si no hay movimientos
                    if (movimientos.Count == 0)
                    {
                        MessageBox.Show(
                            "No hay movimientos de inventario registrados.\n\n" +
                            "Los movimientos se registrarán automáticamente cuando uses los botones:\n" +
                            "- Registrar Entrada\n" +
                            "- Registrar Salida",
                            "Historial Vacío",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar historial: {0}\n\n{1}", ex.Message, ex.StackTrace), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Formulario para agregar/editar medicamentos
    public partial class FormMedicamento : Form
    {
        private int? medicamentoId;
        private TMedicamento medicamento;

        public FormMedicamento()
        {
            InitializeComponent();
            medicamentoId = null;
            ConfigurarFormulario();
        }

        public FormMedicamento(int medicamentoId)
        {
            InitializeComponent();
            this.medicamentoId = medicamentoId;
            ConfigurarFormulario();
            CargarMedicamento();
        }

        private void InitializeComponent()
        {
            this.Text = medicamentoId.HasValue ? "Editar Medicamento" : "Nuevo Medicamento";
            this.Size = new System.Drawing.Size(600, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var panelForm = new Guna.UI2.WinForms.Guna2Panel();
            panelForm.Dock = DockStyle.Fill;
            panelForm.FillColor = System.Drawing.Color.FromArgb(240, 244, 247);
            panelForm.Padding = new Padding(30);

            var label1 = new Label { Text = "Nombre del Medicamento:", Location = new System.Drawing.Point(30, 30), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            txtNombre = new Guna.UI2.WinForms.Guna2TextBox { Location = new System.Drawing.Point(30, 55), Size = new System.Drawing.Size(520, 36), BorderRadius = 8 };

            var label2 = new Label { Text = "Presentación:", Location = new System.Drawing.Point(30, 100), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            txtPresentacion = new Guna.UI2.WinForms.Guna2TextBox { Location = new System.Drawing.Point(30, 125), Size = new System.Drawing.Size(250, 36), BorderRadius = 8 };

            var label3 = new Label { Text = "Stock:", Location = new System.Drawing.Point(300, 100), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            numStock = new Guna.UI2.WinForms.Guna2NumericUpDown { Location = new System.Drawing.Point(300, 125), Size = new System.Drawing.Size(250, 36), BorderRadius = 8, Maximum = 999999 };

            var label4 = new Label { Text = "Precio Unitario (L):", Location = new System.Drawing.Point(30, 170), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            numPrecio = new Guna.UI2.WinForms.Guna2NumericUpDown { Location = new System.Drawing.Point(30, 195), Size = new System.Drawing.Size(250, 36), BorderRadius = 8, DecimalPlaces = 2, Maximum = 999999 };

            var label5 = new Label { Text = "Fecha de Vencimiento:", Location = new System.Drawing.Point(300, 170), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            dtpVencimiento = new Guna.UI2.WinForms.Guna2DateTimePicker { Location = new System.Drawing.Point(300, 195), Size = new System.Drawing.Size(250, 36), BorderRadius = 8 };

            var label6 = new Label { Text = "Dosis:", Location = new System.Drawing.Point(30, 240), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            txtDosis = new Guna.UI2.WinForms.Guna2TextBox { Location = new System.Drawing.Point(30, 265), Size = new System.Drawing.Size(250, 36), BorderRadius = 8 };

            var label7 = new Label { Text = "Proveedor:", Location = new System.Drawing.Point(300, 240), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            txtProveedor = new Guna.UI2.WinForms.Guna2TextBox { Location = new System.Drawing.Point(300, 265), Size = new System.Drawing.Size(250, 36), BorderRadius = 8 };

            btnGuardar = new Guna.UI2.WinForms.Guna2Button { Text = "Guardar", Location = new System.Drawing.Point(300, 330), Size = new System.Drawing.Size(120, 40), BorderRadius = 8, FillColor = System.Drawing.Color.FromArgb(76, 175, 80), Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.White };
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Guna.UI2.WinForms.Guna2Button { Text = "Cancelar", Location = new System.Drawing.Point(430, 330), Size = new System.Drawing.Size(120, 40), BorderRadius = 8, FillColor = System.Drawing.Color.FromArgb(244, 67, 54), Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.White };
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] { label1, txtNombre, label2, txtPresentacion, label3, numStock, label4, numPrecio, label5, dtpVencimiento, label6, txtDosis, label7, txtProveedor, btnGuardar, btnCancelar });
        }

        private Guna.UI2.WinForms.Guna2TextBox txtNombre, txtPresentacion, txtDosis, txtProveedor;
        private Guna.UI2.WinForms.Guna2NumericUpDown numStock, numPrecio;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpVencimiento;
        private Guna.UI2.WinForms.Guna2Button btnGuardar, btnCancelar;

        private void ConfigurarFormulario()
        {
            dtpVencimiento.Value = DateTime.Now.AddMonths(6);
        }

        private void CargarMedicamento()
        {
            try
            {
                using (var nMedicamento = new NMedicamento())
                {
                    medicamento = nMedicamento.BuscarPorId(medicamentoId.Value);

                    if (medicamento != null)
                    {
                        txtNombre.Text = medicamento.Nombre;
                        txtPresentacion.Text = medicamento.Presentacion;
                        numStock.Value = medicamento.Stock;
                        numPrecio.Value = medicamento.PrecioUnitario;
                        dtpVencimiento.Value = medicamento.FechaVencimiento;
                        txtDosis.Text = medicamento.Dosis;
                        txtProveedor.Text = medicamento.Proveedor;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar medicamento: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre del medicamento es obligatorio.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var med = new TMedicamento
                {
                    MedicamentoId = medicamentoId ?? 0,
                    Nombre = txtNombre.Text.Trim(),
                    Presentacion = txtPresentacion.Text.Trim(),
                    Stock = (int)numStock.Value,
                    PrecioUnitario = numPrecio.Value,
                    FechaVencimiento = dtpVencimiento.Value,
                    Dosis = txtDosis.Text.Trim(),
                    Proveedor = txtProveedor.Text.Trim()
                };

                using (var nMedicamento = new NMedicamento())
                {
                    if (medicamentoId.HasValue)
                    {
                        nMedicamento.EditarMedicamento(med);
                    }
                    else
                    {
                        nMedicamento.RegistrarMedicamento(med);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Formulario para movimientos de inventario
    public partial class FormMovimientoInventario : Form
    {
        private TMedicamento medicamento;
        private string tipoMovimiento;

        public FormMovimientoInventario(TMedicamento medicamento, string tipoMovimiento)
        {
            this.medicamento = medicamento;
            this.tipoMovimiento = tipoMovimiento;
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void InitializeComponent()
        {
            this.Text = string.Format("{0} de Inventario - {1}", tipoMovimiento, medicamento.Nombre);
            this.Size = new System.Drawing.Size(500, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var label1 = new Label { Text = "Medicamento:", Location = new System.Drawing.Point(30, 30), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            lblMedicamento = new Label { Text = medicamento.Nombre, Location = new System.Drawing.Point(150, 30), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F) };

            var label2 = new Label { Text = "Stock Actual:", Location = new System.Drawing.Point(30, 60), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            lblStockActual = new Label { Text = medicamento.Stock.ToString(), Location = new System.Drawing.Point(150, 60), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F) };

            var label3 = new Label { Text = "Cantidad:", Location = new System.Drawing.Point(30, 100), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            numCantidad = new Guna.UI2.WinForms.Guna2NumericUpDown { Location = new System.Drawing.Point(150, 95), Size = new System.Drawing.Size(300, 36), BorderRadius = 8, Maximum = 999999, Minimum = 1, Value = 1 };

            var label4 = new Label { Text = "Motivo:", Location = new System.Drawing.Point(30, 145), AutoSize = true, Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold) };
            txtMotivo = new Guna.UI2.WinForms.Guna2TextBox { Location = new System.Drawing.Point(30, 170), Size = new System.Drawing.Size(420, 80), BorderRadius = 8, Multiline = true };

            btnGuardar = new Guna.UI2.WinForms.Guna2Button { Text = "Registrar", Location = new System.Drawing.Point(200, 260), Size = new System.Drawing.Size(120, 40), BorderRadius = 8, FillColor = System.Drawing.Color.FromArgb(76, 175, 80), Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.White };
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Guna.UI2.WinForms.Guna2Button { Text = "Cancelar", Location = new System.Drawing.Point(330, 260), Size = new System.Drawing.Size(120, 40), BorderRadius = 8, FillColor = System.Drawing.Color.FromArgb(244, 67, 54), Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.White };
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] { label1, lblMedicamento, label2, lblStockActual, label3, numCantidad, label4, txtMotivo, btnGuardar, btnCancelar });
        }

        private Label lblMedicamento, lblStockActual;
        private Guna.UI2.WinForms.Guna2NumericUpDown numCantidad;
        private Guna.UI2.WinForms.Guna2TextBox txtMotivo;
        private Guna.UI2.WinForms.Guna2Button btnGuardar, btnCancelar;

        private void ConfigurarFormulario()
        {
            // Configurar según tipo de movimiento
            if (tipoMovimiento == "Salida")
            {
                numCantidad.Maximum = medicamento.Stock;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = (int)numCantidad.Value;
                string motivo = txtMotivo.Text.Trim();

                if (tipoMovimiento == "Salida" && cantidad > medicamento.Stock)
                {
                    MessageBox.Show("No hay suficiente stock disponible.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int stockAnterior = medicamento.Stock;
                int stockNuevo = tipoMovimiento == "Entrada"
                    ? stockAnterior + cantidad
                    : stockAnterior - cantidad;

                string usuarioRegistro = ModuloLogin.SesionUsuario.UsuarioActual?.NombreUsuario ?? "Sistema";

                // Actualizar stock
                using (var nMedicamento = new NMedicamento())
                {
                    if (tipoMovimiento == "Entrada")
                    {
                        // Pasar cantidad positiva para incrementar
                        nMedicamento.ActualizarStock(medicamento.MedicamentoId, cantidad);
                    }
                    else // Salida
                    {
                        // Pasar cantidad negativa para decrementar
                        nMedicamento.ActualizarStock(medicamento.MedicamentoId, -cantidad);
                    }
                }

                // Registrar movimiento en historial
                using (var nInventario = new NInventario())
                {
                    if (tipoMovimiento == "Entrada")
                    {
                        nInventario.RegistrarEntrada(
                            medicamento.MedicamentoId,
                            cantidad,
                            stockAnterior,
                            stockNuevo,
                            motivo,
                            usuarioRegistro
                        );
                    }
                    else // Salida
                    {
                        nInventario.RegistrarSalida(
                            medicamento.MedicamentoId,
                            cantidad,
                            stockAnterior,
                            stockNuevo,
                            motivo,
                            usuarioRegistro
                        );
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al registrar movimiento: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
