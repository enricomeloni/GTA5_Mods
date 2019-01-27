using System;
using System.Collections.Generic;
using System.Linq;
using Rage;

namespace DatasetGenerator.BoundingBoxes
{
    partial class BoundingBox
    {

        public Vector3[] Edges { get; set; }

        /// <summary>
        /// <exception cref="AccessViolationException"> For some unknown reasons, sometimes projecting world position to screen position causes memory access violation. </exception>
        /// </summary>
        public Vector2[] ProjectedEdges => Edges.Select(edge => ExtensionMethods.ProjectToScreen(edge)).ToArray();

        public BoundingBox(Vector3 center, Vector3 size, Quaternion orientation)
        {
            var coefficients = new List<int> { -1, 1 };
            var wireboxEdges = new List<Vector3>();

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
                            size.X / 2f * coefficientX,
                            size.Y / 2f * coefficientY,
                            size.Z / 2f * coefficientZ
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


        public static BoundingBox FromBone(Ped ped, PedBoneId boneId)
        {
            Vector3 bonePosition = ped.GetBonePosition(boneId);
            Quaternion boneQuaternion = ped.GetBoneOrientation(boneId);

            switch (boneId)
            {
                case PedBoneId.Head:
                    return FromHead(bonePosition, boneQuaternion);
                default:
                    return null;
            }

        }
    }
}
