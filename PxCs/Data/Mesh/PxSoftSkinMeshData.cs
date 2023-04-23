using PxCs.Data.Mesh.Misc;
using PxCs.Data.Misc;

namespace PxCs.Data.Mesh
{
    public class PxSoftSkinMeshData
    {
        public PxProtoMeshData? mesh;
        public PxOBBData? bboxes;

        public PxWedgeNormalData[]? wedgeNormals;
        public int[]? nodes;

        public PxWeightEntryData[,]? weights;
    }
}
