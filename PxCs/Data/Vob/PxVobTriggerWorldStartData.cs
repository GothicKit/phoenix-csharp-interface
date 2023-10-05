using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobTriggerWorldStartData : PxVobData
    {
        public string target = default!;
        public bool fireOnce;

        // Save-game only variables
        public bool sHasFired;
    }
}
