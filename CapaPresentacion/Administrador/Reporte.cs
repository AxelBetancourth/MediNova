using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Administrador
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
            AjustarDiseñoTextos();
        }

        private void AjustarDiseñoTextos()
        {
            lblTitulo.TextAlignment = ContentAlignment.MiddleLeft;
            lblIcono.TextAlignment = ContentAlignment.MiddleCenter;
            lblDescripcion.TextAlignment = ContentAlignment.MiddleCenter;
        }

        private void BtnAbrirPowerBI_Click(object sender, EventArgs e)
        {
            try
            {
                // Ruta del archivo Power BI en la carpeta del proyecto
                // Desde bin/Debug subir dos niveles (..\..\ ) para llegar a CapaPresentacion
                string rutaBase = Path.GetDirectoryName(Application.ExecutablePath);
                string rutaPowerBI = Path.Combine(rutaBase, "..", "..", "Reportes", "ReporteProyecto.pbix");

                // Normalizar la ruta
                rutaPowerBI = Path.GetFullPath(rutaPowerBI);

                // Verificar si el archivo existe
                if (!File.Exists(rutaPowerBI))
                {
                    MessageBox.Show(
                        string.Format("No se encontró el archivo de Power BI en la ruta:\n{0}\n\nAsegúrese de que el archivo ReporteProyecto.pbix existe en la carpeta:\nCapaPresentacion\\Reportes\\", rutaPowerBI),
                        "Archivo no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Abrir el archivo con la aplicación predeterminada (Power BI Desktop)
                // Esto no bloquea la aplicación, se abre en un proceso separado
                Process.Start(rutaPowerBI);

                MessageBox.Show(
                    "Se ha abierto el archivo de Power BI Desktop.\n\nSi Power BI Desktop no está instalado, por favor instálelo desde:\nhttps://powerbi.microsoft.com/desktop/",
                    "Dashboard Power BI",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error al abrir el archivo de Power BI:\n{0}", ex.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelContenido_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
