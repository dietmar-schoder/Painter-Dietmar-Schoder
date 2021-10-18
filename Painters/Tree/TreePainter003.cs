using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreePainter003 : IPainter
    {
        private ICanvas _canvas;
        private List<PointF> _pixelPath = new List<PointF>();
        private readonly Random _rnd = new Random();
        private bool[,] _referenceBitmap;
        private int _startX, _startY;
        private int _branchLength = 35;
        private int _maxDepth = 2000;
        private int _angle = 19;
        private double _density = 30;
        private bool _bezier = true;

        public TreePainter003()
        {
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            _startX = canvas.Width / 2;
            _startY = canvas.Height - 50;
            var refWidth = (int)Math.Round(_canvas.Width / _density, 0, MidpointRounding.AwayFromZero);
            var refHeight = (int)Math.Round(_canvas.Height / _density, 0, MidpointRounding.AwayFromZero);
            _referenceBitmap = new bool[refWidth, refHeight];
            var root = new Node(_startX, _startY);
            var rootTop = root.AddChild(0, _branchLength);

            CreateChildren(rootTop, 0);

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
            currentDepth++;
            // for (int i = -1; i < 2; i++)
            for (int i = _rnd.Next(-2, 0); i < _rnd.Next(1, 4); i++)
            {
                var newChild = new Node(node, NewBranch(node, i));
                var denseX = (int)Math.Round(newChild.X / _density, 0, MidpointRounding.AwayFromZero);
                var denseY = (int)Math.Round(newChild.Y / _density, 0, MidpointRounding.AwayFromZero);
                if (IsOutOfBitmap(denseX, denseY, _referenceBitmap) || _referenceBitmap[denseX, denseY])
                {
                    continue;
                }
                _referenceBitmap[denseX, denseY] = true;
                node.AddChild(NewBranch(node, i));
            }
            if (currentDepth < _maxDepth)
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
            //var branchDirection = new Vector(node.Parent.X, node.Parent.Y, node.X, node.Y).NewLength(_rnd.Next(40, 150));
            //return branchDirection.Rotated(childIndex * _rnd.Next(17, 24));
            return childIndex == 0 ? branchDirection : branchDirection.Rotated(childIndex * _angle);
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
                    //if (_pixelPath.Count == 0)
                    //{
                    //}
                    DrawTree2(childNode);
                }
                return;
            }
        }

        private bool IsOutOfBitmap(int x, int y, bool[,] bitmap)
        {
            return x < 1 || y < 1 || x >= bitmap.GetLength(0) - 1 || y >= bitmap.GetLength(1) - 1;
        }
    }
}
