using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConversion
{
    public partial class RotateProp : UserControl
    {
        public RotateProp()
        {
            InitializeComponent();

            numericUpDownAngle.Minimum = 0;
            numericUpDownAngle.Maximum = 360;
            numericUpDownAngle.Value = 0;

            radioClockwise.Checked = true;
        }
        public double Angle => (double)numericUpDownAngle.Value;
        public bool Clockwise => radioClockwise.Checked;
    }
}
