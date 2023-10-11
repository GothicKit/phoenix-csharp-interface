using System;
using System.Numerics;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobEarthQuakeData : PxVobData
    {
        public float radius;
        public float duration;
        public Vector3 amplitude;
    }
}
