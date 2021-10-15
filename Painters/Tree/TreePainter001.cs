using Painter_Dietmar_Schoder.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Painter_Dietmar_Schoder.Painters.Square
{
    public class TreePainter001 : IPainter
    {
        private readonly Random _rnd = new Random();
        private List<PointF> _pixelPath = new List<PointF>();
        private ICanvas _canvas;

        public TreePainter001()
        {
        }

        public void DrawOnCanvas(ICanvas canvas)
        {
            _canvas = canvas;
            var root = new Node(canvas.Width / 2, canvas.Height - 50);
            var rootTop = root.AddChild(0, +50);
            rootTop.AddChild(-100, +50);
            var child2 = rootTop.AddChild(0, +50);
            rootTop.AddChild(+100, +50);
            child2.AddChild(-100, +50);
            child2.AddChild(0, +50);
            child2.AddChild(+100, +50);

            DrawTree(root);
        }

        private void CreateTree()
        {

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
            using var pen = new Pen(Color.Black, 1);
            gr.DrawCurve(pen, _pixelPath.ToArray());
            _pixelPath = new List<PointF>();
        }
    }
}
