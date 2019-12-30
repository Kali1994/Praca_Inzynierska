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
        String imageLocation = "";

        public EncryptionScreen()
        {
            InitializeComponent();
            CenterToScreen();
            VisualisationCheckBox.Checked = true;
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
            Bitmap bitmap = new Bitmap(imageLocation);

            int width = bitmap.Width;
            int height = bitmap.Height;

            int percentage = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color first = Color.FromArgb(255, 0, 0);
                    Color second;
                    Color third;
                    
                    if (Image1.InvokeRequired)
                    {
                        Image1.Invoke(new MethodInvoker(
                        delegate ()
                        {
                            //bitmap.SetPixel(i, j, first);
                            Image1.Image = bitmap;
                        }));
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
                    backgroundWorker1.ReportProgress((percentage * 100) /(width * height));
                }
            }

            timer.Stop();

            if (saveButton.InvokeRequired)
            {
                saveButton.Invoke(new MethodInvoker(
                delegate ()
                {
                    saveButton.Enabled = true;
                }));
            }
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            percentageLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EncryptButton.Enabled = false;
            loadButton.Enabled = false;
            backButton.Enabled = false;
            saveButton.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.jpg;)|*.jpg;";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageLocation = dialog.FileName;
                Image1.ImageLocation = imageLocation;
                LoadPathTextBox.Text = imageLocation;
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
            dialog.Filter = "JPG(*.JPG)|*.jpg";

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
                Image1.ImageLocation = imageLocation;
            }
            else
            {
                Image1.Image = Properties.Resources.No_Image;
            }
        }

        private void GenerationButton_Click(object sender, EventArgs e)
        {
            GenerationButton.Enabled = false;
            EncryptButton.Enabled = true;
            VisualisationCheckBox.Enabled = true;

            FirstKeyTextBox.Text = "1232312";
            SecondKeyTextBox.Text = "546454";
        }
    }
}
