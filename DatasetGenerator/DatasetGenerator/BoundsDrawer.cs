using System.Drawing;
using DatasetGenerator.BoundingBoxes;
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
