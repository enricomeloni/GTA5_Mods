using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace DatasetGenerator
{
    class BoundingBox
    {
        //This controls how much boxes should be bigger than the real object
        public const float ScaleFactor = 1.05f;

        public Vector3[] Edges { get; set; }

        /// <summary>
        /// <exception cref="AccessViolationException"> For some unknown reasons, sometimes projecting world position to screen position causes memory access violation. </exception>
        /// </summary>
        public Vector2[] ProjectedEdges => Edges.Select(World.ConvertWorldPositionToScreenPosition).ToArray();

        public BoundingBox(Vector3 center, Vector3 size, Quaternion orientation)
        {
            var coefficients = new List<int> { -1, 1 };
            var wireboxEdges = new List<Vector3>();

            var scaledSize = size * ScaleFactor;

            /*
             * The wirebox edges can be computed by summing or subtracting half the size of the wirebox
             * We have 8 possible combinations, (-1,-1,-1), (-1,-1,+1) etc
             */
            foreach (var coefficientX in coefficients)
            {
                foreach (var coefficientY in coefficients)
                {
                    foreach (var coefficientZ in coefficients)
                    {
                        //this is an offset from the wirebox center, not yet rotated to match the entity orientation
                        Vector3 edgeOffsetFromWireBoxCenter = new Vector3(
                            scaledSize.X / 2f * coefficientX,
                            scaledSize.Y / 2f * coefficientY,
                            scaledSize.Z / 2f * coefficientZ
                        );

                        //rotate the offset to match the entity orientation
                        Vector3 rotatedEdgeOffset = edgeOffsetFromWireBoxCenter.Rotate(orientation);
                        var edge = center + rotatedEdgeOffset;
                        wireboxEdges.Add(edge);
                    }
                }
            }

            Edges = wireboxEdges.ToArray();
        }

        public BoundingRect ToBoundingRect()
        {
            var maxX = ProjectedEdges.Max(edge => edge.X);
            var maxY = ProjectedEdges.Max(edge => edge.Y);
            var minX = ProjectedEdges.Min(edge => edge.X);
            var minY = ProjectedEdges.Min(edge => edge.Y);

            Vector2 topLeft = new Vector2(minX, maxY);
            Vector2 bottomRight = new Vector2(maxX, minY);

            return new BoundingRect(topLeft, bottomRight);
        }


        public static BoundingBox FromWeapon(Weapon weapon)
        {
            weapon.Model.GetDimensions(out var weaponBottomLeft, out var weaponTopRight);

            //Compute the size of the three sides of the box
            Vector3 size = weaponTopRight - weaponBottomLeft;

            //Compute the offset of the weapon center relative to the origin of the model
            Vector3 centerOffset = (weaponTopRight + weaponBottomLeft) / 2.0f;

            //Now we must rotate the center offset computed on the model, to match the entity orientation
            var rotatedCenterOffset = centerOffset.Rotate(weapon.Orientation);

            var wireBoxCenter = weapon.Position + rotatedCenterOffset;
            return new BoundingBox(wireBoxCenter, size, weapon.Orientation);
        }


    }
}
