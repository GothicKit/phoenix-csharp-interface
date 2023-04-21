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

        [DllImport(DLLNAME)] public static extern uint pxMdsGetMeshCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetMesh(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetDisabledAnimationsCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetDisabledAnimation(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetModelTagCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern void pxMdsGetModelTag(IntPtr mds, uint i, out IntPtr bone);

        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationCombinationCount(IntPtr mds);
        [DllImport(DLLNAME)]
        public static extern void pxMdsGetAnimationCombination(IntPtr mds, uint i,
            out IntPtr name,
            out uint layer,
            out IntPtr next,
            out float blend_in,
            out float blend_out,
            out PxAnimationFlags flags,
            out IntPtr model,
            out int last_frame);

        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationBlendingCount(IntPtr mds);
        [DllImport(DLLNAME)]
        public static extern void pxMdsGetAnimationBlending(IntPtr mds, uint i,
            out IntPtr name,
            out IntPtr next,
            out float blend_in,
            out float blend_out);

        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationAliasCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern void pxMdsGetAnimationAlias(IntPtr mds, uint i,
                    out IntPtr name,
                    out uint layer,
                    out IntPtr next,
                    out float blend_in,
                    out float blend_out,
                    out PxAnimationFlags flags,
                    out IntPtr alias,
                    out PxAnimationDirection direction);



        public static PxModelScriptData? GetModelScriptFromVdf(IntPtr vdfPtr, string name)
        {
            var mdsPtr = pxMdsLoadFromVdf(vdfPtr, name);

            if (mdsPtr == IntPtr.Zero)
                return null;

            var data = new PxModelScriptData()
            {
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
                pxMdsGetAnimationCombination(mdsPtr, i,
                    out IntPtr name,
                    out uint layer,
                    out IntPtr next,
                    out float blend_in,
                    out float blend_out,
                    out PxAnimationFlags flags,
                    out IntPtr model,
                    out int last_frame);

                array[i] = new PxAnimationCombination()
                {
                    name = name.MarshalAsString(),
                    layer = layer,
                    next = next.MarshalAsString(),
                    blend_in = blend_in,
                    blend_out = blend_out,
                    flags = flags,
                    model = model.MarshalAsString(),
                    last_frame = last_frame
                };
            }

            return array;
        }

        public static PxAnimationBlending[] GetAnimationBlendings(IntPtr mdsPtr)
        {
            var count = pxMdsGetAnimationBlendingCount(mdsPtr);
            var array = new PxAnimationBlending[count];

            for (var i = 0u; i < count; i++)
            {
                pxMdsGetAnimationBlending(mdsPtr, i,
                out IntPtr name,
                out IntPtr next,
                out float blend_in,
                out float blend_out);

                array[i] = new PxAnimationBlending()
                {
                    name = name.MarshalAsString(),
                    next = next.MarshalAsString(),
                    blend_in = blend_in,
                    blend_out = blend_out
                };
            }

            return array;
        }

        public static PxAnimationAlias[] GetAnimationAliases(IntPtr mdsPtr)
        {
            var count = pxMdsGetAnimationAliasCount(mdsPtr);
            var array = new PxAnimationAlias[count];

            for (var i = 0u; i < count; i++)
            {
                pxMdsGetAnimationAlias(mdsPtr, i,
                out IntPtr name,
                out uint layer,
                out IntPtr next,
                out float blend_in,
                out float blend_out,
                out PxAnimationFlags flags,
                out IntPtr alias,
                out PxAnimationDirection direction);

                array[i] = new PxAnimationAlias()
                {
                    name = name.MarshalAsString(),
                    layer = layer,
                    next = next.MarshalAsString(),
                    blend_in = blend_in,
                    blend_out = blend_out,
                    flags = flags,
                    alias = alias.MarshalAsString(),
                    direction = direction
                };
            }

            return array;
        }

        public static PxModelTag[] GetModelTags(IntPtr mdsPtr)
        {
            var count = pxMdsGetModelTagCount(mdsPtr);
            var array = new PxModelTag[count];

            for (var i = 0u; i < count; i++)
            {
                pxMdsGetModelTag(mdsPtr, i, out IntPtr bone);

                array[i] = new PxModelTag()
                {
                    bone = bone.MarshalAsString()
                };
            }

            return array;
        }
    }
}
