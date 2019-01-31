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
        public static void DrawBoundingRect(BoundingRect rect, Graphics graphics, Color color)
        {
            rect.Draw(graphics, color);
        }

        
        public static void DrawBoundingBox(BoundingBox box, Graphics graphics, Color color)
        {
            box.Draw(graphics, color);
        }
    }
}
