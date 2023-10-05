using System;
using PxCs.Data.Event;
using static PxCs.Interface.PxModelScript;

namespace PxCs.Data.ModelScript
{
    [Serializable]
    public class PxModelScriptAnimationData
    {
        public string name = default!;
        public uint layer;
        public string next = default!;
        public float blendIn;
        public float blendOut;
        public PxAnimationFlags flags;
        public string model = default!;
        public PxAnimationDirection direction;
        public int firstFrame;
        public int lastFrame;
        public float fps;
        public float speed;
        public float collisionVolumeScale;

        public PxEventTagData[] events = default!;
        public PxEventPfxData[] pfx = default!;
        public PxEventPfxStopData[] pfxStop = default!;
        public PxEventSfxData[] sfx = default!;
        public PxEventSfxGroundData[] sfxGround = default!;
        public PxEventMorphAnimateData[] morph = default!;
        public PxEventCameraTremorData[] tremors = default!;
    }
}
