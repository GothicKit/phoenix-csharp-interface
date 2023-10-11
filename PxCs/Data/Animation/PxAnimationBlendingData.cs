using System;

namespace PxCs.Data.Animation
{
    [Serializable]
    public class PxAnimationBlendingData
    {
        public string name = default!;
        public string next = default!;
        public float blendIn;
        public float blendOut;
    }
}
