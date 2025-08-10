using ImageConversion.Properties;
using OpenCvSharp;
using OpenCvSharp.Extensions;
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
using WeifenLuo.WinFormsUI.Docking;

namespace ImageConversion
{
    public partial class CameraForm : DockContent
    {
       
        public CameraForm()
        {
            InitializeComponent();
        }

        public void LoadImage(string filePath)
        {
            if (File.Exists(filePath) == false)
                return;

            Image bitmap = Image.FromFile(filePath);
            imageView.LoadBitmap((Bitmap)bitmap);

        }
    
        private void CameraForm_Resize(object sender, EventArgs e)
        {
            int margin = 0;
            imageView.Width = this.Width - margin * 2;
            imageView.Height = this.Height - margin * 2;
            imageView.Location = new System.Drawing.Point(margin, margin);
        }

        public void UpdateDisplay(Bitmap bitmap = null, float rotationAngle = 0f)
        {
            if (imageView != null)
                imageView.UpdateBitmap(bitmap);           
        }

        
        public Bitmap GetDisplayImage()
        {
            Bitmap curImage = null;
            if (imageView != null)
                curImage = imageView.GetCurBitmap();
            return curImage;
        }

        public void ShowBlobs(List<BlobResult> blobs)
        {
            imageView.SetBlobs(blobs);
        }

        private void btnBlob_Click(object sender, EventArgs e)
        {
            var mainForm = MainForm.Instance;
            var binaryProp = mainForm?.GetBinaryProp();
            if (binaryProp == null)
            {
                MessageBox.Show("BinaryProp 컨트롤을 찾을 수 없습니다!");
                return;
            }
            var bmp = imageView.GetCurBitmap();
            var src = BitmapConverter.ToMat(bmp);
            var binary = binaryProp.GetBinaryImage(src);

            var blobForm = new BlobForm();
            blobForm.SetBinaryImage(binary.Clone());
            blobForm.Show();
        }

        /*
        public void ResetDisplay()
        {
            imageView.ResetEntity();
        }

        //FIXME 검사 결과를 그래픽으로 출력하기 위한 정보를 받는 함수
        public void AddRect(List<DrawInspectInfo> rectInfos)
        {
            imageView.AddRect(rectInfos);
        }
        */
    }
}
