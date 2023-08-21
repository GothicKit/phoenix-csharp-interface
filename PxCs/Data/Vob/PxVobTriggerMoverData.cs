using System.Collections.Generic;
using System.Numerics;
using PxCs.Data.Animation;
using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    public class PxVobTriggerMoverData : PxVobTriggerData
    {
        public PxVobTriggerMoverBehaviour behaviour;
        public float touchBlockerDamage;
        public float stayOpenTimeSec;
        public bool locked;
        public bool autoLink;
        public bool autoRotate;

        public float speed;
        public PxVobTriggerMoverLerpMode lerpMode;
        public PxVobTriggerMoverSpeedMode speedMode;

        public PxAnimationSampleData[]? keyframes;

        public string? sfxOpenStart;
        public string? sfxOpenEnd;
        public string? sfxTransitioning;
        public string? sfxCloseStart;
        public string? sfxCloseEnd;
        public string? sfxLock;
        public string? sfxUnlock;
        public string? sfxUseLocked;

        // Save-game only variables
        public Vector3 sActKeyPosDelta;
        public float sActKeyframeF;
        public int sActKeyframe;
        public int sNextKeyframe;
        public float sMoveSpeedUnit;
        public float sAdvanceDir;
        public uint sMoverState;
        public int sTriggerEventCount;
        public float sStayOpenTimeDest;
    }
}
