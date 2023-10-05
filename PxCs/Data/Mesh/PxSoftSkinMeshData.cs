using System;
using PxCs.Data.Struct;

namespace PxCs.Data.Mesh
{
    [Serializable]
    public class PxSoftSkinMeshData
    {
        public PxMultiResolutionMeshData mesh = default!;
        public PxOBBData bboxes = default!; // FIXME - not loaded from phoenix as of now!

        public PxWedgeNormalData[] wedgeNormals = default!;
        public int[] nodes = default!;

        public PxWeightEntryData[][] weights = default!;
    }
}
