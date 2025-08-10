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
    public partial class MainForm : Form
    {
        private static DockPanel   _dockPanel;
        private CameraForm         _cameraForm;
        private PropertiesForm      _propertiesForm;       
        public ImageConvertProcess _convertProcess;
        public static MainForm Instance { get; private set; }
        public BinaryProp GetBinaryProp() => _propertiesForm?.GetBinaryProp();
        public double PixelPerMm { get; set; } = 1.0;
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            _dockPanel = new DockPanel 
            {
                Dock = DockStyle.Fill, 
                Theme = new VS2015DarkTheme() 
            };
            mainContainer.ContentPanel.Controls.Add(_dockPanel);          

            _cameraForm = new CameraForm();

            _convertProcess = new ImageConvertProcess(() => ImageConvertSettings.FromForm(_propertiesForm));

            _propertiesForm = new PropertiesForm(_convertProcess, this);
            _convertProcess.OnImageUpdated += mat =>
            {               
                var bmp = BitmapConverter.ToBitmap(mat);
                _cameraForm.UpdateDisplay(bmp);               
            };

            
            LoadDockingWindows();
            
        }

        private void LoadDockingWindows()
        {
            _dockPanel.AllowEndUserDocking = false;
            _cameraForm.Show(_dockPanel, DockState.Document);
            _propertiesForm.Show(_dockPanel, DockState.DockRight);
         
        }

        public static T GetDockForm<T>() where T : DockContent
        {
            var findForm = _dockPanel.Documents.OfType<T>().FirstOrDefault();
            return findForm;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = GetDockForm<CameraForm>();
            if (cameraForm == null)
                return;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "이미지 파일 선택";
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    cameraForm.LoadImage(filePath);
                    textImagePath.Text = "파일경로 :" + filePath;
                    var mat = BitmapConverter.ToMat((Bitmap)Image.FromFile(filePath));
                    _convertProcess.LoadImage(mat);
                }                
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mat = _convertProcess.GetCurrentImage();
            if (mat == null || mat.Empty())
            {
                MessageBox.Show("저장할 이미지가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (Bitmap bmp = BitmapConverter.ToBitmap(mat))
            {
              
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Title = "이미지 저장";
                    dlg.Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|BMP (*.bmp)|*.bmp";
                    dlg.DefaultExt = "png";

                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    bmp.Save(dlg.FileName);
                }
            }
        }

        private void measureTool_Click(object sender, EventArgs e)
        {
            MeasureForm measureform = new MeasureForm(this);
            measureform.Show();
        }
        private void calibrationTool_Click(object sender, EventArgs e)
        {
            Calibration caliform = new Calibration(this);
            caliform.Show();
        }
       
        private void blobTool_Click(object sender, EventArgs e)
        {
            var srcImage = this.GetCurrentImage(); // 원본
            var binImage = GetBinaryProp().GetBinaryImage(srcImage); // 이진화!
           
            var blobForm = new BlobForm();
            blobForm.SetBinaryImage(binImage); // 여기서 binImage를 넘김
            blobForm.Show();
        }
       
       


        public Mat GetCurrentImage()
        {
            return _convertProcess.GetCurrentImage(); // 구조에 따라
        }


    }
}
