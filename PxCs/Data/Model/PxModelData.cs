using System;

namespace PxCs.Data.Model
{
    [Serializable]
    public class PxModelData
    {
        public PxModelHierarchyData hierarchy = default!;
        public PxModelMeshData mesh = default!;
    }
}
