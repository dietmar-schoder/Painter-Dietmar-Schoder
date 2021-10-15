﻿using System.Collections.Generic;

namespace Painter_Dietmar_Schoder.Tools
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children{ get; set; } = new List<Node>();

        public Node(Node parent, int x, int y)
        {
            Parent = parent;
            X = Parent.X + x;
            Y = Parent.Y - y;
        }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Node AddChild(int x, int y)
        {
            var child = new Node(this, x, y);
            Children.Add(child);
            return child;
        }
    }
}