using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaNegocio.Medico;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class CompletarExpediente : Form
    {
        private TExpediente expediente;
        private int pacienteId;
        private bool datosCompletos;

        public CompletarExpediente(int pacienteId)
        {
            InitializeComponent();
            this.pacienteId = pacienteId;
            ConfigurarFormulario();
            CargarExpediente();
        }

        public bool DatosCompletos => datosCompletos;

        private void ConfigurarFormulario()
        {
            // Configurar responsive
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1100, 750);
            this.MinimumSize = new System.Drawing.Size(1024, 768);

            // Configurar ComboBox Tipo de Sangre
            cboTipoSangre.Items.AddRange(new object[] {
                "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
            });
        }

        private void CargarExpediente()
        {
            try
            {
                using (var nExp = new NExpediente())
                {
                    expediente = nExp.BuscarPorPacienteId(pacienteId);

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
                            this.Close();
                            return;
                        }
                    }

                    // Cargar información del paciente (solo lectura)
                    lblPaciente.Text = expediente.Paciente != null ? expediente.Paciente.NombreCompleto : "N/A";
                    lblExpediente.Text = expediente.NumeroExpediente != null ? expediente.NumeroExpediente : "N/A";
                    lblDNI.Text = expediente.Paciente != null ? expediente.Paciente.DNI : "N/A";
                    lblEdad.Text = expediente.Paciente != null && expediente.Paciente.FechaNacimiento != null
                        ? string.Format("{0} años", CalcularEdad(expediente.Paciente.FechaNacimiento))
                        : "N/A";
                    lblSexo.Text = expediente.Paciente != null ? expediente.Paciente.Sexo : "N/A";
                    lblTelefono.Text = expediente.Paciente != null ? expediente.Paciente.Telefono : "N/A";
                    lblDireccion.Text = expediente.Paciente != null ? expediente.Paciente.Direccion : "N/A";

                    // Cargar datos del expediente (editables)
                    txtAlergias.Text = expediente.Alergias != null ? expediente.Alergias : "";
                    txtAntecedentesFamiliares.Text = expediente.AntecedentesFamiliares != null ? expediente.AntecedentesFamiliares : "";
                    txtAntecedentesPersonales.Text = expediente.AntecedentesPersonales != null ? expediente.AntecedentesPersonales : "";
                    cboTipoSangre.Text = expediente.TipoSangre != null ? expediente.TipoSangre : "";
                    txtNotasGenerales.Text = expediente.NotasGenerales != null ? expediente.NotasGenerales : "";
                    txtContactoEmergencia.Text = expediente.ContactoEmergencia != null ? expediente.ContactoEmergencia : "";
                    txtTelefonoEmergencia.Text = expediente.TelefonoEmergencia != null ? expediente.TelefonoEmergencia : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar expediente: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(cboTipoSangre.Text))
            {
                MessageBox.Show("El tipo de sangre es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTipoSangre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContactoEmergencia.Text))
            {
                MessageBox.Show("El contacto de emergencia es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactoEmergencia.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefonoEmergencia.Text))
            {
                MessageBox.Show("El teléfono de emergencia es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefonoEmergencia.Focus();
                return false;
            }

            return true;
        }

        private void GuardarDatos()
        {
            try
            {
                if (!ValidarDatos())
                    return;

                expediente.Alergias = txtAlergias.Text.Trim();
                expediente.AntecedentesFamiliares = txtAntecedentesFamiliares.Text.Trim();
                expediente.AntecedentesPersonales = txtAntecedentesPersonales.Text.Trim();
                expediente.TipoSangre = cboTipoSangre.Text.Trim();
                expediente.NotasGenerales = txtNotasGenerales.Text.Trim();
                expediente.ContactoEmergencia = txtContactoEmergencia.Text.Trim();
                expediente.TelefonoEmergencia = txtTelefonoEmergencia.Text.Trim();

                using (var nExp = new NExpediente())
                {
                    nExp.EditarExpediente(expediente);
                }

                datosCompletos = true;

                MessageBox.Show("Expediente completado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;

                // Solo cerrar si NO está en panel_main (modo diálogo)
                Panel panelMain = EncontrarPanelMain(this);
                if (panelMain == null)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();

            // Si está dentro del panel_main, regresar a Consultas
            Panel panelMain = EncontrarPanelMain(this);
            if (panelMain != null && datosCompletos)
            {
                var consultasForm = new Consultas();
                panelMain.Controls.Clear();
                consultasForm.TopLevel = false;
                consultasForm.FormBorderStyle = FormBorderStyle.None;
                consultasForm.Dock = DockStyle.Fill;
                panelMain.Controls.Add(consultasForm);
                consultasForm.Show();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro de cancelar?\n\nSe perderán los cambios no guardados.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                datosCompletos = false;
                this.DialogResult = DialogResult.Cancel;

                // Si está dentro del panel_main, regresar a Consultas
                Panel panelMain = EncontrarPanelMain(this);
                if (panelMain != null)
                {
                    var consultasForm = new Consultas();
                    panelMain.Controls.Clear();
                    consultasForm.TopLevel = false;
                    consultasForm.FormBorderStyle = FormBorderStyle.None;
                    consultasForm.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(consultasForm);
                    consultasForm.Show();
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

        private void CompletarExpediente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                var resultado = MessageBox.Show(
                    "¿Desea salir sin completar el expediente?\n\n" +
                    "No podrá crear consultas hasta que complete la información del expediente.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
