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
  
    public partial class BinaryProp : UserControl
    {
       
        public BinaryProp()
        {
            InitializeComponent();
            rangeSliderBinary.Minimum = 0;
            rangeSliderBinary.Maximum = 255;
            rangeSliderBinary.SliderMin = 40;
            rangeSliderBinary.SliderMax = 200;


           
        }
        public int MinValue => rangeSliderBinary.SliderMin;
        public int MaxValue => rangeSliderBinary.SliderMax;

        public bool Invert => checkBoxBinaryInvert.Checked;
        
        public event EventHandler ValueChanged;
        private void rangeSliderBinary_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
   
        public Mat GetBinaryImage(Mat src)
        {
            Mat input = src.Clone();
            if (input.Channels() == 4)
                Cv2.CvtColor(input, input, ColorConversionCodes.BGRA2BGR);
            Mat gray = new Mat();
            if (input.Channels() == 3)
                Cv2.CvtColor(input, gray, ColorConversionCodes.BGR2GRAY);
            else
                gray = input.Clone();
            Cv2.MinMaxLoc(gray, out double minGray, out double maxGray);          

            bool isBinary = (minGray == 0 && maxGray == 255 && gray.Type() == MatType.CV_8UC1);
            if (isBinary)
            {
              // Invert 체크는 필요하다면 적용
              //  if (Invert)
                {
             //      Mat inverted = new Mat();
             //      Cv2.BitwiseNot(gray, inverted);
             //      return inverted;
                }
                return gray;
            }
            else
            {
               
                int min = MinValue;
                int max = MaxValue;
                Mat binary = new Mat();
                Cv2.InRange(gray, min, max, binary);
               //if (Invert)
               //Cv2.BitwiseNot(binary, binary);
                return binary;
            }
        }
     
    }

}
