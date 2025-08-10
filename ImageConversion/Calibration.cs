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
    public partial class Calibration : Form
    {
        private MainForm _mainForm;
        private double _pixelDist = 0;

        public Calibration(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;


            btnStartCalib.Click += btnStartCalib_Click;
            btnApplyCalib.Click += btnApplyCalib_Click;
        }

        private void btnStartCalib_Click(object sender, EventArgs e)
        {
            lblCalibGuide.Text = "이미지에서 두 점을 클릭하세요";
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm == null)
            {
                MessageBox.Show("카메라 창이 없습니다.");
                return;
            }
            cameraForm.imageView.MeasureLineSelected -= OnMeasureLineSelected;
            cameraForm.imageView.EnableMeasureMode(true);
            cameraForm.imageView.MeasureLineSelected += OnMeasureLineSelected;
        }

        private void OnMeasureLineSelected(Point pt1, Point pt2)
        {
            _pixelDist = Math.Sqrt(Math.Pow(pt1.X - pt2.X, 2) + Math.Pow(pt1.Y - pt2.Y, 2));
            textPixelLength.Text = _pixelDist.ToString("0.##");

            if (double.TryParse(txtRealLength.Text, out double realMm) && realMm > 0)
            {
                double pixelPerMm = _pixelDist / realMm;
                textPPM.Text = pixelPerMm.ToString("0.###");
                lblCalibGuide.Text = $"측정완료: {_pixelDist:0.##} px, 1mm={pixelPerMm:0.###} px";
            }
            else
            {
                textPPM.Text = "";
                lblCalibGuide.Text = $"측정완료: {_pixelDist:0.##} px. 먼저 실제 거리(mm)를 입력하세요.";
            }
        }

        private void btnApplyCalib_Click(object sender, EventArgs e)
        {
            if (_pixelDist <= 0)
            {
                MessageBox.Show("먼저 두 점을 지정하여 픽셀 거리를 측정하세요.");
                return;
            }
            if (!double.TryParse(txtRealLength.Text, out double realMm) || realMm <= 0)
            {
                MessageBox.Show("실제 거리(mm)를 올바르게 입력하세요.");
                return;
            }
            double pixelPerMm = _pixelDist / realMm;
            _mainForm.PixelPerMm = pixelPerMm;
            textPPM.Text = pixelPerMm.ToString("0.###");
            lblCalibResult.Text = $"캘리브레이션 완료: 1mm = {pixelPerMm:0.###} px";
            lblCalibGuide.Text = "캘리브레이션 성공!";
            MessageBox.Show($"캘리브레이션 완료!\n1mm = {pixelPerMm:0.###} 픽셀", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

          
        }

    }
}
