using System;
using System.Numerics;

namespace PxCs.Data.WayNet
{
    [Serializable]
    public class PxWayPointData
    {
        public string name = "";
        public Vector3 position;
        public Vector3 direction;
        public bool freePoint;
        public bool underwater;
        public int waterDepth;
    }
}
