using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConversion
{
    public static class ImageConvert
    {
    


        public static Mat Convert(Mat original, Mat current, ImageConvertSettings s)
        {
            if (original == null || original.Empty())
            {
                MessageBox.Show("입력 이미지가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            /*
            // 누적 옵션 적용
            var baseImg = (s.KeepResult && current != null && !current.Empty() )
                ? current.Clone()
                : original.Clone();
            */
            Mat baseImg;

            if (s.ConvertType == PropType.Binary)
            {
                baseImg = (current != null && !current.Empty())
                    ? current.Clone()
                    : original.Clone();
            }
            else
            {
                baseImg = (s.KeepResult && current != null && !current.Empty())
                    ? current.Clone()
                    : original.Clone();
            }
            Mat resultImg = new Mat();
            switch (s.ConvertType)
            {
                case PropType.CvtColor:
                    if (s.Mono)
                    {
                        if (baseImg.Channels() == 1)
                        {
                            resultImg = baseImg.Clone();
                        }
                        else
                        {
                            Cv2.CvtColor(baseImg, resultImg, ColorConversionCodes.BGR2GRAY);
                        }
                    }
                    else if (s.HSV)
                    {
                        Mat tempSrc = baseImg;
                        if (baseImg.Channels() == 1)
                        {
                            tempSrc = new Mat();
                            Cv2.CvtColor(baseImg, tempSrc, ColorConversionCodes.GRAY2BGR);
                        }
                        Cv2.CvtColor(tempSrc, resultImg, ColorConversionCodes.BGR2HSV);
                        if (tempSrc != baseImg)
                        {
                            tempSrc.Dispose();
                        }
                    }
                    else
                    {
                        resultImg = baseImg.Clone();
                    }
                    break;


                case PropType.Flip:
                    if (s.FlipXY) Cv2.Flip(baseImg, resultImg, FlipMode.XY);
                    else if (s.FlipX) Cv2.Flip(baseImg, resultImg, FlipMode.X);
                    else if (s.FlipY) Cv2.Flip(baseImg, resultImg, FlipMode.Y);
                    else resultImg = baseImg.Clone();
                    break;
                case PropType.Resize:
                    double fx = s.ScaleX / 100.0;
                    double fy = s.ScaleY / 100.0;
                    Cv2.Resize(baseImg, resultImg, new Size(), fx, fy);
                    break;
                case PropType.Pyramid:
                    if (s.PyrUp) Cv2.PyrUp(baseImg, resultImg);
                    else if (s.PyrDown) Cv2.PyrDown(baseImg, resultImg);
                    else resultImg = baseImg.Clone();
                    break;

                case PropType.Binary:
                    Mat gray = new Mat();
                    if (baseImg.Channels() == 3)
                        Cv2.CvtColor(baseImg, gray, ColorConversionCodes.BGR2GRAY);
                    else
                        gray = baseImg.Clone();
                    Cv2.InRange(gray,
                                new Scalar(s.BinaryMin),
                                new Scalar(s.BinaryMax),
                                resultImg);
                    if (s.InvertBinary)
                    {
                        Cv2.BitwiseNot(resultImg, resultImg);
                    }
                    gray.Dispose();
                    break;
                default:
                    resultImg = baseImg.Clone();
                    break;

                case PropType.Rotate:
                    double angle = s.RotateAngle;
                    Mat src = baseImg;
                    int w = src.Width, h = src.Height;
                    var center = new OpenCvSharp.Point2f(w / 2f, h / 2f);
                    var rotMat = Cv2.GetRotationMatrix2D(center, angle, 1.0);
                    Cv2.WarpAffine(src, resultImg, rotMat, new OpenCvSharp.Size(w, h), InterpolationFlags.Linear, BorderTypes.Constant, new Scalar(0, 0, 0));

                    break;
                case PropType.Blur:
                    switch (s.BlurType)
                    {
                        case "Gaussian":
                            Cv2.GaussianBlur(baseImg, resultImg, new OpenCvSharp.Size(s.BlurKernelSize, s.BlurKernelSize), 0);
                            break;
                        case "Median":
                            Cv2.MedianBlur(baseImg, resultImg, s.BlurKernelSize);
                            break;
                        case "Bilateral":
                            Cv2.BilateralFilter(baseImg, resultImg, s.BlurKernelSize, s.BilateralSigmaColor, s.BilateralSigmaSpace);
                            break;
                        default:
                            resultImg = baseImg.Clone();
                            break;
                    }
                    break;

                case PropType.Edge:
                    {
                        // 그레이 스케일로 준비
                        Mat baseImgGray = new Mat();
                        if (baseImg.Channels() == 3) Cv2.CvtColor(baseImg, baseImgGray, ColorConversionCodes.BGR2GRAY);
                        else baseImgGray = baseImg.Clone();

                        if (s.EdgeMethod == "Canny")
                        {
                            Cv2.Canny(baseImgGray, resultImg, s.CannyTh1, s.CannyTh2, s.CannyAperture, true);
                        }
                        else if (s.EdgeMethod == "Sobel")
                        {
                            Mat sobel = new Mat();
                            Cv2.Sobel(baseImgGray, sobel, MatType.CV_16S, s.SobelDx, s.SobelDy, s.SobelKsize);
                            Mat abs = new Mat();
                            Cv2.ConvertScaleAbs(sobel, abs);
                            resultImg = abs;
                            sobel.Dispose();
                        }
                        else // Laplacian
                        {
                            Mat lap = new Mat();
                            Cv2.Laplacian(baseImgGray, lap, MatType.CV_16S, s.LaplacianKsize);
                            Mat abs = new Mat();
                            Cv2.ConvertScaleAbs(lap, abs);
                            resultImg = abs;
                            lap.Dispose();
                        }
                        baseImgGray.Dispose();
                        break;
                    }

                case PropType.Morphology:
                    {
                        // 바이너리/그레이 모두 동작할 수 있게 입력은 그대로 사용
                        int k = Math.Max(1, s.MorphKernel);
                        if (k % 2 == 0) k += 1; // 홀수 권장

                        MorphShapes shape = MorphShapes.Rect;
                        if (s.MorphShape == "Ellipse") shape = MorphShapes.Ellipse;
                        else if (s.MorphShape == "Cross") shape = MorphShapes.Cross;

                        using (var kernel = Cv2.GetStructuringElement(shape, new Size(k, k)))
                        {
                            MorphTypes op = MorphTypes.Erode;
                            switch (s.MorphOp)
                            {
                                case "Erode": op = MorphTypes.Erode; break;
                                case "Dilate": op = MorphTypes.Dilate; break;
                                case "Open": op = MorphTypes.Open; break;
                                case "Close": op = MorphTypes.Close; break;
                            }

                            // Erode/Dilate는 MorphologyEx로 통일해서 처리해도 OK
                            Cv2.MorphologyEx(baseImg, resultImg, op, kernel, iterations: s.MorphIterations);
                        }
                        break;
                    }
            }
            baseImg.Dispose();
            return resultImg;
        }
    }
}
