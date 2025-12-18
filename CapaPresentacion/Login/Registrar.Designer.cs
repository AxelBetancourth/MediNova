namespace CapaPresentacion.ModuloLogin
{
    partial class Registrar
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Status = new Guna.UI2.WinForms.Guna2Panel();
            this.Estado = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.CBRol = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAgregar = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.TxtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.TxtUsuario = new Guna.UI2.WinForms.Guna2TextBox();
            this.BtnLogin = new Guna.UI2.WinForms.Guna2Button();
            this.DtgUsuarios = new System.Windows.Forms.DataGridView();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.BtnRecuperar = new Guna.UI2.WinForms.Guna2ImageButton();
            this.label3 = new System.Windows.Forms.Label();
            this.CheckEliminados = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.BotonBuscar = new Guna.UI2.WinForms.Guna2TextBox();
            this.BtnEditar = new Guna.UI2.WinForms.Guna2ImageButton();
            this.BtnEliminar = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtgUsuarios)).BeginInit();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.Status.Controls.Add(this.Estado);
            this.Status.Controls.Add(this.label2);
            this.Status.Controls.Add(this.CBRol);
            this.Status.Controls.Add(this.label1);
            this.Status.Controls.Add(this.BtnAgregar);
            this.Status.Controls.Add(this.guna2PictureBox2);
            this.Status.Controls.Add(this.TxtPassword);
            this.Status.Controls.Add(this.TxtUsuario);
            this.Status.Location = new System.Drawing.Point(0, 0);
            this.Status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(1100, 440);
            this.Status.TabIndex = 6;
            this.Status.Paint += new System.Windows.Forms.PaintEventHandler(this.Status_Paint);
            // 
            // Estado
            // 
            this.Estado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Estado.BackColor = System.Drawing.Color.Transparent;
            this.Estado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Estado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Estado.Location = new System.Drawing.Point(384, 399);
            this.Estado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Estado.Name = "Estado";
            this.Estado.Size = new System.Drawing.Size(126, 22);
            this.Estado.TabIndex = 12;
            this.Estado.Text = "Status: Sin agregar";
            this.Estado.Click += new System.EventHandler(this.Estado_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(370, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "Rol";
            // 
            // CBRol
            // 
            this.CBRol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CBRol.BackColor = System.Drawing.Color.Transparent;
            this.CBRol.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.CBRol.BorderRadius = 8;
            this.CBRol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBRol.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBRol.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CBRol.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CBRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CBRol.ItemHeight = 34;
            this.CBRol.Location = new System.Drawing.Point(370, 255);
            this.CBRol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CBRol.Name = "CBRol";
            this.CBRol.Size = new System.Drawing.Size(340, 40);
            this.CBRol.TabIndex = 10;
            this.CBRol.SelectedIndexChanged += new System.EventHandler(this.CBRol_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label1.Location = new System.Drawing.Point(395, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 41);
            this.label1.TabIndex = 9;
            this.label1.Text = "Gestión de Usuarios";
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnAgregar.Animated = true;
            this.BtnAgregar.BackColor = System.Drawing.Color.Transparent;
            this.BtnAgregar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BtnAgregar.BorderRadius = 8;
            this.BtnAgregar.BorderThickness = 1;
            this.BtnAgregar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnAgregar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnAgregar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnAgregar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnAgregar.FillColor = System.Drawing.Color.White;
            this.BtnAgregar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.BtnAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BtnAgregar.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BtnAgregar.HoverState.ForeColor = System.Drawing.Color.White;
            this.BtnAgregar.Location = new System.Drawing.Point(530, 334);
            this.BtnAgregar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(180, 40);
            this.BtnAgregar.TabIndex = 7;
            this.BtnAgregar.Text = "Capturar Huella";
            this.BtnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // guna2PictureBox2
            // 
            this.guna2PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2PictureBox2.Image = global::CapaPresentacion.Properties.Resources.huella_dactilar_N;
            this.guna2PictureBox2.ImageRotate = 0F;
            this.guna2PictureBox2.Location = new System.Drawing.Point(370, 313);
            this.guna2PictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2PictureBox2.Name = "guna2PictureBox2";
            this.guna2PictureBox2.Size = new System.Drawing.Size(140, 70);
            this.guna2PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox2.TabIndex = 6;
            this.guna2PictureBox2.TabStop = false;
            // 
            // TxtPassword
            // 
            this.TxtPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtPassword.Animated = true;
            this.TxtPassword.BorderRadius = 8;
            this.TxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtPassword.DefaultText = "";
            this.TxtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TxtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtPassword.Location = new System.Drawing.Point(370, 160);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.TxtPassword.PlaceholderText = "Contraseña";
            this.TxtPassword.SelectedText = "";
            this.TxtPassword.Size = new System.Drawing.Size(340, 45);
            this.TxtPassword.TabIndex = 3;
            this.TxtPassword.UseSystemPasswordChar = true;
            this.TxtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtUsuario.Animated = true;
            this.TxtUsuario.BorderRadius = 8;
            this.TxtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtUsuario.DefaultText = "";
            this.TxtUsuario.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtUsuario.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtUsuario.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtUsuario.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtUsuario.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TxtUsuario.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtUsuario.IconLeft = global::CapaPresentacion.Properties.Resources._usuario;
            this.TxtUsuario.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.TxtUsuario.IconLeftSize = new System.Drawing.Size(18, 18);
            this.TxtUsuario.Location = new System.Drawing.Point(370, 90);
            this.TxtUsuario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.TxtUsuario.PlaceholderText = "Nombre de Usuario";
            this.TxtUsuario.SelectedText = "";
            this.TxtUsuario.Size = new System.Drawing.Size(340, 45);
            this.TxtUsuario.TabIndex = 2;
            this.TxtUsuario.TextChanged += new System.EventHandler(this.TxtUsuario_TextChanged);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnLogin.Animated = true;
            this.BtnLogin.BorderRadius = 8;
            this.BtnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.BtnLogin.ForeColor = System.Drawing.Color.White;
            this.BtnLogin.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(118)))), ((int)(((byte)(225)))));
            this.BtnLogin.Location = new System.Drawing.Point(543, 26);
            this.BtnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(208, 38);
            this.BtnLogin.TabIndex = 4;
            this.BtnLogin.Text = "Registrar";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // DtgUsuarios
            // 
            this.DtgUsuarios.AllowUserToAddRows = false;
            this.DtgUsuarios.AllowUserToDeleteRows = false;
            this.DtgUsuarios.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.DtgUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.DtgUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DtgUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DtgUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.DtgUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DtgUsuarios.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DtgUsuarios.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DtgUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.DtgUsuarios.ColumnHeadersHeight = 40;
            this.DtgUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DtgUsuarios.DefaultCellStyle = dataGridViewCellStyle12;
            this.DtgUsuarios.EnableHeadersVisualStyles = false;
            this.DtgUsuarios.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DtgUsuarios.Location = new System.Drawing.Point(0, 528);
            this.DtgUsuarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DtgUsuarios.MultiSelect = false;
            this.DtgUsuarios.Name = "DtgUsuarios";
            this.DtgUsuarios.ReadOnly = true;
            this.DtgUsuarios.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DtgUsuarios.RowHeadersVisible = false;
            this.DtgUsuarios.RowHeadersWidth = 51;
            this.DtgUsuarios.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DtgUsuarios.RowTemplate.Height = 35;
            this.DtgUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DtgUsuarios.Size = new System.Drawing.Size(1100, 448);
            this.DtgUsuarios.TabIndex = 7;
            this.DtgUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgUsuarios_CellClick);
            this.DtgUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgUsuarios_CellContentClick);
            this.DtgUsuarios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtgUsuarios_CellDoubleClick);
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2CustomGradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CustomGradientPanel1.BorderRadius = 1;
            this.guna2CustomGradientPanel1.Controls.Add(this.BtnRecuperar);
            this.guna2CustomGradientPanel1.Controls.Add(this.label3);
            this.guna2CustomGradientPanel1.Controls.Add(this.CheckEliminados);
            this.guna2CustomGradientPanel1.Controls.Add(this.BotonBuscar);
            this.guna2CustomGradientPanel1.Controls.Add(this.BtnEditar);
            this.guna2CustomGradientPanel1.Controls.Add(this.BtnEliminar);
            this.guna2CustomGradientPanel1.Controls.Add(this.BtnLogin);
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 440);
            this.guna2CustomGradientPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.ShadowDecoration.BorderRadius = 0;
            this.guna2CustomGradientPanel1.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.guna2CustomGradientPanel1.ShadowDecoration.Depth = 5;
            this.guna2CustomGradientPanel1.ShadowDecoration.Enabled = true;
            this.guna2CustomGradientPanel1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, -2, 0, 0);
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(1100, 88);
            this.guna2CustomGradientPanel1.TabIndex = 8;
            this.guna2CustomGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2CustomGradientPanel1_Paint);
            // 
            // BtnRecuperar
            // 
            this.BtnRecuperar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnRecuperar.BackColor = System.Drawing.Color.Transparent;
            this.BtnRecuperar.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnRecuperar.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnRecuperar.Image = global::CapaPresentacion.Properties.Resources.recuperar;
            this.BtnRecuperar.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnRecuperar.ImageRotate = 0F;
            this.BtnRecuperar.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnRecuperar.IndicateFocus = true;
            this.BtnRecuperar.Location = new System.Drawing.Point(1040, 18);
            this.BtnRecuperar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnRecuperar.Name = "BtnRecuperar";
            this.BtnRecuperar.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.BtnRecuperar.Size = new System.Drawing.Size(48, 46);
            this.BtnRecuperar.TabIndex = 20;
            this.BtnRecuperar.UseTransparentBackground = true;
            this.BtnRecuperar.Click += new System.EventHandler(this.BtnRecuperar_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(823, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Eliminados:";
            // 
            // CheckEliminados
            // 
            this.CheckEliminados.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CheckEliminados.BackColor = System.Drawing.Color.Transparent;
            this.CheckEliminados.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CheckEliminados.CheckedState.BorderRadius = 4;
            this.CheckEliminados.CheckedState.BorderThickness = 0;
            this.CheckEliminados.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CheckEliminados.Location = new System.Drawing.Point(914, 34);
            this.CheckEliminados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CheckEliminados.Name = "CheckEliminados";
            this.CheckEliminados.Size = new System.Drawing.Size(24, 24);
            this.CheckEliminados.TabIndex = 18;
            this.CheckEliminados.Text = "guna2CustomCheckBox1";
            this.CheckEliminados.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.CheckEliminados.UncheckedState.BorderRadius = 4;
            this.CheckEliminados.UncheckedState.BorderThickness = 2;
            this.CheckEliminados.UncheckedState.FillColor = System.Drawing.Color.White;
            this.CheckEliminados.Click += new System.EventHandler(this.CheckEliminados_Click);
            // 
            // BotonBuscar
            // 
            this.BotonBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BotonBuscar.Animated = true;
            this.BotonBuscar.BorderRadius = 8;
            this.BotonBuscar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BotonBuscar.DefaultText = "";
            this.BotonBuscar.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.BotonBuscar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.BotonBuscar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BotonBuscar.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.BotonBuscar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.BotonBuscar.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BotonBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BotonBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BotonBuscar.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BotonBuscar.Location = new System.Drawing.Point(20, 26);
            this.BotonBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BotonBuscar.Name = "BotonBuscar";
            this.BotonBuscar.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.BotonBuscar.PlaceholderText = "Buscar usuario...";
            this.BotonBuscar.SelectedText = "";
            this.BotonBuscar.Size = new System.Drawing.Size(450, 38);
            this.BotonBuscar.TabIndex = 17;
            this.BotonBuscar.TextChanged += new System.EventHandler(this.BotonBuscar_TextChanged);
            // 
            // BtnEditar
            // 
            this.BtnEditar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnEditar.BackColor = System.Drawing.Color.Transparent;
            this.BtnEditar.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnEditar.HoverState.ImageSize = new System.Drawing.Size(28, 28);
            this.BtnEditar.Image = global::CapaPresentacion.Properties.Resources.editar;
            this.BtnEditar.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnEditar.ImageRotate = 0F;
            this.BtnEditar.ImageSize = new System.Drawing.Size(26, 26);
            this.BtnEditar.IndicateFocus = true;
            this.BtnEditar.Location = new System.Drawing.Point(970, 27);
            this.BtnEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnEditar.Name = "BtnEditar";
            this.BtnEditar.PressedState.ImageSize = new System.Drawing.Size(26, 26);
            this.BtnEditar.Size = new System.Drawing.Size(36, 36);
            this.BtnEditar.TabIndex = 15;
            this.BtnEditar.UseTransparentBackground = true;
            this.BtnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnEliminar.BackColor = System.Drawing.Color.Transparent;
            this.BtnEliminar.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.BtnEliminar.HoverState.ImageSize = new System.Drawing.Size(28, 28);
            this.BtnEliminar.Image = global::CapaPresentacion.Properties.Resources.borrar;
            this.BtnEliminar.ImageOffset = new System.Drawing.Point(0, 0);
            this.BtnEliminar.ImageRotate = 0F;
            this.BtnEliminar.ImageSize = new System.Drawing.Size(26, 26);
            this.BtnEliminar.Location = new System.Drawing.Point(1012, 27);
            this.BtnEliminar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.PressedState.ImageSize = new System.Drawing.Size(26, 26);
            this.BtnEliminar.Size = new System.Drawing.Size(36, 36);
            this.BtnEliminar.TabIndex = 16;
            this.BtnEliminar.UseTransparentBackground = true;
            this.BtnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 15;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.Status;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // Registrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 975);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Controls.Add(this.DtgUsuarios);
            this.Controls.Add(this.Status);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1099, 798);
            this.Name = "Registrar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuarios";
            this.Load += new System.EventHandler(this.Registro_Load);
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtgUsuarios)).EndInit();
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel Status;
        private Guna.UI2.WinForms.Guna2Button BtnAgregar;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private Guna.UI2.WinForms.Guna2Button BtnLogin;
        private Guna.UI2.WinForms.Guna2TextBox TxtPassword;
        private Guna.UI2.WinForms.Guna2TextBox TxtUsuario;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox CBRol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DtgUsuarios;
        private Guna.UI2.WinForms.Guna2HtmlLabel Estado;
        private Guna.UI2.WinForms.Guna2ImageButton BtnEliminar;
        private Guna.UI2.WinForms.Guna2ImageButton BtnEditar;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2TextBox BotonBuscar;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2CustomCheckBox CheckEliminados;
        private Guna.UI2.WinForms.Guna2ImageButton BtnRecuperar;
    }
}