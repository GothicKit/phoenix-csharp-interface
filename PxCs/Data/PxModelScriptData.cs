using static PxCs.PxModelScript;

namespace PxCs.Data
{
    public class PxModelScriptData
    {
        public PxSkeleton skeleton;
        public string[] meshes = default!;
        public string[] disabledAnimations = default!;
        public PxAnimationCombination[] combinations = default!;
        public PxAnimationBlending[] blends = default!;
        public PxAnimationAlias[] aliases = default!;
        public PxModelTag[] model_tags = default!;
        public PxAnimation[] animations = default!;


        public struct PxSkeleton
        {
            public string name;
            public bool disableMesh;
        }

        public struct PxAnimationCombination
        {
            public string name;
            public uint layer;
            public string next;
            public float blend_in;
            public float blend_out;
            public PxAnimationFlags flags;
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
            public PxAnimationFlags flags;
            public string alias;
            public PxAnimationDirection direction;
        }

        public struct PxModelTag
        {
            public string bone;
        }

        public struct PxAnimation
        {
            public string name;
            public uint layer;
            public string next;
            public float blendIn;
            public float blendOut;
            public PxAnimationFlags flags;
            public string model;
            public PxAnimationDirection direction;
            public int firstFrame;
            public int lastFrame;
            public float fps;
            public float speed;
            public float collisionVolumeScale;

            public PxEventTag[] events;
            public PxEventPfx[] pfx;
            public PxEventPfxStop[] pfx_stop;
            public PxEventSfx[] sfx;
            public PxEventSfxGround[] sfx_ground;
            public PxEventMorphAnimate[] morph;
            public PxEventCameraTremor[] tremors;
        }

        public struct PxEventTag
        {
            public int frame;
            public PxEventTagType type;
            public string slot;
            public string slot2;
            public string item;
            public int[] frames;
            public PxEventFightMode fightMode;
            public bool attached;
        };

        public struct PxEventPfx
        {
            public int frame;
            public int index;
            public string name;
            public string position;
            public bool attached;
        };

        public struct PxEventPfxStop
        {
            public int frame;
            public int index;
        };

        public struct PxEventSfx
        {
            public int frame;
            public string name;
            public float range;
            public bool emptySlot;
        };

        public struct PxEventSfxGround
        {
            public int frame;
            public string name;
            public float range;
            public bool emptySlot;
        };

        public struct PxEventMorphAnimate
        {
            public int frame;
            public string animation;
            public string node;
        };

        public struct PxEventCameraTremor
        {
            public int frame;
            public int field1;
            public int field2;
            public int field3;
            public int field4;
        };
    }
}
