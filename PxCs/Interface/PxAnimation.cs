using PxCs.Data.Animation;
using PxCs.Data.Struct;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
	public class PxAnimation
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxManLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxManLoadFromVfs(IntPtr vfs, string name);
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


        [DllImport(DLLNAME)] public static extern uint pxManGetSampleCount(IntPtr man);



        public static PxAnimationData? LoadFromVfs(IntPtr vfsPtr, string name)
        {
            var manPtr = pxManLoadFromVfs(vfsPtr, name);

            if (manPtr == IntPtr.Zero)
                return null;

            var man = new PxAnimationData()
            {
                name = pxManGetName(manPtr).MarshalAsString(),
                next = pxManGetNext(manPtr).MarshalAsString(),
                layer = pxManGetLayer(manPtr),

                frameCount = pxManGetFrameCount(manPtr),
                nodeCount = pxManGetNodeCount(manPtr),

                fps = pxManGetFps(manPtr),

                bbox = pxManGetBbox(manPtr),
                checksum = pxManGetChecksum(manPtr),

                samples = GetSamples(manPtr),
                nodeIndices = pxManGetNodeIndices(manPtr, out uint length).MarshalAsArray<uint>(length)
            };

            pxManDestroy(manPtr);
            return man;
        }

        // FIXME - do we always have same sample size as pxManGetFrameCount() size? If not, we need to add that method.
        public static PxAnimationSampleData[] GetSamples(IntPtr manPtr)
        {
            var count = pxManGetSampleCount(manPtr);
            var array = new PxAnimationSampleData[count];

            for (var i = 0u; i < count; i++)
            {
                pxManGetSample(manPtr, i, out Vector3 position, out PxQuaternionData rotation);
                array[i] = new PxAnimationSampleData()
                {
                    position = position,
                    rotation = rotation
                };
            }

            return array;
        }
    }
}
