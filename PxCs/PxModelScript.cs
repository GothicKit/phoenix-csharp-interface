using PxCs.Data;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using static PxCs.Data.PxModelScriptData;
using static PxCs.PxTexture;

namespace PxCs
{
    public static class PxModelScript
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        public enum PxAnimationFlags
        {
            af_none = 0,
            af_move = 1,
            af_rotate = 2,
            af_queue = 4,
            af_fly = 8,
            af_idle = 16
        };

        public enum PxAnimationDirection
        {
            forward = 0,
            backward = 1
        };


        [DllImport(DLLNAME)] public static extern IntPtr pxMdsLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMdsDestroy(IntPtr mdm);

        // Misc parameters
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMdsGetskeletonDisableMesh(IntPtr mds);

        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetskeletonName(IntPtr mds);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetMeshCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetMesh(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetDisabledAnimationsCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetDisabledAnimation(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetModelTagCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetModelTagBone(IntPtr mds, uint i);

        // AnimationCombination
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationCombinationCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationCombinationName(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationCombinationLayer(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationCombinationNext(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationCombinationBlendIn(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationCombinationBlendOut(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern PxAnimationFlags pxMdsGetAnimationCombinationFlags(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationCombinationModel(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimationCombinationLastFrame(IntPtr mds, uint i);

        // AnimationBlending
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationBlendingCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationBlendingName(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationBlendingNext(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationBlendingBlendIn(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationBlendingBlendOut(IntPtr mds, uint i);

        // AnimationAlias
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationAliasCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationAliasName(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationAliasLayer(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationAliasNext(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationAliasBlendIn(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationAliasBlendOut(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern PxAnimationFlags pxMdsGetAnimationAliasFlags(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationAliasAlias(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern PxAnimationDirection pxMdsGetAnimationAliasDirection(IntPtr mds, uint i);



        public static PxModelScriptData? GetModelScriptFromVdf(IntPtr vdfPtr, string name)
        {
            var mdsPtr = pxMdsLoadFromVdf(vdfPtr, name);

            if (mdsPtr == IntPtr.Zero)
                return null;

            var data = new PxModelScriptData()
            {
                skeleton = GetSkeleton(mdsPtr),
                meshes = GetMeshes(mdsPtr),
                disabled_animations = GetDisabledAnimations(mdsPtr),
                combinations = GetCombinations(mdsPtr),
                blends = GetAnimationBlendings(mdsPtr),
                aliases = GetAnimationAliases(mdsPtr),
                model_tags = GetModelTags(mdsPtr)
            };

            pxMdsDestroy(mdsPtr);
            return data;
        }

        public static PxSkeleton GetSkeleton(IntPtr mdsPtr)
        {
            return new PxSkeleton()
            {
                name = pxMdsGetskeletonName(mdsPtr).MarshalAsString(),
                disable_mesh = pxMdsGetskeletonDisableMesh(mdsPtr)
            };
        }

        public static string[] GetMeshes(IntPtr mdsPtr)
        {
            var count = pxMdsGetMeshCount(mdsPtr);
            var array = new string[count];

            for (var i = 0u; i < count; i++)
            {
                array[i] = pxMdsGetMesh(mdsPtr, i).MarshalAsString();
            }

            return array;
        }

        public static string[] GetDisabledAnimations(IntPtr mdsPtr)
        {
            var count = pxMdsGetDisabledAnimationsCount(mdsPtr);
            var array = new string[count];

            for (var i = 0u; i < count; i++)
            {
                array[i] = pxMdsGetDisabledAnimation(mdsPtr, i).MarshalAsString();
            }

            return array;
        }

        public static PxAnimationCombination[] GetCombinations(IntPtr mdsPtr)
        {
            var count = pxMdsGetAnimationCombinationCount(mdsPtr);
            var array = new PxAnimationCombination[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].name = pxMdsGetAnimationCombinationName(mdsPtr, i).MarshalAsString();
                array[i].layer = pxMdsGetAnimationCombinationLayer(mdsPtr, i);
                array[i].next = pxMdsGetAnimationCombinationNext(mdsPtr, i).MarshalAsString();
                array[i].blend_in = pxMdsGetAnimationCombinationBlendIn(mdsPtr, i);
                array[i].blend_out = pxMdsGetAnimationCombinationBlendOut(mdsPtr, i);
                array[i].flags = pxMdsGetAnimationCombinationFlags(mdsPtr, i);
                array[i].model = pxMdsGetAnimationCombinationModel(mdsPtr, i).MarshalAsString();
                array[i].last_frame = pxMdsGetAnimationCombinationLastFrame(mdsPtr, i);
            }

            return array;
        }

        public static PxAnimationBlending[] GetAnimationBlendings(IntPtr mdsPtr)
        {
            var count = pxMdsGetAnimationBlendingCount(mdsPtr);
            var array = new PxAnimationBlending[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].name = pxMdsGetAnimationBlendingName(mdsPtr, i).MarshalAsString();
                array[i].next = pxMdsGetAnimationBlendingNext(mdsPtr, i).MarshalAsString();
                array[i].blend_in = pxMdsGetAnimationBlendingBlendIn(mdsPtr, i);
                array[i].blend_out = pxMdsGetAnimationBlendingBlendOut(mdsPtr, i);
            }

            return array;
        }

        public static PxAnimationAlias[] GetAnimationAliases(IntPtr mdsPtr)
        {
            var count = pxMdsGetAnimationAliasCount(mdsPtr);
            var array = new PxAnimationAlias[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].name = pxMdsGetAnimationAliasName(mdsPtr, i).MarshalAsString();
                array[i].layer = pxMdsGetAnimationAliasLayer(mdsPtr, i);
                array[i].next = pxMdsGetAnimationAliasNext(mdsPtr, i).MarshalAsString();
                array[i].blend_in = pxMdsGetAnimationAliasBlendIn(mdsPtr, i);
                array[i].blend_out = pxMdsGetAnimationAliasBlendOut(mdsPtr, i);
                array[i].flags = pxMdsGetAnimationAliasFlags(mdsPtr, i);
                array[i].alias = pxMdsGetAnimationAliasAlias(mdsPtr, i).MarshalAsString();
                array[i].direction = pxMdsGetAnimationAliasDirection(mdsPtr, i);
            }

            return array;
        }

        public static PxModelTag[] GetModelTags(IntPtr mdsPtr)
        {
            var count = pxMdsGetModelTagCount(mdsPtr);
            var array = new PxModelTag[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].bone = pxMdsGetModelTagBone(mdsPtr, i).MarshalAsString();
            }

            return array;
        }
    }
}
