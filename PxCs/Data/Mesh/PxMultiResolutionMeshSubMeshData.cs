namespace PxCs.Data.Mesh
{
    public class PxMultiResolutionMeshSubMeshData
    {
        public PxMaterialData material = default!;

        public float[] colors = default!;

        // Triangles
        public PxTriangleData[] triangles = default!;
        public PxTrianglePlaneData[] trianglePlanes = default!;
        public PxTriangleEdgeData[] triangleEdges = default!;
        public ushort[] trianglePlaneIndices = default!;

        // Wedges
        public PxWedgeData[] wedges = default!;
        public ushort[] wedgeMap = default!;

        // Edges
        public PxEdgeData[] edges = default!;
        public float[] edgeScores = default!;
    }
}
