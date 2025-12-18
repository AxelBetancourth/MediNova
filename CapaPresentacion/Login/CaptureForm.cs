using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion.ModuloLogin
{
    delegate void Function();

    public partial class CaptureForm : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Processing.Enrollment Enroller;
        private DPFP.Capture.Capture Capturer;
        private int muestrasTomadas = 0;
        private const int totalMuestras = 4;
        private bool isClosed = false;
        private bool huellaCompletada = false;

        public event Action<byte[]> OnHuellaCapturada;

        public CaptureForm()
        {
            InitializeComponent();
        }

        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();
                Enroller = new DPFP.Processing.Enrollment();

                if (Capturer != null)
                    Capturer.EventHandler = this;
                else
                    SetPrompt("No se pudo iniciar la captura");
            }
            catch
            {
                MessageBox.Show("Error al iniciar el lector", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleToBitmap(Sample));
        }

        protected void Start()
        {
            if (Capturer != null)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Escanea tu huella usando el lector");
                }
                catch
                {
                    SetPrompt("No se puede iniciar la captura");
                }
            }
        }

        protected void Stop()
        {
            if (Capturer != null)
            {
                try { Capturer.StopCapture(); } catch { }
            }
        }

        #region Form Event Handlers

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            Init();
            Start();
        }

        private void CaptureForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
            isClosed = true;

            if (Capturer != null)
            {
                Capturer.EventHandler = null;
                Capturer = null;
            }
        }

        #endregion

        #region EventHandler Members

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            if (huellaCompletada)
                return;

            Process(Sample);
            var features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if (features != null)
            {
                Enroller.AddFeatures(features);
                muestrasTomadas++;
                SetStatus(string.Format("Progreso: {0}/{1}", muestrasTomadas, totalMuestras));

                if (muestrasTomadas >= totalMuestras ||
                    Enroller.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready)
                {
                    if (Enroller.TemplateStatus != DPFP.Processing.Enrollment.Status.Ready)
                        System.Threading.Thread.Sleep(300);

                    if (Enroller.TemplateStatus == DPFP.Processing.Enrollment.Status.Ready)
                    {
                        DPFP.Template template = Enroller.Template;
                        byte[] templateBytes;
                        using (var ms = new MemoryStream())
                        {
                            template.Serialize(ms);
                            templateBytes = ms.ToArray();
                        }

                        huellaCompletada = true;

                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            try
                            {
                                try { OnHuellaCapturada?.Invoke(templateBytes); }
                                catch (Exception exSub)
                                {
                                    MakeReport("Error en OnHuellaCapturada: " + exSub.Message);
                                }

                                Stop();
                                MakeReport("✅ Huella registrada correctamente.");
                                MessageBox.Show("Huella registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MakeReport("Error al finalizar enrolamiento: " + ex.Message);
                            }
                        }));
                    }
                    else if (Enroller.TemplateStatus == DPFP.Processing.Enrollment.Status.Failed)
                    {
                        MakeReport("⚠️ Error en el enrolamiento. Reiniciando...");
                        ReiniciarCaptura();
                    }
                }
            }
        }


        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("Dedo removido del lector");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("Huella ingresada con exito");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("El lector de huellas ha sido conectado");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("El lector de huellas ha sido desconectado");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("La calidad de la muestra es buena");
            else
                MakeReport("La calidad de la muestra es mala");
        }

        #endregion

        #region Funciones auxiliares

        private void ReiniciarCaptura()
        {
            Stop();
            Enroller = new DPFP.Processing.Enrollment();
            muestrasTomadas = 0;
            huellaCompletada = false;
            MakeReport("🔄 Listo para una nueva captura de huella.");
            Start();
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);
            return feedback == DPFP.Capture.CaptureFeedback.Good ? features : null;
        }

        protected void SetStatus(string status)
        {
            if (isClosed) return;
            if (this.IsHandleCreated && !this.IsDisposed)
                this.BeginInvoke(new MethodInvoker(() => StatusLine.Text = status));
        }

        protected void SetPrompt(string prompt)
        {
            if (isClosed) return;
            if (this.IsHandleCreated && !this.IsDisposed)
                this.BeginInvoke(new MethodInvoker(() => Prompt.Text = prompt));
        }

        protected void MakeReport(string message)
        {
            if (isClosed) return;
            if (this.IsHandleCreated && !this.IsDisposed)
                this.BeginInvoke(new MethodInvoker(() => StatusText.AppendText(message + "\r\n")));
        }

        private void DrawPicture(Bitmap bitmap)
        {
            if (isClosed) return;
            if (this.IsHandleCreated && !this.IsDisposed)
                this.BeginInvoke(new MethodInvoker(() =>
                    Picture.Image = new Bitmap(bitmap, Picture.Size)
                ));
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Stop();
            if (Capturer != null)
            {
                Capturer.EventHandler = null;
                Capturer = null;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        private void StatusLine_Click(object sender, EventArgs e)
        {

        }
    }
}
