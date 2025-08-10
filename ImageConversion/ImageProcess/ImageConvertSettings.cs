using ImageConversion.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ImageConversion
{
    public class ImageConvertSettings  // 각 변환의 옵션 설정
    {
        public PropType ConvertType { get; set; }        // 변환 타입 설정
        public bool Mono { get; set; }
        public bool HSV { get; set; }
        public bool FlipX { get; set; }
        public bool FlipY { get; set; }
        public bool FlipXY { get; set; }
        public double ScaleX { get; set; } = 1.0;
        public double ScaleY { get; set; } = 1.0;
        public int BinaryMin { get; set; } = 0;
        public int BinaryMax { get; set; } = 255;
        public double RotateAngle { get; set; } = 0.0;
        public bool InvertBinary { get; set; } = false;
        public bool PyrUp { get; set; }
        public bool PyrDown { get; set; }
        public bool KeepResult { get; set; }        // 변환 누적

        public string BlurType { get; set; } = "Gaussian";
        public int BlurKernelSize { get; set; } = 5;
        public double BilateralSigmaColor { get; set; } = 75;
        public double BilateralSigmaSpace { get; set; } = 75;
       public string EdgeMethod { get; set; } = "Canny";
        public double CannyTh1 { get; set; } = 50;
        public double CannyTh2 { get; set; } = 150;
        public int CannyAperture { get; set; } = 3;
        public int SobelDx { get; set; } = 1;
        public int SobelDy { get; set; } = 0;
        public int SobelKsize { get; set; } = 3;

        public int LaplacianKsize { get; set; } = 3;
       
        public string MorphOp { get; set; } = "Erode";
        public string MorphShape { get; set; } = "Rect";
        public int MorphKernel { get; set; } = 3;
        public int MorphIterations { get; set; } = 1;

        public static ImageConvertSettings FromForm(PropertiesForm form)  // 폼의상태를 읽어서, 그 값들로 변환 옵션 객체(ImageConvertSettings)를 만들어 반환
        {
            var convertType = (PropType)form.tabPropControl.SelectedIndex;
            var settings = new ImageConvertSettings
            {
                ConvertType = convertType,
                KeepResult = form.checkBoxKeepResult.Checked,
                InvertBinary = false
            };

            var page = form.tabPropControl.SelectedTab;
            
            if (page?.Controls.Count > 0)
            {
                var ctrl = page.Controls[0];
                if (settings.ConvertType == PropType.Binary && ctrl is BinaryProp bin)
                {
                    settings.BinaryMin = bin.MinValue;
                    settings.BinaryMax = bin.MaxValue;
                    settings.InvertBinary = bin.Invert;  
                }

                switch (convertType)
                {
                    case PropType.CvtColor:

                        var cvt = ctrl as CvtColorProp;
                        if (cvt != null)
                        {
                            settings.Mono = cvt.MonoChecked;
                            settings.HSV = cvt.HSVChecked;
                        }
                        break;

                    case PropType.Flip:
                        var flip = ctrl as FlipProp;
                        if (flip != null)
                        {
                            settings.FlipXY = flip.FlipXY;
                            settings.FlipX = flip.FlipX;
                            settings.FlipY = flip.FlipY;

                        }
                        break;

                    case PropType.Resize:
                        var resize = ctrl as ResizeProp;
                        if (resize != null)
                        {
                            settings.ScaleX = resize.ScaleX;
                            settings.ScaleY = resize.ScaleY;
                        }
                        break;

                    case PropType.Pyramid:
                        var pyr = ctrl as PyramidProp;
                        if (pyr != null)
                        {
                            settings.PyrUp = pyr.PyrUp;
                            settings.PyrDown = pyr.PyrDown;
                        }
                        break;

                    case PropType.Binary:
                        var binary = ctrl as BinaryProp;
                        if (binary != null)
                        {
                            settings.BinaryMin = binary.MinValue;
                            settings.BinaryMax = binary.MaxValue;
                            settings.InvertBinary = binary.Invert;
                        }
                        break;

                    case PropType.Rotate:
                        var rotate = ctrl as RotateProp;
                        if(rotate != null)
                        {
                            settings.RotateAngle = rotate.Angle * (rotate.Clockwise ? 1 : -1);
                        }
                        break;

                    case PropType.Blur:
                        var blur = ctrl as BlurProp;
                        if (blur != null)
                        {
                            settings.BlurType = blur.BlurType;
                            settings.BlurKernelSize = blur.KernelSize;
                            settings.BilateralSigmaColor = blur.SigmaColor;
                            settings.BilateralSigmaSpace = blur.SigmaSpace;
                        }
                        break;

                        case PropType.Edge:
                        if (ctrl is EdgeProp ep)
                        {
                            settings.EdgeMethod = ep.Method;
                            settings.CannyTh1 = ep.CannyThreshold1;
                            settings.CannyTh2 = ep.CannyThreshold2;
                            settings.CannyAperture = ep.CannyApertureSize;
                            settings.SobelDx = ep.SobelDx;
                            settings.SobelDy = ep.SobelDy;
                            settings.SobelKsize = ep.SobelKsize;
                            settings.LaplacianKsize = ep.LaplacianKsize;
                        }
                        break;
                    case PropType.Morphology:
                        if (ctrl is MorphologyProp mp)
                        {
                            settings.MorphOp = mp.Operation;
                            settings.MorphShape = mp.Shape;
                            settings.MorphKernel = mp.KernelSize;
                            settings.MorphIterations = mp.Iterations;
                        }
                        break;
                }

            }
                return settings;
            }


        }
    }

