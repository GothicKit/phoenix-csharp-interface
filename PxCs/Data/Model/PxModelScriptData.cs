using PxCs.Data.Animation;
using PxCs.Data.ModelScript;

namespace PxCs.Data.Model
{
    public class PxModelScriptData
    {
        public PxSkeletonData? skeleton;
        public string[]? meshes;
        public string[]? disabledAnimations;
        public PxAnimationCombinationData[]? combinations;
        public PxAnimationBlendingData[]? blends;
        public PxAnimationAliasData[]? aliases;
        public PxModelTagData[]? modelTags;
        public PxModelScriptAnimationData[]? animations;
    }
}
