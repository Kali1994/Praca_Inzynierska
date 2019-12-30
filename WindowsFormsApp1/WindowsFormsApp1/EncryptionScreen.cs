using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            // This event will be raised when we call ReportProgress
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap bitmap = new Bitmap(imageLocation);

            int width = bitmap.Width;
            int heigt = bitmap.Height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigt; j++)
                {
                    Color color = Color.FromArgb(255, 0, 0);

                    if (Image1.InvokeRequired)
                    {
                        Image1.Invoke(new MethodInvoker(
                        delegate ()
                        {
                            bitmap.SetPixel(i, j, color);
                            Image1.Image = bitmap;
                        }));
                    }
                    backgroundWorker1.ReportProgress(i);
                }
            }
        }

        // Back on the 'UI' thread so we can update the progress bar
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            //progressBar1.Value = e.ProgressPercentage;
            //label2.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = cppClass.getTestValue().ToString();
            button1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cppClass.setTestValue(7);
            label2.Text = cppClass.getTestValue().ToString();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.jpg;)|*.jpg;";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageLocation = dialog.FileName;
                Image1.ImageLocation = imageLocation;
                label1.Text = imageLocation;
                byte[] bytes = Encoding.ASCII.GetBytes(imageLocation);

                unsafe
                {
                    fixed (byte* p = bytes)
                    {
                        sbyte* sp = (sbyte*)p;
                        cppClass.loadImage(sp);
                    }
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
    }
}
