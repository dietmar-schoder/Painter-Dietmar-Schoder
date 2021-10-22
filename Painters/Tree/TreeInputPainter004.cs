using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreeInputPainter004 : IPainter
    {
        private Bitmap _inputBitmap;
        private ICanvas _canvas;
        private List<PointF> _pixelPath = new List<PointF>();
        private Queue<int> _pixelQueue = new Queue<int>();
        private readonly Random _rnd = new Random();
        private Bitmap _referenceBitmap;

        //private int[] _branchAngles = new int[] { 0, 180, 90, 270 };
        //private int[] _branchAngles = new int[] { 0, 90, 180, 270 };
        //private int[] _branchAngles = new int[] { 45, 135, 225, 315 };
        //private int[] _branchAngles = new int[] { 0, 90, 270, 45, 315, 135, 225 };
        private int[] _branchAngles = new int[] { 0, 27, 60, 89, 120, 152, 180, 212, 240, 273, 300, 330 };
        // private int[] _branchAngles = new int[] { 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330 };

        //private int[] _branchAngles = new int[] { 0, 50, 100, 150, 200, 250, 300, 350 };

        private int _margin = 10;
        private int _branchLength = 20;
        private int _maxDepth = 200;
        private int _density = 18;
        private int _tolerance = 25;
        private bool _bezier = false;
        private bool _curves = true;
        private bool _paintDots = true;

        public TreeInputPainter004() { }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            _inputBitmap = canvas.InputBitmap;
            _referenceBitmap = new Bitmap(canvas.Width, canvas.Height);
            var startX = canvas.Width / 2;
            var startY = canvas.Height / 2;
            _pixelQueue.Enqueue(XyToPixelNumber(startX, startY));

            while (_pixelQueue.Count > 0)
            {
                while (_pixelQueue.Count > 0)
                {
                    var pixel = PixelNumbertoXy(_pixelQueue.Dequeue());
                    if (!AreaIsFree(pixel.X, pixel.Y)) { continue; }
                    var root = new Node2(pixel.X, pixel.Y, new Vector(0, -_branchLength));
                    root.StartColor = root.Color = _inputBitmap.GetPixel(root.X, root.Y);
                    SetDotToCovered(pixel.X, pixel.Y);
                    //for (int i = 0; i < _maxDepth; i++)
                    //{
                    //CreateChildren(root, i);
                    //}
                    CreateChildren(root, 0);

                    if (_bezier)
                    {
                        DrawTree2(root);
                        if (_pixelPath.Count < 4) { continue; }
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
                    _pixelPath = new List<PointF>();
                }
                EnqueueNextFreePixel();
            }
            //canvas.Bitmap = _referenceBitmap;
        }

        private void EnqueueNextFreePixel()
        {
            for (int y = 0; y < _referenceBitmap.Height - 1; y += _branchLength)
            {
                for (int x = 0; x < _referenceBitmap.Width - 1; x += _branchLength)
                {
                    if (AreaIsFree(x, y))
                    {
                        _pixelQueue.Enqueue(XyToPixelNumber(x, y));
                        return;
                    }
                }
            }
        }

        private void CreateChildren(Node2 node, int currentDepth)
        {
            currentDepth++;
            if (node.Children.Count == 0)
            {
                for (int i = 0; i < _branchAngles.Length - 1; i++)
                {
                    // var angleIndex = _rnd.Next(0, _branchAngles.Length);
                    //var angleIndex = _branchAngles[i];
                    //if (!TryGetNewChild(node, _branchAngles[angleIndex], out var newChild)) { continue; }
                    if (!TryGetNewChild(node, _branchAngles[i], out var newChild)) { continue; }
                    //newChild.Color = _inputBitmap.GetPixel(newChild.X, newChild.Y);
                    SetAreaToCovered(node.X, node.Y, newChild.X, newChild.Y);
                }
            }
            //if (node.Level < currentDepth)
            if (currentDepth < _maxDepth)
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
            while (IsWithinCanvas(x, y) && AreaIsFree(x, y) && IsWithinTolerance(node.StartColor, x, y));
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
            if (_pixelPath.Count < 2) { return; }
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
            var isWithinTolerance = (int)testColor.R > (int)(color.R - _tolerance) && (int)testColor.R < (int)(color.R + _tolerance);
            if (!isWithinTolerance)
            {
                var pixelNumber = XyToPixelNumber(x, y);
                if (!_pixelQueue.Contains(pixelNumber))
                    _pixelQueue.Enqueue(pixelNumber);
            }
            return isWithinTolerance;
        }

        private bool AreaIsFree(int x, int y)
        {
            return _referenceBitmap.GetPixel(x, y).R == 0;
        }

        private void SetAreaToCovered(int x1, int y1, int x2, int y2)
        {
            using var gr = Graphics.FromImage(_referenceBitmap);
            gr.SmoothingMode = SmoothingMode.Default;
            using var pen = new Pen(Color.White, _density);
            gr.DrawLine(pen, x1, y1, x2, y2);
        }

        private void SetDotToCovered(int x, int y)
        {
            using var gr = Graphics.FromImage(_referenceBitmap);
            gr.SmoothingMode = SmoothingMode.Default;
            using var brush = new SolidBrush(Color.White);
            gr.FillRectangle(brush, new Rectangle(x - _density / 2, y - _density / 2, _density, _density));
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

        private int XyToPixelNumber(int x, int y)
        {
            return y * _canvas.Width + x;
        }

        private (int X, int Y) PixelNumbertoXy(int pixelNumber)
        {
            return (pixelNumber % _canvas.Width, pixelNumber / _canvas.Width);
        }
    }
}
