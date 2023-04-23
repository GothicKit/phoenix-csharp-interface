using PxCs.Data.Misc;
using System.Numerics;

namespace PxCs.Data.Mesh
{
    /// <summary>
    /// MRM == MultiResolutionMesh
    /// </summary>
    public class PxMRMData
    {
        public Vector3[]? positions;
        public Vector3[]? normals;
        public byte alphaTest;
        public PxAABBData? bbox;

        public PxMaterialData[]? materials;
        public PxMRMSubMeshData[]? subMeshes;
    }
}
