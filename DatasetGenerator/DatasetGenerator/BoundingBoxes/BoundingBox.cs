using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private Vector3[] _edges;
        public Vector3[] Edges
        {
            get => _edges;
            private set
            {
                _edges = value;
                
                //add occlusion checkpoints
                OcclusionCheckPoints = ComputeExtendedOcclusionCheckPoints();
            }
        }

        /// <summary>
        /// Provides an extended set of points on which to apply occlusion detection.
        /// Aside from box edges, it contains other points such as the center of the box and intermediate points.
        /// </summary>
        private Vector3[] OcclusionCheckPoints { get; set; }

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
            bool hitEveryTime = true;
            int hitCounter = 0;

            foreach (var edge in OcclusionCheckPoints)
            {
                var hitResult = World.TraceLine(camera.Position, edge, TraceFlags.IntersectEverything, Entity);
                if (!hitResult.Hit)
                {
                    hitEveryTime = false;
                    break;
                }
                else
                {
                    hitCounter++;
                }
            }

            Game.DisplaySubtitle("Hit counter is: " + hitCounter);
            return !hitEveryTime;
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

        private Vector3[] ComputeExtendedOcclusionCheckPoints()
        {
            var extendedPoints = new List<Vector3>(_edges) {Center};

            foreach(var edge1 in _edges)
            {
                foreach (var edge2 in _edges)
                {
                    if(edge1 != edge2)
                        extendedPoints.Add((edge1 + edge2)/2);
                }
            }

            return extendedPoints.ToArray();
        }


        public abstract DetectedObject ToDetectedObject();
    }
}
