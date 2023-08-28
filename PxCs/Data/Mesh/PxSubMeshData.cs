namespace PxCs.Data.Mesh
{
    public class PxSubMeshData
    {
        float[] colors = default!;

        PxTriangleData[] triangles = default!;
        PxTrianglePlaneData[] trianglePlanes = default!;
        ushort[] trianglePlaneIndices = default!;
        PxTriangleEdgeData[] triangleEdges = default!;

        PxEdgeData[] edges = default!;
        float[] edgeScores = default!;

        PxWedgeData[] wedges = default!;
        ushort[] wedgeMap = default!;
    }
}
