using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaNegocio.Compartido;
using CapaNegocio.Farmacia;
using CapaNegocio.Medico;
using CapaNegocio.Recepcionista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Farmacia
{
    public partial class Ventas : Form
    {
        private TPaciente pacienteActual;
        private List<ItemCarrito> carrito = new List<ItemCarrito>();
        private decimal subtotal = 0;
        private decimal descuento = 0;
        private decimal total = 0;

        public Ventas()
        {
            InitializeComponent();
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrids();
            LimpiarFormulario();
        }

        private void ConfigurarDataGrids()
        {
            // Configurar grid de recetas
            this.dgvRecetasPendientes.AutoGenerateColumns = false;
            this.dgvRecetasPendientes.Columns.Clear();
            this.dgvRecetasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "RecetaId", DataPropertyName = "RecetaId", Visible = false });
            this.dgvRecetasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroReceta", HeaderText = "N° Receta", DataPropertyName = "NumeroReceta", Width = 120 });
            this.dgvRecetasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaEmision", HeaderText = "Fecha", DataPropertyName = "FechaEmision", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            this.dgvRecetasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", DataPropertyName = "Estado", Width = 80 });

            // Agregar botón para marcar receta como no surtida
            var btnNoSurtida = new DataGridViewButtonColumn
            {
                Name = "btnNoSurtida",
                HeaderText = "Acción",
                Text = "No Surtir",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            this.dgvRecetasPendientes.Columns.Add(btnNoSurtida);

            // Agregar evento de clic para el botón
            this.dgvRecetasPendientes.CellContentClick += DgvRecetasPendientes_CellContentClick;

            // Configurar grid de consultas
            this.dgvConsultasPendientes.AutoGenerateColumns = false;
            this.dgvConsultasPendientes.Columns.Clear();
            this.dgvConsultasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "ConsultaId", DataPropertyName = "ConsultaId", Visible = false });
            this.dgvConsultasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroConsulta", HeaderText = "N° Consulta", DataPropertyName = "NumeroConsulta", Width = 120 });
            this.dgvConsultasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaConsulta", HeaderText = "Fecha", DataPropertyName = "FechaConsulta", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            this.dgvConsultasPendientes.Columns.Add(new DataGridViewTextBoxColumn { Name = "CostoConsulta", HeaderText = "Costo", DataPropertyName = "CostoConsulta", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" } });

            // Configurar grid de exámenes
            if (this.Controls.Find("dgvExamenesPendientes", true).Length > 0)
            {
                var dgvExamenes = this.Controls.Find("dgvExamenesPendientes", true)[0] as DataGridView;
                if (dgvExamenes != null)
                {
                    dgvExamenes.AutoGenerateColumns = false;
                    dgvExamenes.Columns.Clear();
                    dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "ExamenId", DataPropertyName = "ExamenId", Visible = false });
                    dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Examen", DataPropertyName = "Nombre", Width = 200 });
                    dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", DataPropertyName = "Estado", Width = 80 });
                    dgvExamenes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Costo", HeaderText = "Costo", DataPropertyName = "Costo", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" } });
                }
            }

            // Configurar grid del carrito
            this.dgvCarrito.AutoGenerateColumns = false;
            this.dgvCarrito.Columns.Clear();
            this.dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Tipo", DataPropertyName = "Tipo", Width = 100 });
            this.dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { Name = "Descripcion", HeaderText = "Descripción", DataPropertyName = "Descripcion", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cant.", DataPropertyName = "Cantidad", Width = 60 });
            this.dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecioUnitario", HeaderText = "Precio Unit.", DataPropertyName = "PrecioUnitario", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" } });
            this.dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" } });
        }

        private void BtnBuscarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = txtBuscarPaciente.Text.Trim();

                if (string.IsNullOrWhiteSpace(criterio))
                {
                    MessageBox.Show("Ingrese DNI o nombre del paciente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var nPaciente = new NPacientes())
                {
                    var pacientes = nPaciente.BuscarPacientes(criterio);

                    if (pacientes == null || pacientes.Count == 0)
                    {
                        MessageBox.Show("No se encontró ningún paciente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (pacientes.Count == 1)
                    {
                        pacienteActual = pacientes[0];
                        CargarDatosPaciente();
                    }
                    else
                    {
                        // Mostrar selección de múltiples pacientes
                        MostrarSeleccionPaciente(pacientes);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar paciente: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarSeleccionPaciente(List<TPaciente> pacientes)
        {
            using (var formSeleccion = new Form())
            {
                formSeleccion.Text = "Seleccionar Paciente";
                formSeleccion.Size = new System.Drawing.Size(600, 400);
                formSeleccion.StartPosition = FormStartPosition.CenterParent;

                var dgv = new DataGridView
                {
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(560, 280),
                    AutoGenerateColumns = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false
                };

                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNI", HeaderText = "DNI", DataPropertyName = "DNI", Width = 120 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreCompleto", HeaderText = "Nombre Completo", DataPropertyName = "NombreCompleto", Width = 300 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Telefono", HeaderText = "Teléfono", DataPropertyName = "Telefono", Width = 100 });
                dgv.DataSource = pacientes;

                var btnSeleccionar = new Button
                {
                    Text = "Seleccionar",
                    Location = new System.Drawing.Point(250, 320),
                    Size = new System.Drawing.Size(100, 35)
                };

                btnSeleccionar.Click += (s, ev) =>
                {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        pacienteActual = dgv.SelectedRows[0].DataBoundItem as TPaciente;
                        formSeleccion.DialogResult = DialogResult.OK;
                        formSeleccion.Close();
                    }
                };

                formSeleccion.Controls.Add(dgv);
                formSeleccion.Controls.Add(btnSeleccionar);

                if (formSeleccion.ShowDialog() == DialogResult.OK)
                {
                    CargarDatosPaciente();
                }
            }
        }

        private void CargarDatosPaciente()
        {
            try
            {
                if (pacienteActual == null) return;

                // Mostrar información del paciente
                lblNombrePaciente.Text = pacienteActual.NombreCompleto ?? "N/A";
                lblDNIPaciente.Text = string.Format("DNI: {0}", pacienteActual.DNI ?? "N/A");

                panelInfoPaciente.Visible = true;

                // Cargar recetas pendientes
                using (var nReceta = new NReceta())
                {
                    var recetas = nReceta.BuscarRecetasPendientesPorPaciente(pacienteActual.PacienteId);
                    dgvRecetasPendientes.DataSource = recetas;
                }

                // Cargar consultas pendientes de pago
                using (var nConsulta = new NConsulta())
                {
                    var consultas = nConsulta.ObtenerConsultasPendientesPagoPorPaciente(pacienteActual.PacienteId);
                    dgvConsultasPendientes.DataSource = consultas;
                }

                // Cargar exámenes pendientes de pago (solo internos, los externos no se cobran en MediNova)
                var dgvExamenes = this.Controls.Find("dgvExamenesPendientes", true).FirstOrDefault() as DataGridView;
                if (dgvExamenes != null)
                {
                    using (var nExamen = new NExamen())
                    {
                        var examenes = nExamen.BuscarPorExpedienteId(pacienteActual.PacienteId)
                            .Where(e => !e.EsExterno && e.Estado != "Cancelado" && e.Estado != "Pagado" && !e.Eliminado)
                            .ToList();
                        dgvExamenes.DataSource = examenes;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar datos del paciente: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvRecetasPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar si se hizo clic en el botón "No Surtir"
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                var dgv = sender as DataGridView;
                if (dgv == null) return;

                // Verificar si es la columna del botón
                if (dgv.Columns[e.ColumnIndex].Name == "btnNoSurtida")
                {
                    var receta = dgv.Rows[e.RowIndex].DataBoundItem as TReceta;
                    if (receta == null) return;

                    // Preguntar confirmación
                    var resultado = MessageBox.Show(
                        string.Format("¿Está seguro de marcar la receta {0} como 'No Surtida'?\n\n", receta.NumeroReceta) +
                        "Esta receta dejará de aparecer en la lista de recetas pendientes.\n" +
                        "Use esta opción cuando:\n" +
                        "- No hay stock disponible y no se reabastecerá\n" +
                        "- El paciente comprará los medicamentos en otro lugar\n" +
                        "- La receta tiene medicamentos externos que no se venden aquí",
                        "Confirmar - No Surtir Receta",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        using (var nReceta = new NReceta())
                        {
                            receta.Estado = "NoSurtida";
                            nReceta.EditarReceta(receta);
                            MessageBox.Show("Receta marcada como 'No Surtida'", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatosPaciente(); // Recargar lista
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al marcar receta: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCargarReceta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRecetasPendientes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una receta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var receta = dgvRecetasPendientes.SelectedRows[0].DataBoundItem as TReceta;
                if (receta == null) return;

                // Cargar detalles de la receta
                using (var nReceta = new NReceta())
                {
                    var detalles = nReceta.ObtenerDetallesReceta(receta.RecetaId);

                    // IMPORTANTE: Filtrar SOLO medicamentos internos (que están en el inventario de MediNova)
                    // Los medicamentos externos (con NombreMedicamentoExterno) NO se venden aquí y se excluyen
                    var detallesPendientes = detalles
                        .Where(d => d.MedicamentoId.HasValue) // Solo medicamentos del inventario interno
                        .ToList();

                    // Si no hay medicamentos internos en la receta, informar al usuario
                    if (detallesPendientes.Count == 0)
                    {
                        MessageBox.Show(
                            "Esta receta solo contiene medicamentos externos que no están disponibles en la farmacia de MediNova.\n\n" +
                            "Use el botón 'No Surtir' para marcar esta receta como no completable.",
                            "Medicamentos Externos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    // Verificar que la receta no esté ya cargada en el carrito
                    if (detallesPendientes.Any(d => carrito.Any(c => c.RecetaDetalleId.HasValue && c.RecetaDetalleId.Value == d.DetalleRecetaId)))
                    {
                        MessageBox.Show("Esta receta ya está cargada en el carrito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Analizar disponibilidad de stock (solo medicamentos internos)
                    var disponibles = new List<TDetalleReceta>();
                    var noDisponibles = new List<TDetalleReceta>();

                    foreach (var detalle in detallesPendientes)
                    {
                        int cantidadPendiente = detalle.CantidadPrescrita - detalle.CantidadSurtida;
                        int stockDisponible = detalle.Medicamento?.Stock ?? 0;

                        if (cantidadPendiente > 0)
                        {
                            if (stockDisponible > 0)
                            {
                                disponibles.Add(detalle);
                            }
                            else
                            {
                                noDisponibles.Add(detalle);
                            }
                        }
                    }

                    // Evaluar escenarios
                    if (noDisponibles.Count == 0)
                    {
                        // Escenario 1: Todos los medicamentos están disponibles
                        CargarMedicamentosAlCarrito(disponibles, nReceta);
                        MessageBox.Show("Medicamentos de la receta agregados al carrito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (disponibles.Count == 0)
                    {
                        // Escenario 2: Ningún medicamento está disponible
                        string mensaje = "No hay stock disponible para ninguno de los medicamentos de esta receta:\n\n";
                        foreach (var detalle in noDisponibles)
                        {
                            mensaje += string.Format("- {0}\n", detalle.Medicamento != null ? detalle.Medicamento.Nombre ?? "Medicamento" : "Medicamento");
                        }
                        mensaje += "\n¿Desea marcar esta receta como 'No Surtida'?";

                        var result = MessageBox.Show(mensaje, "Sin Stock", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            receta.Estado = "NoSurtida";
                            nReceta.EditarReceta(receta);
                            MessageBox.Show("Receta marcada como 'No Surtida' por falta de stock", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatosPaciente(); // Recargar para actualizar la lista
                        }
                    }
                    else
                    {
                        // Escenario 3: Solo algunos medicamentos están disponibles
                        string mensaje = "Algunos medicamentos no tienen stock disponible:\n\n";
                        mensaje += "DISPONIBLES:\n";
                        foreach (var detalle in disponibles)
                        {
                            int cantidadPendiente = detalle.CantidadPrescrita - detalle.CantidadSurtida;
                            int stockDisponible = detalle.Medicamento?.Stock ?? 0;
                            int cantidad = Math.Min(cantidadPendiente, stockDisponible);
                            mensaje += string.Format("- {0} (Cantidad: {1})\n", detalle.Medicamento != null ? detalle.Medicamento.Nombre : null, cantidad);
                        }
                        mensaje += "\nNO DISPONIBLES:\n";
                        foreach (var detalle in noDisponibles)
                        {
                            mensaje += string.Format("- {0}\n", detalle.Medicamento != null ? detalle.Medicamento.Nombre ?? "Medicamento" : "Medicamento");
                        }
                        mensaje += "\n¿Desea agregar solo los medicamentos disponibles como venta libre?";

                        var result = MessageBox.Show(mensaje, "Stock Parcial", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            CargarMedicamentosAlCarrito(disponibles, nReceta);
                            MessageBox.Show("Medicamentos disponibles agregados al carrito como venta libre.\nLa receta permanece pendiente para los demás medicamentos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar receta: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMedicamentosAlCarrito(List<TDetalleReceta> detalles, NReceta nReceta)
        {
            foreach (var detalle in detalles)
            {
                int cantidadPendiente = detalle.CantidadPrescrita - detalle.CantidadSurtida;
                int stockDisponible = detalle.Medicamento?.Stock ?? 0;
                int cantidad = Math.Min(cantidadPendiente, stockDisponible);

                if (cantidad > 0)
                {
                    var item = new ItemCarrito
                    {
                        Tipo = "Medicamento",
                        ItemId = detalle.MedicamentoId.Value,
                        RecetaDetalleId = detalle.DetalleRecetaId,
                        Descripcion = detalle.Medicamento?.Nombre ?? "Medicamento",
                        Cantidad = cantidad,
                        PrecioUnitario = detalle.Medicamento?.PrecioUnitario ?? 0,
                        Subtotal = 0
                    };
                    item.Subtotal = item.Cantidad * item.PrecioUnitario;
                    carrito.Add(item);
                }
            }
            ActualizarCarrito();
        }

        private void BtnAgregarConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvConsultasPendientes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una consulta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var consulta = dgvConsultasPendientes.SelectedRows[0].DataBoundItem as TConsulta;
                if (consulta == null) return;

                // Verificar que no esté ya en el carrito
                if (carrito.Any(c => c.Tipo == "Consulta" && c.ItemId == consulta.ConsultaId))
                {
                    MessageBox.Show("Esta consulta ya está en el carrito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var item = new ItemCarrito
                {
                    Tipo = "Consulta",
                    ItemId = consulta.ConsultaId,
                    Descripcion = string.Format("Consulta Médica - {0}", consulta.NumeroConsulta),
                    Cantidad = 1,
                    PrecioUnitario = consulta.CostoConsulta,
                    Subtotal = consulta.CostoConsulta
                };

                carrito.Add(item);
                ActualizarCarrito();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al agregar consulta: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCargarExamen_Click(object sender, EventArgs e)
        {
            try
            {
                var dgvExamenes = this.Controls.Find("dgvExamenesPendientes", true).FirstOrDefault() as DataGridView;
                if (dgvExamenes == null || dgvExamenes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un examen", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var examen = dgvExamenes.SelectedRows[0].DataBoundItem as TExamen;
                if (examen == null) return;

                // Verificar que no esté ya en el carrito
                if (carrito.Any(c => c.Tipo == "Examen" && c.ItemId == examen.ExamenId))
                {
                    MessageBox.Show("Este examen ya está en el carrito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var item = new ItemCarrito
                {
                    Tipo = "Examen",
                    ItemId = examen.ExamenId,
                    Descripcion = string.Format("{0} - {1}", examen.Nombre, examen.Tipo),
                    Cantidad = 1,
                    PrecioUnitario = examen.Costo,
                    Subtotal = examen.Costo
                };

                carrito.Add(item);
                ActualizarCarrito();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al agregar examen: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAgregarMedicamento_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir nuevo formulario moderno de venta libre
                using (var formVentaLibre = new FormVentaLibre())
                {
                    if (formVentaLibre.ShowDialog() == DialogResult.OK)
                    {
                        var medicamento = formVentaLibre.MedicamentoSeleccionado;
                        if (medicamento != null)
                        {
                            var item = new ItemCarrito
                            {
                                Tipo = "Medicamento",
                                ItemId = medicamento.MedicamentoId,
                                Descripcion = medicamento.Nombre,
                                Cantidad = formVentaLibre.Cantidad,
                                PrecioUnitario = medicamento.PrecioUnitario,
                                Subtotal = formVentaLibre.Cantidad * medicamento.PrecioUnitario
                            };
                            carrito.Add(item);
                            ActualizarCarrito();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQuitarMedicamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCarrito.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un item del carrito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var item = dgvCarrito.SelectedRows[0].DataBoundItem as ItemCarrito;
                if (item != null)
                {
                    carrito.Remove(item);
                    ActualizarCarrito();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarCarrito()
        {
            dgvCarrito.DataSource = null;
            dgvCarrito.DataSource = carrito.ToList();
            CalcularTotales();
        }

        private void CalcularTotales()
        {
            subtotal = carrito.Sum(c => c.Subtotal);
            total = subtotal - descuento;

            lblSubtotal.Text = string.Format("L {0:#,##0.00}", subtotal);
            lblDescuento.Text = string.Format("L {0:#,##0.00}", descuento);
            lblTotal.Text = string.Format("L {0:#,##0.00}", total);
        }

        private void BtnProcesarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                // Permitir ventas libres sin paciente si solo hay medicamentos
                bool esVentaLibre = carrito.All(c => c.Tipo == "Medicamento");

                if (pacienteActual == null && !esVentaLibre)
                {
                    MessageBox.Show("Debe seleccionar un paciente para ventas con consultas, exámenes o recetas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (carrito.Count == 0)
                {
                    MessageBox.Show("El carrito está vacío", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Mostrar diálogo de pago
                var resultadoPago = MostrarDialogoPago(total);
                if (resultadoPago == null) return; // Usuario canceló

                // Procesar venta
                using (var nVenta = new NVenta())
                {
                    // Crear venta
                    var venta = new TVenta
                    {
                        PacienteId = esVentaLibre ? (int?)null : pacienteActual.PacienteId,
                        UsuarioVentaId = 1, // TODO: Get from logged user
                        FechaVenta = DateTime.Now,
                        Subtotal = subtotal,
                        Descuento = descuento,
                        Total = total,
                        MetodoPago1 = resultadoPago.MetodoPago1,
                        MontoPago1 = resultadoPago.MontoPago1,
                        MetodoPago2 = resultadoPago.MetodoPago2,
                        MontoPago2 = resultadoPago.MontoPago2,
                        MontoPagado = resultadoPago.MontoPagado,
                        Cambio = resultadoPago.Cambio,
                        TipoVenta = esVentaLibre ? "Libre" : (carrito.Any(c => c.Tipo == "Medicamento" && c.RecetaDetalleId.HasValue) ? "Receta" : "Mixta"),
                        Estado = "Completada",
                        Eliminado = false
                    };

                    // Crear detalles
                    var detalles = new List<TDetalleVenta>();
                    foreach (var item in carrito.Where(c => c.Tipo == "Medicamento"))
                    {
                        detalles.Add(new TDetalleVenta
                        {
                            MedicamentoId = item.ItemId,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioUnitario,
                            Subtotal = item.Subtotal,
                            Eliminado = false
                        });
                    }

                    // Guardar venta
                    nVenta.GuardarVenta(venta, detalles);

                    // Actualizar estados de consultas y vincular con la venta
                    foreach (var item in carrito.Where(c => c.Tipo == "Consulta"))
                    {
                        using (var nConsulta = new NConsulta())
                        {
                            nConsulta.RegistrarPago(item.ItemId, venta.VentaId);
                        }
                    }

                    // Actualizar estados de exámenes a Pagado y vincular con la venta
                    foreach (var item in carrito.Where(c => c.Tipo == "Examen"))
                    {
                        using (var nExamen = new NExamen())
                        {
                            var examen = nExamen.BuscarPorId(item.ItemId);
                            if (examen != null)
                            {
                                examen.Estado = "Pagado";
                                examen.VentaId = venta.VentaId;
                                nExamen.EditarExamen(examen);
                            }
                        }
                    }

                    // Actualizar estado de recetas si hay
                    ActualizarEstadoRecetas();

                    // Mostrar resumen con cambio
                    string mensaje = "Venta procesada exitosamente\n\n";
                    mensaje += string.Format("Total: L {0:#,##0.00}\n", total);
                    mensaje += string.Format("Pagado: L {0:#,##0.00}\n", resultadoPago.MontoPagado);
                    if (resultadoPago.Cambio > 0)
                    {
                        mensaje += string.Format("Cambio: L {0:#,##0.00}", resultadoPago.Cambio);
                    }

                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al procesar venta: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarEstadoRecetas()
        {
            try
            {
                var recetasAfectadas = carrito
                    .Where(c => c.Tipo == "Medicamento" && c.RecetaDetalleId.HasValue)
                    .Select(c => c.RecetaDetalleId.Value)
                    .Distinct();

                foreach (var detalleId in recetasAfectadas)
                {
                    var item = carrito.First(c => c.RecetaDetalleId == detalleId);

                    using (var nReceta = new NReceta())
                    {
                        nReceta.ActualizarCantidadSurtida(detalleId, item.Cantidad);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error pero no interrumpir el flujo
                Console.WriteLine(string.Format("Error al actualizar recetas: {0}", ex.Message));
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            pacienteActual = null;
            carrito.Clear();
            txtBuscarPaciente.Clear();
            lblNombrePaciente.Text = "Nombre del Paciente";
            lblDNIPaciente.Text = "DNI: N/A";
            panelInfoPaciente.Visible = false;
            dgvRecetasPendientes.DataSource = null;
            dgvConsultasPendientes.DataSource = null;

            // Limpiar grid de exámenes si existe
            var dgvExamenes = this.Controls.Find("dgvExamenesPendientes", true).FirstOrDefault() as DataGridView;
            if (dgvExamenes != null)
            {
                dgvExamenes.DataSource = null;
            }

            dgvCarrito.DataSource = null;
            subtotal = 0;
            descuento = 0;
            total = 0;
            CalcularTotales();
        }

        private ResultadoPago MostrarDialogoPago(decimal totalPagar)
        {
            // Usar nuevo formulario moderno de procesar pago con lista de productos
            using (var formPago = new FormProcesarPago(totalPagar, carrito.ToList()))
            {
                if (formPago.ShowDialog() == DialogResult.OK)
                {
                    return formPago.Resultado;
                }
                return null;
            }
        }

        private void BtnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                using (var formHistorial = new FormHistorialVentas())
                {
                    formHistorial.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir historial: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelInfoPaciente_Click(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// Clase para representar un item en el carrito de ventas
    /// </summary>
    public class ItemCarrito
    {
        public string Tipo { get; set; } // "Medicamento", "Consulta" o "Examen"
        public int ItemId { get; set; }
        public int? RecetaDetalleId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    /// <summary>
    /// Clase para devolver el resultado del procesamiento de pago
    /// </summary>
    public class ResultadoPago
    {
        public string MetodoPago1 { get; set; }
        public decimal MontoPago1 { get; set; }
        public string MetodoPago2 { get; set; }
        public decimal? MontoPago2 { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal Cambio { get; set; }
    }
}
