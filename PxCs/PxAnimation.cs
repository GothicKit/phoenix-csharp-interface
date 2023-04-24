using PxCs.Data.Animation;
using PxCs.Data.Misc;
using PxCs.Data.ModelScript;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs
{
    public class PxAnimation
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxManLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxManLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxManDestroy(IntPtr ani);

        [DllImport(DLLNAME)] public static extern IntPtr pxManGetName(IntPtr man);
        [DllImport(DLLNAME)] public static extern IntPtr pxManGetNext(IntPtr man);
        [DllImport(DLLNAME)] public static extern uint pxManGetLayer(IntPtr man);
        [DllImport(DLLNAME)] public static extern uint pxManGetFrameCount(IntPtr man);
        [DllImport(DLLNAME)] public static extern uint pxManGetNodeCount(IntPtr man);
        [DllImport(DLLNAME)] public static extern float pxManGetFps(IntPtr man);
        [DllImport(DLLNAME)] public static extern PxAABBData pxManGetBbox(IntPtr man);
        [DllImport(DLLNAME)] public static extern uint pxManGetChecksum(IntPtr man);
        [DllImport(DLLNAME)] public static extern void pxManGetSample(IntPtr man, uint i, out Vector3 position, out PxQuaternionData rotation);
        [DllImport(DLLNAME)] public static extern IntPtr pxManGetNodeIndices(IntPtr man, out uint length); // return uint[]

        public static PxAnimationData? LoadFromVdf(IntPtr vdfPtr, string name)
        {
            var manPtr = pxManLoadFromVdf(vdfPtr, name);

            if (manPtr == IntPtr.Zero)
                return null;

            var man = new PxAnimationData()
            {

            };

            pxManDestroy(manPtr);
            return man;
        }
    }
}
