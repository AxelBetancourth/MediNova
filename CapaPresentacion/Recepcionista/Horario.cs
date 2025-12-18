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
    public partial class Horario : Form
    {
        private string fechaSeleccionada;
        private DateTime fechaSeleccionadaDateTime;
        private List<ucHoraSlot> todosLosSlots = new List<ucHoraSlot>();
        private ucHoraSlot slotSeleccionado = null;
        private List<TCita> citasDelDia = new List<TCita>();

        // 🔹 FlowLayoutPanels para mañana y tarde
        private FlowLayoutPanel flowPanelMañana;
        private FlowLayoutPanel flowPanelTarde;

        public Horario()
        {
            InitializeComponent();
            CrearFlowLayoutPanels(); // Crear los FlowLayoutPanel dinámicamente
            GenerarHorarios();
        }

        public void SetDiaSeleccionado(string fecha)
        {
            fechaSeleccionada = fecha;
            lbDay.Text = "Día seleccionado: " + fecha;

            // Parsear la fecha
            if (DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaSeleccionadaDateTime))
            {
                CargarCitasDelDia();
                ActualizarEstadoSlots();
            }
        }

        private void CargarCitasDelDia()
        {
            try
            {
                DateTime fechaInicio = fechaSeleccionadaDateTime.Date;
                DateTime fechaFin = fechaInicio.AddDays(1).AddSeconds(-1);

                using (var nCitas = new NCitas())
                {
                    citasDelDia = nCitas.ObtenerCitasPorRango(fechaInicio, fechaFin)
                        .Where(c => !c.Eliminado)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar citas: {0}", ex.Message), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                citasDelDia = new List<TCita>();
            }
        }

        private void ActualizarEstadoSlots()
        {
            foreach (var slot in todosLosSlots)
            {
                var slotHoraInicio = ParsearHoraCompleta(slot.HoraInicio);
                var slotHoraFin = ParsearHoraCompleta(slot.HoraFin);

                var citaEnHorario = citasDelDia.FirstOrDefault(c =>
                {
                    var citaInicio = c.FechaHoraInicio.TimeOfDay;
                    var citaFin = c.FechaHoraFin.TimeOfDay;
                    return (citaInicio < slotHoraFin && citaFin > slotHoraInicio);
                });

                if (citaEnHorario != null)
                {
                    slot.Disponible = false;
                    slot.CitaId = citaEnHorario.CitaId;
                }
                else
                {
                    slot.Disponible = true;
                    slot.CitaId = null;
                }
            }
        }

        private TimeSpan ParsearHoraCompleta(string horaStr)
        {
            try
            {
                horaStr = horaStr.Trim();

                bool tieneAM = horaStr.ToUpper().Contains("AM");
                bool tienePM = horaStr.ToUpper().Contains("PM");

                string horaSinSufijo = horaStr.Replace(" AM", "").Replace(" PM", "")
                                              .Replace("AM", "").Replace("PM", "")
                                              .Replace(" am", "").Replace(" pm", "")
                                              .Replace("am", "").Replace("pm", "")
                                              .Trim();

                string[] partes = horaSinSufijo.Split(':');
                if (partes.Length != 2)
                    return TimeSpan.Zero;

                if (!int.TryParse(partes[0], out int hora))
                    return TimeSpan.Zero;

                if (!int.TryParse(partes[1], out int minutos))
                    return TimeSpan.Zero;

                // Convertir a formato 24 horas
                if (tienePM && hora != 12)
                    hora += 12;
                else if (tieneAM && hora == 12)
                    hora = 0;

                return new TimeSpan(hora, minutos, 0);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }

        // 🔹 CREAR FLOWLAYOUTPANELS DINÁMICAMENTE - 2 COLUMNAS UNIFORMES
        private void CrearFlowLayoutPanels()
        {
            // FlowLayoutPanel para la mañana - 2 COLUMNAS
            flowPanelMañana = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,  // De izquierda a derecha
                WrapContents = true,  // Permitir que se envuelva en 2 columnas
                Padding = new Padding(10),
                BackColor = Color.White,
                MaximumSize = new Size(482, 0)  // Forzar el ancho máximo
            };

            // FlowLayoutPanel para la tarde - 2 COLUMNAS (IDÉNTICO AL DE MAÑANA)
            flowPanelTarde = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,  // De izquierda a derecha
                WrapContents = true,  // Permitir que se envuelva en 2 columnas
                Padding = new Padding(10),
                BackColor = Color.White,
                MaximumSize = new Size(482, 0)  // Forzar el ancho máximo
            };

            // Agregar los FlowLayoutPanel dentro de los Guna2Panel existentes
            panelMañana.Controls.Add(flowPanelMañana);
            panelTarde.Controls.Add(flowPanelTarde);
        }

        private void GenerarHorarios()
        {
            // Horarios de la mañana (00:00 - 11:59 AM)
            var horariosMañana = new List<(string inicio, string fin)>
            {
                ("12:00 AM", "01:00 AM"),
                ("01:00 AM", "02:00 AM"),
                ("02:00 AM", "03:00 AM"),
                ("03:00 AM", "04:00 AM"),
                ("04:00 AM", "05:00 AM"),
                ("05:00 AM", "06:00 AM"),
                ("06:00 AM", "07:00 AM"),
                ("07:00 AM", "08:00 AM"),
                ("08:00 AM", "09:00 AM"),
                ("09:00 AM", "10:00 AM"),
                ("10:00 AM", "11:00 AM"),
                ("11:00 AM", "12:00 PM")
            };

            // Horarios de la tarde (12:00 PM - 11:59 PM)
            var horariosTarde = new List<(string inicio, string fin)>
            {
                ("12:00 PM", "01:00 PM"),
                ("01:00 PM", "02:00 PM"),
                ("02:00 PM", "03:00 PM"),
                ("03:00 PM", "04:00 PM"),
                ("04:00 PM", "05:00 PM"),
                ("05:00 PM", "06:00 PM"),
                ("06:00 PM", "07:00 PM"),
                ("07:00 PM", "08:00 PM"),
                ("08:00 PM", "09:00 PM"),
                ("09:00 PM", "10:00 PM"),
                ("10:00 PM", "11:00 PM"),
                ("11:00 PM", "12:00 AM")
            };

            // Generar slots de la mañana
            foreach (var horario in horariosMañana)
            {
                bool disponible = EsHoraDisponible(horario.inicio);
                ucHoraSlot slot = new ucHoraSlot(horario.inicio, horario.fin, disponible);
                slot.SlotClicked += Slot_Clicked;
                flowPanelMañana.Controls.Add(slot); // 🔹 Usar flowPanelMañana
                todosLosSlots.Add(slot);
            }

            // Generar slots de la tarde
            foreach (var horario in horariosTarde)
            {
                bool disponible = EsHoraDisponible(horario.inicio);
                ucHoraSlot slot = new ucHoraSlot(horario.inicio, horario.fin, disponible);
                slot.SlotClicked += Slot_Clicked;
                flowPanelTarde.Controls.Add(slot); // 🔹 Usar flowPanelTarde
                todosLosSlots.Add(slot);
            }
        }

        // 🔹 MÉTODO PARA VERIFICAR DISPONIBILIDAD (conectar con BD)
        private bool EsHoraDisponible(string hora)
        {
            // TODO: Consultar en la base de datos si esta hora está ocupada
            // Por ahora retorna true (todas disponibles)

            // Ejemplo de lógica:
            // return !_nCitas.ExisteCitaEnHorario(fechaSeleccionada, hora);

            return true;
        }

        private void Slot_Clicked(object sender, EventArgs e)
        {
            // Deseleccionar todos los slots
            foreach (var slot in todosLosSlots)
            {
                slot.Deseleccionar();
            }

            // Seleccionar el slot clickeado
            slotSeleccionado = sender as ucHoraSlot;
            if (slotSeleccionado != null)
            {
                slotSeleccionado.Seleccionado = true;

                // Abrir directamente el formulario AgendarCita
                AbrirAgendarCita();
            }
        }

        private void AbrirAgendarCita()
        {
            if (slotSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione una hora primero.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AgendarCita frmAgendar = new AgendarCita();

            // Si el slot está ocupado, abrir en modo edición
            if (slotSeleccionado.CitaId.HasValue)
            {
                frmAgendar.CargarCita(slotSeleccionado.CitaId.Value);
            }
            else
            {
                // Si está disponible, abrir en modo nueva cita con datos iniciales
                frmAgendar.SetDatosIniciales(fechaSeleccionada, slotSeleccionado.HoraCompleta);
            }

            Panel panelMain = this.Parent as Panel;
            if (panelMain != null && panelMain.Name == "PanelMain")
            {
                panelMain.Controls.Clear();
                frmAgendar.TopLevel = false;
                frmAgendar.FormBorderStyle = FormBorderStyle.None;
                frmAgendar.Dock = DockStyle.Fill;
                panelMain.Controls.Add(frmAgendar);
                frmAgendar.Show();
            }
        }

        private void btnAgendar_Click(object sender, EventArgs e)
        {
            AbrirAgendarCita();
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
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

        private void Horario_Load(object sender, EventArgs e)
        {
        }

        private void lbDay_Click(object sender, EventArgs e)
        {
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}