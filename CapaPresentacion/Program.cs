using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Capturar excepciones no manejadas
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new ModuloLogin.Login());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error crítico al iniciar la aplicación:\n\n{0}\n\n{1}", ex.Message, ex.StackTrace),
                    "Error de Inicio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            //Application.Run(new Medico.Main());

        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(
                string.Format("Error en la aplicación:\n\n{0}\n\n{1}", e.Exception.Message, e.Exception.StackTrace),
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(
                string.Format("Error no manejado:\n\n{0}\n\n{1}", ex != null ? ex.Message : "", ex != null ? ex.StackTrace : ""),
                "Error Crítico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
