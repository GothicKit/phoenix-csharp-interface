using PxCs.Data.Mesh.Misc;

namespace PxCs.Data.Mesh
{
    public class PxSubMeshData
    {
        float[]? colors;

        PxTriangleData[]? triangles;
        PxTrianglePlaneData[]? trianglePlanes;
        ushort[]? trianglePlaneIndices;
        PxTriangleEdgeData[]? triangleEdges;

        PxEdgeData[]? edges;
        float[]? edgeScores;

        PxWedgeData[]? wedges;
        ushort[]? wedgeMap;
    }
}
