using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Recepcionista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Recepcionista
{
    public partial class Pacientes : Form
    {
        private NPacientes _nPacientes;
        private int pacienteIdSeleccionado = 0;

        private List<TPaciente> listaTodosLosPacientes;
        private bool columnasConfiguradas = false; 

        public Pacientes()
        {
            InitializeComponent();
            _nPacientes = new NPacientes();
            listaTodosLosPacientes = new List<TPaciente>();

            // Optimizaciones del DataGridView
            DtgPacientes.DoubleBuffered(true);
            DtgPacientes.AutoGenerateColumns = false;
            ConfigurarColumnasDataGrid();
        }

        private async void Pacientes_Load(object sender, EventArgs e)
        {
            // Cargar datos de forma asíncrona para no bloquear la UI
            await CargarYRefrescarPacientesAsync();
            ConfigurarEstadoFormulario(true);
        }

        private void Pacientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            _nPacientes.Dispose();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string dniSinFormato = ObtenerDNISinFormato(txtDNI.Text);

                if (string.IsNullOrEmpty(dniSinFormato))
                {
                    MessageBox.Show("El DNI es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dniSinFormato.Length != 13)
                {
                    MessageBox.Show("El DNI debe tener 13 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!dniSinFormato.All(char.IsDigit))
                {
                    MessageBox.Show("El DNI solo debe contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(TxtNombreCompleto.Text))
                {
                    MessageBox.Show("El nombre completo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CbxSexo.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar el sexo del paciente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoPaciente = new TPaciente
                {
                    DNI = FormatearDNI(dniSinFormato),
                    NombreCompleto = TxtNombreCompleto.Text,
                    FechaNacimiento = DtpFechaNacimiento.Value,
                    Sexo = CbxSexo.SelectedItem.ToString(),
                    Telefono = TxtTelefono.Text,
                    Direccion = TxtDireccion.Text,
                    Eliminado = false
                };

                _nPacientes.GuardarPaciente(nuevoPaciente);

                MessageBox.Show("Paciente registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarYRefrescarPacientes();
                LimpiarControles();
                ConfigurarEstadoFormulario(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al registrar el paciente: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteIdSeleccionado == 0) return;

                string dniSinFormato = ObtenerDNISinFormato(txtDNI.Text);

                if (string.IsNullOrEmpty(dniSinFormato))
                {
                    MessageBox.Show("El DNI es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dniSinFormato.Length != 13)
                {
                    MessageBox.Show("El DNI debe tener 13 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!dniSinFormato.All(char.IsDigit))
                {
                    MessageBox.Show("El DNI solo debe contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(TxtNombreCompleto.Text))
                {
                    MessageBox.Show("El nombre completo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CbxSexo.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar el sexo del paciente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var pacienteAEditar = _nPacientes.BuscarPorId(pacienteIdSeleccionado);

                if (pacienteAEditar == null)
                {
                    MessageBox.Show("No se encontró el paciente para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                pacienteAEditar.DNI = FormatearDNI(dniSinFormato);
                pacienteAEditar.NombreCompleto = TxtNombreCompleto.Text;
                pacienteAEditar.FechaNacimiento = DtpFechaNacimiento.Value;
                pacienteAEditar.Sexo = CbxSexo.SelectedItem.ToString();
                pacienteAEditar.Telefono = TxtTelefono.Text;
                pacienteAEditar.Direccion = TxtDireccion.Text;

                _nPacientes.EditarPaciente(pacienteAEditar);

                MessageBox.Show("Paciente actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarYRefrescarPacientes();
                LimpiarControles();
                ConfigurarEstadoFormulario(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al actualizar el paciente: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (pacienteIdSeleccionado == 0) return;

            var confirmacion = MessageBox.Show(
                string.Format("¿Está seguro de que desea eliminar al paciente '{0}'? Esta acción no se puede deshacer.", TxtNombreCompleto.Text),
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _nPacientes.EliminarPaciente(pacienteIdSeleccionado);

                    MessageBox.Show("Paciente eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarYRefrescarPacientes();
                    LimpiarControles();
                    ConfigurarEstadoFormulario(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al eliminar el paciente: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarYRefrescarPacientes()
        {
            try
            {
                listaTodosLosPacientes = _nPacientes.ListadoActivos();
                AplicarFiltroYRefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pacientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltroYRefrescarGrid()
        {
            string filtro = BotonBuscar.Text.ToLower().Trim();

            List<TPaciente> listaFiltrada;

            if (string.IsNullOrEmpty(filtro))
            {
                listaFiltrada = listaTodosLosPacientes;
            }
            else
            {
                listaFiltrada = listaTodosLosPacientes
                    .Where(p =>
                        p.NombreCompleto.ToLower().Contains(filtro) ||
                        (p.DNI != null && p.DNI.Replace("-", "").Contains(filtro.Replace("-", ""))) ||
                        (p.Telefono != null && p.Telefono.Contains(filtro))
                    )
                    .ToList();
            }

            // 🔹 Optimización: Suspender el layout para mejorar rendimiento
            DtgPacientes.SuspendLayout();
            try
            {
                DtgPacientes.DataSource = null;
                DtgPacientes.DataSource = listaFiltrada;
            }
            finally
            {
                DtgPacientes.ResumeLayout();
            }
        }

        private void LimpiarControles()
        {
            pacienteIdSeleccionado = 0;
            txtDNI.Clear();
            TxtNombreCompleto.Clear();
            DtpFechaNacimiento.Value = DateTime.Now;
            CbxSexo.SelectedIndex = -1;
            TxtTelefono.Clear();
            TxtDireccion.Clear();
        }

        private void ConfigurarEstadoFormulario(bool esModoNuevo)
        {
            BtnNuevo.Enabled = !esModoNuevo;
            BtnRegistrar.Enabled = esModoNuevo;
            BtnEditar.Enabled = !esModoNuevo;
            BtnEliminar.Enabled = !esModoNuevo;

            if (esModoNuevo)
            {
                LimpiarControles();
            }
        }

        private void BotonBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltroYRefrescarGrid();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            ConfigurarEstadoFormulario(true);
            txtDNI.Focus();
        }

        private void DtgPacientes_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                var fila = DtgPacientes.Rows[e.RowIndex];
                pacienteIdSeleccionado = Convert.ToInt32(fila.Cells["PacienteId"].Value);

                txtDNI.Text = fila.Cells["DNI"].Value.ToString();
                TxtNombreCompleto.Text = fila.Cells["NombreCompleto"].Value.ToString();
                DtpFechaNacimiento.Value = Convert.ToDateTime(fila.Cells["FechaNacimiento"].Value);
                CbxSexo.SelectedItem = fila.Cells["Sexo"].Value.ToString();
                TxtTelefono.Text = fila.Cells["Telefono"].Value != null ? fila.Cells["Telefono"].Value.ToString() : null;
                TxtDireccion.Text = fila.Cells["Direccion"].Value != null ? fila.Cells["Direccion"].Value.ToString() : null;

                ConfigurarEstadoFormulario(false);
            }
        }

        // 🔹 Métodos de ayuda para formateo de DNI
        private string FormatearDNI(string dniSinFormato)
        {
            if (string.IsNullOrEmpty(dniSinFormato) || dniSinFormato.Length != 13)
                return dniSinFormato;

            // Formato: 0801-2004-13346
            return $"{dniSinFormato.Substring(0, 4)}-{dniSinFormato.Substring(4, 4)}-{dniSinFormato.Substring(8, 5)}";
        }

        private string ObtenerDNISinFormato(string dniConFormato)
        {
            if (string.IsNullOrEmpty(dniConFormato))
                return string.Empty;

            return dniConFormato.Replace("-", "").Trim();
        }

        // 🔹 Event Handlers para validación de entrada
        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números, backspace y guiones
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            // Auto-formatear mientras escribe
            string textoActual = txtDNI.Text;
            string soloNumeros = new string(textoActual.Where(char.IsDigit).ToArray());

            if (soloNumeros.Length > 13)
            {
                soloNumeros = soloNumeros.Substring(0, 13);
            }

            if (soloNumeros.Length > 0)
            {
                string textoFormateado = "";

                if (soloNumeros.Length <= 4)
                {
                    textoFormateado = soloNumeros;
                }
                else if (soloNumeros.Length <= 8)
                {
                    textoFormateado = $"{soloNumeros.Substring(0, 4)}-{soloNumeros.Substring(4)}";
                }
                else
                {
                    textoFormateado = $"{soloNumeros.Substring(0, 4)}-{soloNumeros.Substring(4, 4)}-{soloNumeros.Substring(8)}";
                }

                if (textoFormateado != textoActual)
                {
                    int cursorPos = txtDNI.SelectionStart;
                    int diferencia = textoFormateado.Length - textoActual.Length;

                    txtDNI.Text = textoFormateado;
                    txtDNI.SelectionStart = Math.Max(0, Math.Min(cursorPos + diferencia, textoFormateado.Length));
                }
            }
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private async System.Threading.Tasks.Task CargarYRefrescarPacientesAsync()
        {
            try
            {
                // Cargar datos en background thread para no bloquear la UI
                listaTodosLosPacientes = await System.Threading.Tasks.Task.Run(() => _nPacientes.ListadoActivos());
                AplicarFiltroYRefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pacientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasDataGrid()
        {
            if (DtgPacientes.Columns.Count > 0) return;

            DtgPacientes.Columns.Clear();

            // Definir solo las columnas necesarias
            DtgPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PacienteId",
                HeaderText = "ID",
                DataPropertyName = "PacienteId",
                Width = 50,
                ReadOnly = true
            });

            DtgPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNI",
                HeaderText = "DNI",
                DataPropertyName = "DNI",
                Width = 130,
                ReadOnly = true
            });

            DtgPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreCompleto",
                HeaderText = "Nombre Completo",
                DataPropertyName = "NombreCompleto",
                Width = 200,
                ReadOnly = true
            });

            DtgPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaNacimiento",
                HeaderText = "Fecha Nacimiento",
                DataPropertyName = "FechaNacimiento",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            DtgPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sexo",
                HeaderText = "Sexo",
                DataPropertyName = "Sexo",
                Width = 80,
                ReadOnly = true
            });

            DtgPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Teléfono",
                DataPropertyName = "Telefono",
                Width = 100,
                ReadOnly = true
            });

            DtgPacientes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Direccion",
                HeaderText = "Dirección",
                DataPropertyName = "Direccion",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            columnasConfiguradas = true;
        }

        private void ConfigurarColumnasOptimizado()
        {
            // Método obsoleto - Las columnas ahora se configuran manualmente en ConfigurarColumnasDataGrid
        }

        private void TxtUsuario_TextChanged(object sender, EventArgs e) { }
        private void TxtPassword_TextChanged(object sender, EventArgs e) { }
        private void CbxSexo_SelectedIndexChanged(object sender, EventArgs e) { }
        private void TxtDireccion_TextChanged(object sender, EventArgs e) { }
        private void DtpFechaNacimiento_ValueChanged(object sender, EventArgs e) { }
        private void DtgPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }

    // Clase de extensión para DoubleBuffered en DataGridView
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            doubleBufferPropertyInfo?.SetValue(control, enable, null);
        }
    }
}