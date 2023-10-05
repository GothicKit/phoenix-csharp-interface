using System;
using static PxCs.Interface.PxModelScript;

namespace PxCs.Data.Animation
{
    [Serializable]
    public class PxAnimationCombinationData
    {
        public string name = default!;
        public uint layer;
        public string next = default!;
        public float blendIn;
        public float blendOut;
        public PxAnimationFlags flags;
        public string model = default!;
        public int lastFrame;
    }
}
