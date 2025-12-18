using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaNegocio.Compartido;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion.Administrador
{
    public partial class Empresa : Form
    {
        private TEmpresa empresaActual;
        private byte[] logoBytes;

        public Empresa()
        {
            InitializeComponent();
            CargarInformacionEmpresa();
        }

        private void CargarInformacionEmpresa()
        {
            try
            {
                using (var nEmpresa = new NEmpresa())
                {
                    empresaActual = nEmpresa.ObtenerInformacion();

                    if (empresaActual != null)
                    {
                        txtNombreEmpresa.Text = empresaActual.NombreEmpresa;
                        txtRTN.Text = empresaActual.RTN;
                        txtDireccion.Text = empresaActual.Direccion;
                        txtTelefono.Text = empresaActual.Telefono;
                        txtEmail.Text = empresaActual.Email;
                        txtSitioWeb.Text = empresaActual.SitioWeb;
                        txtSlogan.Text = empresaActual.Slogan;
                        txtRepresentante.Text = empresaActual.RepresentanteLegal;

                        if (empresaActual.Logo != null && empresaActual.Logo.Length > 0)
                        {
                            using (var ms = new MemoryStream(empresaActual.Logo))
                            {
                                picLogo.Image = Image.FromStream(ms);
                                logoBytes = empresaActual.Logo;
                            }
                        }
                    }
                    else
                    {
                        empresaActual = new TEmpresa();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar información: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCargarLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Seleccionar Logo de la Empresa";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(openFileDialog.FileName);
                        picLogo.Image = img;

                        using (var ms = new MemoryStream())
                        {
                            img.Save(ms, ImageFormat.Png);
                            logoBytes = ms.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al cargar imagen: {0}", ex.Message),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnQuitarLogo_Click(object sender, EventArgs e)
        {
            picLogo.Image = null;
            logoBytes = null;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                empresaActual.NombreEmpresa = txtNombreEmpresa.Text.Trim();
                empresaActual.RTN = txtRTN.Text.Trim();
                empresaActual.Direccion = txtDireccion.Text.Trim();
                empresaActual.Telefono = txtTelefono.Text.Trim();
                empresaActual.Email = txtEmail.Text.Trim();
                empresaActual.SitioWeb = txtSitioWeb.Text.Trim();
                empresaActual.Slogan = txtSlogan.Text.Trim();
                empresaActual.RepresentanteLegal = txtRepresentante.Text.Trim();
                empresaActual.Logo = logoBytes;

                using (var nEmpresa = new NEmpresa())
                {
                    nEmpresa.GuardarInformacion(empresaActual);
                }

                MessageBox.Show("Información de la empresa guardada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarInformacionEmpresa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar: {0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
