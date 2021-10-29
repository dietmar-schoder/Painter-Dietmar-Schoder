using System;
using System.Drawing;

namespace Painter_Dietmar_Schoder.Tools
{
    public interface ICanvas
    {
        int Width { get; set; }
        int Height { get; set; }
        int EnlargeFactor { get; set; }
        public Bitmap InputBitmap { get; set; }
        public Bitmap Bitmap { get; set; }
        public DateTime SignedDateTime { get; set; }
        public Rectangle SignArea { get; set; }

        public string BackgroundImageFileName { get; set; }

        void FillWithBrush(Brush brush);

        void FillWithImage(string imageFileName);

        void SignDrawing(Color color1, Color color2);

        void Save();
    }
}
