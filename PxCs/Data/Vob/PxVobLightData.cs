using System;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobLightData : PxVobData
    {
        //since in c# we don't have multiple inheretance we can use this
        public PxLightPresetData? lightPreset;
    }
}
