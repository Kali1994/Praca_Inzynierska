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
    public partial class DecryptionScreen : Form
    {
        AlgorithmWrapper.WrapperClass cppClass = new AlgorithmWrapper.WrapperClass();
        Form messageForm = new MessageForm("Preparing rules. Please Wait");
        Form messageKeys = new MessageForm("Generating keys. Please Wait");

        Bitmap bitmap;
        String imageLocation = "";

        double firstKey;
        double secondKey;

        Stopwatch timer;
        int percentage = 0;
        int numberCount;
 
        bool decryptionThreads = false;

        private static Mutex mutProgress = new Mutex();

        public DecryptionScreen()
        {
            InitializeComponent();
            CenterToScreen();
            VisualisationCheckBox.Checked = false;
            saveButton.Enabled = false;
            EncryptButton.Enabled = false;
            VisualisationCheckBox.Enabled = false;
            button1.Enabled = false;
            threadCheckBox.Enabled = false;
            threadNumericUpDown.Enabled = false;
            FirstKeyTextBox.ReadOnly = true;
            SecondKeyTextBox.ReadOnly = true;

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

        void descramblingPixels(int startX, int endX, int startY, int endY)
        {
            for (int k = 2; k >= 0; k--)
            {
                for (int i = endX - 1; i >= startX; i--)
                {
                    for (int j = endY - 1; j >= startY; j--)
                    {

                        AlgorithmWrapper.WrapperPixels pixel = cppClass.descramblingPixels(i, j, k);

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

        void threadDescramblingPixels(object data)
        {
            ThreadParameters param = (ThreadParameters)data;

            for (int k = 2; k >= 0; k--)
            {
                for (int i = param.endX - 1; i >= param.startX; i--)
                {
                    for (int j = param.endY - 1; j >= param.startY; j--)
                    {
                        AlgorithmWrapper.WrapperPixels pixel = cppClass.threadDescramblingPixels(i, j, k, param.startX, param.endX, param.startY, param.endY);

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
            percentage = 0;
            numberCount = bitmap.Width * bitmap.Height * 3;

            timer = new Stopwatch();
            timer.Start();

            if (threadCheckBox.Checked)
            {
                int numberThreads = Convert.ToInt32(Math.Round(threadNumericUpDown.Value, 0));
                int pieces = Convert.ToInt32(Math.Round(threadNumericUpDown.Maximum, 0));
                int currentThread = 0;

                Thread[] threads = new Thread[numberThreads];

                int piecesI = 0, piecesJ = 0;
                getFromPieces(ref piecesI, ref piecesJ, pieces);

                int startX = 0, startY = 0;
                int endX = 0, endY = 0;

                for (int i = 0; i < piecesI; i++)
                {
                    for (int j = 0; j < piecesJ; j++)
                    {
                        threads[currentThread] = new Thread(new ParameterizedThreadStart(this.threadDescramblingPixels));
                        getXandY(ref startX, ref startY, ref endX, ref endY, i, j, piecesI, piecesJ);
                        threads[currentThread].Start(new ThreadParameters(startX, endX, startY, endY));

                        if (currentThread == numberThreads - 1)
                        {
                            for (int k = 0; k < numberThreads; k++)
                            {
                                threads[k].Join();
                            }
                            currentThread = 0;
                        }
                        else if (i == piecesI - 1 && j == piecesJ - 1)
                        {
                            for (int x = 0; x < currentThread + 1; x++)
                            {
                                threads[x].Join();
                            }
                        }
                        else
                        {
                            currentThread++;
                        }
                    }
                }

                for (int i = 0; i <bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++ )
                    {
                        AlgorithmWrapper.WrapperPixel pixel = cppClass.getPixel(i, j);
                        Color color = Color.FromArgb(pixel.R, pixel.G, pixel.B);
                        bitmap.SetPixel(j, i, color);
                    }
                }
            }
            else
            {
                descramblingPixels(0, bitmap.Height, 0, bitmap.Width);
            }

            timer.Stop();

            if (saveButton.InvokeRequired)
            {
                saveButton.Invoke(new MethodInvoker(
                delegate () { saveButton.Enabled = true; }));
            }

            if (loadButton.InvokeRequired)
            {
                loadButton.Invoke(new MethodInvoker(
                delegate () { loadButton.Enabled = true; }));
            }

            if (backButton.InvokeRequired)
            {
                backButton.Invoke(new MethodInvoker(
                delegate () { backButton.Enabled = true; }));
            }

            if (EncryptButton.InvokeRequired)
            {
                EncryptButton.Invoke(new MethodInvoker(
                delegate () { EncryptButton.Enabled = false; }));
            }

            Image1.Image = bitmap;
            MessageBox.Show("Picture was decrypted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            percentageLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            EncryptButton.Enabled = false;
            VisualisationCheckBox.Enabled = false;
            loadButton.Enabled = false;
            backButton.Enabled = false;
            saveButton.Enabled = false;
            threadCheckBox.Enabled = false;
            threadNumericUpDown.Enabled = false;

            if (messageForm.IsDisposed)
            {
                messageForm = new MessageForm("Preparing rules. Please Wait");

            }

            messageForm.Show(this);
            backgroundWorker1.RunWorkerAsync();
        }

        private void loadButton_Click(object sender, EventArgs e)
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
                    FirstKeyTextBox.Text = "";
                    SecondKeyTextBox.Text = "";
                    FirstKeyTextBox.ReadOnly = false;
                    SecondKeyTextBox.ReadOnly = false;
                    button1.Enabled = true;
                    saveButton.Enabled = false;
                    progressBar1.Value = 0;
                    percentageLabel.Text = "0%";
                    timeLabel.Text = "00:00:00";
                    VisualisationCheckBox.Checked = false;
                    VisualisationCheckBox.Enabled = false;
                    threadCheckBox.Checked = false;
                    decryptionThreads = false;

                    string fileName = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                    int index = fileName.LastIndexOf('_');

                    if (index != -1)
                    {
                        int number;
                        if (Int32.TryParse(fileName.Substring(index + 1), out number))
                        {
                            if (25 >= number && 0 <= number)
                            {
                                threadCheckBox.Checked = true;
                                decryptionThreads = true;
                                threadNumericUpDown.Maximum = number;
                            }
                        }
                    }

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

        private void saveButton_Click_1(object sender, EventArgs e)
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

        private void VisualisationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualisationCheckBox.Checked)
            {
                if (MessageBox.Show("Visualisation will delay decryption. Are you sure you want to check this option ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    VisualisationCheckBox.Checked = true;
                }
                else
                {
                    VisualisationCheckBox.Checked = false;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!validateKey(FirstKeyTextBox.Text.ToString())){
                MessageBox.Show("Invalid Key 1", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!validateKey(SecondKeyTextBox.Text.ToString())){
                MessageBox.Show("Invalid Key 2", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button1.Enabled = false;
            EncryptButton.Enabled = true;
            FirstKeyTextBox.ReadOnly = true;
            SecondKeyTextBox.ReadOnly = true;

            if (!decryptionThreads) { 
                VisualisationCheckBox.Enabled = true;
            } else {
                threadNumericUpDown.Enabled = true;
            }

            firstKey = Convert.ToDouble(FirstKeyTextBox.Text);
            secondKey = Convert.ToDouble(SecondKeyTextBox.Text);
        }

        private bool isDigits(String str)
        {
            foreach (char c in str)
            {
                if ((c < '0' || c > '9') && (c != '.' && c != '-'))
                    return false;
            }

            return true;
        }

        private bool validateKey(String key)
        {
            if (key.StartsWith("-"))
            {
                if (key.Length < 10  || key.Length > 15 || key.ToCharArray().Count(c => c == '.') != 1 ||
                    !isDigits(key) || !validateRange(key))
                {
                    return false;
                }
            }
            else
            {
                if (key.Length < 9 || key.Length > 14 || key.ToCharArray().Count(c => c == '.') != 1 ||
                    !isDigits(key) || !validateRange(key))
                {
                    return false;
                }
            }

            return true;
        }

        private bool validateRange(String key)
        {
            double value = Convert.ToDouble(key);

            if ((1 > value) && (-1 < value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void getXandY(ref int startX, ref int startY, ref int endX, ref int endY, int i, int j, int piecesI, int piecesJ)
        {
            int fieldX = (bitmap.Height / piecesI);
            int fieldY = (bitmap.Width / piecesJ);

            int restX = (bitmap.Height % piecesI);
            int restY = (bitmap.Width % piecesJ);

            startX = fieldX * i;
            startY = fieldY * j;

            if (i == piecesI - 1 && j == piecesJ - 1)
            {
                endX = (fieldX * (i + 1)) + restX;
                endY = (fieldY * (j + 1)) + restY;
            }
            else if (i == piecesI - 1 && j != piecesJ - 1)
            {
                endX = (fieldX * (i + 1)) + restX;
                endY = (fieldY * (j + 1));
            }
            else if (i != piecesI - 1 && j == piecesJ - 1)
            {
                endX = (fieldX * (i + 1));
                endY = (fieldY * (j + 1)) + restY;
            }
            else
            {
                endX = fieldX * (i + 1);
                endY = fieldY * (j + 1);
            }
        }

        private void getFromPieces(ref int piecesI, ref int piecesJ, int pieces)
        {
            if (pieces == 2)
            {
                piecesI = 2;
                piecesJ = 1;
            }
            else if (pieces == 4)
            {
                piecesI = 2;
                piecesJ = 2;
            }
            else if (pieces == 9)
            {
                piecesI = 3;
                piecesJ = 3;
            }
            else if (pieces == 16)
            {
                piecesI = 4;
                piecesJ = 4;
            }
            else if (pieces == 25)
            {
                piecesI = 5;
                piecesJ = 5;
            }
        }
    }
}
