using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobMobContainerData : PxVobMobInterData
    {
        public bool locked;
        public string key = default!;
        public string pickString = default!;
        public string contents = default!;
    }
}
