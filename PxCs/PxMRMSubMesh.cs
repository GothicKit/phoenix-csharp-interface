using PxCs.Data;
using PxCs.Extensions;
using PxCs.Types;
using System;
using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices;
using static PxCs.Data.PxMRMSubMeshData;

namespace PxCs
{
    /// <summary>
    /// MRM == MultiResolutionMesh
    /// </summary>
    public class PxMRMSubMesh
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern uint pxMrmGetSubMeshCount(IntPtr mrm);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmGetSubMesh(IntPtr mrm, uint i);

        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetMaterial(IntPtr sub);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetTriangleCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetTriangle(IntPtr sub, uint i, out ushort a, out ushort b, out ushort c);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetWedgeCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetWedge(IntPtr sub, uint i, out Vector3 normal, out Vector2 texture, out ushort index);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetColors(IntPtr sub, out uint length);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetTrianglePlaneIndices(IntPtr sub, out uint length);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetTrianglePlaneCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetTrianglePlane(IntPtr sub, uint i, out float distance, out Vector3 normal);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetTriangleEdgeCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetTriangleEdge(IntPtr sub, uint i, out ushort a, out ushort b, out ushort c);
        [DllImport(DLLNAME)] public static extern uint pxMrmSubMeshGetEdgeCount(IntPtr sub);
        [DllImport(DLLNAME)] public static extern void pxMrmSubMeshGetEdge(IntPtr sub, uint i, out ushort a, out ushort b);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetEdgeScores(IntPtr sub, out uint length);
        [DllImport(DLLNAME)] public static extern IntPtr pxMrmSubMeshGetWedgeMap(IntPtr sub, out uint length);


        public static PxMRMSubMeshData[] GetSubMeshes(IntPtr mrmPtr)
        {
            var count = pxMrmGetSubMeshCount(mrmPtr);
            var array = new PxMRMSubMeshData[count];

            for (var i = 0u; i < count; i++)
            {
                var subMeshPtr = pxMrmGetSubMesh(mrmPtr, i);
                array[i] = GetSubMesh(subMeshPtr);
            }

            return array;
        }

        public static PxMRMSubMeshData GetSubMesh(IntPtr subMeshPtr)
        {
            var materialPtr = pxMrmSubMeshGetMaterial(subMeshPtr);
            var material = PxMaterial.GetMaterial(materialPtr);

            var triangles = GetSubMeshTriangles(subMeshPtr);
            var trianglePlanes = GetSubMeshTrianglePlanes(subMeshPtr);
            var triangleEdges = GetSubMeshTriangleEdges(subMeshPtr);
            var trianglePlaneIndices = GetSubMeshTrianglePlaneIndices(subMeshPtr);

            var wedges = GetSubMeshWedges(subMeshPtr);
            var wedgeMap = GetSubMeshWedgeMap(subMeshPtr);

            var edges = GetSubMeshEdges(subMeshPtr);
            var edgeScores = GetSubMeshEdgeScores(subMeshPtr);
            
            var colors = GetSubMeshColors(subMeshPtr);

            return new PxMRMSubMeshData()
            {
                material = material,

                colors = colors,

                triangles = triangles,
                trianglePlanes = trianglePlanes,
                triangleEdges = triangleEdges,
                trianglePlaneIndices = trianglePlaneIndices,

                wedges = wedges,
                wedgeMap = wedgeMap,

                edges = edges,
                edgeScores = edgeScores
            };
        }

        public static Triangle[] GetSubMeshTriangles(IntPtr subMeshPtr)
        {
            var length = pxMrmSubMeshGetTriangleCount(subMeshPtr);
            var array = new T[length];

            return null;
        }

        public static TrianglePlane[] GetSubMeshTrianglePlanes(IntPtr subMeshPtr)
        {
            return null;
        }

        public static  TriangleEdge[] GetSubMeshTriangleEdges(IntPtr subMeshPtr)
        {
            return null;
        }

        public static Wedge[] GetSubMeshWedges(IntPtr subMeshPtr)
        {
            return null;
        }

        public static Edge[] GetSubMeshEdges(IntPtr subMeshPtr)
        {
            return null;
        }

        public static float[] GetSubMeshColors(IntPtr subMeshPtr)
        {
            return pxMrmSubMeshGetColors(subMeshPtr, out uint length).MarshalAsArray<float>(length);
        }

        public static float[] GetSubMeshEdgeScores(IntPtr subMeshPtr)
        {
            return pxMrmSubMeshGetEdgeScores(subMeshPtr, out uint length).MarshalAsArray<float>(length);
        }

        public static ushort[] GetSubMeshTrianglePlaneIndices(IntPtr msh)
        {
            return pxMrmSubMeshGetTrianglePlaneIndices(msh, out uint length).MarshalAsArray<ushort>(length);
        }

        public static ushort[] GetSubMeshWedgeMap(IntPtr msh)
        {
            return pxMrmSubMeshGetWedgeMap(msh, out uint length).MarshalAsArray<ushort>(length);
        }
    }
}
