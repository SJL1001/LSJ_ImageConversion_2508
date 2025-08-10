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
    public partial class BlurProp : UserControl
    {
        public string BlurType => blurList.SelectedItem?.ToString() ?? "Gaussian";
        public int KernelSize => (int)kernalSize.Value;
        public double SigmaColor => (double)sigmaColor.Value;
        public double SigmaSpace => (double)sigmaSpace.Value;
        public event EventHandler ValueChanged;

        public BlurProp()
        {
            InitializeComponent();
            blurList.SelectedIndexChanged += BlurList_SelectedIndexChanged;
            kernalSize.ValueChanged += AnyValueChanged;
            sigmaColor.ValueChanged += AnyValueChanged;
            sigmaSpace.ValueChanged += AnyValueChanged;

           
            if (blurList.Items.Count > 0)
                blurList.SelectedIndex = 0;

            ShowBilateralOptions(false);
        }
        private void BlurList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isBilateral = BlurType == "Bilateral";
            ShowBilateralOptions(isBilateral);

          }

        private void AnyValueChanged(object sender, EventArgs e)
        {

            if (KernelSize % 2 == 0)
                kernalSize.Value = KernelSize + 1;

            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ShowBilateralOptions(bool visible)
        {
            labelSigmaColor.Visible = visible;
            labelSigmaSpace.Visible = visible;
            sigmaColor.Visible = visible;
            sigmaSpace.Visible = visible;
            label3.Visible = visible;
            label4.Visible = visible;
        }
    }
}
