using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Medico;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class ucEditarExamen : UserControl
    {
        private TExamen examen;
        private bool esNuevo;
        private Form formularioOrigen;

        public event EventHandler ExamenGuardado;

        // Constructor para nuevo examen
        public ucEditarExamen(TExamen nuevoExamen, int expedienteId)
        {
            InitializeComponent();
            examen = nuevoExamen;
            examen.ExpedienteId = expedienteId;
            esNuevo = true;
            lblTitulo.Text = "Nuevo Examen";
            CrearControlesAdicionales();
            CargarDatos();
        }

        // Constructor para editar examen existente
        public ucEditarExamen(TExamen examenExistente)
        {
            InitializeComponent();
            examen = examenExistente;
            esNuevo = false;
            lblTitulo.Text = "Editar Examen";
            CrearControlesAdicionales();
            CargarDatos();
        }

        private void CrearControlesAdicionales()
        {
            // Los controles ya están creados en el designer
            // Solo configurar eventos adicionales si es necesario
        }

        private void ChkEsExterno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsExterno.Checked)
            {
                // Si es externo - deshabilitar lugar ya que MediNova no controla lugares externos
                txtLugarRealizacion.Enabled = false;
                txtLugarRealizacion.Text = "";
                txtLugarRealizacion.PlaceholderText = "No aplica - Examen externo";
                lblLugarRealizacion.Text = "Lugar de Realización:";

                // Deshabilitar costo y ponerlo en 0 (no se cobra en MediNova)
                txtCosto.Text = "0.00";
                txtCosto.Enabled = false;
                txtCosto.PlaceholderText = "No aplica - Examen externo";
            }
            else
            {
                // Si es interno (MediNova) - habilitar para especificar lugar dentro de MediNova
                txtLugarRealizacion.Enabled = true;
                txtLugarRealizacion.PlaceholderText = "Ingrese el edificio, departamento o área dentro de MediNova";
                lblLugarRealizacion.Text = "Lugar de Realización (Interno):";

                // Habilitar costo
                txtCosto.Enabled = true;
                txtCosto.PlaceholderText = "0.00";
            }
        }

        private void ChkTieneResultado_CheckedChanged(object sender, EventArgs e)
        {
            // Solo habilitar fecha de resultado si tiene resultado
            dtpFechaResultado.Enabled = chkTieneResultado.Checked;

            if (chkTieneResultado.Checked)
            {
                // Establecer automáticamente al día de hoy
                if (!examen.FechaResultado.HasValue)
                {
                    dtpFechaResultado.Value = DateTime.Now;
                }
            }
        }

        public void EstablecerFormularioOrigen(Form origen)
        {
            formularioOrigen = origen;
        }

        private void CargarDatos()
        {
            if (examen != null)
            {
                txtNombre.Text = examen.Nombre ?? "";
                cboTipo.SelectedItem = examen.Tipo ?? "Sangre";
                txtCosto.Text = examen.Costo.ToString("0.00");

                // Convertir estados antiguos a los nuevos
                string estadoActual = examen.Estado ?? "Pendiente";
                if (estadoActual == "Solicitado" || estadoActual == "EnProceso")
                {
                    estadoActual = "Pendiente";
                }
                cboEstado.SelectedItem = estadoActual;

                dtpFechaSolicitud.Value = examen.FechaSolicitud;

                // Cargar checkbox y campo de examen externo
                chkEsExterno.Checked = examen.EsExterno;

                // Configurar campos según si es externo o interno
                if (examen.EsExterno)
                {
                    // Si es externo - deshabilitar lugar ya que MediNova no controla lugares externos
                    txtLugarRealizacion.Enabled = false;
                    txtLugarRealizacion.Text = "";
                    txtLugarRealizacion.PlaceholderText = "No aplica - Examen externo";
                    lblLugarRealizacion.Text = "Lugar de Realización:";
                    txtCosto.Text = "0.00";
                    txtCosto.Enabled = false;
                    txtCosto.PlaceholderText = "No aplica - Examen externo";
                }
                else
                {
                    // Si es interno - habilitar y cargar el lugar
                    txtLugarRealizacion.Enabled = true;
                    txtLugarRealizacion.Text = examen.LugarRealizacion ?? "";
                    txtLugarRealizacion.PlaceholderText = "Ingrese el edificio, departamento o área dentro de MediNova";
                    lblLugarRealizacion.Text = "Lugar de Realización (Interno):";
                    txtCosto.Enabled = true;
                    txtCosto.PlaceholderText = "0.00";
                }

                if (examen.FechaResultado.HasValue)
                {
                    dtpFechaResultado.Value = examen.FechaResultado.Value;
                    chkTieneResultado.Checked = true;
                    dtpFechaResultado.Enabled = true;
                }
                else
                {
                    chkTieneResultado.Checked = false;
                    dtpFechaResultado.Enabled = false;
                }

                txtResultado.Text = examen.Resultado ?? "";
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre del examen es obligatorio.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (cboTipo.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione el tipo de examen.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboTipo.Focus();
                    return;
                }

                // Validar costo solo si NO es examen externo
                decimal costo = 0;
                if (!chkEsExterno.Checked)
                {
                    if (!decimal.TryParse(txtCosto.Text, out costo) || costo < 0)
                    {
                        MessageBox.Show("Ingrese un costo válido.",
                            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCosto.Focus();
                        return;
                    }
                }
                // Si es externo, el costo siempre es 0 (no se cobra en MediNova)

                // Asignar valores
                examen.Nombre = txtNombre.Text.Trim();
                examen.Tipo = cboTipo.SelectedItem.ToString();
                examen.Costo = chkEsExterno.Checked ? 0 : costo; // Costo 0 si es externo
                examen.FechaSolicitud = dtpFechaSolicitud.Value;
                examen.FechaResultado = chkTieneResultado.Checked ? dtpFechaResultado.Value : (DateTime?)null;
                examen.Resultado = txtResultado.Text.Trim();

                // Cambiar automáticamente a "Completado" si tiene resultado
                if (chkTieneResultado.Checked && !string.IsNullOrWhiteSpace(txtResultado.Text.Trim()))
                {
                    examen.Estado = "Completado";
                }
                else
                {
                    examen.Estado = cboEstado.SelectedItem?.ToString() ?? "Pendiente";
                }
                examen.EsExterno = chkEsExterno.Checked;

                // Guardar lugar de realización solo si es interno (externo no tiene lugar controlado por MediNova)
                if (chkEsExterno.Checked)
                {
                    examen.LugarRealizacion = null; // Externo: no se guarda lugar
                }
                else
                {
                    // Interno: guardar lugar si se especificó
                    examen.LugarRealizacion = string.IsNullOrWhiteSpace(txtLugarRealizacion.Text)
                        ? null
                        : txtLugarRealizacion.Text.Trim();
                }

                // Guardar en base de datos
                using (var nExamen = new NExamen())
                {
                    if (esNuevo)
                    {
                        nExamen.SolicitarExamen(examen);
                        MessageBox.Show("Examen agregado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        nExamen.EditarExamen(examen);
                        MessageBox.Show("Examen actualizado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Notificar que se guardó
                ExamenGuardado?.Invoke(this, EventArgs.Empty);

                // Volver al formulario anterior
                Volver();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar examen: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro de cancelar? Los cambios no guardados se perderán.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Volver();
            }
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "¿Está seguro de volver? Los cambios no guardados se perderán.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Volver();
            }
        }

        private void Volver()
        {
            // Buscar el panel principal
            Panel panelMain = EncontrarPanelMain(this);
            if (panelMain != null && formularioOrigen != null)
            {
                // Regresar al formulario de origen
                panelMain.Controls.Clear();
                formularioOrigen.TopLevel = false;
                formularioOrigen.FormBorderStyle = FormBorderStyle.None;
                formularioOrigen.Dock = DockStyle.Fill;
                panelMain.Controls.Add(formularioOrigen);
                formularioOrigen.Show();
            }
            else
            {
                // Si no hay panel principal, ocultar este control
                this.Visible = false;
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
    }
}
