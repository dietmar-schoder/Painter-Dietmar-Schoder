﻿using Painter_Dietmar_Schoder.Tools;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class SquarePainter001 : IPainter
    {
        private readonly int _squareSizeFactor;

        public SquarePainter001(int squareSizeFactor)
        {
            _squareSizeFactor = squareSizeFactor;
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            float angle = 90;
            float angleDiff = (float)360 / 756 / 2;
            for (int y = 0; y < canvas.Height - 1; y += canvas.EnlargeFactor * _squareSizeFactor * 2)
                for (int x = 0; x < canvas.Width - 1; x += canvas.EnlargeFactor * _squareSizeFactor * 2)
                {
                    DrawSquarePixels(canvas.Bitmap, canvas.EnlargeFactor, x * canvas.EnlargeFactor, y * canvas.EnlargeFactor, angle += angleDiff); // _rnd.Next(0, 360)
                }
        }

        private void DrawSquarePixels(Bitmap bitmap, int enlargeFactor, int x, int y, float angle)
        {
            var squareSize = _squareSizeFactor * enlargeFactor * 2;

            using var gr = Graphics.FromImage(bitmap);

            using var brush = new LinearGradientBrush(new Rectangle(7 + x, 7 + y, squareSize + 7, squareSize), Color.FromArgb(255, 255, 255, 255), Color.Black, angle);
            gr.FillRectangle(
                brush,
                new Rectangle(7 + x, 7 + y, squareSize, squareSize));
        }
    }
}
