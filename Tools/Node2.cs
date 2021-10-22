using System.Collections.Generic;
using System.Drawing;

namespace Painter_Dietmar_Schoder.Tools
{
    public class Node2
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Level { get; set; }
        public Color StartColor { get; set; }
        public Color Color { get; set; }
        public Vector Vector { get; set; }
        public Node2 Parent { get; set; }
        public List<Node2> Children{ get; set; } = new List<Node2>();

        public Node2(int x, int y, Vector vector)
        {
            X = x;
            Y = y;
            Vector = vector;
        }

        public Node2(Node2 parent, int x, int y, Vector vector)
            : this(x, y, vector)
        {
            Parent = parent;
            Parent.Children.Add(this);
            StartColor = parent.StartColor;
            Color = parent.Color;
            Level = parent.Level + 1;
        }
    }
}
