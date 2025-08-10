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
    public partial class PyramidProp : UserControl
    {
        public bool PyrUp => pyramidUpButton.Checked;
        public bool PyrDown => pyramidDownButton.Checked;

        public PyramidProp()
        {
            InitializeComponent();
        }
    }
}
