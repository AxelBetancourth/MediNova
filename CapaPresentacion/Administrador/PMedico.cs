using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Administrador;
using CapaNegocio.Recepcionista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.Administrador
{
    public partial class PMedico : Form
    {
        private NDoctor nDoctor;
        private NLogin nLogin;

        private int doctorIdSeleccionado = 0;
        private string nombreDoctorSeleccionado = "";
        private string modoGrid = "Doctores";

        // Struct para mostrar días en español
        private struct DiaSemanaDisplay
        {
            public string Nombre { get; set; }
            public DayOfWeek Valor { get; set; }
            public override string ToString() => Nombre;
        }

        // Clase interna para mostrar horarios en el ListBox
        private class HorarioDisplay
        {
            public THorario Horario { get; set; }
            public override string ToString()
            {
                string dia = TraducirDia(Horario.DiaSemana); // Usar la función de traducción
                return string.Format("{0}: {1:hh\\:mm} - {2:hh\\:mm}", dia, Horario.HoraInicio, Horario.HoraFin);
            }
        }

        public PMedico()
        {
            InitializeComponent();
            nDoctor = new NDoctor();
            nLogin = new NLogin();

            // Optimizar rendimiento del DataGridView usando reflexión
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, DtgMedicos, new object[] { true });

            DtgMedicos.AutoGenerateColumns = true;

            // Configurar estilo del encabezado (azul)
            DtgMedicos.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            DtgMedicos.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            DtgMedicos.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            DtgMedicos.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            DtgMedicos.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DtgMedicos.ColumnHeadersHeight = 40;
            DtgMedicos.EnableHeadersVisualStyles = false; // Importante para aplicar colores personalizados

            // Configurar estilo de las celdas
            DtgMedicos.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            DtgMedicos.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            DtgMedicos.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            DtgMedicos.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 240, 255);
            DtgMedicos.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
        }

        private async void PMedico_Load(object sender, EventArgs e)
        {
            CargarCombos();
            await CargarGridDoctoresAsync();
        }

        // Función para traducir DayOfWeek a Español
        private static string TraducirDia(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miércoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sábado";
                case DayOfWeek.Sunday: return "Domingo";
                default: return dia.ToString();
            }
        }

        private void CargarCombos()
        {
            // Cargar Especialidades
            CBEspecialidad.Items.Clear();
            CBEspecialidad.Items.AddRange(new string[] {
                "Medicina General", "Pediatría", "Ginecología",
                "Cardiología", "Dermatología", "Oftalmología", "Ortopedia"
            });
            CBEspecialidad.SelectedIndex = 0;

            // Cargar Días usando el struct
            checkedListDiasSemana.Items.Clear();
            checkedListDiasSemana.Items.Add(new DiaSemanaDisplay { Nombre = "Lunes", Valor = DayOfWeek.Monday });
            checkedListDiasSemana.Items.Add(new DiaSemanaDisplay { Nombre = "Martes", Valor = DayOfWeek.Tuesday });
            checkedListDiasSemana.Items.Add(new DiaSemanaDisplay { Nombre = "Miércoles", Valor = DayOfWeek.Wednesday });
            checkedListDiasSemana.Items.Add(new DiaSemanaDisplay { Nombre = "Jueves", Valor = DayOfWeek.Thursday });
            checkedListDiasSemana.Items.Add(new DiaSemanaDisplay { Nombre = "Viernes", Valor = DayOfWeek.Friday });
            checkedListDiasSemana.Items.Add(new DiaSemanaDisplay { Nombre = "Sábado", Valor = DayOfWeek.Saturday });
            checkedListDiasSemana.Items.Add(new DiaSemanaDisplay { Nombre = "Domingo", Valor = DayOfWeek.Sunday });

            // Cargar Horas
            CBInicio.Items.Clear();
            CBFinal.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                CBInicio.Items.Add(new TimeSpan(i, 0, 0).ToString(@"hh\:mm") + (i < 12 ? " AM" : " PM"));
                CBFinal.Items.Add(new TimeSpan(i, 0, 0).ToString(@"hh\:mm") + (i < 12 ? " AM" : " PM"));
            }
            CBInicio.SelectedIndex = 8; // Default 8:00 AM
            CBFinal.SelectedIndex = 17; // Default 5:00 PM (17:00)
        }

        private async System.Threading.Tasks.Task CargarGridDoctoresAsync()
        {
            try
            {
                modoGrid = "Doctores";
                HabilitarControles(true);
                BtnRegistrar.Enabled = true;
                BtnHorario.Text = "Ver Horario";

                // Cargar datos en segundo plano
                var listaDoctores = await System.Threading.Tasks.Task.Run(() => nDoctor.ListarDoctores());

                var dataParaGrid = listaDoctores.Select(d => new
                {
                    Id = d.DoctorId,
                    Nombre = d.NombreCompleto,
                    Especialidad = d.Especialidad,
                    Telefono = d.Telefono,
                    Correo = d.Correo,
                    Disponible = d.Disponible ? "Sí" : "No",
                    Usuario = d.Usuario != null ? d.Usuario.NombreUsuario : "Sin Asignar"
                }).ToList();

                DtgMedicos.SuspendLayout();
                DtgMedicos.DataSource = dataParaGrid;

                if (DtgMedicos.Columns.Count > 0)
                {
                    DtgMedicos.Columns["Id"].Width = 40;
                    DtgMedicos.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                DtgMedicos.ResumeLayout();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar doctores: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGridDoctores()
        {
            CargarGridDoctoresAsync().Wait();
        }

        private void LimpiarFormulario()
        {
            doctorIdSeleccionado = 0;
            nombreDoctorSeleccionado = "";
            TxtNombre.Text = "";
            TxtTelefono.Text = "";
            Email.Text = "";
            CBEspecialidad.SelectedIndex = 0;
            CbDisponible.Checked = true;

            for (int i = 0; i < checkedListDiasSemana.Items.Count; i++)
            {
                checkedListDiasSemana.SetItemChecked(i, false);
            }

            CBInicio.SelectedIndex = 8;
            CBFinal.SelectedIndex = 17;
            lstHorariosAgregados.Items.Clear();

            BtnRegistrar.Enabled = true;
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;

            // 🔽 NUEVO: Ocultar botones de acción y labels
            BtnUsuario.Visible = false;
            BtnHorario.Visible = false;
            label7.Visible = false; // Asumo que label7 es de BtnUsuario
            label8.Visible = false; // Asumo que label8 es de BtnHorario
        }

        private void HabilitarControles(bool habilitar)
        {
            TxtNombre.Enabled = habilitar;
            TxtTelefono.Enabled = habilitar;
            Email.Enabled = habilitar;
            CBEspecialidad.Enabled = habilitar;
            CbDisponible.Enabled = habilitar;

            CBInicio.Enabled = habilitar;
            CBFinal.Enabled = habilitar;
            checkedListDiasSemana.Enabled = habilitar;
            lstHorariosAgregados.Enabled = habilitar;
            BtnAgregarHorario.Enabled = habilitar;
            BtnQuitarHorario.Enabled = habilitar;

            BtnRegistrar.Enabled = habilitar;
            BtnEditar.Enabled = habilitar;
            BtnEliminar.Enabled = habilitar;

            // ⚠️ CORREGIDO: BtnUsuario y BtnHorario se manejan por separado
            // (se habilitan al hacer clic en el grid)
        }

        // Este método ahora lee el ListBox (esto está bien)
        private List<THorario> ObtenerHorariosDesdeForm()
        {
            if (lstHorariosAgregados.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un turno horario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            var horarios = lstHorariosAgregados.Items
                            .Cast<HorarioDisplay>()
                            .Select(h => h.Horario)
                            .ToList();

            return horarios;
        }

        // Lógica para el botón ">>" (Agregar Horario)
        private void BtnAgregarHorario_Click(object sender, EventArgs e)
        {
            TimeSpan horaInicio, horaFin;
            try
            {
                string strInicio = CBInicio.SelectedItem.ToString().Split(' ')[0];
                string strFin = CBFinal.SelectedItem.ToString().Split(' ')[0];
                horaInicio = TimeSpan.Parse(strInicio);
                horaFin = TimeSpan.Parse(strFin);

                if (horaFin <= horaInicio)
                {
                    MessageBox.Show("La hora final debe ser mayor que la hora de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Formato de hora inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkedListDiasSemana.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un día.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var item in checkedListDiasSemana.CheckedItems)
            {
                var diaDisplay = (DiaSemanaDisplay)item;

                var nuevoHorario = new THorario
                {
                    DiaSemana = diaDisplay.Valor,
                    HoraInicio = horaInicio,
                    HoraFin = horaFin
                };

                lstHorariosAgregados.Items.Add(new HorarioDisplay { Horario = nuevoHorario });
            }

            for (int i = 0; i < checkedListDiasSemana.Items.Count; i++)
            {
                checkedListDiasSemana.SetItemChecked(i, false);
            }
        }

        // Lógica para el botón "X" (Quitar Horario)
        private void BtnQuitarHorario_Click(object sender, EventArgs e)
        {
            if (lstHorariosAgregados.SelectedItem != null)
            {
                lstHorariosAgregados.Items.Remove(lstHorariosAgregados.SelectedItem);
            }
            else
            {
                MessageBox.Show("Seleccione un horario de la lista para quitar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Evento TextChanged del Buscador
        private async void BotonBuscar_TextChanged(object sender, EventArgs e)
        {
            string busqueda = (sender as TextBox).Text.Trim();

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                if (modoGrid != "Doctores")
                {
                    await CargarGridDoctoresAsync();
                }
            }
            else
            {
                try
                {
                    modoGrid = "Horarios";
                    BtnHorario.Text = "Ver Doctores";

                    // Buscar en segundo plano
                    var horarios = await System.Threading.Tasks.Task.Run(() =>
                    {
                        var doctoresFiltrados = nDoctor.ListarDoctores()
                            .Where(d => d.NombreCompleto.ToLower().Contains(busqueda.ToLower()))
                            .ToList();

                        return doctoresFiltrados
                            .SelectMany(d => (d.Horarios ?? new List<THorario>())
                                .Select(h => new {
                                    Doctor = d.NombreCompleto,
                                    Dia = TraducirDia(h.DiaSemana),
                                    Inicio = h.HoraInicio.ToString(@"hh\:mm"),
                                    Fin = h.HoraFin.ToString(@"hh\:mm")
                                }))
                            .OrderBy(h => h.Doctor).ThenBy(h => h.Dia)
                            .ToList();
                    });

                    DtgMedicos.SuspendLayout();
                    DtgMedicos.DataSource = horarios;
                    DtgMedicos.ResumeLayout();

                    HabilitarControles(false);
                    BtnRegistrar.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al buscar horarios: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Lógica de Toggle para el botón "Ver Horario"
        private async void BtnHorario_Click(object sender, EventArgs e)
        {
            if (modoGrid == "Horarios" || modoGrid == "Usuarios")
            {
                await CargarGridDoctoresAsync();
                return;
            }

            if (doctorIdSeleccionado == 0)
            {
                MessageBox.Show("Primero seleccione un doctor de la lista para ver su horario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MostrarHorariosExistentes(doctorIdSeleccionado);
        }

        // 🔹 Método separado para SOLO VER horarios existentes (sin edición en el formulario)
        private void MostrarHorariosExistentes(int doctorId)
        {
            try
            {
                // 🔹 Consultar SIEMPRE datos frescos de la BD
                var doctor = nDoctor.BuscarPorId(doctorId);

                if (doctor == null || doctor.Horarios == null || !doctor.Horarios.Any())
                {
                    MessageBox.Show(string.Format("El doctor '{0}' no tiene horarios registrados.", nombreDoctorSeleccionado), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Mostrar horarios ordenados por día de la semana
                var horariosText = string.Join("\n", doctor.Horarios
                    .OrderBy(h => h.DiaSemana)
                    .Select(h => string.Format("{0}: {1:hh\\:mm} - {2:hh\\:mm}", TraducirDia(h.DiaSemana), h.HoraInicio, h.HoraFin)));

                MessageBox.Show(
                    string.Format("Horarios del doctor '{0}':\n\n{1}", nombreDoctorSeleccionado, horariosText),
                    "Horarios Registrados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al mostrar horarios: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Se llena el ListBox, ya no los combos/checks
        private void CargarDatosEnFormulario(int doctorId)
        {
            try
            {
                // 🔹 CRÍTICO: Limpiar el ListBox ANTES de consultar la BD
                lstHorariosAgregados.Items.Clear();

                var doctor = nDoctor.BuscarPorId(doctorId);
                if (doctor == null)
                {
                    MessageBox.Show("No se encontró el doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpiarFormulario();
                    return;
                }

                TxtNombre.Text = doctor.NombreCompleto;
                TxtTelefono.Text = doctor.Telefono;
                Email.Text = doctor.Correo;
                CBEspecialidad.SelectedItem = doctor.Especialidad;
                CbDisponible.Checked = doctor.Disponible;

                // Cargar horarios existentes desde la BD
                if (doctor.Horarios != null && doctor.Horarios.Any())
                {
                    foreach (var horario in doctor.Horarios.OrderBy(h => h.DiaSemana))
                    {
                        lstHorariosAgregados.Items.Add(new HorarioDisplay { Horario = horario });
                    }
                }

                // Limpiar los checkboxes de días (no se usan para edición, solo para agregar nuevos)
                for (int i = 0; i < checkedListDiasSemana.Items.Count; i++)
                {
                    checkedListDiasSemana.SetItemChecked(i, false);
                }
                CBInicio.SelectedIndex = 8;
                CBFinal.SelectedIndex = 17;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar datos del doctor: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EVENTOS DE BOTONES (CRUD) ---

        private async void BtnUsuario_Click(object sender, EventArgs e)
        {
            if (doctorIdSeleccionado == 0)
            {
                MessageBox.Show("Primero debe seleccionar un doctor del grid.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Cargar usuarios en segundo plano
                var usuariosMedicos = await System.Threading.Tasks.Task.Run(() =>
                    nLogin.ListarMedicosDisponiblesParaAsociar());

                if (!usuariosMedicos.Any())
                {
                    MessageBox.Show("No hay usuarios con rol 'Medico' disponibles para asociar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                modoGrid = "Usuarios";
                BtnHorario.Text = "Ver Doctores";
                HabilitarControles(false);
                BtnRegistrar.Enabled = false;

                var dataParaGrid = usuariosMedicos.Select(u => new
                {
                    Id = u.UsuarioId,
                    Nombre = u.NombreUsuario,
                    Registrado = u.FechaRegistro,
                    Huella = u.HuellaDactilar != null ? "Sí" : "No"
                }).ToList();

                DtgMedicos.SuspendLayout();
                DtgMedicos.DataSource = dataParaGrid;
                DtgMedicos.ResumeLayout();

                MessageBox.Show(
                    string.Format("Seleccione un usuario de la lista para asociarlo al doctor '{0}'.\n\n(Para cancelar, vuelva con el botón 'Ver Doctores').", nombreDoctorSeleccionado),
                    "Asociar Usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar usuarios: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var horarios = ObtenerHorariosDesdeForm();
                if (horarios == null) return;

                var nuevoDoctor = new TDoctor
                {
                    NombreCompleto = TxtNombre.Text.Trim(),
                    Especialidad = CBEspecialidad.SelectedItem.ToString(),
                    Telefono = TxtTelefono.Text.Trim(),
                    Correo = Email.Text.Trim(),
                    Disponible = CbDisponible.Checked
                };

                int resultado = await System.Threading.Tasks.Task.Run(() =>
                    nDoctor.RegistrarDoctor(nuevoDoctor, horarios));

                if (resultado > 0)
                {
                    MessageBox.Show("Doctor registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarGridDoctoresAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar al doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al registrar: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Este método ahora preserva el UsuarioId
        private async void BtnEditar_Click(object sender, EventArgs e)
        {
            if (doctorIdSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un doctor para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(TxtNombre.Text))
                {
                    MessageBox.Show("El nombre del doctor es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtTelefono.Text))
                {
                    MessageBox.Show("El teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtTelefono.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Email.Text))
                {
                    MessageBox.Show("El correo electrónico es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Email.Focus();
                    return;
                }

                var horarios = ObtenerHorariosDesdeForm();
                if (horarios == null) return;

                // Capturar valores de los controles de UI ANTES del Task.Run
                string nombreCompleto = TxtNombre.Text.Trim();
                string especialidad = CBEspecialidad.SelectedItem != null ? CBEspecialidad.SelectedItem.ToString() : null;
                string telefono = TxtTelefono.Text.Trim();
                string correo = Email.Text.Trim();
                bool disponible = CbDisponible.Checked;

                // Ejecutar la edición en segundo plano
                int resultado = await System.Threading.Tasks.Task.Run(() =>
                {
                    var doctorParaEditar = nDoctor.BuscarPorId(doctorIdSeleccionado);
                    if (doctorParaEditar == null)
                        return 0;

                    doctorParaEditar.NombreCompleto = nombreCompleto;
                    doctorParaEditar.Especialidad = especialidad;
                    doctorParaEditar.Telefono = telefono;
                    doctorParaEditar.Correo = correo;
                    doctorParaEditar.Disponible = disponible;

                    foreach (var h in horarios)
                    {
                        h.DoctorId = doctorIdSeleccionado;
                    }

                    return nDoctor.EditarDoctor(doctorParaEditar, horarios);
                });

                if (resultado > 0)
                {
                    MessageBox.Show("Doctor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarGridDoctoresAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar al doctor. Verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string mensajeError = string.Format("Error al editar: {0}", ex.Message);

                if (ex.InnerException != null)
                {
                    mensajeError += string.Format("\n\nDetalles técnicos: {0}", ex.InnerException.Message);
                }

                MessageBox.Show(mensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                System.Diagnostics.Debug.WriteLine(string.Format("Error en BtnEditar_Click: {0}", ex.Message));
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("InnerException: {0}", ex.InnerException.Message));
                }
            }
        }

        private async void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (doctorIdSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un doctor para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                string.Format("¿Está seguro de eliminar al doctor '{0}'? Esta acción es permanente.", nombreDoctorSeleccionado),
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int resultado = await System.Threading.Tasks.Task.Run(() =>
                        nDoctor.EliminarDoctor(doctorIdSeleccionado));

                    if (resultado > 0)
                    {
                        MessageBox.Show("Doctor eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CargarGridDoctoresAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar al doctor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al eliminar: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void DtgMedicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = DtgMedicos.Rows[e.RowIndex];

            if (modoGrid == "Doctores")
            {
                // 🔽 NUEVO: Lógica para Deseleccionar
                int clickedId = (int)fila.Cells["Id"].Value;
                if (clickedId == doctorIdSeleccionado)
                {
                    LimpiarFormulario(); // Limpia el form, oculta botones y resetea 'doctorIdSeleccionado'
                    return; // Salimos del método
                }
                // --- Fin de la lógica de deselección ---

                doctorIdSeleccionado = clickedId;
                nombreDoctorSeleccionado = fila.Cells["Nombre"].Value.ToString();

                CargarDatosEnFormulario(doctorIdSeleccionado);

                BtnRegistrar.Enabled = false;
                BtnEditar.Enabled = true;
                BtnEliminar.Enabled = true;

                // Mostrar botones de acción y labels
                BtnUsuario.Visible = true;
                BtnHorario.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
            }
            else if (modoGrid == "Usuarios")
            {
                int usuarioId = (int)fila.Cells["Id"].Value;
                string nombreUsuario = fila.Cells["Nombre"].Value.ToString();

                var confirmacion = MessageBox.Show(
                    string.Format("¿Desea asociar el usuario '{0}' al doctor '{1}'?\n\n(Si el doctor ya tenía un usuario, será reemplazado).", nombreUsuario, nombreDoctorSeleccionado),
                    "Confirmar Asociación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    try
                    {
                        int resultado = nLogin.AsociarDoctorAUsuario(doctorIdSeleccionado, usuarioId);
                        if (resultado > 0)
                        {
                            MessageBox.Show("Usuario asociado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await CargarGridDoctoresAsync(); // Regresa automáticamente al grid de doctores
                        }
                        else
                        {
                            MessageBox.Show("No se pudo asociar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al asociar: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DtgMedicos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void TxtNombre_TextChanged(object sender, EventArgs e) { }
        private void TxtTelefono_TextChanged(object sender, EventArgs e) { }
        private void Email_TextChanged(object sender, EventArgs e) { }
        private void CBEspecialidad_SelectedIndexChanged(object sender, EventArgs e) { }
        private void CbDisponible_CheckedChanged(object sender, EventArgs e) { }
        private void CBInicio_SelectedIndexChanged(object sender, EventArgs e) { }
        private void CBFinal_SelectedIndexChanged(object sender, EventArgs e) { }
        private void checkedListDiasSemana_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}