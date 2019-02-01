using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DatasetGenerator.DetectedObjects;
using Rage;
using Graphics = Rage.Graphics;

namespace DatasetGenerator.BoundingBoxes
{
    abstract class BoundingBox
    {
        public const int HitTreshold = 8;

        public Entity Entity { get; private set; }
        public Vector3 Center { get; private set; }
        public Vector3 Size { get; private set; }
        public Quaternion Orientation { get; private set; }

        public Vector3[] Edges { get; private set; }

        /// <summary>
        /// <exception cref="AccessViolationException"> For some unknown reasons, sometimes projecting world position to screen position causes memory access violation. </exception>
        /// </summary>
        public Vector2[] ProjectedEdges => Edges.Select(edge => ExtensionMethods.ProjectToScreen(edge)).ToArray();

        protected void Initialize(Vector3 center, Vector3 size, Quaternion orientation, Entity entity)
        {
            Entity = entity;
            Center = center;
            Size = size;
            Orientation = orientation;

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
                            Size.X / 2f * coefficientX,
                            Size.Y / 2f * coefficientY,
                            Size.Z / 2f * coefficientZ
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

        public bool ShouldDraw(Camera camera)
        {
            int hitCounter = 0;

            foreach (var edge in Edges)
            {
                var hitResult = World.TraceLine(camera.Position, edge, TraceFlags.IntersectEverything, Entity);
                if (hitResult.Hit)
                {
                    ++hitCounter;
                }
            }
            
            return hitCounter < HitTreshold;
        }


        protected BoundingRect ToBoundingRect()
        {
            var maxX = ProjectedEdges.Max(edge => edge.X);
            var maxY = ProjectedEdges.Max(edge => edge.Y);
            var minX = ProjectedEdges.Min(edge => edge.X);
            var minY = ProjectedEdges.Min(edge => edge.Y);

            Vector2 topLeft = new Vector2(minX, maxY);
            Vector2 bottomRight = new Vector2(maxX, minY);

            return new BoundingRect(topLeft, bottomRight);
        }

        public void Draw(Graphics graphics, Color color)
        {
            foreach (Vector2 edge1 in ProjectedEdges)
            {
                foreach (Vector2 edge2 in ProjectedEdges)
                {
                    if (!edge1.Equals(edge2))
                    {
                        graphics.DrawLine(edge1, edge2, color);
                    }
                }
            }
        }


        public abstract DetectedObject ToDetectedObject();
    }
}
