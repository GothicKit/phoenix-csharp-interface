using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobMessageFilterData : PxVobData
    {
        public string target = default!;
        public uint onTrigger;
        public uint onUntrigger;
    }
}
