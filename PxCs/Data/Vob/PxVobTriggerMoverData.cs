using System.Collections.Generic;
using System.Numerics;
using PxCs.Data.Animation;
using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    public class PxVobTriggerMoverData : PxVobTriggerData
    {
        public PxVobTriggerMoverBehaviour behaviour = PxVobTriggerMoverBehaviour.PxVobTriggerMoverBehaviourToggle;
        public float touchBlockerDamage = 0;
        public float stayOpenTimeSec = 0;
        public bool locked = true;
        public bool autoLink = false;
        public bool autoRotate = false;

        public float speed = 0;
        public PxVobTriggerMoverLerpMode lerpMode = PxVobTriggerMoverLerpMode.PxVobTriggerMoverLerpModeCurve;
        public PxVobTriggerMoverSpeedMode speedMode = PxVobTriggerMoverSpeedMode.PxVobTriggerMoverSpeedModeSegConstant;

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
