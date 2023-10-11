using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobMoverControllerData : PxVobData
    {
        public string target = default!;
        public uint message;
        public int key;
    }
}
