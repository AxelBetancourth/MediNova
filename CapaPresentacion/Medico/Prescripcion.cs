using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaNegocio.Farmacia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class Prescripcion : Form
    {
        private TConsulta consulta;
        private int consultaId;
        private BindingList<DetalleRecetaTemporal> detallesReceta;
        private NReceta nReceta;
        private NMedicamento nMedicamento;
        private Form formularioOrigen;
        private bool modoEdicion = false;
        private int indiceEdicion = -1;
        private List<TReceta> recetasConsulta;
        private Guna.UI2.WinForms.Guna2Button btnHistorialRecetas;
        private Guna.UI2.WinForms.Guna2Button btnEliminarReceta;
        private Guna.UI2.WinForms.Guna2CheckBox chkMedicamentoExterno;
        private Guna.UI2.WinForms.Guna2TextBox txtNombreMedicamentoExterno;
        private int recetaIdEnEdicion = 0; // 0 = nueva receta, > 0 = editando receta existente

        public Prescripcion(int consultaId)
        {
            InitializeComponent();
            this.consultaId = consultaId;
            detallesReceta = new BindingList<DetalleRecetaTemporal>();
            recetasConsulta = new List<TReceta>();
            nReceta = new NReceta();
            nMedicamento = new NMedicamento();
            ConfigurarFormulario();
            CargarDatosConsulta();
            CargarRecetasConsulta();
        }

        public void EstablecerFormularioOrigen(Form formulario)
        {
            formularioOrigen = formulario;
        }

        private void ConfigurarFormulario()
        {
            // Configurar DataGridView de medicamentos
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.Columns.Clear();

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Medicamento",
                HeaderText = "Medicamento",
                DataPropertyName = "NombreMedicamento",
                Width = 200,
                ReadOnly = true
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Dosis",
                HeaderText = "Dosis",
                DataPropertyName = "Dosis",
                Width = 150,
                ReadOnly = true
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "CantidadPrescrita",
                Width = 80,
                ReadOnly = true
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Duracion",
                HeaderText = "Duración (días)",
                DataPropertyName = "DuracionDias",
                Width = 100,
                ReadOnly = true
            });

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Indicaciones",
                HeaderText = "Indicaciones",
                DataPropertyName = "Indicaciones",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            var btnEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Acciones",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvDetalles.Columns.Add(btnEliminar);
            dgvDetalles.CellClick += DgvDetalles_CellClick;

            // Configuraciones adicionales para evitar errores con grid vacío
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.AllowUserToResizeRows = false;
            dgvDetalles.RowHeadersVisible = false;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.MultiSelect = false;
            dgvDetalles.ReadOnly = true;

            // Agregar evento MouseDown para prevenir clicks en áreas vacías
            dgvDetalles.MouseDown += DgvDetalles_MouseDown;

            // Agregar evento DataError para capturar errores silenciosamente
            dgvDetalles.DataError += DgvDetalles_DataError;

            // Agregar evento KeyDown para eliminar con tecla Supr
            dgvDetalles.KeyDown += DgvDetalles_KeyDown;

            // Establecer el DataSource inicial con la lista vacía
            dgvDetalles.DataSource = detallesReceta;

            // Configurar fecha de vencimiento (30 días por defecto)
            dtpFechaVencimiento.Value = DateTime.Now.AddDays(30);

            // Agregar checkbox para medicamento externo (no en inventario)
            var panelMedicamentos = this.Controls.Find("panelMedicamentos", true).FirstOrDefault() as Guna.UI2.WinForms.Guna2Panel;
            if (panelMedicamentos != null)
            {
                // Crear checkbox - ubicado debajo de los botones de búsqueda
                chkMedicamentoExterno = new Guna.UI2.WinForms.Guna2CheckBox
                {
                    Name = "chkMedicamentoExterno",
                    Text = "✓ Medicamento externo (no en inventario)",
                    Location = new System.Drawing.Point(500, 20),
                    Size = new System.Drawing.Size(320, 25),
                    CheckedState = { BorderRadius = 2, BorderThickness = 2 },
                    Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.FromArgb(255, 152, 0)
                };
                chkMedicamentoExterno.CheckedChanged += ChkMedicamentoExterno_CheckedChanged;
                panelMedicamentos.Controls.Add(chkMedicamentoExterno);
                chkMedicamentoExterno.BringToFront();

                // Crear textbox para nombre de medicamento externo (inicialmente oculto)
                txtNombreMedicamentoExterno = new Guna.UI2.WinForms.Guna2TextBox
                {
                    Name = "txtNombreMedicamentoExterno",
                    PlaceholderText = "Ingrese el nombre del medicamento externo",
                    Location = new System.Drawing.Point(20, 80),
                    Size = new System.Drawing.Size(550, 36),
                    BorderRadius = 6,
                    BorderColor = System.Drawing.Color.FromArgb(255, 152, 0),
                    BorderThickness = 2,
                    Visible = false
                };
                panelMedicamentos.Controls.Add(txtNombreMedicamentoExterno);
                txtNombreMedicamentoExterno.BringToFront();
            }
        }

        private void CargarDatosConsulta()
        {
            try
            {
                using (var nConsulta = new CapaNegocio.Medico.NConsulta())
                {
                    consulta = nConsulta.BuscarPorId(consultaId);

                    if (consulta == null)
                    {
                        MessageBox.Show("No se encontró la consulta.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // Mostrar información del paciente
                    using (var nExp = new CapaNegocio.Medico.NExpediente())
                    {
                        var expediente = nExp.BuscarPorId(consulta.ExpedienteId);
                        if (expediente != null && expediente.Paciente != null)
                        {
                            lblPaciente.Text = string.Format("Paciente: {0}", expediente.Paciente.NombreCompleto);
                            lblDNI.Text = string.Format("DNI: {0}", expediente.Paciente.DNI);
                        }
                    }

                    lblNumeroConsulta.Text = string.Format("Consulta: {0}", consulta.NumeroConsulta);
                    lblFechaConsulta.Text = string.Format("Fecha: {0:dd/MM/yyyy HH:mm}", consulta.FechaConsulta);

                    // Pre-llenar diagnóstico si existe
                    if (!string.IsNullOrWhiteSpace(consulta.Diagnostico))
                    {
                        txtDiagnostico.Text = consulta.Diagnostico;
                    }

                    // Verificar si la consulta está finalizada
                    if (consulta.Estado == "Finalizada")
                    {
                        // Mostrar mensaje de solo lectura
                        MessageBox.Show("Esta consulta está finalizada.\n\n" +
                            "Solo puede ver el historial de recetas.\n" +
                            "No se pueden agregar nuevas recetas.",
                            "Consulta Finalizada - Solo Lectura",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Deshabilitar controles de edición
                        txtBuscarMedicamento.Enabled = false;
                        btnBuscarMedicamento.Enabled = false;
                        txtDosis.Enabled = false;
                        numCantidad.Enabled = false;
                        numDuracion.Enabled = false;
                        txtIndicaciones.Enabled = false;
                        txtDiagnostico.Enabled = false;
                        txtIndicacionesGenerales.Enabled = false;
                        dtpFechaVencimiento.Enabled = false;
                        btnAgregarDetalle.Enabled = false;
                        btnGuardar.Enabled = false;

                        if (chkMedicamentoExterno != null)
                            chkMedicamentoExterno.Enabled = false;
                        if (txtNombreMedicamentoExterno != null)
                            txtNombreMedicamentoExterno.Enabled = false;

                        // El botón de historial permanece habilitado para ver recetas existentes
                        // El botón de cancelar permanece habilitado para salir
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar datos de la consulta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRecetasConsulta()
        {
            try
            {
                // Cargar todas las recetas de la consulta para mostrar en el historial
                recetasConsulta = nReceta.BuscarPorConsulta(consultaId).ToList();

                // Crear botón de historial si no existe
                if (btnHistorialRecetas == null)
                {
                    btnHistorialRecetas = new Guna.UI2.WinForms.Guna2Button
                    {
                        Name = "btnHistorialRecetas",
                        Text = string.Format("📋 Historial ({0})", recetasConsulta.Count),
                        Size = new System.Drawing.Size(180, 40),
                        Location = new System.Drawing.Point(800, 52),
                        BorderRadius = 8,
                        FillColor = System.Drawing.Color.FromArgb(255, 152, 0),
                        Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold),
                        ForeColor = System.Drawing.Color.White
                    };
                    btnHistorialRecetas.Click += BtnHistorialRecetas_Click;

                    // Agregar al panelTop
                    var panelTop = this.Controls.Find("panelTop", true).FirstOrDefault();
                    if (panelTop != null)
                    {
                        panelTop.Controls.Add(btnHistorialRecetas);
                        btnHistorialRecetas.BringToFront();
                    }
                }
                else
                {
                    // Actualizar el texto con el contador
                    btnHistorialRecetas.Text = string.Format("📋 Historial ({0})", recetasConsulta.Count);
                }

                // Crear botón de eliminar receta si no existe
                if (btnEliminarReceta == null)
                {
                    btnEliminarReceta = new Guna.UI2.WinForms.Guna2Button
                    {
                        Name = "btnEliminarReceta",
                        Text = "🗑️ Eliminar Receta",
                        Size = new System.Drawing.Size(160, 40),
                        Location = new System.Drawing.Point(995, 52),
                        BorderRadius = 8,
                        FillColor = System.Drawing.Color.FromArgb(231, 76, 60),
                        Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold),
                        ForeColor = System.Drawing.Color.White,
                        Visible = false // Solo visible cuando se está editando
                    };
                    btnEliminarReceta.Click += BtnEliminarReceta_Click;

                    // Agregar al panelTop
                    var panelTop = this.Controls.Find("panelTop", true).FirstOrDefault();
                    if (panelTop != null)
                    {
                        panelTop.Controls.Add(btnEliminarReceta);
                        btnEliminarReceta.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar recetas de la consulta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnHistorialRecetas_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el PacienteId de la consulta
                int pacienteId = consulta.Expediente.PacienteId;

                // Abrir formulario de historial de recetas
                var formHistorial = new HistorialRecetas(pacienteId, consultaId);
                formHistorial.RecetaEditada += (s, args) =>
                {
                    // Recargar recetas cuando se edite una
                    CargarRecetasConsulta();
                };

                if (formHistorial.ShowDialog(this) == DialogResult.OK)
                {
                    // El usuario seleccionó una receta para editar
                    if (formHistorial.RecetaSeleccionada != null)
                    {
                        CargarRecetaParaEditar(formHistorial.RecetaSeleccionada);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir historial de recetas: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRecetaParaEditar(TReceta receta)
        {
            try
            {
                // Guardar el ID de la receta que estamos editando
                recetaIdEnEdicion = receta.RecetaId;

                // Limpiar los detalles actuales
                detallesReceta.Clear();

                // Cargar información de la receta
                txtDiagnostico.Text = receta.Diagnostico ?? "";
                txtIndicacionesGenerales.Text = receta.IndicacionesGenerales ?? "";

                if (receta.FechaVencimiento.HasValue)
                {
                    dtpFechaVencimiento.Value = receta.FechaVencimiento.Value;
                }

                // Cargar los detalles de medicamentos usando NReceta para obtener la receta completa
                using (var nRec = new NReceta())
                using (var nMed = new NMedicamento())
                {
                    var recetaCompleta = nRec.BuscarPorId(receta.RecetaId);

                    if (recetaCompleta != null && recetaCompleta.DetallesReceta != null)
                    {
                        foreach (var detalle in recetaCompleta.DetallesReceta.Where(d => !d.Eliminado))
                        {
                            string nombreMedicamento;
                            int medicamentoId;
                            bool esMedicamentoExterno;

                            // Verificar si es medicamento externo (MedicamentoId es null)
                            if (!detalle.MedicamentoId.HasValue)
                            {
                                // Medicamento externo
                                nombreMedicamento = detalle.NombreMedicamentoExterno ?? "Medicamento externo";
                                medicamentoId = 0;
                                esMedicamentoExterno = true;
                            }
                            else
                            {
                                // Medicamento del inventario
                                nombreMedicamento = detalle.Medicamento?.Nombre;
                                if (string.IsNullOrEmpty(nombreMedicamento))
                                {
                                    var medicamento = nMed.BuscarPorId(detalle.MedicamentoId.Value);
                                    nombreMedicamento = medicamento?.Nombre ?? "Medicamento no encontrado";
                                }
                                medicamentoId = detalle.MedicamentoId.Value;
                                esMedicamentoExterno = false;
                            }

                            detallesReceta.Add(new DetalleRecetaTemporal
                            {
                                MedicamentoId = medicamentoId,
                                NombreMedicamento = nombreMedicamento,
                                Dosis = detalle.Dosis,
                                CantidadPrescrita = detalle.CantidadPrescrita,
                                DuracionDias = detalle.DuracionDias,
                                Indicaciones = detalle.Indicaciones,
                                EsMedicamentoExterno = esMedicamentoExterno
                            });
                        }
                    }
                }

                ActualizarGridDetalles();

                // Mostrar botón de eliminar receta cuando se está editando
                if (btnEliminarReceta != null)
                {
                    btnEliminarReceta.Visible = true;
                }

                MessageBox.Show(string.Format("Receta {0} cargada para edición.\n\nPuede modificar los datos y guardar los cambios.\nTambién puede eliminar medicamentos seleccionándolos y presionando la tecla Supr.\nO puede eliminar toda la receta con el botón 'Eliminar Receta'.", receta.NumeroReceta),
                    "Receta Cargada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar receta para editar: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarMedicamento_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = txtBuscarMedicamento.Text.Trim();

                if (string.IsNullOrWhiteSpace(criterio))
                {
                    MessageBox.Show("Ingrese el nombre del medicamento a buscar.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var medicamentos = nMedicamento.BuscarPorNombre(criterio);

                if (medicamentos == null || medicamentos.Count == 0)
                {
                    MessageBox.Show("No se encontraron medicamentos con ese criterio.",
                        "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (medicamentos.Count == 1)
                {
                    SeleccionarMedicamento(medicamentos[0]);
                }
                else
                {
                    MostrarSeleccionMedicamento(medicamentos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar medicamento: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarSeleccionMedicamento(List<TMedicamento> medicamentos)
        {
            using (var formSeleccion = new Form())
            {
                formSeleccion.Text = "Seleccionar Medicamento";
                formSeleccion.Size = new System.Drawing.Size(700, 450);
                formSeleccion.StartPosition = FormStartPosition.CenterParent;

                var dgv = new DataGridView
                {
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(660, 330),
                    AutoGenerateColumns = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false
                };

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    HeaderText = "Nombre",
                    DataPropertyName = "Nombre",
                    Width = 250
                });
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Descripcion",
                    HeaderText = "Descripción",
                    DataPropertyName = "Descripcion",
                    Width = 300
                });
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Stock",
                    HeaderText = "Stock",
                    DataPropertyName = "Stock",
                    Width = 80
                });

                dgv.DataSource = medicamentos;

                var btnSeleccionar = new Button
                {
                    Text = "Seleccionar",
                    Location = new System.Drawing.Point(260, 365),
                    Size = new System.Drawing.Size(180, 45)
                };

                btnSeleccionar.Click += (s, ev) =>
                {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        var medicamento = dgv.SelectedRows[0].DataBoundItem as TMedicamento;
                        formSeleccion.Tag = medicamento;
                        formSeleccion.DialogResult = DialogResult.OK;
                        formSeleccion.Close();
                    }
                };

                dgv.CellDoubleClick += (s, ev) =>
                {
                    if (ev.RowIndex >= 0)
                    {
                        var medicamento = dgv.Rows[ev.RowIndex].DataBoundItem as TMedicamento;
                        formSeleccion.Tag = medicamento;
                        formSeleccion.DialogResult = DialogResult.OK;
                        formSeleccion.Close();
                    }
                };

                formSeleccion.Controls.Add(dgv);
                formSeleccion.Controls.Add(btnSeleccionar);

                if (formSeleccion.ShowDialog(this) == DialogResult.OK)
                {
                    var medicamentoSelec = formSeleccion.Tag as TMedicamento;
                    if (medicamentoSelec != null)
                    {
                        SeleccionarMedicamento(medicamentoSelec);
                    }
                }
            }
        }

        private void SeleccionarMedicamento(TMedicamento medicamento)
        {
            lblMedicamentoSeleccionado.Text = string.Format("Medicamento: {0}", medicamento.Nombre);
            lblMedicamentoSeleccionado.Tag = medicamento;
            txtBuscarMedicamento.Clear();
        }

        private void ChkMedicamentoExterno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMedicamentoExterno.Checked)
                {
                    // Medicamento externo - deshabilitar búsqueda y mostrar textbox manual
                    txtBuscarMedicamento.Enabled = false;
                    btnBuscarMedicamento.Enabled = false;
                    lblMedicamentoSeleccionado.Visible = false;
                    txtNombreMedicamentoExterno.Visible = true;
                    txtNombreMedicamentoExterno.Focus();

                    // Limpiar selección actual
                    lblMedicamentoSeleccionado.Text = "Medicamento:";
                    lblMedicamentoSeleccionado.Tag = null;
                }
                else
                {
                    // Medicamento del inventario - habilitar búsqueda y ocultar textbox manual
                    txtBuscarMedicamento.Enabled = true;
                    btnBuscarMedicamento.Enabled = true;
                    lblMedicamentoSeleccionado.Visible = true;
                    txtNombreMedicamentoExterno.Visible = false;
                    txtNombreMedicamentoExterno.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cambiar modo de medicamento: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar medicamento (del inventario o externo)
                bool esMedicamentoExterno = chkMedicamentoExterno != null && chkMedicamentoExterno.Checked;
                string nombreMedicamento = "";
                int medicamentoId = 0;

                if (esMedicamentoExterno)
                {
                    // Medicamento externo - validar nombre ingresado manualmente
                    if (string.IsNullOrWhiteSpace(txtNombreMedicamentoExterno.Text))
                    {
                        MessageBox.Show("Debe ingresar el nombre del medicamento.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNombreMedicamentoExterno.Focus();
                        return;
                    }
                    nombreMedicamento = txtNombreMedicamentoExterno.Text.Trim();
                    medicamentoId = 0; // 0 indica medicamento externo
                }
                else
                {
                    // Medicamento del inventario - validar selección
                    if (lblMedicamentoSeleccionado.Tag == null)
                    {
                        MessageBox.Show("Debe seleccionar un medicamento primero.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var medicamento = lblMedicamentoSeleccionado.Tag as TMedicamento;
                    nombreMedicamento = medicamento.Nombre;
                    medicamentoId = medicamento.MedicamentoId;
                }

                if (string.IsNullOrWhiteSpace(txtDosis.Text))
                {
                    MessageBox.Show("Debe ingresar la dosis.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDosis.Focus();
                    return;
                }

                if (numCantidad.Value <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a 0.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numCantidad.Focus();
                    return;
                }

                if (numDuracion.Value <= 0)
                {
                    MessageBox.Show("La duración debe ser mayor a 0 días.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numDuracion.Focus();
                    return;
                }

                if (modoEdicion)
                {
                    // MODO EDICIÓN: Actualizar medicamento existente
                    if (indiceEdicion >= 0 && indiceEdicion < detallesReceta.Count)
                    {
                        var detalleExistente = detallesReceta[indiceEdicion];

                        detalleExistente.Dosis = txtDosis.Text.Trim();
                        detalleExistente.CantidadPrescrita = (int)numCantidad.Value;
                        detalleExistente.DuracionDias = (int)numDuracion.Value;
                        detalleExistente.Indicaciones = txtIndicaciones.Text.Trim();

                        ActualizarGridDetalles();
                        LimpiarCamposDetalle();

                        MessageBox.Show("Medicamento actualizado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // MODO AGREGAR: Verificar que no esté duplicado (solo para medicamentos del inventario)
                    if (!esMedicamentoExterno && detallesReceta.Any(d => d.MedicamentoId == medicamentoId))
                    {
                        MessageBox.Show("Este medicamento ya está en la receta.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var detalle = new DetalleRecetaTemporal
                    {
                        MedicamentoId = medicamentoId,
                        NombreMedicamento = nombreMedicamento,
                        Dosis = txtDosis.Text.Trim(),
                        CantidadPrescrita = (int)numCantidad.Value,
                        DuracionDias = (int)numDuracion.Value,
                        Indicaciones = txtIndicaciones.Text.Trim(),
                        EsMedicamentoExterno = esMedicamentoExterno
                    };

                    detallesReceta.Add(detalle);
                    ActualizarGridDetalles();
                    LimpiarCamposDetalle();

                    MessageBox.Show("Medicamento agregado a la receta.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposDetalle()
        {
            lblMedicamentoSeleccionado.Text = "Ninguno seleccionado";
            lblMedicamentoSeleccionado.Tag = null;
            txtDosis.Clear();
            numCantidad.Value = 1;
            numDuracion.Value = 7;
            txtIndicaciones.Clear();
            txtBuscarMedicamento.Clear();

            // Resetear modo edición
            modoEdicion = false;
            indiceEdicion = -1;
            btnAgregarDetalle.Text = "Agregar";
            btnAgregarDetalle.FillColor = System.Drawing.Color.FromArgb(76, 175, 80); // Verde
            btnCancelarEdicion.Visible = false;
        }

        private void ActualizarGridDetalles()
        {
            try
            {
                // Refrescar el binding y forzar actualización visual
                dgvDetalles.SuspendLayout();
                ((BindingList<DetalleRecetaTemporal>)dgvDetalles.DataSource).ResetBindings();
                dgvDetalles.Refresh();
                dgvDetalles.ResumeLayout();

                // Limpiar selección si no hay datos
                if (detallesReceta.Count == 0)
                {
                    dgvDetalles.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al actualizar grid: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDetalles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Capturar errores del DataGridView silenciosamente
            e.ThrowException = false;
            e.Cancel = true;

            // Log para debugging
            System.Diagnostics.Debug.WriteLine(string.Format("DataError en DataGridView: {0}", e.Exception != null ? e.Exception.Message : ""));
        }

        private void DgvDetalles_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // Verificar si hay datos en el grid
                if (detallesReceta == null || detallesReceta.Count == 0)
                {
                    return;
                }

                // Obtener información del hit test
                var hitTest = dgvDetalles.HitTest(e.X, e.Y);

                // Si el click no es en una fila válida, cancelar
                if (hitTest.RowIndex < 0 || hitTest.RowIndex >= detallesReceta.Count)
                {
                    // Limpiar selección actual si existe
                    if (dgvDetalles.CurrentCell != null)
                    {
                        dgvDetalles.ClearSelection();
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                // Log error pero no mostrar al usuario para no interrumpir flujo
                System.Diagnostics.Debug.WriteLine(string.Format("Error en MouseDown: {0}", ex.Message));
            }
        }

        private void DgvDetalles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;

                // Verificar que hay datos
                if (detallesReceta == null || detallesReceta.Count == 0) return;
                if (e.RowIndex >= detallesReceta.Count) return;

                // Verificar que la columna existe
                var columnaEliminar = dgvDetalles.Columns["Eliminar"];
                if (columnaEliminar == null) return;

                var detalle = dgvDetalles.Rows[e.RowIndex].DataBoundItem as DetalleRecetaTemporal;
                if (detalle == null) return;

                // Si se hizo click en la columna Eliminar
                if (e.ColumnIndex == columnaEliminar.Index)
                {
                    var resultado = MessageBox.Show(
                        string.Format("¿Está seguro de eliminar '{0}' de la receta?", detalle.NombreMedicamento),
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        detallesReceta.RemoveAt(e.RowIndex);
                        LimpiarCamposDetalle();
                        ActualizarGridDetalles();
                        MessageBox.Show("Medicamento eliminado de la receta.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Click en cualquier otra columna = cargar para editar
                    CargarDetalleParaEdicion(e.RowIndex, detalle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDetalles_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Si se presiona la tecla Supr
                if (e.KeyCode == Keys.Delete)
                {
                    // Verificar que hay una fila seleccionada
                    if (dgvDetalles.CurrentRow == null) return;
                    if (dgvDetalles.CurrentRow.Index < 0) return;
                    if (dgvDetalles.CurrentRow.Index >= detallesReceta.Count) return;

                    var detalle = dgvDetalles.CurrentRow.DataBoundItem as DetalleRecetaTemporal;
                    if (detalle == null) return;

                    var resultado = MessageBox.Show(
                        string.Format("¿Está seguro de eliminar '{0}' de la receta?", detalle.NombreMedicamento),
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        detallesReceta.RemoveAt(dgvDetalles.CurrentRow.Index);
                        LimpiarCamposDetalle();
                        ActualizarGridDetalles();
                        MessageBox.Show("Medicamento eliminado de la receta.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    e.Handled = true; // Indicar que la tecla fue manejada
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar medicamento: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDetalleParaEdicion(int indice, DetalleRecetaTemporal detalle)
        {
            modoEdicion = true;
            indiceEdicion = indice;

            // Cargar datos en los campos
            lblMedicamentoSeleccionado.Text = string.Format("Medicamento: {0}", detalle.NombreMedicamento);
            lblMedicamentoSeleccionado.Tag = new TMedicamento
            {
                MedicamentoId = detalle.MedicamentoId,
                Nombre = detalle.NombreMedicamento
            };

            txtDosis.Text = detalle.Dosis;
            numCantidad.Value = detalle.CantidadPrescrita;
            numDuracion.Value = detalle.DuracionDias;
            txtIndicaciones.Text = detalle.Indicaciones ?? "";

            // Cambiar texto del botón
            btnAgregarDetalle.Text = "Actualizar";
            btnAgregarDetalle.FillColor = System.Drawing.Color.FromArgb(25, 118, 210); // Azul

            // Mostrar botón cancelar
            btnCancelarEdicion.Visible = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (detallesReceta.Count == 0)
                {
                    MessageBox.Show("Debe agregar al menos un medicamento a la receta.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDiagnostico.Text))
                {
                    MessageBox.Show("Debe ingresar el diagnóstico.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiagnostico.Focus();
                    return;
                }

                bool esEdicion = recetaIdEnEdicion > 0;

                if (esEdicion)
                {
                    // EDITAR receta existente
                    EditarRecetaExistente();
                }
                else
                {
                    // CREAR nueva receta
                    CrearNuevaReceta();

                    // Ocultar botón de eliminar después de crear nueva receta
                    if (btnEliminarReceta != null)
                    {
                        btnEliminarReceta.Visible = false;
                    }
                }

                MessageBox.Show(esEdicion ? "Receta actualizada exitosamente." : "Receta guardada exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Si hay un formulario de origen, regresar a él
                if (formularioOrigen != null)
                {
                    Panel panelMain = EncontrarPanelMain(this);
                    if (panelMain != null)
                    {
                        panelMain.Controls.Clear();
                        formularioOrigen.TopLevel = false;
                        formularioOrigen.FormBorderStyle = FormBorderStyle.None;
                        formularioOrigen.Dock = DockStyle.Fill;
                        panelMain.Controls.Add(formularioOrigen);
                        formularioOrigen.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar receta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            // Cancelar edición y volver a modo agregar
            LimpiarCamposDetalle();
        }

        private void BtnEliminarReceta_Click(object sender, EventArgs e)
        {
            try
            {
                if (recetaIdEnEdicion <= 0)
                {
                    MessageBox.Show("No hay ninguna receta cargada para eliminar.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmar eliminación
                var resultado = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta receta?\n\n" +
                    "Esta acción marcará la receta como cancelada.\n" +
                    "El número de receta podrá ser reutilizado.",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    // Eliminar la receta (soft delete)
                    int filasAfectadas = nReceta.CancelarReceta(recetaIdEnEdicion);

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Receta eliminada exitosamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpiar el formulario
                        recetaIdEnEdicion = 0;
                        detallesReceta.Clear();
                        txtDiagnostico.Clear();
                        txtIndicacionesGenerales.Clear();
                        dtpFechaVencimiento.Value = DateTime.Now.AddDays(30);
                        ActualizarGridDetalles();

                        // Ocultar botón de eliminar
                        if (btnEliminarReceta != null)
                        {
                            btnEliminarReceta.Visible = false;
                        }

                        // Recargar la lista de recetas
                        CargarRecetasConsulta();

                        // Si hay un formulario de origen, regresar a él
                        if (formularioOrigen != null)
                        {
                            Panel panelMain = EncontrarPanelMain(this);
                            if (panelMain != null)
                            {
                                panelMain.Controls.Remove(this);
                                formularioOrigen.Show();
                                panelMain.Controls.Add(formularioOrigen);
                                formularioOrigen.BringToFront();
                            }
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la receta.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar receta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearNuevaReceta()
        {
            // Crear receta nueva
            var receta = new TReceta
            {
                ConsultaId = consultaId,
                PacienteId = consulta.Expediente.PacienteId,
                DoctorId = consulta.DoctorId,
                CitaId = consulta.CitaId,
                FechaEmision = DateTime.Now,
                FechaVencimiento = dtpFechaVencimiento.Value,
                Diagnostico = txtDiagnostico.Text.Trim(),
                IndicacionesGenerales = txtIndicacionesGenerales.Text.Trim(),
                Estado = "Pendiente",
                Eliminado = false
            };

            // Convertir detalles temporales a detalles de receta
            var detalles = detallesReceta.Select(d => new TDetalleReceta
            {
                MedicamentoId = d.EsMedicamentoExterno ? (int?)null : d.MedicamentoId,
                NombreMedicamentoExterno = d.EsMedicamentoExterno ? d.NombreMedicamento : null,
                CantidadPrescrita = d.CantidadPrescrita,
                Dosis = d.Dosis,
                DuracionDias = d.DuracionDias,
                Indicaciones = d.Indicaciones,
                CantidadSurtida = 0,
                Eliminado = false
            }).ToList();

            // Guardar receta con detalles
            nReceta.GuardarReceta(receta, detalles);
        }

        private void EditarRecetaExistente()
        {
            using (var uow = new CapaDatos.Core.UnitOfWork())
            {
                uow.ComenzarTransaccion();
                try
                {
                    // Obtener la receta existente
                    var recetaExistente = uow.Repository<TReceta>().Consulta()
                        .Include(r => r.DetallesReceta)
                        .FirstOrDefault(r => r.RecetaId == recetaIdEnEdicion);

                    if (recetaExistente == null)
                    {
                        throw new Exception("No se encontró la receta a editar.");
                    }

                    // Actualizar datos de la receta
                    recetaExistente.FechaVencimiento = dtpFechaVencimiento.Value;
                    recetaExistente.Diagnostico = txtDiagnostico.Text.Trim();
                    recetaExistente.IndicacionesGenerales = txtIndicacionesGenerales.Text.Trim();

                    uow.Repository<TReceta>().Editar(recetaExistente);

                    // Eliminar todos los detalles existentes (soft delete)
                    foreach (var detalle in recetaExistente.DetallesReceta)
                    {
                        detalle.Eliminado = true;
                        uow.Repository<TDetalleReceta>().Editar(detalle);
                    }

                    // Agregar los nuevos detalles
                    foreach (var detalleTemporal in detallesReceta)
                    {
                        var nuevoDetalle = new TDetalleReceta
                        {
                            RecetaId = recetaIdEnEdicion,
                            MedicamentoId = detalleTemporal.EsMedicamentoExterno ? (int?)null : detalleTemporal.MedicamentoId,
                            NombreMedicamentoExterno = detalleTemporal.EsMedicamentoExterno ? detalleTemporal.NombreMedicamento : null,
                            CantidadPrescrita = detalleTemporal.CantidadPrescrita,
                            Dosis = detalleTemporal.Dosis,
                            DuracionDias = detalleTemporal.DuracionDias,
                            Indicaciones = detalleTemporal.Indicaciones,
                            CantidadSurtida = 0,
                            Eliminado = false
                        };
                        uow.Repository<TDetalleReceta>().Agregar(nuevoDetalle);
                    }

                    uow.Guardar();
                    uow.ConfirmarTransaccion();

                    // Resetear el ID de edición
                    recetaIdEnEdicion = 0;
                }
                catch
                {
                    uow.ReversarTransaccion();
                    throw;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro de cancelar?\n\nSe perderán todos los datos ingresados.",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Si hay un formulario de origen, regresar a él
                if (formularioOrigen != null)
                {
                    Panel panelMain = EncontrarPanelMain(this);
                    if (panelMain != null)
                    {
                        panelMain.Controls.Clear();
                        formularioOrigen.TopLevel = false;
                        formularioOrigen.FormBorderStyle = FormBorderStyle.None;
                        formularioOrigen.Dock = DockStyle.Fill;
                        panelMain.Controls.Add(formularioOrigen);
                        formularioOrigen.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        private Panel EncontrarPanelMain(Control control)
        {
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

    // Clase temporal para manejar detalles de receta en el grid
    public class DetalleRecetaTemporal
    {
        public int MedicamentoId { get; set; }  // 0 para medicamentos externos
        public string NombreMedicamento { get; set; }
        public string Dosis { get; set; }
        public int CantidadPrescrita { get; set; }
        public int DuracionDias { get; set; }
        public string Indicaciones { get; set; }
        public bool EsMedicamentoExterno { get; set; }  // true = no está en inventario, false = está en inventario
    }
}
