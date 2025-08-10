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
    public partial class EdgeProp : UserControl
    {
        public EdgeProp()
        {
            InitializeComponent();

            comboMethod.Items.AddRange(new[] { "Canny", "Sobel", "Laplacian" });
            comboMethod.SelectedIndex = 0;
            // 기본값
            rangeSliderEdge.Minimum = 0;
            rangeSliderEdge.Maximum = 255;
            rangeSliderEdge.SliderMin = 40;
            rangeSliderEdge.SliderMax = 200;
          
            numAperture.Value = 3;

            numSobelKsize.Value = 3;
            numSobelDx.Value = 1;
            numSobelDy.Value = 0;

            numLapKsize.Value = 3;

            comboMethod.SelectedIndexChanged += (_, __) => UpdateVisibility();
            UpdateVisibility();
        }

        // UI에서 읽어갈 속성들
        public string Method => comboMethod.SelectedItem?.ToString() ?? "Canny";

        // Canny
        public double CannyThreshold1 => (double)rangeSliderEdge.SliderMin;
        public double CannyThreshold2 => (double)rangeSliderEdge.SliderMax;
        public int CannyApertureSize => (int)numAperture.Value; // 3,5,7…

        // Sobel
        public int SobelDx => (int)numSobelDx.Value;
        public int SobelDy => (int)numSobelDy.Value;
        public int SobelKsize => (int)numSobelKsize.Value;       // 1,3,5,7

        // Laplacian
        public int LaplacianKsize => (int)numLapKsize.Value;     // 1,3,5,7

        private void UpdateVisibility()
        {
            var m = Method;
            panelCanny.Visible = (m == "Canny");
            panelSobel.Visible = (m == "Sobel");
            panelLaplacian.Visible = (m == "Laplacian");
        }

        public event EventHandler ValueChanged;
        private void rangeSliderEdge_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void lbThresholdMin_Click(object sender, EventArgs e)
        {

        }

        private void layoutRoot_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
