using System.Numerics;

namespace PxCs.Data.WayNet
{
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
