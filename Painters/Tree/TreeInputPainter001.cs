using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreeInputPainter001 : IPainter
    {
        private ICanvas _canvas;
        private List<PointF> _pixelPath = new List<PointF>();
        private readonly Random _rnd = new Random();
        private Bitmap _inputBitmap;
        private bool[,] _referenceBitmap;
        private int _branchLength = 100;
        private int _maxDepth = 1000;
        private int _angle = 32;
        private double _density = 20;
        private int _color;
        private bool _bezier = true;

        public TreeInputPainter001()
        {
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            var refWidth = (int)Math.Round(_canvas.Width / _density, 0, MidpointRounding.AwayFromZero);
            var refHeight = (int)Math.Round(_canvas.Height / _density, 0, MidpointRounding.AwayFromZero);
            _inputBitmap = canvas.InputBitmap;

            for (int color = 5; color < 256; color += 25)
            {
                _referenceBitmap = new bool[refWidth, refHeight];
                for (int y = _canvas.Height / 4; y < _canvas.Height * 3 / 4; y += 100)
                {
                    for (int x = _canvas.Width / 4; x < _canvas.Width * 3 / 4; x += 100)
                    {
                        _branchLength = (255 - color) / 8 + 40;

                        var root = new Node(x, y);
                        var rootTop = root.AddChild(0, _branchLength);
                        _pixelPath = new List<PointF>();
                        _color = color;
                        for (int i = 0; i < _maxDepth; i++)
                        {
                            CreateChildren(rootTop, i);
                        }
                        if (_bezier)
                        {
                            DrawTree2(root);
                            if (_pixelPath.Count > 5)
                            {
                                using var gr = Graphics.FromImage(_canvas.Bitmap);
                                gr.SmoothingMode = SmoothingMode.HighSpeed;
                                using var pen = new Pen(Color.FromArgb(255, color, color, color), 1);
                                // gr.DrawCurve(pen, _pixelPath.ToArray());
                                if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
                                if ((_pixelPath.Count - 1) % 3 > 0) _pixelPath.RemoveAt(_pixelPath.Count - 1);
                                gr.DrawBeziers(pen, _pixelPath.ToArray());
                            }
                        }
                        else
                        {
                            //if (_pixelPath.Count < 3) { continue; }
                            DrawTree(root);
                        }
                    }
                }
            }
        }

        private void CreateChildren(Node node, int currentDepth)
        {
            if (node.Children.Count == 0)
            {
                //for (int i = -2; i < 3; i++)
                //for (int i = -1; i < 2; i++)
                for (int i = _rnd.Next(-3, 0); i < _rnd.Next(1, 5); i++)
                {
                    var newChild = new Node(node, NewBranch(node, i));
                    var denseX = (int)Math.Round(newChild.X / _density, 0, MidpointRounding.AwayFromZero);
                    var denseY = (int)Math.Round(newChild.Y / _density, 0, MidpointRounding.AwayFromZero);
                    if (!IsOutOfBitmap(denseX, denseY, _referenceBitmap)
                        && !_referenceBitmap[denseX, denseY]
                        && IsInTolerance(newChild.X, newChild.Y, _inputBitmap))
                    {
                        _referenceBitmap[denseX, denseY] = true;
                        node.AddChild(NewBranch(node, i));
                    }
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
            //var branchDirection = new Vector(node.Parent.X, node.Parent.Y, node.X, node.Y).NewLength(_rnd.Next(40, 150));
            //return branchDirection.Rotated(childIndex * _rnd.Next(17, 24));
            var branchDirection = new Vector(node.Parent.X, node.Parent.Y, node.X, node.Y);
            // return childIndex == 0 ? branchDirection : branchDirection.Rotated(childIndex * _angle);
            return childIndex == 0 ? branchDirection : branchDirection.Rotated(childIndex * _rnd.Next(40, 90));
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

        private bool IsOutOfBitmap(int x, int y, bool[,] bitmap)
        {
            return x < 1 || y < 1 || x >= bitmap.GetLength(0) - 1 || y >= bitmap.GetLength(1) - 1;
        }

        private bool IsInTolerance(int x, int y, Bitmap bitmap)
        {
            var r = bitmap.GetPixel(x, y).R;
            return r <= _color + 25 && r >= _color -25;
        }
    }
}
