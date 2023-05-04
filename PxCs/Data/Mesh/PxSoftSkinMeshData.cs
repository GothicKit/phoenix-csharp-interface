using PxCs.Data.Struct;

namespace PxCs.Data.Mesh
{
    public class PxSoftSkinMeshData
    {
        public PxMultiResolutionMeshData? mesh;
        public PxOBBData? bboxes; // FIXME - not loaded from phoenix as of now!

        public PxWedgeNormalData[]? wedgeNormals;
        public int[]? nodes;

        public PxWeightEntryData[][]? weights;
    }
}
