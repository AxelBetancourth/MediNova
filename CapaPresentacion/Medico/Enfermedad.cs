using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Compartido;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class Enfermedad : Form
    {
        private TEnfermedad enfermedad;
        private int? enfermedadId;
        private bool esNueva;

        // Constructor para nueva enfermedad
        public Enfermedad()
        {
            InitializeComponent();
            this.esNueva = true;
            ConfigurarFormulario();
            InicializarNuevaEnfermedad();
        }

        // Constructor para editar enfermedad existente
        public Enfermedad(int enfermedadId)
        {
            InitializeComponent();
            this.enfermedadId = enfermedadId;
            this.esNueva = false;
            ConfigurarFormulario();
            CargarEnfermedadExistente();
        }

        private void ConfigurarFormulario()
        {
            // Configurar ComboBox Tipo
            cboTipo.Items.AddRange(new object[] {
                "Infecciosa",
                "Crónica",
                "Degenerativa",
                "Cardiovascular",
                "Respiratoria",
                "Digestiva",
                "Endocrina",
                "Neurológica",
                "Dermatológica",
                "Traumática",
                "Genética",
                "Autoinmune",
                "Oncológica",
                "Mental",
                "Otra"
            });
        }

        private void InicializarNuevaEnfermedad()
        {
            enfermedad = new TEnfermedad();
            lblTitulo.Text = "Registrar Nueva Enfermedad";
        }

        private void CargarEnfermedadExistente()
        {
            try
            {
                using (var nEnfermedad = new NEnfermedad())
                {
                    enfermedad = nEnfermedad.BuscarPorId(enfermedadId.Value);

                    if (enfermedad == null)
                    {
                        MessageBox.Show("Enfermedad no encontrada.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    CargarDatosEnFormulario();
                    lblTitulo.Text = "Editar Enfermedad";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar enfermedad: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CargarDatosEnFormulario()
        {
            txtNombre.Text = enfermedad.Nombre ?? "";
            txtSintomas.Text = enfermedad.Sintomas ?? "";
            txtTratamiento.Text = enfermedad.Tratamiento ?? "";
            cboTipo.Text = enfermedad.Tipo ?? "";
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la enfermedad es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (txtNombre.Text.Length > 100)
            {
                MessageBox.Show("El nombre no puede exceder 100 caracteres.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtSintomas.Text) && txtSintomas.Text.Length > 1000)
            {
                MessageBox.Show("Los síntomas no pueden exceder 1000 caracteres.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSintomas.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtTratamiento.Text) && txtTratamiento.Text.Length > 1000)
            {
                MessageBox.Show("El tratamiento no puede exceder 1000 caracteres.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTratamiento.Focus();
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

                enfermedad.Nombre = txtNombre.Text.Trim();
                enfermedad.Sintomas = txtSintomas.Text.Trim();
                enfermedad.Tratamiento = txtTratamiento.Text.Trim();
                enfermedad.Tipo = cboTipo.Text.Trim();

                using (var nEnfermedad = new NEnfermedad())
                {
                    if (esNueva)
                    {
                        nEnfermedad.RegistrarEnfermedad(enfermedad);
                        MessageBox.Show("Enfermedad registrada correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        nEnfermedad.EditarEnfermedad(enfermedad);
                        MessageBox.Show("Enfermedad actualizada correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
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
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
