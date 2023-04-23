using static PxCs.PxModelScript;

namespace PxCs.Data.ModelScript
{
    public class PxAnimationCombinationData
    {
        public string? name;
        public uint layer;
        public string? next;
        public float blendIn;
        public float blendOut;
        public PxAnimationFlags flags;
        public string? model;
        public int lastFrame;
    }
}
