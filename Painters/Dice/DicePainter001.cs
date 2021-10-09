using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using Point = Painter_Dietmar_Schoder.Tools.Point;

namespace Painter_Dietmar_Schoder.Painters.Dice
{
    public class DicePainter001 : IPainter
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

        public DicePainter001(int diceSizeFactor)
        {
            _diceSizeFactor = diceSizeFactor;
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            for (int y = 0; y < canvas.Height; y++)
            {
                for (int x = 0; x < canvas.Width; x++)
                {
                    DrawDicePixels(canvas.Bitmap, x * canvas.EnlargeFactor, y * canvas.EnlargeFactor, _rnd.Next(0, 6));
                }
            }
        }

        private void DrawDicePixels(Bitmap bitmap, int x, int y, int value)
        {
            foreach (var point in GetDicePoints(value))
                bitmap.SetPixel(x + point.X * _diceSizeFactor, y + point.Y * _diceSizeFactor, Color.Black);
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
