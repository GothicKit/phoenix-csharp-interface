using System;

namespace PxCs.Data.Event
{
    [Serializable]
    public class PxEventPfxData
    {
        public int frame;
        public int index;
        public string name = default!;
        public string position = default!;
        public bool attached;
    }
}
