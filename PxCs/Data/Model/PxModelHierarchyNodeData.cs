using PxCs.Data.Struct;

namespace PxCs.Data.Model
{
    public class PxModelHierarchyNodeData
    {
        public short parentIndex;
        public string? name;
        public PxMatrix4x4Data transform;
    }
}
