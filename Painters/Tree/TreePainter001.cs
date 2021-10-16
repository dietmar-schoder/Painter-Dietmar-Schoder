using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreePainter001 : IPainter
    {
        private ICanvas _canvas;
        private List<PointF> _pixelPath = new List<PointF>();
        private readonly Random _rnd = new Random();
        private int _maxDepth;

        public TreePainter001()
        {
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            _maxDepth = 7;
            var root = new Node(canvas.Width / 2, canvas.Height - 50);
            var rootTop = root.AddChild(0, +150);
            CreateChildren(rootTop, 0);

            DrawTree2(root);
            using var gr = Graphics.FromImage(_canvas.Bitmap);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            using var pen = new Pen(Color.Black, 1);
            //gr.DrawCurve(pen, _pixelPath.ToArray());
            if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
            if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
            gr.DrawBeziers(pen, _pixelPath.ToArray());
        }

        private void CreateChildren(Node node, int currentDepth)
        {
            currentDepth++;
            for (int i = -1; i < 2; i++)
                //for (int i = _rnd.Next(-2, 0); i < _rnd.Next(1, 4); i++)
            {
                var child = node.AddChild(NewBranch(node, i));
                if (currentDepth < _maxDepth)
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
            return branchDirection.Rotated(childIndex * 20);
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
    }
}
