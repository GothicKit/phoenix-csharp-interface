using System.Collections.Generic;
using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    public class PxVobTriggerListData : PxVobTriggerData
    {
        public struct PxTarget
        {
            public string name;
            public float delay;
        }

        public PxVobTriggerBatchMode mode;
        public PxTarget[] targets = default!;

        // Save-game only variables
        public byte sActTarget;
        public bool sSendOnTrigger;
    }
}
