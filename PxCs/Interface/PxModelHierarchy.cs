using PxCs.Data.Model;
using PxCs.Data.Struct;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public class PxModelHierarchy
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;


        [DllImport(DLLNAME)] public static extern IntPtr pxMdhLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdhLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMdhDestroy(IntPtr mdh);

        [DllImport(DLLNAME)] public static extern PxAABBData pxMdhGetBbox(IntPtr mdh);
        [DllImport(DLLNAME)] public static extern PxAABBData pxMdhGetCollisionBbox(IntPtr mdh);
        [DllImport(DLLNAME)] public static extern Vector3 pxMdhGetRootTranslation(IntPtr mdh);
        [DllImport(DLLNAME)] public static extern uint pxMdhGetChecksum(IntPtr mdh);
        [DllImport(DLLNAME)] public static extern uint pxMdhGetNodeCount(IntPtr mdh);
        [DllImport(DLLNAME)] public static extern void pxMdhGetNode(IntPtr mdh, uint i, out short parent, out IntPtr name /*, TODO: Node transform*/);


        public static PxModelHierarchyData? GetFromPtr(IntPtr mdhPtr)
        {
            if (mdhPtr == IntPtr.Zero)
                return null;

            return new PxModelHierarchyData()
            {
                nodes = GetNodes(mdhPtr),
                bbox = pxMdhGetBbox(mdhPtr),
                rootTranslation = pxMdhGetRootTranslation(mdhPtr),
                checksum = pxMdhGetChecksum(mdhPtr)
            };
        }

        public static PxModelHierarchyNodeData[] GetNodes(IntPtr mdhPtr)
        {
            var count = pxMdhGetNodeCount(mdhPtr);
            var array = new PxModelHierarchyNodeData[count];

            for (var i = 0u; i < count; i++)
            {
                pxMdhGetNode(mdhPtr, i, out short parent, out IntPtr name);
                array[i] = new PxModelHierarchyNodeData()
                {
                    parentIndex = parent,
                    name = name.MarshalAsString()
                };
            }

            return array;
        }
    }
}
