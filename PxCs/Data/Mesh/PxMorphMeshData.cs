using System.Numerics;

namespace PxCs.Data.Mesh
{
    public class PxMorphMeshData
    {
        public string? name;
        public PxMultiResolutionMeshData? mesh;
        public Vector3[]? positions;
        public PxMorphAnimationData[]? animations;
    }
}
