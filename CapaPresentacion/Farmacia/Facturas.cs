using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Farmacia;
using CapaNegocio.Compartido;
using CapaNegocio.Medico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.IO;

namespace CapaPresentacion.Farmacia
{
    public partial class Facturas : Form
    {
        public Facturas()
        {
            InitializeComponent();
            this.Load += Facturas_Load;
            btnBuscar.Click += BtnBuscar_Click;
            btnVerDetalle.Click += BtnVerDetalle_Click;
            btnRefrescar.Click += BtnRefrescar_Click;
            btnImprimirPDF.Click += BtnImprimirPDF_Click;
            btnImprimirTXT.Click += BtnImprimirTXT_Click;
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid();
            CargarFacturas();
        }

        private void ConfigurarDataGrid()
        {
            dgvFacturas.AutoGenerateColumns = false;
            dgvFacturas.Columns.Clear();
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "VentaId", DataPropertyName = "VentaId", Visible = false });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroFactura", HeaderText = "N° Factura", DataPropertyName = "NumeroFactura", Width = 120 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha", DataPropertyName = "Fecha", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombrePaciente", HeaderText = "Paciente", Width = 200 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total", DataPropertyName = "Total", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "L #,##0.00" } });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "MetodoPago", HeaderText = "Método Pago", DataPropertyName = "MetodoPago", Width = 120 });

            // Evento para mostrar el nombre del paciente desde la propiedad de navegación
            dgvFacturas.CellFormatting += DgvFacturas_CellFormatting;
        }

        private void DgvFacturas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvFacturas.Columns[e.ColumnIndex].Name == "NombrePaciente")
            {
                if (dgvFacturas.Rows[e.RowIndex].DataBoundItem is TFactura factura)
                {
                    e.Value = factura.Paciente?.NombreCompleto ?? "N/A";
                    e.FormattingApplied = true;
                }
            }
        }

        private void CargarFacturas()
        {
            try
            {
                using (var nFactura = new NFactura())
                {
                    var facturas = nFactura.ListarFacturas();
                    dgvFacturas.DataSource = facturas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar facturas: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string criterio = txtBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(criterio))
                {
                    CargarFacturas();
                    return;
                }

                using (var nFactura = new NFactura())
                {
                    var factura = nFactura.BuscarPorNumero(criterio);
                    if (factura != null)
                    {
                        dgvFacturas.DataSource = new List<TFactura> { factura };
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la factura", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFacturas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una factura", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var factura = dgvFacturas.SelectedRows[0].DataBoundItem as TFactura;
                if (factura == null) return;

                // Mostrar detalles de la factura
                string detalles = string.Format("Número: {0}\n", factura.NumeroFactura) +
                               string.Format("Fecha: {0:dd/MM/yyyy}\n", factura.Fecha) +
                               string.Format("Paciente: {0}\n", factura.Paciente != null ? factura.Paciente.NombreCompleto : "N/A") +
                               string.Format("Método de Pago: {0}\n", factura.MetodoPago) +
                               string.Format("Subtotal: L {0:#,##0.00}\n", factura.Subtotal) +
                               string.Format("Descuento: L {0:#,##0.00}\n", factura.Descuento) +
                               string.Format("Impuesto: L {0:#,##0.00}\n", factura.Impuesto) +
                               string.Format("Total: L {0:#,##0.00}", factura.Total);

                MessageBox.Show(detalles, string.Format("Detalle de Factura - {0}", factura.NumeroFactura), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al ver detalle: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarFacturas();
        }

        private void BtnImprimirPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFacturas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una factura", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var factura = dgvFacturas.SelectedRows[0].DataBoundItem as TFactura;
                if (factura == null) return;

                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                    saveDialog.FileName = string.Format("Factura_{0}_{1:yyyyMMdd}.pdf", factura.NumeroFactura, DateTime.Now);
                    saveDialog.Title = "Guardar Factura PDF";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        GenerarFacturaPDF(factura, saveDialog.FileName);

                        MessageBox.Show(string.Format("Factura guardada exitosamente en:\n{0}", saveDialog.FileName), "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Preguntar si desea abrir el archivo
                        if (MessageBox.Show("¿Desea abrir el archivo?", "Abrir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al imprimir factura PDF: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImprimirTXT_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFacturas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una factura", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var factura = dgvFacturas.SelectedRows[0].DataBoundItem as TFactura;
                if (factura == null) return;

                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveDialog.FileName = string.Format("Factura_{0}_{1:yyyyMMdd}.txt", factura.NumeroFactura, DateTime.Now);
                    saveDialog.Title = "Guardar Factura TXT";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        GenerarFacturaTXT(factura, saveDialog.FileName);

                        MessageBox.Show(string.Format("Factura guardada exitosamente en:\n{0}", saveDialog.FileName), "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Preguntar si desea abrir el archivo
                        if (MessageBox.Show("¿Desea abrir el archivo?", "Abrir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al imprimir factura TXT: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarFacturaTXT(TFactura factura, string rutaArchivo)
        {
            try
            {
                // Obtener información de la empresa
                TEmpresa empresa = null;
                using (var nEmpresa = new NEmpresa())
                {
                    empresa = nEmpresa.ObtenerInformacion();
                }

                using (var writer = new System.IO.StreamWriter(rutaArchivo))
                {
                    writer.WriteLine("╔══════════════════════════════════════════════════════════════╗");

                    if (empresa != null)
                    {
                        string nombreEmpresa = empresa.NombreEmpresa ?? "MEDINOVA";
                        int padding = (60 - nombreEmpresa.Length) / 2;
                        writer.WriteLine(string.Format("║{0}{1}{2}║", new string(' ', padding), nombreEmpresa, new string(' ', 60 - nombreEmpresa.Length - padding)));

                        if (!string.IsNullOrWhiteSpace(empresa.Slogan))
                        {
                            string slogan = empresa.Slogan.Length > 58 ? empresa.Slogan.Substring(0, 55) + "..." : empresa.Slogan;
                            padding = (60 - slogan.Length) / 2;
                            writer.WriteLine(string.Format("║{0}{1}{2}║", new string(' ', padding), slogan, new string(' ', 60 - slogan.Length - padding)));
                        }

                        if (!string.IsNullOrWhiteSpace(empresa.RTN))
                            writer.WriteLine(string.Format("║  RTN: {0,-53}║", empresa.RTN));
                        if (!string.IsNullOrWhiteSpace(empresa.Telefono))
                            writer.WriteLine(string.Format("║  Tel: {0,-53}║", empresa.Telefono));
                        if (!string.IsNullOrWhiteSpace(empresa.Direccion))
                        {
                            string direccion = empresa.Direccion.Length > 53 ? empresa.Direccion.Substring(0, 50) + "..." : empresa.Direccion;
                            writer.WriteLine(string.Format("║  Dir: {0,-53}║", direccion));
                        }
                        if (!string.IsNullOrWhiteSpace(empresa.Email))
                            writer.WriteLine(string.Format("║  Email: {0,-51}║", empresa.Email));
                    }
                    else
                    {
                        writer.WriteLine("║                         MEDINOVA                             ║");
                        writer.WriteLine("║                    Sistema Médico                            ║");
                    }

                    writer.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                    writer.WriteLine();
                    writer.WriteLine(string.Format("FACTURA: {0}", factura.NumeroFactura));
                    writer.WriteLine(string.Format("FECHA: {0:dd/MM/yyyy HH:mm}", factura.Fecha));
                    writer.WriteLine(string.Format("PACIENTE: {0}", factura.Paciente != null ? factura.Paciente.NombreCompleto : "Venta Libre"));
                    writer.WriteLine(string.Format("DNI: {0}", factura.Paciente != null ? factura.Paciente.DNI ?? "N/A" : "N/A"));
                    writer.WriteLine();
                    writer.WriteLine("──────────────────────────────────────────────────────────────");
                    writer.WriteLine("DETALLE DE FACTURA");
                    writer.WriteLine("──────────────────────────────────────────────────────────────");
                    writer.WriteLine();

                    // Obtener todos los detalles: medicamentos, consultas y exámenes
                    writer.WriteLine(string.Format("{0,-30} {1,10} {2,15} {3,15}", "DESCRIPCIÓN", "CANT", "PRECIO", "SUBTOTAL"));
                    writer.WriteLine("──────────────────────────────────────────────────────────────");

                    // MEDICAMENTOS de la venta
                    using (var nVenta = new NVenta())
                    {
                        var detallesMedicamentos = nVenta.ObtenerDetallesVenta(factura.VentaId);
                        if (detallesMedicamentos != null && detallesMedicamentos.Count > 0)
                        {
                            foreach (var detalle in detallesMedicamentos)
                            {
                                string descripcion = detalle.Medicamento != null ? detalle.Medicamento.Nombre ?? "Medicamento" : "Medicamento";
                                if (descripcion.Length > 28) descripcion = descripcion.Substring(0, 25) + "...";
                                writer.WriteLine(string.Format("{0,-30} {1,10} {2,15} {3,15}", descripcion, detalle.Cantidad, string.Format("L {0:#,##0.00}", detalle.PrecioUnitario), string.Format("L {0:#,##0.00}", detalle.Subtotal)));
                            }
                        }
                    }

                    // CONSULTAS de esta venta (si hay paciente)
                    if (factura.PacienteId.HasValue)
                    {
                        using (var nConsulta = new CapaNegocio.Medico.NConsulta())
                        {
                            var consultas = nConsulta.ObtenerConsultasPorPaciente(factura.PacienteId.Value)
                                .Where(c => c.VentaId == factura.VentaId)
                                .ToList();

                            foreach (var consulta in consultas)
                            {
                                string descripcion = string.Format("Consulta Médica - {0}", consulta.NumeroConsulta);
                                if (descripcion.Length > 28) descripcion = descripcion.Substring(0, 25) + "...";
                                writer.WriteLine(string.Format("{0,-30} {1,10} {2,15} {3,15}", descripcion, 1, string.Format("L {0:#,##0.00}", consulta.CostoConsulta), string.Format("L {0:#,##0.00}", consulta.CostoConsulta)));
                            }
                        }

                        // EXÁMENES de esta venta
                        using (var nExpediente = new CapaNegocio.Medico.NExpediente())
                        using (var nExamen = new CapaNegocio.Medico.NExamen())
                        {
                            var expediente = nExpediente.BuscarPorPacienteId(factura.PacienteId.Value);
                            if (expediente != null)
                            {
                                var examenes = nExamen.BuscarPorExpedienteId(expediente.PacienteId)
                                    .Where(ex => ex.VentaId == factura.VentaId && ex.Estado != "Cancelado")
                                    .ToList();

                                foreach (var examen in examenes)
                                {
                                    string descripcion = string.Format("Examen: {0}", examen.Nombre);
                                    if (descripcion.Length > 28) descripcion = descripcion.Substring(0, 25) + "...";
                                    writer.WriteLine(string.Format("{0,-30} {1,10} {2,15} {3,15}", descripcion, 1, string.Format("L {0:#,##0.00}", examen.Costo), string.Format("L {0:#,##0.00}", examen.Costo)));
                                }
                            }
                        }
                    }

                    writer.WriteLine("──────────────────────────────────────────────────────────────");
                    writer.WriteLine(string.Format("{0,45} {1,15}", "SUBTOTAL:", string.Format("L {0:#,##0.00}", factura.Subtotal)));
                    writer.WriteLine(string.Format("{0,45} {1,15}", "DESCUENTO:", string.Format("L {0:#,##0.00}", factura.Descuento)));
                    writer.WriteLine(string.Format("{0,45} {1,15}", "IMPUESTO:", string.Format("L {0:#,##0.00}", factura.Impuesto)));
                    writer.WriteLine("══════════════════════════════════════════════════════════════");
                    writer.WriteLine(string.Format("{0,45} {1,15}", "TOTAL:", string.Format("L {0:#,##0.00}", factura.Total)));
                    writer.WriteLine("══════════════════════════════════════════════════════════════");
                    writer.WriteLine();

                    // Mostrar métodos de pago (puede ser uno o dos si es pago mixto)
                    if (factura.Venta != null && !string.IsNullOrEmpty(factura.Venta.MetodoPago2))
                    {
                        // Pago mixto
                        writer.WriteLine("MÉTODOS DE PAGO:");
                        writer.WriteLine(string.Format("  • {0}: L {1:#,##0.00}", factura.Venta.MetodoPago1, factura.Venta.MontoPago1));
                        writer.WriteLine(string.Format("  • {0}: L {1:#,##0.00}", factura.Venta.MetodoPago2, factura.Venta.MontoPago2));
                    }
                    else if (factura.Venta != null)
                    {
                        // Pago único
                        writer.WriteLine(string.Format("MÉTODO DE PAGO: {0}", factura.Venta.MetodoPago1));
                    }
                    else
                    {
                        writer.WriteLine(string.Format("MÉTODO DE PAGO: {0}", factura.MetodoPago));
                    }

                    writer.WriteLine("ESTADO: Pagada");
                    writer.WriteLine();
                    writer.WriteLine("──────────────────────────────────────────────────────────────");
                    writer.WriteLine("             Gracias por su preferencia");
                    writer.WriteLine("──────────────────────────────────────────────────────────────");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al generar factura TXT: {0}", ex.Message), ex);
            }
        }

        private void GenerarFacturaPDF(TFactura factura, string rutaArchivo)
        {
            try
            {
                // Obtener información de la empresa
                TEmpresa empresa = null;
                using (var nEmpresa = new NEmpresa())
                {
                    empresa = nEmpresa.ObtenerInformacion();
                }

                Document documento = new Document(PageSize.A4, 35, 35, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));

                documento.Open();

                // Paleta de colores profesional
                BaseColor azulPrimario = new BaseColor(41, 128, 185);    // #2980b9
                BaseColor azulOscuro = new BaseColor(23, 84, 133);       // #175485
                BaseColor grisClaro = new BaseColor(236, 240, 241);      // #ecf0f1
                BaseColor grisTexto = new BaseColor(52, 73, 94);         // #34495e

                // Fuentes modernas
                Font fontTituloEmpresa = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22, azulOscuro);
                Font fontSlogan = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY);
                Font fontTituloFactura = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, azulPrimario);
                Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, grisTexto);
                Font fontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 9, grisTexto);
                Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, grisTexto);
                Font fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
                Font fontTotal = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, azulOscuro);

                // ========== ENCABEZADO CON BANNER AZUL Y LOGO ==========
                PdfPTable bannerTable = new PdfPTable(3);
                bannerTable.WidthPercentage = 100;
                bannerTable.SetWidths(new float[] { 0.8f, 1.7f, 1.5f });

                // Celda del LOGO
                PdfPCell logoCell = new PdfPCell();
                logoCell.BackgroundColor = azulPrimario;
                logoCell.Border = Rectangle.NO_BORDER;
                logoCell.Padding = 10;
                logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
                logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                if (empresa != null && empresa.Logo != null && empresa.Logo.Length > 0)
                {
                    try
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(empresa.Logo);
                        logo.ScaleToFit(60f, 60f);
                        logo.Alignment = Element.ALIGN_CENTER;
                        logoCell.AddElement(logo);
                    }
                    catch
                    {
                        // Si falla, dejar celda vacía
                    }
                }
                bannerTable.AddCell(logoCell);

                // Celda del CENTRO con información de empresa
                PdfPCell empresaCell = new PdfPCell();
                empresaCell.BackgroundColor = azulPrimario;
                empresaCell.Border = Rectangle.NO_BORDER;
                empresaCell.Padding = 15;
                empresaCell.PaddingBottom = 10;

                if (empresa != null)
                {
                    Paragraph nombreEmp = new Paragraph(empresa.NombreEmpresa ?? "MEDINOVA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.WHITE));
                    nombreEmp.SpacingAfter = 3;
                    empresaCell.AddElement(nombreEmp);

                    if (!string.IsNullOrWhiteSpace(empresa.Slogan))
                    {
                        Paragraph sloganP = new Paragraph(empresa.Slogan, FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.WHITE));
                        sloganP.SpacingAfter = 8;
                        empresaCell.AddElement(sloganP);
                    }

                    if (!string.IsNullOrWhiteSpace(empresa.RTN))
                        empresaCell.AddElement(new Paragraph(string.Format("RTN: {0}", empresa.RTN), FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE)));
                    if (!string.IsNullOrWhiteSpace(empresa.Telefono))
                        empresaCell.AddElement(new Paragraph(string.Format("Tel: {0}", empresa.Telefono), FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE)));
                    if (!string.IsNullOrWhiteSpace(empresa.Email))
                        empresaCell.AddElement(new Paragraph(string.Format("Email: {0}", empresa.Email), FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE)));
                }
                else
                {
                    empresaCell.AddElement(new Paragraph("MEDINOVA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.WHITE)));
                    empresaCell.AddElement(new Paragraph("Sistema de Gestión Médica", FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.WHITE)));
                }
                bannerTable.AddCell(empresaCell);

                // Celda derecha con información de factura
                PdfPCell facturaInfoCell = new PdfPCell();
                facturaInfoCell.BackgroundColor = azulOscuro;
                facturaInfoCell.Border = Rectangle.NO_BORDER;
                facturaInfoCell.Padding = 15;
                facturaInfoCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                Paragraph facturaTitle = new Paragraph("FACTURA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.WHITE));
                facturaTitle.Alignment = Element.ALIGN_RIGHT;
                facturaTitle.SpacingAfter = 5;
                facturaInfoCell.AddElement(facturaTitle);

                Paragraph numFactura = new Paragraph(factura.NumeroFactura, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.WHITE));
                numFactura.Alignment = Element.ALIGN_RIGHT;
                numFactura.SpacingAfter = 8;
                facturaInfoCell.AddElement(numFactura);

                Paragraph fechaP = new Paragraph(factura.Fecha.ToString("dd/MM/yyyy HH:mm"), FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.WHITE));
                fechaP.Alignment = Element.ALIGN_RIGHT;
                facturaInfoCell.AddElement(fechaP);

                bannerTable.AddCell(facturaInfoCell);

                documento.Add(bannerTable);
                documento.Add(new Paragraph(" ", fontNormal));

                // ========== INFORMACIÓN DEL CLIENTE Y PAGO ==========
                PdfPTable infoTable = new PdfPTable(2);
                infoTable.WidthPercentage = 100;
                infoTable.SetWidths(new float[] { 1, 1 });
                infoTable.SpacingBefore = 10;
                infoTable.SpacingAfter = 15;

                // Columna izquierda - Información del cliente
                PdfPCell clienteCell = new PdfPCell();
                clienteCell.BackgroundColor = grisClaro;
                clienteCell.Border = Rectangle.NO_BORDER;
                clienteCell.Padding = 10;

                Paragraph clienteTitulo = new Paragraph("INFORMACIÓN DEL CLIENTE", fontSubtitulo);
                clienteTitulo.SpacingAfter = 5;
                clienteCell.AddElement(clienteTitulo);

                clienteCell.AddElement(new Paragraph(string.Format("Nombre: {0}", factura.Paciente != null ? factura.Paciente.NombreCompleto : "Venta Libre"), fontNormal));
                clienteCell.AddElement(new Paragraph(string.Format("DNI: {0}", factura.Paciente != null ? factura.Paciente.DNI ?? "N/A" : "N/A"), fontNormal));
                infoTable.AddCell(clienteCell);

                // Columna derecha - Información del pago
                PdfPCell pagoCell = new PdfPCell();
                pagoCell.BackgroundColor = grisClaro;
                pagoCell.Border = Rectangle.NO_BORDER;
                pagoCell.Padding = 10;

                Paragraph pagoTitulo = new Paragraph("MÉTODO DE PAGO", fontSubtitulo);
                pagoTitulo.SpacingAfter = 5;
                pagoCell.AddElement(pagoTitulo);

                if (factura.Venta != null)
                {
                    if (!string.IsNullOrEmpty(factura.Venta.MetodoPago2))
                    {
                        pagoCell.AddElement(new Paragraph(string.Format("{0}: L {1:#,##0.00}", factura.Venta.MetodoPago1, factura.Venta.MontoPago1), fontNormal));
                        pagoCell.AddElement(new Paragraph(string.Format("{0}: L {1:#,##0.00}", factura.Venta.MetodoPago2, factura.Venta.MontoPago2), fontNormal));
                    }
                    else
                    {
                        pagoCell.AddElement(new Paragraph(factura.Venta.MetodoPago1, fontNormal));
                    }
                }
                else
                {
                    pagoCell.AddElement(new Paragraph(factura.MetodoPago ?? "Efectivo", fontNormal));
                }
                infoTable.AddCell(pagoCell);

                documento.Add(infoTable);

                // ========== TABLA DE DETALLES CON FILAS ALTERNADAS ==========
                PdfPTable detallesTable = new PdfPTable(4);
                detallesTable.WidthPercentage = 100;
                detallesTable.SetWidths(new float[] { 3.5f, 1, 1.5f, 1.5f });
                detallesTable.SpacingBefore = 5;

                // Encabezados
                string[] headers = { "Descripción", "Cant.", "Precio Unit.", "Subtotal" };
                int[] alignments = { Element.ALIGN_LEFT, Element.ALIGN_CENTER, Element.ALIGN_RIGHT, Element.ALIGN_RIGHT };

                for (int i = 0; i < headers.Length; i++)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(headers[i], fontHeader));
                    headerCell.BackgroundColor = azulPrimario;
                    headerCell.HorizontalAlignment = alignments[i];
                    headerCell.Padding = 10;
                    headerCell.Border = Rectangle.NO_BORDER;
                    detallesTable.AddCell(headerCell);
                }

                // Agregar TODOS los servicios: medicamentos, consultas y exámenes
                bool filaAlternada = false;

                // MEDICAMENTOS de la venta
                using (var nVenta = new NVenta())
                {
                    var detallesMedicamentos = nVenta.ObtenerDetallesVenta(factura.VentaId);
                    if (detallesMedicamentos != null && detallesMedicamentos.Count > 0)
                    {
                        foreach (var detalle in detallesMedicamentos)
                        {
                            BaseColor colorFila = filaAlternada ? grisClaro : BaseColor.WHITE;

                            PdfPCell dataCell = new PdfPCell(new Phrase(detalle.Medicamento?.Nombre ?? "N/A", fontNormal));
                            dataCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            dataCell = new PdfPCell(new Phrase(detalle.Cantidad.ToString(), fontNormal));
                            dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            dataCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", detalle.PrecioUnitario), fontNormal));
                            dataCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            dataCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", detalle.Subtotal), fontBold));
                            dataCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            filaAlternada = !filaAlternada;
                        }
                    }
                }

                // CONSULTAS de esta venta (si hay paciente)
                if (factura.PacienteId.HasValue)
                {
                    using (var nConsulta = new CapaNegocio.Medico.NConsulta())
                    {
                        var consultas = nConsulta.ObtenerConsultasPorPaciente(factura.PacienteId.Value)
                            .Where(c => c.VentaId == factura.VentaId)
                            .ToList();

                        foreach (var consulta in consultas)
                        {
                            BaseColor colorFila = filaAlternada ? grisClaro : BaseColor.WHITE;

                            PdfPCell dataCell = new PdfPCell(new Phrase(string.Format("Consulta Médica - {0}", consulta.NumeroConsulta), fontNormal));
                            dataCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            dataCell = new PdfPCell(new Phrase("1", fontNormal));
                            dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            dataCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", consulta.CostoConsulta), fontNormal));
                            dataCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            dataCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", consulta.CostoConsulta), fontBold));
                            dataCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            dataCell.Padding = 8;
                            dataCell.BackgroundColor = colorFila;
                            dataCell.Border = Rectangle.NO_BORDER;
                            dataCell.BorderWidthBottom = 1;
                            dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                            detallesTable.AddCell(dataCell);

                            filaAlternada = !filaAlternada;
                        }
                    }

                    // EXÁMENES de esta venta
                    using (var nExpediente = new CapaNegocio.Medico.NExpediente())
                    using (var nExamen = new CapaNegocio.Medico.NExamen())
                    {
                        var expediente = nExpediente.BuscarPorPacienteId(factura.PacienteId.Value);
                        if (expediente != null)
                        {
                            var examenes = nExamen.BuscarPorExpedienteId(expediente.PacienteId)
                                .Where(ex => ex.VentaId == factura.VentaId && ex.Estado != "Cancelado")
                                .ToList();

                            foreach (var examen in examenes)
                            {
                                BaseColor colorFila = filaAlternada ? grisClaro : BaseColor.WHITE;

                                PdfPCell dataCell = new PdfPCell(new Phrase(string.Format("Examen: {0} ({1})", examen.Nombre, examen.Tipo), fontNormal));
                                dataCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                dataCell.Padding = 8;
                                dataCell.BackgroundColor = colorFila;
                                dataCell.Border = Rectangle.NO_BORDER;
                                dataCell.BorderWidthBottom = 1;
                                dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                                detallesTable.AddCell(dataCell);

                                dataCell = new PdfPCell(new Phrase("1", fontNormal));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 8;
                                dataCell.BackgroundColor = colorFila;
                                dataCell.Border = Rectangle.NO_BORDER;
                                dataCell.BorderWidthBottom = 1;
                                dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                                detallesTable.AddCell(dataCell);

                                dataCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", examen.Costo), fontNormal));
                                dataCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                dataCell.Padding = 8;
                                dataCell.BackgroundColor = colorFila;
                                dataCell.Border = Rectangle.NO_BORDER;
                                dataCell.BorderWidthBottom = 1;
                                dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                                detallesTable.AddCell(dataCell);

                                dataCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", examen.Costo), fontBold));
                                dataCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                dataCell.Padding = 8;
                                dataCell.BackgroundColor = colorFila;
                                dataCell.Border = Rectangle.NO_BORDER;
                                dataCell.BorderWidthBottom = 1;
                                dataCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
                                detallesTable.AddCell(dataCell);

                                filaAlternada = !filaAlternada;
                            }
                        }
                    }
                }

                documento.Add(detallesTable);

                // ========== TOTALES CON RECUADRO DESTACADO ==========
                PdfPTable totalesTable = new PdfPTable(2);
                totalesTable.WidthPercentage = 45;
                totalesTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalesTable.SpacingBefore = 15;
                totalesTable.SpacingAfter = 20;

                // Subtotal
                PdfPCell labelCell = new PdfPCell(new Phrase("Subtotal:", fontBold));
                labelCell.Border = Rectangle.NO_BORDER;
                labelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                labelCell.Padding = 5;
                totalesTable.AddCell(labelCell);

                PdfPCell valueCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", factura.Subtotal), fontNormal));
                valueCell.Border = Rectangle.NO_BORDER;
                valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                valueCell.Padding = 5;
                totalesTable.AddCell(valueCell);

                // Descuento
                labelCell = new PdfPCell(new Phrase("Descuento:", fontBold));
                labelCell.Border = Rectangle.NO_BORDER;
                labelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                labelCell.Padding = 5;
                totalesTable.AddCell(labelCell);

                valueCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", factura.Descuento), fontNormal));
                valueCell.Border = Rectangle.NO_BORDER;
                valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                valueCell.Padding = 5;
                totalesTable.AddCell(valueCell);

                // Impuesto
                labelCell = new PdfPCell(new Phrase("Impuesto:", fontBold));
                labelCell.Border = Rectangle.NO_BORDER;
                labelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                labelCell.Padding = 5;
                labelCell.PaddingBottom = 8;
                totalesTable.AddCell(labelCell);

                valueCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", factura.Impuesto), fontNormal));
                valueCell.Border = Rectangle.NO_BORDER;
                valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                valueCell.Padding = 5;
                valueCell.PaddingBottom = 8;
                totalesTable.AddCell(valueCell);

                // Total destacado
                PdfPCell totalLabelCell = new PdfPCell(new Phrase("TOTAL:", fontTotal));
                totalLabelCell.BackgroundColor = azulPrimario;
                totalLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalLabelCell.Padding = 10;
                totalLabelCell.Border = Rectangle.NO_BORDER;
                totalesTable.AddCell(totalLabelCell);

                PdfPCell totalValueCell = new PdfPCell(new Phrase(string.Format("L {0:#,##0.00}", factura.Total), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15, BaseColor.WHITE)));
                totalValueCell.BackgroundColor = azulPrimario;
                totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalValueCell.Padding = 10;
                totalValueCell.Border = Rectangle.NO_BORDER;
                totalesTable.AddCell(totalValueCell);

                documento.Add(totalesTable);

                // ========== PIE DE PÁGINA MODERNO ==========
                PdfPTable footerTable = new PdfPTable(1);
                footerTable.WidthPercentage = 100;
                footerTable.SpacingBefore = 25;

                PdfPCell footerCell = new PdfPCell();
                footerCell.BackgroundColor = grisClaro;
                footerCell.Border = Rectangle.NO_BORDER;
                footerCell.Padding = 15;
                footerCell.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph gracias = new Paragraph("¡Gracias por su preferencia!", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, azulOscuro));
                gracias.Alignment = Element.ALIGN_CENTER;
                gracias.SpacingAfter = 3;
                footerCell.AddElement(gracias);

                Paragraph electronico = new Paragraph("Este es un documento generado electrónicamente", FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.GRAY));
                electronico.Alignment = Element.ALIGN_CENTER;
                footerCell.AddElement(electronico);

                footerTable.AddCell(footerCell);
                documento.Add(footerTable);

                documento.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al generar factura PDF: {0}", ex.Message), ex);
            }
        }

        private string GenerarHTMLFactura(TFactura factura)
        {
            string html = @"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Factura " + factura.NumeroFactura + @"</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; }
        .header { text-align: center; border-bottom: 2px solid #333; padding-bottom: 20px; margin-bottom: 20px; }
        .header h1 { margin: 0; color: #2c3e50; }
        .header p { margin: 5px 0; color: #7f8c8d; }
        .info { margin: 20px 0; }
        .info-row { display: flex; justify-content: space-between; margin: 10px 0; }
        .info-label { font-weight: bold; color: #2c3e50; }
        table { width: 100%; border-collapse: collapse; margin: 20px 0; }
        th { background-color: #3498db; color: white; padding: 12px; text-align: left; }
        td { padding: 10px; border-bottom: 1px solid #ddd; }
        tr:hover { background-color: #f5f5f5; }
        .totals { margin-top: 20px; text-align: right; }
        .totals-row { margin: 5px 0; padding: 5px 0; }
        .total-final { font-size: 1.5em; font-weight: bold; color: #2c3e50; border-top: 2px solid #333; padding-top: 10px; }
        .footer { text-align: center; margin-top: 40px; padding-top: 20px; border-top: 1px solid #ddd; color: #7f8c8d; }
    </style>
</head>
<body>
    <div class='header'>
        <h1>MEDINOVA</h1>
        <p>Sistema de Gestión Médica</p>
        <p>Factura Electrónica</p>
    </div>

    <div class='info'>
        <div class='info-row'>
            <div><span class='info-label'>Factura N°:</span> " + factura.NumeroFactura + @"</div>
            <div><span class='info-label'>Fecha:</span> " + factura.Fecha.ToString("dd/MM/yyyy HH:mm") + @"</div>
        </div>
        <div class='info-row'>
            <div><span class='info-label'>Paciente:</span> " + (factura.Paciente?.NombreCompleto ?? "N/A") + @"</div>
            <div><span class='info-label'>DNI:</span> " + (factura.Paciente?.DNI ?? "N/A") + @"</div>
        </div>
        <div class='info-row'>
            <div><span class='info-label'>Método de Pago:</span> " + factura.MetodoPago + @"</div>
        </div>
    </div>

    <h3>Detalle de la Factura</h3>
    <table>
        <thead>
            <tr>
                <th>Descripción</th>
                <th style='text-align: center;'>Cantidad</th>
                <th style='text-align: right;'>Precio Unitario</th>
                <th style='text-align: right;'>Subtotal</th>
            </tr>
        </thead>
        <tbody>";

            // MEDICAMENTOS de la venta
            if (factura.Venta != null)
            {
                using (var nVenta = new NVenta())
                {
                    var detalles = nVenta.ObtenerDetallesVenta(factura.VentaId);
                    foreach (var detalle in detalles)
                    {
                        html += string.Format(@"
            <tr>
                <td>{0}</td>
                <td style='text-align: center;'>{1}</td>
                <td style='text-align: right;'>L {2:#,##0.00}</td>
                <td style='text-align: right;'>L {3:#,##0.00}</td>
            </tr>", detalle.Medicamento != null ? detalle.Medicamento.Nombre ?? "Medicamento" : "Medicamento", detalle.Cantidad, detalle.PrecioUnitario, detalle.Subtotal);
                    }
                }
            }

            // CONSULTAS de esta venta
            if (factura.PacienteId.HasValue)
            {
                using (var nConsulta = new CapaNegocio.Medico.NConsulta())
                {
                    var consultas = nConsulta.ObtenerConsultasPorPaciente(factura.PacienteId.Value)
                        .Where(c => c.VentaId == factura.VentaId)
                        .ToList();

                    foreach (var consulta in consultas)
                    {
                        html += string.Format(@"
            <tr>
                <td>Consulta Médica - {0}</td>
                <td style='text-align: center;'>1</td>
                <td style='text-align: right;'>L {1:#,##0.00}</td>
                <td style='text-align: right;'>L {1:#,##0.00}</td>
            </tr>", consulta.NumeroConsulta, consulta.CostoConsulta);
                    }
                }

                // EXÁMENES de esta venta
                using (var nExpediente = new CapaNegocio.Medico.NExpediente())
                using (var nExamen = new CapaNegocio.Medico.NExamen())
                {
                    var expediente = nExpediente.BuscarPorPacienteId(factura.PacienteId.Value);
                    if (expediente != null)
                    {
                        var examenes = nExamen.BuscarPorExpedienteId(expediente.PacienteId)
                            .Where(ex => ex.VentaId == factura.VentaId && ex.Estado != "Cancelado")
                            .ToList();

                        foreach (var examen in examenes)
                        {
                            html += string.Format(@"
            <tr>
                <td>Examen: {0} ({1})</td>
                <td style='text-align: center;'>1</td>
                <td style='text-align: right;'>L {2:#,##0.00}</td>
                <td style='text-align: right;'>L {2:#,##0.00}</td>
            </tr>", examen.Nombre, examen.Tipo, examen.Costo);
                        }
                    }
                }
            }

            html += @"
        </tbody>
    </table>

    <div class='totals'>
        <div class='totals-row'>
            <span class='info-label'>Subtotal:</span> L " + string.Format("{0:#,##0.00}", factura.Subtotal) + @"
        </div>
        <div class='totals-row'>
            <span class='info-label'>Descuento:</span> L " + string.Format("{0:#,##0.00}", factura.Descuento) + @"
        </div>
        <div class='totals-row'>
            <span class='info-label'>Impuesto:</span> L " + string.Format("{0:#,##0.00}", factura.Impuesto) + @"
        </div>
        <div class='totals-row total-final'>
            <span>TOTAL:</span> L " + string.Format("{0:#,##0.00}", factura.Total) + @"
        </div>
    </div>

    <div class='footer'>
        <p>Gracias por su preferencia</p>
        <p>Este es un documento generado electrónicamente</p>
    </div>
</body>
</html>";

            return html;
        }
    }
}
