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
    public partial class ResizeProp : UserControl
    {
        public ResizeProp()
        {
            InitializeComponent();
        }

        public double ScaleX => (double)numScaleX.Value;
        public double ScaleY => (double)numScaleY.Value;
        private void ResizeProp_Load(object sender, EventArgs e)
        {

        }
    }
}
