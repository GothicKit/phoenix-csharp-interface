using System.Collections.Generic;
using PxCs.Data.Mesh;

namespace PxCs.Data.Model
{
    public class PxModelMeshData
    {
        public uint checksum;

        public PxSoftSkinMeshData[]? meshes;
        public Dictionary<string, PxMultiResolutionMeshData>? attachments; // FIXME - No clue how to load them from phoenix via string.name right now!
    }
}
