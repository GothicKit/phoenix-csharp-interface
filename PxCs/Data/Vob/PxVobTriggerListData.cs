using System.Collections.Generic;

namespace PxCs.Data.Vob
{
    public enum PxTriggerBatchMode
    {
        all = 0,
        next = 1,
        random = 2,
    };
    public class PxVobTriggerListData : PxVobTriggerData
    {
        public struct PxTarget
        {
            public string name;
            public float delay;

            public bool Equals(PxTarget tgt)
            {
                return this.name == tgt.name && this.delay == tgt.delay;
            }
        }

        public PxTriggerBatchMode mode;
        public List<PxTarget>? targets;

        // Save-game only variables
        public byte sActTarget;
        public bool sSendOnTrigger;
    }
}
