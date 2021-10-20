using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreePainter005 : IPainter
    {
        private ICanvas _canvas;
        private List<PointF> _pixelPath = new List<PointF>();
        private readonly Random _rnd = new Random();
        private Bitmap _referenceBitmap;
        private int _densityHalf;

        private int[] _branchIndices = new int[] { 0, -1, 1};
        private int _margin = 10;
        private int _branchLength = 170;
        private int _maxDepth = 7;
        private int _angle = 15;
        private int _density = 15;
        private bool _bezier = false;

        public TreePainter005() { }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            _densityHalf = _density / 2;
            _referenceBitmap = new Bitmap(canvas.Width, canvas.Height);
            var root = new Node(canvas.Width / 2 + 7, canvas.Height - 50);
            var rootTop = root.AddChild(0, _branchLength);

            for (int i = 0; i < _maxDepth; i++)
            {
                CreateChildren(rootTop, i);
            }

            if (_bezier)
            {
                DrawTree2(root);
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
                DrawTree(root);
            }
        }

        private void CreateChildren(Node node, int currentDepth)
        {
            if (node.Children.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                    //for (int i = _rnd.Next(-3, 0); i < _rnd.Next(1, 5); i++)
                {
                    var branchIndex = _branchIndices[i];
                    var vector = NewBranch(node, branchIndex);
                    var newChild = new Node(node, vector);
                    if (IsOutOfCanvas(newChild.X, newChild.Y)) { continue; }
                    if (AreaIsCovered(newChild.X, newChild.Y)) { continue; }
                    SetAreaToCovered(newChild.X, newChild.Y);
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
            using var pen = new Pen(Color.Black, 1);
            gr.DrawLines(pen, _pixelPath.ToArray());
            _pixelPath = new List<PointF>();
        }

        private void DrawTree2(Node node)
        {
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
            //var coveredPixelR = _referenceBitmap.GetPixel(x, y).R;
            var coveredPixelR = _canvas.Bitmap.GetPixel(x, y).B;
            return coveredPixelR < 255;
            // return coveredPixelR > 0;
        }

        private void SetAreaToCovered(int x, int y)
        {
            // using var gr = Graphics.FromImage(_referenceBitmap);
            using var gr = Graphics.FromImage(_canvas.Bitmap);
            gr.SmoothingMode = SmoothingMode.HighSpeed;
            using var brush = new SolidBrush(Color.Black);
            //gr.FillEllipse(brush, new Rectangle(x - _densityHalf, y - _densityHalf, _density, _density));
            gr.FillRectangle(brush, new Rectangle(x - _densityHalf, y - _densityHalf, _density, _density));
        }
    }
}
