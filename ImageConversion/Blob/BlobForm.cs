using OpenCvSharp;
using OpenCvSharp.Extensions;
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
    public partial class BlobForm : Form
    {

        private readonly BlobAlgorithm blobAlgo = new BlobAlgorithm();
        private Mat currentBinary = null;
        private List<BlobResult> lastResult = new List<BlobResult>();

        public BlobForm()
        {
            InitializeComponent();

            //  imageViewCtrl1.Location = new System.Drawing.Point(10, 10);
            // imageViewCtrl1.Size = new System.Drawing.Size(500, 500);
            dgvBlobResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBlobResult.Columns.Add("No", "No");
            dgvBlobResult.Columns.Add("Area", "Area");
            dgvBlobResult.Columns.Add("Centroid", "Centroid");
            dgvBlobResult.Columns.Add("BoundingBox", "BoundingBox");
            dgvBlobResult.ReadOnly = true;
            dgvBlobResult.AllowUserToAddRows = false;
            dgvBlobResult.AllowUserToResizeRows = false;
            dgvBlobResult.RowHeadersVisible = false;
            dgvBlobResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lblStat.Location = new System.Drawing.Point(520, 370);
            lblStat.Size = new System.Drawing.Size(350, 30);
            lblStat.Text = "Blob 개수: 0";

            // --- NumericUpDown (Min/Max Area) ---
            lblMin.Text = "Min Area";
            lblMin.Location = new System.Drawing.Point(520, 420);
            lblMin.Size = new System.Drawing.Size(60, 30);

            numMinArea.Location = new System.Drawing.Point(590, 420);
            numMinArea.Size = new System.Drawing.Size(70, 30);
            numMinArea.Minimum = 0;
            numMinArea.Maximum = 100000;
            numMinArea.Value = 1000;
            numMinArea.ValueChanged += (s, e) => RunBlob();

            lblMax.Text = "Max Area";
            lblMax.Location = new System.Drawing.Point(670, 420);
            lblMax.Size = new System.Drawing.Size(60, 30);

            numMaxArea.Location = new System.Drawing.Point(740, 420);
            numMaxArea.Size = new System.Drawing.Size(70, 30);
            numMaxArea.Minimum = 1;
            numMaxArea.Maximum = 1000000;
            numMaxArea.Value = 1000000;
            numMaxArea.ValueChanged += (s, e) => RunBlob();

            imageViewCtrl1.BringToFront();
            imageViewCtrl1.Visible = true;

        }

        // private ImageViewCtrl imageViewCtrl;

        public void SetBinaryImage(Mat binary)
        {

            currentBinary = binary.Clone();

            // 썸네일로 보기 좋게 줄여서 보여줌(너무 크면) - optional
            Bitmap bmp = BitmapConverter.ToBitmap(currentBinary);

            imageViewCtrl1.LoadBitmap(bmp);
            RunBlob();
        }
        /// <summary>
        /// Blob 분석 및 결과 표시
        /// </summary>
        /// 

        private void RunBlob()
        {

            if (currentBinary == null) return;
            blobAlgo.MinArea = (double)numMinArea.Value;
            blobAlgo.MaxArea = (double)numMaxArea.Value;
            lastResult = blobAlgo.Analyze(currentBinary);
            ShowGrid();
            imageViewCtrl1.SetBlobs(lastResult);
        }

        /// <summary>
        /// GridView/통계 업데이트
        /// </summary>
        private void ShowGrid()
        {
            dgvBlobResult.Rows.Clear();
            foreach (var blob in lastResult)
            {
                dgvBlobResult.Rows.Add(
                    blob.Index,
                    blob.Area.ToString("0.##"),
                    $"({blob.Centroid.X:0.##}, {blob.Centroid.Y:0.##})",
                    $"({blob.BoundingBox.X},{blob.BoundingBox.Y},{blob.BoundingBox.Width},{blob.BoundingBox.Height})"
                );
            }
            if (lastResult.Count > 0)
            {
                double avg = lastResult.Average(b => b.Area);
                double max = lastResult.Max(b => b.Area);
                double min = lastResult.Min(b => b.Area);
                lblStat.Text = $"Blob 개수: {lastResult.Count} | 평균: {avg:0.##} | 최대: {max:0.##} | 최소: {min:0.##}";
            }
            else lblStat.Text = "Blob 개수: 0";
        }

        private void btnSendToMain_Click(object sender, EventArgs e)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
                cameraForm.ShowBlobs(lastResult);
        }

        private void dgvBlobResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvBlobResult.SelectedRows.Count > 0)
            {
                int idx = dgvBlobResult.SelectedRows[0].Index;
                if (idx >= 0 && idx < lastResult.Count)
                {
                    lastResult.RemoveAt(idx);
                    ShowGrid();
                    imageViewCtrl1.SetBlobs(lastResult); // Blob Overlay 갱신
                }
            }
        }
        private void dgvBlobResult_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBlobResult.SelectedRows.Count > 0)
            {
                int idx = dgvBlobResult.SelectedRows[0].Index;
                imageViewCtrl1.SelectBlob(idx); // 이미지뷰에서 해당 Blob 노란색 하이라이트
            }
            else
            {
                imageViewCtrl1.SelectBlob(-1); // 선택 해제
            }
        }

        private void dgvBlobResult_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dgvBlobResult.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dgvBlobResult.ClearSelection();
                    dgvBlobResult.Rows[hit.RowIndex].Selected = true;
                    contextMenuStrip1.Show(dgvBlobResult, e.Location);
                }
            }
        }

        private void menuLabelEdit_Click(object sender, EventArgs e)
        {
            if (dgvBlobResult.SelectedRows.Count == 0) return;
            int idx = dgvBlobResult.SelectedRows[0].Index;
            var blob = lastResult[idx];
            string label = Microsoft.VisualBasic.Interaction.InputBox("라벨 입력", "Blob 라벨", blob.Label ?? "");
            if (!string.IsNullOrWhiteSpace(label))
            {
                blob.Label = label;
                ShowGrid();
                imageViewCtrl1.Invalidate();
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvBlobResult.SelectedRows)
            {
                int idx = row.Index;
                if (idx >= 0 && idx < lastResult.Count)
                    lastResult[idx].Label = "DeleteMe"; // 일단 표시 후
            }
            // 삭제(역순으로)
            lastResult = lastResult.Where(b => b.Label != "DeleteMe").ToList();
            ShowGrid();
            imageViewCtrl1.SetBlobs(lastResult);
        }
    }
    }

