using Phoenix.Csharp.Interface.Types;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Phoenix.Csharp.Interface
{
    public static class Mesh
    {
        private const string DLLNAME = Phoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxMshLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMshLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMshDestroy(IntPtr msh);

        [DllImport(DLLNAME)] public static extern string pxMshGetName(string msh);
        [DllImport(DLLNAME)] public static extern AABB pxMshGetBbox(IntPtr msh);
        [DllImport(DLLNAME)] public static extern uint PxMshGetMaterialCount(IntPtr msh);
        [DllImport(DLLNAME)] public static extern IntPtr pxMshGetMaterial(IntPtr msh, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMshGetVertexCount(IntPtr msh);
        [DllImport(DLLNAME)] public static extern Vector3 pxMshGetVertex(IntPtr msh, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMshGetFeatureCount(IntPtr msh);
        [DllImport(DLLNAME)] public static extern void pxMshGetFeature(IntPtr msh, uint i, out Vector2 texture, out uint light, out Vector3 normal);
        
        /*[DllImport(DLLNAME)]*/ public static /*extern*/ void pxMshGetPolygonMaterialIndices(IntPtr msh, out uint length) { throw new NotImplementedException("FIXME returns an array (aka uint32_t const* pointers) - We need to check if this is possible..."); }
        /*[DllImport(DLLNAME)]*/ public static /*extern*/ void pxMshGetPolygonFeatureIndices(IntPtr msh, out uint length) { throw new NotImplementedException("FIXME returns an array (aka uint32_t const* pointers) - We need to check if this is possible..."); }
        /*[DllImport(DLLNAME)]*/ public static /*extern*/ void pxMshGetPolygonVertexIndices(IntPtr msh, out uint length) { throw new NotImplementedException("FIXME returns an array (aka uint32_t const* pointers) - We need to check if this is possible..."); }
    }

}
