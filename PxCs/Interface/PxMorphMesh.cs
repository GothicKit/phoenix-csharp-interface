using PxCs.Data.Mesh;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public class PxMorphMesh
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxMmbLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMmbLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMmbDestroy(IntPtr mmb);
        [DllImport(DLLNAME)] public static extern IntPtr pxMmbGetName(IntPtr mmb);
        [DllImport(DLLNAME)] public static extern IntPtr pxMmbGetMesh(IntPtr mmb);
        [DllImport(DLLNAME)] public static extern uint pxMmbGetMorphPositionCount(IntPtr mmb);
        [DllImport(DLLNAME)] public static extern Vector3 pxMmbGetMorphPosition(IntPtr mmb, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMmbGetAnimationCount(IntPtr mmb);
        [DllImport(DLLNAME)] public static extern IntPtr pxMmbGetAnimation(IntPtr mmb, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMmbAniGetName(IntPtr ani);
        [DllImport(DLLNAME)] public static extern int pxMmbAniGetLayer(IntPtr ani);
        [DllImport(DLLNAME)] public static extern float pxMmbAniGetBlendIn(IntPtr ani);
        [DllImport(DLLNAME)] public static extern float pxMmbAniGetBlendOut(IntPtr ani);
        [DllImport(DLLNAME)] public static extern float pxMmbAniGetDuration(IntPtr ani);
        [DllImport(DLLNAME)] public static extern float pxMmbAniGetSpeed(IntPtr ani);
        [DllImport(DLLNAME)] public static extern uint pxMmbAniGetFlags(IntPtr ani);
        [DllImport(DLLNAME)] public static extern uint pxMmbAniGetFrameCount(IntPtr ani);
        [DllImport(DLLNAME)] public static extern IntPtr pxMmbAniGetVertices(IntPtr ani, out int length);
        [DllImport(DLLNAME)] public static extern uint pxMmbAniGetSampleCount(IntPtr ani);
        [DllImport(DLLNAME)] public static extern Vector3 pxMmbAniGetSample(IntPtr ani, uint i);


        public static PxMorphMeshData? LoadMorphMeshFromVdf(IntPtr vdfPtr, string name)
        {
            var mmbPtr = pxMmbLoadFromVdf(vdfPtr, name);
            if (mmbPtr == IntPtr.Zero)
                return null;

            var data = GetMorphMeshFromPtr(mmbPtr);
            return data;

        }

        public static PxMorphMeshData GetMorphMeshFromPtr(IntPtr morphMeshPtr)
        {
            var name = GetName(morphMeshPtr);
            var mesh = GetMeshData(morphMeshPtr);
            var positions = GetPositionData(morphMeshPtr);
            var animations = GetAnimationData(morphMeshPtr);
            return new PxMorphMeshData()
            {
                name = name,
                mesh = mesh,
                positions = positions,
                animations = animations
            };
        }

        public static string GetName(IntPtr mmb)
        {
            return pxMmbGetName(mmb).MarshalAsString();
        }

        public static PxMultiResolutionMeshData GetMeshData(IntPtr mmb)
        {
            var meshPtr = pxMmbGetMesh(mmb);
            return PxMultiResolutionMesh.GetMRMFromPtr(meshPtr);
        }

        public static Vector3[] GetPositionData(IntPtr mmb)
        {
            var positionsCount = pxMmbGetMorphPositionCount(mmb);
            var array = new Vector3[positionsCount];

            for (var i = 0u; i < positionsCount; i++)
            {
                array[i] = pxMmbGetMorphPosition(mmb, i);
            }

            return array;
        }

        public static int[] getVertices(IntPtr animationPtr)
        {
            var verticesPtr = pxMmbAniGetVertices(animationPtr, out int verticesCount);

            return verticesPtr.MarshalAsArray<int>((uint)verticesCount);
        }
        public static Vector3[] getSamples(IntPtr animationPtr)
        {
            var samplesCount = pxMmbAniGetSampleCount(animationPtr);
            var array = new Vector3[samplesCount];

            for (var i = 0u; i < samplesCount; i++)
            {
                array[i] = pxMmbAniGetSample(animationPtr, i);
            }

            return array;
        }

        public static PxMorphAnimationData[] GetAnimationData(IntPtr mmb)
        {
            var animationsCount = pxMmbGetAnimationCount(mmb);
            var array = new PxMorphAnimationData[animationsCount];

            for (var i = 0u; i < animationsCount; i++)
            {
                var ani = pxMmbGetAnimation(mmb, i);
                var name = pxMmbAniGetName(ani).MarshalAsString();
                var layer = pxMmbAniGetLayer(ani);
                var blendIn = pxMmbAniGetBlendIn(ani);
                var blendOut = pxMmbAniGetBlendOut(ani);
                var duration = pxMmbAniGetDuration(ani);
                var speed = pxMmbAniGetSpeed(ani);
                var flags = pxMmbAniGetFlags(ani);
                var frameCount = pxMmbAniGetFrameCount(ani);
                var vertices = getVertices(ani);
                var samples = getSamples(ani);

                array[i] = new PxMorphAnimationData()
                {
                    name = name,
                    layer = layer,
                    blendIn = blendIn,
                    blendOut = blendOut,
                    duration = duration,
                    speed = speed,
                    flags = flags,
                    frameCount = frameCount,
                    vertices = vertices,
                    samples = samples
                };
            }

            return array;
        }
    }
}
