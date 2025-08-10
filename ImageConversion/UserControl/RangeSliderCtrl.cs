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
    public partial class RangeSliderCtrl : UserControl
    {
    
        private int sliderMinX = 30;
        private int sliderMaxX = 200;
        private bool draggingMin = false;
        private bool draggingMax = false;

        public RangeSliderCtrl()
        {

            InitializeComponent();
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.Minimum = 0;
            this.Maximum = 255;
            this.SliderMin = 30;
            this.SliderMax = 200;

            this.BackColor = Color.Empty;

            this.Size = new Size(200, 45);
        }

        // 공개 속성
        [Category("Range")]
        public int Minimum { get; set; }

        [Category("Range")]
        public int Maximum { get; set; }

        private int _sliderMinValue = 30;
        private int _sliderMaxValue = 200;

        [Category("Range")]
        public int SliderMin
        {
            get => _sliderMinValue;
            set
            {
                _sliderMinValue = value;
                UpdateSliderPosition();
                Invalidate();
            }
        }

        [Category("Range")]
        public int SliderMax
        {
            get => _sliderMaxValue;
            set
            {
                _sliderMaxValue = value;
                UpdateSliderPosition();
                Invalidate();
            }
        }

        private void UpdateSliderPosition()
        {
            int range = Maximum - Minimum;
            int width = this.Width - 20;
            sliderMinX = 10 + (_sliderMinValue - Minimum) * width / range;
            sliderMaxX = 10 + (_sliderMaxValue - Minimum) * width / range;
        }

        private void UpdateSliderValue()
        {
            int width = this.Width - 20;
            int range = Maximum - Minimum;
            _sliderMinValue = Minimum + (sliderMinX - 10) * range / width;
            _sliderMaxValue = Minimum + (sliderMaxX - 10) * range / width;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int height = this.Height;

            // 슬라이더 전체 바
            int margin = 10;
            int barWidth = this.Width - 2 * margin;
            int barX = margin;
            int barY = this.Height - 15;

            g.FillRectangle(Brushes.LightGray, barX, barY - 4, barWidth, 8);


            // 선택 범위
            g.FillRectangle(Brushes.Aqua, sliderMinX, barY - 4, sliderMaxX - sliderMinX, 8);

            // 썸들
            DrawThumb(g, sliderMinX, barY);
            DrawThumb(g, sliderMaxX, barY);
            g.DrawString(SliderMin.ToString(), this.Font, Brushes.Black, sliderMinX - 10, barY - 25);
            g.DrawString(SliderMax.ToString(), this.Font, Brushes.Black, sliderMaxX - 10, barY - 25);
            //sliderMinX = barX + (_sliderMinValue - Minimum) * barWidth / (Maximum - Minimum);
            //sliderMaxX = barX + (_sliderMaxValue - Minimum) * barWidth / (Maximum - Minimum);
        }

        private void DrawThumb(Graphics g, int x, int centerY)
        {

            int radius = 8;
            g.FillEllipse(Brushes.White, x - radius, centerY - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, x - radius, centerY - radius, radius * 2, radius * 2);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Math.Abs(e.X - sliderMinX) <= 8)
                draggingMin = true;
            else if (Math.Abs(e.X - sliderMaxX) <= 8)
                draggingMax = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (draggingMin)
            {
                // 최소값 썸이 슬라이더 바 왼쪽 밖으로 못 나가고, 오른쪽 썸보다 너무 가까이 못 가도록
                sliderMinX = Math.Max(10, Math.Min(e.X, sliderMaxX - 10));
                UpdateSliderValue();
                RaiseValueChanged();
                Invalidate();
            }
            else if (draggingMax)
            {
                // 최대값 썸이 슬라이더 바 오른쪽 밖으로 못 나가고, 왼쪽 썸보다 너무 가까이 못 가도록
                sliderMaxX = Math.Min(this.Width - 10, Math.Max(e.X, sliderMinX + 10));
                UpdateSliderValue();
                RaiseValueChanged();
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            draggingMin = draggingMax = false;
        }
        private void RangeSliderControl_Load(object sender, EventArgs e)
        {

        }
        public event EventHandler ValueChanged;

        private void RaiseValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Height < 40)
                this.Height = 40;
        }


        private void RangeSliderControlExam_Load(object sender, EventArgs e)
        {

        }
    }
}

