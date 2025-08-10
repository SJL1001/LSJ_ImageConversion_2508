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
    public partial class CvtColorProp : UserControl
    {

        public CvtColorProp()
        {
            InitializeComponent();
            cvtMonoBtn.Checked = true;
        }

        public bool MonoChecked => cvtMonoBtn.Checked;
        public bool HSVChecked => cvtHSVBtn.Checked;
    }
}
