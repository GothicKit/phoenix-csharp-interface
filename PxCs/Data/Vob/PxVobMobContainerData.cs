using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobMobContainerData : PxVobMobInterData
    {
        public bool locked;
        public string key;
        public string pickString;
        public string contents;
    }
}
