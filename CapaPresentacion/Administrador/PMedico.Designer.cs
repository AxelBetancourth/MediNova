namespace CapaPresentacion.Administrador
{
    partial class PMedico
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Status = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelIzquierdo = new Guna.UI2.WinForms.Guna2Panel();
            this.Email = new Guna.UI2.WinForms.Guna2TextBox();
            this.TxtTelefono = new Guna.UI2.WinForms.Guna2TextBox();
            this.TxtNombre = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelCentro = new Guna.UI2.WinForms.Guna2Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lstHorariosAgregados = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CBEspecialidad = new Guna.UI2.WinForms.Guna2ComboBox();
            this.CbDisponible = new Guna.UI2.WinForms.Guna2CheckBox();
            this.panelDerecho = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.checkedListDiasSemana = new System.Windows.Forms.CheckedListBox();
            this.BtnQuitarHorario = new Guna.UI2.WinForms.Guna2Button();
            this.BtnAgregarHorario = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CBInicio = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CBFinal = new Guna.UI2.WinForms.Guna2ComboBox();
            this.BtnHora = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.BotonBuscar = new Guna.UI2.WinForms.Guna2TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BtnHorario = new Guna.UI2.WinForms.Guna2ImageButton();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnUsuario = new Guna.UI2.WinForms.Guna2ImageButton();
            this.BtnEditar = new Guna.UI2.WinForms.Guna2ImageButton();
            this.BtnEliminar = new Guna.UI2.WinForms.Guna2ImageButton();
            this.BtnRegistrar = new Guna.UI2.WinForms.Guna2Button();
            this.DtgMedicos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Status.SuspendLayout();
            this.panelIzquierdo.SuspendLayout();
            this.panelCentro.SuspendLayout();
            this.panelDerecho.SuspendLayout();
            this.BtnHora.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtgMedicos)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Status);
            this.splitContainer1.Panel1.Controls.Add(this.BtnHora);
            this.splitContainer1.Panel1MinSize = 500;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DtgMedicos);
            this.splitContainer1.Size = new System.Drawing.Size(1409, 824);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // Status
            // 
            this.Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Status.Controls.Add(this.label1);
            this.Status.Controls.Add(this.panelIzquierdo);
            this.Status.Controls.Add(this.panelCentro);
            this.Status.Controls.Add(this.panelDerecho);
            this.Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status.Location = new System.Drawing.Point(0, 0);
            this.Status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Status.Name = "Status";
            this.Status.Padding = new System.Windows.Forms.Padding(10);
            this.Status.Size = new System.Drawing.Size(1409, 420);
            this.Status.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Location = new System.Drawing.Point(310, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(749, 40);
            this.label1.TabIndex = 9;
            this.label1.Text = "Gestión de Médicos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.Controls.Add(this.Email);
            this.panelIzquierdo.Controls.Add(this.TxtTelefono);
            this.panelIzquierdo.Controls.Add(this.TxtNombre);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(10, 10);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.panelIzquierdo.Size = new System.Drawing.Size(300, 400);
            this.panelIzquierdo.TabIndex = 32;
            // 
            // Email
            // 
            this.Email.Animated = true;
            this.Email.BorderRadius = 6;
            this.Email.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Email.DefaultText = "";
            this.Email.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Email.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Email.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Email.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Email.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Email.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Email.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Email.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Email.Location = new System.Drawing.Point(7, 270);
            this.Email.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Email.Name = "Email";
            this.Email.PlaceholderText = "Email";
            this.Email.SelectedText = "";
            this.Email.Size = new System.Drawing.Size(280, 43);
            this.Email.TabIndex = 18;
            this.Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // TxtTelefono
            // 
            this.TxtTelefono.Animated = true;
            this.TxtTelefono.BorderRadius = 6;
            this.TxtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtTelefono.DefaultText = "";
            this.TxtTelefono.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtTelefono.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtTelefono.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtTelefono.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtTelefono.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtTelefono.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TxtTelefono.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtTelefono.Location = new System.Drawing.Point(7, 176);
            this.TxtTelefono.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtTelefono.Name = "TxtTelefono";
            this.TxtTelefono.PlaceholderText = "Telefono";
            this.TxtTelefono.SelectedText = "";
            this.TxtTelefono.Size = new System.Drawing.Size(280, 43);
            this.TxtTelefono.TabIndex = 15;
            this.TxtTelefono.TextChanged += new System.EventHandler(this.TxtTelefono_TextChanged);
            // 
            // TxtNombre
            // 
            this.TxtNombre.Animated = true;
            this.TxtNombre.BorderRadius = 6;
            this.TxtNombre.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtNombre.DefaultText = "";
            this.TxtNombre.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtNombre.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtNombre.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtNombre.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtNombre.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TxtNombre.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtNombre.Location = new System.Drawing.Point(7, 89);
            this.TxtNombre.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtNombre.Name = "TxtNombre";
            this.TxtNombre.PlaceholderText = "Nombre Completo";
            this.TxtNombre.SelectedText = "";
            this.TxtNombre.Size = new System.Drawing.Size(280, 43);
            this.TxtNombre.TabIndex = 14;
            this.TxtNombre.TextChanged += new System.EventHandler(this.TxtNombre_TextChanged);
            // 
            // panelCentro
            // 
            this.panelCentro.Controls.Add(this.label6);
            this.panelCentro.Controls.Add(this.lstHorariosAgregados);
            this.panelCentro.Controls.Add(this.label4);
            this.panelCentro.Controls.Add(this.CBEspecialidad);
            this.panelCentro.Controls.Add(this.CbDisponible);
            this.panelCentro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentro.Location = new System.Drawing.Point(10, 10);
            this.panelCentro.Name = "panelCentro";
            this.panelCentro.Padding = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.panelCentro.Size = new System.Drawing.Size(1049, 400);
            this.panelCentro.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(23, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 31;
            this.label6.Text = "Horarios:";
            // 
            // lstHorariosAgregados
            // 
            this.lstHorariosAgregados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHorariosAgregados.BackColor = System.Drawing.Color.White;
            this.lstHorariosAgregados.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstHorariosAgregados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstHorariosAgregados.FormattingEnabled = true;
            this.lstHorariosAgregados.Location = new System.Drawing.Point(328, 232);
            this.lstHorariosAgregados.Margin = new System.Windows.Forms.Padding(4);
            this.lstHorariosAgregados.Name = "lstHorariosAgregados";
            this.lstHorariosAgregados.Size = new System.Drawing.Size(697, 136);
            this.lstHorariosAgregados.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(23, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Especialidad:";
            // 
            // CBEspecialidad
            // 
            this.CBEspecialidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBEspecialidad.BackColor = System.Drawing.Color.Transparent;
            this.CBEspecialidad.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.CBEspecialidad.BorderRadius = 10;
            this.CBEspecialidad.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBEspecialidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBEspecialidad.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBEspecialidad.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBEspecialidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBEspecialidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CBEspecialidad.ItemHeight = 30;
            this.CBEspecialidad.Items.AddRange(new object[] {
            "Medicina General",
            "Pediatría",
            "Ginecología",
            "Cardiología",
            "Dermatología",
            "Oftalmología",
            "Ortopedia"});
            this.CBEspecialidad.Location = new System.Drawing.Point(328, 115);
            this.CBEspecialidad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CBEspecialidad.Name = "CBEspecialidad";
            this.CBEspecialidad.Size = new System.Drawing.Size(550, 36);
            this.CBEspecialidad.TabIndex = 16;
            this.CBEspecialidad.SelectedIndexChanged += new System.EventHandler(this.CBEspecialidad_SelectedIndexChanged);
            // 
            // CbDisponible
            // 
            this.CbDisponible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CbDisponible.AutoSize = true;
            this.CbDisponible.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CbDisponible.CheckedState.BorderRadius = 0;
            this.CbDisponible.CheckedState.BorderThickness = 0;
            this.CbDisponible.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CbDisponible.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CbDisponible.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CbDisponible.Location = new System.Drawing.Point(922, 127);
            this.CbDisponible.Margin = new System.Windows.Forms.Padding(4);
            this.CbDisponible.Name = "CbDisponible";
            this.CbDisponible.Size = new System.Drawing.Size(103, 24);
            this.CbDisponible.TabIndex = 19;
            this.CbDisponible.Text = "Disponible";
            this.CbDisponible.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.CbDisponible.UncheckedState.BorderRadius = 0;
            this.CbDisponible.UncheckedState.BorderThickness = 0;
            this.CbDisponible.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.CbDisponible.CheckedChanged += new System.EventHandler(this.CbDisponible_CheckedChanged);
            // 
            // panelDerecho
            // 
            this.panelDerecho.Controls.Add(this.label5);
            this.panelDerecho.Controls.Add(this.checkedListDiasSemana);
            this.panelDerecho.Controls.Add(this.BtnQuitarHorario);
            this.panelDerecho.Controls.Add(this.BtnAgregarHorario);
            this.panelDerecho.Controls.Add(this.label2);
            this.panelDerecho.Controls.Add(this.CBInicio);
            this.panelDerecho.Controls.Add(this.label3);
            this.panelDerecho.Controls.Add(this.CBFinal);
            this.panelDerecho.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDerecho.Location = new System.Drawing.Point(1059, 10);
            this.panelDerecho.Name = "panelDerecho";
            this.panelDerecho.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.panelDerecho.Size = new System.Drawing.Size(340, 400);
            this.panelDerecho.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(13, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Dias de atención:";
            // 
            // checkedListDiasSemana
            // 
            this.checkedListDiasSemana.BackColor = System.Drawing.Color.White;
            this.checkedListDiasSemana.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkedListDiasSemana.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkedListDiasSemana.FormattingEnabled = true;
            this.checkedListDiasSemana.Items.AddRange(new object[] {
            "Lunes",
            "Martes",
            "Miércoles",
            "Jueves",
            "Viernes",
            "Sabado",
            "Domingo"});
            this.checkedListDiasSemana.Location = new System.Drawing.Point(165, 209);
            this.checkedListDiasSemana.Margin = new System.Windows.Forms.Padding(4);
            this.checkedListDiasSemana.Name = "checkedListDiasSemana";
            this.checkedListDiasSemana.Size = new System.Drawing.Size(159, 136);
            this.checkedListDiasSemana.TabIndex = 23;
            this.checkedListDiasSemana.SelectedIndexChanged += new System.EventHandler(this.checkedListDiasSemana_SelectedIndexChanged);
            // 
            // BtnQuitarHorario
            // 
            this.BtnQuitarHorario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnQuitarHorario.BorderRadius = 6;
            this.BtnQuitarHorario.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnQuitarHorario.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnQuitarHorario.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnQuitarHorario.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnQuitarHorario.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(0)))), ((int)(((byte)(57)))));
            this.BtnQuitarHorario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnQuitarHorario.ForeColor = System.Drawing.Color.White;
            this.BtnQuitarHorario.Location = new System.Drawing.Point(13, 360);
            this.BtnQuitarHorario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnQuitarHorario.Name = "BtnQuitarHorario";
            this.BtnQuitarHorario.Size = new System.Drawing.Size(137, 36);
            this.BtnQuitarHorario.TabIndex = 30;
            this.BtnQuitarHorario.Text = "Quitar turno";
            this.BtnQuitarHorario.Click += new System.EventHandler(this.BtnQuitarHorario_Click);
            // 
            // BtnAgregarHorario
            // 
            this.BtnAgregarHorario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAgregarHorario.BorderRadius = 6;
            this.BtnAgregarHorario.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnAgregarHorario.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnAgregarHorario.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnAgregarHorario.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnAgregarHorario.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BtnAgregarHorario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregarHorario.ForeColor = System.Drawing.Color.White;
            this.BtnAgregarHorario.Location = new System.Drawing.Point(184, 360);
            this.BtnAgregarHorario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnAgregarHorario.Name = "BtnAgregarHorario";
            this.BtnAgregarHorario.Size = new System.Drawing.Size(140, 36);
            this.BtnAgregarHorario.TabIndex = 29;
            this.BtnAgregarHorario.Text = "Agregar turno";
            this.BtnAgregarHorario.Click += new System.EventHandler(this.BtnAgregarHorario_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Hora Inicio:";
            // 
            // CBInicio
            // 
            this.CBInicio.BackColor = System.Drawing.Color.Transparent;
            this.CBInicio.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.CBInicio.BorderRadius = 10;
            this.CBInicio.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBInicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBInicio.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBInicio.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBInicio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CBInicio.ItemHeight = 30;
            this.CBInicio.Items.AddRange(new object[] {
            "0:00 AM",
            "1:00 AM",
            "2:00 AM",
            "3:00 AM",
            "4:00 AM",
            "5:00 AM",
            "6:00 AM",
            "7:00 AM",
            "8:00 AM",
            "9:00 AM",
            "10:00 AM",
            "11:00 AM",
            "12:00 AM"});
            this.CBInicio.Location = new System.Drawing.Point(165, 89);
            this.CBInicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CBInicio.Name = "CBInicio";
            this.CBInicio.Size = new System.Drawing.Size(159, 36);
            this.CBInicio.TabIndex = 10;
            this.CBInicio.SelectedIndexChanged += new System.EventHandler(this.CBInicio_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(13, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Hora Final:";
            // 
            // CBFinal
            // 
            this.CBFinal.BackColor = System.Drawing.Color.Transparent;
            this.CBFinal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.CBFinal.BorderRadius = 10;
            this.CBFinal.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBFinal.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBFinal.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBFinal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CBFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CBFinal.ItemHeight = 30;
            this.CBFinal.Items.AddRange(new object[] {
            "0:00 PM",
            "1:00 PM",
            "2:00 PM",
            "3:00 PM",
            "4:00 PM",
            "5:00 PM",
            "6:00 PM",
            "7:00 PM",
            "8:00 PM",
            "9:00 PM",
            "10:00 PM",
            "11:00 PM",
            "12:00 PM"});
            this.CBFinal.Location = new System.Drawing.Point(165, 146);
            this.CBFinal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CBFinal.Name = "CBFinal";
            this.CBFinal.Size = new System.Drawing.Size(159, 36);
            this.CBFinal.TabIndex = 12;
            this.CBFinal.SelectedIndexChanged += new System.EventHandler(this.CBFinal_SelectedIndexChanged);
            // 
            // BtnHora
            // 
            this.BtnHora.BackColor = System.Drawing.SystemColors.Control;
            this.BtnHora.Controls.Add(this.BotonBuscar);
            this.BtnHora.Controls.Add(this.label8);
            this.BtnHora.Controls.Add(this.BtnHorario);
            this.BtnHora.Controls.Add(this.label7);
            this.BtnHora.Controls.Add(this.BtnUsuario);
            this.BtnHora.Controls.Add(this.BtnEditar);
            this.BtnHora.Controls.Add(this.BtnEliminar);
            this.BtnHora.Controls.Add(this.BtnRegistrar);
            this.BtnHora.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnHora.Location = new System.Drawing.Point(0, 420);
            this.BtnHora.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnHora.Name = "BtnHora";
            this.BtnHora.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.BtnHora.Size = new System.Drawing.Size(1409, 80);
            this.BtnHora.TabIndex = 12;
            // 
            // BotonBuscar
            // 
            this.BotonBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BotonBuscar.BorderRadius = 6;
            this.BotonBuscar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BotonBuscar.DefaultText = "";
            this.BotonBuscar.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.BotonBuscar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BotonBuscar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BotonBuscar.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BotonBuscar.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BotonBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BotonBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BotonBuscar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BotonBuscar.Location = new System.Drawing.Point(13, 18);
            this.BotonBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BotonBuscar.Name = "BotonBuscar";
            this.BotonBuscar.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.BotonBuscar.PlaceholderText = "Buscar médico...";
            this.BotonBuscar.SelectedText = "";
            this.BotonBuscar.Size = new System.Drawing.Size(455, 44);
            this.BotonBuscar.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(745, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "Ver horario:";
            // 
            // BtnHorario
            // 
            this.BtnHorario.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnHorario.BackColor = System.Drawing.Color.Transparent;
            this.BtnHorario.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnHorario.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnHorario.Image = global::CapaPresentacion.Properties.Resources.hora;
            this.BtnHorario.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnHorario.ImageRotate = 0F;
            this.BtnHorario.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnHorario.IndicateFocus = true;
            this.BtnHorario.Location = new System.Drawing.Point(836, 18);
            this.BtnHorario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnHorario.Name = "BtnHorario";
            this.BtnHorario.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnHorario.Size = new System.Drawing.Size(45, 44);
            this.BtnHorario.TabIndex = 26;
            this.BtnHorario.UseTransparentBackground = true;
            this.BtnHorario.Click += new System.EventHandler(this.BtnHorario_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(896, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 20);
            this.label7.TabIndex = 29;
            this.label7.Text = "Asociar usuario:";
            // 
            // BtnUsuario
            // 
            this.BtnUsuario.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnUsuario.BackColor = System.Drawing.Color.Transparent;
            this.BtnUsuario.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnUsuario.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnUsuario.Image = global::CapaPresentacion.Properties.Resources._usuario;
            this.BtnUsuario.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnUsuario.ImageRotate = 0F;
            this.BtnUsuario.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnUsuario.IndicateFocus = true;
            this.BtnUsuario.Location = new System.Drawing.Point(1015, 18);
            this.BtnUsuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnUsuario.Name = "BtnUsuario";
            this.BtnUsuario.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnUsuario.Size = new System.Drawing.Size(36, 44);
            this.BtnUsuario.TabIndex = 25;
            this.BtnUsuario.UseTransparentBackground = true;
            this.BtnUsuario.Click += new System.EventHandler(this.BtnUsuario_Click);
            // 
            // BtnEditar
            // 
            this.BtnEditar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnEditar.BackColor = System.Drawing.Color.Transparent;
            this.BtnEditar.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnEditar.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnEditar.Image = global::CapaPresentacion.Properties.Resources.editar;
            this.BtnEditar.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnEditar.ImageRotate = 0F;
            this.BtnEditar.ImageSize = new System.Drawing.Size(32, 32);
            this.BtnEditar.IndicateFocus = true;
            this.BtnEditar.Location = new System.Drawing.Point(1059, 18);
            this.BtnEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnEditar.Name = "BtnEditar";
            this.BtnEditar.PressedState.ImageSize = new System.Drawing.Size(28, 28);
            this.BtnEditar.Size = new System.Drawing.Size(43, 44);
            this.BtnEditar.TabIndex = 27;
            this.BtnEditar.UseTransparentBackground = true;
            this.BtnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnEliminar.BackColor = System.Drawing.Color.Transparent;
            this.BtnEliminar.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnEliminar.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnEliminar.Image = global::CapaPresentacion.Properties.Resources.borrar;
            this.BtnEliminar.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnEliminar.ImageRotate = 0F;
            this.BtnEliminar.ImageSize = new System.Drawing.Size(32, 32);
            this.BtnEliminar.Location = new System.Drawing.Point(1107, 16);
            this.BtnEliminar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.PressedState.ImageSize = new System.Drawing.Size(28, 28);
            this.BtnEliminar.Size = new System.Drawing.Size(47, 48);
            this.BtnEliminar.TabIndex = 28;
            this.BtnEliminar.UseTransparentBackground = true;
            this.BtnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // BtnRegistrar
            // 
            this.BtnRegistrar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnRegistrar.BorderRadius = 6;
            this.BtnRegistrar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnRegistrar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnRegistrar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnRegistrar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnRegistrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.BtnRegistrar.ForeColor = System.Drawing.Color.White;
            this.BtnRegistrar.Location = new System.Drawing.Point(1186, 18);
            this.BtnRegistrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnRegistrar.Name = "BtnRegistrar";
            this.BtnRegistrar.Size = new System.Drawing.Size(210, 48);
            this.BtnRegistrar.TabIndex = 24;
            this.BtnRegistrar.Text = "Registrar";
            this.BtnRegistrar.Click += new System.EventHandler(this.BtnRegistrar_Click);
            // 
            // DtgMedicos
            // 
            this.DtgMedicos.AllowUserToAddRows = false;
            this.DtgMedicos.AllowUserToDeleteRows = false;
            this.DtgMedicos.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DtgMedicos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.DtgMedicos.BackgroundColor = System.Drawing.Color.White;
            this.DtgMedicos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DtgMedicos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DtgMedicos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgMedicos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DtgMedicos.ColumnHeadersHeight = 40;
            this.DtgMedicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgMedicos.DefaultCellStyle = dataGridViewCellStyle9;
            this.DtgMedicos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DtgMedicos.EnableHeadersVisualStyles = false;
            this.DtgMedicos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DtgMedicos.Location = new System.Drawing.Point(0, 0);
            this.DtgMedicos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DtgMedicos.Name = "DtgMedicos";
            this.DtgMedicos.ReadOnly = true;
            this.DtgMedicos.RowHeadersVisible = false;
            this.DtgMedicos.RowHeadersWidth = 51;
            this.DtgMedicos.RowTemplate.Height = 40;
            this.DtgMedicos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgMedicos.Size = new System.Drawing.Size(1409, 319);
            this.DtgMedicos.TabIndex = 11;
            this.DtgMedicos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgMedicos_CellClick);
            this.DtgMedicos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgMedicos_CellContentClick);
            // 
            // PMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1409, 824);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "PMedico";
            this.Text = "PMedico";
            this.Load += new System.EventHandler(this.PMedico_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.Status.ResumeLayout(false);
            this.panelIzquierdo.ResumeLayout(false);
            this.panelCentro.ResumeLayout(false);
            this.panelCentro.PerformLayout();
            this.panelDerecho.ResumeLayout(false);
            this.panelDerecho.PerformLayout();
            this.BtnHora.ResumeLayout(false);
            this.BtnHora.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtgMedicos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView DtgMedicos;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel BtnHora;
        private Guna.UI2.WinForms.Guna2TextBox BotonBuscar;
        private Guna.UI2.WinForms.Guna2Panel Status;
        private Guna.UI2.WinForms.Guna2Button BtnRegistrar;
        private System.Windows.Forms.CheckedListBox checkedListDiasSemana;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2CheckBox CbDisponible;
        private Guna.UI2.WinForms.Guna2TextBox Email;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2ComboBox CBEspecialidad;
        private Guna.UI2.WinForms.Guna2TextBox TxtTelefono;
        private Guna.UI2.WinForms.Guna2TextBox TxtNombre;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox CBFinal;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ComboBox CBInicio;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ImageButton BtnUsuario;
        private Guna.UI2.WinForms.Guna2ImageButton BtnHorario;
        private Guna.UI2.WinForms.Guna2ImageButton BtnEditar;
        private Guna.UI2.WinForms.Guna2ImageButton BtnEliminar;
        private System.Windows.Forms.CheckedListBox lstHorariosAgregados;
        private Guna.UI2.WinForms.Guna2Button BtnQuitarHorario;
        private Guna.UI2.WinForms.Guna2Button BtnAgregarHorario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2Panel panelIzquierdo;
        private Guna.UI2.WinForms.Guna2Panel panelCentro;
        private Guna.UI2.WinForms.Guna2Panel panelDerecho;
    }
}
