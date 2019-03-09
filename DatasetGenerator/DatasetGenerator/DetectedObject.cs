using System.Drawing;
using System.Globalization;
using Rage;

namespace DatasetGenerator
{
    class DetectedObject
    {
        public BoundingRect BoundingRect { get; set; }
        public ObjectClass ObjectClass { get; set; }

        public override string ToString()
        {
            Size resolution = Game.Resolution;
            var center = BoundingRect.Center;
            
            var normalizedCenter = new Vector2(center.X / resolution.Width, center.Y / resolution.Height);
            var normalizedSize = new Vector2(BoundingRect.Width / resolution.Width, BoundingRect.Height / resolution.Height);

            //for enum just use the number, yolo wants a number anyway
            //<object-class> <x> <y> <width> <height>
            return
                $"{(int) ObjectClass} " +
                $"{normalizedCenter.X.ToString(CultureInfo.InvariantCulture)} " +
                $"{normalizedCenter.Y.ToString(CultureInfo.InvariantCulture)} " +
                $"{normalizedSize.X.ToString(CultureInfo.InvariantCulture)} " +
                $"{normalizedSize.Y.ToString(CultureInfo.InvariantCulture)} ";
        }
    }
}
