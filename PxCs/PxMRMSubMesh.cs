using PxCs.Data.Mesh;
using PxCs.Data.Mesh.Misc;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

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


        public static PxMultiResolutionMeshSubMeshData[] GetSubMeshes(IntPtr mrmPtr)
        {
            var count = pxMrmGetSubMeshCount(mrmPtr);
            var array = new PxMultiResolutionMeshSubMeshData[count];

            for (var i = 0u; i < count; i++)
            {
                var subMeshPtr = pxMrmGetSubMesh(mrmPtr, i);
                array[i] = GetSubMesh(subMeshPtr);
            }

            return array;
        }

        public static PxMultiResolutionMeshSubMeshData GetSubMesh(IntPtr subMeshPtr)
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

            return new PxMultiResolutionMeshSubMeshData()
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

        public static PxTriangleData[] GetSubMeshTriangles(IntPtr subMeshPtr)
        {
            var length = pxMrmSubMeshGetTriangleCount(subMeshPtr);
            var array = new PxTriangleData[length];

            for (var i = 0u; i < length; i++)
            {
                pxMrmSubMeshGetTriangle(subMeshPtr, i, out ushort a, out ushort b, out ushort c);

                array[i] = new PxTriangleData()
                {
                    a = a,
                    b = b,
                    c = c
                };
            }

            return array;
        }

        public static PxTrianglePlaneData[] GetSubMeshTrianglePlanes(IntPtr subMeshPtr)
        {
            var length = pxMrmSubMeshGetTrianglePlaneCount(subMeshPtr);
            var array = new PxTrianglePlaneData[length];

            for (var i = 0u; i < length; i++)
            {
                pxMrmSubMeshGetTrianglePlane(subMeshPtr, i, out float distance, out Vector3 normal);

                array[i] = new PxTrianglePlaneData()
                {
                    distance = distance,
                    normal = normal
                };
            }

            return array;
        }

        public static  PxTriangleEdgeData[] GetSubMeshTriangleEdges(IntPtr subMeshPtr)
        {
            var length = pxMrmSubMeshGetTriangleEdgeCount(subMeshPtr);
            var array = new PxTriangleEdgeData[length];

            for (var i = 0u; i < length; i++)
            {
                pxMrmSubMeshGetTriangleEdge(subMeshPtr, i, out ushort a, out ushort b, out ushort c);

                array[i] = new PxTriangleEdgeData()
                {
                    a = a,
                    b = b,
                    c = c
                };
            }

            return array;
        }

        public static PxWedgeData[] GetSubMeshWedges(IntPtr subMeshPtr)
        {
            var length = pxMrmSubMeshGetWedgeCount(subMeshPtr);
            var array = new PxWedgeData[length];

            for (var i = 0u; i < length; i++)
            {
                pxMrmSubMeshGetWedge(subMeshPtr, i, out Vector3 normal, out Vector2 texture, out ushort index);

                array[i] = new PxWedgeData()
                {
                    normal = normal,
                    texture = texture,
                    index = index
                };
            }

            return array;
        }

        public static PxEdgeData[] GetSubMeshEdges(IntPtr subMeshPtr)
        {
            var length = pxMrmSubMeshGetTriangleEdgeCount(subMeshPtr);
            var array = new PxEdgeData[length];

            for (var i = 0u; i < length; i++)
            {
                pxMrmSubMeshGetEdge(subMeshPtr, i, out ushort a, out ushort b);

                array[i] = new PxEdgeData()
                {
                    a = a,
                    b = b
                };
            }

            return array;
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
