using System;
using PxCs.Data.Animation;
using PxCs.Data.ModelScript;

namespace PxCs.Data.Model
{
    [Serializable]
    public class PxModelScriptData
    {
        public PxSkeletonData skeleton = default!;
        public string[] meshes = default!;
        public string[] disabledAnimations = default!;
        public PxAnimationCombinationData[] combinations = default!;
        public PxAnimationBlendingData[] blends = default!;
        public PxAnimationAliasData[] aliases = default!;
        public PxModelTagData[] modelTags = default!;
        public PxModelScriptAnimationData[] animations = default!;
    }
}
