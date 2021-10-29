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
        public DateTime SignedDateTime { get; set; }
        public Rectangle SignArea { get; set; }

        public string BackgroundImageFileName { get; set; }

        private readonly string _pathAndFileName;

        public Canvas(string inputImagePathFilename, string path, string fileName, int width, int height, int enlargeFactor, DateTime signedDateTime, Rectangle signArea)
            : this(path, fileName, width, height, enlargeFactor, signedDateTime, signArea)
        {
            InputBitmap = new Bitmap(inputImagePathFilename);
        }

        public Canvas(string path, string fileName, int width, int height, int enlargeFactor, DateTime signedDateTime, Rectangle signArea)
        {
            Width = width * enlargeFactor;
            Height = height * enlargeFactor;
            EnlargeFactor = enlargeFactor;
            Bitmap = new Bitmap(width * enlargeFactor, height * enlargeFactor);
            SignedDateTime = signedDateTime;
            SignArea = new Rectangle(signArea.X * enlargeFactor, signArea.Y * enlargeFactor, signArea.Width * enlargeFactor, signArea.Height * enlargeFactor);
            _pathAndFileName = path + fileName;
        }

        public void FillWithBrush(Brush brush)
        {
            using Graphics gr = Graphics.FromImage(Bitmap);
            gr.FillRectangle(brush, new RectangleF(0, 0, Bitmap.Width, Bitmap.Height));
        }

        public void FillWithImage(string imageFileName)
        {
            using Graphics gr = Graphics.FromImage(Bitmap);
            gr.DrawImage(Image.FromFile(imageFileName), new RectangleF(0, 0, Bitmap.Width, Bitmap.Height));
        }

        public void SignDrawing(Color color1, Color color2)
        {
            var sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
            var signArea2 = new Rectangle(SignArea.X - 1, SignArea.Y + 1, SignArea.Width, SignArea.Height);
            using Graphics gr = Graphics.FromImage(Bitmap);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            using var brush1 = new SolidBrush(color1);
            gr.DrawString
            (
                $"schoder.uk {SignedDateTime:yyyyMMddHHmmss}",
                new Font("Segoe UI", 14), brush1, SignArea, sf
            );
            using var brush2 = new SolidBrush(color2);
            gr.DrawString
            (
                $"schoder.uk {SignedDateTime:yyyyMMddHHmmss}",
                new Font("Segoe UI", 14), brush2, signArea2, sf
            );
        }

        public void Save() => Bitmap.Save(_pathAndFileName, _pathAndFileName.EndsWith(".png") ? ImageFormat.Png : ImageFormat.Jpeg);
    }
}
