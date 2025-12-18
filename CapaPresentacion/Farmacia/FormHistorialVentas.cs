using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaNegocio.Farmacia;
using CapaNegocio.Medico;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace CapaPresentacion.Farmacia
{
    public partial class FormHistorialVentas : Form
    {
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

        public FormHistorialVentas()
        {
            InitializeComponent();
            this.Load += FormHistorialVentas_Load;

            // Aplica el redondeo en el constructor
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); // Radio de 20
        }

        private void FormHistorialVentas_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid();
            CargarVentas();
        }

        private void ConfigurarDataGrid()
        {
            dgvHistorial.AutoGenerateColumns = false;
            dgvHistorial.Columns.Clear();

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "VentaId",
                DataPropertyName = "VentaId",
                Visible = false
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumeroVenta",
                HeaderText = "N° Venta",
                DataPropertyName = "NumeroVenta",
                Width = 120
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaVenta",
                HeaderText = "Fecha",
                DataPropertyName = "FechaVenta",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoVenta",
                HeaderText = "Tipo",
                DataPropertyName = "TipoVenta",
                Width = 100
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Paciente",
                HeaderText = "Paciente",
                Width = 200
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" }
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MetodoPago",
                HeaderText = "Método Pago",
                DataPropertyName = "MetodoPago1",
                Width = 130
            });

            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

            // Event handler for formatting patient cell
            dgvHistorial.CellFormatting += DgvHistorial_CellFormatting;
        }

        private void DgvHistorial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHistorial.Columns[e.ColumnIndex].Name == "Paciente")
            {
                var venta = dgvHistorial.Rows[e.RowIndex].DataBoundItem as TVenta;
                if (venta != null)
                {
                    e.Value = venta.Paciente != null ? venta.Paciente.NombreCompleto : "Venta Libre";
                    e.FormattingApplied = true;
                }
            }
        }

        private void CargarVentas(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                using (var nVenta = new NVenta())
                {
                    var ventas = nVenta.ListarVentas();

                    // Filtrar por fechas si se proporcionan
                    if (fechaInicio.HasValue && fechaFin.HasValue)
                    {
                        ventas = ventas.Where(v =>
                            v.FechaVenta >= fechaInicio.Value &&
                            v.FechaVenta <= fechaFin.Value
                        ).ToList();
                    }
                    else
                    {
                        // Por defecto mostrar ventas de hoy
                        var hoy = DateTime.Today;
                        ventas = ventas.Where(v => v.FechaVenta >= hoy).ToList();
                    }

                    dgvHistorial.DataSource = ventas.OrderByDescending(v => v.FechaVenta).ToList();
                    lblTotal.Text = string.Format("Total de ventas: {0} | Monto total: L {1:#,##0.00}", ventas.Count, ventas.Sum(v => v.Total));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar ventas: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFiltrar_Click(object sender, EventArgs e)
        {
            CargarVentas(dtpInicio.Value.Date, dtpFin.Value.Date.AddDays(1).AddSeconds(-1));
        }

        private void BtnHoy_Click(object sender, EventArgs e)
        {
            var hoy = DateTime.Today;
            dtpInicio.Value = hoy;
            dtpFin.Value = hoy;
            CargarVentas(hoy, hoy.AddDays(1).AddSeconds(-1));
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvHistorial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var venta = dgvHistorial.Rows[e.RowIndex].DataBoundItem as TVenta;
                if (venta != null)
                {
                    MostrarDetalleVenta(venta);
                }
            }
        }

        private void MostrarDetalleVenta(TVenta venta)
        {
            try
            {
                using (var nVenta = new NVenta())
                {
                    var detalles = nVenta.ObtenerDetallesVenta(venta.VentaId);

                    string mensaje = string.Format("Detalles de Venta: {0}\n", venta.NumeroVenta);
                    mensaje += string.Format("Fecha: {0:dd/MM/yyyy HH:mm}\n", venta.FechaVenta);
                    mensaje += string.Format("Paciente: {0}\n", venta.Paciente != null ? venta.Paciente.NombreCompleto : "Venta Libre");
                    mensaje += string.Format("Tipo: {0}\n\n", venta.TipoVenta);

                    bool hayItems = false;

                    // Mostrar medicamentos
                    if (detalles != null && detalles.Count > 0)
                    {
                        mensaje += "MEDICAMENTOS:\n";
                        mensaje += "─────────────────────────────────────\n";
                        foreach (var detalle in detalles)
                        {
                            mensaje += string.Format("• {0}\n", detalle.Medicamento != null ? detalle.Medicamento.Nombre : "N/A");
                            mensaje += string.Format("  Cantidad: {0} | Precio: L {1:#,##0.00} | Subtotal: L {2:#,##0.00}\n", detalle.Cantidad, detalle.PrecioUnitario, detalle.Subtotal);
                        }
                        mensaje += "\n";
                        hayItems = true;
                    }

                    // Mostrar consultas pagadas en esta venta
                    if (venta.PacienteId.HasValue)
                    {
                        using (var nConsulta = new NConsulta())
                        {
                            var consultas = nConsulta.ObtenerConsultasPorPaciente(venta.PacienteId.Value)
                                .Where(c => c.VentaId == venta.VentaId)
                                .ToList();

                            if (consultas.Count > 0)
                            {
                                mensaje += "CONSULTAS MÉDICAS:\n";
                                mensaje += "─────────────────────────────────────\n";
                                foreach (var consulta in consultas)
                                {
                                    mensaje += string.Format("• Consulta: {0}\n", consulta.NumeroConsulta);
                                    mensaje += string.Format("  Doctor: {0}\n", consulta.Doctor != null ? consulta.Doctor.NombreCompleto : "N/A");
                                    mensaje += string.Format("  Fecha: {0:dd/MM/yyyy}\n", consulta.FechaConsulta);
                                    mensaje += string.Format("  Costo: L {0:#,##0.00}\n", consulta.CostoConsulta);
                                }
                                mensaje += "\n";
                                hayItems = true;
                            }
                        }

                        // Mostrar exámenes pagados en esta venta
                        using (var nExpediente = new NExpediente())
                        using (var nExamen = new NExamen())
                        {
                            var expediente = nExpediente.BuscarPorPacienteId(venta.PacienteId.Value);
                            if (expediente != null)
                            {
                                var examenes = nExamen.BuscarPorExpedienteId(expediente.PacienteId)
                                    .Where(ex => ex.VentaId == venta.VentaId)
                                    .ToList();

                                if (examenes.Count > 0)
                                {
                                    mensaje += "EXÁMENES MÉDICOS:\n";
                                    mensaje += "─────────────────────────────────────\n";
                                    foreach (var examen in examenes)
                                    {
                                        mensaje += string.Format("• {0} ({1})\n", examen.Nombre, examen.Tipo);
                                        mensaje += string.Format("  Fecha Solicitud: {0:dd/MM/yyyy}\n", examen.FechaSolicitud);
                                        mensaje += string.Format("  Estado: {0}\n", examen.Estado);
                                        mensaje += string.Format("  Costo: L {0:#,##0.00}\n", examen.Costo);
                                    }
                                    mensaje += "\n";
                                    hayItems = true;
                                }
                            }
                        }
                    }

                    if (!hayItems)
                    {
                        mensaje += "No hay items en esta venta.\n\n";
                    }

                    mensaje += "═════════════════════════════════════\n";
                    mensaje += string.Format("Subtotal: L {0:#,##0.00}\n", venta.Subtotal);
                    if (venta.Descuento > 0)
                        mensaje += string.Format("Descuento: L {0:#,##0.00}\n", venta.Descuento);
                    if (venta.Impuesto > 0)
                        mensaje += string.Format("Impuesto: L {0:#,##0.00}\n", venta.Impuesto);
                    mensaje += string.Format("TOTAL: L {0:#,##0.00}\n", venta.Total);
                    mensaje += "═════════════════════════════════════\n";
                    mensaje += string.Format("Método de pago: {0}", venta.MetodoPago1);
                    if (!string.IsNullOrEmpty(venta.MetodoPago2))
                        mensaje += string.Format(" + {0}", venta.MetodoPago2);
                    if (venta.Cambio.HasValue && venta.Cambio.Value > 0)
                        mensaje += string.Format("\nCambio: L {0:#,##0.00}", venta.Cambio.Value);

                    MessageBox.Show(mensaje, "Detalle de Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar detalles: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormHistorialVentas_Load_1(object sender, EventArgs e)
        {

        }
    }
}
