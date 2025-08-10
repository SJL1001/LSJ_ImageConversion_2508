using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ImageConversion
{
    public partial class MorphologyProp : UserControl
    {
        public MorphologyProp()
        {
            InitializeComponent();

            cbOperation.Items.AddRange(new[] { "Erode", "Dilate", "Open", "Close" });
            cbShape.Items.AddRange(new[] { "Rect", "Ellipse", "Cross" });
            cbOperation.SelectedIndex = 0;
            cbShape.SelectedIndex = 0;

            numKernel.Value = 3;      // 1,3,5,7 권장
            numIter.Value = 1;
        }

        public string Operation => cbOperation.SelectedItem?.ToString() ?? "Erode";
        public string Shape => cbShape.SelectedItem?.ToString() ?? "Rect";
        public int KernelSize => (int)numKernel.Value;  // 홀수 권장
        public int Iterations => (int)numIter.Value;

        private void cbShape_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
