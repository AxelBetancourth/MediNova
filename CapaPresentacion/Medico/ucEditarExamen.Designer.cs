namespace CapaPresentacion.Medico
{
    partial class ucEditarExamen : System.Windows.Forms.UserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelPrincipal = new Guna.UI2.WinForms.Guna2Panel();
            this.panelContenido = new Guna.UI2.WinForms.Guna2Panel();
            this.panelFormulario = new Guna.UI2.WinForms.Guna2Panel();
            this.txtLugarRealizacion = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLugarRealizacion = new System.Windows.Forms.Label();
            this.chkEsExterno = new Guna.UI2.WinForms.Guna2CheckBox();
            this.txtResultado = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblResultado = new System.Windows.Forms.Label();
            this.chkTieneResultado = new Guna.UI2.WinForms.Guna2CheckBox();
            this.dtpFechaResultado = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblFechaResultado = new System.Windows.Forms.Label();
            this.dtpFechaSolicitud = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblFechaSolicitud = new System.Windows.Forms.Label();
            this.cboEstado = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtCosto = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblCosto = new System.Windows.Forms.Label();
            this.cboTipo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.txtNombre = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.panelBotones = new Guna.UI2.WinForms.Guna2Panel();
            this.BtnCancelar = new Guna.UI2.WinForms.Guna2Button();
            this.BtnGuardar = new Guna.UI2.WinForms.Guna2Button();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.BtnVolver = new Guna.UI2.WinForms.Guna2Button();
            this.panelPrincipal.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.Controls.Add(this.panelContenido);
            this.panelPrincipal.Controls.Add(this.panelHeader);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.panelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipal.Margin = new System.Windows.Forms.Padding(4);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelPrincipal.Size = new System.Drawing.Size(1600, 862);
            this.panelPrincipal.TabIndex = 0;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.Transparent;
            this.panelContenido.BorderRadius = 15;
            this.panelContenido.Controls.Add(this.panelFormulario);
            this.panelContenido.Controls.Add(this.panelBotones);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.FillColor = System.Drawing.Color.White;
            this.panelContenido.Location = new System.Drawing.Point(27, 111);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(4);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(40, 37, 40, 37);
            this.panelContenido.ShadowDecoration.BorderRadius = 15;
            this.panelContenido.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelContenido.ShadowDecoration.Depth = 10;
            this.panelContenido.ShadowDecoration.Enabled = true;
            this.panelContenido.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.panelContenido.Size = new System.Drawing.Size(1546, 726);
            this.panelContenido.TabIndex = 1;
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.txtLugarRealizacion);
            this.panelFormulario.Controls.Add(this.lblLugarRealizacion);
            this.panelFormulario.Controls.Add(this.chkEsExterno);
            this.panelFormulario.Controls.Add(this.txtResultado);
            this.panelFormulario.Controls.Add(this.lblResultado);
            this.panelFormulario.Controls.Add(this.chkTieneResultado);
            this.panelFormulario.Controls.Add(this.dtpFechaResultado);
            this.panelFormulario.Controls.Add(this.lblFechaResultado);
            this.panelFormulario.Controls.Add(this.dtpFechaSolicitud);
            this.panelFormulario.Controls.Add(this.lblFechaSolicitud);
            this.panelFormulario.Controls.Add(this.cboEstado);
            this.panelFormulario.Controls.Add(this.lblEstado);
            this.panelFormulario.Controls.Add(this.txtCosto);
            this.panelFormulario.Controls.Add(this.lblCosto);
            this.panelFormulario.Controls.Add(this.cboTipo);
            this.panelFormulario.Controls.Add(this.lblTipo);
            this.panelFormulario.Controls.Add(this.txtNombre);
            this.panelFormulario.Controls.Add(this.lblNombre);
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormulario.FillColor = System.Drawing.Color.White;
            this.panelFormulario.Location = new System.Drawing.Point(40, 37);
            this.panelFormulario.Margin = new System.Windows.Forms.Padding(4);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Size = new System.Drawing.Size(1466, 566);
            this.panelFormulario.TabIndex = 1;
            // 
            // txtLugarRealizacion
            // 
            this.txtLugarRealizacion.BorderRadius = 8;
            this.txtLugarRealizacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLugarRealizacion.DefaultText = "";
            this.txtLugarRealizacion.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLugarRealizacion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLugarRealizacion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLugarRealizacion.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLugarRealizacion.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLugarRealizacion.Font = new System.Drawing.Font("Arial", 10F);
            this.txtLugarRealizacion.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLugarRealizacion.Location = new System.Drawing.Point(1012, 172);
            this.txtLugarRealizacion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLugarRealizacion.Name = "txtLugarRealizacion";
            this.txtLugarRealizacion.PlaceholderText = "Ingrese el nombre del laboratorio o lugar donde se realizará";
            this.txtLugarRealizacion.SelectedText = "";
            this.txtLugarRealizacion.Size = new System.Drawing.Size(427, 44);
            this.txtLugarRealizacion.TabIndex = 17;
            // 
            // lblLugarRealizacion
            // 
            this.lblLugarRealizacion.AutoSize = true;
            this.lblLugarRealizacion.BackColor = System.Drawing.Color.Transparent;
            this.lblLugarRealizacion.Font = new System.Drawing.Font("Arial", 10F);
            this.lblLugarRealizacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLugarRealizacion.Location = new System.Drawing.Point(1012, 141);
            this.lblLugarRealizacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLugarRealizacion.Name = "lblLugarRealizacion";
            this.lblLugarRealizacion.Size = new System.Drawing.Size(168, 19);
            this.lblLugarRealizacion.TabIndex = 16;
            this.lblLugarRealizacion.Text = "Lugar de Realización:";
            // 
            // chkEsExterno
            // 
            this.chkEsExterno.AutoSize = true;
            this.chkEsExterno.BackColor = System.Drawing.Color.Transparent;
            this.chkEsExterno.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.chkEsExterno.CheckedState.BorderRadius = 2;
            this.chkEsExterno.CheckedState.BorderThickness = 2;
            this.chkEsExterno.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.chkEsExterno.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.chkEsExterno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.chkEsExterno.Location = new System.Drawing.Point(1012, 105);
            this.chkEsExterno.Margin = new System.Windows.Forms.Padding(4);
            this.chkEsExterno.Name = "chkEsExterno";
            this.chkEsExterno.Size = new System.Drawing.Size(174, 23);
            this.chkEsExterno.TabIndex = 15;
            this.chkEsExterno.Text = "✓ Examen externo";
            this.chkEsExterno.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkEsExterno.UncheckedState.BorderRadius = 2;
            this.chkEsExterno.UncheckedState.BorderThickness = 2;
            this.chkEsExterno.UncheckedState.FillColor = System.Drawing.Color.White;
            this.chkEsExterno.UseVisualStyleBackColor = false;
            this.chkEsExterno.CheckedChanged += new System.EventHandler(this.ChkEsExterno_CheckedChanged);
            // 
            // txtResultado
            // 
            this.txtResultado.BorderRadius = 8;
            this.txtResultado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtResultado.DefaultText = "";
            this.txtResultado.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtResultado.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtResultado.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtResultado.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtResultado.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtResultado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtResultado.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtResultado.Location = new System.Drawing.Point(40, 357);
            this.txtResultado.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.PlaceholderText = "Ingrese los resultados del examen";
            this.txtResultado.SelectedText = "";
            this.txtResultado.Size = new System.Drawing.Size(933, 185);
            this.txtResultado.TabIndex = 14;
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.BackColor = System.Drawing.Color.Transparent;
            this.lblResultado.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblResultado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblResultado.Location = new System.Drawing.Point(40, 326);
            this.lblResultado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(236, 25);
            this.lblResultado.TabIndex = 13;
            this.lblResultado.Text = "📝 Resultado del Examen:";
            // 
            // chkTieneResultado
            // 
            this.chkTieneResultado.AutoSize = true;
            this.chkTieneResultado.BackColor = System.Drawing.Color.Transparent;
            this.chkTieneResultado.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkTieneResultado.CheckedState.BorderRadius = 2;
            this.chkTieneResultado.CheckedState.BorderThickness = 1;
            this.chkTieneResultado.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkTieneResultado.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkTieneResultado.Location = new System.Drawing.Point(760, 279);
            this.chkTieneResultado.Margin = new System.Windows.Forms.Padding(4);
            this.chkTieneResultado.Name = "chkTieneResultado";
            this.chkTieneResultado.Size = new System.Drawing.Size(149, 27);
            this.chkTieneResultado.TabIndex = 12;
            this.chkTieneResultado.Text = "Tiene resultado";
            this.chkTieneResultado.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkTieneResultado.UncheckedState.BorderRadius = 2;
            this.chkTieneResultado.UncheckedState.BorderThickness = 1;
            this.chkTieneResultado.UncheckedState.FillColor = System.Drawing.Color.White;
            this.chkTieneResultado.UseVisualStyleBackColor = false;
            this.chkTieneResultado.CheckedChanged += new System.EventHandler(this.ChkTieneResultado_CheckedChanged);
            // 
            // dtpFechaResultado
            // 
            this.dtpFechaResultado.BackColor = System.Drawing.Color.Transparent;
            this.dtpFechaResultado.BorderRadius = 8;
            this.dtpFechaResultado.Checked = true;
            this.dtpFechaResultado.FillColor = System.Drawing.Color.White;
            this.dtpFechaResultado.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpFechaResultado.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaResultado.Location = new System.Drawing.Point(520, 271);
            this.dtpFechaResultado.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaResultado.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaResultado.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaResultado.Name = "dtpFechaResultado";
            this.dtpFechaResultado.Size = new System.Drawing.Size(213, 44);
            this.dtpFechaResultado.TabIndex = 11;
            this.dtpFechaResultado.Value = new System.DateTime(2025, 1, 25, 0, 0, 0, 0);
            // 
            // lblFechaResultado
            // 
            this.lblFechaResultado.AutoSize = true;
            this.lblFechaResultado.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaResultado.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblFechaResultado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblFechaResultado.Location = new System.Drawing.Point(520, 240);
            this.lblFechaResultado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaResultado.Name = "lblFechaResultado";
            this.lblFechaResultado.Size = new System.Drawing.Size(213, 25);
            this.lblFechaResultado.TabIndex = 10;
            this.lblFechaResultado.Text = "📅 Fecha de Resultado:";
            // 
            // dtpFechaSolicitud
            // 
            this.dtpFechaSolicitud.BackColor = System.Drawing.Color.Transparent;
            this.dtpFechaSolicitud.BorderRadius = 8;
            this.dtpFechaSolicitud.Checked = true;
            this.dtpFechaSolicitud.FillColor = System.Drawing.Color.White;
            this.dtpFechaSolicitud.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpFechaSolicitud.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaSolicitud.Location = new System.Drawing.Point(40, 271);
            this.dtpFechaSolicitud.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaSolicitud.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaSolicitud.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaSolicitud.Name = "dtpFechaSolicitud";
            this.dtpFechaSolicitud.Size = new System.Drawing.Size(453, 44);
            this.dtpFechaSolicitud.TabIndex = 9;
            this.dtpFechaSolicitud.Value = new System.DateTime(2025, 1, 25, 0, 0, 0, 0);
            // 
            // lblFechaSolicitud
            // 
            this.lblFechaSolicitud.AutoSize = true;
            this.lblFechaSolicitud.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaSolicitud.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblFechaSolicitud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblFechaSolicitud.Location = new System.Drawing.Point(40, 240);
            this.lblFechaSolicitud.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaSolicitud.Name = "lblFechaSolicitud";
            this.lblFechaSolicitud.Size = new System.Drawing.Size(202, 25);
            this.lblFechaSolicitud.TabIndex = 8;
            this.lblFechaSolicitud.Text = "📅 Fecha de Solicitud:";
            // 
            // cboEstado
            // 
            this.cboEstado.BackColor = System.Drawing.Color.Transparent;
            this.cboEstado.BorderRadius = 8;
            this.cboEstado.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboEstado.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboEstado.ItemHeight = 30;
            this.cboEstado.Items.AddRange(new object[] {
            "Pendiente",
            "Completado",
            "Pagado",
            "Cancelado"});
            this.cboEstado.Location = new System.Drawing.Point(520, 172);
            this.cboEstado.Margin = new System.Windows.Forms.Padding(4);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(452, 36);
            this.cboEstado.TabIndex = 7;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblEstado.Location = new System.Drawing.Point(520, 142);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(105, 25);
            this.lblEstado.TabIndex = 6;
            this.lblEstado.Text = "📊 Estado:";
            // 
            // txtCosto
            // 
            this.txtCosto.BorderRadius = 8;
            this.txtCosto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCosto.DefaultText = "";
            this.txtCosto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCosto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCosto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCosto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCosto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCosto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCosto.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCosto.Location = new System.Drawing.Point(40, 172);
            this.txtCosto.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.PlaceholderText = "0.00";
            this.txtCosto.SelectedText = "";
            this.txtCosto.Size = new System.Drawing.Size(453, 44);
            this.txtCosto.TabIndex = 5;
            // 
            // lblCosto
            // 
            this.lblCosto.AutoSize = true;
            this.lblCosto.BackColor = System.Drawing.Color.Transparent;
            this.lblCosto.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblCosto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCosto.Location = new System.Drawing.Point(40, 142);
            this.lblCosto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(191, 25);
            this.lblCosto.TabIndex = 4;
            this.lblCosto.Text = "💰 Costo (Lempiras):";
            // 
            // cboTipo
            // 
            this.cboTipo.BackColor = System.Drawing.Color.Transparent;
            this.cboTipo.BorderRadius = 8;
            this.cboTipo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboTipo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTipo.ItemHeight = 30;
            this.cboTipo.Items.AddRange(new object[] {
            "Sangre",
            "Orina",
            "Heces",
            "Rayos X",
            "Ecografía",
            "Tomografía",
            "Resonancia Magnética",
            "Electrocardiograma",
            "Biopsia",
            "Cultivo",
            "Otros"});
            this.cboTipo.Location = new System.Drawing.Point(520, 74);
            this.cboTipo.Margin = new System.Windows.Forms.Padding(4);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(452, 36);
            this.cboTipo.TabIndex = 3;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.BackColor = System.Drawing.Color.Transparent;
            this.lblTipo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTipo.Location = new System.Drawing.Point(520, 43);
            this.lblTipo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(184, 25);
            this.lblTipo.TabIndex = 2;
            this.lblTipo.Text = "🔬 Tipo de Examen:";
            // 
            // txtNombre
            // 
            this.txtNombre.BorderRadius = 8;
            this.txtNombre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombre.DefaultText = "";
            this.txtNombre.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNombre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNombre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombre.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombre.Location = new System.Drawing.Point(40, 74);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.PlaceholderText = "Ingrese el nombre del examen";
            this.txtNombre.SelectedText = "";
            this.txtNombre.Size = new System.Drawing.Size(453, 44);
            this.txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblNombre.Location = new System.Drawing.Point(40, 43);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(220, 25);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "📝 Nombre del Examen:";
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.BtnCancelar);
            this.panelBotones.Controls.Add(this.BtnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.FillColor = System.Drawing.Color.White;
            this.panelBotones.Location = new System.Drawing.Point(40, 603);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(4);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1466, 86);
            this.panelBotones.TabIndex = 0;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnCancelar.BorderRadius = 10;
            this.BtnCancelar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnCancelar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnCancelar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnCancelar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnCancelar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnCancelar.ForeColor = System.Drawing.Color.White;
            this.BtnCancelar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(35)))), ((int)(((byte)(51)))));
            this.BtnCancelar.Location = new System.Drawing.Point(1252, 18);
            this.BtnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(187, 55);
            this.BtnCancelar.TabIndex = 1;
            this.BtnCancelar.Text = "✕ Cancelar";
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnGuardar.BorderRadius = 10;
            this.BtnGuardar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnGuardar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnGuardar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnGuardar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnGuardar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.BtnGuardar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.BtnGuardar.ForeColor = System.Drawing.Color.White;
            this.BtnGuardar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(58)))));
            this.BtnGuardar.Location = new System.Drawing.Point(1039, 18);
            this.BtnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(187, 55);
            this.BtnGuardar.TabIndex = 0;
            this.BtnGuardar.Text = "✓ Guardar";
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Transparent;
            this.panelHeader.BorderRadius = 15;
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Controls.Add(this.BtnVolver);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.FillColor = System.Drawing.Color.White;
            this.panelHeader.Location = new System.Drawing.Point(27, 25);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelHeader.ShadowDecoration.BorderRadius = 15;
            this.panelHeader.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelHeader.ShadowDecoration.Depth = 8;
            this.panelHeader.ShadowDecoration.Enabled = true;
            this.panelHeader.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.panelHeader.Size = new System.Drawing.Size(1546, 86);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTitulo.Location = new System.Drawing.Point(31, 25);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(336, 41);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "📋 Gestión de Examen";
            // 
            // BtnVolver
            // 
            this.BtnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnVolver.BorderRadius = 5;
            this.BtnVolver.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnVolver.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnVolver.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnVolver.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnVolver.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.BtnVolver.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.BtnVolver.ForeColor = System.Drawing.Color.White;
            this.BtnVolver.Location = new System.Drawing.Point(1368, 21);
            this.BtnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(147, 44);
            this.BtnVolver.TabIndex = 0;
            this.BtnVolver.Text = "← Volver";
            this.BtnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
            // 
            // ucEditarExamen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelPrincipal);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucEditarExamen";
            this.Size = new System.Drawing.Size(1600, 862);
            this.panelPrincipal.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelPrincipal;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private Guna.UI2.WinForms.Guna2Button BtnVolver;
        private Guna.UI2.WinForms.Guna2Panel panelContenido;
        private Guna.UI2.WinForms.Guna2Panel panelFormulario;
        private Guna.UI2.WinForms.Guna2Panel panelBotones;
        private Guna.UI2.WinForms.Guna2Button BtnGuardar;
        private Guna.UI2.WinForms.Guna2Button BtnCancelar;
        private System.Windows.Forms.Label lblNombre;
        private Guna.UI2.WinForms.Guna2TextBox txtNombre;
        private System.Windows.Forms.Label lblTipo;
        private Guna.UI2.WinForms.Guna2ComboBox cboTipo;
        private System.Windows.Forms.Label lblCosto;
        private Guna.UI2.WinForms.Guna2TextBox txtCosto;
        private System.Windows.Forms.Label lblEstado;
        private Guna.UI2.WinForms.Guna2ComboBox cboEstado;
        private System.Windows.Forms.Label lblFechaSolicitud;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaSolicitud;
        private System.Windows.Forms.Label lblFechaResultado;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaResultado;
        private Guna.UI2.WinForms.Guna2CheckBox chkTieneResultado;
        private System.Windows.Forms.Label lblResultado;
        private Guna.UI2.WinForms.Guna2TextBox txtResultado;
        private Guna.UI2.WinForms.Guna2CheckBox chkEsExterno;
        private System.Windows.Forms.Label lblLugarRealizacion;
        private Guna.UI2.WinForms.Guna2TextBox txtLugarRealizacion;
    }
}
