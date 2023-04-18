using PxCs.Types;
using System.Numerics;
using System.Runtime.InteropServices;
using System;

namespace PxCs.Data
{
    /// <summary>
    /// MRM == MultiResolutionMesh
    /// </summary>
    public class PxMRMData
    {
        public Vector3[] positions = default!;
        public Vector3[] normals = default!;
        public byte alphaTest;
        public PxAABB bbox;

        public PxMaterialData[] materials = default!;
        public PxMRMSubMeshData[] subMeshes = default!;
    }
}
