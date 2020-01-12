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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class EncryptionScreen : Form
    {
        AlgorithmWrapper.WrapperClass cppClass = new AlgorithmWrapper.WrapperClass();
        Form messageForm = new MessageForm("Preparing rules.  Please Wait");
        Form messageKeys = new MessageForm("Generating keys. Please Wait");

        Bitmap bitmap;
        String imageLocation = "";

        double firstKey;
        double secondKey;

        Stopwatch timer;
        int percentage = 0;
        int numberCount;

        private static Mutex mutProgress = new Mutex();

        public EncryptionScreen()
        {
            InitializeComponent();
            CenterToScreen();
            VisualisationCheckBox.Checked = false;
            GenerationButton.Enabled = false;
            saveKeysButton.Enabled = false;
            saveButton.Enabled = false;
            EncryptButton.Enabled = false;
            VisualisationCheckBox.Enabled = false;
            threadCheckBox.Enabled = false;
            threadNumericUpDown.Enabled = false;

            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
           // backgroundWorker1.DoWork -= new DoWorkEventHandler(backgroundWorker1_DoWork);
            // This event will be raised when we call ReportProgress
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        void Progress()
        {
            percentage++;
            backgroundWorker1.ReportProgress((percentage * 100) / numberCount);
        }

        void ScramblingPixels(int startX, int endX, int startY, int endY)
        {
            for (int k = 0; k < 3; k++)
            {
                for (int i = startX; i < endX; i++)
                {
                    for (int j = startY; j < endY; j++)
                    {

                        AlgorithmWrapper.WrapperPixels pixel = cppClass.scramblingPixels(i, j, k);

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
                        Progress();
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
        }

        void threadScramblingPixels(object data)
        {
            ThreadParameters param = (ThreadParameters)data;

            for (int k = 0; k < 3; k++)
            {
                for (int i = param.startX; i < param.endX; i++)
                {
                    for (int j = param.startY; j < param.endY; j++)
                    {

                        AlgorithmWrapper.WrapperPixels pixel = cppClass.threadScramblingPixels(i, j, k, param.startX, param.endX, param.startY, param.endY);

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

                        mutProgress.WaitOne();
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

                        Progress();
                        mutProgress.ReleaseMutex();
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
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            cppClass.preparingRules(firstKey, secondKey);

            if (messageForm.InvokeRequired) {
                messageForm.Invoke(new MethodInvoker(
                delegate () { messageForm.Close(); }));
            }

            bitmap = new Bitmap(imageLocation);
            numberCount = bitmap.Width * bitmap.Height * 3;

            timer = new Stopwatch();
            timer.Start();

            if (threadCheckBox.Checked)
            {
                int fieldX = (bitmap.Height / 3);
                int fieldY = (bitmap.Width / 3);

                Thread[] threads = new Thread[9];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        threads[(i * 3) + j] = new Thread(new ParameterizedThreadStart(this.threadScramblingPixels));
                        threads[(i * 3) + j].Start(new ThreadParameters(fieldX * i, fieldX * (i + 1), fieldY * j, fieldY * (j + 1)));
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    threads[i].Join();
                }
            }
            else
            {
                ScramblingPixels(0, bitmap.Height, 0, bitmap.Width);
            }
               
            timer.Stop();

            if (saveButton.InvokeRequired){
                saveButton.Invoke(new MethodInvoker(
                delegate (){ saveButton.Enabled = true; }));
            }

            if (saveKeysButton.InvokeRequired)
            {
                saveKeysButton.Invoke(new MethodInvoker(
                delegate () { saveKeysButton.Enabled = true; }));
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
            saveKeysButton.Enabled = false;
            threadCheckBox.Enabled = false;
            threadNumericUpDown.Enabled = false;

            if (messageForm.IsDisposed)
            {
                messageForm = new MessageForm("Preparing rules.  Please Wait");

            }
            messageForm.Show(this);

            backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool result;
                byte[] bytes = Encoding.ASCII.GetBytes(dialog.FileName);

                unsafe
                {
                    fixed (byte* p = bytes)
                    {
                        sbyte* sp = (sbyte*)p;
                        result = cppClass.loadImage(sp);
                    }
                }

                if (result)
                {
                    imageLocation = dialog.FileName;
                    Image1.ImageLocation = imageLocation;
                    LoadPathTextBox.Text = imageLocation;
                    GenerationButton.Enabled = true;
                    FirstKeyTextBox.Text = "";
                    SecondKeyTextBox.Text = "";
                    saveButton.Enabled = false;
                    saveKeysButton.Enabled = false;
                    progressBar1.Value = 0;
                    percentageLabel.Text = "0%";
                    timeLabel.Text = "00:00:00";
                    GenerationButton.Enabled = true;
                    VisualisationCheckBox.Checked = false;
                    VisualisationCheckBox.Enabled = false;
                    threadCheckBox.Checked = false;
                    threadCheckBox.Enabled = false;
                    threadNumericUpDown.Value = 1;
                    threadNumericUpDown.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Invalid Picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            saveKeysButton.Enabled = true;
            threadCheckBox.Enabled = true;

            if (messageKeys.IsDisposed)
            {
                messageKeys = new MessageForm("Generating keys. Please Wait");
            }
            messageKeys.Show(this);

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

            messageKeys.Close();
        }

        private void saveKeysButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "TXT(*.TXT)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.Create(dialog.FileName).Dispose();
                TextWriter tw = new StreamWriter(dialog.FileName);
                tw.WriteLine(FirstKeyTextBox.Text);
                tw.WriteLine(SecondKeyTextBox.Text);
                tw.Close();
            }
        }

        private void threadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Bitmap picture = new Bitmap(imageLocation);
            if (threadCheckBox.Checked)
            {
                if (50 < picture.Height && 50 < picture.Width)
                {
                    threadCheckBox.Checked = true;
                    threadNumericUpDown.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Picture is too small for this option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    threadCheckBox.Checked = false;
                }
            }
            else
            {
                threadNumericUpDown.Value = 1;
                threadNumericUpDown.Enabled = false;
            }
        }
    }

    public class ThreadParameters
    {
        public int startX;
        public int endX;
        public int startY;
        public int endY;

        public ThreadParameters(int StartX, int EndX, int StartY, int EndY)
        {
            startX = StartX;
            endX = EndX;
            startY = StartY;
            endY = EndY;
        }
    }
}
