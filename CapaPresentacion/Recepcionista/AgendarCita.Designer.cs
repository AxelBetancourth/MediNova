namespace CapaPresentacion.Recepcionista
{
    partial class AgendarCita
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

        private void InitializeComponent()
        {
            this.lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelFormulario = new Guna.UI2.WinForms.Guna2Panel();
            this.chkTodoElDia = new Guna.UI2.WinForms.Guna2CheckBox();
            this.txtUbicacion = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.cmbEstado = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.dtpFechaFin = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.dtpHoraFin = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpHoraInicio = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblHoraFin = new System.Windows.Forms.Label();
            this.lblHoraInicio = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.txtAsunto = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblAsunto = new System.Windows.Forms.Label();
            this.cmbDoctor = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.btnBuscarPaciente = new Guna.UI2.WinForms.Guna2Button();
            this.txtPaciente = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.btnGuardar = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            this.btnRegresar = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegresar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
            this.lblTitulo.Location = new System.Drawing.Point(40, 25);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(205, 47);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Agendar Cita";
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFormulario.AutoScroll = true;
            this.panelFormulario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelFormulario.BorderRadius = 10;
            this.panelFormulario.BorderThickness = 1;
            this.panelFormulario.Controls.Add(this.chkTodoElDia);
            this.panelFormulario.Controls.Add(this.txtUbicacion);
            this.panelFormulario.Controls.Add(this.lblUbicacion);
            this.panelFormulario.Controls.Add(this.cmbEstado);
            this.panelFormulario.Controls.Add(this.lblEstado);
            this.panelFormulario.Controls.Add(this.dtpFechaFin);
            this.panelFormulario.Controls.Add(this.lblFechaFin);
            this.panelFormulario.Controls.Add(this.dtpHoraFin);
            this.panelFormulario.Controls.Add(this.dtpHoraInicio);
            this.panelFormulario.Controls.Add(this.lblHoraFin);
            this.panelFormulario.Controls.Add(this.lblHoraInicio);
            this.panelFormulario.Controls.Add(this.dtpFechaInicio);
            this.panelFormulario.Controls.Add(this.lblFechaInicio);
            this.panelFormulario.Controls.Add(this.txtAsunto);
            this.panelFormulario.Controls.Add(this.lblAsunto);
            this.panelFormulario.Controls.Add(this.cmbDoctor);
            this.panelFormulario.Controls.Add(this.lblDoctor);
            this.panelFormulario.Controls.Add(this.btnBuscarPaciente);
            this.panelFormulario.Controls.Add(this.txtPaciente);
            this.panelFormulario.Controls.Add(this.lblPaciente);
            this.panelFormulario.Location = new System.Drawing.Point(40, 98);
            this.panelFormulario.Margin = new System.Windows.Forms.Padding(4);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.panelFormulario.Size = new System.Drawing.Size(1227, 566);
            this.panelFormulario.TabIndex = 1;
            // 
            // chkTodoElDia
            // 
            this.chkTodoElDia.AutoSize = true;
            this.chkTodoElDia.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkTodoElDia.CheckedState.BorderRadius = 0;
            this.chkTodoElDia.CheckedState.BorderThickness = 0;
            this.chkTodoElDia.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkTodoElDia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkTodoElDia.Location = new System.Drawing.Point(653, 331);
            this.chkTodoElDia.Margin = new System.Windows.Forms.Padding(4);
            this.chkTodoElDia.Name = "chkTodoElDia";
            this.chkTodoElDia.Size = new System.Drawing.Size(115, 27);
            this.chkTodoElDia.TabIndex = 19;
            this.chkTodoElDia.Text = "Todo el día";
            this.chkTodoElDia.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkTodoElDia.UncheckedState.BorderRadius = 0;
            this.chkTodoElDia.UncheckedState.BorderThickness = 0;
            this.chkTodoElDia.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            // 
            // txtUbicacion
            // 
            this.txtUbicacion.BorderRadius = 8;
            this.txtUbicacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUbicacion.DefaultText = "";
            this.txtUbicacion.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtUbicacion.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtUbicacion.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUbicacion.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtUbicacion.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUbicacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUbicacion.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUbicacion.Location = new System.Drawing.Point(40, 409);
            this.txtUbicacion.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.PlaceholderText = "Ej: Consultorio 1";
            this.txtUbicacion.SelectedText = "";
            this.txtUbicacion.Size = new System.Drawing.Size(533, 49);
            this.txtUbicacion.TabIndex = 18;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUbicacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUbicacion.Location = new System.Drawing.Point(40, 378);
            this.lblUbicacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(88, 23);
            this.lblUbicacion.TabIndex = 17;
            this.lblUbicacion.Text = "Ubicación";
            // 
            // cmbEstado
            // 
            this.cmbEstado.BackColor = System.Drawing.Color.Transparent;
            this.cmbEstado.BorderRadius = 8;
            this.cmbEstado.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbEstado.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbEstado.ItemHeight = 34;
            this.cmbEstado.Items.AddRange(new object[] {
            "Pendiente",
            "Confirmada",
            "Cancelada",
            "Completada"});
            this.cmbEstado.Location = new System.Drawing.Point(653, 409);
            this.cmbEstado.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(532, 40);
            this.cmbEstado.StartIndex = 0;
            this.cmbEstado.TabIndex = 16;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEstado.Location = new System.Drawing.Point(653, 378);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(63, 23);
            this.lblEstado.TabIndex = 15;
            this.lblEstado.Text = "Estado";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.BorderRadius = 8;
            this.dtpFechaFin.Checked = true;
            this.dtpFechaFin.FillColor = System.Drawing.Color.White;
            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(653, 271);
            this.dtpFechaFin.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFin.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaFin.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(267, 49);
            this.dtpFechaFin.TabIndex = 14;
            this.dtpFechaFin.Value = new System.DateTime(2025, 11, 16, 0, 0, 0, 0);
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFechaFin.Location = new System.Drawing.Point(653, 240);
            this.lblFechaFin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(84, 23);
            this.lblFechaFin.TabIndex = 13;
            this.lblFechaFin.Text = "Fecha Fin";
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.BorderRadius = 8;
            this.dtpHoraFin.Checked = true;
            this.dtpHoraFin.FillColor = System.Drawing.Color.White;
            this.dtpHoraFin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraFin.Location = new System.Drawing.Point(960, 271);
            this.dtpHoraFin.Margin = new System.Windows.Forms.Padding(4);
            this.dtpHoraFin.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHoraFin.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.ShowUpDown = true;
            this.dtpHoraFin.Size = new System.Drawing.Size(227, 49);
            this.dtpHoraFin.TabIndex = 12;
            this.dtpHoraFin.Value = new System.DateTime(2025, 11, 16, 0, 0, 0, 0);
            // 
            // dtpHoraInicio
            // 
            this.dtpHoraInicio.BorderRadius = 8;
            this.dtpHoraInicio.Checked = true;
            this.dtpHoraInicio.FillColor = System.Drawing.Color.White;
            this.dtpHoraInicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraInicio.Location = new System.Drawing.Point(347, 271);
            this.dtpHoraInicio.Margin = new System.Windows.Forms.Padding(4);
            this.dtpHoraInicio.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpHoraInicio.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpHoraInicio.Name = "dtpHoraInicio";
            this.dtpHoraInicio.ShowUpDown = true;
            this.dtpHoraInicio.Size = new System.Drawing.Size(227, 49);
            this.dtpHoraInicio.TabIndex = 11;
            this.dtpHoraInicio.Value = new System.DateTime(2025, 11, 16, 0, 0, 0, 0);
            // 
            // lblHoraFin
            // 
            this.lblHoraFin.AutoSize = true;
            this.lblHoraFin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHoraFin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHoraFin.Location = new System.Drawing.Point(960, 240);
            this.lblHoraFin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoraFin.Name = "lblHoraFin";
            this.lblHoraFin.Size = new System.Drawing.Size(78, 23);
            this.lblHoraFin.TabIndex = 10;
            this.lblHoraFin.Text = "Hora Fin";
            // 
            // lblHoraInicio
            // 
            this.lblHoraInicio.AutoSize = true;
            this.lblHoraInicio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHoraInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHoraInicio.Location = new System.Drawing.Point(347, 240);
            this.lblHoraInicio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoraInicio.Name = "lblHoraInicio";
            this.lblHoraInicio.Size = new System.Drawing.Size(97, 23);
            this.lblHoraInicio.TabIndex = 9;
            this.lblHoraInicio.Text = "Hora Inicio";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.BorderRadius = 8;
            this.dtpFechaInicio.Checked = true;
            this.dtpFechaInicio.FillColor = System.Drawing.Color.White;
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(40, 271);
            this.dtpFechaInicio.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicio.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFechaInicio.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(267, 49);
            this.dtpFechaInicio.TabIndex = 8;
            this.dtpFechaInicio.Value = new System.DateTime(2025, 11, 16, 0, 0, 0, 0);
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFechaInicio.Location = new System.Drawing.Point(40, 240);
            this.lblFechaInicio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(103, 23);
            this.lblFechaInicio.TabIndex = 7;
            this.lblFechaInicio.Text = "Fecha Inicio";
            // 
            // txtAsunto
            // 
            this.txtAsunto.BorderRadius = 8;
            this.txtAsunto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAsunto.DefaultText = "";
            this.txtAsunto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAsunto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAsunto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAsunto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAsunto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAsunto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAsunto.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAsunto.Location = new System.Drawing.Point(40, 178);
            this.txtAsunto.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtAsunto.Name = "txtAsunto";
            this.txtAsunto.PlaceholderText = "Ej: Consulta general";
            this.txtAsunto.SelectedText = "";
            this.txtAsunto.Size = new System.Drawing.Size(1147, 49);
            this.txtAsunto.TabIndex = 6;
            // 
            // lblAsunto
            // 
            this.lblAsunto.AutoSize = true;
            this.lblAsunto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAsunto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAsunto.Location = new System.Drawing.Point(40, 148);
            this.lblAsunto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAsunto.Name = "lblAsunto";
            this.lblAsunto.Size = new System.Drawing.Size(66, 23);
            this.lblAsunto.TabIndex = 5;
            this.lblAsunto.Text = "Asunto";
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.BackColor = System.Drawing.Color.Transparent;
            this.cmbDoctor.BorderRadius = 8;
            this.cmbDoctor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctor.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbDoctor.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbDoctor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbDoctor.ItemHeight = 34;
            this.cmbDoctor.Location = new System.Drawing.Point(653, 86);
            this.cmbDoctor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(532, 40);
            this.cmbDoctor.TabIndex = 4;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDoctor.Location = new System.Drawing.Point(653, 55);
            this.lblDoctor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(65, 23);
            this.lblDoctor.TabIndex = 3;
            this.lblDoctor.Text = "Doctor";
            // 
            // btnBuscarPaciente
            // 
            this.btnBuscarPaciente.BorderRadius = 8;
            this.btnBuscarPaciente.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBuscarPaciente.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBuscarPaciente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBuscarPaciente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBuscarPaciente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscarPaciente.ForeColor = System.Drawing.Color.White;
            this.btnBuscarPaciente.Image = global::CapaPresentacion.Properties.Resources.usuario;
            this.btnBuscarPaciente.ImageSize = new System.Drawing.Size(18, 18);
            this.btnBuscarPaciente.Location = new System.Drawing.Point(467, 86);
            this.btnBuscarPaciente.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscarPaciente.Name = "btnBuscarPaciente";
            this.btnBuscarPaciente.Size = new System.Drawing.Size(107, 49);
            this.btnBuscarPaciente.TabIndex = 2;
            this.btnBuscarPaciente.Text = "Buscar";
            // 
            // txtPaciente
            // 
            this.txtPaciente.BorderRadius = 8;
            this.txtPaciente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPaciente.DefaultText = "";
            this.txtPaciente.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPaciente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPaciente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPaciente.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPaciente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPaciente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPaciente.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPaciente.Location = new System.Drawing.Point(40, 86);
            this.txtPaciente.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.PlaceholderText = "Buscar paciente por nombre o cédula";
            this.txtPaciente.SelectedText = "";
            this.txtPaciente.Size = new System.Drawing.Size(413, 49);
            this.txtPaciente.TabIndex = 1;
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPaciente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPaciente.Location = new System.Drawing.Point(40, 55);
            this.lblPaciente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(77, 23);
            this.lblPaciente.TabIndex = 0;
            this.lblPaciente.Text = "Paciente";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BorderRadius = 10;
            this.btnGuardar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuardar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuardar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuardar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuardar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(213)))), ((int)(((byte)(115)))));
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(920, 689);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(173, 55);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BorderRadius = 10;
            this.btnCancelar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancelar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancelar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancelar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(71)))), ((int)(((byte)(87)))));
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(1107, 689);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(160, 55);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            // 
            // btnRegresar
            // 
            this.btnRegresar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.Image = global::CapaPresentacion.Properties.Resources.flecha_izquierda;
            this.btnRegresar.ImageRotate = 0F;
            this.btnRegresar.Location = new System.Drawing.Point(1213, 25);
            this.btnRegresar.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(53, 49);
            this.btnRegresar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRegresar.TabIndex = 4;
            this.btnRegresar.TabStop = false;
            // 
            // AgendarCita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1307, 763);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.panelFormulario);
            this.Controls.Add(this.lblTitulo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AgendarCita";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agendar Cita";
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegresar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2Panel panelFormulario;
        private System.Windows.Forms.Label lblPaciente;
        private Guna.UI2.WinForms.Guna2TextBox txtPaciente;
        private Guna.UI2.WinForms.Guna2Button btnBuscarPaciente;
        private Guna.UI2.WinForms.Guna2ComboBox cmbDoctor;
        private System.Windows.Forms.Label lblDoctor;
        private Guna.UI2.WinForms.Guna2TextBox txtAsunto;
        private System.Windows.Forms.Label lblAsunto;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaInicio;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHoraInicio;
        private System.Windows.Forms.Label lblHoraInicio;
        private System.Windows.Forms.Label lblHoraFin;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpHoraFin;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label lblFechaFin;
        private Guna.UI2.WinForms.Guna2ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstado;
        private Guna.UI2.WinForms.Guna2TextBox txtUbicacion;
        private System.Windows.Forms.Label lblUbicacion;
        private Guna.UI2.WinForms.Guna2CheckBox chkTodoElDia;
        private Guna.UI2.WinForms.Guna2Button btnGuardar;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
        private Guna.UI2.WinForms.Guna2PictureBox btnRegresar;
    }
}