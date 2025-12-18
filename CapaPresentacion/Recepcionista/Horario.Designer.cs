namespace CapaPresentacion.Recepcionista
{
    partial class Horario
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
            this.lbDay = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelMañana = new Guna.UI2.WinForms.Guna2Panel();
            this.panelTarde = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMañana = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTarde = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnRegresar = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegresar)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDay
            // 
            this.lbDay.BackColor = System.Drawing.Color.Transparent;
            this.lbDay.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lbDay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
            this.lbDay.Location = new System.Drawing.Point(20, 15);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(196, 34);
            this.lbDay.TabIndex = 46;
            this.lbDay.Text = "Día seleccionado";
            this.lbDay.Click += new System.EventHandler(this.lbDay_Click);
            // 
            // panelMañana
            // 
            this.panelMañana.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMañana.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMañana.BorderRadius = 12;
            this.panelMañana.BorderThickness = 2;
            this.panelMañana.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMañana.Location = new System.Drawing.Point(20, 125);
            this.panelMañana.Name = "panelMañana";
            this.panelMañana.Size = new System.Drawing.Size(482, 475);
            this.panelMañana.TabIndex = 71;
            //
            // panelTarde
            //
            this.panelTarde.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelTarde.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelTarde.BorderRadius = 12;
            this.panelTarde.BorderThickness = 2;
            this.panelTarde.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelTarde.Location = new System.Drawing.Point(518, 125);
            this.panelTarde.Name = "panelTarde";
            this.panelTarde.Size = new System.Drawing.Size(482, 475);
            this.panelTarde.TabIndex = 58;
            //
            // lblMañana
            //
            this.lblMañana.BackColor = System.Drawing.Color.Transparent;
            this.lblMañana.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblMañana.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblMañana.Location = new System.Drawing.Point(20, 90);
            this.lblMañana.Name = "lblMañana";
            this.lblMañana.Size = new System.Drawing.Size(197, 27);
            this.lblMañana.TabIndex = 72;
            this.lblMañana.Text = "🌅 HORARIO MAÑANA";
            //
            // lblTarde
            //
            this.lblTarde.BackColor = System.Drawing.Color.Transparent;
            this.lblTarde.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTarde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblTarde.Location = new System.Drawing.Point(518, 90);
            this.lblTarde.Name = "lblTarde";
            this.lblTarde.Size = new System.Drawing.Size(184, 27);
            this.lblTarde.TabIndex = 73;
            this.lblTarde.Text = "🌇 HORARIO TARDE";
            // 
            // btnRegresar
            // 
            this.btnRegresar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegresar.Image = global::CapaPresentacion.Properties.Resources.flecha_izquierda;
            this.btnRegresar.ImageRotate = 0F;
            this.btnRegresar.Location = new System.Drawing.Point(920, 15);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(40, 40);
            this.btnRegresar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRegresar.TabIndex = 70;
            this.btnRegresar.TabStop = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click_1);
            //
            // Horario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1020, 620);
            this.Controls.Add(this.lblTarde);
            this.Controls.Add(this.lblMañana);
            this.Controls.Add(this.panelMañana);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.panelTarde);
            this.Controls.Add(this.lbDay);
            this.Name = "Horario";
            this.Text = "Horario - Seleccione una hora disponible";
            this.Load += new System.EventHandler(this.Horario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnRegresar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel lbDay;
        private Guna.UI2.WinForms.Guna2Panel panelMañana;
        private Guna.UI2.WinForms.Guna2Panel panelTarde;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMañana;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTarde;
        private Guna.UI2.WinForms.Guna2PictureBox btnRegresar;
    }
}