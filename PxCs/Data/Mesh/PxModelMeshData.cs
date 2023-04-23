using System.Collections.Generic;

namespace PxCs.Data.Mesh
{
    public class PxModelMeshData
    {
        public uint checksum;

        public PxSoftSkinMeshData[]? meshes;
        public Dictionary<string, PxMultiResolutionMeshData>? attachments;
    }
}
