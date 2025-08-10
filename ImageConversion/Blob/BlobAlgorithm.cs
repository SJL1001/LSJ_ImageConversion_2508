using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConversion
{
    public class BlobAlgorithm
    {
        public double MinArea { get; set; } = 5;
        public double MaxArea { get; set; } = double.MaxValue;

        public List<BlobResult> Analyze(Mat binaryImage)
        {
            var result = new List<BlobResult>();
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binaryImage, out contours, out hierarchy,
                RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            Console.WriteLine($"Contour count: {contours.Length}");
            int idx = 1;
            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
               if (area < MinArea || area > MaxArea) continue;
                var moments = Cv2.Moments(contour);
                float cx = (float)(moments.M10 / (moments.M00 + 1e-5));
                float cy = (float)(moments.M01 / (moments.M00 + 1e-5));
                var boundingRect = Cv2.BoundingRect(contour);
                result.Add(new BlobResult
                {
                    Index = idx++,
                    Area = area,
                    Centroid = new PointF(cx, cy),
                    BoundingBox = new Rectangle(boundingRect.X, boundingRect.Y, boundingRect.Width, boundingRect.Height)
                });
            }
            return result;
        }
    }
    public class BlobResult
    {
        public int Index { get; set; }
        public double Area { get; set; }
        public PointF Centroid { get; set; }
        public Rectangle BoundingBox { get; set; }

        public string Label { get; set; }
    }
}

