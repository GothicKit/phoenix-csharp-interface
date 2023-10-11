using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobPfxControllerData : PxVobData
    {
        public string pfxName = default!;
        public bool killWhenDone;
        public bool initiallyRunning;
    }
}
