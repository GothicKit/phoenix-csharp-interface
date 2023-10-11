using PxCs.Data.Mesh;
using PxCs.Data.Struct;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public static class PxMesh
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [StructLayout(LayoutKind.Sequential)]
        public struct PolygonFlags
        {
            public byte is_portal;
            public byte is_occluder;
            public byte is_sector;
            public byte should_relight;
            public byte is_outdoor;
            public byte is_ghost_occluder;
            public byte is_dynamically_lit;
            public short sector_index;
            public byte is_lod;
            public byte normal_axis;
        }

        [DllImport(DLLNAME)]
        public static extern IntPtr pxMshLoad(IntPtr buffer);

        [DllImport(DLLNAME)]
        public static extern IntPtr pxMshLoadFromVfs(IntPtr vfs, string name);

        [DllImport(DLLNAME)]
        public static extern void pxMshDestroy(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern string pxMshGetName(string msh);

        [DllImport(DLLNAME)]
        public static extern PxAABBData pxMshGetBbox(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern uint pxMshGetMaterialCount(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern IntPtr pxMshGetMaterial(IntPtr msh, uint i);

        [DllImport(DLLNAME)]
        public static extern uint pxMshGetVertexCount(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern Vector3 pxMshGetVertex(IntPtr msh, uint i);

        [DllImport(DLLNAME)]
        public static extern uint pxMshGetFeatureCount(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern void pxMshGetFeature(IntPtr msh, uint i, out Vector2 texture, out uint light,
            out Vector3 normal);

        [DllImport(DLLNAME)]
        public static extern uint pxMshGetLightMapCount(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern void pxMshGetLightMap(IntPtr msh, uint i, out IntPtr image,
            out Vector3 origin);

        [DllImport(DLLNAME)]
        public static extern IntPtr pxMshGetPolygonMaterialIndices(IntPtr msh, out uint length);

        [DllImport(DLLNAME)]
        public static extern IntPtr pxMshGetPolygonFeatureIndices(IntPtr msh, out uint length);

        [DllImport(DLLNAME)]
        public static extern IntPtr pxMshGetPolygonVertexIndices(IntPtr msh, out uint length);

        [DllImport(DLLNAME)]
        public static extern IntPtr pxMshGetPolygonLightMapIndices(IntPtr msh, out uint length);

        [DllImport(DLLNAME)]
        public static extern uint pxMshGetPolygonFlagCount(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetIsPortal(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetIsOccluder(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetIsSector(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetShouldRelight(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetIsOutdoor(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetIsGhostGccluder(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetIsDynamicallyLit(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern short pxMshGetPolygonFlagGetSector_Index(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetIsLod(IntPtr msh);

        [DllImport(DLLNAME)]
        public static extern byte pxMshGetPolygonFlagGetNormalAxis(IntPtr msh);

        public static int[] GetPolygonVertexIndices(IntPtr msh)
        {
            return pxMshGetPolygonVertexIndices(msh, out uint length).MarshalAsArray<int>(length);
        }

        public static int[] GetPolygonMaterialIndices(IntPtr msh)
        {
            return pxMshGetPolygonMaterialIndices(msh, out uint length).MarshalAsArray<int>(length);
        }

        public static int[] GetPolygonFeatureIndices(IntPtr msh)
        {
            return pxMshGetPolygonFeatureIndices(msh, out uint length).MarshalAsArray<int>(length);
        }

        public static int[] GetPolygonLightMapIndices(IntPtr msh)
        {
            return pxMshGetPolygonLightMapIndices(msh, out uint length).MarshalAsArray<int>(length);
        }

        public static Vector3[] GetVertices(IntPtr msh)
        {
            var count = pxMshGetVertexCount(msh);
            var array = new Vector3[count];

            if (count > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{count}< was given.");

            for (var i = 0u; i < count; i++)
            {
                array[i] = pxMshGetVertex(msh, i);
            }

            return array;
        }

        public static PxFeatureData[] GetFeatures(IntPtr msh)
        {
            var count = pxMshGetFeatureCount(msh);
            var array = new PxFeatureData[count];

            if (count > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{count}< was given.");

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

            if (count > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{count}< was given.");

            for (var i = 0u; i < count; i++)
            {
                var matPtr = pxMshGetMaterial(msh, i);

                array[i] = PxMaterial.GetMaterial(matPtr);
            }

            return array;
        }

        public static PxLightMapData[] GetLightMaps(IntPtr msh)
        {
            var count = pxMshGetLightMapCount(msh);
            var array = new PxLightMapData[count];

            if (count > int.MaxValue)
                throw new ArgumentOutOfRangeException(
                    $"We can only handle int.MaxValue of elements but >{count}< was given.");

            for (var i = 0u; i < count; i++)
            {
                pxMshGetLightMap(msh, i, out IntPtr texturePtr, out Vector3 origin);

                
                var image = PxTexture.GetTextureFromPtr(texturePtr, true, PxTexture.Format.tex_R5G6B5);

                array[i] = new PxLightMapData()
                {
                    image = image,
                    // normals = normals,
                    origin = origin
                };
            }

            return array;
        }
    }
}