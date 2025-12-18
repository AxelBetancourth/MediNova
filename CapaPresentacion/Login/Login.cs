using CapaDatos.BaseDatos.Tablas;
using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.Login;
using CapaNegocio.Administrador;
using DPFP;
using DPFP.Capture;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion.ModuloLogin
{
    public partial class Login : Form, DPFP.Capture.EventHandler
    {
        private readonly NLogin _nLogin;
        private DPFP.Capture.Capture Capturer;
        private bool isCapturing = false;
        private bool lectorDisponible = false;

        public Login()
        {
            try
            {
                InitializeComponent();
                _nLogin = new NLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error al inicializar el formulario Login:\n\n{0}\n\n{1}", ex.Message, ex.StackTrace),
                    "Error de Inicialización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                throw; // Re-lanzar la excepción para que se capture en Program.cs
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            TxtPassword.UseSystemPasswordChar = true;

            InicializarLectorHuella();

            guna2Panel3.Visible = false;

            if (Picture != null)
                Picture.Image = null;
            if (StatusText != null)
                StatusText.Clear();
            if (StatusLabel != null)
                StatusLabel.Text = "";

            try
            {
                _nLogin.InicializarSistema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Advertencia al inicializar sistema:\n{0}", ex.Message),
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InicializarLectorHuella()
        {
            try
            {
                //Intentar crear el objeto Capture
                Capturer = new DPFP.Capture.Capture();
                if (Capturer != null)
                {
                    Capturer.EventHandler = this;
                    lectorDisponible = true; 
                }
            }
            catch (Exception ex)
            {
                // ⚠️ Lector NO disponible - pero NO detener la aplicación
                lectorDisponible = false;
                Capturer = null;

                MessageBox.Show(
                    string.Format("El lector de huellas no está disponible.\nPuede iniciar sesión usando usuario y contraseña.\n\nDetalle técnico: {0}", ex.Message),
                    "Lector de Huellas No Disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtUsuario.Text))
                {
                    MessageBox.Show("Por favor, ingrese su nombre de usuario",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(TxtPassword.Text))
                {
                    MessageBox.Show("Por favor, ingrese su contraseña",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtPassword.Focus();
                    return;
                }

                var usuario = _nLogin.Login(TxtUsuario.Text.Trim(), TxtPassword.Text);

                if (usuario != null)
                {
                    MessageBox.Show(string.Format("¡Bienvenido {0}!", usuario.NombreUsuario),
                        "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AbrirFormularioSegunRol(usuario);
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos",
                        "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtPassword.Clear();
                    TxtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al iniciar sesión:\n{0}", ex.Message),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuella_Click(object sender, EventArgs e)
        {
            //Verificar si el lector está disponible
            if (!lectorDisponible || Capturer == null)
            {
                MessageBox.Show(
                    "El lector de huellas no está disponible en este equipo.\n" +
                    "Por favor, use usuario y contraseña para iniciar sesión.",
                    "Lector No Disponible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (!isCapturing)
            {
                try
                {
                    guna2Panel3.Visible = true;

                    if (Picture != null)
                        Picture.Image = null;
                    if (StatusText != null)
                        StatusText.Clear();

                    Capturer.StartCapture();
                    isCapturing = true;

                    SetStatus("Esperando huella...");
                    MakeReport("👆 Coloque su dedo en el lector de huellas");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al iniciar captura:\n{0}", ex.Message),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    guna2Panel3.Visible = false;
                    isCapturing = false;
                }
            }
        }

        #region Implementación de DPFP.Capture.EventHandler

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            if (!lectorDisponible) return;

            try
            {
                DrawPicture(ConvertSampleToBitmap(Sample));

                if (isCapturing && Capturer != null)
                {
                    Capturer.StopCapture();
                    isCapturing = false;
                }

                DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

                if (features != null)
                {
                    MakeReport("✅ Huella capturada correctamente");
                    SetStatus("Verificando...");

                    // Verificar contra base de datos
                    var usuario = VerificarHuellaContraBD(features);

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        if (usuario != null)
                        {
                            SetStatus(string.Format("✅ Usuario verificado: {0}", usuario.NombreUsuario));
                            MakeReport(string.Format("🎉 ¡Bienvenido {0}!", usuario.NombreUsuario));

                            System.Threading.Thread.Sleep(200);

                            MessageBox.Show(string.Format("¡Bienvenido {0}!\nInicio de sesión con huella exitoso.", usuario.NombreUsuario),
                                "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            guna2Panel3.Visible = false;

                            AbrirFormularioSegunRol(usuario);
                        }
                        else
                        {
                            SetStatus("❌ Huella no reconocida");
                            MakeReport("⚠️ Acceso denegado - Huella no encontrada en el sistema");

                            MessageBox.Show("Huella no reconocida.\nAcceso denegado.",
                                "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            guna2Panel3.Visible = false;
                        }
                    }));
                }
                else
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        SetStatus("⚠️ Calidad insuficiente");
                        MakeReport("La calidad de la huella es insuficiente. Intente nuevamente.");

                        MessageBox.Show("La calidad de la huella es insuficiente.\nIntente nuevamente.",
                            "Calidad Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        guna2Panel3.Visible = false;
                    }));
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    SetStatus("❌ Error");
                    MakeReport(string.Format("Error: {0}", ex.Message));

                    MessageBox.Show(string.Format("Error al procesar huella:\n{0}", ex.Message),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    guna2Panel3.Visible = false;
                }));
            }
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            if (!lectorDisponible) return;
            MakeReport("🔄 Dedo removido del lector");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            if (!lectorDisponible) return;
            MakeReport("👆 Huella detectada - Procesando...");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            if (!lectorDisponible) return;

            this.BeginInvoke(new MethodInvoker(() =>
            {
                MakeReport("✅ Lector de huellas conectado");
            }));
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            if (!lectorDisponible) return;

            this.BeginInvoke(new MethodInvoker(() =>
            {
                MakeReport("❌ Lector de huellas desconectado");
                SetStatus("Lector desconectado");
                MessageBox.Show("Lector de huellas desconectado",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2Panel3.Visible = false;
            }));
            isCapturing = false;
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (!lectorDisponible) return;

            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("✅ La calidad de la muestra es buena");
            else
                MakeReport("⚠️ La calidad de la muestra es mala");
        }

        #endregion

        #region Métodos Auxiliares

        private DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            if (!lectorDisponible) return null;

            try
            {
                DPFP.Processing.FeatureExtraction extractor = new DPFP.Processing.FeatureExtraction();
                DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
                DPFP.FeatureSet features = new DPFP.FeatureSet();
                extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);

                if (feedback == DPFP.Capture.CaptureFeedback.Good)
                    return features;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        private TUsuario VerificarHuellaContraBD(DPFP.FeatureSet features)
        {
            if (!lectorDisponible) return null;

            try
            {
                // Obtener todos los usuarios activos
                var usuarios = _nLogin.ListadoUsuarios();

                if (usuarios == null || usuarios.Count == 0)
                    return null;

                // Verificador de huellas
                DPFP.Verification.Verification verificador = new DPFP.Verification.Verification();

                foreach (var usuario in usuarios)
                {
                    if (usuario.HuellaDactilar != null && usuario.HuellaDactilar.Length > 0 &&
                        !usuario.Eliminado && usuario.Estado)
                    {
                        try
                        {
                            // Deserializar template del usuario
                            DPFP.Template template;
                            using (var ms = new MemoryStream(usuario.HuellaDactilar))
                            {
                                template = new DPFP.Template(ms);
                            }

                            // Verificar coincidencia
                            DPFP.Verification.Verification.Result resultado = new DPFP.Verification.Verification.Result();
                            verificador.Verify(features, template, ref resultado);

                            if (resultado.Verified)
                            {
                                // Actualizar última sesión
                                usuario.UltimoAcceso = DateTime.Now;
                                _nLogin.ActualizarUltimoAcceso(usuario.UsuarioId);
                                return usuario;
                            }
                        }
                        catch
                        {
                            // Continuar con el siguiente usuario
                            continue;
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private void AbrirFormularioSegunRol(TUsuario usuario)
        {
            Form formularioPrincipal = null;

            string rolNombre = usuario.Rol != null && usuario.Rol.Nombre != null ? usuario.Rol.Nombre.Trim() : null;

            //Obtener doctor si el usuario es médico
            TDoctor doctor = null;
            if (rolNombre == "Medico")
            {
                doctor = _nLogin.ObtenerDoctorDelUsuario(usuario.UsuarioId);

                // 🔹 Validación TEMPRANA para médicos sin doctor asociado
                if (doctor == null)
                {
                    MessageBox.Show(
                        "Su usuario no tiene un doctor asociado en el sistema.\n\n" +
                        "Por favor, contacte al administrador para que le asocie un perfil de doctor.\n\n" +
                        "No podrá acceder al módulo de médicos hasta que esto se resuelva.",
                        "Doctor No Asociado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    // Limpiar campos de login
                    TxtUsuario.Clear();
                    TxtPassword.Clear();
                    TxtUsuario.Focus();

                    // NO cerrar ni ocultar el login - quedarse aquí
                    return;
                }
            }

            //Guardar sesión
            SesionUsuario.IniciarSesion(usuario, doctor);

            switch (rolNombre)
            {
                case "Administrador":
                    formularioPrincipal = new CapaPresentacion.Administrador.Main();
                    break;

                case "Medico":
                    formularioPrincipal = new CapaPresentacion.Medico.Main();
                    break;

                case "Recepcionista":
                    formularioPrincipal = new CapaPresentacion.Recepcionista.Main();
                    break;

                case "Farmaceutico":
                    formularioPrincipal = new CapaPresentacion.Farmacia.Main();
                    break;

                default:
                    MessageBox.Show(string.Format("Rol '{0}' no reconocido.\nContacte al administrador.", usuario.Rol != null ? usuario.Rol.Nombre : ""),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            // Configurar y mostrar formulario
            if (formularioPrincipal != null)
            {
                try
                {
                    if (this.MdiParent != null)
                        formularioPrincipal.MdiParent = this.MdiParent;

                    formularioPrincipal.Show();

                    // Solo ocultar el login si el formulario se mostró correctamente
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        string.Format("Error al abrir el módulo:\n{0}", ex.Message),
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    // Si hubo error, limpiar la sesión
                    SesionUsuario.CerrarSesion();

                    // Cerrar el formulario que falló si se creó
                    if (formularioPrincipal != null && !formularioPrincipal.IsDisposed)
                    {
                        formularioPrincipal.Close();
                    }
                }
            }
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            if (!lectorDisponible) return null;

            try
            {
                DPFP.Capture.SampleConversion convertor = new DPFP.Capture.SampleConversion();
                Bitmap bitmap = null;
                convertor.ConvertToPicture(Sample, ref bitmap);
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        protected void SetStatus(string status)
        {
            if (this.IsHandleCreated && !this.IsDisposed && StatusLabel != null)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    StatusLabel.Text = status;
                }));
            }
        }

        protected void MakeReport(string message)
        {
            if (this.IsHandleCreated && !this.IsDisposed && StatusText != null)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    StatusText.AppendText(message + "\r\n");
                }));
            }
        }

        private void DrawPicture(Bitmap bitmap)
        {
            if (this.IsHandleCreated && !this.IsDisposed && Picture != null && bitmap != null)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    Picture.Image = new Bitmap(bitmap, Picture.Size);
                }));
            }
        }

        #endregion

        #region Cleanup

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //Solo intentar detener si el lector está disponible
            if (lectorDisponible && Capturer != null)
            {
                try
                {
                    if (isCapturing)
                    {
                        Capturer.StopCapture();
                    }

                    Capturer.EventHandler = null;

                    try
                    {
                        Capturer.Dispose();
                    }
                    catch
                    {
                    }
                }
                catch
                {
                }
                finally
                {
                    Capturer = null;
                    isCapturing = false;
                }
            }
        }

        #endregion

        #region Eventos de Controles

        private void TxtUsuario_TextChanged(object sender, EventArgs e) { }
        private void TxtPassword_TextChanged(object sender, EventArgs e) { }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e) { }
        private void guna2Panel3_Paint(object sender, PaintEventArgs e) { }
        private void Picture_Click(object sender, EventArgs e) { }
        private void StatusText_TextChanged(object sender, EventArgs e) { }
        private void StatusLabel_Click(object sender, EventArgs e) { }
        private void guna2PictureBox1_Click(object sender, EventArgs e) { }

        #endregion

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}