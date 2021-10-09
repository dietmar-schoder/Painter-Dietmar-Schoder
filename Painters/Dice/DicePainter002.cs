using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Point = Painter_Dietmar_Schoder.Tools.Point;

namespace Painter_Dietmar_Schoder.Painters.Dice
{
    public class DicePainter002 : IPainter
    {
        private readonly Random _rnd = new Random();
        private readonly List<int>[] _dicePointsX = new List<int>[]
        {
                new List<int> { 2 },
                new List<int> { 1, 3 },
                new List<int> { 1, 2, 3 },
                new List<int> { 1, 3, 1, 3 },
                new List<int> { 1, 3, 2, 1, 3 },
                new List<int> { 1, 1, 1, 3, 3, 3 },
        };
        private readonly List<int>[] _dicePointsY = new List<int>[]
        {
                new List<int> { 2 },
                new List<int> { 1, 3 },
                new List<int> { 1, 2, 3 },
                new List<int> { 1, 1, 3, 3 },
                new List<int> { 1, 1, 2, 3, 3 },
                new List<int> { 1, 2, 3, 1, 2, 3 },
        };
        private readonly int _diceSizeFactor;
        private readonly int _diceSizeFactorHalf;

        public DicePainter002(int diceSizeFactor)
        {
            _diceSizeFactor = diceSizeFactor;
            _diceSizeFactorHalf = _diceSizeFactor / 2;
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            for (int y = 0; y < canvas.Height - 1; y += canvas.EnlargeFactor * _diceSizeFactor * 2)
            {
                for (int x = 0; x < canvas.Width - 1; x += canvas.EnlargeFactor * _diceSizeFactor * 2)
                {
                    DrawDicePixels(canvas.Bitmap, canvas.EnlargeFactor, x * canvas.EnlargeFactor, y * canvas.EnlargeFactor, _rnd.Next(0, 6));
                }
            }
        }

        private void DrawDicePixels(Bitmap bitmap, int enlargeFactor, int x, int y, int value)
        {
            using var gr = Graphics.FromImage(bitmap);
            gr.SmoothingMode = SmoothingMode.AntiAlias;

            using var brush = new SolidBrush(Color.Black);
            foreach (var point in GetDicePoints(value))
            {
                gr.FillEllipse(
                    brush,
                    new Rectangle(new System.Drawing.Point(3 + x + point.X * _diceSizeFactor, 3 + y + point.Y * _diceSizeFactor),
                    new Size(_diceSizeFactorHalf, _diceSizeFactorHalf)));
            }

            using var pen = new Pen(Color.Black, 1);
            gr.DrawRectangle(
                pen,
                new Rectangle(3 + x + _diceSizeFactorHalf / 2, 3 + y + _diceSizeFactorHalf / 2,
                    _diceSizeFactor * enlargeFactor * 2, _diceSizeFactor * enlargeFactor * 2));
        }

        private Point[] GetDicePoints(int value)
        {
            var dicePoints = new Point[value + 1];
            for (int i = 0; i < _dicePointsX[value].Count; i++)
            {
                dicePoints[i] = new Point(_dicePointsX[value][i], _dicePointsY[value][i]);
            }
            return dicePoints;
        }
    }
}
