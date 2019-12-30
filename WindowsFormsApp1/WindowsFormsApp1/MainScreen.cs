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
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void encryptionButton_Click(object sender, EventArgs e)
        {
            EncryptionScreen screen = new EncryptionScreen();
            screen.Show();
            Hide();
        }

        private void decryptionButton_Click(object sender, EventArgs e)
        {
            DecryptionScreen screen = new DecryptionScreen();
            screen.Show();
            Hide();
        }
    }
}
