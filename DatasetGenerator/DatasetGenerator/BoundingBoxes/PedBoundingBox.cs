using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    class PedBoundingBox : BoundingBox
    {
        private const float PedScaleFactor = 0.8f;
        
        public PedBoundingBox(Ped ped)
        {
            ped.Model.GetDimensions(out var rearBottomLeft, out var frontTopRight);
            Vector3 size = new Vector3(ped.Width, ped.Length, ped.Height) * PedScaleFactor;
            Vector3 centerOffset = (frontTopRight + rearBottomLeft) / 2.0f;
            Vector3 wireboxCenter = ped.Position + centerOffset.Rotate(ped.Orientation);

            Initialize(wireboxCenter, size, ped.Orientation, ped);
        }

        public override DetectedObject ToDetectedObject()
        {
            return new DetectedObject
            {
                BoundingRect = ToBoundingRect(),
                ObjectClass = ObjectClass.WholePerson
            };
        }
    }
}