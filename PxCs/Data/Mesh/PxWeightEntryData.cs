using System;
using System.Numerics;

namespace PxCs.Data.Mesh
{
    [Serializable]
    public class PxWeightEntryData
    {
        public float weight;
        public Vector3 position;
        public byte nodeIndex;
    }
}
