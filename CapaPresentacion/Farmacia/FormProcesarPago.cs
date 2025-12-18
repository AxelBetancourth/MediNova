using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace CapaPresentacion.Farmacia
{
    public partial class FormProcesarPago : Form
    {
        private decimal totalPagar;
        private List<ItemCarrito> itemsVenta;
        public ResultadoPago Resultado { get; private set; }
        private bool actualizandoMonto = false; // Flag para evitar recursión

        public FormProcesarPago(decimal total, List<ItemCarrito> items = null)
        {
            InitializeComponent();
            this.totalPagar = total;
            this.itemsVenta = items ?? new List<ItemCarrito>();
            this.Load += FormProcesarPago_Load;
        }

        private void FormProcesarPago_Load(object sender, EventArgs e)
        {
            lblTotalValor.Text = string.Format("L {0:#,##0.00}", totalPagar);

            // Mostrar resumen detallado de productos
            MostrarResumenDetallado();

            // Configurar ComboBox de métodos de pago
            cboMetodo1.Items.AddRange(new object[] { "Efectivo", "Tarjeta de Crédito", "Tarjeta de Débito", "Transferencia Bancaria" });
            cboMetodo2.Items.AddRange(new object[] { "Efectivo", "Tarjeta de Crédito", "Tarjeta de Débito", "Transferencia Bancaria" });

            cboMetodo1.SelectedIndex = 0;
            txtMonto1.Text = totalPagar.ToString("F2");

            ActualizarVisibilidadCampos();
        }

        private void ChkPagoMixto_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadCampos();

            if (!chkPagoMixto.Checked)
            {
                txtMonto1.Text = totalPagar.ToString("F2");
                txtMonto2.Text = "0";
                cboMetodo2.SelectedIndex = -1;
            }
            else
            {
                // Al activar pago mixto, dividir el total
                decimal mitad = totalPagar / 2;
                txtMonto1.Text = mitad.ToString("F2");
                txtMonto2.Text = mitad.ToString("F2");
            }
        }

        private void ActualizarVisibilidadCampos()
        {
            bool pagoMixto = chkPagoMixto.Checked;

            panelMetodo2.Enabled = pagoMixto;
            panelMetodo2.Visible = pagoMixto;

            // Determinar si hay efectivo en alguno de los métodos
            bool esEfectivoMetodo1 = cboMetodo1.SelectedItem != null && cboMetodo1.SelectedItem.ToString() == "Efectivo";
            bool esEfectivoMetodo2 = pagoMixto && cboMetodo2.SelectedItem != null && cboMetodo2.SelectedItem.ToString() == "Efectivo";

            panelEfectivo.Visible = esEfectivoMetodo1 || esEfectivoMetodo2;

            // Actualizar el label para indicar qué método es efectivo
            if (esEfectivoMetodo1 && !pagoMixto)
            {
                lblMontoPagado.Text = "Monto Recibido en Efectivo:";
            }
            else if (esEfectivoMetodo1 && pagoMixto)
            {
                lblMontoPagado.Text = "Monto Recibido en Efectivo (Método 1):";
            }
            else if (esEfectivoMetodo2)
            {
                lblMontoPagado.Text = "Monto Recibido en Efectivo (Método 2):";
            }
        }

        private void CboMetodo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadCampos();
            CalcularCambio();
        }

        private void CboMetodo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadCampos();
            CalcularCambio();
        }

        private void TxtMonto1_TextChanged(object sender, EventArgs e)
        {
            if (chkPagoMixto.Checked && !actualizandoMonto)
            {
                RecalcularMontoRestante(1);
            }
            CalcularCambio();
        }

        private void TxtMonto2_TextChanged(object sender, EventArgs e)
        {
            if (chkPagoMixto.Checked && !actualizandoMonto)
            {
                RecalcularMontoRestante(2);
            }
            CalcularCambio();
        }

        private void RecalcularMontoRestante(int montoModificado)
        {
            try
            {
                actualizandoMonto = true;

                if (montoModificado == 1)
                {
                    // El usuario modificó monto1, calcular monto2 automáticamente
                    if (decimal.TryParse(txtMonto1.Text, out decimal monto1))
                    {
                        decimal monto2 = totalPagar - monto1;
                        if (monto2 >= 0)
                        {
                            txtMonto2.Text = monto2.ToString("F2");
                        }
                        else
                        {
                            txtMonto2.Text = "0.00";
                        }
                    }
                }
                else if (montoModificado == 2)
                {
                    // El usuario modificó monto2, calcular monto1 automáticamente
                    if (decimal.TryParse(txtMonto2.Text, out decimal monto2))
                    {
                        decimal monto1 = totalPagar - monto2;
                        if (monto1 >= 0)
                        {
                            txtMonto1.Text = monto1.ToString("F2");
                        }
                        else
                        {
                            txtMonto1.Text = "0.00";
                        }
                    }
                }
            }
            finally
            {
                actualizandoMonto = false;
            }
        }

        private void TxtMontoPagado_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
        }

        private void CalcularCambio()
        {
            try
            {
                // Determinar montos y métodos
                decimal.TryParse(txtMonto1.Text, out decimal monto1);
                decimal.TryParse(txtMonto2.Text, out decimal monto2);
                decimal.TryParse(txtMontoPagado.Text, out decimal montoPagado);

                bool esEfectivoMetodo1 = cboMetodo1.SelectedItem != null && cboMetodo1.SelectedItem.ToString() == "Efectivo";
                bool esEfectivoMetodo2 = chkPagoMixto.Checked && cboMetodo2.SelectedItem != null && cboMetodo2.SelectedItem.ToString() == "Efectivo";

                decimal cambio = 0;

                if (!chkPagoMixto.Checked)
                {
                    // Pago simple
                    if (esEfectivoMetodo1)
                    {
                        cambio = montoPagado - totalPagar;
                    }
                }
                else
                {
                    // Pago mixto - calcular cambio solo sobre el efectivo
                    if (esEfectivoMetodo1)
                    {
                        // Efectivo es método 1, el método 2 es fijo
                        cambio = montoPagado - monto1;
                    }
                    else if (esEfectivoMetodo2)
                    {
                        // Efectivo es método 2, el método 1 es fijo
                        cambio = montoPagado - monto2;
                    }
                }

                lblCambioValor.Text = string.Format("L {0:#,##0.00}", (cambio >= 0 ? cambio : 0));
                lblCambioValor.ForeColor = cambio >= 0 ? Color.FromArgb(76, 175, 80) : Color.FromArgb(255, 71, 87);
            }
            catch
            {
                lblCambioValor.Text = "L 0.00";
                lblCambioValor.ForeColor = Color.FromArgb(128, 128, 128);
            }
        }

        private void MostrarResumenDetallado()
        {
            if (itemsVenta == null || itemsVenta.Count == 0)
            {
                dgvDetalles.DataSource = null;
                return;
            }

            // Agrupar por tipo para mostrar claramente
            var recetas = itemsVenta.Where(i => i.Tipo == "Medicamento").ToList();
            var consultas = itemsVenta.Where(i => i.Tipo == "Consulta").ToList();
            var examenes = itemsVenta.Where(i => i.Tipo == "Examen").ToList();

            // Crear lista de detalles para el DataGridView
            var detalles = new List<DetalleVentaDisplay>();

            // Agregar medicamentos/recetas
            if (recetas.Count > 0)
            {
                detalles.Add(new DetalleVentaDisplay
                {
                    Tipo = "─── MEDICAMENTOS ───",
                    Descripcion = "",
                    Cantidad = "",
                    PrecioUnitario = "",
                    Subtotal = "",
                    EsEncabezado = true
                });

                foreach (var item in recetas)
                {
                    detalles.Add(new DetalleVentaDisplay
                    {
                        Tipo = "Medicamento",
                        Descripcion = item.Descripcion,
                        Cantidad = item.Cantidad.ToString(),
                        PrecioUnitario = string.Format("L {0:#,##0.00}", item.PrecioUnitario),
                        Subtotal = string.Format("L {0:#,##0.00}", item.Subtotal),
                        EsEncabezado = false
                    });
                }
            }

            // Agregar consultas
            if (consultas.Count > 0)
            {
                detalles.Add(new DetalleVentaDisplay
                {
                    Tipo = "─── CONSULTAS ───",
                    Descripcion = "",
                    Cantidad = "",
                    PrecioUnitario = "",
                    Subtotal = "",
                    EsEncabezado = true
                });

                foreach (var item in consultas)
                {
                    detalles.Add(new DetalleVentaDisplay
                    {
                        Tipo = "Consulta",
                        Descripcion = item.Descripcion,
                        Cantidad = item.Cantidad.ToString(),
                        PrecioUnitario = string.Format("L {0:#,##0.00}", item.PrecioUnitario),
                        Subtotal = string.Format("L {0:#,##0.00}", item.Subtotal),
                        EsEncabezado = false
                    });
                }
            }

            // Agregar exámenes
            if (examenes.Count > 0)
            {
                detalles.Add(new DetalleVentaDisplay
                {
                    Tipo = "─── EXÁMENES ───",
                    Descripcion = "",
                    Cantidad = "",
                    PrecioUnitario = "",
                    Subtotal = "",
                    EsEncabezado = true
                });

                foreach (var item in examenes)
                {
                    detalles.Add(new DetalleVentaDisplay
                    {
                        Tipo = "Examen",
                        Descripcion = item.Descripcion,
                        Cantidad = item.Cantidad.ToString(),
                        PrecioUnitario = string.Format("L {0:#,##0.00}", item.PrecioUnitario),
                        Subtotal = string.Format("L {0:#,##0.00}", item.Subtotal),
                        EsEncabezado = false
                    });
                }
            }

            dgvDetalles.DataSource = detalles;

            // Aplicar estilos a las filas de encabezado
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.DataBoundItem is DetalleVentaDisplay detalle && detalle.EsEncabezado)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                    row.DefaultCellStyle.Font = new Font(dgvDetalles.Font, FontStyle.Bold);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(33, 150, 243);
                }
            }
        }

        private void BtnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar método 1
                if (cboMetodo1.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione el método de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtMonto1.Text, out decimal monto1) || monto1 <= 0)
                {
                    MessageBox.Show("Ingrese un monto válido para el primer método de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal monto2 = 0;
                string metodoPago2 = null;

                // Validar método 2 si es pago mixto
                if (chkPagoMixto.Checked)
                {
                    if (cboMetodo2.SelectedIndex == -1)
                    {
                        MessageBox.Show("Seleccione el segundo método de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!decimal.TryParse(txtMonto2.Text, out monto2) || monto2 <= 0)
                    {
                        MessageBox.Show("Ingrese un monto válido para el segundo método de pago", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    decimal totalMetodos = monto1 + monto2;
                    if (Math.Abs(totalMetodos - totalPagar) > 0.01m) // Tolerancia de 1 centavo
                    {
                        MessageBox.Show(string.Format("La suma de los pagos (L {0:#,##0.00}) debe ser igual al total (L {1:#,##0.00})", totalMetodos, totalPagar), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    metodoPago2 = cboMetodo2.SelectedItem.ToString();
                }
                else
                {
                    decimal diferencia = Math.Abs(monto1 - totalPagar);
                    if (diferencia > 0.01m) // Tolerancia de 1 centavo
                    {
                        MessageBox.Show(string.Format("El monto debe ser igual al total (L {0:#,##0.00})", totalPagar), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Validar efectivo y calcular cambio
                bool esEfectivoMetodo1 = cboMetodo1.SelectedItem.ToString() == "Efectivo";
                bool esEfectivoMetodo2 = chkPagoMixto.Checked && cboMetodo2.SelectedItem != null && cboMetodo2.SelectedItem.ToString() == "Efectivo";

                decimal montoPagado = 0;
                decimal cambio = 0;

                if (esEfectivoMetodo1 || esEfectivoMetodo2)
                {
                    if (!decimal.TryParse(txtMontoPagado.Text, out montoPagado) || montoPagado <= 0)
                    {
                        MessageBox.Show("Ingrese el monto recibido en efectivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Calcular cambio según el método que sea efectivo
                    if (!chkPagoMixto.Checked)
                    {
                        // Pago simple con efectivo
                        if (montoPagado < totalPagar)
                        {
                            MessageBox.Show(string.Format("El monto recibido (L {0:#,##0.00}) debe ser mayor o igual al total (L {1:#,##0.00})", montoPagado, totalPagar), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        cambio = montoPagado - totalPagar;
                    }
                    else
                    {
                        // Pago mixto - validar que el efectivo cubra su parte
                        if (esEfectivoMetodo1)
                        {
                            if (montoPagado < monto1)
                            {
                                MessageBox.Show(string.Format("El monto recibido en efectivo (L {0:#,##0.00}) debe ser mayor o igual al monto asignado al efectivo (L {1:#,##0.00})", montoPagado, monto1), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            cambio = montoPagado - monto1;
                        }
                        else if (esEfectivoMetodo2)
                        {
                            if (montoPagado < monto2)
                            {
                                MessageBox.Show(string.Format("El monto recibido en efectivo (L {0:#,##0.00}) debe ser mayor o igual al monto asignado al efectivo (L {1:#,##0.00})", montoPagado, monto2), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            cambio = montoPagado - monto2;
                        }
                    }
                }
                else
                {
                    // No hay efectivo, el monto pagado es igual al total
                    montoPagado = totalPagar;
                }

                // Crear resultado
                Resultado = new ResultadoPago
                {
                    MetodoPago1 = cboMetodo1.SelectedItem.ToString(),
                    MontoPago1 = monto1,
                    MetodoPago2 = metodoPago2,
                    MontoPago2 = chkPagoMixto.Checked ? (decimal?)monto2 : null,
                    MontoPagado = montoPagado,
                    Cambio = cambio
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    // Clase auxiliar para mostrar los detalles en el DataGridView
    public class DetalleVentaDisplay
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public string PrecioUnitario { get; set; }
        public string Subtotal { get; set; }
        public bool EsEncabezado { get; set; }
    }
}
