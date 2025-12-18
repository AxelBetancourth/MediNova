using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaNegocio.Recepcionista;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Recepcionista
{
    public partial class AgendarCita : Form
    {
        private int? pacienteIdSeleccionado;
        private int? citaIdActual; // Para edición
        private Dictionary<string, int> doctoresDictionary = new Dictionary<string, int>();
        private Label lblDisponibilidad; // Label para mostrar disponibilidad

        public AgendarCita()
        {
            InitializeComponent();
            ConfigurarFormulario();
            CrearLabelDisponibilidad();
            AjustarDiseñoTextos();
        }

        private void AjustarDiseñoTextos()
        {
            lblTitulo.TextAlignment = ContentAlignment.MiddleLeft;
        }

        private void CrearLabelDisponibilidad()
        {
            // Crear label dinámico para mostrar disponibilidad
            lblDisponibilidad = new Label();
            lblDisponibilidad.AutoSize = false;
            lblDisponibilidad.Size = new System.Drawing.Size(860, 35);
            lblDisponibilidad.Location = new System.Drawing.Point(30, 382);
            lblDisponibilidad.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblDisponibilidad.TextAlign = ContentAlignment.MiddleCenter;
            lblDisponibilidad.Visible = false;
            panelFormulario.Controls.Add(lblDisponibilidad);
        }

        private void ConfigurarFormulario()
        {
            // Configurar eventos
            btnCancelar.Click += btnCancelar_Click;
            btnRegresar.Click += btnRegresar_Click;
            btnBuscarPaciente.Click += BtnBuscarPaciente_Click;

            // Permitir escritura en el textbox y búsqueda al presionar Enter
            txtPaciente.KeyPress += TxtPaciente_KeyPress;

            // Configurar fecha/hora por defecto
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            dtpHoraInicio.Value = DateTime.Now;
            dtpHoraFin.Value = DateTime.Now.AddHours(1);

            // Evento para sincronizar fecha de fin cuando cambia fecha de inicio
            dtpFechaInicio.ValueChanged += (s, e) =>
            {
                // Actualizar fecha de fin para que siempre esté sincronizada con fecha de inicio
                dtpFechaFin.Value = dtpFechaInicio.Value;
                ValidarDisponibilidadDoctor();
            };

            // Evento para calcular automáticamente la hora fin
            dtpHoraInicio.ValueChanged += (s, e) =>
            {
                dtpHoraFin.Value = dtpHoraInicio.Value.AddHours(1);
                ValidarDisponibilidadDoctor();
            };

            // Eventos para validar disponibilidad en tiempo real
            cmbDoctor.SelectedIndexChanged += (s, e) => ValidarDisponibilidadDoctor();
            dtpHoraFin.ValueChanged += (s, e) => ValidarDisponibilidadDoctor();

            // Cargar doctores desde la base de datos
            CargarDoctores();
        }

        private async void ValidarDisponibilidadDoctor()
        {
            try
            {
                // No validar si el formulario no está listo
                if (!this.IsHandleCreated || this.Disposing || this.IsDisposed)
                    return;

                if (cmbDoctor.SelectedIndex == -1)
                {
                    if (lblDisponibilidad.InvokeRequired)
                        lblDisponibilidad.BeginInvoke(new Action(() => lblDisponibilidad.Visible = false));
                    else
                        lblDisponibilidad.Visible = false;
                    return;
                }

                int doctorId = doctoresDictionary[cmbDoctor.Text];

                // Construir fechas con validación para evitar crashes
                DateTime fechaHoraInicio, fechaHoraFin;
                try
                {
                    fechaHoraInicio = dtpFechaInicio.Value.Date.Add(dtpHoraInicio.Value.TimeOfDay);
                    fechaHoraFin = dtpFechaFin.Value.Date.Add(dtpHoraFin.Value.TimeOfDay);
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Si hay un error al construir las fechas, mostrar mensaje y salir
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            if (!this.IsDisposed)
                                MostrarDisponibilidad(false, "Fechas u horas inválidas");
                        }));
                    }
                    return;
                }

                // Validar que fecha de inicio sea menor que fecha de fin
                if (fechaHoraInicio >= fechaHoraFin)
                {
                    if (this.IsHandleCreated && !this.IsDisposed)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            if (!this.IsDisposed)
                                MostrarDisponibilidad(false, "La fecha/hora de inicio debe ser menor que la de fin");
                        }));
                    }
                    return;
                }

                // Validar en background para no bloquear UI
                await System.Threading.Tasks.Task.Run(() =>
                {
                    // Verificar nuevamente que el formulario sigue válido
                    if (this.IsDisposed || !this.IsHandleCreated)
                        return;

                    using (var nDoctor = new NDoctor())
                    using (var nCitas = new NCitas())
                    {
                        var doctor = nDoctor.BuscarPorId(doctorId);

                        if (doctor == null || !doctor.Disponible)
                        {
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    if (!this.IsDisposed)
                                        MostrarDisponibilidad(false, "Doctor no disponible");
                                }));
                            }
                            return;
                        }

                        // Verificar horario laboral
                        if (doctor.Horarios == null || !doctor.Horarios.Any())
                        {
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    if (!this.IsDisposed)
                                        MostrarDisponibilidad(false, "Doctor sin horarios definidos");
                                }));
                            }
                            return;
                        }

                        DayOfWeek diaDeCita = fechaHoraInicio.DayOfWeek;
                        TimeSpan horaInicioCita = fechaHoraInicio.TimeOfDay;
                        TimeSpan horaFinCita = fechaHoraFin.TimeOfDay;

                        var horarioDelDia = doctor.Horarios.FirstOrDefault(h => h.DiaSemana == diaDeCita);

                        if (horarioDelDia == null)
                        {
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    if (!this.IsDisposed)
                                    {
                                        string diaNombre = ObtenerNombreDia(diaDeCita);
                                        MostrarDisponibilidad(false, string.Format("Doctor no trabaja los {0}", diaNombre));
                                    }
                                }));
                            }
                            return;
                        }

                        bool dentroDeHorario = (horaInicioCita >= horarioDelDia.HoraInicio &&
                                                horaFinCita <= horarioDelDia.HoraFin);

                        if (!dentroDeHorario)
                        {
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    if (!this.IsDisposed)
                                        MostrarDisponibilidad(false,
                                            string.Format("Horario laboral: {0:hh\\:mm} - {1:hh\\:mm}", horarioDelDia.HoraInicio, horarioDelDia.HoraFin));
                                }));
                            }
                            return;
                        }

                        // Verificar conflictos de citas
                        var citasEnRango = nCitas.ObtenerCitasPorRango(fechaHoraInicio, fechaHoraFin)
                                                 .Where(c => c.CitaId != (citaIdActual ?? 0) && !c.Eliminado)
                                                 .ToList();

                        bool doctorOcupado = citasEnRango.Any(c => c.DoctorId == doctorId);

                        if (doctorOcupado)
                        {
                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    if (!this.IsDisposed)
                                        MostrarDisponibilidad(false, "Doctor tiene otra cita en ese horario");
                                }));
                            }
                            return;
                        }

                        // Todo está bien
                        if (this.IsHandleCreated && !this.IsDisposed)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                if (!this.IsDisposed)
                                    MostrarDisponibilidad(true, "✓ Doctor disponible en este horario");
                            }));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                // Silencioso, no mostrar errores de validación
                System.Diagnostics.Debug.WriteLine($"Error validando disponibilidad: {ex.Message}");
            }
        }

        private void MostrarDisponibilidad(bool disponible, string mensaje)
        {
            lblDisponibilidad.Visible = true;
            lblDisponibilidad.Text = mensaje;

            if (disponible)
            {
                lblDisponibilidad.BackColor = Color.FromArgb(46, 213, 115);
                lblDisponibilidad.ForeColor = Color.White;
            }
            else
            {
                lblDisponibilidad.BackColor = Color.FromArgb(255, 71, 87);
                lblDisponibilidad.ForeColor = Color.White;
            }
        }

        private string ObtenerNombreDia(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábados";
                case DayOfWeek.Sunday: return "Domingos";
                default: return dia.ToString();
            }
        }

        private void TxtPaciente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnBuscarPaciente_Click(sender, e);
            }
        }

        private void CargarDoctores()
        {
            try
            {
                cmbDoctor.Items.Clear();
                doctoresDictionary.Clear();

                using (var nDoctor = new NDoctor())
                {
                    var doctores = nDoctor.ListarDoctores()
                        .Where(d => !d.Eliminado && d.Disponible)
                        .OrderBy(d => d.NombreCompleto)
                        .ToList();

                    foreach (var doctor in doctores)
                    {
                        string displayText = $"{doctor.NombreCompleto} - {doctor.Especialidad}";
                        cmbDoctor.Items.Add(displayText);
                        doctoresDictionary.Add(displayText, doctor.DoctorId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar doctores: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                string terminoBusqueda = txtPaciente.Text.Trim();

                if (string.IsNullOrWhiteSpace(terminoBusqueda))
                {
                    MessageBox.Show("Por favor, ingrese un nombre o DNI para buscar.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPaciente.Focus();
                    return;
                }

                using (var nPaciente = new NPacientes())
                {
                    var pacientes = nPaciente.BuscarPorNombreODNI(terminoBusqueda);

                    if (pacientes.Count == 0)
                    {
                        MessageBox.Show("No se encontraron pacientes con ese criterio de búsqueda.",
                            "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (pacientes.Count == 1)
                    {
                        // Si solo hay uno, seleccionarlo automáticamente
                        SeleccionarPaciente(pacientes[0]);
                    }
                    else
                    {
                        // Mostrar lista para selección
                        MostrarListaPacientes(pacientes);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar paciente: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarListaPacientes(List<TPaciente> pacientes)
        {
            // Crear un formulario simple para seleccionar el paciente
            Form formSeleccion = new Form();
            formSeleccion.Text = "Seleccionar Paciente";
            formSeleccion.Size = new System.Drawing.Size(500, 400);
            formSeleccion.StartPosition = FormStartPosition.CenterParent;
            formSeleccion.FormBorderStyle = FormBorderStyle.FixedDialog;
            formSeleccion.MaximizeBox = false;
            formSeleccion.MinimizeBox = false;

            ListBox listBox = new ListBox();
            listBox.Dock = DockStyle.Fill;
            listBox.DisplayMember = "Display";
            listBox.Font = new System.Drawing.Font("Segoe UI", 10);

            foreach (var p in pacientes)
            {
                listBox.Items.Add(new
                {
                    Display = $"{p.NombreCompleto} - DNI: {p.DNI}",
                    Paciente = p
                });
            }

            listBox.DoubleClick += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    dynamic selected = listBox.SelectedItem;
                    SeleccionarPaciente(selected.Paciente);
                    formSeleccion.Close();
                }
            };

            Button btnSeleccionar = new Button();
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.Dock = DockStyle.Bottom;
            btnSeleccionar.Height = 40;
            btnSeleccionar.Click += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    dynamic selected = listBox.SelectedItem;
                    SeleccionarPaciente(selected.Paciente);
                    formSeleccion.Close();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un paciente.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            formSeleccion.Controls.Add(listBox);
            formSeleccion.Controls.Add(btnSeleccionar);
            formSeleccion.ShowDialog();
        }

        private void SeleccionarPaciente(TPaciente paciente)
        {
            pacienteIdSeleccionado = paciente.PacienteId;
            txtPaciente.Text = string.Format("{0} - DNI: {1}", paciente.NombreCompleto, paciente.DNI);
        }

        // 🔹 MÉTODO PARA RECIBIR PACIENTE Y DOCTOR DESDE CONSULTA MÉDICA
        public void SetPacienteYDoctor(int pacienteId, int doctorId, DateTime? fechaSugerida = null)
        {
            try
            {
                // Cargar datos del paciente
                using (var nPaciente = new NPacientes())
                {
                    var paciente = nPaciente.BuscarPorId(pacienteId);
                    if (paciente != null)
                    {
                        pacienteIdSeleccionado = paciente.PacienteId;
                        txtPaciente.Text = string.Format("{0} - DNI: {1}", paciente.NombreCompleto, paciente.DNI);
                        txtPaciente.Enabled = false; // Deshabilitar para que no se cambie
                        btnBuscarPaciente.Enabled = false;
                    }
                }

                // Seleccionar el doctor en el combo
                using (var nDoctor = new NDoctor())
                {
                    var doctor = nDoctor.BuscarPorId(doctorId);
                    if (doctor != null)
                    {
                        string doctorDisplay = string.Format("{0} - {1}", doctor.NombreCompleto, doctor.Especialidad);
                        int index = cmbDoctor.Items.IndexOf(doctorDisplay);
                        if (index >= 0)
                        {
                            cmbDoctor.SelectedIndex = index;
                            cmbDoctor.Enabled = false; // Deshabilitar para que no se cambie
                        }
                    }
                }

                // Si hay fecha sugerida, configurarla
                if (fechaSugerida.HasValue)
                {
                    dtpFechaInicio.Value = fechaSugerida.Value;
                    dtpFechaFin.Value = fechaSugerida.Value;
                    dtpHoraInicio.Value = fechaSugerida.Value;
                    dtpHoraFin.Value = fechaSugerida.Value.AddHours(1);
                }

                // Cambiar título para indicar que es desde consulta
                lblTitulo.Text = "Agendar Próxima Cita";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar datos: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 MÉTODO PARA RECIBIR DATOS DESDE HORARIO
        public void SetDatosIniciales(string fecha, string horaCompleta)
        {
            try
            {
                DateTime fechaParsed = default(DateTime);
                if (DateTime.TryParseExact(fecha, "d 'de' MMMM 'de' yyyy",
                    new CultureInfo("es-ES"), DateTimeStyles.None, out fechaParsed))
                {
                    dtpFechaInicio.Value = fechaParsed;
                    dtpFechaFin.Value = fechaParsed;
                }
                else if (DateTime.TryParse(fecha, out fechaParsed))
                {
                    dtpFechaInicio.Value = fechaParsed;
                    dtpFechaFin.Value = fechaParsed;
                }

                if (!string.IsNullOrEmpty(horaCompleta))
                {
                    string[] partes = horaCompleta.Split('-');
                    if (partes.Length == 2)
                    {
                        string horaInicio = partes[0].Trim();
                        string horaFin = partes[1].Trim();

                        DateTime horaInicioParsed = default(DateTime);
                        if (DateTime.TryParse(horaInicio, out horaInicioParsed))
                        {
                            dtpHoraInicio.Value = horaInicioParsed;
                        }

                        DateTime horaFinParsed = default(DateTime);
                        if (DateTime.TryParse(horaFin, out horaFinParsed))
                        {
                            dtpHoraFin.Value = horaFinParsed;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al configurar datos iniciales: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void CargarCita(int citaId)
        {
            try
            {
                citaIdActual = citaId;

                using (var nCitas = new NCitas())
                {
                    var cita = nCitas.ObtenerCitaPorId(citaId);

                    if (cita == null)
                    {
                        MessageBox.Show("No se encontró la cita seleccionada.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Cambiar título
                    lblTitulo.Text = "Editar Cita";

                    // Cargar datos del paciente
                    if (cita.Paciente != null)
                    {
                        pacienteIdSeleccionado = cita.PacienteId;
                        txtPaciente.Text = string.Format("{0} - DNI: {1}", cita.Paciente.NombreCompleto, cita.Paciente.DNI);
                    }

                    // Cargar doctor
                    if (cita.Doctor != null)
                    {
                        string doctorDisplay = string.Format("{0} - {1}", cita.Doctor.NombreCompleto, cita.Doctor.Especialidad);
                        int index = cmbDoctor.Items.IndexOf(doctorDisplay);
                        if (index >= 0)
                            cmbDoctor.SelectedIndex = index;
                    }

                    // Cargar datos de la cita
                    txtAsunto.Text = cita.Asunto;
                    dtpFechaInicio.Value = cita.FechaHoraInicio.Date;
                    dtpFechaFin.Value = cita.FechaHoraFin.Date;
                    dtpHoraInicio.Value = cita.FechaHoraInicio;
                    dtpHoraFin.Value = cita.FechaHoraFin;
                    chkTodoElDia.Checked = cita.TodoElDia;
                    txtUbicacion.Text = cita.Ubicacion != null ? cita.Ubicacion : "";
                    cmbEstado.Text = cita.Estado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar la cita: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                TCita cita = new TCita
                {
                    PacienteId = pacienteIdSeleccionado.Value,
                    DoctorId = doctoresDictionary[cmbDoctor.Text],
                    Asunto = txtAsunto.Text.Trim(),
                    FechaHoraInicio = dtpFechaInicio.Value.Date.Add(dtpHoraInicio.Value.TimeOfDay),
                    FechaHoraFin = dtpFechaFin.Value.Date.Add(dtpHoraFin.Value.TimeOfDay),
                    TodoElDia = chkTodoElDia.Checked,
                    Ubicacion = txtUbicacion.Text.Trim(),
                    Estado = cmbEstado.Text,
                    Eliminado = false
                };

                using (var nCitas = new NCitas())
                {
                    if (citaIdActual.HasValue)
                    {
                        cita.CitaId = citaIdActual.Value;
                        nCitas.GuardarCita(cita);
                        MessageBox.Show("Cita actualizada correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        nCitas.GuardarCita(cita);
                        MessageBox.Show("Cita guardada correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                RegresarAlCalendario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar la cita: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (!pacienteIdSeleccionado.HasValue)
            {
                MessageBox.Show("Por favor, seleccione un paciente.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaciente.Focus();
                return false;
            }

            if (cmbDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un doctor.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDoctor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAsunto.Text))
            {
                MessageBox.Show("Por favor, ingrese el asunto de la cita.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAsunto.Focus();
                return false;
            }

            // Validar que la fecha de inicio no sea mayor que la de fin
            DateTime fechaHoraInicio = dtpFechaInicio.Value.Date.Add(dtpHoraInicio.Value.TimeOfDay);
            DateTime fechaHoraFin = dtpFechaFin.Value.Date.Add(dtpHoraFin.Value.TimeOfDay);

            if (fechaHoraInicio >= fechaHoraFin)
            {
                MessageBox.Show("La fecha/hora de inicio debe ser menor que la fecha/hora de fin.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Si se está editando una cita, preguntar si desea cancelar la cita (cambiar estado)
            if (citaIdActual.HasValue)
            {
                var result = MessageBox.Show(
                    "¿Desea cambiar el estado de esta cita a 'Cancelada'?\n\n" +
                    "- Sí: Cancelar la cita (cambiar estado a 'Cancelada')\n" +
                    "- No: Salir sin cambios",
                    "Cancelar Cita",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (var nCitas = new NCitas())
                        {
                            nCitas.CancelarCita(citaIdActual.Value);
                            MessageBox.Show("La cita ha sido cancelada correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RegresarAlCalendario();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al cancelar la cita: {0}", ex.Message),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (result == DialogResult.No)
                {
                    RegresarAlCalendario();
                }
            }
            else
            {
                // Si es una nueva cita, solo preguntar si desea salir
                DialogResult result = MessageBox.Show(
                    "¿Está seguro que desea salir? Se perderán los datos ingresados.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    RegresarAlCalendario();
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
        }

        private void RegresarAlCalendario()
        {
            Panel panelMain = this.Parent as Panel;
            if (panelMain != null && panelMain.Name == "PanelMain")
            {
                var calendarioForm = new Calendario();
                panelMain.Controls.Clear();
                calendarioForm.TopLevel = false;
                calendarioForm.FormBorderStyle = FormBorderStyle.None;
                calendarioForm.Dock = DockStyle.Fill;
                panelMain.Controls.Add(calendarioForm);
                calendarioForm.Show();
            }
        }

        private void RegresarAlHorario()
        {
            Panel panelMain = this.Parent as Panel;
            if (panelMain != null && panelMain.Name == "PanelMain")
            {
                var horarioForm = new Horario();
                panelMain.Controls.Clear();
                horarioForm.TopLevel = false;
                horarioForm.FormBorderStyle = FormBorderStyle.None;
                horarioForm.Dock = DockStyle.Fill;
                panelMain.Controls.Add(horarioForm);
                horarioForm.Show();
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}