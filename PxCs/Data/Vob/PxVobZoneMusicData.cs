using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobZoneMusicData : PxVobData
    {
        public bool enabled;
        public int priority;
        public bool ellipsoid;
        public float reverb;
        public float volume;
        public bool loop;
    }
}
