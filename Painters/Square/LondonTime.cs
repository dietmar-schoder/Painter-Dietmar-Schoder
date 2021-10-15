using Painter_Dietmar_Schoder.Tools;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class LondonTime : IPainter
    {
        private readonly int _squareSize;
        private readonly int _fontSize;
        private readonly DateTime _now;
        private int _margin;

        public LondonTime(int squareSize, DateTime currentDateTime)
        {
            _squareSize = squareSize;
            _fontSize = squareSize / 2;
            _now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            int hour = 0, minute = 0, second = 0;
            _margin = (canvas.Width - 25 * _squareSize) / 2;
            for (int x = 0; x < 2; x++)
                for (int y = 0; y < 12; y++)
                {
                    DrawScale(canvas.Bitmap, _squareSize + x * _squareSize * 2, _squareSize + y * _squareSize * 2, hour, hour == _now.Hour);
                    hour++;
                }
            for (int x = 2; x < 7; x++)
                for (int y = 0; y < 12; y++)
                {
                    DrawScale(canvas.Bitmap, _squareSize + x * _squareSize * 2, _squareSize + y * _squareSize * 2, minute, minute == _now.Minute);
                    minute++;
                }
            for (int x = 7; x < 12; x++)
                for (int y = 0; y < 12; y++)
                {
                    DrawScale(canvas.Bitmap, _squareSize + x * _squareSize * 2, _squareSize + y * _squareSize * 2, second, second == _now.Second);
                    second++;
                }
        }

        private void DrawScale(Bitmap bitmap, int posX, int posY, int number, bool isCurrentTime)
        {
            var rect = new Rectangle(_margin + posX, _margin + posY, _squareSize, _squareSize);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

            using var gr = Graphics.FromImage(bitmap);
            if (isCurrentTime)
            {
                using var brush1 = new SolidBrush(Color.Black);
                gr.FillRectangle(brush1, rect);
            }
            else
            {
                using var pen = new Pen(isCurrentTime ? Color.Black : Color.FromArgb(255, 127, 127, 127), 1);
                gr.DrawRectangle(pen, rect);
            }
            using var brush = new SolidBrush(isCurrentTime ? Color.White : Color.Black);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.DrawString(number.ToString(), new Font("Malgun Gothic", _fontSize), brush, rect, sf);
        }
    }
}
