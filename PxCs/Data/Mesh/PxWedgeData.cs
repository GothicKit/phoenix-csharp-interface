using System;
using System.Numerics;

namespace PxCs.Data.Mesh
{
    [Serializable]
    public class PxWedgeData
    {
        public Vector3 normal;
        public Vector2 texture;
        public ushort index;
    }
}
