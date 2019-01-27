using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatasetGenerator.BoundingBoxes;
using Rage;
using Graphics = Rage.Graphics;

namespace DatasetGenerator
{
    class BoundsDrawer
    {
        public static void DrawBoundingRect(BoundingRect rect, Graphics graphics)
        {
            graphics.DrawLine(rect.Edges[0], rect.Edges[1], Color.Red);
            graphics.DrawLine(rect.Edges[1], rect.Edges[2], Color.Red);
            graphics.DrawLine(rect.Edges[2], rect.Edges[3], Color.Red);
            graphics.DrawLine(rect.Edges[3], rect.Edges[0], Color.Red);
        }

        
        public static void DrawBoundingBox(BoundingBox box, Graphics graphics)
        {
            foreach (Vector2 edge1 in box.ProjectedEdges)
            {
                foreach (Vector2 edge2 in box.ProjectedEdges)
                {
                    if (!edge1.Equals(edge2))
                    {
                        graphics.DrawLine(edge1, edge2, Color.Blue);
                    }
                }
            }
        }
    }
}
