using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaNegocio.Compartido;
using CapaNegocio.Farmacia;
using CapaNegocio.Medico;
using CapaNegocio.Recepcionista;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class Consultas : Form
    {
        private TPaciente pacienteSeleccionado;
        private TExpediente expedienteActual;
        private List<TConsulta> consultasDelPaciente;
        private bool mostrarSoloFinalizadas = false;

        public Consultas()
        {
            InitializeComponent();

            // Mejorar renderizado de texto
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point);

            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar título
            this.Text = "Consultas Médicas";

            // Configurar eventos de búsqueda
            BotonBuscar.KeyPress += BotonBuscar_KeyPress;
            BtnBuscar.Click += BtnBuscar_Click;
            BtnLimpiar.Click += BtnLimpiar_Click;

            // Configurar eventos de botones del expediente
            BtnNuevaConsulta.Click += BtnNuevaConsulta_Click;
            BtnVerConsultas.Click += BtnVerConsultas_Click;
            BtnEditarExpediente.Click += BtnEditarExpediente_Click;

            // Agregar botón de exportar PDF
            AgregarBotonExportarPDF();

            // Configurar eventos de botones del grid
            BtnEditar.Click += BtnEditarConsulta_Click;
            BtnEliminar.Click += BtnEliminarConsulta_Click;

            // Configurar grid de consultas
            ConfigurarGridConsultas();

            // Agregar botón de filtro para consultas finalizadas
            AgregarBotonFiltroConsultas();

            // Configurar estado inicial
            panelExpediente.Visible = false;
            panelConsultas.Visible = false;
            BotonBuscar.Focus();
        }

        private void AgregarBotonFiltroConsultas()
        {
            // El botón ya está en el Designer (BtnTodas), no necesitamos crearlo dinámicamente
        }

        private void BtnTodas_Click(object sender, EventArgs e)
        {
            mostrarSoloFinalizadas = !mostrarSoloFinalizadas;

            if (mostrarSoloFinalizadas)
            {
                BtnTodas.Text = "✓ Finalizadas";
                BtnTodas.FillColor = Color.FromArgb(33, 150, 243);
            }
            else
            {
                BtnTodas.Text = "Todas";
                BtnTodas.FillColor = Color.FromArgb(76, 175, 80);
            }

            // Recargar consultas con el nuevo filtro
            if (panelConsultas.Visible)
            {
                CargarConsultasPaciente();
            }
        }

        private void AgregarBotonExportarPDF()
        {
            // Buscar el panel de botones de acción
            var panel2 = this.Controls.Find("panelExpediente", true).FirstOrDefault();
            if (panel2 != null)
            {
                var guna2Panel2 = panel2.Controls.Find("guna2Panel2", true).FirstOrDefault() as Guna.UI2.WinForms.Guna2Panel;
                if (guna2Panel2 != null)
                {
                    var btnExportarPDF = new Guna.UI2.WinForms.Guna2Button
                    {
                        Name = "BtnExportarPDF",
                        Text = "Exportar PDF",
                        BorderRadius = 8,
                        FillColor = Color.FromArgb(156, 39, 176),
                        Font = new Font("Arial", 10F, FontStyle.Bold),
                        ForeColor = Color.White,
                        Dock = DockStyle.Top,
                        Height = 50,
                        Margin = new Padding(0, 0, 0, 8)
                    };

                    btnExportarPDF.Click += BtnExportarPDF_Click;
                    guna2Panel2.Controls.Add(btnExportarPDF);
                    btnExportarPDF.BringToFront();
                }
            }
        }

        private void ConfigurarGridConsultas()
        {
            DtgUsuarios.AutoGenerateColumns = false;
            DtgUsuarios.Columns.Clear();
            DtgUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DtgUsuarios.MultiSelect = false;
            DtgUsuarios.ReadOnly = true;
            DtgUsuarios.AllowUserToAddRows = false;
            DtgUsuarios.AllowUserToDeleteRows = false;
            DtgUsuarios.RowHeadersVisible = false;

            // Mejorar renderizado de fuentes
            DtgUsuarios.DefaultCellStyle.Font = new Font("Arial", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            DtgUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point);
            DtgUsuarios.DefaultCellStyle.Padding = new Padding(6, 3, 6, 3);
            DtgUsuarios.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 3, 6, 3);
            DtgUsuarios.RowTemplate.Height = 40;

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ConsultaId",
                HeaderText = "ID",
                DataPropertyName = "ConsultaId",
                Width = 60,
                Visible = false
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumeroConsulta",
                HeaderText = "N° Consulta",
                DataPropertyName = "NumeroConsulta",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 3, 8, 3)
                }
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaConsulta",
                HeaderText = "Fecha",
                DataPropertyName = "FechaConsulta",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm",
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 3, 8, 3)
                }
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MotivoConsulta",
                HeaderText = "Motivo de Consulta",
                DataPropertyName = "MotivoConsulta",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 220,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 3, 8, 3),
                    WrapMode = DataGridViewTriState.False
                }
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreDoctor",
                HeaderText = "Médico",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 3, 8, 3)
                }
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Diagnostico",
                HeaderText = "Diagnóstico",
                DataPropertyName = "Diagnostico",
                Width = 220,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 3, 8, 3),
                    WrapMode = DataGridViewTriState.False
                }
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(8, 3, 8, 3)
                }
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EstadoPago",
                HeaderText = "Pago",
                DataPropertyName = "EstadoPago",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Padding = new Padding(8, 3, 8, 3)
                }
            });

            DtgUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CostoConsulta",
                HeaderText = "Costo (L)",
                DataPropertyName = "CostoConsulta",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "L #,##0.00",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Padding = new Padding(8, 3, 8, 3)
                }
            });

            DtgUsuarios.CellDoubleClick += DtgUsuarios_CellDoubleClick;
            DtgUsuarios.CellFormatting += DtgUsuarios_CellFormatting;
        }

        private void BotonBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BuscarPaciente();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPaciente();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            BotonBuscar.Clear();
            pacienteSeleccionado = null;
            expedienteActual = null;
            consultasDelPaciente = null;
            DtgUsuarios.DataSource = null;

            panelExpediente.Visible = false;
            panelConsultas.Visible = false;

            BotonBuscar.Focus();
        }

        private void BuscarPaciente()
        {
            try
            {
                string criterio = BotonBuscar.Text.Trim();

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

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "DNI",
                    HeaderText = "DNI",
                    DataPropertyName = "DNI",
                    Width = 120
                });
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "NombreCompleto",
                    HeaderText = "Nombre Completo",
                    DataPropertyName = "NombreCompleto",
                    Width = 300
                });
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "FechaNacimiento",
                    HeaderText = "Fecha Nac.",
                    DataPropertyName = "FechaNacimiento",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
                });
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Sexo",
                    HeaderText = "Sexo",
                    DataPropertyName = "Sexo",
                    Width = 80
                });
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Telefono",
                    HeaderText = "Teléfono",
                    DataPropertyName = "Telefono",
                    Width = 100
                });

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
                    MessageBox.Show("Este paciente no tiene expediente creado.\n\nDebe completar el expediente primero.",
                        "Expediente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    panelExpediente.Visible = false;
                    panelConsultas.Visible = false;
                    BtnNuevaConsulta.Enabled = false;
                    return;
                }

                // Mostrar información del paciente
                lblNombrePaciente.Text = paciente.NombreCompleto ?? "N/A";
                lblDNI.Text = string.Format("DNI: {0}", paciente.DNI ?? "N/A");
                lblSexo.Text = string.Format("Sexo: {0}", paciente.Sexo ?? "N/A");
                lblTelefono.Text = string.Format("📞 {0}", paciente.Telefono ?? "N/A");
                lblDireccion.Text = string.Format("📍 {0}", paciente.Direccion ?? "N/A");

                if (paciente.FechaNacimiento != null)
                {
                    int edad = CalcularEdad(paciente.FechaNacimiento);
                    lblEdad.Text = string.Format("Edad: {0} a.", edad);
                }
                else
                {
                    lblEdad.Text = "Edad: N/A";
                }

                // Mostrar información del expediente
                lblNumeroExpediente.Text = expedienteActual.NumeroExpediente ?? "Sin expediente";
                lblTipoSangre.Text = expedienteActual.TipoSangre ?? "No especificado";
                lblAlergias.Text = string.IsNullOrWhiteSpace(expedienteActual.Alergias) ?
                    "Ninguna" : expedienteActual.Alergias;
                lblContactoEmergencia.Text = expedienteActual.ContactoEmergencia ?? "N/A";
                lblTelefonoEmergencia.Text = expedienteActual.TelefonoEmergencia ?? "N/A";

                BtnNuevaConsulta.Enabled = true;

                // Mostrar solo panel de expediente (consultas se muestra con el botón)
                panelExpediente.Visible = true;
                panelConsultas.Visible = false;
                BtnVerConsultas.Text = "Ver Consultas";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar expediente: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }

        private void CargarConsultasPaciente()
        {
            try
            {
                if (expedienteActual == null)
                {
                    DtgUsuarios.DataSource = null;
                    MessageBox.Show("No hay expediente seleccionado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var nConsulta = new NConsulta())
                {
                    consultasDelPaciente = nConsulta.ObtenerConsultasPorExpediente(expedienteActual.PacienteId);

                    // Limpiar el DataSource primero
                    DtgUsuarios.DataSource = null;

                    if (consultasDelPaciente != null && consultasDelPaciente.Count > 0)
                    {
                        // Filtrar según el estado seleccionado
                        var consultasFiltradas = mostrarSoloFinalizadas
                            ? consultasDelPaciente.Where(c => c.Estado == "Finalizada").ToList()
                            : consultasDelPaciente;

                        // Asignar el DataSource
                        DtgUsuarios.DataSource = consultasFiltradas.ToList();
                        DtgUsuarios.Refresh();

                        // Actualizar el título con la cantidad
                        ActualizarTituloGrid(consultasFiltradas.Count(), consultasDelPaciente.Count);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Este paciente aún no tiene consultas registradas.\n\nPaciente ID: {0}", expedienteActual.PacienteId),
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar consultas: {0}\n\nStack: {1}", ex.Message, ex.StackTrace),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarTituloGrid(int consultasMostradas, int consultasTotales)
        {
            var label = panelAccionesGrid.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "label2");
            if (label != null)
            {
                if (mostrarSoloFinalizadas)
                {
                    label.Text = string.Format("Consultas Finalizadas ({0} de {1} totales)", consultasMostradas, consultasTotales);
                }
                else
                {
                    label.Text = string.Format("Historial de Consultas ({0} consultas)", consultasTotales);
                }
            }
        }

        private void DtgUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var consulta = DtgUsuarios.Rows[e.RowIndex].DataBoundItem as TConsulta;
            if (consulta != null)
            {
                AbrirConsultaExistente(consulta.ConsultaId);
            }
        }

        private void DtgUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DtgUsuarios.Columns[e.ColumnIndex].Name == "NombreDoctor")
            {
                var consulta = DtgUsuarios.Rows[e.RowIndex].DataBoundItem as TConsulta;
                if (consulta != null && consulta.Doctor != null)
                {
                    e.Value = consulta.Doctor.NombreCompleto;
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = "No asignado";
                    e.FormattingApplied = true;
                }
            }
        }

        private void BtnNuevaConsulta_Click(object sender, EventArgs e)
        {
            if (pacienteSeleccionado == null || expedienteActual == null) return;

            // Verificar que el expediente esté completo
            if (string.IsNullOrWhiteSpace(expedienteActual.TipoSangre) ||
                string.IsNullOrWhiteSpace(expedienteActual.ContactoEmergencia))
            {
                var resultado = MessageBox.Show(
                    "El expediente del paciente no está completo.\n\n¿Desea completarlo ahora?",
                    "Expediente Incompleto",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    var formExpediente = new CompletarExpediente(pacienteSeleccionado.PacienteId);
                    if (formExpediente.ShowDialog(this) == DialogResult.OK)
                    {
                        // Recargar expediente
                        using (var nExp = new NExpediente())
                        {
                            expedienteActual = nExp.BuscarPorPacienteId(pacienteSeleccionado.PacienteId);
                        }

                        if (expedienteActual != null)
                        {
                            CargarExpedientePaciente(pacienteSeleccionado);
                        }
                    }
                }
                return;
            }

            // Verificar que no haya consultas en progreso
            using (var nConsulta = new NConsulta())
            {
                var consultasEnProgreso = nConsulta.ObtenerConsultasPorExpediente(expedienteActual.PacienteId)
                    .Where(c => c.Estado == "EnProgreso")
                    .ToList();

                if (consultasEnProgreso.Count > 0)
                {
                    MessageBox.Show(string.Format("Este paciente tiene una consulta en progreso que debe finalizar primero.\n\nNúmero de consulta: {0}\nFecha: {1:dd/MM/yyyy HH:mm}", consultasEnProgreso[0].NumeroConsulta, consultasEnProgreso[0].FechaConsulta),
                        "Consulta en Progreso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                var citaTemporal = new TCita
                {
                    PacienteId = pacienteSeleccionado.PacienteId,
                    Asunto = "Consulta directa",
                    FechaHoraInicio = DateTime.Now
                };

                // Buscar el panel principal
                Panel panelMain = EncontrarPanelMain(this);

                if (panelMain != null)
                {
                    // Navegar a ConsultaMedica en el panel main
                    var formConsulta = new ConsultaMedica(citaTemporal);
                    formConsulta.EstablecerFormularioOrigen("Consultas"); // Regresar a Consultas
                    panelMain.Controls.Clear();
                    formConsulta.TopLevel = false;
                    formConsulta.FormBorderStyle = FormBorderStyle.None;
                    formConsulta.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(formConsulta);
                    formConsulta.Show();
                }
                else
                {
                    // Fallback a ShowDialog si no encuentra el panel
                    var formConsulta = new ConsultaMedica(citaTemporal);
                    if (formConsulta.ShowDialog(this) == DialogResult.OK)
                    {
                        CargarConsultasPaciente();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir consulta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditarConsulta_Click(object sender, EventArgs e)
        {
            if (DtgUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una consulta para editar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var consulta = DtgUsuarios.SelectedRows[0].DataBoundItem as TConsulta;
            if (consulta != null)
            {
                if (consulta.Estado == "Finalizada")
                {
                    MessageBox.Show("No se puede editar una consulta finalizada.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                AbrirConsultaExistente(consulta.ConsultaId);
            }
        }

        private void AbrirConsultaExistente(int consultaId)
        {
            try
            {
                // Buscar el panel principal
                Panel panelMain = EncontrarPanelMain(this);

                if (panelMain != null)
                {
                    // Navegar a ConsultaMedica en el panel main
                    var formConsulta = new ConsultaMedica(consultaId);
                    formConsulta.EstablecerFormularioOrigen("Consultas"); // Regresar a Consultas
                    panelMain.Controls.Clear();
                    formConsulta.TopLevel = false;
                    formConsulta.FormBorderStyle = FormBorderStyle.None;
                    formConsulta.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(formConsulta);
                    formConsulta.Show();
                }
                else
                {
                    // Fallback a ShowDialog si no encuentra el panel
                    var formConsulta = new ConsultaMedica(consultaId);
                    if (formConsulta.ShowDialog(this) == DialogResult.OK)
                    {
                        CargarConsultasPaciente();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir consulta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnEliminarConsulta_Click(object sender, EventArgs e)
        {
            if (DtgUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una consulta para eliminar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var consulta = DtgUsuarios.SelectedRows[0].DataBoundItem as TConsulta;
            if (consulta != null)
            {
                if (consulta.Estado == "Finalizada")
                {
                    MessageBox.Show("No se puede eliminar una consulta finalizada.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var resultado = MessageBox.Show(
                    string.Format("¿Está seguro de eliminar la consulta {0}?\n\nEsta acción no se puede deshacer.", consulta.NumeroConsulta),
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        using (var nConsulta = new NConsulta())
                        {
                            nConsulta.EliminarConsulta(consulta.ConsultaId);
                        }

                        MessageBox.Show("Consulta eliminada correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarConsultasPaciente();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al eliminar consulta: {0}", ex.Message),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnVerConsultas_Click(object sender, EventArgs e)
        {
            if (pacienteSeleccionado == null || expedienteActual == null)
            {
                MessageBox.Show("Primero debe buscar y seleccionar un paciente.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Toggle: mostrar/ocultar panel de consultas
            panelConsultas.Visible = !panelConsultas.Visible;

            if (panelConsultas.Visible)
            {
                BtnVerConsultas.Text = "Ocultar Consultas";
                CargarConsultasPaciente();

                // Forzar actualización del layout y visibilidad
                panelPrincipal.SuspendLayout();
                panelConsultas.BringToFront();
                panelConsultas.Height = Math.Max(300, panelPrincipal.Height - panelExpediente.Height - panelBusqueda.Height - 60);
                panelPrincipal.ResumeLayout(true);
                panelPrincipal.PerformLayout();

                panelConsultas.Refresh();
                DtgUsuarios.Refresh();
                this.Refresh();
            }
            else
            {
                BtnVerConsultas.Text = "Ver Consultas";
            }
        }

        private void BtnEditarExpediente_Click(object sender, EventArgs e)
        {
            if (pacienteSeleccionado == null || expedienteActual == null)
            {
                MessageBox.Show("Primero debe buscar y seleccionar un paciente.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Buscar el panel principal
                Panel panelMain = EncontrarPanelMain(this);

                if (panelMain != null)
                {
                    // Navegar a CompletarExpediente en el panel main
                    var formExpediente = new CompletarExpediente(pacienteSeleccionado.PacienteId);
                    panelMain.Controls.Clear();
                    formExpediente.TopLevel = false;
                    formExpediente.FormBorderStyle = FormBorderStyle.None;
                    formExpediente.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(formExpediente);
                    formExpediente.Show();
                }
                else
                {
                    // Fallback a ShowDialog si no encuentra el panel
                    var formExpediente = new CompletarExpediente(pacienteSeleccionado.PacienteId);
                    if (formExpediente.ShowDialog(this) == DialogResult.OK)
                    {
                        // Recargar expediente después de editar
                        using (var nExp = new NExpediente())
                        {
                            expedienteActual = nExp.BuscarPorPacienteId(pacienteSeleccionado.PacienteId);
                        }

                        if (expedienteActual != null)
                        {
                            CargarExpedientePaciente(pacienteSeleccionado);
                        }

                        MessageBox.Show("Expediente actualizado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al editar expediente: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportarPDF_Click(object sender, EventArgs e)
        {
            if (pacienteSeleccionado == null || expedienteActual == null)
            {
                MessageBox.Show("Primero debe buscar y seleccionar un paciente.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Crear SaveFileDialog para seleccionar ubicación
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                    saveDialog.Title = "Guardar Expediente como PDF";
                    saveDialog.FileName = string.Format("Expediente_{0}_{1:yyyyMMdd}.pdf", pacienteSeleccionado.DNI, DateTime.Now);

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        GenerarPDFExpediente(saveDialog.FileName);
                        MessageBox.Show("Expediente exportado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Preguntar si desea abrir el PDF
                        var result = MessageBox.Show("¿Desea abrir el archivo PDF?",
                            "Abrir PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al exportar expediente: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarPDFExpediente(string rutaPDF)
        {
            try
            {
                using (var fs = new System.IO.FileStream(rutaPDF, System.IO.FileMode.Create))
                {
                    var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 30, 30, 40, 40);
                    var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);

                    document.Open();

                    // Colores personalizados
                    var colorPrimario = new iTextSharp.text.BaseColor(41, 128, 185); // Azul profesional
                    var colorSecundario = new iTextSharp.text.BaseColor(52, 73, 94); // Gris oscuro
                    var colorExito = new iTextSharp.text.BaseColor(46, 204, 113); // Verde
                    var colorFondo = new iTextSharp.text.BaseColor(236, 240, 241); // Gris claro
                    var colorBorde = new iTextSharp.text.BaseColor(189, 195, 199); // Gris medio

                    // Fuentes mejoradas
                    var fontTitle = iTextSharp.text.FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, colorPrimario);
                    var fontSubtitle = iTextSharp.text.FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, colorSecundario);
                    var fontSectionTitle = iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE);
                    var fontLabel = iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, colorSecundario);
                    var fontNormal = iTextSharp.text.FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    var fontSmall = iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.GRAY);

                    // Encabezado con fondo de color
                    var headerTable = new iTextSharp.text.pdf.PdfPTable(1) { WidthPercentage = 100, SpacingAfter = 20 };
                    var headerCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("EXPEDIENTE MÉDICO ELECTRÓNICO", fontTitle));
                    headerCell.BackgroundColor = colorFondo;
                    headerCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    headerCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    headerCell.PaddingTop = 15;
                    headerCell.PaddingBottom = 15;
                    headerTable.AddCell(headerCell);
                    document.Add(headerTable);

                    // Información del paciente con diseño mejorado
                    var sectionHeader = new iTextSharp.text.pdf.PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 5, SpacingAfter = 8 };
                    var sectionCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("📋 INFORMACIÓN DEL PACIENTE", fontSectionTitle));
                    sectionCell.BackgroundColor = colorPrimario;
                    sectionCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    sectionCell.Padding = 8;
                    sectionHeader.AddCell(sectionCell);
                    document.Add(sectionHeader);

                    var tablePaciente = new iTextSharp.text.pdf.PdfPTable(4) { WidthPercentage = 100, SpacingAfter = 15 };
                    tablePaciente.SetWidths(new float[] { 1.5f, 2.5f, 1.5f, 2.5f });

                    AgregarCeldaBonita(tablePaciente, "Nombre Completo:", pacienteSeleccionado.NombreCompleto, fontLabel, fontNormal, colorFondo, 4);
                    AgregarCeldaBonita(tablePaciente, "DNI:", pacienteSeleccionado.DNI, fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "N° Expediente:", expedienteActual.NumeroExpediente, fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "Fecha de Nacimiento:", string.Format("{0:dd/MM/yyyy}", pacienteSeleccionado.FechaNacimiento), fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "Edad:", string.Format("{0} años", CalcularEdad(pacienteSeleccionado.FechaNacimiento)), fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "Sexo:", pacienteSeleccionado.Sexo, fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "Tipo de Sangre:", expedienteActual.TipoSangre ?? "No especificado", fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "Teléfono:", pacienteSeleccionado.Telefono ?? "N/A", fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "Dirección:", pacienteSeleccionado.Direccion ?? "N/A", fontLabel, fontNormal, colorFondo);
                    AgregarCeldaBonita(tablePaciente, "Alergias:", expedienteActual.Alergias ?? "Ninguna", fontLabel, fontNormal, new iTextSharp.text.BaseColor(255, 235, 235), 4);
                    AgregarCeldaBonita(tablePaciente, "Contacto de Emergencia:", expedienteActual.ContactoEmergencia ?? "N/A", fontLabel, fontNormal, colorFondo, 2);
                    AgregarCeldaBonita(tablePaciente, "Tel. Emergencia:", expedienteActual.TelefonoEmergencia ?? "N/A", fontLabel, fontNormal, colorFondo, 2);

                    document.Add(tablePaciente);

                    // Cargar consultas directamente desde la base de datos
                    List<TConsulta> consultas = null;
                    try
                    {
                        using (var nConsulta = new NConsulta())
                        {
                            consultas = nConsulta.ObtenerConsultasPorExpediente(expedienteActual.PacienteId);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(string.Format("Error al cargar consultas: {0}", ex.Message));
                    }

                    // Historial de Consultas con diseño mejorado
                    if (consultas != null && consultas.Count > 0)
                    {
                        var consultasFinalizadas = consultas.Where(c => c.Estado == "Finalizada").OrderByDescending(c => c.FechaConsulta).ToList();

                        if (consultasFinalizadas.Count > 0)
                        {
                            var sectionConsultas = new iTextSharp.text.pdf.PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 10, SpacingAfter = 8 };
                            var sectionCellConsultas = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(string.Format("🩺 HISTORIAL DE CONSULTAS ({0})", consultasFinalizadas.Count), fontSectionTitle));
                            sectionCellConsultas.BackgroundColor = colorExito;
                            sectionCellConsultas.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            sectionCellConsultas.Padding = 8;
                            sectionConsultas.AddCell(sectionCellConsultas);
                            document.Add(sectionConsultas);

                            foreach (var consulta in consultasFinalizadas)
                            {
                                // Encabezado de consulta
                                var consultaHeader = new iTextSharp.text.pdf.PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 8 };
                                var consultaTitulo = string.Format("Consulta #{0} - {1:dd/MM/yyyy HH:mm}", consulta.NumeroConsulta, consulta.FechaConsulta);
                                var consultaCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(consultaTitulo, fontSubtitle));
                                consultaCell.BackgroundColor = new iTextSharp.text.BaseColor(240, 248, 255);
                                consultaCell.BorderColor = colorBorde;
                                consultaCell.Padding = 6;
                                consultaHeader.AddCell(consultaCell);
                                document.Add(consultaHeader);

                                // Detalles de la consulta
                                var tableConsulta = new iTextSharp.text.pdf.PdfPTable(2) { WidthPercentage = 100, SpacingAfter = 5 };
                                tableConsulta.SetWidths(new float[] { 1.2f, 2.8f });

                                AgregarCeldaConsulta(tableConsulta, "👨‍⚕️ Médico:", consulta.Doctor?.NombreCompleto ?? "No asignado", fontLabel, fontNormal, colorBorde);
                                AgregarCeldaConsulta(tableConsulta, "📝 Motivo:", consulta.MotivoConsulta ?? "N/A", fontLabel, fontNormal, colorBorde);

                                if (!string.IsNullOrWhiteSpace(consulta.Sintomas))
                                    AgregarCeldaConsulta(tableConsulta, "🤒 Síntomas:", consulta.Sintomas, fontLabel, fontNormal, colorBorde);

                                // Signos vitales
                                if (consulta.PresionArterial != null || consulta.Temperatura.HasValue || consulta.FrecuenciaCardiaca.HasValue)
                                {
                                    var vitales = "";
                                    if (consulta.PresionArterial != null) vitales += string.Format("PA: {0} | ", consulta.PresionArterial);
                                    if (consulta.Temperatura.HasValue) vitales += string.Format("Temp: {0:F1}°C | ", consulta.Temperatura);
                                    if (consulta.FrecuenciaCardiaca.HasValue) vitales += string.Format("FC: {0} lpm | ", consulta.FrecuenciaCardiaca);
                                    if (consulta.FrecuenciaRespiratoria.HasValue) vitales += string.Format("FR: {0} rpm", consulta.FrecuenciaRespiratoria);
                                    AgregarCeldaConsulta(tableConsulta, "💓 Signos Vitales:", vitales.TrimEnd('|', ' '), fontLabel, fontNormal, colorBorde);
                                }

                                if (!string.IsNullOrWhiteSpace(consulta.Diagnostico))
                                    AgregarCeldaConsulta(tableConsulta, "🔍 Diagnóstico:", consulta.Diagnostico, fontLabel, fontNormal, colorBorde);

                                if (consulta.EnfermedadId.HasValue && consulta.Enfermedad != null)
                                    AgregarCeldaConsulta(tableConsulta, "🦠 Enfermedad:", consulta.Enfermedad.Nombre, fontLabel, fontNormal, colorBorde);

                                if (!string.IsNullOrWhiteSpace(consulta.Tratamiento))
                                    AgregarCeldaConsulta(tableConsulta, "💊 Tratamiento:", consulta.Tratamiento, fontLabel, fontNormal, colorBorde);

                                if (!string.IsNullOrWhiteSpace(consulta.IndicacionesMedicas))
                                    AgregarCeldaConsulta(tableConsulta, "📋 Indicaciones:", consulta.IndicacionesMedicas, fontLabel, fontNormal, colorBorde);

                                AgregarCeldaConsulta(tableConsulta, "💰 Costo:", string.Format("L {0:N2} ({1})", consulta.CostoConsulta, consulta.EstadoPago), fontLabel, fontNormal, colorBorde);

                                document.Add(tableConsulta);

                                // Agregar medicamentos prescritos (solo pagados: estado Surtida)
                                try
                                {
                                    using (var nRec = new NReceta())
                                    {
                                        var recetasConsulta = nRec.BuscarPorConsulta(consulta.ConsultaId);
                                        if (recetasConsulta != null && recetasConsulta.Count > 0)
                                        {
                                            var recetasPagadas = recetasConsulta.Where(r => r.Estado == "Surtida").ToList();
                                            if (recetasPagadas.Count > 0)
                                            {
                                                var tableMedicamentos = new iTextSharp.text.pdf.PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 5, SpacingAfter = 5 };
                                                var headerMed = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("💊 Medicamentos Prescritos (Pagados)", fontLabel));
                                                headerMed.BackgroundColor = new iTextSharp.text.BaseColor(220, 255, 220);
                                                headerMed.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                                headerMed.Padding = 5;
                                                tableMedicamentos.AddCell(headerMed);
                                                document.Add(tableMedicamentos);

                                                foreach (var receta in recetasPagadas)
                                                {
                                                    var detalles = nRec.ObtenerDetallesReceta(receta.RecetaId);
                                                    if (detalles != null && detalles.Count > 0)
                                                    {
                                                        var tableMed = new iTextSharp.text.pdf.PdfPTable(4) { WidthPercentage = 100, SpacingAfter = 3 };
                                                        tableMed.SetWidths(new float[] { 2f, 1.2f, 0.8f, 2f });

                                                        // Encabezados
                                                        var cellHeader1 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Medicamento", fontLabel));
                                                        cellHeader1.BackgroundColor = new iTextSharp.text.BaseColor(245, 245, 245);
                                                        cellHeader1.Padding = 3;
                                                        tableMed.AddCell(cellHeader1);

                                                        var cellHeader2 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Dosis", fontLabel));
                                                        cellHeader2.BackgroundColor = new iTextSharp.text.BaseColor(245, 245, 245);
                                                        cellHeader2.Padding = 3;
                                                        tableMed.AddCell(cellHeader2);

                                                        var cellHeader3 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Cant.", fontLabel));
                                                        cellHeader3.BackgroundColor = new iTextSharp.text.BaseColor(245, 245, 245);
                                                        cellHeader3.Padding = 3;
                                                        tableMed.AddCell(cellHeader3);

                                                        var cellHeader4 = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Indicaciones", fontLabel));
                                                        cellHeader4.BackgroundColor = new iTextSharp.text.BaseColor(245, 245, 245);
                                                        cellHeader4.Padding = 3;
                                                        tableMed.AddCell(cellHeader4);

                                                        // Detalles
                                                        foreach (var det in detalles)
                                                        {
                                                            tableMed.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(det.Medicamento?.Nombre ?? "N/A", fontNormal)) { Padding = 3, Border = iTextSharp.text.Rectangle.NO_BORDER });
                                                            tableMed.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(det.Dosis ?? "N/A", fontNormal)) { Padding = 3, Border = iTextSharp.text.Rectangle.NO_BORDER });
                                                            tableMed.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(string.Format("{0}/{1}", det.CantidadSurtida, det.CantidadPrescrita), fontNormal)) { Padding = 3, Border = iTextSharp.text.Rectangle.NO_BORDER });
                                                            tableMed.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(det.Indicaciones ?? "-", fontNormal)) { Padding = 3, Border = iTextSharp.text.Rectangle.NO_BORDER });
                                                        }

                                                        document.Add(tableMed);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception exMed)
                                {
                                    System.Diagnostics.Debug.WriteLine(string.Format("Error al cargar medicamentos: {0}", exMed.Message));
                                }
                            }
                        }
                        else
                        {
                            document.Add(new iTextSharp.text.Paragraph("⚠️ No hay consultas finalizadas registradas.", fontNormal) { SpacingBefore = 10, SpacingAfter = 10, Alignment = iTextSharp.text.Element.ALIGN_CENTER });
                        }
                    }
                    else
                    {
                        document.Add(new iTextSharp.text.Paragraph("⚠️ No hay consultas registradas para este paciente.", fontNormal) { SpacingBefore = 10, SpacingAfter = 10, Alignment = iTextSharp.text.Element.ALIGN_CENTER });
                    }

                    // Exámenes - Diseño mejorado
                    try
                    {
                        using (var nExamen = new NExamen())
                        {
                            var examenes = nExamen.BuscarPorExpedienteId(expedienteActual.PacienteId)
                                .Where(ex => ex.Estado != "Cancelado")
                                .ToList();
                            if (examenes != null && examenes.Count > 0)
                            {
                                // Header de sección con fondo naranja
                                var headerExamenes = new iTextSharp.text.pdf.PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 15, SpacingAfter = 10 };
                                var cellHeaderExamenes = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(string.Format("🔬 EXÁMENES MÉDICOS ({0})", examenes.Count), fontSectionTitle));
                                cellHeaderExamenes.BackgroundColor = new iTextSharp.text.BaseColor(255, 152, 0); // Naranja
                                cellHeaderExamenes.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                cellHeaderExamenes.Padding = 8;
                                cellHeaderExamenes.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                headerExamenes.AddCell(cellHeaderExamenes);
                                document.Add(headerExamenes);

                                foreach (var examen in examenes.OrderBy(ex => ex.FechaSolicitud))
                                {
                                    // Tarjeta de examen con fondo amarillo claro
                                    var tableExamen = new iTextSharp.text.pdf.PdfPTable(2) { WidthPercentage = 100, SpacingAfter = 10 };
                                    tableExamen.SetWidths(new float[] { 1, 2 });

                                    // Header del examen
                                    var examenHeaderCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(string.Format("🧪 {0}", examen.Nombre), fontLabel));
                                    examenHeaderCell.Colspan = 2;
                                    examenHeaderCell.BackgroundColor = new iTextSharp.text.BaseColor(255, 243, 224); // Amarillo claro
                                    examenHeaderCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                    examenHeaderCell.Padding = 6;
                                    tableExamen.AddCell(examenHeaderCell);

                                    // Detalles del examen
                                    AgregarCeldaConsulta(tableExamen, "📋 Tipo:", examen.Tipo, fontLabel, fontNormal, colorBorde);
                                    AgregarCeldaConsulta(tableExamen, "📅 Solicitud:", examen.FechaSolicitud.ToString("dd/MM/yyyy"), fontLabel, fontNormal, colorBorde);

                                    if (examen.FechaResultado.HasValue)
                                    {
                                        AgregarCeldaConsulta(tableExamen, "✅ Fecha de Resultado:", examen.FechaResultado.Value.ToString("dd/MM/yyyy"), fontLabel, fontNormal, colorBorde);
                                    }

                                    if (!string.IsNullOrWhiteSpace(examen.Resultado))
                                    {
                                        AgregarCeldaConsulta(tableExamen, "📊 Resultado:", examen.Resultado, fontLabel, fontNormal, colorBorde);
                                    }

                                    string estadoIcon = examen.Estado == "Completado" ? "✅" : "⏳";
                                    AgregarCeldaConsulta(tableExamen, "🔄 Estado:", string.Format("{0} {1}", estadoIcon, examen.Estado), fontLabel, fontNormal, colorBorde);
                                    AgregarCeldaConsulta(tableExamen, "💰 Costo:", string.Format("L {0:N2}", examen.Costo), fontLabel, fontNormal, colorBorde);

                                    document.Add(tableExamen);
                                }
                            }
                        }
                    }
                    catch
                    {
                        // Si falla cargar exámenes, continuar sin ellos
                    }

                    // Pie de página mejorado
                    var footerTable = new iTextSharp.text.pdf.PdfPTable(1) { WidthPercentage = 100, SpacingBefore = 25 };
                    var footerCell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(
                        string.Format("📄 Documento generado el {0:dd/MM/yyyy} a las {1:HH:mm}\n🏥 Sistema MediNova - Expediente Médico Electrónico", DateTime.Now, DateTime.Now),
                        iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.ITALIC, colorSecundario)));
                    footerCell.Border = iTextSharp.text.Rectangle.TOP_BORDER;
                    footerCell.BorderColor = colorSecundario;
                    footerCell.BorderWidth = 1;
                    footerCell.Padding = 10;
                    footerCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    footerTable.AddCell(footerCell);
                    document.Add(footerTable);

                    document.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al generar PDF: {0}", ex.Message), ex);
            }
        }

        // Método para agregar celdas en la tabla de información del paciente (4 columnas)
        private void AgregarCeldaBonita(iTextSharp.text.pdf.PdfPTable tabla, string label, string valor,
            iTextSharp.text.Font fontLabel, iTextSharp.text.Font fontNormal,
            iTextSharp.text.BaseColor backgroundColor, int colspan = 1)
        {
            // Celda de etiqueta
            var celdaLabel = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(label, fontLabel));
            celdaLabel.BackgroundColor = backgroundColor;
            celdaLabel.Border = iTextSharp.text.Rectangle.NO_BORDER;
            celdaLabel.Padding = 5;
            if (colspan > 1 && colspan % 2 == 0)
            {
                // Si el colspan es par, la celda de etiqueta ocupa la mitad
                celdaLabel.Colspan = colspan / 2;
            }
            tabla.AddCell(celdaLabel);

            // Celda de valor
            var celdaValor = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(valor ?? "N/A", fontNormal));
            celdaValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
            celdaValor.Padding = 5;
            if (colspan > 1)
            {
                // La celda de valor ocupa el resto del colspan
                celdaValor.Colspan = colspan - (colspan > 1 && colspan % 2 == 0 ? colspan / 2 : 1);
            }
            tabla.AddCell(celdaValor);
        }

        // Método para agregar filas en las tablas de consultas y exámenes (2 columnas)
        private void AgregarCeldaConsulta(iTextSharp.text.pdf.PdfPTable tabla, string label, string valor,
            iTextSharp.text.Font fontLabel, iTextSharp.text.Font fontNormal,
            iTextSharp.text.BaseColor borderColor)
        {
            // Celda de etiqueta con icono
            var celdaLabel = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(label, fontLabel));
            celdaLabel.BackgroundColor = new iTextSharp.text.BaseColor(245, 245, 245);
            celdaLabel.Border = iTextSharp.text.Rectangle.NO_BORDER;
            celdaLabel.Padding = 5;
            tabla.AddCell(celdaLabel);

            // Celda de valor
            var celdaValor = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(valor ?? "N/A", fontNormal));
            celdaValor.Border = iTextSharp.text.Rectangle.NO_BORDER;
            celdaValor.Padding = 5;
            tabla.AddCell(celdaValor);
        }

        private string GenerarHTMLExpediente()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8'>");
            sb.AppendLine("<title>Expediente Médico</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
            sb.AppendLine("h1 { color: #2c3e50; border-bottom: 3px solid #3498db; padding-bottom: 10px; }");
            sb.AppendLine("h2 { color: #34495e; margin-top: 30px; border-bottom: 2px solid #95a5a6; padding-bottom: 5px; }");
            sb.AppendLine("h3 { color: #7f8c8d; margin-top: 20px; }");
            sb.AppendLine(".paciente-info { background: #ecf0f1; padding: 15px; border-radius: 5px; margin: 20px 0; }");
            sb.AppendLine(".consulta { border: 1px solid #bdc3c7; padding: 15px; margin: 15px 0; border-radius: 5px; page-break-inside: avoid; }");
            sb.AppendLine(".examen { background: #e8f5e9; padding: 10px; margin: 10px 0; border-left: 4px solid #4caf50; }");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; margin: 10px 0; }");
            sb.AppendLine("th, td { padding: 8px; text-align: left; border-bottom: 1px solid #ddd; }");
            sb.AppendLine("th { background-color: #3498db; color: white; }");
            sb.AppendLine(".label { font-weight: bold; color: #2c3e50; }");
            sb.AppendLine(".value { color: #34495e; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            // Encabezado del expediente
            sb.AppendLine("<h1>📋 EXPEDIENTE MÉDICO</h1>");

            // Información del paciente
            sb.AppendLine("<div class='paciente-info'>");
            sb.AppendLine("<h2>Información del Paciente</h2>");
            sb.AppendLine(string.Format("<p><span class='label'>Nombre:</span> <span class='value'>{0}</span></p>", pacienteSeleccionado.NombreCompleto));
            sb.AppendLine(string.Format("<p><span class='label'>DNI:</span> <span class='value'>{0}</span></p>", pacienteSeleccionado.DNI));
            sb.AppendLine(string.Format("<p><span class='label'>Fecha de Nacimiento:</span> <span class='value'>{0:dd/MM/yyyy}</span> ", pacienteSeleccionado.FechaNacimiento));
            sb.AppendLine(string.Format("<span class='label'>Edad:</span> <span class='value'>{0} años</span></p>", CalcularEdad(pacienteSeleccionado.FechaNacimiento)));
            sb.AppendLine(string.Format("<p><span class='label'>Sexo:</span> <span class='value'>{0}</span></p>", pacienteSeleccionado.Sexo));
            sb.AppendLine(string.Format("<p><span class='label'>Teléfono:</span> <span class='value'>{0}</span></p>", pacienteSeleccionado.Telefono));
            sb.AppendLine(string.Format("<p><span class='label'>Dirección:</span> <span class='value'>{0}</span></p>", pacienteSeleccionado.Direccion));
            sb.AppendLine(string.Format("<p><span class='label'>N° Expediente:</span> <span class='value'>{0}</span></p>", expedienteActual.NumeroExpediente));
            sb.AppendLine(string.Format("<p><span class='label'>Tipo de Sangre:</span> <span class='value'>{0}</span></p>", expedienteActual.TipoSangre ?? "No especificado"));
            sb.AppendLine(string.Format("<p><span class='label'>Alergias:</span> <span class='value'>{0}</span></p>", expedienteActual.Alergias ?? "Ninguna"));
            sb.AppendLine(string.Format("<p><span class='label'>Contacto de Emergencia:</span> <span class='value'>{0} - {1}</span></p>", expedienteActual.ContactoEmergencia, expedienteActual.TelefonoEmergencia));
            sb.AppendLine("</div>");

            // Consultas
            if (consultasDelPaciente != null && consultasDelPaciente.Count > 0)
            {
                sb.AppendLine("<h2>Historial de Consultas</h2>");

                var consultasParaMostrar = mostrarSoloFinalizadas
                    ? consultasDelPaciente.Where(c => c.Estado == "Finalizada").ToList()
                    : consultasDelPaciente.ToList();

                foreach (var consulta in consultasParaMostrar.OrderBy(c => c.FechaConsulta))
                {
                    sb.AppendLine("<div class='consulta'>");
                    sb.AppendLine(string.Format("<h3>Consulta {0} - {1:dd/MM/yyyy HH:mm}</h3>", consulta.NumeroConsulta, consulta.FechaConsulta));
                    sb.AppendLine(string.Format("<p><span class='label'>Médico:</span> <span class='value'>{0}</span></p>", consulta.Doctor != null ? consulta.Doctor.NombreCompleto ?? "No asignado" : "No asignado"));
                    sb.AppendLine(string.Format("<p><span class='label'>Motivo:</span> <span class='value'>{0}</span></p>", consulta.MotivoConsulta));

                    if (!string.IsNullOrWhiteSpace(consulta.Sintomas))
                        sb.AppendLine(string.Format("<p><span class='label'>Síntomas:</span> <span class='value'>{0}</span></p>", consulta.Sintomas));

                    // Signos vitales
                    if (!string.IsNullOrWhiteSpace(consulta.PresionArterial) || consulta.Temperatura.HasValue)
                    {
                        sb.AppendLine("<p><span class='label'>Signos Vitales:</span></p>");
                        sb.AppendLine("<table>");
                        sb.AppendLine("<tr><th>Presión Arterial</th><th>Temperatura</th><th>Frecuencia Cardíaca</th><th>Peso</th><th>Altura</th></tr>");
                        sb.AppendLine("<tr>");
                        sb.AppendLine(string.Format("<td>{0}</td>", consulta.PresionArterial ?? "N/A"));
                        sb.AppendLine(string.Format("<td>{0}</td>", consulta.Temperatura.HasValue ? consulta.Temperatura.Value.ToString("F1") + "°C" : "N/A"));
                        sb.AppendLine(string.Format("<td>{0}</td>", consulta.FrecuenciaCardiaca.HasValue ? consulta.FrecuenciaCardiaca.Value + " lpm" : "N/A"));
                        sb.AppendLine(string.Format("<td>{0}</td>", consulta.Peso.HasValue ? consulta.Peso.Value + " kg" : "N/A"));
                        sb.AppendLine(string.Format("<td>{0}</td>", consulta.Altura.HasValue ? consulta.Altura.Value + " cm" : "N/A"));
                        sb.AppendLine("</tr></table>");
                    }

                    if (!string.IsNullOrWhiteSpace(consulta.Diagnostico))
                        sb.AppendLine(string.Format("<p><span class='label'>Diagnóstico:</span> <span class='value'>{0}</span></p>", consulta.Diagnostico));

                    if (consulta.EnfermedadId.HasValue && consulta.Enfermedad != null)
                        sb.AppendLine(string.Format("<p><span class='label'>Enfermedad:</span> <span class='value'>{0}</span></p>", consulta.Enfermedad.Nombre));

                    if (!string.IsNullOrWhiteSpace(consulta.Tratamiento))
                        sb.AppendLine(string.Format("<p><span class='label'>Tratamiento:</span> <span class='value'>{0}</span></p>", consulta.Tratamiento));

                    sb.AppendLine(string.Format("<p><span class='label'>Estado:</span> <span class='value'>{0}</span></p>", consulta.Estado));
                    sb.AppendLine(string.Format("<p><span class='label'>Costo:</span> <span class='value'>{0:C2}</span> ", consulta.CostoConsulta));
                    sb.AppendLine(string.Format("<span class='label'>Estado de Pago:</span> <span class='value'>{0}</span></p>", consulta.EstadoPago));
                    sb.AppendLine("</div>");
                }
            }
            else
            {
                sb.AppendLine("<p>No hay consultas registradas.</p>");
            }

            // Exámenes del expediente
            try
            {
                using (var nExamen = new NExamen())
                {
                    var examenes = nExamen.BuscarPorExpedienteId(expedienteActual.PacienteId)
                        .Where(ex => ex.Estado != "Cancelado")
                        .ToList();
                    if (examenes != null && examenes.Count > 0)
                    {
                        sb.AppendLine("<h2>Exámenes Médicos</h2>");
                        foreach (var examen in examenes.OrderBy(ex => ex.FechaSolicitud))
                        {
                            sb.AppendLine("<div class='examen'>");
                            sb.AppendLine(string.Format("<p><span class='label'>Nombre:</span> <span class='value'>{0}</span></p>", examen.Nombre));
                            sb.AppendLine(string.Format("<p><span class='label'>Tipo:</span> <span class='value'>{0}</span></p>", examen.Tipo));
                            sb.AppendLine(string.Format("<p><span class='label'>Fecha Solicitud:</span> <span class='value'>{0:dd/MM/yyyy}</span></p>", examen.FechaSolicitud));
                            if (examen.FechaResultado.HasValue)
                                sb.AppendLine(string.Format("<p><span class='label'>Fecha Resultado:</span> <span class='value'>{0:dd/MM/yyyy}</span></p>", examen.FechaResultado.Value));
                            if (!string.IsNullOrWhiteSpace(examen.Resultado))
                                sb.AppendLine(string.Format("<p><span class='label'>Resultado:</span> <span class='value'>{0}</span></p>", examen.Resultado));
                            sb.AppendLine(string.Format("<p><span class='label'>Estado:</span> <span class='value'>{0}</span></p>", examen.Estado));
                            sb.AppendLine(string.Format("<p><span class='label'>Costo:</span> <span class='value'>{0:C2}</span></p>", examen.Costo));
                            sb.AppendLine("</div>");
                        }
                    }
                }
            }
            catch
            {
                // Si falla cargar exámenes, continuar sin ellos
            }

            sb.AppendLine("<p style='margin-top: 40px; text-align: center; color: #7f8c8d;'>");
            sb.AppendLine(string.Format("Generado el {0:dd/MM/yyyy HH:mm} - Sistema MediNova", DateTime.Now));
            sb.AppendLine("</p>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
