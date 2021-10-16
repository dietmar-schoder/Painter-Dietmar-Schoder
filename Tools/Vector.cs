using System;

namespace Painter_Dietmar_Schoder.Tools
{
    public struct Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(int x1, int y1, int x2, int y2)
        {
            X = x2 - x1;
            Y = y2 - y1;
        }

        public Vector Rotated(float degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return new Vector((int)(Math.Cos(angle) * X - Math.Sin(angle) * Y), (int)(Math.Sin(angle) * X + Math.Cos(angle) * Y));
        }

        public Vector NewLength(float percent)
        {
            X = (int)(X * percent / 100);
            Y = (int)(Y * percent / 100);
            return this;
        }
    }
}
