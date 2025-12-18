using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaNegocio.Farmacia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace CapaPresentacion.Medico
{
    public partial class HistorialRecetas : Form
    {
        private int pacienteId;
        private int consultaActualId;
        private NReceta nReceta;
        private List<TReceta> recetasPaciente;

        public event EventHandler RecetaEditada;
        public TReceta RecetaSeleccionada { get; private set; }

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

        public HistorialRecetas(int pacienteId, int consultaActualId)
        {
            InitializeComponent();
            this.pacienteId = pacienteId;
            this.consultaActualId = consultaActualId;
            nReceta = new NReceta();
            ConfigurarDataGridView();
            CargarHistorialRecetas();

            // Aplica el redondeo en el constructor
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); // Radio de 20
        }

        private void ConfigurarDataGridView()
        {
            dgvRecetas.Columns.Clear();

            dgvRecetas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumeroReceta",
                HeaderText = "Nº Receta",
                DataPropertyName = "NumeroReceta",
                Width = 150
            });

            dgvRecetas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaEmision",
                HeaderText = "Fecha Emisión",
                DataPropertyName = "FechaEmisionStr",
                Width = 130
            });

            dgvRecetas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 120
            });

            dgvRecetas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Doctor",
                HeaderText = "Doctor",
                DataPropertyName = "DoctorNombre",
                Width = 250
            });

            var btnVerDetalle = new DataGridViewButtonColumn
            {
                Name = "VerDetalle",
                HeaderText = "Seleccionar",
                Text = "Seleccionar",
                UseColumnTextForButtonValue = true,
                Width = 120
            };
            dgvRecetas.Columns.Add(btnVerDetalle);

            var btnImprimir = new DataGridViewButtonColumn
            {
                Name = "Imprimir",
                HeaderText = "Imprimir",
                Text = "📄 TXT",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            dgvRecetas.Columns.Add(btnImprimir);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarHistorialRecetas()
        {
            try
            {
                // Obtener todas las recetas del paciente
                recetasPaciente = nReceta.BuscarPorPaciente(pacienteId)
                    .OrderByDescending(r => r.FechaEmision)
                    .ToList();

                // Crear lista de objetos anónimos para el binding
                var recetasParaMostrar = recetasPaciente.Select(r => new
                {
                    r.NumeroReceta,
                    FechaEmisionStr = r.FechaEmision.ToString("dd/MM/yyyy"),
                    r.Estado,
                    DoctorNombre = r.Doctor != null ? r.Doctor.NombreCompleto : "N/A"
                }).ToList();

                if (dgvRecetas != null)
                {
                    dgvRecetas.DataSource = recetasParaMostrar;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar historial de recetas: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvRecetas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0) return;

            // Obtener el estado de la fila
            var estadoCell = dgv.Rows[e.RowIndex].Cells["Estado"];
            if (estadoCell.Value != null)
            {
                string estado = estadoCell.Value.ToString();

                // Colorear según el estado
                switch (estado.ToLower())
                {
                    case "pendiente":
                        estadoCell.Style.ForeColor = Color.Orange;
                        estadoCell.Style.Font = new Font(dgv.Font, FontStyle.Bold);
                        break;
                    case "surtida":
                        estadoCell.Style.ForeColor = Color.Green;
                        estadoCell.Style.Font = new Font(dgv.Font, FontStyle.Bold);
                        break;
                    case "parcial":
                        estadoCell.Style.ForeColor = Color.Blue;
                        estadoCell.Style.Font = new Font(dgv.Font, FontStyle.Bold);
                        break;
                    case "vencida":
                    case "cancelada":
                        estadoCell.Style.ForeColor = Color.Red;
                        estadoCell.Style.Font = new Font(dgv.Font, FontStyle.Bold);
                        break;
                }
            }
        }

        private void DgvRecetas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var dgv = sender as DataGridView;
            if (dgv == null) return;

            // Verificar si se hizo clic en el botón "Seleccionar"
            if (e.ColumnIndex == dgv.Columns["VerDetalle"].Index)
            {
                // Obtener la receta seleccionada
                SeleccionarReceta(e.RowIndex);
            }
            // Verificar si se hizo clic en el botón "Imprimir"
            else if (e.ColumnIndex == dgv.Columns["Imprimir"].Index)
            {
                ImprimirRecetaTXT(e.RowIndex);
            }
        }

        private void DgvRecetas_DoubleClick(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null || dgv.CurrentRow == null) return;

            // Seleccionar la receta al hacer doble clic en cualquier parte de la fila
            SeleccionarReceta(dgv.CurrentRow.Index);
        }

        private void SeleccionarReceta(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= recetasPaciente.Count) return;

                // Obtener la receta seleccionada
                var receta = recetasPaciente[rowIndex];

                // Preguntar al usuario qué desea hacer
                var mensaje = receta.Estado == "Pendiente" || receta.Estado == "Parcial"
                    ? string.Format("¿Desea cargar esta receta para editarla?\n\nReceta: {0}\nEstado: {1}\nDiagnóstico: {2}", receta.NumeroReceta, receta.Estado, receta.Diagnostico)
                    : string.Format("Esta receta ya fue {0}.\n\nReceta: {1}\nDiagnóstico: {2}\n\n¿Desea ver los detalles?", receta.Estado.ToLower(), receta.NumeroReceta, receta.Diagnostico);

                var titulo = receta.Estado == "Pendiente" || receta.Estado == "Parcial"
                    ? "Cargar Receta"
                    : "Ver Receta";

                var resultado = MessageBox.Show(mensaje, titulo,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    if (receta.Estado == "Pendiente" || receta.Estado == "Parcial")
                    {
                        // Cargar receta para editar
                        RecetaSeleccionada = receta;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        // Solo mostrar detalles (por implementar formulario de solo lectura)
                        MessageBox.Show(string.Format("Receta {0}\n\nEstado: {1}\nDiagnóstico: {2}\n\nNota: Esta receta ya fue procesada y no se puede editar.", receta.NumeroReceta, receta.Estado, receta.Diagnostico),
                            "Información de Receta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al seleccionar receta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirRecetaTXT(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= recetasPaciente.Count) return;

                var receta = recetasPaciente[rowIndex];

                // Obtener los detalles de la receta con medicamentos
                using (var nRec = new NReceta())
                {
                    var recetaCompleta = nRec.BuscarPorId(receta.RecetaId);

                    if (recetaCompleta == null)
                    {
                        MessageBox.Show("No se pudo cargar la información de la receta.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Crear contenido del archivo TXT
                    var sb = new StringBuilder();
                    sb.AppendLine("═══════════════════════════════════════════════════════════════");
                    sb.AppendLine("                      RECETA MÉDICA");
                    sb.AppendLine("                        MediNova");
                    sb.AppendLine("═══════════════════════════════════════════════════════════════");
                    sb.AppendLine();
                    sb.AppendLine(string.Format("Número de Receta: {0}", recetaCompleta.NumeroReceta));
                    sb.AppendLine(string.Format("Fecha de Emisión: {0:dd/MM/yyyy HH:mm}", recetaCompleta.FechaEmision));
                    if (recetaCompleta.FechaVencimiento.HasValue)
                        sb.AppendLine(string.Format("Fecha de Vencimiento: {0:dd/MM/yyyy}", recetaCompleta.FechaVencimiento));
                    sb.AppendLine();
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine("INFORMACIÓN DEL PACIENTE");
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine(string.Format("Nombre: {0}", recetaCompleta.Paciente != null ? recetaCompleta.Paciente.NombreCompleto : "N/A"));
                    if (recetaCompleta.Paciente != null && !string.IsNullOrEmpty(recetaCompleta.Paciente.DNI))
                        sb.AppendLine(string.Format("DNI: {0}", recetaCompleta.Paciente.DNI));
                    sb.AppendLine();
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine("INFORMACIÓN DEL MÉDICO");
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine(string.Format("Doctor: {0}", recetaCompleta.Doctor != null ? recetaCompleta.Doctor.NombreCompleto : "N/A"));
                    if (recetaCompleta.Doctor != null && !string.IsNullOrEmpty(recetaCompleta.Doctor.Especialidad))
                        sb.AppendLine(string.Format("Especialidad: {0}", recetaCompleta.Doctor.Especialidad));
                    sb.AppendLine();
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine("DIAGNÓSTICO");
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine(recetaCompleta.Diagnostico != null ? recetaCompleta.Diagnostico : "No especificado");
                    sb.AppendLine();
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine("MEDICAMENTOS PRESCRITOS");
                    sb.AppendLine("───────────────────────────────────────────────────────────────");
                    sb.AppendLine();

                    var detalles = nRec.ObtenerDetallesReceta(receta.RecetaId);
                    if (detalles != null && detalles.Count > 0)
                    {
                        int contador = 1;
                        foreach (var detalle in detalles)
                        {
                            // Mostrar nombre del medicamento (del inventario o externo)
                            string nombreMedicamento = detalle.MedicamentoId.HasValue
                                ? (detalle.Medicamento != null ? detalle.Medicamento.Nombre : "Medicamento no encontrado")
                                : (detalle.NombreMedicamentoExterno != null ? detalle.NombreMedicamentoExterno : "Medicamento externo");

                            sb.AppendLine(string.Format("{0}. {1}", contador, nombreMedicamento));
                            sb.AppendLine(string.Format("   Dosis: {0}", detalle.Dosis));
                            sb.AppendLine(string.Format("   Cantidad: {0}", detalle.CantidadPrescrita));
                            sb.AppendLine(string.Format("   Duración: {0} días", detalle.DuracionDias));
                            if (!string.IsNullOrEmpty(detalle.Indicaciones))
                                sb.AppendLine(string.Format("   Indicaciones: {0}", detalle.Indicaciones));
                            sb.AppendLine();
                            contador++;
                        }
                    }
                    else
                    {
                        sb.AppendLine("No hay medicamentos en esta receta.");
                        sb.AppendLine();
                    }

                    if (!string.IsNullOrWhiteSpace(recetaCompleta.IndicacionesGenerales))
                    {
                        sb.AppendLine("───────────────────────────────────────────────────────────────");
                        sb.AppendLine("INDICACIONES GENERALES");
                        sb.AppendLine("───────────────────────────────────────────────────────────────");
                        sb.AppendLine(recetaCompleta.IndicacionesGenerales);
                        sb.AppendLine();
                    }

                    sb.AppendLine("═══════════════════════════════════════════════════════════════");
                    sb.AppendLine(string.Format("Generado: {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now));
                    sb.AppendLine("═══════════════════════════════════════════════════════════════");

                    // Guardar archivo
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Archivo de texto (*.txt)|*.txt";
                        sfd.FileName = string.Format("Receta_{0}_{1:yyyyMMdd_HHmmss}.txt", recetaCompleta.NumeroReceta.Replace("-", "_"), DateTime.Now);
                        sfd.Title = "Guardar Receta";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                            MessageBox.Show(string.Format("Receta guardada exitosamente en:\n{0}", sfd.FileName),
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Preguntar si desea abrir el archivo
                            var resultado = MessageBox.Show("¿Desea abrir el archivo ahora?",
                                "Abrir Archivo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resultado == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start("notepad.exe", sfd.FileName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al generar archivo TXT: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRecetas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void HistorialRecetas_Load(object sender, EventArgs e)
        {

        }
    }
}
