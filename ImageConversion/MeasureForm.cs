using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConversion
{
    public partial class MeasureForm : Form
    {
        private MainForm _mainForm; // MainForm 참조
                                    //   private Point? _firstPoint = null; // 첫 점 기록용
        private Point? _measureLastPt1 = null;
        private Point? _measureLastPt2 = null;

        private double PixelPerMm
        {
            get
            {
                // MainForm에 PixelPerMm이 있다고 가정(없으면 MainForm에서 변수 추가)
                return _mainForm.PixelPerMm > 0 ? _mainForm.PixelPerMm : 1.0;
            }
        }
        public MeasureForm(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;

            comboUnit.Items.AddRange(new string[] { "mm", "px" });
            comboUnit.SelectedIndex = 0;

            btnStartMeasure.Click += btnStartMeasure_Click;
        }

        private void btnStartMeasure_Click(object sender, EventArgs e)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm == null)
            {
                MessageBox.Show("카메라 창이 없습니다.");
                return;
            }
            cameraForm.imageView.MeasureLineSelected -= OnMeasureLineSelected;
            cameraForm.imageView.EnableMeasureMode(true);
            cameraForm.imageView.MeasureLineSelected += OnMeasureLineSelected;
            lblCurrentResult.Text = "포인트를 두 번 클릭하세요";
        }
        private void OnMeasureLineSelected(Point pt1, Point pt2)
        {
            _measureLastPt1 = pt1;
            _measureLastPt2 = pt2;
            // 거리 계산 (픽셀 기준)
            double distPx = Math.Sqrt(Math.Pow(pt1.X - pt2.X, 2) + Math.Pow(pt1.Y - pt2.Y, 2));
            double distMm = distPx / PixelPerMm;
            // (선택) 실거리 변환: _mainForm.PixelPerMm 등 활용 가능
            if (comboUnit.SelectedItem != null && comboUnit.SelectedItem.ToString() == "mm")
                lblCurrentResult.Text = $"실거리: {distMm:0.##} mm";
            else
                lblCurrentResult.Text = $"픽셀: {distPx:0.##} px";

            listMeasurements.Items.Add($"픽셀: {distPx:0.##}, mm: {distMm:0.##}");    

            // 측정모드 종료: 원하면 계속 활성화도 가능
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
                cameraForm.imageView.EnableMeasureMode(false);
        }

        private void btnResetMeasure_Click(object sender, EventArgs e)
        {
            listMeasurements.Items.Clear();
            lblCurrentResult.Text = "측정값 없음";
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
                cameraForm.imageView.ResetMeasureLine();
        }

        private void btnCopyResult_Click(object sender, EventArgs e)
        {
            if (listMeasurements.SelectedItem != null)
            {
                Clipboard.SetText(listMeasurements.SelectedItem.ToString());
            }
            else if (listMeasurements.Items.Count > 0)
            {
                Clipboard.SetText(listMeasurements.Items[listMeasurements.Items.Count - 1].ToString());
            }
        }
    //    private double PixelPerMm => _mainForm.PixelPerMm > 0 ? _mainForm.PixelPerMm : 1.0;
        private void comboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_measureLastPt1 != null && _measureLastPt2 != null)
            {
                double distPx = Math.Sqrt(
                    Math.Pow(_measureLastPt1.Value.X - _measureLastPt2.Value.X, 2) +
                    Math.Pow(_measureLastPt1.Value.Y - _measureLastPt2.Value.Y, 2));
                double distMm = distPx / PixelPerMm;
                if (comboUnit.SelectedItem != null && comboUnit.SelectedItem.ToString() == "mm")
                    lblCurrentResult.Text = $"실거리: {distMm:0.##} mm";
                else
                    lblCurrentResult.Text = $"픽셀: {distPx:0.##} px";
            }
        }
       

        private void btnSaveResult_Click_1(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Text File|*.txt";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (var sw = new StreamWriter(dlg.FileName))
                    {
                        foreach (var item in listMeasurements.Items)
                            sw.WriteLine(item.ToString());
                    }
                }
            }
        }
    }
}
