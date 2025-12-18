namespace CapaPresentacion.Farmacia
{
    partial class FormProcesarPago
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelPrincipal = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelTotalInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panelDetalles = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvDetalles = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panelHeaderDetalles = new Guna.UI2.WinForms.Guna2Panel();
            this.lblHeaderDetalles = new System.Windows.Forms.Label();
            this.panelPago = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutPago = new System.Windows.Forms.TableLayoutPanel();
            this.panelMetodo1 = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutMetodo1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMetodo1 = new System.Windows.Forms.Label();
            this.cboMetodo1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMonto1 = new System.Windows.Forms.Label();
            this.txtMonto1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkPagoMixto = new Guna.UI2.WinForms.Guna2CheckBox();
            this.panelMetodo2 = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutMetodo2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMetodo2 = new System.Windows.Forms.Label();
            this.cboMetodo2 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMonto2 = new System.Windows.Forms.Label();
            this.txtMonto2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelEfectivo = new Guna.UI2.WinForms.Guna2Panel();
            this.tableLayoutEfectivo = new System.Windows.Forms.TableLayoutPanel();
            this.lblMontoPagado = new System.Windows.Forms.Label();
            this.txtMontoPagado = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelCambio = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCambioValor = new System.Windows.Forms.Label();
            this.lblCambio = new System.Windows.Forms.Label();
            this.panelBotones = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnCancelar = new Guna.UI2.WinForms.Guna2Button();
            this.BtnProcesar = new Guna.UI2.WinForms.Guna2Button();
            this.panelPrincipal.SuspendLayout();
            this.tableLayoutPrincipal.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableLayoutHeader.SuspendLayout();
            this.panelTotalInfo.SuspendLayout();
            this.panelDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.panelHeaderDetalles.SuspendLayout();
            this.panelPago.SuspendLayout();
            this.tableLayoutPago.SuspendLayout();
            this.panelMetodo1.SuspendLayout();
            this.tableLayoutMetodo1.SuspendLayout();
            this.panelMetodo2.SuspendLayout();
            this.tableLayoutMetodo2.SuspendLayout();
            this.panelEfectivo.SuspendLayout();
            this.tableLayoutEfectivo.SuspendLayout();
            this.panelCambio.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.flowLayoutBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // panelPrincipal
            //
            this.panelPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.panelPrincipal.Controls.Add(this.tableLayoutPrincipal);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Padding = new System.Windows.Forms.Padding(15);
            this.panelPrincipal.Size = new System.Drawing.Size(900, 750);
            this.panelPrincipal.TabIndex = 0;
            //
            // tableLayoutPrincipal
            //
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.panelDetalles, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.panelPago, 0, 2);
            this.tableLayoutPrincipal.Controls.Add(this.panelBotones, 0, 3);
            this.tableLayoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPrincipal.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPrincipal.Name = "tableLayoutPrincipal";
            this.tableLayoutPrincipal.RowCount = 4;
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPrincipal.Size = new System.Drawing.Size(870, 720);
            this.tableLayoutPrincipal.TabIndex = 0;
            //
            // panelHeader
            //
            this.panelHeader.BackColor = System.Drawing.Color.Transparent;
            this.panelHeader.BorderRadius = 10;
            this.panelHeader.Controls.Add(this.tableLayoutHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.FillColor = System.Drawing.Color.White;
            this.panelHeader.Location = new System.Drawing.Point(3, 3);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelHeader.ShadowDecoration.BorderRadius = 10;
            this.panelHeader.ShadowDecoration.Depth = 5;
            this.panelHeader.ShadowDecoration.Enabled = true;
            this.panelHeader.Size = new System.Drawing.Size(864, 74);
            this.panelHeader.TabIndex = 0;
            //
            // tableLayoutHeader
            //
            this.tableLayoutHeader.ColumnCount = 2;
            this.tableLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutHeader.Controls.Add(this.lblTitulo, 0, 0);
            this.tableLayoutHeader.Controls.Add(this.panelTotalInfo, 1, 0);
            this.tableLayoutHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutHeader.Location = new System.Drawing.Point(15, 10);
            this.tableLayoutHeader.Name = "tableLayoutHeader";
            this.tableLayoutHeader.RowCount = 1;
            this.tableLayoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutHeader.Size = new System.Drawing.Size(834, 54);
            this.tableLayoutHeader.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(494, 54);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Procesar Pago";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelTotalInfo
            //
            this.panelTotalInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.panelTotalInfo.BorderRadius = 8;
            this.panelTotalInfo.Controls.Add(this.lblTotalValor);
            this.panelTotalInfo.Controls.Add(this.lblTotal);
            this.panelTotalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTotalInfo.Location = new System.Drawing.Point(503, 3);
            this.panelTotalInfo.Name = "panelTotalInfo";
            this.panelTotalInfo.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelTotalInfo.Size = new System.Drawing.Size(328, 48);
            this.panelTotalInfo.TabIndex = 1;
            //
            // lblTotalValor
            //
            this.lblTotalValor.AutoSize = true;
            this.lblTotalValor.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalValor.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.lblTotalValor.Location = new System.Drawing.Point(200, 5);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblTotalValor.Size = new System.Drawing.Size(118, 34);
            this.lblTotalValor.TabIndex = 1;
            this.lblTotalValor.Text = "L 0.00";
            this.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotal.Location = new System.Drawing.Point(10, 5);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblTotal.Size = new System.Drawing.Size(123, 27);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total a Pagar:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelDetalles
            //
            this.panelDetalles.BackColor = System.Drawing.Color.Transparent;
            this.panelDetalles.BorderRadius = 10;
            this.panelDetalles.Controls.Add(this.dgvDetalles);
            this.panelDetalles.Controls.Add(this.panelHeaderDetalles);
            this.panelDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalles.FillColor = System.Drawing.Color.White;
            this.panelDetalles.Location = new System.Drawing.Point(3, 83);
            this.panelDetalles.Name = "panelDetalles";
            this.panelDetalles.ShadowDecoration.BorderRadius = 10;
            this.panelDetalles.ShadowDecoration.Depth = 5;
            this.panelDetalles.ShadowDecoration.Enabled = true;
            this.panelDetalles.Size = new System.Drawing.Size(864, 279);
            this.panelDetalles.TabIndex = 1;
            //
            // dgvDetalles
            //
            this.dgvDetalles.AllowUserToAddRows = false;
            this.dgvDetalles.AllowUserToDeleteRows = false;
            this.dgvDetalles.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDetalles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetalles.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalles.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDetalles.Location = new System.Drawing.Point(0, 45);
            this.dgvDetalles.MultiSelect = false;
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.ReadOnly = true;
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.RowTemplate.Height = 30;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(864, 234);
            this.dgvDetalles.TabIndex = 1;
            this.dgvDetalles.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDetalles.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDetalles.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDetalles.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDetalles.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDetalles.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDetalles.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDetalles.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.dgvDetalles.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDetalles.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dgvDetalles.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDetalles.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetalles.ThemeStyle.HeaderStyle.Height = 35;
            this.dgvDetalles.ThemeStyle.ReadOnly = true;
            this.dgvDetalles.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDetalles.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDetalles.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Arial", 9F);
            this.dgvDetalles.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDetalles.ThemeStyle.RowsStyle.Height = 30;
            this.dgvDetalles.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDetalles.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            //
            // panelHeaderDetalles
            //
            this.panelHeaderDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelHeaderDetalles.Controls.Add(this.lblHeaderDetalles);
            this.panelHeaderDetalles.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderDetalles.Location = new System.Drawing.Point(0, 0);
            this.panelHeaderDetalles.Name = "panelHeaderDetalles";
            this.panelHeaderDetalles.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelHeaderDetalles.Size = new System.Drawing.Size(864, 45);
            this.panelHeaderDetalles.TabIndex = 0;
            //
            // lblHeaderDetalles
            //
            this.lblHeaderDetalles.AutoSize = true;
            this.lblHeaderDetalles.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeaderDetalles.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblHeaderDetalles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHeaderDetalles.Location = new System.Drawing.Point(15, 10);
            this.lblHeaderDetalles.Name = "lblHeaderDetalles";
            this.lblHeaderDetalles.Size = new System.Drawing.Size(161, 18);
            this.lblHeaderDetalles.TabIndex = 0;
            this.lblHeaderDetalles.Text = "Detalle de la Venta";
            this.lblHeaderDetalles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelPago
            //
            this.panelPago.BackColor = System.Drawing.Color.Transparent;
            this.panelPago.BorderRadius = 10;
            this.panelPago.Controls.Add(this.tableLayoutPago);
            this.panelPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPago.FillColor = System.Drawing.Color.White;
            this.panelPago.Location = new System.Drawing.Point(3, 368);
            this.panelPago.Name = "panelPago";
            this.panelPago.Padding = new System.Windows.Forms.Padding(15);
            this.panelPago.ShadowDecoration.BorderRadius = 10;
            this.panelPago.ShadowDecoration.Depth = 5;
            this.panelPago.ShadowDecoration.Enabled = true;
            this.panelPago.Size = new System.Drawing.Size(864, 279);
            this.panelPago.TabIndex = 2;
            //
            // tableLayoutPago
            //
            this.tableLayoutPago.ColumnCount = 2;
            this.tableLayoutPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPago.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPago.Controls.Add(this.panelMetodo1, 0, 0);
            this.tableLayoutPago.Controls.Add(this.chkPagoMixto, 0, 1);
            this.tableLayoutPago.Controls.Add(this.panelMetodo2, 1, 0);
            this.tableLayoutPago.Controls.Add(this.panelEfectivo, 0, 2);
            this.tableLayoutPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPago.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPago.Name = "tableLayoutPago";
            this.tableLayoutPago.RowCount = 3;
            this.tableLayoutPago.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPago.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPago.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPago.Size = new System.Drawing.Size(834, 249);
            this.tableLayoutPago.TabIndex = 0;
            //
            // panelMetodo1
            //
            this.panelMetodo1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMetodo1.BorderRadius = 8;
            this.panelMetodo1.BorderThickness = 1;
            this.panelMetodo1.Controls.Add(this.tableLayoutMetodo1);
            this.panelMetodo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMetodo1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMetodo1.Location = new System.Drawing.Point(3, 3);
            this.panelMetodo1.Name = "panelMetodo1";
            this.panelMetodo1.Padding = new System.Windows.Forms.Padding(10);
            this.panelMetodo1.Size = new System.Drawing.Size(411, 84);
            this.panelMetodo1.TabIndex = 0;
            //
            // tableLayoutMetodo1
            //
            this.tableLayoutMetodo1.ColumnCount = 2;
            this.tableLayoutMetodo1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutMetodo1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMetodo1.Controls.Add(this.lblMetodo1, 0, 0);
            this.tableLayoutMetodo1.Controls.Add(this.cboMetodo1, 1, 0);
            this.tableLayoutMetodo1.Controls.Add(this.lblMonto1, 0, 1);
            this.tableLayoutMetodo1.Controls.Add(this.txtMonto1, 1, 1);
            this.tableLayoutMetodo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMetodo1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutMetodo1.Name = "tableLayoutMetodo1";
            this.tableLayoutMetodo1.RowCount = 2;
            this.tableLayoutMetodo1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutMetodo1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutMetodo1.Size = new System.Drawing.Size(391, 64);
            this.tableLayoutMetodo1.TabIndex = 0;
            //
            // lblMetodo1
            //
            this.lblMetodo1.AutoSize = true;
            this.lblMetodo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMetodo1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMetodo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetodo1.Location = new System.Drawing.Point(3, 0);
            this.lblMetodo1.Name = "lblMetodo1";
            this.lblMetodo1.Size = new System.Drawing.Size(114, 32);
            this.lblMetodo1.TabIndex = 0;
            this.lblMetodo1.Text = "Método de Pago:";
            this.lblMetodo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cboMetodo1
            //
            this.cboMetodo1.BackColor = System.Drawing.Color.Transparent;
            this.cboMetodo1.BorderRadius = 6;
            this.cboMetodo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMetodo1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMetodo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMetodo1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMetodo1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMetodo1.Font = new System.Drawing.Font("Arial", 9F);
            this.cboMetodo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboMetodo1.ItemHeight = 20;
            this.cboMetodo1.Location = new System.Drawing.Point(123, 3);
            this.cboMetodo1.Name = "cboMetodo1";
            this.cboMetodo1.Size = new System.Drawing.Size(265, 26);
            this.cboMetodo1.TabIndex = 1;
            this.cboMetodo1.SelectedIndexChanged += new System.EventHandler(this.CboMetodo1_SelectedIndexChanged);
            //
            // lblMonto1
            //
            this.lblMonto1.AutoSize = true;
            this.lblMonto1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonto1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonto1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMonto1.Location = new System.Drawing.Point(3, 32);
            this.lblMonto1.Name = "lblMonto1";
            this.lblMonto1.Size = new System.Drawing.Size(114, 32);
            this.lblMonto1.TabIndex = 2;
            this.lblMonto1.Text = "Monto:";
            this.lblMonto1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtMonto1
            //
            this.txtMonto1.BorderRadius = 6;
            this.txtMonto1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMonto1.DefaultText = "";
            this.txtMonto1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMonto1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMonto1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMonto1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMonto1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMonto1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMonto1.Font = new System.Drawing.Font("Arial", 9F);
            this.txtMonto1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMonto1.Location = new System.Drawing.Point(123, 35);
            this.txtMonto1.Name = "txtMonto1";
            this.txtMonto1.PasswordChar = '\0';
            this.txtMonto1.PlaceholderText = "0.00";
            this.txtMonto1.SelectedText = "";
            this.txtMonto1.Size = new System.Drawing.Size(265, 26);
            this.txtMonto1.TabIndex = 3;
            this.txtMonto1.TextChanged += new System.EventHandler(this.TxtMonto1_TextChanged);
            //
            // chkPagoMixto
            //
            this.chkPagoMixto.AutoSize = true;
            this.chkPagoMixto.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkPagoMixto.CheckedState.BorderRadius = 2;
            this.chkPagoMixto.CheckedState.BorderThickness = 0;
            this.chkPagoMixto.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkPagoMixto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPagoMixto.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkPagoMixto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkPagoMixto.Location = new System.Drawing.Point(3, 93);
            this.chkPagoMixto.Name = "chkPagoMixto";
            this.chkPagoMixto.Size = new System.Drawing.Size(411, 34);
            this.chkPagoMixto.TabIndex = 1;
            this.chkPagoMixto.Text = "Pago Mixto (Dos Métodos)";
            this.chkPagoMixto.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkPagoMixto.UncheckedState.BorderRadius = 2;
            this.chkPagoMixto.UncheckedState.BorderThickness = 0;
            this.chkPagoMixto.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkPagoMixto.CheckedChanged += new System.EventHandler(this.ChkPagoMixto_CheckedChanged);
            //
            // panelMetodo2
            //
            this.panelMetodo2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMetodo2.BorderRadius = 8;
            this.panelMetodo2.BorderThickness = 1;
            this.panelMetodo2.Controls.Add(this.tableLayoutMetodo2);
            this.panelMetodo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMetodo2.Enabled = false;
            this.panelMetodo2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMetodo2.Location = new System.Drawing.Point(420, 3);
            this.panelMetodo2.Name = "panelMetodo2";
            this.panelMetodo2.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPago.SetRowSpan(this.panelMetodo2, 2);
            this.panelMetodo2.Size = new System.Drawing.Size(411, 124);
            this.panelMetodo2.TabIndex = 2;
            this.panelMetodo2.Visible = false;
            //
            // tableLayoutMetodo2
            //
            this.tableLayoutMetodo2.ColumnCount = 2;
            this.tableLayoutMetodo2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutMetodo2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMetodo2.Controls.Add(this.lblMetodo2, 0, 0);
            this.tableLayoutMetodo2.Controls.Add(this.cboMetodo2, 1, 0);
            this.tableLayoutMetodo2.Controls.Add(this.lblMonto2, 0, 1);
            this.tableLayoutMetodo2.Controls.Add(this.txtMonto2, 1, 1);
            this.tableLayoutMetodo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMetodo2.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutMetodo2.Name = "tableLayoutMetodo2";
            this.tableLayoutMetodo2.RowCount = 2;
            this.tableLayoutMetodo2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutMetodo2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutMetodo2.Size = new System.Drawing.Size(391, 104);
            this.tableLayoutMetodo2.TabIndex = 0;
            //
            // lblMetodo2
            //
            this.lblMetodo2.AutoSize = true;
            this.lblMetodo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMetodo2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMetodo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMetodo2.Location = new System.Drawing.Point(3, 0);
            this.lblMetodo2.Name = "lblMetodo2";
            this.lblMetodo2.Size = new System.Drawing.Size(114, 52);
            this.lblMetodo2.TabIndex = 0;
            this.lblMetodo2.Text = "Segundo Método:";
            this.lblMetodo2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cboMetodo2
            //
            this.cboMetodo2.BackColor = System.Drawing.Color.Transparent;
            this.cboMetodo2.BorderRadius = 6;
            this.cboMetodo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMetodo2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMetodo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMetodo2.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMetodo2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMetodo2.Font = new System.Drawing.Font("Arial", 9F);
            this.cboMetodo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboMetodo2.ItemHeight = 20;
            this.cboMetodo2.Location = new System.Drawing.Point(123, 3);
            this.cboMetodo2.Name = "cboMetodo2";
            this.cboMetodo2.Size = new System.Drawing.Size(265, 26);
            this.cboMetodo2.TabIndex = 1;
            this.cboMetodo2.SelectedIndexChanged += new System.EventHandler(this.CboMetodo2_SelectedIndexChanged);
            //
            // lblMonto2
            //
            this.lblMonto2.AutoSize = true;
            this.lblMonto2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonto2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonto2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMonto2.Location = new System.Drawing.Point(3, 52);
            this.lblMonto2.Name = "lblMonto2";
            this.lblMonto2.Size = new System.Drawing.Size(114, 52);
            this.lblMonto2.TabIndex = 2;
            this.lblMonto2.Text = "Monto:";
            this.lblMonto2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtMonto2
            //
            this.txtMonto2.BorderRadius = 6;
            this.txtMonto2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMonto2.DefaultText = "";
            this.txtMonto2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMonto2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMonto2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMonto2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMonto2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMonto2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMonto2.Font = new System.Drawing.Font("Arial", 9F);
            this.txtMonto2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMonto2.Location = new System.Drawing.Point(123, 55);
            this.txtMonto2.Name = "txtMonto2";
            this.txtMonto2.PasswordChar = '\0';
            this.txtMonto2.PlaceholderText = "0.00";
            this.txtMonto2.SelectedText = "";
            this.txtMonto2.Size = new System.Drawing.Size(265, 46);
            this.txtMonto2.TabIndex = 3;
            this.txtMonto2.TextChanged += new System.EventHandler(this.TxtMonto2_TextChanged);
            //
            // panelEfectivo
            //
            this.panelEfectivo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelEfectivo.BorderRadius = 8;
            this.panelEfectivo.BorderThickness = 2;
            this.tableLayoutPago.SetColumnSpan(this.panelEfectivo, 2);
            this.panelEfectivo.Controls.Add(this.tableLayoutEfectivo);
            this.panelEfectivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEfectivo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(252)))), ((int)(((byte)(247)))));
            this.panelEfectivo.Location = new System.Drawing.Point(3, 133);
            this.panelEfectivo.Name = "panelEfectivo";
            this.panelEfectivo.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelEfectivo.Size = new System.Drawing.Size(828, 113);
            this.panelEfectivo.TabIndex = 3;
            this.panelEfectivo.Visible = false;
            //
            // tableLayoutEfectivo
            //
            this.tableLayoutEfectivo.ColumnCount = 2;
            this.tableLayoutEfectivo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutEfectivo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutEfectivo.Controls.Add(this.lblMontoPagado, 0, 0);
            this.tableLayoutEfectivo.Controls.Add(this.txtMontoPagado, 0, 1);
            this.tableLayoutEfectivo.Controls.Add(this.panelCambio, 1, 0);
            this.tableLayoutEfectivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutEfectivo.Location = new System.Drawing.Point(15, 10);
            this.tableLayoutEfectivo.Name = "tableLayoutEfectivo";
            this.tableLayoutEfectivo.RowCount = 2;
            this.tableLayoutEfectivo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutEfectivo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutEfectivo.Size = new System.Drawing.Size(798, 93);
            this.tableLayoutEfectivo.TabIndex = 0;
            //
            // lblMontoPagado
            //
            this.lblMontoPagado.AutoSize = true;
            this.lblMontoPagado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMontoPagado.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblMontoPagado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMontoPagado.Location = new System.Drawing.Point(3, 0);
            this.lblMontoPagado.Name = "lblMontoPagado";
            this.lblMontoPagado.Size = new System.Drawing.Size(393, 30);
            this.lblMontoPagado.TabIndex = 0;
            this.lblMontoPagado.Text = "Monto Recibido en Efectivo:";
            this.lblMontoPagado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtMontoPagado
            //
            this.txtMontoPagado.BorderRadius = 8;
            this.txtMontoPagado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMontoPagado.DefaultText = "";
            this.txtMontoPagado.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMontoPagado.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMontoPagado.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMontoPagado.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMontoPagado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMontoPagado.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.txtMontoPagado.Font = new System.Drawing.Font("Arial", 11F);
            this.txtMontoPagado.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.txtMontoPagado.Location = new System.Drawing.Point(4, 34);
            this.txtMontoPagado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMontoPagado.Name = "txtMontoPagado";
            this.txtMontoPagado.PasswordChar = '\0';
            this.txtMontoPagado.PlaceholderText = "0.00";
            this.txtMontoPagado.SelectedText = "";
            this.txtMontoPagado.Size = new System.Drawing.Size(391, 55);
            this.txtMontoPagado.TabIndex = 1;
            this.txtMontoPagado.TextChanged += new System.EventHandler(this.TxtMontoPagado_TextChanged);
            //
            // panelCambio
            //
            this.panelCambio.BackColor = System.Drawing.Color.White;
            this.panelCambio.BorderRadius = 8;
            this.panelCambio.Controls.Add(this.lblCambioValor);
            this.panelCambio.Controls.Add(this.lblCambio);
            this.panelCambio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCambio.Location = new System.Drawing.Point(402, 3);
            this.panelCambio.Name = "panelCambio";
            this.panelCambio.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.tableLayoutEfectivo.SetRowSpan(this.panelCambio, 2);
            this.panelCambio.Size = new System.Drawing.Size(393, 87);
            this.panelCambio.TabIndex = 2;
            //
            // lblCambioValor
            //
            this.lblCambioValor.AutoSize = true;
            this.lblCambioValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCambioValor.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.lblCambioValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblCambioValor.Location = new System.Drawing.Point(15, 40);
            this.lblCambioValor.Name = "lblCambioValor";
            this.lblCambioValor.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblCambioValor.Size = new System.Drawing.Size(111, 37);
            this.lblCambioValor.TabIndex = 1;
            this.lblCambioValor.Text = "L 0.00";
            this.lblCambioValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblCambio
            //
            this.lblCambio.AutoSize = true;
            this.lblCambio.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCambio.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblCambio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCambio.Location = new System.Drawing.Point(15, 5);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblCambio.Size = new System.Drawing.Size(70, 28);
            this.lblCambio.TabIndex = 0;
            this.lblCambio.Text = "Cambio:";
            this.lblCambio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelBotones
            //
            this.panelBotones.BackColor = System.Drawing.Color.Transparent;
            this.panelBotones.Controls.Add(this.flowLayoutBotones);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.Location = new System.Drawing.Point(3, 653);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelBotones.Size = new System.Drawing.Size(864, 64);
            this.panelBotones.TabIndex = 3;
            //
            // flowLayoutBotones
            //
            this.flowLayoutBotones.Controls.Add(this.BtnCancelar);
            this.flowLayoutBotones.Controls.Add(this.BtnProcesar);
            this.flowLayoutBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutBotones.Location = new System.Drawing.Point(0, 10);
            this.flowLayoutBotones.Name = "flowLayoutBotones";
            this.flowLayoutBotones.Size = new System.Drawing.Size(864, 54);
            this.flowLayoutBotones.TabIndex = 0;
            //
            // BtnCancelar
            //
            this.BtnCancelar.BorderRadius = 8;
            this.BtnCancelar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnCancelar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnCancelar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnCancelar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnCancelar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.BtnCancelar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.BtnCancelar.ForeColor = System.Drawing.Color.White;
            this.BtnCancelar.Location = new System.Drawing.Point(711, 3);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(150, 45);
            this.BtnCancelar.TabIndex = 1;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            //
            // BtnProcesar
            //
            this.BtnProcesar.BorderRadius = 8;
            this.BtnProcesar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BtnProcesar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BtnProcesar.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BtnProcesar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BtnProcesar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.BtnProcesar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.BtnProcesar.ForeColor = System.Drawing.Color.White;
            this.BtnProcesar.Location = new System.Drawing.Point(525, 3);
            this.BtnProcesar.Name = "BtnProcesar";
            this.BtnProcesar.Size = new System.Drawing.Size(180, 45);
            this.BtnProcesar.TabIndex = 0;
            this.BtnProcesar.Text = "Procesar Venta";
            this.BtnProcesar.Click += new System.EventHandler(this.BtnProcesar_Click);
            //
            // FormProcesarPago
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 750);
            this.Controls.Add(this.panelPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProcesarPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Procesar Pago";
            this.panelPrincipal.ResumeLayout(false);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.tableLayoutHeader.ResumeLayout(false);
            this.tableLayoutHeader.PerformLayout();
            this.panelTotalInfo.ResumeLayout(false);
            this.panelTotalInfo.PerformLayout();
            this.panelDetalles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.panelHeaderDetalles.ResumeLayout(false);
            this.panelHeaderDetalles.PerformLayout();
            this.panelPago.ResumeLayout(false);
            this.tableLayoutPago.ResumeLayout(false);
            this.tableLayoutPago.PerformLayout();
            this.panelMetodo1.ResumeLayout(false);
            this.tableLayoutMetodo1.ResumeLayout(false);
            this.tableLayoutMetodo1.PerformLayout();
            this.panelMetodo2.ResumeLayout(false);
            this.tableLayoutMetodo2.ResumeLayout(false);
            this.tableLayoutMetodo2.PerformLayout();
            this.panelEfectivo.ResumeLayout(false);
            this.tableLayoutEfectivo.ResumeLayout(false);
            this.tableLayoutEfectivo.PerformLayout();
            this.panelCambio.ResumeLayout(false);
            this.panelCambio.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.flowLayoutBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelPrincipal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPrincipal;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutHeader;
        private System.Windows.Forms.Label lblTitulo;
        private Guna.UI2.WinForms.Guna2Panel panelTotalInfo;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.Label lblTotal;
        private Guna.UI2.WinForms.Guna2Panel panelDetalles;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDetalles;
        private Guna.UI2.WinForms.Guna2Panel panelHeaderDetalles;
        private System.Windows.Forms.Label lblHeaderDetalles;
        private Guna.UI2.WinForms.Guna2Panel panelPago;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPago;
        private Guna.UI2.WinForms.Guna2Panel panelMetodo1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMetodo1;
        private System.Windows.Forms.Label lblMetodo1;
        private Guna.UI2.WinForms.Guna2ComboBox cboMetodo1;
        private System.Windows.Forms.Label lblMonto1;
        private Guna.UI2.WinForms.Guna2TextBox txtMonto1;
        private Guna.UI2.WinForms.Guna2CheckBox chkPagoMixto;
        private Guna.UI2.WinForms.Guna2Panel panelMetodo2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMetodo2;
        private System.Windows.Forms.Label lblMetodo2;
        private Guna.UI2.WinForms.Guna2ComboBox cboMetodo2;
        private System.Windows.Forms.Label lblMonto2;
        private Guna.UI2.WinForms.Guna2TextBox txtMonto2;
        private Guna.UI2.WinForms.Guna2Panel panelEfectivo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutEfectivo;
        private System.Windows.Forms.Label lblMontoPagado;
        private Guna.UI2.WinForms.Guna2TextBox txtMontoPagado;
        private Guna.UI2.WinForms.Guna2Panel panelCambio;
        private System.Windows.Forms.Label lblCambioValor;
        private System.Windows.Forms.Label lblCambio;
        private Guna.UI2.WinForms.Guna2Panel panelBotones;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutBotones;
        private Guna.UI2.WinForms.Guna2Button BtnCancelar;
        private Guna.UI2.WinForms.Guna2Button BtnProcesar;
    }
}
