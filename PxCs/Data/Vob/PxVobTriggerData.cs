using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobTriggerData : PxVobData
    {
        public string target = default!;
        public uint flags;
        public uint filterFlags;
        public string vobTarget = default!;
        public int maxActivationCount;
        public float retriggerDelaySec;
        public float damageThreshold;
        public float fireDelaySec;

        // Save-game only variables

        public float sNextTimeTriggerable;
        public int sCountCanBeActivated;
        public bool sIsEnabled;
    }
}
