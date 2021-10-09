using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Painter_Dietmar_Schoder.Tools
{
    public class Canvas : ICanvas
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int EnlargeFactor { get; set; }
        public Bitmap InputBitmap { get; set; }
        public Bitmap Bitmap { get; set; }

        private readonly string _pathAndFileName;

        public Canvas(string inputImagePathFilename, string path, string fileName, int width, int height, int enlargeFactor = 1)
            : this(path, fileName, width, height, enlargeFactor)
        {
            InputBitmap = new Bitmap(inputImagePathFilename);
        }

        public Canvas(string path, string fileName, int width, int height, int enlargeFactor = 1)
        {
            Width = width;
            Height = height;
            EnlargeFactor = enlargeFactor;
            Bitmap = new Bitmap(width * enlargeFactor, height * enlargeFactor);
            _pathAndFileName = path + fileName;
        }

        public void FillWithBrush(Brush brush)
        {
            using Graphics gr = Graphics.FromImage(Bitmap);
            gr.FillRectangle(brush, new RectangleF(0, 0, Bitmap.Width, Bitmap.Height));
        }

        public void SignDrawing(Color color1, Color color2)
        {
            var signedDate = DateTime.UtcNow.AddDays(+4);
            using Graphics gr = Graphics.FromImage(Bitmap);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            using var whiteBrush = new SolidBrush(color1);
            gr.DrawString
            (
                $"schoder {signedDate:yyyyMMddHHmmss}",
                new Font("Segoe UI", 14),
                whiteBrush,
                new PointF(Width * EnlargeFactor - 260 + 1, Height * EnlargeFactor - 60 + 1)
            );
            using var blackBrush = new SolidBrush(color2);
            gr.DrawString
            (
                $"schoder {signedDate:yyyyMMddHHmmss}",
                new Font("Segoe UI", 14),
                blackBrush,
                new PointF(Width * EnlargeFactor - 260, Height * EnlargeFactor - 60)
            );
        }

        public void Save() => Bitmap.Save(_pathAndFileName, _pathAndFileName.EndsWith(".png") ? ImageFormat.Png : ImageFormat.Jpeg);
    }
}
