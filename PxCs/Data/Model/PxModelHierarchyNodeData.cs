using System;
using System.Numerics;

namespace PxCs.Data.Model
{
    public class PxModelHierarchyNodeData
    {
        public short parentIndex;
        public string? name;

        [Obsolete("Not yet exported from phoenix-shared-interface.")]
        public Matrix4x4 transform;
    }
}
