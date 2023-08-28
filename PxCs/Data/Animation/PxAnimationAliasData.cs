﻿using static PxCs.Interface.PxModelScript;

namespace PxCs.Data.Animation
{
    public class PxAnimationAliasData
    {
        public string name = default!;
        public uint layer;
        public string next = default!;
        public float blendIn;
        public float blendOut;
        public PxAnimationFlags flags;
        public string alias = default!;
        public PxAnimationDirection direction;
    }
}
