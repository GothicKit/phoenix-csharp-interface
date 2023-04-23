using PxCs.Data.Misc;
using System.Numerics;

namespace PxCs.Data.Mesh
{
    public class PxProtoMeshData
    {
        Vector3[]? positions;
        Vector3[]? normals;
        PxSubMeshData[]? subMeshes;
        PxMaterialData[]? materials;

        bool alphaTest;
        PxAABBData? bbox;
        PxOBBData? obbox;
    }
}
