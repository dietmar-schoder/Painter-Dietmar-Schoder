using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreeInputPainter003 : IPainter
    {
        private Bitmap _inputBitmap;
        private ICanvas _canvas;
        private List<PointF> _pixelPath = new List<PointF>();
        private readonly Random _rnd = new Random();
        private Bitmap _referenceBitmap;
        private int _densityHalf;

        //private int[] _branchAngles = new int[] { 0, 180, 90, 270 };
        //private int[] _branchAngles = new int[] { 0, 90, 180, 270 };
        //private int[] _branchAngles = new int[] { 45, 135, 225, 315 };
        //private int[] _branchAngles = new int[] { 45, 135, 225 };
        //private int[] _branchAngles = new int[] { 0, 90, 270, 45, 315, 135, 225 };
        // private int[] _branchAngles = new int[] { 0, 27, 60, 89, 120, 152, 180, 212, 240, 273, 300, 330 };
        private int[] _branchAngles = new int[] { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 };

        //private int[] _branchAngles = new int[] { 0, 50, 100, 150, 200, 250, 300, 350 };

        private int _margin = 0;
        private int _branchLength = 5;
        private int _maxDepth = 100;
        private int _density = 4;
        private int _tolerance = 15;
        private bool _bezier = true;
        private bool _curves = true;
        private bool _paintDots = true;

        public TreeInputPainter003()
        {
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            _inputBitmap = canvas.InputBitmap;
            _densityHalf = _density / 2;
            _referenceBitmap = new Bitmap(canvas.Width, canvas.Height);
            var root = new Node2(canvas.Width / 2, canvas.Height / 2 + _branchLength, new Vector(0, -_branchLength));
            root.Color = _inputBitmap.GetPixel(root.X, root.Y);

            for (int i = 0; i < _maxDepth; i++)
            {
                CreateChildren(root, i);
            }

            if (_bezier)
            {
                DrawTree2(root);
                using var gr = Graphics.FromImage(_canvas.Bitmap);
                gr.SmoothingMode = SmoothingMode.HighQuality;
                using var pen = new Pen(Color.Gray, 1);
                // gr.DrawCurve(pen, _pixelPath.ToArray());
                if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
                if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
                gr.DrawBeziers(pen, _pixelPath.ToArray());
                if (_paintDots) PaintDots(root);
            }
            else
            {
                DrawTree(root);
            }
        }

        private void CreateChildren(Node2 node, int currentDepth)
        {
            if (node.Children.Count == 0)
            {
                for (int i = 0; i < _branchAngles.Length - 1; i++)
                {
                    var angleIndex = _rnd.Next(0, _branchAngles.Length);
                    // var angleIndex = _branchAngles[i];
                    if (!TryGetNewChild(node, _branchAngles[angleIndex], out var newChild)) { continue; }
                    newChild.Color = _inputBitmap.GetPixel(newChild.X, newChild.Y);
                    SetAreaToCovered(node.X, node.Y, newChild.X, newChild.Y);
                }
            }
            if (node.Level < currentDepth)
            {
                foreach (var child in node.Children)
                {
                    CreateChildren(child, currentDepth);
                }
            }
        }

        private bool TryGetNewChild(Node2 node, int angle, out Node2 newChild)
        {
            int distance = 0, x, y;
            var vector = node.Vector.Rotated(angle);

            newChild = null;
            do
            {
                distance++;
                x = node.X + vector.X * distance;
                y = node.Y + vector.Y * distance;
            }
            while (IsWithinCanvas(x, y) && AreaIsFree(x, y) && IsWithinTolerance(node.Color, x, y));
            distance--;
            if (distance == 0) { return false; }
            newChild = new Node2(node, node.X + vector.X * distance, node.Y + vector.Y * distance, vector);
            return true;
        }

        private void DrawTree(Node2 node)
        {
            _pixelPath.Add(new PointF(node.X, node.Y));
            if (node.Children.Count > 0)
            {
                foreach (var childNode in node.Children)
                {
                    if (_pixelPath.Count == 0)
                    {
                        _pixelPath.Add(new PointF(node.X, node.Y));
                    }
                    DrawTree(childNode);
                }
                return;
            }
            using var gr = Graphics.FromImage(_canvas.Bitmap);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            using var pen = new Pen(node.Color, 1);
            if (_curves) gr.DrawCurve(pen, _pixelPath.ToArray());
            else gr.DrawLines(pen, _pixelPath.ToArray());
            _pixelPath = new List<PointF>();
        }

        private void DrawTree2(Node2 node)
        {
            _pixelPath.Add(new PointF(node.X, node.Y));
            if (node.Children.Count > 0)
            {
                foreach (var childNode in node.Children)
                {
                    DrawTree2(childNode);
                }
                return;
            }
        }

        private bool IsWithinCanvas(int x, int y)
        {
            return x >= _margin && y >= _margin && x < _canvas.Width - _margin && y < _canvas.Height - _margin;
        }

        private bool IsWithinTolerance(Color color, int x, int y)
        {
            var testColor = _inputBitmap.GetPixel(x, y);
            return testColor.R > color.R - _tolerance
                && testColor.R < color.R + _tolerance;
        }

        private bool AreaIsFree(int x, int y)
        {
            return _referenceBitmap.GetPixel(x, y).A == 0;
        }

        private void SetAreaToCovered(int x1, int y1, int x2, int y2)
        {
            using var gr = Graphics.FromImage(_referenceBitmap);
            gr.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.White, _density);
            gr.DrawLine(pen, x1, y1, x2, y2);
        }


        private void PaintDots(Node2 node)
        {
            PaintDot(node);
            if (node.Children.Count > 0)
            {
                foreach (var childNode in node.Children)
                {
                    PaintDots(childNode);
                }
            }
        }

        private void PaintDot(Node2 node)
        {
            using var gr = Graphics.FromImage(_canvas.Bitmap);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            using var brush = new SolidBrush(node.Color);
            gr.FillRectangle(brush, new Rectangle(node.X - 2, node.Y - 2, 5, 5));
        }
    }
}
