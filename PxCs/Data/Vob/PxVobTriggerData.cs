namespace PxCs.Data.Vob
{
    public class PxVobTriggerData : PxVobData
    {
        public string? target;
        public uint flags;
        public uint filterFlags;
        public string? vobTarget;
        public int maxActivationCount;
        public float retriggerDelaySec;
        public float damageThreshold;
        public float fireDelaySec;

        // Save-game only variables

        public float sNextTimeTriggerable = 0;
        public int sCountCanBeActivated = 0;
        public bool sIsEnabled = true;
    }
}
