namespace CapaPresentacion.Recepcionista
{
    partial class ucHoraSlot
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
            this.panelContenedor = new Guna.UI2.WinForms.Guna2Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            //
            // panelContenedor
            //
            this.panelContenedor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(213)))), ((int)(((byte)(115)))));
            this.panelContenedor.BorderRadius = 10;
            this.panelContenedor.BorderThickness = 2;
            this.panelContenedor.Controls.Add(this.lblHora);
            this.panelContenedor.Controls.Add(this.lblEstado);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(215, 55);
            this.panelContenedor.TabIndex = 0;
            //
            // lblEstado
            //
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblEstado.Location = new System.Drawing.Point(130, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(80, 55);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "DISPONIBLE";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblHora
            //
            this.lblHora.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHora.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHora.Location = new System.Drawing.Point(0, 0);
            this.lblHora.Name = "lblHora";
            this.lblHora.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblHora.Size = new System.Drawing.Size(135, 55);
            this.lblHora.TabIndex = 0;
            this.lblHora.Text = "08:00 - 09:00 AM";
            this.lblHora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ucHoraSlot
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContenedor);
            this.Name = "ucHoraSlot";
            this.Size = new System.Drawing.Size(215, 55);
            this.panelContenedor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2Panel panelContenedor;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblHora;
    }
}