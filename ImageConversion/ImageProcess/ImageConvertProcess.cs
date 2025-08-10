using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConversion
{
    public class ImageConvertProcess //: IDisposable
    {
        private Stack<Mat> _undoHistory = new Stack<Mat>();      //undo와 redo 스택 기록
        private Stack<Mat> _redoHistory = new Stack<Mat>();

        private Mat _srcImage;                          // 원본 이미지
        private Mat _currentImage;                      // 변환된 이미지

        private Func<ImageConvertSettings> _getSettings;  //Func는 ImageConvertSettings의 세팅 값을 최신상태로 받아와서 값을 반환함(리턴값이 있을때)
        public event Action<Mat> OnImageUpdated;              //Action은 Mat형식의 이미지를 받아 단순히 처리만함 (반환값이 없을때) 

        /*위의 두개는 이해가 어려워 gpt에 질문
        Func와 Action은 미리 C#에서 정의된 deligate방식으로, 코드를 더 단순화 할수있음(deligate를 직접쓰면 코드가 길어짐)
        */
        private Mat _preBinaryImage;

        public ImageConvertProcess(Func<ImageConvertSettings> getSettings)
        {                              //ImageConvertSettings의 값을 최신 상태로 받아올 수 있도록, 함수를 매개변수로 받아 _getSettings에 저장
            _getSettings = getSettings;
        }

        public void LoadImage(Mat image)          // 이미지를 새로 불러올때 이미지박스 초기화
        {
            ClearHistory();
            _srcImage?.Dispose();
            _currentImage?.Dispose();

            _srcImage = image?.Clone();
            _currentImage = image?.Clone();
            _preBinaryImage?.Dispose();
            _preBinaryImage = _currentImage?.Clone();
            OnImageUpdated?.Invoke(_currentImage);
        }

        public void ApplyConversion()
        {

            if (_srcImage == null || _srcImage.Empty())
            {
                System.Windows.Forms.MessageBox.Show("먼저 이미지를 불러오세요.");
                return;
            }

            var settings = _getSettings();
            if (settings.ConvertType == PropType.Binary)
            {
                // 만약 _preBinaryImage가 없다면(처음 Binary 선택) 현재 이미지를 저장
                if (_preBinaryImage == null || _preBinaryImage.Empty())
                {
                    _preBinaryImage?.Dispose();
                    _preBinaryImage = _currentImage?.Clone();
                }

                var converted = ImageConvert.Convert(_preBinaryImage, _preBinaryImage, settings);

                if (converted == null)
                    return;

                if (_currentImage != null && !_currentImage.Empty())
                    _undoHistory.Push(_currentImage.Clone());
                else
                    _undoHistory.Push(_srcImage.Clone());

                _currentImage.Dispose();
                _currentImage = converted;
                _redoHistory.Clear();

                OnImageUpdated?.Invoke(_currentImage);
            }
            else
            {
                // Binary 외의 변환이면 기존처럼
                var converted = ImageConvert.Convert(_srcImage, _currentImage, settings);
                if (converted == null)
                    return;

                if (_currentImage != null && !_currentImage.Empty())
                    _undoHistory.Push(_currentImage.Clone());
                else
                    _undoHistory.Push(_srcImage.Clone());

                _currentImage.Dispose();
                _currentImage = converted;
                _redoHistory.Clear();

                // Binary가 아닌 변환에서는 반드시 preBinaryImage도 동기화
                _preBinaryImage?.Dispose();
                _preBinaryImage = _currentImage?.Clone();

                OnImageUpdated?.Invoke(_currentImage);
            }
        }

        public void Undo()           //Undo
        {
            if (_undoHistory.Count == 0) return;

            _redoHistory.Push(_currentImage.Clone());
            _currentImage.Dispose();

            _currentImage = _undoHistory.Pop();
            OnImageUpdated?.Invoke(_currentImage);
        }

        public void Redo()                // Redo
        {
            if (_redoHistory.Count == 0) return;

            _undoHistory.Push(_currentImage.Clone());
            _currentImage.Dispose();

            _currentImage = _redoHistory.Pop();
            OnImageUpdated?.Invoke(_currentImage);
        }

        public void ClearHistory()            // undo,redo 기록 제거
        {
            while (_undoHistory.Count > 0)
            {
                _undoHistory.Pop()?.Dispose();
            }
            while (_redoHistory.Count > 0)
            {
                _redoHistory.Pop()?.Dispose();
            }
            _undoHistory.Clear();
            _redoHistory.Clear();
        }
        public void Reset()
        {
            if (_srcImage == null) return;
            ClearHistory();
            _currentImage?.Dispose();
            _currentImage = _srcImage.Clone();
            _preBinaryImage?.Dispose();
            _preBinaryImage = _currentImage?.Clone();
            OnImageUpdated?.Invoke(_currentImage);
        }
        public Mat GetCurrentImage() => _currentImage?.Clone();

        public void DisposeImages()
        {
            _srcImage?.Dispose();
            _currentImage?.Dispose();
            ClearHistory(); // 스택의 이미지들도 해제
        }

        public void CropToRoi(Rectangle roi)
        {
            if (_currentImage == null || _currentImage.Empty())
            {
                Console.WriteLine("Current image is null or empty!");
                return;
            }
            // Undo 기록
            _undoHistory.Push(_currentImage.Clone());
            _redoHistory.Clear();
            Console.WriteLine($"CropToRoi 호출: 입력 ROI={roi}, 이미지 크기={_currentImage.Width}x{_currentImage.Height}");
            // ROI 보정 (이미지 범위 초과 방지)
            Rectangle safeRoi = new Rectangle(
                Math.Max(roi.X, 0),
                Math.Max(roi.Y, 0),
                Math.Min(roi.Width, _currentImage.Width - roi.X),
                Math.Min(roi.Height, _currentImage.Height - roi.Y)
            );
            Console.WriteLine($"보정된 ROI={safeRoi}");
            if (safeRoi.Width <= 0 || safeRoi.Height <= 0) return;

            using (var bmp = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_currentImage))
            {
                Bitmap croppedBmp = bmp.Clone(safeRoi, bmp.PixelFormat);
                _currentImage.Dispose();
                _currentImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(croppedBmp);
                croppedBmp.Dispose();
            }
            OnImageUpdated?.Invoke(_currentImage);
        }
    }
}
