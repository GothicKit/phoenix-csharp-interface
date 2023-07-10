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
    }
}
