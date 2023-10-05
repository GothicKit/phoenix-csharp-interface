using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobTriggerChangeLevelData : PxVobTriggerData
    {
        public string levelName = default!;
        public string startVob = default!;
    }
}
