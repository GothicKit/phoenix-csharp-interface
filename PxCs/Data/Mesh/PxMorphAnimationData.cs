using System.Numerics;

namespace PxCs.Data.Mesh
{
    public class PxMorphAnimationData
    {
        public string name = default!;
        public int layer;
        public float blendIn;
        public float blendOut;
        public float duration;
        public float speed;
        public uint flags;
        public uint frameCount;
        public int verticesCount;
        public int[] vertices = default!;
        public Vector3[] samples = default!;
    }
}
