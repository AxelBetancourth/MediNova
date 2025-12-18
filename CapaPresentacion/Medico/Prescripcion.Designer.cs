namespace CapaPresentacion.Medico
{
    partial class Prescripcion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                nReceta?.Dispose();
                nMedicamento?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFechaConsulta = new System.Windows.Forms.Label();
            this.lblNumeroConsulta = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelDatos = new Guna.UI2.WinForms.Guna2Panel();
            this.dtpFechaVencimiento = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIndicacionesGenerales = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiagnostico = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMedicamentos = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCancelarEdicion = new Guna.UI2.WinForms.Guna2Button();
            this.btnAgregarDetalle = new Guna.UI2.WinForms.Guna2Button();
            this.txtIndicaciones = new Guna.UI2.WinForms.Guna2TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numDuracion = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numCantidad = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDosis = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMedicamentoSeleccionado = new System.Windows.Forms.Label();
            this.btnBuscarMedicamento = new Guna.UI2.WinForms.Guna2Button();
            this.txtBuscarMedicamento = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            this.btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            this.panelTop.SuspendLayout();
            this.panelDatos.SuspendLayout();
            this.panelMedicamentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuracion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.panelTop.Controls.Add(this.lblFechaConsulta);
            this.panelTop.Controls.Add(this.lblNumeroConsulta);
            this.panelTop.Controls.Add(this.lblDNI);
            this.panelTop.Controls.Add(this.lblPaciente);
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1333, 123);
            this.panelTop.TabIndex = 0;
            // 
            // lblFechaConsulta
            // 
            this.lblFechaConsulta.AutoSize = true;
            this.lblFechaConsulta.Font = new System.Drawing.Font("Arial", 10F);
            this.lblFechaConsulta.ForeColor = System.Drawing.Color.White;
            this.lblFechaConsulta.Location = new System.Drawing.Point(800, 89);
            this.lblFechaConsulta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaConsulta.Name = "lblFechaConsulta";
            this.lblFechaConsulta.Size = new System.Drawing.Size(60, 19);
            this.lblFechaConsulta.TabIndex = 4;
            this.lblFechaConsulta.Text = "Fecha:";
            // 
            // lblNumeroConsulta
            // 
            this.lblNumeroConsulta.AutoSize = true;
            this.lblNumeroConsulta.Font = new System.Drawing.Font("Arial", 10F);
            this.lblNumeroConsulta.ForeColor = System.Drawing.Color.White;
            this.lblNumeroConsulta.Location = new System.Drawing.Point(800, 62);
            this.lblNumeroConsulta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumeroConsulta.Name = "lblNumeroConsulta";
            this.lblNumeroConsulta.Size = new System.Drawing.Size(77, 19);
            this.lblNumeroConsulta.TabIndex = 3;
            this.lblNumeroConsulta.Text = "Consulta:";
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Font = new System.Drawing.Font("Arial", 10F);
            this.lblDNI.ForeColor = System.Drawing.Color.White;
            this.lblDNI.Location = new System.Drawing.Point(29, 89);
            this.lblDNI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(42, 19);
            this.lblDNI.TabIndex = 2;
            this.lblDNI.Text = "DNI:";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Arial", 10F);
            this.lblPaciente.ForeColor = System.Drawing.Color.White;
            this.lblPaciente.Location = new System.Drawing.Point(29, 62);
            this.lblPaciente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(78, 19);
            this.lblPaciente.TabIndex = 1;
            this.lblPaciente.Text = "Paciente:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(27, 18);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(202, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Receta Médica";
            // 
            // panelDatos
            // 
            this.panelDatos.BackColor = System.Drawing.Color.White;
            this.panelDatos.Controls.Add(this.dtpFechaVencimiento);
            this.panelDatos.Controls.Add(this.label3);
            this.panelDatos.Controls.Add(this.txtIndicacionesGenerales);
            this.panelDatos.Controls.Add(this.label2);
            this.panelDatos.Controls.Add(this.txtDiagnostico);
            this.panelDatos.Controls.Add(this.label1);
            this.panelDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDatos.Location = new System.Drawing.Point(0, 123);
            this.panelDatos.Margin = new System.Windows.Forms.Padding(4);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelDatos.Size = new System.Drawing.Size(1333, 205);
            this.panelDatos.TabIndex = 1;
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.BorderRadius = 8;
            this.dtpFechaVencimiento.Checked = true;
            this.dtpFechaVencimiento.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.dtpFechaVencimiento.Font = new System.Drawing.Font("Arial", 9F);
            this.dtpFechaVencimiento.ForeColor = System.Drawing.Color.White;
            this.dtpFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(671, 43);
            this.dtpFechaVencimiento.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaVencimiento.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaVencimiento.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(267, 44);
            this.dtpFechaVencimiento.TabIndex = 5;
            this.dtpFechaVencimiento.Value = new System.DateTime(2025, 1, 18, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(667, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha de Vencimiento:";
            // 
            // txtIndicacionesGenerales
            // 
            this.txtIndicacionesGenerales.BorderRadius = 8;
            this.txtIndicacionesGenerales.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIndicacionesGenerales.DefaultText = "";
            this.txtIndicacionesGenerales.Font = new System.Drawing.Font("Arial", 9F);
            this.txtIndicacionesGenerales.Location = new System.Drawing.Point(31, 139);
            this.txtIndicacionesGenerales.Margin = new System.Windows.Forms.Padding(4);
            this.txtIndicacionesGenerales.Name = "txtIndicacionesGenerales";
            this.txtIndicacionesGenerales.PlaceholderText = "";
            this.txtIndicacionesGenerales.SelectedText = "";
            this.txtIndicacionesGenerales.Size = new System.Drawing.Size(600, 44);
            this.txtIndicacionesGenerales.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(27, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Indicaciones Generales:";
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.BorderRadius = 8;
            this.txtDiagnostico.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiagnostico.DefaultText = "";
            this.txtDiagnostico.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDiagnostico.Location = new System.Drawing.Point(31, 43);
            this.txtDiagnostico.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.PlaceholderText = "";
            this.txtDiagnostico.SelectedText = "";
            this.txtDiagnostico.Size = new System.Drawing.Size(600, 44);
            this.txtDiagnostico.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Diagnóstico:";
            // 
            // panelMedicamentos
            // 
            this.panelMedicamentos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelMedicamentos.Controls.Add(this.btnCancelarEdicion);
            this.panelMedicamentos.Controls.Add(this.btnAgregarDetalle);
            this.panelMedicamentos.Controls.Add(this.txtIndicaciones);
            this.panelMedicamentos.Controls.Add(this.label8);
            this.panelMedicamentos.Controls.Add(this.numDuracion);
            this.panelMedicamentos.Controls.Add(this.label7);
            this.panelMedicamentos.Controls.Add(this.numCantidad);
            this.panelMedicamentos.Controls.Add(this.label6);
            this.panelMedicamentos.Controls.Add(this.txtDosis);
            this.panelMedicamentos.Controls.Add(this.label5);
            this.panelMedicamentos.Controls.Add(this.lblMedicamentoSeleccionado);
            this.panelMedicamentos.Controls.Add(this.btnBuscarMedicamento);
            this.panelMedicamentos.Controls.Add(this.txtBuscarMedicamento);
            this.panelMedicamentos.Controls.Add(this.label4);
            this.panelMedicamentos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMedicamentos.Location = new System.Drawing.Point(0, 328);
            this.panelMedicamentos.Margin = new System.Windows.Forms.Padding(4);
            this.panelMedicamentos.Name = "panelMedicamentos";
            this.panelMedicamentos.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelMedicamentos.Size = new System.Drawing.Size(1333, 246);
            this.panelMedicamentos.TabIndex = 2;
            // 
            // btnCancelarEdicion
            // 
            this.btnCancelarEdicion.BorderRadius = 8;
            this.btnCancelarEdicion.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(87)))), ((int)(((byte)(34)))));
            this.btnCancelarEdicion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelarEdicion.ForeColor = System.Drawing.Color.White;
            this.btnCancelarEdicion.Location = new System.Drawing.Point(1129, 183);
            this.btnCancelarEdicion.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelarEdicion.Name = "btnCancelarEdicion";
            this.btnCancelarEdicion.Size = new System.Drawing.Size(173, 44);
            this.btnCancelarEdicion.TabIndex = 13;
            this.btnCancelarEdicion.Text = "Cancelar";
            this.btnCancelarEdicion.Visible = false;
            this.btnCancelarEdicion.Click += new System.EventHandler(this.btnCancelarEdicion_Click);
            // 
            // btnAgregarDetalle
            // 
            this.btnAgregarDetalle.BorderRadius = 8;
            this.btnAgregarDetalle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnAgregarDetalle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnAgregarDetalle.Location = new System.Drawing.Point(922, 183);
            this.btnAgregarDetalle.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarDetalle.Name = "btnAgregarDetalle";
            this.btnAgregarDetalle.Size = new System.Drawing.Size(187, 44);
            this.btnAgregarDetalle.TabIndex = 12;
            this.btnAgregarDetalle.Text = "Agregar";
            this.btnAgregarDetalle.Click += new System.EventHandler(this.btnAgregarDetalle_Click);
            // 
            // txtIndicaciones
            // 
            this.txtIndicaciones.BorderRadius = 8;
            this.txtIndicaciones.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIndicaciones.DefaultText = "";
            this.txtIndicaciones.Font = new System.Drawing.Font("Arial", 9F);
            this.txtIndicaciones.Location = new System.Drawing.Point(673, 183);
            this.txtIndicaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtIndicaciones.Name = "txtIndicaciones";
            this.txtIndicaciones.PlaceholderText = "Ej: Tomar con alimentos";
            this.txtIndicaciones.SelectedText = "";
            this.txtIndicaciones.Size = new System.Drawing.Size(227, 44);
            this.txtIndicaciones.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(669, 155);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 18);
            this.label8.TabIndex = 10;
            this.label8.Text = "Indicaciones:";
            // 
            // numDuracion
            // 
            this.numDuracion.BackColor = System.Drawing.Color.Transparent;
            this.numDuracion.BorderRadius = 8;
            this.numDuracion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numDuracion.Font = new System.Drawing.Font("Arial", 9F);
            this.numDuracion.Location = new System.Drawing.Point(513, 183);
            this.numDuracion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numDuracion.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numDuracion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDuracion.Name = "numDuracion";
            this.numDuracion.Size = new System.Drawing.Size(133, 44);
            this.numDuracion.TabIndex = 9;
            this.numDuracion.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.numDuracion.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(509, 155);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 18);
            this.label7.TabIndex = 8;
            this.label7.Text = "Duración (días):";
            // 
            // numCantidad
            // 
            this.numCantidad.BackColor = System.Drawing.Color.Transparent;
            this.numCantidad.BorderRadius = 8;
            this.numCantidad.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numCantidad.Font = new System.Drawing.Font("Arial", 9F);
            this.numCantidad.Location = new System.Drawing.Point(353, 183);
            this.numCantidad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numCantidad.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(133, 44);
            this.numCantidad.TabIndex = 7;
            this.numCantidad.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.numCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(349, 155);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "Cantidad:";
            // 
            // txtDosis
            // 
            this.txtDosis.BorderRadius = 8;
            this.txtDosis.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDosis.DefaultText = "";
            this.txtDosis.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDosis.Location = new System.Drawing.Point(33, 183);
            this.txtDosis.Margin = new System.Windows.Forms.Padding(4);
            this.txtDosis.Name = "txtDosis";
            this.txtDosis.PlaceholderText = "Ej: 1 tableta cada 8 horas";
            this.txtDosis.SelectedText = "";
            this.txtDosis.Size = new System.Drawing.Size(293, 44);
            this.txtDosis.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(29, 155);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Dosis:";
            // 
            // lblMedicamentoSeleccionado
            // 
            this.lblMedicamentoSeleccionado.AutoSize = true;
            this.lblMedicamentoSeleccionado.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic);
            this.lblMedicamentoSeleccionado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblMedicamentoSeleccionado.Location = new System.Drawing.Point(627, 55);
            this.lblMedicamentoSeleccionado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMedicamentoSeleccionado.Name = "lblMedicamentoSeleccionado";
            this.lblMedicamentoSeleccionado.Size = new System.Drawing.Size(152, 17);
            this.lblMedicamentoSeleccionado.TabIndex = 3;
            this.lblMedicamentoSeleccionado.Text = "Ninguno seleccionado";
            // 
            // btnBuscarMedicamento
            // 
            this.btnBuscarMedicamento.BorderRadius = 8;
            this.btnBuscarMedicamento.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnBuscarMedicamento.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscarMedicamento.ForeColor = System.Drawing.Color.White;
            this.btnBuscarMedicamento.Location = new System.Drawing.Point(447, 43);
            this.btnBuscarMedicamento.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarMedicamento.Name = "btnBuscarMedicamento";
            this.btnBuscarMedicamento.Size = new System.Drawing.Size(160, 44);
            this.btnBuscarMedicamento.TabIndex = 2;
            this.btnBuscarMedicamento.Text = "Buscar";
            this.btnBuscarMedicamento.Click += new System.EventHandler(this.btnBuscarMedicamento_Click);
            // 
            // txtBuscarMedicamento
            // 
            this.txtBuscarMedicamento.BorderRadius = 8;
            this.txtBuscarMedicamento.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBuscarMedicamento.DefaultText = "";
            this.txtBuscarMedicamento.Font = new System.Drawing.Font("Arial", 9F);
            this.txtBuscarMedicamento.Location = new System.Drawing.Point(31, 43);
            this.txtBuscarMedicamento.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuscarMedicamento.Name = "txtBuscarMedicamento";
            this.txtBuscarMedicamento.PlaceholderText = "Nombre del medicamento...";
            this.txtBuscarMedicamento.SelectedText = "";
            this.txtBuscarMedicamento.Size = new System.Drawing.Size(400, 44);
            this.txtBuscarMedicamento.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(27, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Buscar Medicamento:";
            // 
            // panelGrid
            // 
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.Controls.Add(this.dgvDetalles);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 574);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(4);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelGrid.Size = new System.Drawing.Size(1333, 226);
            this.panelGrid.TabIndex = 3;
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.AllowUserToAddRows = false;
            this.dgvDetalles.AllowUserToDeleteRows = false;
            this.dgvDetalles.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalles.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalles.Location = new System.Drawing.Point(27, 25);
            this.dgvDetalles.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.ReadOnly = true;
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.RowHeadersWidth = 51;
            this.dgvDetalles.RowTemplate.Height = 32;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(1279, 176);
            this.dgvDetalles.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelButtons.Controls.Add(this.btnCancelar);
            this.panelButtons.Controls.Add(this.btnGuardar);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 800);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelButtons.Size = new System.Drawing.Size(1333, 98);
            this.panelButtons.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BorderRadius = 8;
            this.btnCancelar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(667, 25);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(240, 55);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BorderRadius = 8;
            this.btnGuardar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnGuardar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(933, 25);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(240, 55);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar Receta";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // Prescripcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 898);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelMedicamentos);
            this.Controls.Add(this.panelDatos);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Prescripcion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receta Médica";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            this.panelMedicamentos.ResumeLayout(false);
            this.panelMedicamentos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuracion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.Label lblNumeroConsulta;
        private System.Windows.Forms.Label lblFechaConsulta;
        private Guna.UI2.WinForms.Guna2Panel panelDatos;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtDiagnostico;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtIndicacionesGenerales;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaVencimiento;
        private Guna.UI2.WinForms.Guna2Panel panelMedicamentos;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox txtBuscarMedicamento;
        private Guna.UI2.WinForms.Guna2Button btnBuscarMedicamento;
        private System.Windows.Forms.Label lblMedicamentoSeleccionado;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox txtDosis;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2NumericUpDown numCantidad;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2NumericUpDown numDuracion;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2TextBox txtIndicaciones;
        private Guna.UI2.WinForms.Guna2Button btnAgregarDetalle;
        private Guna.UI2.WinForms.Guna2Button btnCancelarEdicion;
        private Guna.UI2.WinForms.Guna2Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private Guna.UI2.WinForms.Guna2Panel panelButtons;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
    }
}
