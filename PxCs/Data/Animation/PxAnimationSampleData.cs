using System;
using PxCs.Data.Struct;
using System.Numerics;

namespace PxCs.Data.Animation
{
    [Serializable]
    public class PxAnimationSampleData
    {
        public Vector3 position;
        public PxQuaternionData rotation;
    }
}
