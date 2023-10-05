using System;
using PxCs.Data.Struct;

namespace PxCs.Data.Model
{
    [Serializable]
    public class PxModelHierarchyNodeData
    {
        public short parentIndex;
        public string name = default!;
        public PxMatrix4x4Data transform;
    }
}
