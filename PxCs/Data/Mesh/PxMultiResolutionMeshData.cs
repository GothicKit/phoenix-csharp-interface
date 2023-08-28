using PxCs.Data.Struct;
using System.Numerics;

namespace PxCs.Data.Mesh
{
    public class PxMultiResolutionMeshData
    {
        public Vector3[] positions = default!;
        public Vector3[] normals = default!;
        public byte alphaTest;
        public PxAABBData bbox = default!;

        public PxMaterialData[] materials = default!;
        public PxMultiResolutionMeshSubMeshData[] subMeshes = default!;
    }
}
