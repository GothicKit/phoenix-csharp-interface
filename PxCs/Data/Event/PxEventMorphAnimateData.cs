using System;

namespace PxCs.Data.Event
{
    [Serializable]
    public class PxEventMorphAnimateData
    {
        public int frame;
        public string animation = default!;
        public string node = default!;
    }
}
