using PxCs.Data.Mesh.Misc;

namespace PxCs.Data.Mesh
{
    public class PxMRMSubMeshData
    {
        public PxMaterialData? material;

        public float[]? colors;

        // Triangles
        public PxTriangleData[]? triangles;
        public PxTrianglePlaneData[]? trianglePlanes;
        public PxTriangleEdgeData[]? triangleEdges;
        public ushort[]? trianglePlaneIndices;

        // Wedges
        public PxWedgeData[]? wedges;
        public ushort[]? wedgeMap;

        // Edges
        public PxEdgeData[]? edges;
        public float[]? edgeScores;
    }
}
