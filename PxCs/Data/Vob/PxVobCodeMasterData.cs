using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobCodeMasterData : PxVobData
    {
        public string target = default!;
        public bool ordered;
        public bool firstFalseIsFailure;
        public string failureTarget = default!;
        public bool untriggeredCancels;
        public string[]? slaves;

    }
}
