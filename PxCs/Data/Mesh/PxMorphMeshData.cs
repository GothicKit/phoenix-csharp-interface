using System;
using System.Numerics;

namespace PxCs.Data.Mesh
{
    [Serializable]
    public class PxMorphMeshData
    {
        public string name = default!;
        public PxMultiResolutionMeshData mesh = default!;
        public Vector3[] positions = default!;
        public PxMorphAnimationData[] animations = default!;
    }
}
