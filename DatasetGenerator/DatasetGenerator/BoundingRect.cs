using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

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
    }
}
