using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion.Recepcionista
{
    public partial class ucHoraSlot : UserControl
    {
        // Propiedades públicas
        public string HoraInicio { get; private set; }
        public string HoraFin { get; private set; }
        public string HoraCompleta { get { return string.Format("{0} - {1}", HoraInicio, HoraFin); } }
        public int? CitaId { get; set; } // ID de la cita si está ocupado

        private bool _disponible = true;
        public bool Disponible
        {
            get => _disponible;
            set
            {
                _disponible = value;
                ActualizarEstadoVisual();
            }
        }

        private bool _seleccionado = false;
        public bool Seleccionado
        {
            get => _seleccionado;
            set
            {
                _seleccionado = value;
                ActualizarEstadoVisual();
            }
        }

        // Evento cuando se hace clic en el slot
        public event EventHandler SlotClicked;

        // Colores
        private Color colorDisponible = Color.FromArgb(46, 213, 115);      // Verde
        private Color colorOcupado = Color.FromArgb(255, 71, 87);          // Rojo
        private Color colorSeleccionado = Color.FromArgb(0, 152, 255);     // Azul
        private Color colorHover = Color.FromArgb(240, 240, 240);          // Gris claro

        public ucHoraSlot(string horaInicio, string horaFin, bool disponible = true)
        {
            InitializeComponent();

            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Disponible = disponible;

            // Configurar diseño
            ConfigurarControl();
            ActualizarEstadoVisual();
        }

        private void ConfigurarControl()
        {
            this.Size = new Size(215, 55);
            this.Cursor = Cursors.Hand;
            this.Margin = new Padding(5);

            // Label de hora
            lblHora.Text = HoraCompleta;
            lblHora.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHora.ForeColor = Color.Black;
            lblHora.TextAlign = ContentAlignment.MiddleLeft;
            lblHora.Dock = DockStyle.Fill;
            lblHora.Padding = new Padding(10, 0, 0, 0);

            // Label de estado
            lblEstado.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblEstado.TextAlign = ContentAlignment.MiddleCenter;
            lblEstado.Dock = DockStyle.Right;
            lblEstado.Width = 80;

            // Panel contenedor
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.BorderRadius = 10;
            panelContenedor.BorderThickness = 2;
            panelContenedor.Padding = new Padding(5);
            panelContenedor.ShadowDecoration.Enabled = true;
            panelContenedor.ShadowDecoration.Depth = 5;
            panelContenedor.ShadowDecoration.Shadow = new Padding(2);

            // Eventos
            this.Click += UcHoraSlot_Click;
            panelContenedor.Click += UcHoraSlot_Click;
            lblHora.Click += UcHoraSlot_Click;
            lblEstado.Click += UcHoraSlot_Click;

            this.MouseEnter += UcHoraSlot_MouseEnter;
            this.MouseLeave += UcHoraSlot_MouseLeave;
            panelContenedor.MouseEnter += UcHoraSlot_MouseEnter;
            panelContenedor.MouseLeave += UcHoraSlot_MouseLeave;
        }

        private void ActualizarEstadoVisual()
        {
            if (_seleccionado)
            {
                // Seleccionado
                panelContenedor.FillColor = colorSeleccionado;
                panelContenedor.BorderColor = colorSeleccionado;
                lblHora.ForeColor = Color.White;
                lblEstado.ForeColor = Color.White;
                lblEstado.Text = "SELECCIONADO";
            }
            else if (_disponible)
            {
                // Disponible
                panelContenedor.FillColor = Color.White;
                panelContenedor.BorderColor = colorDisponible;
                lblHora.ForeColor = Color.Black;
                lblEstado.ForeColor = colorDisponible;
                lblEstado.Text = "DISPONIBLE";
            }
            else
            {
                // Ocupado
                panelContenedor.FillColor = Color.FromArgb(250, 250, 250);
                panelContenedor.BorderColor = colorOcupado;
                lblHora.ForeColor = Color.Gray;
                lblEstado.ForeColor = colorOcupado;
                lblEstado.Text = "OCUPADO";
                this.Cursor = Cursors.No;
            }
        }

        private void UcHoraSlot_Click(object sender, EventArgs e)
        {
            // Permitir click tanto en disponibles (crear) como ocupados (editar)
            SlotClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UcHoraSlot_MouseEnter(object sender, EventArgs e)
        {
            if (_disponible && !_seleccionado)
            {
                panelContenedor.FillColor = colorHover;
            }
        }

        private void UcHoraSlot_MouseLeave(object sender, EventArgs e)
        {
            if (!_seleccionado)
            {
                ActualizarEstadoVisual();
            }
        }

        public void Deseleccionar()
        {
            Seleccionado = false;
        }
    }
}