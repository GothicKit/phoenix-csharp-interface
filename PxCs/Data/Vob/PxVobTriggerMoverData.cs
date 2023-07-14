using System.Collections.Generic;
using System.Numerics;
using PxCs.Data.Animation;

namespace PxCs.Data.Vob
{
    public enum PxMoverBehavior : uint
    {
        toggle = 0,
        triggerControl = 1,
        openTimed = 2,
        loop = 3,
        singleKeys = 4,
    }

    public enum PxMoverLerpMode : uint
    {
        curve = 0,
        linear = 1,
    }

    public enum PxMoverSpeedMode : uint
    {
        segConstant = 0,
        slowStartEnd = 1,
        slowStart = 2,
        slowEnd = 3,
        segSlowStartEnd = 4,
        segSlowStart = 5,
        segSlowEnd = 6,
    }

    public class PxVobTriggerMoverData : PxVobTriggerData
    {
        public PxMoverBehavior behaviour = PxMoverBehavior.toggle;
        public float touchBlockerDamage = 0;
        public float stayOpenTimeSec = 0;
        public bool locked = true;
        public bool autoLink = false;
        public bool autoRotate = false;

        public float speed = 0;
        public PxMoverLerpMode lerpMode = PxMoverLerpMode.curve;
        public PxMoverSpeedMode speedMode = PxMoverSpeedMode.segConstant;

        public List<PxAnimationSampleData>? keyframes;

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
