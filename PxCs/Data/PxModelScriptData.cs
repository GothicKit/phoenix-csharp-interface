namespace PxCs.Data
{
    public class PxModelScriptData
    {
        public struct PxAnimationCombination
        {
            public string name;
            public uint layer;
            public string next;
            public float blend_in;
            public float blend_out;
            public PxModelScript.PxAnimationFlags flags;
            public string model;
            public int last_frame;
        }

        public struct PxAnimationBlending
        {
            public string name;
            public string next;
            public float blend_in;
            public float blend_out;
        }

        public struct PxAnimationAlias
        {
            public string name;
            public uint layer;
            public string next;
            public float blend_in;
            public float blend_out;
            public PxModelScript.PxAnimationFlags flags;
            public string alias;
            public PxModelScript.PxAnimationDirection direction;
        }

        public struct PxModelTag
        {
            public string bone;
        }


        public string[] meshes = default!;
        public string[] disabled_animations = default!;
        public PxAnimationCombination[] combinations = default!;
        public PxAnimationBlending[] blends = default!;
        public PxAnimationAlias[] aliases = default!;
        public PxModelTag[] model_tags = default!;


        // TODO - Implement
        // mds::skeleton skeleton {};
        // std::vector<mds::animation> animations {};
    }
}
