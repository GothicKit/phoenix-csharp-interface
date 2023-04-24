using static PxCs.PxModelScript;

namespace PxCs.Data.ModelScript
{
    public class PxModelScriptAnimationData
    {
        public string? name;
        public uint layer;
        public string? next;
        public float blendIn;
        public float blendOut;
        public PxAnimationFlags flags;
        public string? model;
        public PxAnimationDirection direction;
        public int firstFrame;
        public int lastFrame;
        public float fps;
        public float speed;
        public float collisionVolumeScale;

        public PxEventTagData[]? events;
        public PxEventPfxData[]? pfx;
        public PxEventPfxStopData[]? pfxStop;
        public PxEventSfxData[]? sfx;
        public PxEventSfxGroundData[]? sfxGround;
        public PxEventMorphAnimateData[]? morph;
        public PxEventCameraTremorData[]? tremors;
    }
}
