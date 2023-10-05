using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobMobDoorData : PxVobMobInterData
    {
        public bool locked;
        public string key = default!;
        public string pickString = default!;
    }
}
