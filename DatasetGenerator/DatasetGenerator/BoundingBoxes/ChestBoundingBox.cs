using System;
using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    
    partial class BoundingBox
    {
        private const float ChestScaleFactor = 1.1f;
        private const float HeightScaleFactor = 2.5f / 7.5f;
        private const float WidthScaleFactor = 0.6f;
        private const float LengthScaleFactor = 0.8f;

        public static BoundingBox FromChest(Ped ped)
        {
            //we use standard human proportions to compute chest dimensions

            Vector3 spineBonePosition = ped.GetBonePosition(PedBoneId.Spine2);
            Quaternion chestOrientation = ped.Orientation;

            float chestHeight = ped.Height * HeightScaleFactor;
            float chestWidth = ped.Width * WidthScaleFactor;
            float chestLength = ped.Length * LengthScaleFactor;

            Vector3 chestSize = new Vector3(chestWidth, chestLength, chestHeight) * ChestScaleFactor;

            return new BoundingBox(spineBonePosition, chestSize, chestOrientation);
        }

    }
}
