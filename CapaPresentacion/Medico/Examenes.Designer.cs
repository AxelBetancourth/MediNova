namespace CapaPresentacion.Medico
{
    partial class Examenes
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelPrincipal = new Guna.UI2.WinForms.Guna2Panel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.BtnVolver = new Guna.UI2.WinForms.Guna2Button();
            this.panelExamenes = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvExamenes = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panelBotonesExamenes = new Guna.UI2.WinForms.Guna2Panel();
            this.BtnEliminarExamen = new Guna.UI2.WinForms.Guna2Button();
            this.BtnEditarExamen = new Guna.UI2.WinForms.Guna2Button();
            this.BtnNuevoExamen = new Guna.UI2.WinForms.Guna2Button();
            this.panelExpediente = new Guna.UI2.WinForms.Guna2Panel();
            this.lblNumeroExpediente = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.lblNombrePaciente = new System.Windows.Forms.Label();
            this.panelBusqueda = new Guna.UI2.WinForms.Guna2Panel();
            this.BtnLimpiar = new Guna.UI2.WinForms.Guna2Button();
            this.BtnBuscarPaciente = new Guna.UI2.WinForms.Guna2Button();
            this.txtBuscarPaciente = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelExamenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamenes)).BeginInit();
            this.panelBotonesExamenes.SuspendLayout();
            this.panelExpediente.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.SuspendLayout();
            //
            // panelPrincipal
            //
            this.panelPrincipal.Controls.Add(this.panelExamenes);
            this.panelPrincipal.Controls.Add(this.panelExpediente);
            this.panelPrincipal.Controls.Add(this.panelBusqueda);
            this.panelPrincipal.Controls.Add(this.panelHeader);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.panelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Padding = new System.Windows.Forms.Padding(20);
            this.panelPrincipal.Size = new System.Drawing.Size(1200, 700);
            this.panelPrincipal.TabIndex = 0;
            //
            // panelHeader
            //
            this.panelHeader.Controls.Add(this.BtnVolver);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.FillColor = System.Drawing.Color.Transparent;
            this.panelHeader.Location = new System.Drawing.Point(20, 20);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1160, 50);
            this.panelHeader.TabIndex = 3;
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
            this.BtnVolver.Location = new System.Drawing.Point(1027, 7);
            this.BtnVolver.Name = "BtnVolver";
            this.BtnVolver.Size = new System.Drawing.Size(110, 36);
            this.BtnVolver.TabIndex = 0;
            this.BtnVolver.Text = "← Volver";
            this.BtnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
            //
            // panelExamenes
            //
            this.panelExamenes.BorderRadius = 10;
            this.panelExamenes.Controls.Add(this.dgvExamenes);
            this.panelExamenes.Controls.Add(this.panelBotonesExamenes);
            this.panelExamenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExamenes.FillColor = System.Drawing.Color.White;
            this.panelExamenes.Location = new System.Drawing.Point(20, 220);
            this.panelExamenes.Name = "panelExamenes";
            this.panelExamenes.Padding = new System.Windows.Forms.Padding(15);
            this.panelExamenes.Size = new System.Drawing.Size(1160, 460);
            this.panelExamenes.TabIndex = 2;
            //
            // dgvExamenes
            //
            this.dgvExamenes.AllowUserToAddRows = false;
            this.dgvExamenes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvExamenes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExamenes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExamenes.BackgroundColor = System.Drawing.Color.White;
            this.dgvExamenes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExamenes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvExamenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExamenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExamenes.Location = new System.Drawing.Point(15, 65);
            this.dgvExamenes.Name = "dgvExamenes";
            this.dgvExamenes.ReadOnly = true;
            this.dgvExamenes.RowHeadersVisible = false;
            this.dgvExamenes.Size = new System.Drawing.Size(1130, 380);
            this.dgvExamenes.TabIndex = 1;
            //
            // panelBotonesExamenes
            //
            this.panelBotonesExamenes.Controls.Add(this.BtnEliminarExamen);
            this.panelBotonesExamenes.Controls.Add(this.BtnEditarExamen);
            this.panelBotonesExamenes.Controls.Add(this.BtnNuevoExamen);
            this.panelBotonesExamenes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBotonesExamenes.FillColor = System.Drawing.Color.White;
            this.panelBotonesExamenes.Location = new System.Drawing.Point(15, 15);
            this.panelBotonesExamenes.Name = "panelBotonesExamenes";
            this.panelBotonesExamenes.Size = new System.Drawing.Size(1130, 50);
            this.panelBotonesExamenes.TabIndex = 0;
            //
            // BtnEliminarExamen
            //
            this.BtnEliminarExamen.BorderRadius = 5;
            this.BtnEliminarExamen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnEliminarExamen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnEliminarExamen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnEliminarExamen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnEliminarExamen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.BtnEliminarExamen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnEliminarExamen.ForeColor = System.Drawing.Color.White;
            this.BtnEliminarExamen.Location = new System.Drawing.Point(290, 10);
            this.BtnEliminarExamen.Name = "BtnEliminarExamen";
            this.BtnEliminarExamen.Size = new System.Drawing.Size(130, 35);
            this.BtnEliminarExamen.TabIndex = 2;
            this.BtnEliminarExamen.Text = "Eliminar";
            this.BtnEliminarExamen.Click += new System.EventHandler(this.BtnEliminarExamen_Click);
            //
            // BtnEditarExamen
            //
            this.BtnEditarExamen.BorderRadius = 5;
            this.BtnEditarExamen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnEditarExamen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnEditarExamen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnEditarExamen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnEditarExamen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.BtnEditarExamen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnEditarExamen.ForeColor = System.Drawing.Color.White;
            this.BtnEditarExamen.Location = new System.Drawing.Point(150, 10);
            this.BtnEditarExamen.Name = "BtnEditarExamen";
            this.BtnEditarExamen.Size = new System.Drawing.Size(130, 35);
            this.BtnEditarExamen.TabIndex = 1;
            this.BtnEditarExamen.Text = "Editar";
            this.BtnEditarExamen.Click += new System.EventHandler(this.BtnEditarExamen_Click);
            //
            // BtnNuevoExamen
            //
            this.BtnNuevoExamen.BorderRadius = 5;
            this.BtnNuevoExamen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnNuevoExamen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnNuevoExamen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnNuevoExamen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnNuevoExamen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.BtnNuevoExamen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnNuevoExamen.ForeColor = System.Drawing.Color.White;
            this.BtnNuevoExamen.Location = new System.Drawing.Point(10, 10);
            this.BtnNuevoExamen.Name = "BtnNuevoExamen";
            this.BtnNuevoExamen.Size = new System.Drawing.Size(130, 35);
            this.BtnNuevoExamen.TabIndex = 0;
            this.BtnNuevoExamen.Text = "Nuevo Examen";
            this.BtnNuevoExamen.Click += new System.EventHandler(this.BtnNuevoExamen_Click);
            //
            // panelExpediente
            //
            this.panelExpediente.BorderRadius = 10;
            this.panelExpediente.Controls.Add(this.lblNumeroExpediente);
            this.panelExpediente.Controls.Add(this.lblDNI);
            this.panelExpediente.Controls.Add(this.lblNombrePaciente);
            this.panelExpediente.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExpediente.FillColor = System.Drawing.Color.White;
            this.panelExpediente.Location = new System.Drawing.Point(20, 100);
            this.panelExpediente.Name = "panelExpediente";
            this.panelExpediente.Padding = new System.Windows.Forms.Padding(15);
            this.panelExpediente.Size = new System.Drawing.Size(1160, 120);
            this.panelExpediente.TabIndex = 1;
            //
            // lblNumeroExpediente
            //
            this.lblNumeroExpediente.AutoSize = true;
            this.lblNumeroExpediente.BackColor = System.Drawing.Color.Transparent;
            this.lblNumeroExpediente.Font = new System.Drawing.Font("Arial", 10F);
            this.lblNumeroExpediente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblNumeroExpediente.Location = new System.Drawing.Point(18, 80);
            this.lblNumeroExpediente.Name = "lblNumeroExpediente";
            this.lblNumeroExpediente.Size = new System.Drawing.Size(141, 16);
            this.lblNumeroExpediente.TabIndex = 2;
            this.lblNumeroExpediente.Text = "Número Expediente";
            //
            // lblDNI
            //
            this.lblDNI.AutoSize = true;
            this.lblDNI.BackColor = System.Drawing.Color.Transparent;
            this.lblDNI.Font = new System.Drawing.Font("Arial", 10F);
            this.lblDNI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDNI.Location = new System.Drawing.Point(18, 50);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(37, 16);
            this.lblDNI.TabIndex = 1;
            this.lblDNI.Text = "DNI:";
            //
            // lblNombrePaciente
            //
            this.lblNombrePaciente.AutoSize = true;
            this.lblNombrePaciente.BackColor = System.Drawing.Color.Transparent;
            this.lblNombrePaciente.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblNombrePaciente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblNombrePaciente.Location = new System.Drawing.Point(18, 18);
            this.lblNombrePaciente.Name = "lblNombrePaciente";
            this.lblNombrePaciente.Size = new System.Drawing.Size(221, 22);
            this.lblNombrePaciente.TabIndex = 0;
            this.lblNombrePaciente.Text = "Nombre del Paciente";
            //
            // panelBusqueda
            //
            this.panelBusqueda.BorderRadius = 10;
            this.panelBusqueda.Controls.Add(this.BtnLimpiar);
            this.panelBusqueda.Controls.Add(this.BtnBuscarPaciente);
            this.panelBusqueda.Controls.Add(this.txtBuscarPaciente);
            this.panelBusqueda.Controls.Add(this.label1);
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.FillColor = System.Drawing.Color.White;
            this.panelBusqueda.Location = new System.Drawing.Point(20, 70);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(15);
            this.panelBusqueda.Size = new System.Drawing.Size(1160, 80);
            this.panelBusqueda.TabIndex = 0;
            //
            // BtnLimpiar
            //
            this.BtnLimpiar.BorderRadius = 5;
            this.BtnLimpiar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnLimpiar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnLimpiar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnLimpiar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnLimpiar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.BtnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnLimpiar.ForeColor = System.Drawing.Color.White;
            this.BtnLimpiar.Location = new System.Drawing.Point(770, 28);
            this.BtnLimpiar.Name = "BtnLimpiar";
            this.BtnLimpiar.Size = new System.Drawing.Size(100, 36);
            this.BtnLimpiar.TabIndex = 3;
            this.BtnLimpiar.Text = "Limpiar";
            this.BtnLimpiar.Click += new System.EventHandler(this.BtnLimpiar_Click);
            //
            // BtnBuscarPaciente
            //
            this.BtnBuscarPaciente.BorderRadius = 5;
            this.BtnBuscarPaciente.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnBuscarPaciente.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnBuscarPaciente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnBuscarPaciente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnBuscarPaciente.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BtnBuscarPaciente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnBuscarPaciente.ForeColor = System.Drawing.Color.White;
            this.BtnBuscarPaciente.Location = new System.Drawing.Point(650, 28);
            this.BtnBuscarPaciente.Name = "BtnBuscarPaciente";
            this.BtnBuscarPaciente.Size = new System.Drawing.Size(110, 36);
            this.BtnBuscarPaciente.TabIndex = 2;
            this.BtnBuscarPaciente.Text = "Buscar";
            this.BtnBuscarPaciente.Click += new System.EventHandler(this.BtnBuscarPaciente_Click);
            //
            // txtBuscarPaciente
            //
            this.txtBuscarPaciente.BorderRadius = 5;
            this.txtBuscarPaciente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBuscarPaciente.DefaultText = "";
            this.txtBuscarPaciente.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBuscarPaciente.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBuscarPaciente.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBuscarPaciente.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBuscarPaciente.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBuscarPaciente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBuscarPaciente.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBuscarPaciente.Location = new System.Drawing.Point(200, 28);
            this.txtBuscarPaciente.Name = "txtBuscarPaciente";
            this.txtBuscarPaciente.PasswordChar = '\0';
            this.txtBuscarPaciente.PlaceholderText = "Ingrese DNI o nombre del paciente";
            this.txtBuscarPaciente.SelectedText = "";
            this.txtBuscarPaciente.Size = new System.Drawing.Size(440, 36);
            this.txtBuscarPaciente.TabIndex = 1;
            this.txtBuscarPaciente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBuscarPaciente_KeyPress);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label1.Location = new System.Drawing.Point(18, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar Paciente:";
            //
            // Examenes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelPrincipal);
            this.Name = "Examenes";
            this.Text = "Exámenes Médicos";
            this.panelPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelExamenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamenes)).EndInit();
            this.panelBotonesExamenes.ResumeLayout(false);
            this.panelExpediente.ResumeLayout(false);
            this.panelExpediente.PerformLayout();
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelPrincipal;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2Button BtnVolver;
        private Guna.UI2.WinForms.Guna2Panel panelBusqueda;
        private Guna.UI2.WinForms.Guna2Panel panelExpediente;
        private Guna.UI2.WinForms.Guna2Panel panelExamenes;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtBuscarPaciente;
        private Guna.UI2.WinForms.Guna2Button BtnBuscarPaciente;
        private Guna.UI2.WinForms.Guna2Button BtnLimpiar;
        private System.Windows.Forms.Label lblNombrePaciente;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.Label lblNumeroExpediente;
        private Guna.UI2.WinForms.Guna2DataGridView dgvExamenes;
        private Guna.UI2.WinForms.Guna2Panel panelBotonesExamenes;
        private Guna.UI2.WinForms.Guna2Button BtnNuevoExamen;
        private Guna.UI2.WinForms.Guna2Button BtnEditarExamen;
        private Guna.UI2.WinForms.Guna2Button BtnEliminarExamen;
    }
}
