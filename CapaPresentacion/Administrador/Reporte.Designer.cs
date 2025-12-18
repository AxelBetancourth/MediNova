namespace CapaPresentacion.Administrador
{
    partial class Reporte
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
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelContenido = new Guna.UI2.WinForms.Guna2Panel();
            this.btnAbrirPowerBI = new Guna.UI2.WinForms.Guna2Button();
            this.lblDescripcion = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblIcono = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelHeader.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1100, 98);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHeader_Paint);
            // 
            // lblTitulo
            // 
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(40, 25);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(359, 56);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reportes y Análisis";
            this.lblTitulo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.Controls.Add(this.btnAbrirPowerBI);
            this.panelContenido.Controls.Add(this.lblDescripcion);
            this.panelContenido.Controls.Add(this.lblIcono);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 98);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(4);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1100, 877);
            this.panelContenido.TabIndex = 1;
            this.panelContenido.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenido_Paint);
            // 
            // btnAbrirPowerBI
            // 
            this.btnAbrirPowerBI.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAbrirPowerBI.BorderRadius = 10;
            this.btnAbrirPowerBI.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAbrirPowerBI.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAbrirPowerBI.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAbrirPowerBI.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAbrirPowerBI.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnAbrirPowerBI.ForeColor = System.Drawing.Color.White;
            this.btnAbrirPowerBI.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(33)))), ((int)(((byte)(104)))));
            this.btnAbrirPowerBI.Location = new System.Drawing.Point(264, 492);
            this.btnAbrirPowerBI.Margin = new System.Windows.Forms.Padding(4);
            this.btnAbrirPowerBI.Name = "btnAbrirPowerBI";
            this.btnAbrirPowerBI.Size = new System.Drawing.Size(553, 98);
            this.btnAbrirPowerBI.TabIndex = 2;
            this.btnAbrirPowerBI.Text = "Abrir Dashboard Power BI";
            this.btnAbrirPowerBI.Click += new System.EventHandler(this.BtnAbrirPowerBI_Click);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescripcion.BackColor = System.Drawing.Color.Transparent;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblDescripcion.Location = new System.Drawing.Point(253, 406);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(545, 30);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Visualiza métricas, estadísticas y análisis del sistema MediNova";
            this.lblDescripcion.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIcono
            // 
            this.lblIcono.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblIcono.BackColor = System.Drawing.Color.Transparent;
            this.lblIcono.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblIcono.Location = new System.Drawing.Point(264, 186);
            this.lblIcono.Margin = new System.Windows.Forms.Padding(4);
            this.lblIcono.Name = "lblIcono";
            this.lblIcono.Size = new System.Drawing.Size(546, 108);
            this.lblIcono.TabIndex = 0;
            this.lblIcono.Text = "ESTADÍSTICAS";
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 975);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Reporte";
            this.Text = "Reporte";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.panelContenido.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2Panel panelContenido;
        private Guna.UI2.WinForms.Guna2Button btnAbrirPowerBI;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDescripcion;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIcono;
    }
}