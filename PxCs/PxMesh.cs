﻿using PxCs.Data;
using PxCs.Types;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs
{
    public static class PxMesh
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxMshLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMshLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMshDestroy(IntPtr msh);

        [DllImport(DLLNAME)] public static extern string pxMshGetName(string msh);
        [DllImport(DLLNAME)] public static extern PxAABB pxMshGetBbox(IntPtr msh);
        [DllImport(DLLNAME)] public static extern uint pxMshGetMaterialCount(IntPtr msh);
        [DllImport(DLLNAME)] public static extern IntPtr pxMshGetMaterial(IntPtr msh, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMshGetVertexCount(IntPtr msh);
        [DllImport(DLLNAME)] public static extern Vector3 pxMshGetVertex(IntPtr msh, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMshGetFeatureCount(IntPtr msh);
        [DllImport(DLLNAME)] public static extern void pxMshGetFeature(IntPtr msh, uint i, out Vector2 texture, out uint light, out Vector3 normal);

        [DllImport(DLLNAME)] public static extern IntPtr pxMshGetPolygonMaterialIndices(IntPtr msh, out uint length);
        [DllImport(DLLNAME)] public static extern IntPtr pxMshGetPolygonFeatureIndices(IntPtr msh, out uint length);
        [DllImport(DLLNAME)] public static extern IntPtr pxMshGetPolygonVertexIndices(IntPtr msh, out uint length);


        // FIXME: We're not supporting uint as of now. Need to throw exception if size of elements is too big.
        public static int[] GetPolygonVertexIndices(IntPtr msh)
        {
            var arrayPtr = PxMesh.pxMshGetPolygonVertexIndices(msh, out uint length);
            var array = new int[length];

            Marshal.Copy(arrayPtr, array, 0, (int)length);

            return array;
        }

        // FIXME: We're not supporting uint as of now. Need to throw exception if size of elements is too big.
        public static int[] GetPolygonMaterialIndices(IntPtr msh)
        {
            var arrayPtr = PxMesh.pxMshGetPolygonMaterialIndices(msh, out uint length);
            var array = new int[length];

            Marshal.Copy(arrayPtr, array, 0, (int)length);

            return array;
        }

        // FIXME: We're not supporting uint as of now. Need to throw exception if size of elements is too big.
        public static int[] GetPolygonFeatureIndices(IntPtr msh)
        {
            var arrayPtr = PxMesh.pxMshGetPolygonFeatureIndices(msh, out uint length);
            var array = new int[length];

            Marshal.Copy(arrayPtr, array, 0, (int)length);

            return array;
        }

        public static Vector3[] GetVertices(IntPtr msh)
        {
            var count = pxMshGetVertexCount(msh);
            var array = new Vector3[count];

            for (var i = 0u; i < count; i++)
            {
                array[i] = (pxMshGetVertex(msh, i));
            }

            return array;
        }

        public static PxFeatureData[] GetFeatures(IntPtr msh)
        {
            var count = pxMshGetFeatureCount(msh);
            var array = new PxFeatureData[count];

            for (var i = 0u; i < count; i++)
            {
                pxMshGetFeature(msh, i, out Vector2 texture, out uint light, out Vector3 normal);

                array[i] = new PxFeatureData()
                {
                    texture = texture,
                    light = light,
                    normal = normal
                };
            }

            return array;
        }

        public static PxMaterialData[] GetMaterials(IntPtr msh)
        {
            var count = pxMshGetMaterialCount(msh);
            var array = new PxMaterialData[count];

            for (var i = 0u; i < count; i++)
            {
                var matPtr = pxMshGetMaterial(msh, i);

                array[i] = PxMaterial.GetMaterial(matPtr);
            }

            return array;
        }
    }
}
