using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    class PedBoundingBox : BoundingBox
    {
        private const float PedScaleFactor = 1.1f;
        //we use standard human proportions to compute chest dimensions
        private const float HeightScaleFactor = 180f / 227.4f;
        private const float WidthScaleFactor = 110f / 196.4f;
        private const float LengthScaleFactor = 0.8f;
        
        public PedBoundingBox(Ped ped)
        {
            ped.Model.GetDimensions(out var rearBottomLeft, out var frontTopRight);
            float chestHeight = ped.Height * HeightScaleFactor;
            float chestWidth = ped.Width * WidthScaleFactor;
            float chestLength = ped.Length * LengthScaleFactor;
            Vector3 size = new Vector3(chestWidth, chestLength, chestHeight) * PedScaleFactor;
            Vector3 wireboxCenter = ped.GetBonePosition(PedBoneId.Pelvis) - 0.075f*ped.UpVector;

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