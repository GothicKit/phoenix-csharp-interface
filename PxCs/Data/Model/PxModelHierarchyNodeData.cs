using PxCs.Data.Struct;
using System;
using System.Numerics;

namespace PxCs.Data.Model
{
    public class PxModelHierarchyNodeData
    {
        public short parentIndex;
        public string? name;
        public PxMatrix4x4Data transform;
    }
}
