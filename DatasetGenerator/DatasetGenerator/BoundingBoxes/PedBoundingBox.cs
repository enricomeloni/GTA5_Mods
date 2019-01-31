using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    partial class BoundingBox
    {
        private const float PedScaleFactor = 0.8f;

        public static BoundingBox FromPed(Ped ped)
        {
            ped.Model.GetDimensions(out var rearBottomLeft, out var frontTopRight);
            Vector3 size = new Vector3(ped.Width, ped.Length, ped.Height) * PedScaleFactor;
            Vector3 centerOffset = (frontTopRight + rearBottomLeft) / 2.0f;
            Vector3 wireboxCenter = ped.Position + centerOffset.Rotate(ped.Orientation);

            return new BoundingBox(wireboxCenter, size, ped.Orientation, ped);
        }
    }
}