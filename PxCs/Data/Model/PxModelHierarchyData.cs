using PxCs.Data.Struct;
using System.Numerics;

namespace PxCs.Data.Model
{
    public class PxModelHierarchyData
    {
        public PxModelHierarchyNodeData[] nodes = default!;
        public PxAABBData bbox;
        public Vector3 rootTranslation;
        public uint checksum;
    }
}
