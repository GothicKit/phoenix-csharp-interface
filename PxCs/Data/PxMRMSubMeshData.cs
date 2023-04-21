using System.Numerics;
using System.Runtime.InteropServices;
using System;

namespace PxCs.Data
{
    public class PxMRMSubMeshData
    {
        public class Triangle
        {
            public ushort b;
            public ushort c;
            public ushort a;
        }

        public class TrianglePlane
        {
            public float distance;
            public Vector3 normal;
        }

        public class TriangleEdge
        {
            public ushort a;
            public ushort b;
            public ushort c;
        }

        public class Wedge
        {
            public Vector3 normal;
            public Vector2 texture;
            public ushort index;
        }

        public class Edge
        {
            public ushort a;
            public ushort b;
        }



        public PxMaterialData? material;

        public float[] colors = default!;

        // Triangles
        public Triangle[] triangles = default!;
        public TrianglePlane[] trianglePlanes = default!;
        public TriangleEdge[] triangleEdges = default!;
        public ushort[] trianglePlaneIndices = default!;

        // Wedges
        public Wedge[] wedges = default!;
        public ushort[] wedgeMap = default!;

        // Edges
        public Edge[] edges = default!;
        public float[] edgeScores = default!;
    }
}
