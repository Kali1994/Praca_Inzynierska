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
    public partial class SpinBox : NumericUpDown
    {
        public SpinBox(ref NumericUpDown numberThreads)
        {
            threads = numberThreads;
            InitializeComponent();
        }

        private int currentIndex = 0;
        private int heightImage = 0;
        private int widthImage = 0;
        private NumericUpDown threads;
        private decimal[] possibleValues = null;
        public decimal[] PossibleValues
        {
            get
            {
                if (possibleValues == null)
                {
                    possibleValues = GetPossibleValues().ToArray();
                }
                return possibleValues;
            }
        }

        public override void UpButton()
        {
            if (base.UserEdit)
            {
                this.ParseEditText();
            }

            var values = PossibleValues;
            int index = Math.Min(this.currentIndex + 1, values.Length - 1);
            int spinBoxValue = Convert.ToInt32(Math.Round(values[index], 0));

            if (10 > (heightImage / spinBoxValue) || 10 > (widthImage / spinBoxValue))
            {
                MessageBox.Show("Picture is too small to divide on that number of pieces", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.currentIndex = index;
                this.Value = values[this.currentIndex];
            }
        }

        public override void DownButton()
        {
            if (base.UserEdit)
            {
                this.ParseEditText();
            }
            var values = PossibleValues;
            this.currentIndex = Math.Max(this.currentIndex - 1, 0);
            this.Value = values[this.currentIndex];

            if (threads.Value > this.Value)
            {
                threads.Value = this.Value;
            }
        }

        private IEnumerable<decimal> GetPossibleValues()
        {
            foreach (var value in new decimal[] { 2, 4, 9, 16 })
            {
                yield return value;
            }
        }

        public void setValue(int value)
        {
            if (value == 2)
            {
                Value = value;
                currentIndex = 0;
            }
            else if (value == 4)
            {
                Value = value;
                currentIndex = 1;
            }
            else if (value == 9)
            {
                Value = value;
                currentIndex = 2;
            }
            else if (value == 16)
            {
                Value = value;
                currentIndex = 3;
            }
        }

        public void setImageSize(int height, int width)
        {
            heightImage = height;
            widthImage = width;
        }
    }
}
