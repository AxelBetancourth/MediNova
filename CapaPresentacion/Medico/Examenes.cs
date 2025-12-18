using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaNegocio.Medico;
using CapaNegocio.Recepcionista;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class Examenes : Form
    {
        private TPaciente pacienteSeleccionado;
        private TExpediente expedienteActual;
        private List<TExamen> examenesDelPaciente;
        private Form formularioOrigen; // Formulario que abrió Examenes
        private TConsulta consultaOrigen; // Consulta desde la que se abrió (si aplica)
        private bool modoSoloLectura = false;

        public Examenes()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        // Constructor sobrecargado para abrir con un paciente pre-cargado
        public Examenes(TPaciente paciente)
        {
            InitializeComponent();
            ConfigurarFormulario();

            if (paciente != null)
            {
                // Ocultar el panel de búsqueda ya que el paciente ya está seleccionado
                panelBusqueda.Visible = false;
                CargarExpedientePaciente(paciente);
            }
        }

        // Constructor sobrecargado para abrir desde una consulta
        public Examenes(TPaciente paciente, TConsulta consulta) : this(paciente)
        {
            consultaOrigen = consulta;

            // Si la consulta está finalizada, activar modo solo lectura
            if (consulta != null && consulta.Estado == "Finalizada")
            {
                ActivarModoSoloLectura();
            }
        }

        private void ActivarModoSoloLectura()
        {
            modoSoloLectura = true;

            // Mostrar mensaje informativo
            MessageBox.Show("Esta consulta está finalizada.\n\n" +
                "Solo puede ver los exámenes.\n" +
                "No se pueden agregar, editar o eliminar exámenes.",
                "Consulta Finalizada - Solo Lectura",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Deshabilitar botones de edición
            BtnNuevoExamen.Enabled = false;
            BtnEditarExamen.Enabled = false;
            BtnEliminarExamen.Enabled = false;

            // Cambiar texto del título para indicar modo solo lectura
            this.Text = "Gestión de Exámenes Médicos (Solo Lectura)";
        }

        // Método para establecer el formulario de origen
        public void EstablecerFormularioOrigen(Form origen)
        {
            formularioOrigen = origen;
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Gestión de Exámenes Médicos";
            ConfigurarGridExamenes();

            panelExpediente.Visible = false;
            panelExamenes.Visible = false;
            txtBuscarPaciente.Focus();
        }

        private void ConfigurarGridExamenes()
        {
            dgvExamenes.AutoGenerateColumns = false;
            dgvExamenes.Columns.Clear();
            dgvExamenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExamenes.MultiSelect = false;
            dgvExamenes.ReadOnly = true;
            dgvExamenes.AllowUserToAddRows = false;
            dgvExamenes.AllowUserToDeleteRows = false;
            dgvExamenes.RowHeadersVisible = false;
            dgvExamenes.BackgroundColor = Color.White;
            dgvExamenes.BorderStyle = BorderStyle.None;
            dgvExamenes.RowTemplate.Height = 40; // Aumentar altura de filas
            dgvExamenes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 245, 244); // Color verde claro para selección
            dgvExamenes.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvExamenes.DefaultCellStyle.BackColor = Color.White;
            dgvExamenes.DefaultCellStyle.Font = new Font("Arial", 10F);
            dgvExamenes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 150, 136); // Color del botón Examenes
            dgvExamenes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvExamenes.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            dgvExamenes.ColumnHeadersHeight = 45;
            dgvExamenes.EnableHeadersVisualStyles = false;

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExamenId",
                HeaderText = "ID",
                DataPropertyName = "ExamenId",
                Width = 60,
                Visible = false
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre del Examen",
                DataPropertyName = "Nombre",
                Width = 200
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tipo",
                HeaderText = "Tipo",
                DataPropertyName = "Tipo",
                Width = 120
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

            dgvExamenes.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "EsExterno",
                HeaderText = "Externo",
                DataPropertyName = "EsExterno",
                Width = 80
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaSolicitud",
                HeaderText = "F. Solicitud",
                DataPropertyName = "FechaSolicitud",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaResultado",
                HeaderText = "F. Resultado",
                DataPropertyName = "FechaResultado",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Costo",
                HeaderText = "Costo",
                DataPropertyName = "Costo",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" }
            });

            dgvExamenes.CellDoubleClick += DgvExamenes_CellDoubleClick;
        }

        private void BtnBuscarPaciente_Click(object sender, EventArgs e)
        {
            BuscarPaciente();
        }

        private void TxtBuscarPaciente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BuscarPaciente();
            }
        }

        private void BuscarPaciente()
        {
            try
            {
                string criterio = txtBuscarPaciente.Text.Trim();

                if (string.IsNullOrWhiteSpace(criterio))
                {
                    MessageBox.Show("Ingrese un DNI o nombre para buscar.",
                        "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var nPaciente = new NPacientes())
                {
                    var pacientes = nPaciente.BuscarPacientes(criterio);

                    if (pacientes == null || pacientes.Count == 0)
                    {
                        MessageBox.Show("No se encontraron pacientes con ese criterio.",
                            "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (pacientes.Count == 1)
                    {
                        CargarExpedientePaciente(pacientes[0]);
                    }
                    else
                    {
                        MostrarSeleccionPaciente(pacientes);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar paciente: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarSeleccionPaciente(List<TPaciente> pacientes)
        {
            using (var formSeleccion = new Form())
            {
                formSeleccion.Text = "Seleccionar Paciente";
                formSeleccion.Size = new Size(800, 500);
                formSeleccion.StartPosition = FormStartPosition.CenterParent;
                formSeleccion.BackColor = Color.White;

                var dgv = new DataGridView
                {
                    Location = new Point(20, 20),
                    Size = new Size(760, 380),
                    AutoGenerateColumns = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    BackgroundColor = Color.White
                };

                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNI", HeaderText = "DNI", DataPropertyName = "DNI", Width = 120 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreCompleto", HeaderText = "Nombre Completo", DataPropertyName = "NombreCompleto", Width = 300 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Telefono", HeaderText = "Teléfono", DataPropertyName = "Telefono", Width = 100 });

                dgv.DataSource = pacientes;

                var btnSeleccionar = new Button
                {
                    Text = "Seleccionar",
                    Location = new Point(310, 415),
                    Size = new Size(180, 45),
                    BackColor = Color.FromArgb(94, 148, 255),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };

                btnSeleccionar.Click += (s, ev) =>
                {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        var paciente = dgv.SelectedRows[0].DataBoundItem as TPaciente;
                        formSeleccion.Tag = paciente;
                        formSeleccion.DialogResult = DialogResult.OK;
                        formSeleccion.Close();
                    }
                };

                dgv.CellDoubleClick += (s, ev) =>
                {
                    if (ev.RowIndex >= 0)
                    {
                        var paciente = dgv.Rows[ev.RowIndex].DataBoundItem as TPaciente;
                        formSeleccion.Tag = paciente;
                        formSeleccion.DialogResult = DialogResult.OK;
                        formSeleccion.Close();
                    }
                };

                formSeleccion.Controls.Add(dgv);
                formSeleccion.Controls.Add(btnSeleccionar);

                if (formSeleccion.ShowDialog(this) == DialogResult.OK)
                {
                    var pacienteSelec = formSeleccion.Tag as TPaciente;
                    if (pacienteSelec != null)
                    {
                        CargarExpedientePaciente(pacienteSelec);
                    }
                }
            }
        }

        private void CargarExpedientePaciente(TPaciente paciente)
        {
            try
            {
                pacienteSeleccionado = paciente;

                using (var nExp = new NExpediente())
                {
                    expedienteActual = nExp.BuscarPorPacienteId(paciente.PacienteId);
                }

                if (expedienteActual == null)
                {
                    MessageBox.Show("Este paciente no tiene expediente creado.",
                        "Expediente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    panelExpediente.Visible = false;
                    panelExamenes.Visible = false;
                    return;
                }

                // Mostrar información del paciente
                lblNombrePaciente.Text = paciente.NombreCompleto ?? "N/A";
                lblDNI.Text = string.Format("DNI: {0}", paciente.DNI ?? "N/A");
                lblNumeroExpediente.Text = expedienteActual.NumeroExpediente ?? "Sin expediente";

                panelExpediente.Visible = true;
                panelExamenes.Visible = true;

                CargarExamenesPaciente();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar expediente: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarExamenesPaciente()
        {
            try
            {
                if (expedienteActual == null)
                {
                    dgvExamenes.DataSource = null;
                    return;
                }

                using (var nExamen = new NExamen())
                {
                    examenesDelPaciente = nExamen.BuscarPorExpedienteId(expedienteActual.PacienteId);

                    dgvExamenes.DataSource = null;

                    if (examenesDelPaciente != null && examenesDelPaciente.Count > 0)
                    {
                        dgvExamenes.DataSource = examenesDelPaciente;
                        dgvExamenes.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar exámenes: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevoExamen_Click(object sender, EventArgs e)
        {
            if (modoSoloLectura)
            {
                MessageBox.Show("No se pueden agregar exámenes cuando la consulta está finalizada.",
                    "Modo Solo Lectura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (expedienteActual == null) return;

            MostrarFormularioExamen(null);
        }

        private void BtnEditarExamen_Click(object sender, EventArgs e)
        {
            if (modoSoloLectura)
            {
                MessageBox.Show("No se pueden editar exámenes cuando la consulta está finalizada.",
                    "Modo Solo Lectura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvExamenes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un examen para editar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var examen = dgvExamenes.SelectedRows[0].DataBoundItem as TExamen;
            if (examen != null)
            {
                if (examen.Estado == "Completado")
                {
                    MessageBox.Show("No se puede editar un examen completado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MostrarFormularioExamen(examen);
            }
        }

        private void DgvExamenes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var examen = dgvExamenes.Rows[e.RowIndex].DataBoundItem as TExamen;
            if (examen != null)
            {
                MostrarFormularioExamen(examen);
            }
        }

        private void MostrarFormularioExamen(TExamen examen)
        {
            // Buscar el panel principal
            Panel panelMain = EncontrarPanelMain(this);
            if (panelMain != null)
            {
                // Abrir en el panel con navegación
                bool esNuevo = examen == null;
                ucEditarExamen ucEditar;

                if (esNuevo)
                {
                    var nuevoExamen = new TExamen
                    {
                        ExpedienteId = expedienteActual.PacienteId,
                        FechaSolicitud = DateTime.Now,
                        Estado = "Pendiente"
                    };
                    ucEditar = new ucEditarExamen(nuevoExamen, expedienteActual.PacienteId);
                }
                else
                {
                    ucEditar = new ucEditarExamen(examen);
                }

                // Establecer el formulario de origen y suscribirse al evento
                ucEditar.EstablecerFormularioOrigen(this);
                ucEditar.ExamenGuardado += (s, e) => CargarExamenesPaciente();

                // Cargar en el panel
                panelMain.Controls.Clear();
                ucEditar.Dock = DockStyle.Fill;
                panelMain.Controls.Add(ucEditar);
                return;
            }

            // Fallback: Usar el método anterior con ShowDialog si no hay panel
            using (var formExamen = new Form())
            {
                formExamen.Text = examen == null ? "Nuevo Examen" : "Editar Examen";
                formExamen.Size = new Size(600, 550);
                formExamen.StartPosition = FormStartPosition.CenterParent;
                formExamen.FormBorderStyle = FormBorderStyle.FixedDialog;
                formExamen.MaximizeBox = false;

                bool esNuevo = examen == null;
                if (esNuevo)
                {
                    examen = new TExamen
                    {
                        ExpedienteId = expedienteActual.PacienteId,
                        FechaSolicitud = DateTime.Now,
                        Estado = "Pendiente"
                    };
                }

                // Controles
                var lblNombre = new Label { Text = "Nombre del Examen:", Location = new Point(20, 20), Size = new Size(150, 20) };
                var txtNombre = new TextBox { Location = new Point(180, 18), Size = new Size(380, 25), Text = examen.Nombre };

                var lblTipo = new Label { Text = "Tipo:", Location = new Point(20, 55), Size = new Size(150, 20) };
                var cboTipo = new ComboBox { Location = new Point(180, 53), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                cboTipo.Items.AddRange(new object[] { "Sangre", "Orina", "Rayos X", "Ultrasonido", "Tomografía", "Resonancia", "Electrocardiograma", "Otro" });
                cboTipo.Text = examen.Tipo;

                var lblCosto = new Label { Text = "Costo (L):", Location = new Point(20, 90), Size = new Size(150, 20) };
                var txtCosto = new TextBox { Location = new Point(180, 88), Size = new Size(150, 25), Text = examen.Costo.ToString("F2") };

                var lblEstado = new Label { Text = "Estado:", Location = new Point(20, 125), Size = new Size(150, 20) };
                var cboEstado = new ComboBox { Location = new Point(180, 123), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                cboEstado.Items.AddRange(new object[] { "Pendiente", "Completado", "Pagado", "Cancelado" });

                // Convertir estados antiguos a los nuevos
                string estadoActual = examen.Estado ?? "Pendiente";
                if (estadoActual == "Solicitado" || estadoActual == "EnProceso")
                {
                    estadoActual = "Pendiente";
                }
                cboEstado.Text = estadoActual;

                var lblFechaSolicitud = new Label { Text = "Fecha Solicitud:", Location = new Point(20, 160), Size = new Size(150, 20) };
                var dtpFechaSolicitud = new DateTimePicker { Location = new Point(180, 158), Size = new Size(200, 25), Value = examen.FechaSolicitud };

                var lblFechaResultado = new Label { Text = "Fecha Resultado:", Location = new Point(20, 195), Size = new Size(150, 20) };
                var dtpFechaResultado = new DateTimePicker { Location = new Point(180, 193), Size = new Size(200, 25), Value = examen.FechaResultado ?? DateTime.Now };
                var chkTieneResultado = new CheckBox { Text = "Tiene resultado", Location = new Point(390, 195), Size = new Size(150, 20), Checked = examen.FechaResultado.HasValue };

                var lblResultado = new Label { Text = "Resultado:", Location = new Point(20, 230), Size = new Size(150, 20) };
                var txtResultado = new TextBox { Location = new Point(20, 255), Size = new Size(540, 150), Multiline = true, Text = examen.Resultado };

                var btnGuardar = new Button
                {
                    Text = "Guardar",
                    Location = new Point(250, 440),
                    Size = new Size(120, 40),
                    BackColor = Color.FromArgb(94, 148, 255),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                var btnCancelar = new Button
                {
                    Text = "Cancelar",
                    Location = new Point(380, 440),
                    Size = new Size(120, 40),
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };

                btnGuardar.Click += (s, ev) =>
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(txtNombre.Text))
                        {
                            MessageBox.Show("El nombre del examen es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (cboTipo.SelectedIndex == -1)
                        {
                            MessageBox.Show("Seleccione el tipo de examen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!decimal.TryParse(txtCosto.Text, out decimal costo) || costo < 0)
                        {
                            MessageBox.Show("Ingrese un costo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        examen.Nombre = txtNombre.Text.Trim();
                        examen.Tipo = cboTipo.SelectedItem.ToString();
                        examen.Costo = costo;
                        examen.FechaSolicitud = dtpFechaSolicitud.Value;
                        examen.FechaResultado = chkTieneResultado.Checked ? dtpFechaResultado.Value : (DateTime?)null;
                        examen.Resultado = txtResultado.Text.Trim();

                        // Cambiar automáticamente a "Completado" si tiene resultado
                        if (chkTieneResultado.Checked && !string.IsNullOrWhiteSpace(txtResultado.Text.Trim()))
                        {
                            examen.Estado = "Completado";
                        }
                        else
                        {
                            examen.Estado = cboEstado.SelectedItem?.ToString() ?? "Pendiente";
                        }

                        using (var nExamen = new NExamen())
                        {
                            if (esNuevo)
                            {
                                nExamen.SolicitarExamen(examen);
                                MessageBox.Show("Examen agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                nExamen.EditarExamen(examen);
                                MessageBox.Show("Examen actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        formExamen.DialogResult = DialogResult.OK;
                        formExamen.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al guardar examen: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                btnCancelar.Click += (s, ev) =>
                {
                    formExamen.DialogResult = DialogResult.Cancel;
                    formExamen.Close();
                };

                formExamen.Controls.Add(lblNombre);
                formExamen.Controls.Add(txtNombre);
                formExamen.Controls.Add(lblTipo);
                formExamen.Controls.Add(cboTipo);
                formExamen.Controls.Add(lblCosto);
                formExamen.Controls.Add(txtCosto);
                formExamen.Controls.Add(lblEstado);
                formExamen.Controls.Add(cboEstado);
                formExamen.Controls.Add(lblFechaSolicitud);
                formExamen.Controls.Add(dtpFechaSolicitud);
                formExamen.Controls.Add(lblFechaResultado);
                formExamen.Controls.Add(dtpFechaResultado);
                formExamen.Controls.Add(chkTieneResultado);
                formExamen.Controls.Add(lblResultado);
                formExamen.Controls.Add(txtResultado);
                formExamen.Controls.Add(btnGuardar);
                formExamen.Controls.Add(btnCancelar);

                if (formExamen.ShowDialog() == DialogResult.OK)
                {
                    CargarExamenesPaciente();
                }
            }
        }

        private void BtnEliminarExamen_Click(object sender, EventArgs e)
        {
            if (modoSoloLectura)
            {
                MessageBox.Show("No se pueden eliminar exámenes cuando la consulta está finalizada.",
                    "Modo Solo Lectura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvExamenes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un examen para eliminar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var examen = dgvExamenes.SelectedRows[0].DataBoundItem as TExamen;
            if (examen != null)
            {
                var resultado = MessageBox.Show(
                    string.Format("¿Está seguro de eliminar el examen '{0}'?", examen.Nombre),
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (var nExamen = new NExamen())
                        {
                            nExamen.EliminarExamen(examen.ExamenId);
                        }

                        MessageBox.Show("Examen eliminado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarExamenesPaciente();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al eliminar examen: {0}", ex.Message),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarPaciente.Clear();
            pacienteSeleccionado = null;
            expedienteActual = null;
            examenesDelPaciente = null;
            dgvExamenes.DataSource = null;

            panelExpediente.Visible = false;
            panelExamenes.Visible = false;

            txtBuscarPaciente.Focus();
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            // Buscar el panel principal
            Panel panelMain = EncontrarPanelMain(this);
            if (panelMain != null && formularioOrigen != null)
            {
                // Regresar al formulario de origen
                panelMain.Controls.Clear();
                formularioOrigen.TopLevel = false;
                formularioOrigen.FormBorderStyle = FormBorderStyle.None;
                formularioOrigen.Dock = DockStyle.Fill;
                panelMain.Controls.Add(formularioOrigen);
                formularioOrigen.Show();
            }
            else
            {
                // Si no hay panel principal, simplemente cerrar
                this.Close();
            }
        }

        private Panel EncontrarPanelMain(Control control)
        {
            // Buscar recursivamente el panel_main
            Control actual = control.Parent;
            while (actual != null)
            {
                if (actual is Panel && actual.Name == "panel_main")
                {
                    return (Panel)actual;
                }
                actual = actual.Parent;
            }
            return null;
        }
    }
}
