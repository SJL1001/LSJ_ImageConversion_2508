using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ImageConversion
{     
   
    public enum PropType
    {
        CvtColor,        
        Flip,
        Resize,
        Pyramid,
        Binary,
        Rotate,
        Blur,
        Edge,
        Morphology
    }

    public partial class PropertiesForm : DockContent
    {
        private MainForm _mainForm;
        private readonly ImageConvertProcess _convertProcess;
        Dictionary<string, TabPage> _allTabs = new Dictionary<string, TabPage>();
        public PropertiesForm(ImageConvertProcess convertProcess, MainForm mainForm)
        {
           
            InitializeComponent();
            LoadOptionControl(PropType.CvtColor);
            LoadOptionControl(PropType.Flip);
            LoadOptionControl(PropType.Resize);
            LoadOptionControl(PropType.Pyramid);
            LoadOptionControl(PropType.Binary);
            LoadOptionControl(PropType.Rotate);
            LoadOptionControl(PropType.Blur);
            LoadOptionControl(PropType.Morphology);
            LoadOptionControl(PropType.Edge);
            _convertProcess = convertProcess;
            _mainForm = mainForm;
            resetButton.Click += resetButton_Click;
            undoButton.Click += undoButton_Click;
            redoButton.Click += redoButton_Click;
           
            tabPropControl.SelectedIndex = (int)PropType.CvtColor;
        }

        private void LoadOptionControl(PropType propType)
        {
            string tabName = propType.ToString();

            
            foreach (TabPage tabPage in tabPropControl.TabPages)
            {
                if (tabPage.Text == tabName)
                    return;
            }

          
            if (_allTabs.TryGetValue(tabName, out TabPage page))
            {
                tabPropControl.TabPages.Add(page);
                return;
            }
       
       
            var ctrl = CreateUserControl(propType);
            if (ctrl == null) return;

           
            if (propType == PropType.Binary && ctrl is BinaryProp bin)
            {
                
                bin.ValueChanged += (s, e) => applyButton.PerformClick();
            }

            TabPage newTab = new TabPage(propType.ToString())
            {
                
                Dock = DockStyle.Fill
            };
            ctrl.Dock = DockStyle.Fill;
            newTab.Controls.Add(ctrl);
            tabPropControl.TabPages.Add(newTab);
            tabPropControl.SelectedTab = newTab; // 새 탭 선택

            _allTabs[propType.ToString()] = newTab;        
        }
        public TabPage GetTabPage(string tabName)
        {
            foreach (TabPage tab in tabPropControl.TabPages)
            {
                if (tab.Text == tabName)
                    return tab;
            }
            return null;
        }
        private UserControl CreateUserControl(PropType propType)
        {
            UserControl curProp = null;

            switch (propType)
            {
                case PropType.CvtColor: 
                    curProp = new CvtColorProp();
                    break;

                case PropType.Flip:
                    curProp = new FlipProp(); 
                    break;

                case PropType.Resize:
                    curProp = new ResizeProp();
                    break;
                
                case PropType.Pyramid:
                    curProp = new PyramidProp();
                    break;

                case PropType.Binary:
                    curProp = new BinaryProp();
                    break;

                    case PropType.Rotate:
                        curProp = new RotateProp();
                    break;

                    case PropType.Blur:
                    curProp = new BlurProp();
                    break;
                case PropType.Edge:
                    curProp = new EdgeProp();
                    break;
                case PropType.Morphology:
                    curProp = new MorphologyProp();
                    break;  
                       
                default:
                    MessageBox.Show("유효하지 않은 옵션입니다.");
                    return null;
            }

            return curProp;
        }
        public void SelectTab(string tabName)
        {
            foreach (TabPage tab in tabPropControl.TabPages)
            {
                if (tab.Text == tabName || tab.Name == tabName)
                {
                    tabPropControl.SelectedTab = tab;
                    return;
                }
            }            
            if (Enum.TryParse(tabName, out PropType type))
            {
                LoadOptionControl(type);
                SelectTab(tabName); 
            }
            else
            {
                MessageBox.Show($"'{tabName}' 탭을 찾을 수 없습니다.");
            }
        }             

        private void applyButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Apply Button Clicked!");
            _convertProcess.ApplyConversion();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            _convertProcess.Undo();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {         
            _convertProcess.Reset();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            _convertProcess.Redo();
        }

        private Action<Rectangle> _roiDrawnHandler;

        private void cropButton_Click(object sender, EventArgs e)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm == null) return;

            // ROI 모드 켜기
            cameraForm.imageView.EnableRoiSelection(true);

            // 이전 구독 해제 후 새로 구독
            if (_roiDrawnHandler != null)
                cameraForm.imageView.RoiSelected -= _roiDrawnHandler;

            _roiDrawnHandler = (rect) =>
            {               
                if (rect.Width > 0 && rect.Height > 0)
                {
                    cameraForm.imageView.EnableRoiSelection(false); // 모드 OFF
                                                                    // ROI는 그대로 두어서 적용 버튼에서 읽을 수 있게 둠
                }
                else
                {                    
                    cameraForm.imageView.ClearRoi();
                    cameraForm.imageView.EnableRoiSelection(false);
                }
                              // 한 번만 동작하도록 구독 해제
                cameraForm.imageView.RoiSelected -= _roiDrawnHandler;
                _roiDrawnHandler = null;
            };

            cameraForm.imageView.RoiSelected += _roiDrawnHandler;
        }
        private void OnRoiSelectedOnce(Rectangle roiRect)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm == null) return;

            if (roiRect.Width <= 0 || roiRect.Height <= 0) return;

            Rectangle roiInImage = cameraForm.imageView.ScreenToVirtualRoi(roiRect);
            _mainForm._convertProcess.CropToRoi(roiInImage);

            // ROI 해제 및 모드 종료
            cameraForm.imageView.ClearRoi();
            cameraForm.imageView.EnableRoiSelection(false);

            // 다시 호출되지 않도록 이벤트 해제
            cameraForm.imageView.RoiSelected -= OnRoiSelectedOnce;
        }
        private void cropApplyButton_Click(object sender, EventArgs e)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm == null) return;         
            var roi = cameraForm.imageView.SelectedRoi;
            if (roi == null || roi.Value.Width <= 0 || roi.Value.Height <= 0)
            {
                MessageBox.Show("먼저 ROI를 지정하세요.");
                return;
            }
            Rectangle roiInImage = cameraForm.imageView.ScreenToVirtualRoi(roi.Value);

          //  MainForm mainForm = this.FindForm() as MainForm;
            _mainForm?._convertProcess.CropToRoi(roiInImage);

            cameraForm.imageView.ClearRoi();
            cameraForm.imageView.EnableRoiSelection(false);
        }

        public BinaryProp GetBinaryProp()
        {
            // Binary TabPage에서 BinaryProp를 찾아서 반환
            foreach (TabPage tab in tabPropControl.TabPages)
            {
                if (tab.Text == PropType.Binary.ToString())
                {
                    foreach (Control ctrl in tab.Controls)
                        if (ctrl is BinaryProp bp)
                            return bp;
                }
            }
            return null;
        }
    }
}

