using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    partial class BoundingBox
    {
        private const float HeadScaleFactor = 1.4f;
        private static BoundingBox FromHead(Vector3 headBonePosition, Quaternion headOrientation)
        {
            //since there is no information about head size, i used average sizes of world population

            //todo: insert also data for women

            float headBreadth = 15.2f / 100;
            float headLength = 19.7f / 100;
            float headHeight = 23.2f / 100 * 1.1f; //account 20% more height for helmet

            Vector3 headSize = new Vector3(headHeight, headLength, headBreadth) * HeadScaleFactor;

            Vector3 offset = new Vector3(headHeight * 0.25f, headLength * 0.1f, 0);
            Vector3 rotatedOffset = offset.Rotate(headOrientation);

            Vector3 headCenter = rotatedOffset + headBonePosition;

            return new BoundingBox(headCenter, headSize, headOrientation);
        }
    }
}
