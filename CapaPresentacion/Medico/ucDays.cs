using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion.Medico
{
    public partial class ucDays : UserControl
    {
        public event EventHandler DayClicked;
        public event Action<string> DayDoubleClicked;

        private string _day;
        private bool isSelected = false;
        private bool _isToday = false;

        // Propiedad pública para acceder al día
        public new string Text
        {
            get { return _day; }
        }

        // Colores
        private Color todayFillColor = Color.FromArgb(0, 152, 255);      // Azul para "hoy"
        private Color selectionFillColor = Color.FromArgb(35, 38, 47);   // Oscuro para "seleccionado"
        private Color defaultFillColor = Color.White;                     // Blanco por defecto
        private Color defaultBorderColor;

        public ucDays(string day)
        {
            InitializeComponent();

            _day = day;
            guna2HtmlLabel2.Text = day;
            checkBox1.Hide();

            // 🔹 CONFIGURACIÓN RESPONSIVE
            this.AutoSize = false;
            this.MinimumSize = new Size(50, 45);

            // Hacer que el panel interno ocupe todo el espacio
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Padding = new Padding(2);

            // Centrar el label del número
            guna2HtmlLabel2.Dock = DockStyle.Fill;
            guna2HtmlLabel2.AutoSize = false;

            // Guardar color de borde por defecto
            defaultBorderColor = guna2Panel1.BorderColor;

            // Eventos de click solo si tiene día
            if (!string.IsNullOrEmpty(day))
            {
                this.Click += UcDays_Click;
                this.DoubleClick += UcDays_DoubleClick;
                guna2Panel1.Click += UcDays_Click;
                guna2Panel1.DoubleClick += UcDays_DoubleClick;
                guna2HtmlLabel2.Click += UcDays_Click;
                guna2HtmlLabel2.DoubleClick += UcDays_DoubleClick;
            }

            UpdateVisualState();
        }

        public bool IsToday
        {
            get { return _isToday; }
            set
            {
                _isToday = value;
                UpdateVisualState();
            }
        }

        public void Seleccionar()
        {
            isSelected = true;
            UpdateVisualState();
        }

        public void Deseleccionar()
        {
            isSelected = false;
            UpdateVisualState();
        }

        // 🔹 ACTUALIZAR ESTADO VISUAL
        private void UpdateVisualState()
        {
            guna2HtmlLabel2.BackColor = Color.Transparent;

            if (isSelected)
            {
                // ESTADO SELECCIONADO: Fondo oscuro, texto blanco
                guna2Panel1.BackColor = selectionFillColor;
                guna2Panel1.BorderColor = selectionFillColor;
                guna2HtmlLabel2.ForeColor = Color.White;
                this.BackColor = selectionFillColor;
            }
            else if (_isToday)
            {
                // ESTADO "HOY": Fondo azul, texto blanco
                guna2Panel1.BackColor = todayFillColor;
                guna2Panel1.BorderColor = todayFillColor;
                guna2HtmlLabel2.ForeColor = Color.White;
                this.BackColor = todayFillColor;
            }
            else
            {
                // ESTADO POR DEFECTO: Fondo blanco, borde por defecto, texto negro
                guna2Panel1.BackColor = defaultFillColor;
                guna2Panel1.BorderColor = defaultBorderColor;
                guna2HtmlLabel2.ForeColor = Color.Black;
                this.BackColor = defaultFillColor;
            }
        }

        // Eventos de Click
        private void UcDays_Click(object sender, EventArgs e)
        {
            DayClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UcDays_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_day))
            {
                DayDoubleClicked?.Invoke(_day);
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            // Evento vacío
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}