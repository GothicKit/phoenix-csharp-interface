using System;

namespace PxCs.Data.Struct
{
    /// <summary>
    /// We can't use System.Numerics.Quaternion as it has a different order
    /// of properies (w, x, y, z) than phoenix (x, y, z, w)
    /// </summary>
    [Serializable]
    public struct PxQuaternionData
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }
}
