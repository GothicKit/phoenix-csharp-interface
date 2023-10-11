using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobTouchDamageData : PxVobData
    {
        public float damage;
        public bool barrier;
        public bool blunt;
        public bool edge;
        public bool fire;
        public bool fly;
        public bool magic;
        public bool point;
        public bool fall;
        public float repeatDelaySec;
        public float volumeScale;
        public uint collision;
    }
}
