using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class EncryptionScreen : Form
    {
        AlgorithmWrapper.WrapperClass cppClass = new AlgorithmWrapper.WrapperClass();
        Form messageForm = new MessageForm();
        String imageLocation = "";
        double firstKey;
        double secondKey;

        public EncryptionScreen()
        {
            InitializeComponent();
            CenterToScreen();
            VisualisationCheckBox.Checked = false;
            GenerationButton.Enabled = false;
            saveButton.Enabled = false;
            EncryptButton.Enabled = false;
            VisualisationCheckBox.Enabled = false;

            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
           // backgroundWorker1.DoWork -= new DoWorkEventHandler(backgroundWorker1_DoWork);
            // This event will be raised when we call ReportProgress
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            cppClass.preparingRules(firstKey, secondKey);

            if (messageForm.InvokeRequired){
                messageForm.Invoke(new MethodInvoker(
                delegate () { messageForm.Close(); }));
            }

            Bitmap bitmap = new Bitmap(imageLocation);

            int height = bitmap.Height;
            int width = bitmap.Width;

            int percentage = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int k = 0; k < 3; k++)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {

                        AlgorithmWrapper.WrapperPixels pixel= cppClass.scramblingPixels(i, j, k);

                        Color first = Color.FromArgb(pixel.fR, pixel.fG, pixel.fB);
                        Color second = Color.FromArgb(pixel.sR, pixel.sG, pixel.sB); ;
                        Color third = Color.FromArgb(pixel.tR, pixel.tG, pixel.tB); ;

                        if (VisualisationCheckBox.Checked)
                        {
                            if (Image1.InvokeRequired)
                            {
                                Image1.Invoke(new MethodInvoker(
                                delegate ()
                                {
                                    bitmap.SetPixel(pixel.fposY, pixel.fposX, first);
                                    bitmap.SetPixel(pixel.sposY, pixel.sposX, second);
                                    bitmap.SetPixel(pixel.tposY, pixel.tposX, third);
                                }));
                            }
                        }
                        else
                        {
                            bitmap.SetPixel(pixel.fposY, pixel.fposX, first);
                            bitmap.SetPixel(pixel.sposY, pixel.sposX, second);
                            bitmap.SetPixel(pixel.tposY, pixel.tposX, third);
                        }

                        if (timeLabel.InvokeRequired)
                        {
                            timeLabel.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                TimeSpan ts = timer.Elapsed;
                                timeLabel.Text = String.Format("{0:00}:{1:00}:{2:00}",
                                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                            }));
                        }

                        percentage++;
                        backgroundWorker1.ReportProgress((percentage * 100) / (width * height * 3));
                    }

                    if (VisualisationCheckBox.Checked)
                    {
                        if (Image1.InvokeRequired)
                        {
                            Image1.Invoke(new MethodInvoker(
                            delegate () { Image1.Image = bitmap; }));
                        }
                    }
                }
            }

            timer.Stop();

            if (saveButton.InvokeRequired){
                saveButton.Invoke(new MethodInvoker(
                delegate (){ saveButton.Enabled = true; }));
            }

            if (loadButton.InvokeRequired)
            {
                loadButton.Invoke(new MethodInvoker(
                delegate () { loadButton.Enabled = true; }));
            }

            if (backButton.InvokeRequired){
                backButton.Invoke(new MethodInvoker(
                delegate (){ backButton.Enabled = true; }));
            }

            if (EncryptButton.InvokeRequired){
                EncryptButton.Invoke(new MethodInvoker(
                delegate () { EncryptButton.Enabled = false; }));
            }

            if (GenerationButton.InvokeRequired){
                GenerationButton.Invoke(new MethodInvoker(
                delegate () { GenerationButton.Enabled = false; }));
            }

            Image1.Image = bitmap;
            MessageBox.Show("Picture was encrypted successfully!", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            percentageLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EncryptButton.Enabled = false;
            VisualisationCheckBox.Enabled = false;
            loadButton.Enabled = false;
            backButton.Enabled = false;
            saveButton.Enabled = false;
            messageForm.Show(this);

            backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageLocation = dialog.FileName;
                Image1.ImageLocation = imageLocation;
                LoadPathTextBox.Text = imageLocation;
                GenerationButton.Enabled = true;
                FirstKeyTextBox.Text = "";
                SecondKeyTextBox.Text = "";
                saveButton.Enabled = false;
                progressBar1.Value = 0;
                percentageLabel.Text = "0%";
                timeLabel.Text = "00:00:00";

                byte[] bytes = Encoding.ASCII.GetBytes(imageLocation);

                unsafe
                {
                    fixed (byte* p = bytes)
                    {
                        sbyte* sp = (sbyte*)p;
                        cppClass.loadImage(sp);
                    }
                }

                GenerationButton.Enabled = true;
            }

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainScreen screen = new MainScreen();
            screen.Show();
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PNG(*.PNG)|*.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(dialog.FileName);

                unsafe
                {
                    fixed (byte* p = bytes)
                    {
                        sbyte* sp = (sbyte*)p;
                        cppClass.saveImage(sp);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualisationCheckBox.Checked)
            {
                if (MessageBox.Show("Visualisation will delay encryption. Are you sure you want to check this option ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    VisualisationCheckBox.Checked = true;
                }
                else
                {
                    VisualisationCheckBox.Checked = false;
                }
            }
        }

        private void GenerationButton_Click(object sender, EventArgs e)
        {
            GenerationButton.Enabled = false;
            EncryptButton.Enabled = true;
            VisualisationCheckBox.Enabled = true;

            unsafe
            {
                double firstKeyTmp;
                double secondKeyTmp;

                double* firstKeyPtr = &firstKeyTmp;
                double* secondKeyPtr = &secondKeyTmp;

                cppClass.generateKeys(firstKeyPtr, secondKeyPtr);

                firstKey = firstKeyTmp;
                secondKey = secondKeyTmp;

                FirstKeyTextBox.Text = firstKey.ToString();
                SecondKeyTextBox.Text = secondKey.ToString();
            }
        }
    }
}
