using System;
using PxCs.Data.Struct;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobZoneFogData : PxVobData
    {
        public float rangeCenter;
        public float innerRangePercentage;
        public Vector4Byte color;
        public bool fadeOutSky;
        public bool overrideColor;
    }
}
