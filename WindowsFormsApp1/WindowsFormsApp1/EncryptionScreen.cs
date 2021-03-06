﻿using System;
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
        SpinBox spinBox;
        Bitmap bitmap;
        String imageLocation = "";

        double firstKey;
        double secondKey;

        Stopwatch timer;
        System.Threading.Timer trhreadTimer;
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
            spinBox = new SpinBox(ref threadNumericUpDown);
            spinBox.Bounds = new Rectangle(272, 425, 90, 20);
            this.Controls.Add(spinBox);
            spinBox.Enabled = false;
            spinBox.setValue(2);
            NumberThreadLabel.Enabled = false;
            numberPiecesLabel.Enabled = false;

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
                    }

                    Progress();

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
                    }
                    mutProgress.WaitOne();
                    Progress();
                    mutProgress.ReleaseMutex();
                }
            }
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (threadCheckBox.Checked)
            {
                cppClass.threadPreparingRules(firstKey, secondKey);
            }
            else
            {
                cppClass.preparingRules(firstKey, secondKey);
            }

            if (messageForm.InvokeRequired) {
                messageForm.Invoke(new MethodInvoker(
                delegate () { messageForm.Close(); }));
            }

            bitmap = new Bitmap(imageLocation);
            percentage = 0;
            numberCount = bitmap.Height * 3;

            if (threadCheckBox.Checked)
            {
                int numberThreads = Convert.ToInt32(Math.Round(threadNumericUpDown.Value, 0));
                int pieces = Convert.ToInt32(Math.Round(spinBox.Value, 0));
                int currentThread = 0;

                Thread[] threads = new Thread[numberThreads];

                int piecesI = 0, piecesJ = 0;
                getFromPieces(ref piecesI, ref piecesJ, pieces);
                numberCount *= piecesJ; 

                int startX = 0, startY = 0;
                int endX = 0, endY = 0;

                for (int i = 0; i < piecesI; i++)
                {
                    for (int j = 0; j < piecesJ; j++)
                    {
                        threads[currentThread] = new Thread(new ParameterizedThreadStart(this.threadScramblingPixels));
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

                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        AlgorithmWrapper.WrapperPixel pixel = cppClass.getPixel(i, j);
                        Color color = Color.FromArgb(pixel.R, pixel.G, pixel.B);
                        bitmap.SetPixel(j, i, color);
                    }
                }
            }
            else
            {
                ScramblingPixels(0, bitmap.Height, 0, bitmap.Width);
            }
               
            if (trhreadTimer != null)
            {
                trhreadTimer.Dispose();
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
            spinBox.Enabled = false;
            NumberThreadLabel.Enabled = false;
            numberPiecesLabel.Enabled = false;

            if (messageForm.IsDisposed)
            {
                messageForm = new MessageForm("Preparing rules.  Please Wait");

            }

            timer = new Stopwatch();
            timer.Start();
            trhreadTimer = new System.Threading.Timer(UpdateTimeView, null, 0, 100);
            messageForm.Show(this);

            backgroundWorker1.RunWorkerAsync();
        }

        private void UpdateTimeView(object state)
        {
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
                    Bitmap tempBitmap = new Bitmap(imageLocation);
                    spinBox.setImageSize(tempBitmap.Height, tempBitmap.Width);
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
                byte[] bytes;
                if (threadCheckBox.Checked)
                {
                    bytes = Encoding.ASCII.GetBytes(Path.GetDirectoryName(dialog.FileName) + "/" + System.IO.Path.GetFileNameWithoutExtension(dialog.FileName) + "_" + spinBox.Value.ToString() + ".png");
                }
                else
                {
                    bytes = Encoding.ASCII.GetBytes(dialog.FileName);
                }

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
                if (MessageBox.Show("Visualisation will delay encryption and canot be used with threading type. Are you sure you want to check this option ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    VisualisationCheckBox.Checked = true;
                    threadCheckBox.Checked = false;
                    threadCheckBox.Enabled = false;
                    threadNumericUpDown.Value = 1;
                    threadNumericUpDown.Enabled = false;
                }
                else
                {
                    VisualisationCheckBox.Checked = false;
                    threadCheckBox.Enabled = true;
                }
            }
            else
            {
                threadCheckBox.Enabled = true;
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
                if (20 <= picture.Height && 20 <= picture.Width)
                {
                    threadCheckBox.Checked = true;
                    threadNumericUpDown.Enabled = true;
                    spinBox.Enabled = true;
                    NumberThreadLabel.Enabled = true;
                    numberPiecesLabel.Enabled = true;
                    VisualisationCheckBox.Checked = false;
                    VisualisationCheckBox.Enabled = false;
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
                spinBox.Enabled = false;
                NumberThreadLabel.Enabled = false;
                numberPiecesLabel.Enabled = false;
                spinBox.setValue(2);
                VisualisationCheckBox.Enabled = true;
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

        private void threadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (threadNumericUpDown.Value > spinBox.Value)
            {
                MessageBox.Show("Number Threads can't be higher than pieces", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                threadNumericUpDown.Value = threadNumericUpDown.Value - 1;
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
