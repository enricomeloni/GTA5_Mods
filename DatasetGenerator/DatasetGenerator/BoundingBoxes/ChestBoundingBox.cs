using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    
    class ChestBoundingBox : BoundingBox
    {

        //we use standard human proportions to compute chest dimensions
        private const float ChestScaleFactor = 1.1f;
        private const float HeightScaleFactor = 61.77f / 227.4f;
        private const float WidthScaleFactor = 80f / 196.4f;
        private const float LengthScaleFactor = 0.8f;
        
        public ChestBoundingBox(Ped ped)
        {


            Vector3 spineBonePosition = ped.GetBonePosition(PedBoneId.Spine3);
            Quaternion chestOrientation = ped.Orientation;

            float chestHeight = ped.Height * HeightScaleFactor;
            float chestWidth = ped.Width * WidthScaleFactor;
            float chestLength = ped.Length * LengthScaleFactor;

            Vector3 chestSize = new Vector3(chestWidth, chestLength, chestHeight) * ChestScaleFactor;

            Initialize(spineBonePosition, chestSize, chestOrientation, ped);
        }

        public override DetectedObject ToDetectedObject()
        {
            var ped = (Ped) Entity;
            var pedClassifier = ped.GetPedClassifier();

            ObjectClass objectClass;
            if (pedClassifier.HasHighVisibilityVest())
                objectClass = ObjectClass.ChestWithHighVisibilityVest;
            else
                objectClass = ObjectClass.BareChest;

            return new DetectedObject
            {
                BoundingRect = ToBoundingRect(),
                ObjectClass = objectClass
            };
        }
    }
}
