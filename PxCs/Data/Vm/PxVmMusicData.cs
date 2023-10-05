using System;

namespace PxCs.Data.Vm
{
    [Serializable]
    public class PxVmMusicData : PxVmData
    {
        public string file = default!;
        public float vol;
        public int loop;
        public float reverbMix;
        public float reverbTime;
        public int transitionType;
        public int transitionSubType;
    }
}
