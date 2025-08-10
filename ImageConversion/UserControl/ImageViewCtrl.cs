/*
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ImageConversion.BlobForm;

namespace ImageConversion
{
    [ToolboxItem(true)]
    public partial class ImageViewCtrl : UserControl
    {
        private bool _isInitialized = false;
        private Bitmap _bitmapImage = null;
        private Bitmap Canvas = null;
        private RectangleF ImageRect = new RectangleF(0, 0, 0, 0);
        private float _curZoom = 1.0f;
        private float _zoomFactor = 1.1f;
        private float MinZoom = 0.5f;
        private const float MaxZoom = 100.0f;
        private bool _isSelecting = false;
        private Point _startPoint;
        private Rectangle _roiRect = Rectangle.Empty;
        private Point? _measureLastPt1 = null;
        private Point? _measureLastPt2 = null;
        public Rectangle? SelectedRoi => _roiRect == Rectangle.Empty ? (Rectangle?)null : _roiRect;
        private bool _enableMeasureMode = false;
        private Point? _measureStartPoint = null;
        public event Action<Point, Point> MeasureLineSelected;

        private List<Rectangle> _rectOverlays = new List<Rectangle>();
        private List<PointF> _centroidOverlays = new List<PointF>();

        private int? selectedBlobIndex = null;
        private int? resizingHandleIndex = null; // 0~7(8개) 모서리/변
        private Point lastMousePoint;
        private List<BlobResult> _currentBlobs = null; // Blob 리스트 참조

        private bool _isDraggingBlob = false;
        private Point _dragStartPoint;
        private Rectangle _originalRect;
        private int? _resizeHandleIndex = null; // 어느 핸들에서 리사이즈 시작했는지
        private int? _hoveredBlobIndex = null; // 마우스 오버중인 Blob
        private int? _hoveredHandleIndex = null;

        private bool _isResizingBlob = false;      // Blob 박스 리사이즈 중
        private bool _isShiftSnap = false;
        public ImageViewCtrl()
        {
            InitializeComponent();
            InitializeCanvas();

            MouseWheel += new MouseEventHandler(ImageViewCCtrl_MouseWheel);

        }

        private void InitializeCanvas()
        {

            ResizeCanvas();
            DoubleBuffered = true;
        }

        public Bitmap GetCurBitmap()
        {
            return _bitmapImage;
        }
        private void ResizeCanvas()
        {
            //usercontrol에 있는 Width와 Height에 대한 정보가 있음
            if (Width <= 0 || Height <= 0 || _bitmapImage == null)
                return;

            Canvas = new Bitmap(Width, Height);
            if (Canvas == null)
                return;

            float virtualWidth = _bitmapImage.Width * _curZoom;
            float virtualHeight = _bitmapImage.Height * _curZoom;

            float offsetX = virtualWidth < Width ? (Width - virtualWidth) / 2f : 0f;
            float offsetY = virtualHeight < Height ? (Height - virtualHeight) / 2f : 0f;

            ImageRect = new RectangleF(offsetX, offsetY, virtualWidth, virtualHeight);
        }
        public void LoadBitmap(Bitmap bitmap)
        {

            if (_bitmapImage != null)
            {
                // 이미지 크기가 같다면, 이미지 변경 후, 화면 갱신
                if (_bitmapImage.Width == bitmap.Width && _bitmapImage.Height == bitmap.Height)
                {
                    _bitmapImage = bitmap;
                    Invalidate();
                    return;
                }

                _bitmapImage.Dispose(); // Bitmap 객체가 사용하던 메모리 리소스를 해제
                _bitmapImage = null;    // 객체를 해제하여 가비지 컬렉션(GC)이 수집할 수 있도록 설정
            }

            // 새로운 이미지 로드
            _bitmapImage = bitmap;


            if (_isInitialized == false)
            {
                _isInitialized = true;
            }

            ResizeCanvas();

            FitImageToScreen();

            Invalidate();
        }
        public void UpdateBitmap(Bitmap bitmap)
        {
            // dispose old?
            _bitmapImage?.Dispose();
            _bitmapImage = bitmap;

            // only recalc canvas size, do NOT call FitImageToScreen
            ResizeCanvas();
            Invalidate();
        }
        private void FitImageToScreen()
        {
            RecalcZoomRatio();

            float NewWidth = _bitmapImage.Width * _curZoom;
            float NewHeight = _bitmapImage.Height * _curZoom;

            // 이미지가 UserControl 중앙에 배치되도록 정렬
            ImageRect = new RectangleF(
                (Width - NewWidth) / 2, // UserControl 너비에서 이미지 너비를 뺀 후, 절반을 왼쪽 여백으로 설정하여 중앙 정렬
                (Height - NewHeight) / 2,
                NewWidth,
                NewHeight
            );

            Invalidate();
        }

        public void FitImageToScreenPublic()
        {
            FitImageToScreen();
        }

        private void RecalcZoomRatio()
        {
            if (_bitmapImage == null || Width <= 0 || Height <= 0)
                return;

            Size imageSize = new Size(_bitmapImage.Width, _bitmapImage.Height);

            float aspectRatio = (float)imageSize.Height / (float)imageSize.Width;
            float clientAspect = (float)Height / (float)Width;

            float ratio;
            if (aspectRatio <= clientAspect)
                ratio = (float)Width / (float)imageSize.Width;
            else
                ratio = (float)Height / (float)imageSize.Height;

            float minZoom = ratio;

            MinZoom = minZoom;

            _curZoom = Math.Max(MinZoom, Math.Min(MaxZoom, ratio));

            Invalidate();
        }
        public void ShowBlobs(List<BlobResult> blobs)
        {
            _rectOverlays.Clear();
            _centroidOverlays.Clear();
            foreach (var blob in blobs)
            {
                _rectOverlays.Add(blob.BoundingBox);
                _centroidOverlays.Add(blob.Centroid);
            }
            Invalidate();
        }
        public void ClearBlobs()
        {
            _rectOverlays.Clear();
            _centroidOverlays.Clear();
            Invalidate();
        }
        public void SetBlobs(List<BlobResult> blobs)
        {
            _currentBlobs = blobs;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_bitmapImage != null && Canvas != null)
            {

                using (Graphics g = Graphics.FromImage(Canvas))
                {
                    g.Clear(Color.Transparent); // 배경을 투명하게 설정
                    //이미지 확대or축소때 화질 최적화 방식(Interpolation Mode) 설정                    
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.DrawImage(_bitmapImage, ImageRect);

                    using (var pen = new Pen(Color.Red, 2))
                        foreach (var rect in _rectOverlays)
                        {
                            var screenRect = VirtualToScreen(rect);
                            g.DrawRectangle(pen, screenRect);

                        }
                    /*
                    using (var brush = new SolidBrush(Color.Lime))
                        foreach (var pt in _centroidOverlays)
                        {
                            var screenPt = VirtualToScreen(pt);
                            g.FillEllipse(brush, screenPt.X - 4, screenPt.Y - 4, 8, 8);
                        }*/
// 기존 ROI/측정/다른 오버레이 (그대로 유지)
/*    if (_roiRect != Rectangle.Empty)
        using (var pen = new Pen(Color.Red, 2))
            g.DrawRectangle(pen, _roiRect);

    if (_enableMeasureMode && _measureStartPoint != null)
    {
        using (var pen = new Pen(Color.Lime, 2))
        {
            g.DrawLine(
                pen,
                VirtualToScreen(_measureStartPoint.Value),
                this.PointToClient(Cursor.Position)
            );
        }
        var pt = VirtualToScreen(_measureStartPoint.Value);
        using (var brush = new SolidBrush(Color.Lime))
            g.FillEllipse(brush, pt.X - 5, pt.Y - 5, 10, 10);
    }
    if (_measureLastPt1 != null && _measureLastPt2 != null)
    {
        using (var pen = new Pen(Color.Orange, 2))
        {
            g.DrawLine(
                pen,
                VirtualToScreen(_measureLastPt1.Value),
                VirtualToScreen(_measureLastPt2.Value)
            );
        }
        var pt1 = VirtualToScreen(_measureLastPt1.Value);
        var pt2 = VirtualToScreen(_measureLastPt2.Value);
        using (var brush = new SolidBrush(Color.Red))
        {
            g.FillEllipse(brush, pt1.X - 5, pt1.Y - 5, 10, 10);
            g.FillEllipse(brush, pt2.X - 5, pt2.Y - 5, 10, 10);
        }
    }
    // OnPaint에서 각 Blob 박스 + 핸들 그리기

    if (_currentBlobs != null)
    {
        foreach (var blob in _currentBlobs)
        {
            var rect = VirtualToScreen(blob.BoundingBox);
            Color boxColor = Color.Blue; // 기본
            if (!string.IsNullOrEmpty(blob.Label))
            {
                var label = blob.Label.Trim().ToLower();
                if (label.Contains("ng") || label.Contains("불량"))
                    boxColor = Color.Red;
                else if (label.Contains("ok") || label.Contains("양품"))
                    boxColor = Color.Lime;
                // 필요시 더 추가

            }
            var pen = new Pen(boxColor, blob.Index == selectedBlobIndex ? 3 : 2);
            g.DrawRectangle(pen, rect);

            // 네 모서리에 작은 사각형(핸들)
            foreach (var handle in GetHandles(rect))
            {
                g.FillRectangle(Brushes.White, handle);
                g.DrawRectangle(Pens.Black, handle);
            }
            // 라벨 텍스트 표시 (blob.Label)
            DrawText(g, blob.Label ?? "", new PointF(rect.X, rect.Y - 18), 10f, Color.Yellow);
        }
    }
    // 캔버스를 UserControl 화면에 표시
    e.Graphics.DrawImage(Canvas, 0, 0);


}

}
if (_roiRect != Rectangle.Empty)
{
using (var pen = new Pen(Color.Red, 2))
{
    e.Graphics.DrawRectangle(pen, _roiRect);
}
}
if (_enableMeasureMode && _measureStartPoint != null)
{
using (var pen = new Pen(Color.Lime, 2))
{
    e.Graphics.DrawLine(
        pen,
        VirtualToScreen(_measureStartPoint.Value),
        this.PointToClient(Cursor.Position)
    );
}
var pt = VirtualToScreen(_measureStartPoint.Value);
using (var brush = new SolidBrush(Color.Lime))
    e.Graphics.FillEllipse(brush, pt.X - 5, pt.Y - 5, 10, 10);

}
if (_measureLastPt1 != null && _measureLastPt2 != null)
{
// 선
using (var pen = new Pen(Color.Orange, 2))
{
    e.Graphics.DrawLine(
        pen,
        VirtualToScreen(_measureLastPt1.Value),
        VirtualToScreen(_measureLastPt2.Value)
    );
}
// 점
var pt1 = VirtualToScreen(_measureLastPt1.Value);
var pt2 = VirtualToScreen(_measureLastPt2.Value);
using (var brush = new SolidBrush(Color.Red))
{
    e.Graphics.FillEllipse(brush, pt1.X - 5, pt1.Y - 5, 10, 10);
    e.Graphics.FillEllipse(brush, pt2.X - 5, pt2.Y - 5, 10, 10);
}
}

}


private void DrawText(Graphics g, string text, PointF position, float fontSize, Color color)
{
using (Font font = new Font("Arial", fontSize, FontStyle.Bold))
// 테두리용 검정색 브러시
using (Brush outlineBrush = new SolidBrush(Color.Black))
// 본문용 노란색 브러시
using (Brush textBrush = new SolidBrush(color))
{
// 테두리 효과를 위해 주변 8방향으로 그리기
for (int dx = -1; dx <= 1; dx++)
{
    for (int dy = -1; dy <= 1; dy++)
    {
        if (dx == 0 && dy == 0) continue; // 가운데는 제외
        PointF borderPos = new PointF(position.X + dx, position.Y + dy);
        g.DrawString(text, font, outlineBrush, borderPos);
    }
}

// 본문 텍스트
g.DrawString(text, font, textBrush, position);
}
}

private void ImageViewCCtrl_MouseWheel(object sender, MouseEventArgs e)
{
if (e.Delta < 0)
ZoomMove(_curZoom / _zoomFactor, e.Location);
else
ZoomMove(_curZoom * _zoomFactor, e.Location);

// 새로운 이미지 위치 반영 (점진적으로 초기 상태로 회귀)
if (_bitmapImage != null)
{
ImageRect.Width = _bitmapImage.Width * _curZoom;
ImageRect.Height = _bitmapImage.Height * _curZoom;
}

// 다시 그리기 요청
Invalidate();
}

//휠에 의해, Zoom 확대/축소 값 계산
private void ZoomMove(float zoom, Point zoomOrigin)
{
PointF virtualOrigin = ScreenToVirtual(new PointF(zoomOrigin.X, zoomOrigin.Y));

_curZoom = Math.Max(MinZoom, Math.Min(MaxZoom, zoom));
if (_curZoom <= MinZoom)
return;

PointF zoomedOrigin = VirtualToScreen(virtualOrigin);

float dx = zoomedOrigin.X - zoomOrigin.X;
float dy = zoomedOrigin.Y - zoomOrigin.Y;

ImageRect.X -= dx;
ImageRect.Y -= dy;
}


// Virtual <-> Screen 좌표계 변환
#region 좌표계 변환
private PointF GetScreenOffset()
{
return new PointF(ImageRect.X, ImageRect.Y);
}

private Rectangle ScreenToVirtual(Rectangle screenRect)
{
PointF offset = GetScreenOffset();
return new Rectangle(
(int)((screenRect.X - offset.X) / _curZoom + 0.5f),
(int)((screenRect.Y - offset.Y) / _curZoom + 0.5f),
(int)(screenRect.Width / _curZoom + 0.5f),
(int)(screenRect.Height / _curZoom + 0.5f));
}

private Rectangle VirtualToScreen(Rectangle virtualRect)
{
PointF offset = GetScreenOffset();
return new Rectangle(
(int)(virtualRect.X * _curZoom + offset.X + 0.5f),
(int)(virtualRect.Y * _curZoom + offset.Y + 0.5f),
(int)(virtualRect.Width * _curZoom + 0.5f),
(int)(virtualRect.Height * _curZoom + 0.5f));
}

private PointF ScreenToVirtual(PointF screenPos)
{
PointF offset = GetScreenOffset();
return new PointF(
(screenPos.X - offset.X) / _curZoom,
(screenPos.Y - offset.Y) / _curZoom);
}

private PointF VirtualToScreen(PointF virtualPos)
{
PointF offset = GetScreenOffset();
return new PointF(
virtualPos.X * _curZoom + offset.X,
virtualPos.Y * _curZoom + offset.Y);
}
#endregion

private void ImageViewCtrl_Resize(object sender, EventArgs e)
{
ResizeCanvas();
Invalidate();
}

private void ImageViewCtrl_MouseDoubleClick(object sender, MouseEventArgs e)
{
// 1. 먼저 Blob 박스 더블클릭 시 라벨 입력 처리
if (_currentBlobs != null)
{
for (int i = 0; i < _currentBlobs.Count; i++)
{
    var screenRect = VirtualToScreen(_currentBlobs[i].BoundingBox);
    if (screenRect.Contains(e.Location))
    {
        // 라벨 입력 팝업
        string label = Microsoft.VisualBasic.Interaction.InputBox(
            "Blob 라벨 입력", "Blob 라벨", _currentBlobs[i].Label ?? "");
        if (!string.IsNullOrWhiteSpace(label))
        {
            _currentBlobs[i].Label = label;
            Invalidate();
        }
        return; // Blob 위에서 더블클릭 했으면 FitImageToScreen()은 안 함
    }
}
}

FitImageToScreen();
}
private void ImageViewCtrl_MouseDown(object sender, MouseEventArgs e)
{

// 1. 먼저 측정 모드부터 체크!
if (_enableMeasureMode && _bitmapImage != null)
{
Point virtualPt = ScreenToVirtual(e.Location).ToPoint();

if (_measureStartPoint == null)
{
    _measureStartPoint = virtualPt;
    Invalidate();
}
else
{
    var pt1 = _measureStartPoint.Value;
    var pt2 = virtualPt;
    _measureLastPt1 = pt1;
    _measureLastPt2 = pt2;
    _measureStartPoint = null;
    _enableMeasureMode = false;
    this.Cursor = Cursors.Default;
    Invalidate();
    MeasureLineSelected?.Invoke(pt1, pt2);
}
return;
}

// 2. ROI 모드 체크 (이전 코드 그대로)
// ROI 모드
if (_enableRoiSelection && _bitmapImage != null)
{
_isSelecting = true;
_startPoint = e.Location;
_roiRect = new Rectangle(e.Location, new Size(0, 0));
Invalidate();
return;
}

// Blob 핸들(리사이즈) 체크
if (_currentBlobs != null)
{
Point mouse = e.Location;
for (int i = 0; i < _currentBlobs.Count; i++)
{
    var screenRect = VirtualToScreen(_currentBlobs[i].BoundingBox);

    var handles = GetHandles(screenRect);
    for (int h = 0; h < handles.Count; h++)
    {
        if (handles[h].Contains(mouse))
        {
            selectedBlobIndex = i;
            _resizeHandleIndex = h;
            _isResizingBlob = true;
            _isDraggingBlob = false;
            _dragStartPoint = mouse;
            _originalRect = screenRect;
            Cursor = Cursors.SizeAll;
            Invalidate();
            return;
        }
    }
}

// Blob 박스(이동)
for (int i = 0; i < _currentBlobs.Count; i++)
{
    var screenRect = VirtualToScreen(_currentBlobs[i].BoundingBox);
    if (screenRect.Contains(mouse))
    {
        selectedBlobIndex = i;
        _resizeHandleIndex = null;
        _isResizingBlob = false;
        _isDraggingBlob = true;
        _dragStartPoint = mouse;
        _originalRect = screenRect;
        Cursor = Cursors.SizeAll;
        Invalidate();
        return;
    }
}
}
selectedBlobIndex = null;
_resizeHandleIndex = null;
_isResizingBlob = false;
_isDraggingBlob = false;
Cursor = Cursors.Default;
Invalidate();
}

private void ImageViewCtrl_MouseMove(object sender, MouseEventArgs e)
{
// ROI 드래그
if (_enableRoiSelection && _isSelecting)
{
int x = Math.Min(_startPoint.X, e.X);
int y = Math.Min(_startPoint.Y, e.Y);
int w = Math.Abs(_startPoint.X - e.X);
int h = Math.Abs(_startPoint.Y - e.Y);
_roiRect = new Rectangle(x, y, w, h);
Invalidate();
return;
}

// Blob 리사이즈 중
if (_isResizingBlob && selectedBlobIndex != null)
{
int idx = selectedBlobIndex.Value;
var blob = _currentBlobs[idx];
Rectangle oldScreenRect = _originalRect;

int dx = e.X - _dragStartPoint.X;
int dy = e.Y - _dragStartPoint.Y;
Rectangle newScreenRect = oldScreenRect;

switch (_resizeHandleIndex)
{
    case 0: // 좌상단
        newScreenRect.X = _originalRect.X + dx;
        newScreenRect.Y = _originalRect.Y + dy;
        newScreenRect.Width = _originalRect.Width - dx;
        newScreenRect.Height = _originalRect.Height - dy;
        break;
    case 4:
        newScreenRect.Y = _originalRect.Y + dy;
        newScreenRect.Height = _originalRect.Height - dy;
        break;
    case 1: // 우상단
        newScreenRect.Y = _originalRect.Y + dy;
        newScreenRect.Width = _originalRect.Width + dx;
        newScreenRect.Height = _originalRect.Height - dy;
        break;
    case 5:
        newScreenRect.Width = _originalRect.Width + dx;
        break;
    case 2: // 우하단
        newScreenRect.Width = _originalRect.Width + dx;
        newScreenRect.Height = _originalRect.Height + dy;
        break;
    case 6:
        newScreenRect.Height = _originalRect.Height + dy;
        break;
    case 3: // 좌하단
        newScreenRect.X = _originalRect.X + dx;
        newScreenRect.Width = _originalRect.Width - dx;
        newScreenRect.Height = _originalRect.Height + dy;
        break;
    case 7:
        newScreenRect.X = _originalRect.X + dx;
        newScreenRect.Width = _originalRect.Width - dx;
        break;
}
newScreenRect.Width = Math.Max(10, newScreenRect.Width);
newScreenRect.Height = Math.Max(10, newScreenRect.Height);
Rectangle newVirtualRect = ScreenToVirtual(newScreenRect);
blob.BoundingBox = newVirtualRect;
Invalidate();
return;
}
// Blob 이동 중
if (_isDraggingBlob && selectedBlobIndex != null)
{
int idx = selectedBlobIndex.Value;
var blob = _currentBlobs[idx];
Rectangle oldScreenRect = _originalRect;
int dx = e.X - _dragStartPoint.X;
int dy = e.Y - _dragStartPoint.Y;
Rectangle newScreenRect = oldScreenRect;
newScreenRect.Offset(dx, dy);
Rectangle newVirtualRect = ScreenToVirtual(newScreenRect);
blob.BoundingBox = newVirtualRect;
Invalidate();
return;
}
if (_enableMeasureMode && _measureStartPoint != null)
{
// 쉬프트 키 체크
_isShiftSnap = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

// 마우스 현재 위치
var cur = ScreenToVirtual(e.Location).ToPoint();

if (_isShiftSnap)
{
    // 시작점 기준으로 x/y 방향 중 더 가까운 축으로 스냅
    var start = _measureStartPoint.Value;
    int dx = Math.Abs(cur.X - start.X);
    int dy = Math.Abs(cur.Y - start.Y);
    if (dx > dy)
        cur.Y = start.Y; // 수평
    else
        cur.X = start.X; // 수직
}

// 임시 끝점 저장 (실제 pt2에 반영은 MouseUp에서만)
_measureLastPt2 = cur;


Invalidate();
}


if (_enableMeasureMode && this.Cursor != Cursors.Cross)

this.Cursor = Cursors.Cross;

}
public event Action<Rectangle> RoiSelected;
private void ImageViewCtrl_MouseUp(object sender, MouseEventArgs e)
{
// ROI 드래그 종료
if (_enableRoiSelection && _isSelecting)
{
_isSelecting = false;
Invalidate();
RoiSelected?.Invoke(_roiRect); // ROI 선택 완료 이벤트
return;
}
_isDraggingBlob = false;
_isResizingBlob = false;
_resizeHandleIndex = null;
Cursor = Cursors.Default;

}
public Rectangle ScreenToVirtualRoi(Rectangle roi)
{

PointF topleft = ScreenToVirtual(new PointF(roi.Left, roi.Top));
PointF bottomright = ScreenToVirtual(new PointF(roi.Right, roi.Bottom));
Rectangle rect = new Rectangle(
(int)topleft.X,
(int)topleft.Y,
(int)(bottomright.X - topleft.X),
(int)(bottomright.Y - topleft.Y)
);
return rect;
}

private bool _enableRoiSelection = false;
public void EnableRoiSelection(bool enable)
{
_enableRoiSelection = enable;
this.Cursor = enable ? Cursors.Cross : Cursors.Default;
Invalidate();
}
public void ClearRoi()
{
_roiRect = Rectangle.Empty;
Invalidate();
}
public void EnableMeasureMode(bool enable)
{
_enableMeasureMode = enable;
_measureStartPoint = null;
this.Cursor = enable ? Cursors.Cross : Cursors.Default;
Invalidate();
}
public void ResetMeasureLine()
{
_measureLastPt1 = null;
_measureLastPt2 = null;
Invalidate();
}
// Blob 선택시
public void SelectBlob(int index)
{
if (index < 0 || _currentBlobs == null || index >= _currentBlobs.Count)
selectedBlobIndex = null;
else
selectedBlobIndex = index;
Invalidate();
}
private const int HANDLE_SIZE = 8; // 핸들 크기

// 네 모서리와 네 변의 중앙 (총 8개) 핸들 사각형 좌표 반환
private List<Rectangle> GetHandles(Rectangle rect)
{
var handles = new List<Rectangle>();

// 네 모서리
handles.Add(new Rectangle(rect.Left - HANDLE_SIZE / 2, rect.Top - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 좌상단
handles.Add(new Rectangle(rect.Right - HANDLE_SIZE / 2, rect.Top - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 우상단
handles.Add(new Rectangle(rect.Right - HANDLE_SIZE / 2, rect.Bottom - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 우하단
handles.Add(new Rectangle(rect.Left - HANDLE_SIZE / 2, rect.Bottom - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 좌하단

// 네 변의 중앙
handles.Add(new Rectangle(rect.Left + rect.Width / 2 - HANDLE_SIZE / 2, rect.Top - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 상중앙
handles.Add(new Rectangle(rect.Right - HANDLE_SIZE / 2, rect.Top + rect.Height / 2 - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 우중앙
handles.Add(new Rectangle(rect.Left + rect.Width / 2 - HANDLE_SIZE / 2, rect.Bottom - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 하중앙
handles.Add(new Rectangle(rect.Left - HANDLE_SIZE / 2, rect.Top + rect.Height / 2 - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 좌중앙

return handles;
}


protected override bool IsInputKey(Keys keyData)
{
// 키 입력을 직접 처리할 수 있도록
return true;
}

protected override void OnKeyDown(KeyEventArgs e)
{
base.OnKeyDown(e);
if (e.KeyCode == Keys.Delete && selectedBlobIndex != null && _currentBlobs != null)
{
_currentBlobs.RemoveAt(selectedBlobIndex.Value);
selectedBlobIndex = null;
Invalidate();
}
}
}
public static class PointFExtension
{
public static Point ToPoint(this PointF pf)
{
return new Point((int)Math.Round(pf.X), (int)Math.Round(pf.Y));
}
}
}
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ImageConversion.BlobForm;

namespace ImageConversion
{
    [ToolboxItem(true)]
    public partial class ImageViewCtrl : UserControl
    {
        // ====== 기본 상태 ======
        private bool _isInitialized = false;
        private Bitmap _bitmapImage = null;
        private Bitmap Canvas = null;
        private RectangleF ImageRect = new RectangleF(0, 0, 0, 0);
        private float _curZoom = 1.0f;
        private float _zoomFactor = 1.1f;
        private float MinZoom = 0.5f;
        private const float MaxZoom = 100.0f;

        // ====== ROI 상태 ======
        private bool _enableRoiSelection = false;  // 외부에서 on/off
        private Rectangle _roiRect = Rectangle.Empty; // 화면좌표 기준 ROI (핸들/이동/리사이즈 전용)
        public Rectangle? SelectedRoi => _roiRect == Rectangle.Empty ? (Rectangle?)null : _roiRect;
        public event Action<Rectangle> RoiSelected;

        private enum RoiEditMode { Idle, Drawing, Moving, Resizing }
        private RoiEditMode _roiMode = RoiEditMode.Idle;
        private Point _roiDragStart;
        private Rectangle _roiOriginalRect;
        private int _roiResizeHandle = -1; // 0~7
        private const int HANDLE_SIZE = 8;

        // ====== 측정 상태 ======
        private bool _enableMeasureMode = false;
        private Point? _measureStartPoint = null;
        private Point? _measureLastPt1 = null;
        private Point? _measureLastPt2 = null;
        private bool _isShiftSnap = false;
        public event Action<Point, Point> MeasureLineSelected;

        // ====== Blob 오버레이/편집 ======
        private List<Rectangle> _rectOverlays = new List<Rectangle>();
        private List<PointF> _centroidOverlays = new List<PointF>();
        private List<BlobResult> _currentBlobs = null;
        private int? selectedBlobIndex = null;
        private bool _isDraggingBlob = false;
        private bool _isResizingBlob = false;
        private Point _dragStartPoint;
        private Rectangle _originalRect;
        private int? _resizeHandleIndex = null;

        private bool _roiTempEnableForEdit = false;
        public ImageViewCtrl()
        {
            InitializeComponent();
            InitializeCanvas();
            MouseWheel += new MouseEventHandler(ImageViewCCtrl_MouseWheel);
        }

        private void InitializeCanvas()
        {
            ResizeCanvas();
            DoubleBuffered = true;
        }

        public Bitmap GetCurBitmap() => _bitmapImage;

        private void ResizeCanvas()
        {
            if (Width <= 0 || Height <= 0 || _bitmapImage == null) return;

            Canvas = new Bitmap(Width, Height);
            if (Canvas == null) return;

            float virtualWidth = _bitmapImage.Width * _curZoom;
            float virtualHeight = _bitmapImage.Height * _curZoom;

            float offsetX = virtualWidth < Width ? (Width - virtualWidth) / 2f : 0f;
            float offsetY = virtualHeight < Height ? (Height - virtualHeight) / 2f : 0f;

            ImageRect = new RectangleF(offsetX, offsetY, virtualWidth, virtualHeight);
        }

        public void LoadBitmap(Bitmap bitmap)
        {
            if (_bitmapImage != null)
            {
                if (_bitmapImage.Width == bitmap.Width && _bitmapImage.Height == bitmap.Height)
                {
                    _bitmapImage = bitmap;
                    Invalidate();
                    return;
                }
                _bitmapImage.Dispose();
                _bitmapImage = null;
            }

            _bitmapImage = bitmap;

            if (_isInitialized == false)
                _isInitialized = true;

            ResizeCanvas();
            FitImageToScreen();
            Invalidate();
        }

        public void UpdateBitmap(Bitmap bitmap)
        {
            _bitmapImage?.Dispose();
            _bitmapImage = bitmap;
            ResizeCanvas();
            Invalidate();
        }

        private void FitImageToScreen()
        {
            RecalcZoomRatio();

            float NewWidth = _bitmapImage.Width * _curZoom;
            float NewHeight = _bitmapImage.Height * _curZoom;

            ImageRect = new RectangleF(
                (Width - NewWidth) / 2,
                (Height - NewHeight) / 2,
                NewWidth,
                NewHeight
            );

            Invalidate();
        }

        public void FitImageToScreenPublic() => FitImageToScreen();

        private void RecalcZoomRatio()
        {
            if (_bitmapImage == null || Width <= 0 || Height <= 0) return;

            Size imageSize = new Size(_bitmapImage.Width, _bitmapImage.Height);
            float aspectRatio = (float)imageSize.Height / (float)imageSize.Width;
            float clientAspect = (float)Height / (float)Width;

            float ratio = (aspectRatio <= clientAspect)
                ? (float)Width / (float)imageSize.Width
                : (float)Height / (float)imageSize.Height;

            MinZoom = ratio;
            _curZoom = Math.Max(MinZoom, Math.Min(MaxZoom, ratio));
            Invalidate();
        }

        // ====== Blob 표시/연결 ======
        public void ShowBlobs(List<BlobResult> blobs)
        {
            _rectOverlays.Clear();
            _centroidOverlays.Clear();
            foreach (var blob in blobs)
            {
                _rectOverlays.Add(blob.BoundingBox);
                _centroidOverlays.Add(blob.Centroid);
            }
            Invalidate();
        }

        public void ClearBlobs()
        {
            _rectOverlays.Clear();
            _centroidOverlays.Clear();
            Invalidate();
        }

        public void SetBlobs(List<BlobResult> blobs)
        {
            _currentBlobs = blobs;
            Invalidate();
        }

        // ====== 그리기 ======
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_bitmapImage != null && Canvas != null)
            {
                using (Graphics g = Graphics.FromImage(Canvas))
                {
                    g.Clear(Color.Transparent);
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.DrawImage(_bitmapImage, ImageRect);

                    // Blob 단순 오버레이(빨간 박스)
                    using (var pen = new Pen(Color.Red, 2))
                    {
                        foreach (var rect in _rectOverlays)
                        {
                            var screenRect = VirtualToScreen(rect);
                            g.DrawRectangle(pen, screenRect);
                        }
                    }

                    // ROI 박스 + 핸들
                    if (_roiRect != Rectangle.Empty)
                    {
                        using (var pen = new Pen(Color.Magenta, 2))
                            g.DrawRectangle(pen, _roiRect);

                        foreach (var h in GetHandles(_roiRect))
                        {
                            g.FillRectangle(Brushes.White, h);
                            g.DrawRectangle(Pens.Black, h);
                        }
                    }
                  
                    // 측정 라인
                    if (_enableMeasureMode && _measureStartPoint != null)
                    {
                        using (var pen = new Pen(Color.Lime, 2))
                        {
                            g.DrawLine(
                                pen,
                                VirtualToScreen(_measureStartPoint.Value),
                                this.PointToClient(Cursor.Position)
                            );
                        }
                        var pt = VirtualToScreen(_measureStartPoint.Value);
                        using (var brush = new SolidBrush(Color.Lime))
                            g.FillEllipse(brush, pt.X - 5, pt.Y - 5, 10, 10);
                    }
                    if (_measureLastPt1 != null && _measureLastPt2 != null)
                    {
                        using (var pen = new Pen(Color.Orange, 2))
                        {
                            g.DrawLine(
                                pen,
                                VirtualToScreen(_measureLastPt1.Value),
                                VirtualToScreen(_measureLastPt2.Value)
                            );
                        }
                        var pt1 = VirtualToScreen(_measureLastPt1.Value);
                        var pt2 = VirtualToScreen(_measureLastPt2.Value);
                        using (var brush = new SolidBrush(Color.Red))
                        {
                            g.FillEllipse(brush, pt1.X - 5, pt1.Y - 5, 10, 10);
                            g.FillEllipse(brush, pt2.X - 5, pt2.Y - 5, 10, 10);
                        }
                    }

                    // Blob 편집 박스 + 핸들 + 라벨
                    if (_currentBlobs != null)
                    {
                        foreach (var blob in _currentBlobs)
                        {
                            var rect = VirtualToScreen(blob.BoundingBox);
                            Color boxColor = Color.Blue;
                            if (!string.IsNullOrEmpty(blob.Label))
                            {
                                var label = blob.Label.Trim().ToLower();
                                if (label.Contains("ng") || label.Contains("불량")) boxColor = Color.Red;
                                else if (label.Contains("ok") || label.Contains("양품")) boxColor = Color.Lime;
                            }
                            using (var pen = new Pen(boxColor, blob.Index == selectedBlobIndex ? 3 : 2))
                                g.DrawRectangle(pen, rect);

                            foreach (var handle in GetHandles(rect))
                            {
                                g.FillRectangle(Brushes.White, handle);
                                g.DrawRectangle(Pens.Black, handle);
                            }
                            DrawText(g, blob.Label ?? "", new PointF(rect.X, rect.Y - 18), 10f, Color.Yellow);
                        }
                    }

                    e.Graphics.DrawImage(Canvas, 0, 0);
                }
            }
        }

        private void DrawText(Graphics g, string text, PointF position, float fontSize, Color color)
        {
            using (Font font = new Font("Arial", fontSize, FontStyle.Bold))
            using (Brush outlineBrush = new SolidBrush(Color.Black))
            using (Brush textBrush = new SolidBrush(color))
            {
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;
                        PointF borderPos = new PointF(position.X + dx, position.Y + dy);
                        g.DrawString(text, font, outlineBrush, borderPos);
                    }
                g.DrawString(text, font, textBrush, position);
            }
        }

        // ====== 줌 ======
        private void ImageViewCCtrl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0) ZoomMove(_curZoom / _zoomFactor, e.Location);
            else ZoomMove(_curZoom * _zoomFactor, e.Location);

            if (_bitmapImage != null)
            {
                ImageRect.Width = _bitmapImage.Width * _curZoom;
                ImageRect.Height = _bitmapImage.Height * _curZoom;
            }
            Invalidate();
        }

        private void ZoomMove(float zoom, Point zoomOrigin)
        {
            PointF virtualOrigin = ScreenToVirtual(new PointF(zoomOrigin.X, zoomOrigin.Y));
            _curZoom = Math.Max(MinZoom, Math.Min(MaxZoom, zoom));
            if (_curZoom <= MinZoom) return;

            PointF zoomedOrigin = VirtualToScreen(virtualOrigin);
            float dx = zoomedOrigin.X - zoomOrigin.X;
            float dy = zoomedOrigin.Y - zoomOrigin.Y;
            ImageRect.X -= dx;
            ImageRect.Y -= dy;
        }

        // ====== 좌표 변환 ======
        private PointF GetScreenOffset() => new PointF(ImageRect.X, ImageRect.Y);

        private Rectangle ScreenToVirtual(Rectangle screenRect)
        {
            PointF offset = GetScreenOffset();
            return new Rectangle(
                (int)((screenRect.X - offset.X) / _curZoom + 0.5f),
                (int)((screenRect.Y - offset.Y) / _curZoom + 0.5f),
                (int)(screenRect.Width / _curZoom + 0.5f),
                (int)(screenRect.Height / _curZoom + 0.5f));
        }

        private Rectangle VirtualToScreen(Rectangle virtualRect)
        {
            PointF offset = GetScreenOffset();
            return new Rectangle(
                (int)(virtualRect.X * _curZoom + offset.X + 0.5f),
                (int)(virtualRect.Y * _curZoom + offset.Y + 0.5f),
                (int)(virtualRect.Width * _curZoom + 0.5f),
                (int)(virtualRect.Height * _curZoom + 0.5f));
        }

        private PointF ScreenToVirtual(PointF screenPos)
        {
            PointF offset = GetScreenOffset();
            return new PointF(
                (screenPos.X - offset.X) / _curZoom,
                (screenPos.Y - offset.Y) / _curZoom);
        }

        private PointF VirtualToScreen(PointF virtualPos)
        {
            PointF offset = GetScreenOffset();
            return new PointF(
                virtualPos.X * _curZoom + offset.X,
                virtualPos.Y * _curZoom + offset.Y);
        }

        public Rectangle ScreenToVirtualRoi(Rectangle roi)
        {
            PointF topleft = ScreenToVirtual(new PointF(roi.Left, roi.Top));
            PointF bottomright = ScreenToVirtual(new PointF(roi.Right, roi.Bottom));
            return new Rectangle(
                (int)topleft.X,
                (int)topleft.Y,
                (int)(bottomright.X - topleft.X),
                (int)(bottomright.Y - topleft.Y)
            );
        }

        // ====== 리사이즈/핸들 유틸 ======
        private List<Rectangle> GetHandles(Rectangle rect)
        {
            var handles = new List<Rectangle>();

            // 코너(0~3): TL, TR, BR, BL
            handles.Add(new Rectangle(rect.Left - HANDLE_SIZE / 2, rect.Top - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 0 TL
            handles.Add(new Rectangle(rect.Right - HANDLE_SIZE / 2, rect.Top - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 1 TR
            handles.Add(new Rectangle(rect.Right - HANDLE_SIZE / 2, rect.Bottom - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 2 BR
            handles.Add(new Rectangle(rect.Left - HANDLE_SIZE / 2, rect.Bottom - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 3 BL

            // 엣지(4~7): Top, Right, Bottom, Left
            handles.Add(new Rectangle(rect.Left + rect.Width / 2 - HANDLE_SIZE / 2, rect.Top - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 4 Top
            handles.Add(new Rectangle(rect.Right - HANDLE_SIZE / 2, rect.Top + rect.Height / 2 - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 5 Right
            handles.Add(new Rectangle(rect.Left + rect.Width / 2 - HANDLE_SIZE / 2, rect.Bottom - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 6 Bottom
            handles.Add(new Rectangle(rect.Left - HANDLE_SIZE / 2, rect.Top + rect.Height / 2 - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE)); // 7 Left

            return handles;
        }

        private int HitTestHandle(Point p, Rectangle rect)
        {
            var hs = GetHandles(rect);
            for (int i = 0; i < hs.Count; i++)
                if (hs[i].Contains(p)) return i;
            return -1;
        }

        private static Cursor GetCursorForHandle(int i)
        {
            switch (i)
            {
                case 0: return Cursors.SizeNWSE; // TL
                case 1: return Cursors.SizeNESW; // TR
                case 2: return Cursors.SizeNWSE; // BR
                case 3: return Cursors.SizeNESW; // BL
                case 4: return Cursors.SizeNS;   // Top
                case 5: return Cursors.SizeWE;   // Right
                case 6: return Cursors.SizeNS;   // Bottom
                case 7: return Cursors.SizeWE;   // Left
                default: return Cursors.Default;
            }
        }

        private static Rectangle ResizeByHandle(Rectangle o, int h, int dx, int dy, int minW = 4, int minH = 4)
        {
            int x = o.X, y = o.Y, w = o.Width, hgt = o.Height;
            switch (h)
            {
                case 0: x += dx; y += dy; w -= dx; hgt -= dy; break; // TL
                case 1: y += dy; w += dx; hgt -= dy; break; // TR
                case 2: w += dx; hgt += dy; break; // BR
                case 3: x += dx; w -= dx; hgt += dy; break; // BL
                case 4: y += dy; hgt -= dy; break; // Top
                case 5: w += dx; break; // Right
                case 6: hgt += dy; break; // Bottom
                case 7: x += dx; w -= dx; break; // Left
            }
            if (w < minW) { if (h == 0 || h == 3 || h == 7) x = o.Right - minW; w = minW; }
            if (hgt < minH) { if (h == 0 || h == 1 || h == 4) y = o.Bottom - minH; hgt = minH; }
            return new Rectangle(x, y, w, hgt);
        }

        private Rectangle ClampToClient(Rectangle r)
        {
            int x = Math.Max(0, r.X);
            int y = Math.Max(0, r.Y);
            int right = Math.Min(this.ClientSize.Width, r.Right);
            int bottom = Math.Min(this.ClientSize.Height, r.Bottom);
            return Rectangle.FromLTRB(x, y, Math.Max(x + 1, right), Math.Max(y + 1, bottom));
        }

        // ====== 마우스/키 입력 ======
        private void ImageViewCtrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Blob 라벨 편집
            if (_currentBlobs != null)
            {
                for (int i = 0; i < _currentBlobs.Count; i++)
                {
                    var screenRect = VirtualToScreen(_currentBlobs[i].BoundingBox);
                    if (screenRect.Contains(e.Location))
                    {
                        string label = Microsoft.VisualBasic.Interaction.InputBox(
                            "Blob 라벨 입력", "Blob 라벨", _currentBlobs[i].Label ?? "");
                        if (!string.IsNullOrWhiteSpace(label))
                        {
                            _currentBlobs[i].Label = label;
                            Invalidate();
                        }
                        return;
                    }
                }
            }
            FitImageToScreen();
        }

        private void ImageViewCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            // 1) 측정 모드
            if (_enableMeasureMode && _bitmapImage != null)
            {
                Point virtualPt = ScreenToVirtual(e.Location).ToPoint();
                if (_measureStartPoint == null)
                {
                    _measureStartPoint = virtualPt;
                    Invalidate();
                }
                else
                {
                    var pt1 = _measureStartPoint.Value;
                    var pt2 = virtualPt;
                    _measureLastPt1 = pt1;
                    _measureLastPt2 = pt2;
                    _measureStartPoint = null;
                    _enableMeasureMode = false;
                    this.Cursor = Cursors.Default;
                    Invalidate();
                    MeasureLineSelected?.Invoke(pt1, pt2);
                }
                return;
            }
            // --- ROI 임시 편집 진입: ROI 모드가 꺼져 있어도, 기존 ROI 위를 클릭하면 편집 가능 ---
            if (!_enableRoiSelection && _bitmapImage != null && e.Button == MouseButtons.Left && _roiRect != Rectangle.Empty)
            {
                // 핸들 위면 리사이즈, 내부면 이동
                int h = HitTestHandle(e.Location, _roiRect);
                if (h >= 0)
                {
                    _roiTempEnableForEdit = true;
                    _enableRoiSelection = true;           // 임시로 ON
                    _roiMode = RoiEditMode.Resizing;
                    _roiResizeHandle = h;
                    _roiDragStart = e.Location;
                    _roiOriginalRect = _roiRect;
                    Capture = true;
                    return;
                }
                if (_roiRect.Contains(e.Location))
                {
                    _roiTempEnableForEdit = true;
                    _enableRoiSelection = true;           // 임시로 ON
                    _roiMode = RoiEditMode.Moving;
                    _roiDragStart = e.Location;
                    _roiOriginalRect = _roiRect;
                    Capture = true;
                    return;
                }
            }

            // 2) ROI 모드(그리기/선택/편집)
            if (_enableRoiSelection && _bitmapImage != null && e.Button == MouseButtons.Left)
            {
                if (_roiRect != Rectangle.Empty)
                {
                    int h = HitTestHandle(e.Location, _roiRect);
                    if (h >= 0)
                    {
                        _roiMode = RoiEditMode.Resizing;
                        _roiResizeHandle = h;
                        _roiDragStart = e.Location;
                        _roiOriginalRect = _roiRect;
                        Capture = true;
                        return;
                    }
                    if (_roiRect.Contains(e.Location))
                    {
                        _roiMode = RoiEditMode.Moving;
                        _roiDragStart = e.Location;
                        _roiOriginalRect = _roiRect;
                        Capture = true;
                        return;
                    }
                }
               
                // 새 ROI 그리기
                _roiMode = RoiEditMode.Drawing;
                _roiDragStart = e.Location;
                _roiRect = new Rectangle(e.Location, Size.Empty);
                Capture = true;
                Invalidate();
                return;
            }

            // 3) Blob 핸들/이동
            if (_currentBlobs != null)
            {
                Point mouse = e.Location;
                // 핸들 먼저
                for (int i = 0; i < _currentBlobs.Count; i++)
                {
                    var screenRect = VirtualToScreen(_currentBlobs[i].BoundingBox);
                    var handles = GetHandles(screenRect);
                    for (int h = 0; h < handles.Count; h++)
                    {
                        if (handles[h].Contains(mouse))
                        {
                            selectedBlobIndex = i;
                            _resizeHandleIndex = h;
                            _isResizingBlob = true;
                            _isDraggingBlob = false;
                            _dragStartPoint = mouse;
                            _originalRect = screenRect;
                            Cursor = GetCursorForHandle(h);
                            Invalidate();
                            return;
                        }
                    }
                }
                // 박스 내부 이동
                for (int i = 0; i < _currentBlobs.Count; i++)
                {
                    var screenRect = VirtualToScreen(_currentBlobs[i].BoundingBox);
                    if (screenRect.Contains(mouse))
                    {
                        selectedBlobIndex = i;
                        _resizeHandleIndex = null;
                        _isResizingBlob = false;
                        _isDraggingBlob = true;
                        _dragStartPoint = mouse;
                        _originalRect = screenRect;
                        Cursor = Cursors.SizeAll;
                        Invalidate();
                        return;
                    }
                }
            }

            selectedBlobIndex = null;
            _resizeHandleIndex = null;
            _isResizingBlob = false;
            _isDraggingBlob = false;
            Cursor = Cursors.Default;
            Invalidate();
        }

        private void ImageViewCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            // 커서 모양(ROI 핸들 위)
            if (_enableRoiSelection && _roiMode == RoiEditMode.Idle && _roiRect != Rectangle.Empty)
                this.Cursor = GetCursorForHandle(HitTestHandle(e.Location, _roiRect));

            // ROI 편집
            if (_enableRoiSelection)
            {
                if (_roiMode == RoiEditMode.Drawing)
                {
                    _roiRect = MakeRectFromPoints(_roiDragStart, e.Location);
                    Invalidate();
                    return;
                }
                else if (_roiMode == RoiEditMode.Moving)
                {
                    int dx = e.X - _roiDragStart.X;
                    int dy = e.Y - _roiDragStart.Y;
                    _roiRect = new Rectangle(_roiOriginalRect.X + dx, _roiOriginalRect.Y + dy, _roiOriginalRect.Width, _roiOriginalRect.Height);
                    _roiRect = ClampToClient(_roiRect);
                    Invalidate();
                    return;
                }
                else if (_roiMode == RoiEditMode.Resizing)
                {
                    int dx = e.X - _roiDragStart.X;
                    int dy = e.Y - _roiDragStart.Y;
                    _roiRect = ResizeByHandle(_roiOriginalRect, _roiResizeHandle, dx, dy, 4, 4);
                    _roiRect = ClampToClient(_roiRect);
                    Invalidate();
                    return;
                }
            }
            if (!_enableRoiSelection && _roiRect != Rectangle.Empty && !_enableMeasureMode)
            {
                int h = HitTestHandle(e.Location, _roiRect);
                if (h >= 0) this.Cursor = GetCursorForHandle(h);
                else if (_roiRect.Contains(e.Location)) this.Cursor = Cursors.SizeAll;
                else this.Cursor = Cursors.Default;
            }
            // Blob 리사이즈
            if (_isResizingBlob && selectedBlobIndex != null)
            {
                int idx = selectedBlobIndex.Value;
                var blob = _currentBlobs[idx];
                int dx = e.X - _dragStartPoint.X;
                int dy = e.Y - _dragStartPoint.Y;
                Rectangle newScreenRect = ResizeByHandle(_originalRect, _resizeHandleIndex ?? -1, dx, dy, 10, 10);
                Rectangle newVirtualRect = ScreenToVirtual(newScreenRect);
                blob.BoundingBox = newVirtualRect;
                Invalidate();
                return;
            }

            // Blob 이동
            if (_isDraggingBlob && selectedBlobIndex != null)
            {
                int idx = selectedBlobIndex.Value;
                var blob = _currentBlobs[idx];
                int dx = e.X - _dragStartPoint.X;
                int dy = e.Y - _dragStartPoint.Y;
                Rectangle newScreenRect = _originalRect;
                newScreenRect.Offset(dx, dy);
                Rectangle newVirtualRect = ScreenToVirtual(newScreenRect);
                blob.BoundingBox = newVirtualRect;
                Invalidate();
                return;
            }

            // 측정 미리보기
            if (_enableMeasureMode && _measureStartPoint != null)
            {
                _isShiftSnap = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                var cur = ScreenToVirtual(e.Location).ToPoint();
                if (_isShiftSnap)
                {
                    var start = _measureStartPoint.Value;
                    int dx = Math.Abs(cur.X - start.X);
                    int dy = Math.Abs(cur.Y - start.Y);
                    if (dx > dy) cur.Y = start.Y; else cur.X = start.X;
                }
                _measureLastPt2 = cur;
                Invalidate();
            }

            if (_enableMeasureMode && this.Cursor != Cursors.Cross)
                this.Cursor = Cursors.Cross;
        }

        private void ImageViewCtrl_MouseUp(object sender, MouseEventArgs e)
        {
            // ROI 종료 처리
            if (_enableRoiSelection && (_roiMode == RoiEditMode.Drawing || _roiMode == RoiEditMode.Moving || _roiMode == RoiEditMode.Resizing))
            {
                _roiMode = RoiEditMode.Idle;
                Capture = false;
                Invalidate();

                if (_roiRect.Width > 0 && _roiRect.Height > 0)
                    RoiSelected?.Invoke(_roiRect); // 화면좌표 전달(외부에서 ScreenToVirtualRoi 사용)
                                                   // ★ 임시 편집으로 들어온 경우, 자동으로 ROI 모드 OFF
                if (_roiTempEnableForEdit)
                {
                    _roiTempEnableForEdit = false;
                    EnableRoiSelection(false);   // ROI는 지우지 않고, 편집 모드만 종료
                }
                return;
            }

            _isDraggingBlob = false;
            _isResizingBlob = false;
            _resizeHandleIndex = null;
            Cursor = Cursors.Default;
        }

        private Rectangle MakeRectFromPoints(Point a, Point b)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int w = Math.Abs(a.X - b.X);
            int h = Math.Abs(a.Y - b.Y);
            return new Rectangle(x, y, w, h);
        }

        public void EnableRoiSelection(bool enable)
        {
            _enableRoiSelection = enable;
            if (!enable)
            {
                _roiMode = RoiEditMode.Idle;
                _roiResizeHandle = -1;
                Capture = false;
                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.Cross;
            }
            Invalidate();
        }

        public void ClearRoi()
        {
            _roiRect = Rectangle.Empty;
            _roiMode = RoiEditMode.Idle;
            _roiResizeHandle = -1;
            Invalidate();
        }

        public void EnableMeasureMode(bool enable)
        {
            _enableMeasureMode = enable;
            _measureStartPoint = null;
            this.Cursor = enable ? Cursors.Cross : Cursors.Default;
            Invalidate();
        }

        public void ResetMeasureLine()
        {
            _measureLastPt1 = null;
            _measureLastPt2 = null;
            Invalidate();
        }

        public void SelectBlob(int index)
        {
            if (index < 0 || _currentBlobs == null || index >= _currentBlobs.Count)
                selectedBlobIndex = null;
            else
                selectedBlobIndex = index;
            Invalidate();
        }

        protected override bool IsInputKey(Keys keyData) => true;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Delete && selectedBlobIndex != null && _currentBlobs != null)
            {
                _currentBlobs.RemoveAt(selectedBlobIndex.Value);
                selectedBlobIndex = null;
                Invalidate();
            }
        }

        private void ImageViewCtrl_Resize(object sender, EventArgs e)
        {
            ResizeCanvas();
            Invalidate();
        }
    }

    public static class PointFExtension
    {
        public static Point ToPoint(this PointF pf)
            => new Point((int)Math.Round(pf.X), (int)Math.Round(pf.Y));
    }
}