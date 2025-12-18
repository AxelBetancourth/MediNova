using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaNegocio.Recepcionista;
using CapaNegocio.Medico;
using CapaPresentacion.Recepcionista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class Calendario : Form
    {
        public static int _month, _year;
        private List<ucDays> dayControls = new List<ucDays>();
        private bool isAdjusting = false;
        private List<Control> weekDayLabels = new List<Control>();
        private Dictionary<string, List<TCita>> citasCache = new Dictionary<string, List<TCita>>();

        public Calendario()
        {
            InitializeComponent();

            // ✅ DoubleBuffered con reflexión (para el FlowLayoutPanel y MostrarCitasDia)
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, flowLayoutPanel1, new object[] { true });

            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, MostrarCitasDia, new object[] { true });

            // ✅ DoubleBuffered del formulario
            this.DoubleBuffered = true;

            // Configuración del FlowLayoutPanel
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.Padding = new Padding(10);

            // Guardar referencias a los labels de días de la semana
            weekDayLabels.Add(guna2HtmlLabel1); // Lunes
            weekDayLabels.Add(guna2HtmlLabel2); // Martes
            weekDayLabels.Add(guna2HtmlLabel3); // Miércoles
            weekDayLabels.Add(guna2HtmlLabel4); // Jueves
            weekDayLabels.Add(guna2HtmlLabel5); // Viernes
            weekDayLabels.Add(guna2HtmlLabel6); // Sábado
            weekDayLabels.Add(guna2HtmlLabel7); // Domingo

            // Evento para redimensionar
            this.Resize += Calendario_Resize;
            flowLayoutPanel1.SizeChanged += FlowLayoutPanel1_SizeChanged;
        }

        private async void Calendario_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            _month = today.Month;
            _year = today.Year;
            showDays(_month, _year);

            // Cargar automáticamente las citas del día actual de forma asíncrona
            await MostrarCitasDelDiaAsync(today.Day, _month, _year);

            // Seleccionar visualmente el día actual
            var diaActual = dayControls.FirstOrDefault(d => d.Text == today.Day.ToString());
            if (diaActual != null)
            {
                diaActual.Seleccionar();
            }
        }

        private void Calendario_Resize(object sender, EventArgs e)
        {
            AjustarTamanoDiasYLabels();
        }

        private void FlowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            AjustarTamanoDiasYLabels();
        }

        private void AjustarTamanoDiasYLabels()
        {
            if (isAdjusting) return;
            isAdjusting = true;

            try
            {
                if (flowLayoutPanel1.Width <= 0)
                    return;

                int padding = flowLayoutPanel1.Padding.Horizontal;
                int anchoDisponible = flowLayoutPanel1.ClientSize.Width - padding;

                // 🔹 AJUSTE: Restar el ancho de la scrollbar si está visible
                if (flowLayoutPanel1.VerticalScroll.Visible)
                {
                    anchoDisponible -= SystemInformation.VerticalScrollBarWidth;
                }

                int margen = 5;
                int anchoColumna = (anchoDisponible - (margen * 8)) / 7;
                int altoFila = (int)(anchoColumna * 0.9);

                if (anchoColumna < 50)
                {
                    anchoColumna = 50;
                    altoFila = 45;
                }

                // Ajustar labels de días de la semana
                for (int i = 0; i < weekDayLabels.Count; i++)
                {
                    weekDayLabels[i].Width = anchoColumna;
                    int xPos = padding + (i * (anchoColumna + margen));
                    weekDayLabels[i].Location = new Point(xPos, weekDayLabels[i].Location.Y);
                }

                // Ajustar días del calendario
                if (flowLayoutPanel1.Controls.Count > 0)
                {
                    foreach (Control control in flowLayoutPanel1.Controls)
                    {
                        if (control is ucDays)
                        {
                            control.Size = new Size(anchoColumna, altoFila);
                            control.Margin = new Padding(margen / 2);
                        }
                    }
                }
            }
            finally
            {
                isAdjusting = false;
            }
        }

        public void showDays(int month, int year)
        {
            flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            try
            {
                flowLayoutPanel1.Controls.Clear();
                dayControls.Clear();

                _month = month;
                _year = year;

                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                lbMonth.Text = monthName.ToUpper() + " " + year;

                DateTime startOfMonth = new DateTime(year, month, 1);
                int daysInMonth = DateTime.DaysInMonth(year, month);
                int startDayOfWeek = ((int)startOfMonth.DayOfWeek == 0) ? 7 : (int)startOfMonth.DayOfWeek;

                // Pre-calcular tamaño
                int padding = flowLayoutPanel1.Padding.Horizontal;
                int anchoDisponible = flowLayoutPanel1.ClientSize.Width - padding;

                // Considerar scrollbar
                if (flowLayoutPanel1.VerticalScroll.Visible)
                {
                    anchoDisponible -= SystemInformation.VerticalScrollBarWidth;
                }

                int margen = 5;
                int anchoColumna = Math.Max(50, (anchoDisponible - (margen * 8)) / 7);
                int altoFila = (int)(anchoColumna * 0.9);
                if (anchoColumna < 50) altoFila = 45;

                Size daySize = new Size(anchoColumna, altoFila);
                Padding dayMargin = new Padding(margen / 2);

                List<ucDays> newControls = new List<ucDays>();

                // Días vacíos al inicio
                for (int i = 1; i < startDayOfWeek; i++)
                {
                    ucDays empty = new ucDays("")
                    {
                        Size = daySize,
                        Margin = dayMargin
                    };
                    newControls.Add(empty);
                }

                // Días del mes
                DateTime today = DateTime.Now;
                for (int i = 1; i <= daysInMonth; i++)
                {
                    ucDays day = new ucDays(i.ToString())
                    {
                        Size = daySize,
                        Margin = dayMargin
                    };

                    if (i == today.Day && month == today.Month && year == today.Year)
                    {
                        day.IsToday = true;
                    }

                    day.DayClicked += Day_Clicked;
                    day.DayDoubleClicked += Day_DoubleClicked;

                    newControls.Add(day);
                    dayControls.Add(day);
                }

                flowLayoutPanel1.Controls.AddRange(newControls.ToArray());

                // Ajustar labels de días de la semana
                for (int i = 0; i < weekDayLabels.Count; i++)
                {
                    weekDayLabels[i].Width = anchoColumna;
                    int xPos = padding + (i * (anchoColumna + margen));
                    weekDayLabels[i].Location = new Point(xPos, weekDayLabels[i].Location.Y);
                }
            }
            finally
            {
                flowLayoutPanel1.ResumeLayout(true);
                this.ResumeLayout(true);
            }
        }

        private async void Day_Clicked(object sender, EventArgs e)
        {
            foreach (var d in dayControls)
            {
                d.Deseleccionar();
            }

            ucDays seleccionado = sender as ucDays;
            seleccionado?.Seleccionar();

            // Cargar y mostrar citas del día seleccionado de forma asíncrona
            if (seleccionado != null && !string.IsNullOrEmpty(seleccionado.Text))
            {
                int dia = int.Parse(seleccionado.Text);
                await MostrarCitasDelDiaAsync(dia, _month, _year);
            }
        }

        private async System.Threading.Tasks.Task MostrarCitasDelDiaAsync(int dia, int mes, int anio)
        {
            try
            {
                MostrarCitasDia.SuspendLayout();

                MostrarCitasDia.Controls.Clear();

                // Crear título
                Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
                lblTitulo.Text = string.Format("Citas del {0}/{1}/{2}", dia, mes, anio);
                lblTitulo.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblTitulo.Location = new Point(10, 10);
                lblTitulo.AutoSize = true;
                lblTitulo.ForeColor = Color.FromArgb(35, 38, 47);
                MostrarCitasDia.Controls.Add(lblTitulo);

                // Obtener citas del día (con caché)
                DateTime fechaInicio = new DateTime(anio, mes, dia, 0, 0, 0);
                DateTime fechaFin = fechaInicio.AddDays(1).AddSeconds(-1);
                string cacheKey = string.Format("{0}-{1:00}-{2:00}", anio, mes, dia);

                List<TCita> citasDelDia;

                // Cargar desde base de datos en segundo plano
                citasDelDia = await System.Threading.Tasks.Task.Run(() =>
                {
                    using (var nCitas = new NCitas())
                    {
                        // Filtrar solo las citas del médico en sesión
                        return nCitas.ObtenerCitasPorRango(fechaInicio, fechaFin)
                            .Where(c => !c.Eliminado &&
                                   ModuloLogin.SesionUsuario.TieneDoctorAsociado() &&
                                   c.DoctorId == ModuloLogin.SesionUsuario.DoctorId)
                            .OrderBy(c => c.FechaHoraInicio)
                            .ToList();
                    }
                });

                // Actualizar caché
                citasCache[cacheKey] = citasDelDia;

                if (citasDelDia.Count == 0)
                {
                    Label lblSinCitas = new Label();
                    lblSinCitas.Text = "No hay citas\nagendadas";
                    lblSinCitas.Font = new Font("Segoe UI", 9, FontStyle.Italic);
                    lblSinCitas.ForeColor = Color.Gray;
                    lblSinCitas.AutoSize = true;
                    lblSinCitas.Location = new Point(10, 50);
                    lblSinCitas.TextAlign = ContentAlignment.MiddleCenter;
                    MostrarCitasDia.Controls.Add(lblSinCitas);
                }
                else
                {
                    FlowLayoutPanel flowCitas = new FlowLayoutPanel();
                    flowCitas.SuspendLayout();
                    flowCitas.FlowDirection = FlowDirection.TopDown;
                    flowCitas.WrapContents = false;
                    flowCitas.AutoScroll = true;
                    flowCitas.Location = new Point(5, 40);
                    flowCitas.Size = new Size(MostrarCitasDia.Width - 10, MostrarCitasDia.Height - 45);
                    flowCitas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                    // Pre-crear todas las fuentes para evitar recrearlas en el loop
                    Font fontHora = new Font("Segoe UI", 9, FontStyle.Bold);
                    Font fontPaciente = new Font("Segoe UI", 8, FontStyle.Regular);
                    Font fontAsunto = new Font("Segoe UI", 7, FontStyle.Italic);
                    Font fontEstado = new Font("Segoe UI", 7, FontStyle.Bold);

                    List<Panel> paneles = new List<Panel>(citasDelDia.Count);

                    foreach (var cita in citasDelDia)
                    {
                        Panel panelCita = new Panel();
                        panelCita.SuspendLayout();
                        panelCita.Size = new Size(140, 80);
                        panelCita.BorderStyle = BorderStyle.FixedSingle;
                        panelCita.BackColor = Color.FromArgb(245, 248, 250);
                        panelCita.Margin = new Padding(2);
                        panelCita.Cursor = Cursors.Hand;
                        panelCita.Tag = cita.CitaId;

                        // Hora
                        Label lblHora = new Label();
                        lblHora.Text = cita.FechaHoraInicio.ToString("HH:mm");
                        lblHora.Font = fontHora;
                        lblHora.ForeColor = Color.FromArgb(94, 148, 255);
                        lblHora.AutoSize = true;
                        lblHora.Location = new Point(5, 5);

                        // Paciente
                        Label lblPaciente = new Label();
                        string nombrePaciente = cita.Paciente != null ? cita.Paciente.NombreCompleto : "Sin paciente";
                        lblPaciente.Text = nombrePaciente.Length > 20 ? nombrePaciente.Substring(0, 17) + "..." : nombrePaciente;
                        lblPaciente.Font = fontPaciente;
                        lblPaciente.ForeColor = Color.FromArgb(64, 64, 64);
                        lblPaciente.AutoSize = false;
                        lblPaciente.Size = new Size(130, 15);
                        lblPaciente.Location = new Point(5, 25);

                        // Asunto
                        Label lblAsunto = new Label();
                        string asunto = cita.Asunto;
                        lblAsunto.Text = asunto.Length > 22 ? asunto.Substring(0, 19) + "..." : asunto;
                        lblAsunto.Font = fontAsunto;
                        lblAsunto.ForeColor = Color.Gray;
                        lblAsunto.AutoSize = false;
                        lblAsunto.Size = new Size(130, 15);
                        lblAsunto.Location = new Point(5, 42);

                        // Estado
                        Label lblEstado = new Label();
                        lblEstado.Text = cita.Estado;
                        lblEstado.Font = fontEstado;
                        lblEstado.AutoSize = true;
                        lblEstado.Location = new Point(5, 60);

                        // Color según estado
                        switch (cita.Estado.ToLower())
                        {
                            case "pendiente":
                                lblEstado.ForeColor = Color.Orange;
                                break;
                            case "confirmada":
                                lblEstado.ForeColor = Color.Green;
                                break;
                            case "cancelada":
                                lblEstado.ForeColor = Color.Red;
                                break;
                            case "completada":
                                lblEstado.ForeColor = Color.Blue;
                                break;
                            default:
                                lblEstado.ForeColor = Color.Gray;
                                break;
                        }

                        // Agregar controles al panel
                        panelCita.Controls.Add(lblHora);
                        panelCita.Controls.Add(lblPaciente);
                        panelCita.Controls.Add(lblAsunto);
                        panelCita.Controls.Add(lblEstado);

                        // Crear menú contextual para la cita
                        int citaIdLocal = cita.CitaId;
                        int pacienteIdLocal = cita.PacienteId;
                        ContextMenuStrip menuCita = CrearMenuContextualCita(citaIdLocal, pacienteIdLocal);
                        panelCita.ContextMenuStrip = menuCita;

                        // Doble clic para abrir consulta
                        panelCita.DoubleClick += (s, e) => AbrirConsultaDesdeCita(citaIdLocal, pacienteIdLocal);

                        // Click simple para mostrar detalles (opcional)
                        panelCita.Click += (s, e) => { /* Puedes agregar algo aquí si lo deseas */ };

                        panelCita.ResumeLayout(false);
                        paneles.Add(panelCita);
                    }

                    // Agregar todos los paneles de una vez
                    flowCitas.Controls.AddRange(paneles.ToArray());
                    flowCitas.ResumeLayout(true);
                    MostrarCitasDia.Controls.Add(flowCitas);
                }

                MostrarCitasDia.ResumeLayout(true);
            }
            catch (Exception ex)
            {
                MostrarCitasDia.ResumeLayout(true);
                MessageBox.Show(string.Format("Error al cargar citas: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ContextMenuStrip CrearMenuContextualCita(int citaId, int pacienteId)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Font = new Font("Segoe UI", 9F);

            // Opción: Abrir Consulta
            ToolStripMenuItem itemConsulta = new ToolStripMenuItem("📋 Abrir Consulta");
            itemConsulta.Click += (s, e) => AbrirConsultaDesdeCita(citaId, pacienteId);
            menu.Items.Add(itemConsulta);

            menu.Items.Add(new ToolStripSeparator());

            // Opción: Marcar como Completada
            ToolStripMenuItem itemCompletada = new ToolStripMenuItem("✅ Marcar como Completada");
            itemCompletada.Click += (s, e) => CambiarEstadoCita(citaId, "Completada");
            menu.Items.Add(itemCompletada);

            // Opción: Marcar como Pendiente
            ToolStripMenuItem itemPendiente = new ToolStripMenuItem("🕐 Marcar como Pendiente");
            itemPendiente.Click += (s, e) => CambiarEstadoCita(citaId, "Pendiente");
            menu.Items.Add(itemPendiente);

            // Opción: Cancelar Cita
            ToolStripMenuItem itemCancelar = new ToolStripMenuItem("❌ Cancelar Cita");
            itemCancelar.Click += (s, e) => CambiarEstadoCita(citaId, "Cancelada");
            menu.Items.Add(itemCancelar);

            menu.Items.Add(new ToolStripSeparator());

            // Opción: Editar Cita
            ToolStripMenuItem itemEditar = new ToolStripMenuItem("✏️ Editar Cita");
            itemEditar.Click += (s, e) => AbrirEditarCita(citaId);
            menu.Items.Add(itemEditar);

            return menu;
        }

        private void AbrirConsultaDesdeCita(int citaId, int pacienteId)
        {
            try
            {
                // Verificar si el expediente existe, si no, crearlo automáticamente
                using (var nExp = new NExpediente())
                {
                    var expediente = nExp.BuscarPorPacienteId(pacienteId);

                    if (expediente == null)
                    {
                        // Crear expediente automáticamente para este paciente
                        var nuevoExpediente = new CapaDatos.BaseDatos.Tablas.ExpedienteClinico.TExpediente
                        {
                            PacienteId = pacienteId,
                            FechaApertura = DateTime.Now,
                            NumeroExpediente = string.Format("EXP-{0}-{1}", DateTime.Now.Year, Guid.NewGuid().ToString().Substring(0, 6).ToUpper()),
                            Eliminado = false
                        };

                        nExp.RegistrarExpediente(nuevoExpediente);
                        expediente = nExp.BuscarPorPacienteId(pacienteId);

                        if (expediente == null)
                        {
                            MessageBox.Show("Error al crear el expediente del paciente.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Verificar campos obligatorios del expediente
                    bool expedienteCompleto = !string.IsNullOrWhiteSpace(expediente.TipoSangre) &&
                                               !string.IsNullOrWhiteSpace(expediente.ContactoEmergencia) &&
                                               !string.IsNullOrWhiteSpace(expediente.TelefonoEmergencia);

                    if (!expedienteCompleto)
                    {
                        var resultado = MessageBox.Show(
                            "El expediente del paciente está incompleto.\n\n" +
                            "¿Desea completarlo antes de crear la consulta?",
                            "Expediente Incompleto",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            var formCompletarExp = new CompletarExpediente(pacienteId);
                            if (formCompletarExp.ShowDialog() != DialogResult.OK)
                            {
                                return; // Usuario canceló la edición del expediente
                            }
                        }
                        else
                        {
                            return; // Usuario decidió no completar el expediente
                        }
                    }

                    // Obtener la cita para pasarla a ConsultaMedica
                    TCita cita;
                    using (var nCitas = new NCitas())
                    {
                        cita = nCitas.ObtenerCitaPorId(citaId);
                    }

                    if (cita == null)
                    {
                        MessageBox.Show("No se encontró la cita.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verificar si ya existe una consulta para esta cita
                    int? consultaId = null;
                    using (var nConsulta = new CapaNegocio.Medico.NConsulta())
                    {
                        var consultaExistente = nConsulta.ObtenerConsultaPorCita(cita.CitaId);
                        if (consultaExistente != null)
                        {
                            consultaId = consultaExistente.ConsultaId;
                        }
                    }

                    // Buscar el panel principal y navegar a ConsultaMedica
                    Panel panelMain = this.Parent as Panel;
                    if (panelMain != null && panelMain.Name == "panel_main")
                    {
                        // Si existe una consulta, abrirla; sino, crear una nueva
                        ConsultaMedica formConsulta = consultaId.HasValue
                            ? new ConsultaMedica(consultaId.Value)
                            : new ConsultaMedica(cita);

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
                        ConsultaMedica formConsulta = consultaId.HasValue
                            ? new ConsultaMedica(consultaId.Value)
                            : new ConsultaMedica(cita);

                        formConsulta.ShowDialog();

                        // Recargar las citas para mostrar cambios
                        var seleccionado = dayControls.FirstOrDefault(d => d.BackColor != Color.White);
                        if (seleccionado != null && !string.IsNullOrEmpty(seleccionado.Text))
                        {
                            int dia = int.Parse(seleccionado.Text);
                            _ = MostrarCitasDelDiaAsync(dia, _month, _year);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir consulta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CambiarEstadoCita(int citaId, string nuevoEstado)
        {
            try
            {
                var resultado = MessageBox.Show(
                    string.Format("¿Está seguro de marcar esta cita como '{0}'?", nuevoEstado),
                    "Confirmar cambio de estado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    using (var nCitas = new NCitas())
                    {
                        var cita = nCitas.ObtenerCitaPorId(citaId);
                        if (cita != null)
                        {
                            cita.Estado = nuevoEstado;
                            nCitas.GuardarCita(cita);

                            MessageBox.Show(string.Format("Cita marcada como '{0}'.", nuevoEstado),
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Recargar las citas del día actual
                            var seleccionado = dayControls.FirstOrDefault(d => d.BackColor != Color.White);
                            if (seleccionado != null && !string.IsNullOrEmpty(seleccionado.Text))
                            {
                                int dia = int.Parse(seleccionado.Text);
                                _ = MostrarCitasDelDiaAsync(dia, _month, _year);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cambiar estado: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirEditarCita(int citaId)
        {
            Panel panelMain = this.Parent as Panel;
            if (panelMain != null && panelMain.Name == "PanelMain")
            {
                var agendarForm = new AgendarCita();
                agendarForm.CargarCita(citaId);

                panelMain.Controls.Clear();
                agendarForm.TopLevel = false;
                agendarForm.FormBorderStyle = FormBorderStyle.None;
                agendarForm.Dock = DockStyle.Fill;
                panelMain.Controls.Add(agendarForm);
                agendarForm.Show();
            }
        }

        private void btnPrev_Click_1(object sender, EventArgs e)
        {
            _month--;
            if (_month < 1)
            {
                _month = 12;
                _year--;
            }
            showDays(_month, _year);
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            _month++;
            if (_month > 12)
            {
                _month = 1;
                _year++;
            }
            showDays(_month, _year);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lbMonth_Click(object sender, EventArgs e)
        {
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Day_DoubleClicked(string dayNumber)
        {
            Horario frm = new Horario();
            frm.SetDiaSeleccionado(string.Format("{0}/{1}/{2}", dayNumber, _month, _year));

            Panel panelMain = this.Parent as Panel;
            if (panelMain != null && panelMain.Name == "PanelMain")
            {
                panelMain.Controls.Clear();
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                panelMain.Controls.Add(frm);
                frm.Show();
            }
        }
    }
}
