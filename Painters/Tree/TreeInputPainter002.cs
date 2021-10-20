using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreeInputPainter002 : IPainter
    {
        private Bitmap _inputBitmap;
        private ICanvas _canvas;
        private List<PointF> _pixelPath = new List<PointF>();
        private readonly Random _rnd = new Random();
        private Bitmap _referenceBitmap;
        private int _densityHalf;

        //private int[] _branchIndices = new int[] { 0, -1, 1 };
        private int[] _branchIndices = new int[] { 0, -1, 1, -2, 2 };
        private int _margin = 10;
        private int _branchLength = 30;
        private int _maxDepth = 150;
        private int _angle = 37;
        private int _density = 10;
        private bool _bezier = false;

        public TreeInputPainter002()
        {
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            _inputBitmap = canvas.InputBitmap;
            _densityHalf = _density / 2;
            _referenceBitmap = new Bitmap(canvas.Width, canvas.Height);
            var root = new Node(canvas.Width / 2 + 6, canvas.Height / 2 + _branchLength);
            //var root = new Node(60, canvas.Height - 60);
            var rootTop = root.AddChild(0, _branchLength);

            for (int i = 0; i < _maxDepth; i++)
            {
                CreateChildren(rootTop, i);
            }

            if (_bezier)
            {
                DrawTree2(rootTop);
                using var gr = Graphics.FromImage(_canvas.Bitmap);
                gr.SmoothingMode = SmoothingMode.HighQuality;
                using var pen = new Pen(Color.Black, 1);
                // gr.DrawCurve(pen, _pixelPath.ToArray());
                if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
                if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
                gr.DrawBeziers(pen, _pixelPath.ToArray());
            }
            else
            {
                DrawTree(rootTop);
            }
        }

        private void CreateChildren(Node node, int currentDepth)
        {
            if (node.Children.Count == 0)
            {
                //for (int i = 0; i < 3; i++)
                for (int i = _rnd.Next(0, 2); i < _rnd.Next(3, 5); i++)
                {
                    var branchIndex = _branchIndices[i];
                    var vector = NewBranch(node, branchIndex);
                    var newChild = new Node(node, vector);
                    if (IsOutOfCanvas(newChild.X, newChild.Y)) { continue; }
                    if (AreaIsCovered(newChild.X, newChild.Y)) { continue; }
                    SetAreaToCovered(newChild.X, newChild.Y);
                    PaintDot(newChild.X, newChild.Y);
                    node.AddChild(vector);
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

        private Vector NewBranch(Node node, int childIndex)
        {
            var branchDirection = new Vector(node.Parent.X, node.Parent.Y, node.X, node.Y);
            return childIndex == 0 ? branchDirection : branchDirection.Rotated(childIndex * _angle);
            //var branchDirection = new Vector(node.Parent.X, node.Parent.Y, node.X, node.Y).NewLength(_rnd.Next(40, 150));
            //return branchDirection.Rotated(childIndex * _rnd.Next(17, 24));
        }

        private void DrawTree(Node node)
        {
            if (node.Parent != null)
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
            var color = _inputBitmap.GetPixel(node.X, node.Y);
            using var gr = Graphics.FromImage(_canvas.Bitmap);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            using var pen = new Pen(color, 1);
            //using var pen = new Pen(Color.Black, 1);
            gr.DrawCurve(pen, _pixelPath.ToArray());
            _pixelPath = new List<PointF>();
        }

        private void DrawTree2(Node node)
        {
            if (node.Parent != null)
                _pixelPath.Add(new PointF(node.X, node.Y));
            if (node.Children.Count > 0)
            {
                foreach (var childNode in node.Children)
                {
                    _pixelPath.Add(new PointF(node.X, node.Y));
                    DrawTree2(childNode);
                }
                return;
            }
        }

        private bool IsOutOfCanvas(int x, int y)
        {
            return x < _margin || y < _margin || x >= _canvas.Width - _margin || y >= _canvas.Height - _margin;
        }

        private bool AreaIsCovered(int x, int y)
        {
            var coveredColor = _referenceBitmap.GetPixel(x, y);
            return coveredColor.R > 0;
        }

        private void SetAreaToCovered(int x, int y)
        {
            using var gr = Graphics.FromImage(_referenceBitmap);
            gr.SmoothingMode = SmoothingMode.HighSpeed;
            using var brush = new SolidBrush(Color.White);

            //using var gr = Graphics.FromImage(_canvas.Bitmap);
            //gr.SmoothingMode = SmoothingMode.HighQuality;
            //using var brush = new SolidBrush(Color.Black);

            //gr.FillEllipse(brush, new Rectangle(x - _densityHalf, y - _densityHalf, _density, _density));
            gr.FillRectangle(brush, new Rectangle(x - _densityHalf, y - _densityHalf, _density, _density));
        }

        private void PaintDot(int x, int y)
        {
            var inputColor = _inputBitmap.GetPixel(x, y);
            using var gr = Graphics.FromImage(_canvas.Bitmap);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            using var brush = new SolidBrush(inputColor);
            //gr.FillEllipse(brush, new Rectangle(x - _densityHalf / 2, y - _densityHalf / 2, _densityHalf, _densityHalf));
            //gr.FillRectangle(brush, new Rectangle(x - _densityHalf / 2, y - _densityHalf / 2, _densityHalf, _densityHalf));
            gr.FillRectangle(brush, new Rectangle(x - _densityHalf, y - _densityHalf, _density, _density));
        }
    }
}
