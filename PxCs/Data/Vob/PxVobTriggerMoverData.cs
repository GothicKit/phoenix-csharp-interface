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

        public PxAnimationSampleData[] keyframes = default!;

        public string sfxOpenStart = default!;
        public string sfxOpenEnd = default!;
        public string sfxTransitioning = default!;
        public string sfxCloseStart = default!;
        public string sfxCloseEnd = default!;
        public string sfxLock = default!;
        public string sfxUnlock = default!;
        public string sfxUseLocked = default!;

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
