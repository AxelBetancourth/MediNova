using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaNegocio.Compartido;
using CapaNegocio.Medico;
using CapaPresentacion.ModuloLogin;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class ConsultaMedica : Form
    {
        private TConsulta consulta;
        private TCita cita;
        private int? consultaId;
        private bool esNueva;
        private TExpediente expediente;
        private string formularioOrigen = "Calendario"; // Por defecto regresa al Calendario

        // Constructor para consulta nueva desde una cita
        public ConsultaMedica(TCita cita)
        {
            InitializeComponent();
            this.cita = cita;
            this.esNueva = true;
            ConfigurarFormulario();
            InicializarNuevaConsulta();
        }

        // Constructor para editar consulta existente
        public ConsultaMedica(int consultaId)
        {
            InitializeComponent();
            this.consultaId = consultaId;
            this.esNueva = false;
            ConfigurarFormulario();
            CargarConsultaExistente();
        }

        // Método para establecer el formulario de origen
        public void EstablecerFormularioOrigen(string origen)
        {
            formularioOrigen = origen;
        }

        private void ConfigurarFormulario()
        {
            // Configurar responsive
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1200, 800);
            this.MinimumSize = new System.Drawing.Size(1024, 768);

            // Configurar eventos de cálculo automático
            txtPeso.TextChanged += txtPeso_TextChanged;
            txtAltura.TextChanged += txtAltura_TextChanged;

            // Configurar ComboBoxes
            cboEstadoPago.Items.AddRange(new object[] { "Pendiente", "Pagado", "Cancelado" });
            cboEstadoPago.SelectedIndex = 0;

            // Deshabilitar IMC (solo lectura)
            txtIMC.ReadOnly = true;
            txtIMC.Enabled = false;

            // Agregar botón de Exámenes (si no existe ya en el Designer)
            if (this.Controls.Find("btnExamenes", true).Length == 0)
            {
                var btnExamenes = new Guna.UI2.WinForms.Guna2Button
                {
                    Name = "btnExamenes",
                    Text = "Exámenes",
                    Size = new System.Drawing.Size(120, 45),
                    Location = new System.Drawing.Point(20, 20),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                    BorderRadius = 6,
                    FillColor = System.Drawing.Color.FromArgb(0, 150, 136),
                    Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.White,
                    TabIndex = 0
                };
                btnExamenes.Click += btnExamenes_Click;

                // Buscar el panel footer para agregar el botón
                var pnlFooter = this.Controls.Find("pnlFooter", true).FirstOrDefault();
                if (pnlFooter != null)
                {
                    pnlFooter.Controls.Add(btnExamenes);
                }
            }
        }

        private void InicializarNuevaConsulta()
        {
            try
            {
                using (var nExp = new NExpediente())
                {
                    expediente = nExp.BuscarPorPacienteId(cita.PacienteId);

                    if (expediente == null)
                    {
                        // Crear expediente automáticamente para este paciente
                        var nuevoExpediente = new CapaDatos.BaseDatos.Tablas.ExpedienteClinico.TExpediente
                        {
                            PacienteId = cita.PacienteId,
                            FechaApertura = DateTime.Now,
                            NumeroExpediente = string.Format("EXP-{0}-{1}", DateTime.Now.Year, Guid.NewGuid().ToString().Substring(0, 6).ToUpper()),
                            Eliminado = false
                        };

                        nExp.RegistrarExpediente(nuevoExpediente);
                        expediente = nExp.BuscarPorPacienteId(cita.PacienteId);

                        if (expediente == null)
                        {
                            MessageBox.Show("Error al crear el expediente del paciente.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;
                        }
                    }

                    consulta = new TConsulta
                    {
                        ExpedienteId = expediente.PacienteId,
                        CitaId = cita.CitaId > 0 ? (int?)cita.CitaId : null, // Solo asignar si es válido
                        DoctorId = SesionUsuario.DoctorId,
                        FechaConsulta = DateTime.Now,
                        MotivoConsulta = cita.Asunto,
                        Estado = "EnProgreso",
                        EstadoPago = "Pendiente",
                        CostoConsulta = 0
                    };

                    // Cargar información del paciente
                    lblPaciente.Text = expediente.Paciente?.NombreCompleto ?? "N/A";
                    lblExpediente.Text = expediente.NumeroExpediente ?? "N/A";
                    txtMotivo.Text = cita.Asunto;

                    // Cargar enfermedades en el ComboBox
                    CargarEnfermedades();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al inicializar consulta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CargarConsultaExistente()
        {
            try
            {
                using (var nConsulta = new NConsulta())
                {
                    consulta = nConsulta.BuscarPorId(consultaId.Value);

                    if (consulta == null)
                    {
                        MessageBox.Show("Consulta no encontrada.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    expediente = consulta.Expediente;
                    cita = consulta.Cita;

                    CargarEnfermedades();
                    CargarDatosEnFormulario();

                    // Deshabilitar finalizar si ya está finalizada
                    if (consulta.Estado == "Finalizada")
                    {
                        btnFinalizar.Enabled = false;
                        btnFinalizar.Text = "Consulta Finalizada";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar consulta: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CargarEnfermedades()
        {
            try
            {
                using (var nEnfermedad = new NEnfermedad())
                {
                    var enfermedades = nEnfermedad.ListarEnfermedades();

                    cboEnfermedad.DisplayMember = "Nombre";
                    cboEnfermedad.ValueMember = "EnfermedadId";
                    cboEnfermedad.DataSource = enfermedades;
                    cboEnfermedad.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar enfermedades: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarDatosEnFormulario()
        {
            // Signos vitales
            txtPresion.Text = consulta.PresionArterial ?? "";
            txtTemperatura.Text = consulta.Temperatura?.ToString() ?? "";
            txtFrecuenciaCardiaca.Text = consulta.FrecuenciaCardiaca?.ToString() ?? "";
            txtFrecuenciaRespiratoria.Text = consulta.FrecuenciaRespiratoria?.ToString() ?? "";
            txtSaturacion.Text = consulta.Saturacion ?? "";
            txtPeso.Text = consulta.Peso?.ToString() ?? "";
            txtAltura.Text = consulta.Altura?.ToString() ?? "";
            txtIMC.Text = consulta.IMC?.ToString("F2") ?? "";

            // Consulta
            txtMotivo.Text = consulta.MotivoConsulta ?? "";
            txtSintomas.Text = consulta.Sintomas ?? "";
            txtObservaciones.Text = consulta.Observaciones ?? "";

            // Diagnóstico
            txtDiagnostico.Text = consulta.Diagnostico ?? "";
            if (consulta.EnfermedadId.HasValue)
            {
                cboEnfermedad.SelectedValue = consulta.EnfermedadId.Value;
            }

            // Tratamiento
            txtTratamiento.Text = consulta.Tratamiento ?? "";
            txtIndicaciones.Text = consulta.IndicacionesMedicas ?? "";
            txtCosto.Text = consulta.CostoConsulta.ToString("F2"); // Formato sin símbolo para edición
            cboEstadoPago.Text = consulta.EstadoPago ?? "Pendiente";

            // Información del paciente
            lblPaciente.Text = consulta.Expediente?.Paciente?.NombreCompleto ?? "N/A";
            lblExpediente.Text = consulta.Expediente?.NumeroExpediente ?? "N/A";
        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            CalcularIMC();
        }

        private void txtAltura_TextChanged(object sender, EventArgs e)
        {
            CalcularIMC();
        }

        private void CalcularIMC()
        {
            if (decimal.TryParse(txtPeso.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal peso) &&
                decimal.TryParse(txtAltura.Text.Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal altura) &&
                altura > 0)
            {
                decimal alturaMts = altura / 100;
                decimal imc = peso / (alturaMts * alturaMts);
                txtIMC.Text = imc.ToString("F2");

                // Colorear según clasificación
                if (imc < 18.5m)
                    lblIMCClasificacion.Text = "Bajo peso";
                else if (imc < 25m)
                    lblIMCClasificacion.Text = "Normal";
                else if (imc < 30m)
                    lblIMCClasificacion.Text = "Sobrepeso";
                else
                    lblIMCClasificacion.Text = "Obesidad";
            }
            else
            {
                txtIMC.Text = "";
                lblIMCClasificacion.Text = "";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatos())
                    return;

                GuardarDatos();

                MessageBox.Show("Consulta guardada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Obtener la excepción base (el error real más profundo)
                Exception baseEx = ex.GetBaseException();

                string errorMessage = string.Format("Error al guardar: {0}", ex.Message);
                errorMessage += string.Format("\n\n=== ERROR BASE ===\n{0}", baseEx.Message);

                // Agregar StackTrace para más información
                errorMessage += string.Format("\n\n=== DETALLES ===\n{0}", baseEx.StackTrace);

                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // También escribir en Debug para ver en la ventana de salida
                System.Diagnostics.Debug.WriteLine("=== ERROR COMPLETO ===");
                System.Diagnostics.Debug.WriteLine(string.Format("Message: {0}", baseEx.Message));
                System.Diagnostics.Debug.WriteLine(string.Format("StackTrace: {0}", baseEx.StackTrace));
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("El motivo de consulta es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 1; // Tab de consulta
                txtMotivo.Focus();
                return false;
            }

            if (decimal.TryParse(txtCosto.Text, out decimal costo) && costo < 0)
            {
                MessageBox.Show("El costo no puede ser negativo.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl.SelectedIndex = 3; // Tab de tratamiento
                txtCosto.Focus();
                return false;
            }

            return true;
        }

        private void GuardarDatos()
        {
            // Asignar valores del formulario a la consulta
            consulta.PresionArterial = txtPresion.Text.Trim();
            consulta.Temperatura = ParseDecimal(txtTemperatura.Text);
            consulta.FrecuenciaCardiaca = ParseDecimal(txtFrecuenciaCardiaca.Text);
            consulta.FrecuenciaRespiratoria = ParseDecimal(txtFrecuenciaRespiratoria.Text);
            consulta.Saturacion = txtSaturacion.Text.Trim();
            consulta.Peso = ParseDecimal(txtPeso.Text);
            consulta.Altura = ParseDecimal(txtAltura.Text);
            consulta.IMC = ParseDecimal(txtIMC.Text);

            consulta.MotivoConsulta = txtMotivo.Text.Trim();
            consulta.Sintomas = txtSintomas.Text.Trim();
            consulta.Observaciones = txtObservaciones.Text.Trim();

            consulta.Diagnostico = txtDiagnostico.Text.Trim();
            consulta.EnfermedadId = cboEnfermedad.SelectedValue != null ?
                (int?)cboEnfermedad.SelectedValue : null;

            consulta.Tratamiento = txtTratamiento.Text.Trim();
            consulta.IndicacionesMedicas = txtIndicaciones.Text.Trim();
            consulta.CostoConsulta = ParseDecimal(txtCosto.Text) ?? 0;
            consulta.EstadoPago = cboEstadoPago.Text;

            // Próxima cita se agenda desde el botón dedicado
            consulta.ProximaCita = null;
            consulta.NotasProximaCita = null;

            // IMPORTANTE: Limpiar propiedades de navegación antes de guardar
            // Esto evita que Entity Framework intente insertar/actualizar entidades relacionadas
            consulta.Expediente = null;
            consulta.Doctor = null;
            consulta.Cita = null;
            consulta.Enfermedad = null;
            consulta.Recetas = null;
            consulta.Diagnosticos = null;

            using (var nConsulta = new NConsulta())
            {
                if (esNueva)
                {
                    var resultado = nConsulta.RegistrarConsulta(consulta);
                    if (resultado > 0)
                    {
                        // Recargar la consulta para obtener el ID generado
                        consulta = nConsulta.BuscarPorId(consulta.ConsultaId);
                        esNueva = false;
                        consultaId = consulta.ConsultaId;
                    }
                }
                else
                {
                    nConsulta.EditarConsulta(consulta);
                }
            }
        }

        private decimal? ParseDecimal(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            if (decimal.TryParse(text.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal result))
                return result;

            return null;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que no haya exámenes sin resultados
                if (expediente != null)
                {
                    using (var nExamen = new NExamen())
                    {
                        var examenes = nExamen.BuscarPorExpedienteId(expediente.PacienteId);
                        var examenesSinResultado = examenes?
                            .Where(ex => string.IsNullOrWhiteSpace(ex.Resultado) && ex.Estado != "Cancelado")
                            .ToList();

                        if (examenesSinResultado != null && examenesSinResultado.Count > 0)
                        {
                            var mensaje = "No se puede finalizar la consulta porque hay exámenes sin resultados:\n\n";
                            foreach (var examen in examenesSinResultado)
                            {
                                mensaje += string.Format("• {0} ({1}) - Solicitado: {2:dd/MM/yyyy}\n", examen.Nombre, examen.Tipo, examen.FechaSolicitud);
                            }
                            mensaje += "\nPor favor, complete los resultados de los exámenes antes de finalizar.";

                            MessageBox.Show(mensaje,
                                "Validación - Exámenes Pendientes",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(txtDiagnostico.Text))
                {
                    MessageBox.Show("Debe ingresar un diagnóstico antes de finalizar la consulta.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl.SelectedIndex = 2; // Tab de diagnóstico
                    txtDiagnostico.Focus();
                    return;
                }

                var resultado = MessageBox.Show(
                    "¿Está seguro de finalizar esta consulta?\n\n" +
                    "Una vez finalizada no podrá editarse.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    GuardarDatos();

                    using (var nConsulta = new NConsulta())
                    {
                        nConsulta.FinalizarConsulta(consulta.ConsultaId);
                    }

                    MessageBox.Show("Consulta finalizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;

                    // Si está dentro del panel_main, regresar al formulario de origen
                    Panel panelMain = EncontrarPanelMain(this);
                    if (panelMain != null)
                    {
                        Form formularioDestino = null;

                        // Determinar a qué formulario regresar
                        if (formularioOrigen == "Consultas")
                        {
                            formularioDestino = new Consultas();
                        }
                        else // Por defecto "Calendario"
                        {
                            formularioDestino = new Calendario();
                        }

                        panelMain.Controls.Clear();
                        formularioDestino.TopLevel = false;
                        formularioDestino.FormBorderStyle = FormBorderStyle.None;
                        formularioDestino.Dock = DockStyle.Fill;
                        panelMain.Controls.Add(formularioDestino);
                        formularioDestino.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string mensajeError = string.Format("Error al finalizar la consulta:\n\n{0}", ex.Message);

                if (ex.InnerException != null)
                {
                    mensajeError += string.Format("\n\nDetalle: {0}", ex.InnerException.Message);
                }

                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgendarProximaCita_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya paciente y doctor
                if (expediente?.Paciente == null)
                {
                    MessageBox.Show("No hay un paciente asociado a esta consulta.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (SesionUsuario.DoctorId <= 0)
                {
                    MessageBox.Show("No se pudo obtener el ID del doctor actual.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Abrir el formulario de AgendarCita
                var formAgendarCita = new Recepcionista.AgendarCita();

                // Precargar paciente y doctor, con fecha sugerida (mañana a las 9 AM)
                DateTime fechaSugerida = DateTime.Now.AddDays(1).Date.AddHours(9);
                formAgendarCita.SetPacienteYDoctor(expediente.Paciente.PacienteId, SesionUsuario.DoctorId, fechaSugerida);

                // Mostrar el formulario como diálogo
                if (formAgendarCita.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Próxima cita agendada correctamente.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir formulario de agendar cita:\n\n{0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecetar_Click(object sender, EventArgs e)
        {
            try
            {
                // Guardar primero la consulta si es nueva o tiene cambios
                if (esNueva || consulta.ConsultaId == 0)
                {
                    GuardarDatos();
                }

                if (consulta.ConsultaId > 0)
                {
                    // Buscar el panel principal
                    Panel panelMain = EncontrarPanelMain(this);
                    if (panelMain != null)
                    {
                        // Abrir Prescripcion en el panel con navegación
                        var formPrescripcion = new Prescripcion(consulta.ConsultaId);

                        // Si Prescripcion tiene método para establecer origen, usarlo
                        var setOrigenMethod = formPrescripcion.GetType().GetMethod("EstablecerFormularioOrigen");
                        if (setOrigenMethod != null)
                        {
                            setOrigenMethod.Invoke(formPrescripcion, new object[] { this });
                        }

                        panelMain.Controls.Clear();
                        formPrescripcion.TopLevel = false;
                        formPrescripcion.FormBorderStyle = FormBorderStyle.None;
                        formPrescripcion.Dock = DockStyle.Fill;
                        panelMain.Controls.Add(formPrescripcion);
                        formPrescripcion.Show();
                    }
                    else
                    {
                        // Fallback: abrir como diálogo
                        var formPrescripcion = new Prescripcion(consulta.ConsultaId);
                        if (formPrescripcion.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show(
                                "Receta médica creada exitosamente.",
                                "Éxito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe guardar la consulta antes de crear una receta.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExamenes_Click(object sender, EventArgs e)
        {
            try
            {
                if (expediente == null || expediente.Paciente == null)
                {
                    MessageBox.Show("No hay expediente cargado para gestionar exámenes.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buscar el panel principal
                Panel panelMain = EncontrarPanelMain(this);
                if (panelMain != null)
                {
                    // Abrir Examenes en el panel con navegación, pasando la consulta
                    var formExamenes = new Examenes(expediente.Paciente, consulta);
                    formExamenes.EstablecerFormularioOrigen(this);

                    panelMain.Controls.Clear();
                    formExamenes.TopLevel = false;
                    formExamenes.FormBorderStyle = FormBorderStyle.None;
                    formExamenes.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(formExamenes);
                    formExamenes.Show();
                }
                else
                {
                    // Si no hay panel, abrir como diálogo (fallback)
                    using (var formExamenes = new Examenes(expediente.Paciente, consulta))
                    {
                        formExamenes.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir formulario de exámenes: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevaEnfermedad_Click(object sender, EventArgs e)
        {
            try
            {
                var formEnfermedad = new Enfermedad();
                if (formEnfermedad.ShowDialog() == DialogResult.OK)
                {
                    CargarEnfermedades();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro de cancelar?\n\nSe perderán los cambios no guardados.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;

                // Si está dentro del panel_main, regresar al formulario de origen
                Panel panelMain = EncontrarPanelMain(this);
                if (panelMain != null)
                {
                    Form formularioDestino = null;

                    // Determinar a qué formulario regresar
                    if (formularioOrigen == "Consultas")
                    {
                        formularioDestino = new Consultas();
                    }
                    else // Por defecto "Calendario"
                    {
                        formularioDestino = new Calendario();
                    }

                    panelMain.Controls.Clear();
                    formularioDestino.TopLevel = false;
                    formularioDestino.FormBorderStyle = FormBorderStyle.None;
                    formularioDestino.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(formularioDestino);
                    formularioDestino.Show();
                }
                else
                {
                    this.Close();
                }
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

        private void ConsultaMedica_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None && esNueva)
            {
                var resultado = MessageBox.Show(
                    "¿Desea salir sin guardar la consulta?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
