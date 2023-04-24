using PxCs.Data.Struct;
using System.Numerics;

namespace PxCs.Data.Mesh
{
    public class PxMultiResolutionMeshData
    {
        public Vector3[]? positions;
        public Vector3[]? normals;
        public byte alphaTest;
        public PxAABBData? bbox;

        public PxMaterialData[]? materials;
        public PxMultiResolutionMeshSubMeshData[]? subMeshes;
    }
}
