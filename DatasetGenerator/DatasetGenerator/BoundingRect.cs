using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Graphics = Rage.Graphics;

namespace DatasetGenerator
{
    class BoundingRect
    {
        public Vector2 TopLeft { get; set; }
        public Vector2 BottomRight { get; set; }

        public BoundingRect(Vector2 topLeft, Vector2 bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public List<Vector2> Edges => new List<Vector2>
        {
            new Vector2(TopLeft.X, TopLeft.Y),
            new Vector2(BottomRight.X, TopLeft.Y),
            new Vector2(BottomRight.X, BottomRight.Y),
            new Vector2(TopLeft.X, BottomRight.Y)
        };

        public float Width => (BottomRight.X - TopLeft.X);
        public float Height => (TopLeft.Y - BottomRight.Y);

        public Vector2 Center
        {
            get
            {
                var centerX = (BottomRight.X + TopLeft.X) / 2;
                var centerY = (BottomRight.Y + TopLeft.Y) / 2;

                return new Vector2(centerX, centerY);
            }
        }

        public void Draw(Graphics graphics, Color color)
        {
            graphics.DrawLine(Edges[0], Edges[1], color);
            graphics.DrawLine(Edges[1], Edges[2], color);
            graphics.DrawLine(Edges[2], Edges[3], color);
            graphics.DrawLine(Edges[3], Edges[0], color);
        }
    }
}
