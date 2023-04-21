using PxCs.Data;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static PxCs.Data.PxModelScriptData;
using static PxCs.PxTexture;
using static System.Net.Mime.MediaTypeNames;

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

        public enum PxEventTagType
        {
            unknown,
            create_item,
            insert_item,
            remove_item,
            destroy_item,
            place_item,
            exchange_item,
            fight_mode,
            place_munition,
            remove_munition,
            draw_sound,
            undraw_sound,
            swap_mesh,
            draw_torch,
            inventory_torch,
            drop_torch,
            hit_limb,
            hit_direction,
            dam_multiply,
            par_frame,
            opt_frame,
            hit_end,
            window
        };

        public enum PxEventFightMode
        {
            fist,       // The player fights with his fists.
            one_handed, // The player wields a one-handed weapon.
            two_handed, // The player wields a two-handed weapon.
            bow,        // The player wields a bow.
            crossbow,   // The player wields a crossbow.
            magic,      // The player casts a magic spell.
            none,       // The player is not in a fighting stance.
            invalid     // A fight mode which acts as an `unset` marker. Added for OpenGothic compatibility.
        };

        [DllImport(DLLNAME)] public static extern IntPtr pxMdsLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxMdsDestroy(IntPtr mdm);

        // Misc parameters
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMdsGetSkeletonDisableMesh(IntPtr mds);

        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetSkeletonName(IntPtr mds);
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

        // Animations
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationCount(IntPtr mds);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationName(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationLayer(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationNext(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationBlendIn(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationBlendOut(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern PxAnimationFlags pxMdsGetAnimationFlags(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimationModel(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern PxAnimationDirection pxMdsGetAnimationDirection(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimationFirstFrame(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimationLastFrame(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationFps(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationSpeed(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimationCollisionVolumeScale(IntPtr mds, uint i);

        // Animations -> EventTags
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimation_EventTagCount(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimation_EventTagFrame(IntPtr mds, uint animIndex, uint eventTagIndex);
        [DllImport(DLLNAME)] public static extern PxEventTagType pxMdsGetAnimation_EventTagType(IntPtr mds, uint animIndex, uint eventTagIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventTagSlot(IntPtr mds, uint animIndex, uint eventTagIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventTagSlot2(IntPtr mds, uint animIndex, uint eventTagIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventTagItem(IntPtr mds, uint animIndex, uint eventTagIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventTagFrames(IntPtr mds, uint animIndex, uint eventTagIndex, out uint size);
        [DllImport(DLLNAME)] public static extern PxEventFightMode pxMdsGetAnimation_EventTagFightMode(IntPtr mds, uint animIndex, uint eventTagIndex);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMdsGetAnimation_EventTagAttached(IntPtr mds, uint animIndex, uint eventTagIndex);

        // Animations -> EventPfx
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimation_EventPfxCount(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimation_EventPfxFrame(IntPtr mds, uint animIndex, uint eventIndex);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimation_EventPfxIndex(IntPtr mds, uint animIndex, uint eventIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventPfxName(IntPtr mds, uint animIndex, uint eventIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventPfxPosition(IntPtr mds, uint animIndex, uint eventIndex);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMdsGetAnimation_EventPfxAttached(IntPtr mds, uint animIndex, uint eventIndex);

        // Animations -> PfxStop
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimation_EventPfxStopCount(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern void pxMdsGetAnimation_EventPfxStop(IntPtr mds, uint animIndex, uint pfxStopIndex,
            out int frame,
            out int index);

        // Animations -> Sfx
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimation_EventSfxCount(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimation_EventSfxFrame(IntPtr mds, uint animIndex, uint sfxIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventSfxName(IntPtr mds, uint animIndex, uint sfxIndex);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimation_EventSfxRange(IntPtr mds, uint animIndex, uint sfxIndex);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMdsGetAnimation_EventSfxEmptySlot(IntPtr mds, uint animIndex, uint sfxIndex);

        // Animations -> SfxGround
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimationEventSfxGroundCount(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimation_EventSfxGroundFrame(IntPtr mds, uint animIndex, uint sfxIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventSfxGroundName(IntPtr mds, uint animIndex, uint sfxIndex);
        [DllImport(DLLNAME)] public static extern float pxMdsGetAnimation_EventSfxGroundRange(IntPtr mds, uint animIndex, uint sfxIndex);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMdsGetAnimation_EventSfxGroundEmptySlot(IntPtr mds, uint animIndex, uint sfxIndex);

        // Animations -> EventMorphAnimate
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimation_EventMorphAnimateCount(IntPtr mds, uint i);
        [DllImport(DLLNAME)] public static extern int pxMdsGetAnimation_EventMorphAnimateFrame(IntPtr mds, uint animIndex, uint morphIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventMorphAnimateAnimation(IntPtr mds, uint animIndex, uint morphIndex);
        [DllImport(DLLNAME)] public static extern IntPtr pxMdsGetAnimation_EventMorphAnimateNode(IntPtr mds, uint animIndex, uint morphIndex);

        // Animations -> EventCameraTremor
        [DllImport(DLLNAME)] public static extern uint pxMdsGetAnimation_EventCameraTremorCount(IntPtr mds, uint i);
        [DllImport(DLLNAME)]
        public static extern void pxMdsGetAnimation_EventCameraTremor(IntPtr mds, uint animIndex, uint tremorIndex,
            out int frame,
            out int field1,
            out int field2,
            out int field3,
            out int field4);




        public static PxModelScriptData? GetModelScriptFromVdf(IntPtr vdfPtr, string name)
        {
            var mdsPtr = pxMdsLoadFromVdf(vdfPtr, name);

            if (mdsPtr == IntPtr.Zero)
                return null;

            var data = new PxModelScriptData()
            {
                skeleton = GetSkeleton(mdsPtr),
                meshes = GetMeshes(mdsPtr),
                disabledAnimations = GetDisabledAnimations(mdsPtr),
                combinations = GetCombinations(mdsPtr),
                blends = GetAnimationBlendings(mdsPtr),
                aliases = GetAnimationAliases(mdsPtr),
                model_tags = GetModelTags(mdsPtr),
                animations = GetAnimations(mdsPtr)
            };

            pxMdsDestroy(mdsPtr);
            return data;
        }

        public static PxSkeleton GetSkeleton(IntPtr mdsPtr)
        {
            return new PxSkeleton()
            {
                name = pxMdsGetSkeletonName(mdsPtr).MarshalAsString(),
                disableMesh = pxMdsGetSkeletonDisableMesh(mdsPtr)
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

        public static PxAnimation[] GetAnimations(IntPtr mdsPtr)
        {
            var count = pxMdsGetAnimationCount(mdsPtr);
            var array = new PxAnimation[count];

            for (var i = 0u; i < count; i++)
            {
                array[i] = GetAnimation(mdsPtr, i);
            }

            return array;
        }

        public static PxAnimation GetAnimation(IntPtr mdsPtr, uint index)
        {
            return new PxAnimation()
            {
                name = pxMdsGetAnimationName(mdsPtr, index).MarshalAsString(),
                layer = pxMdsGetAnimationLayer(mdsPtr, index),
                next = pxMdsGetAnimationNext(mdsPtr, index).MarshalAsString(),
                blendIn = pxMdsGetAnimationBlendIn(mdsPtr, index),
                blendOut = pxMdsGetAnimationBlendOut(mdsPtr, index),
                flags = pxMdsGetAnimationFlags(mdsPtr, index),
                model = pxMdsGetAnimationModel(mdsPtr, index).MarshalAsString(),
                direction = pxMdsGetAnimationDirection(mdsPtr, index),
                firstFrame = pxMdsGetAnimationFirstFrame(mdsPtr, index),
                lastFrame = pxMdsGetAnimationLastFrame(mdsPtr, index),
                fps = pxMdsGetAnimationFps(mdsPtr, index),
                speed = pxMdsGetAnimationSpeed(mdsPtr, index),
                collisionVolumeScale = pxMdsGetAnimationCollisionVolumeScale(mdsPtr, index),

                events = GetAnimationEventTags(mdsPtr, index),
                pfx = GetAnimationPfx(mdsPtr, index),
                pfx_stop = GetAnimationPfxStop(mdsPtr, index),
                sfx = GetAnimationSfx(mdsPtr, index),
                sfx_ground = GetAnimationSfxGrounds(mdsPtr, index),
                morph = GetAnimationMorps(mdsPtr, index),
                tremors = GetAnimationTremors(mdsPtr, index)
            };
        }

        public static PxEventTag[] GetAnimationEventTags(IntPtr mdsPtr, uint index)
        {
            var count = pxMdsGetAnimation_EventTagCount(mdsPtr, index);
            var array = new PxEventTag[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].frame = pxMdsGetAnimation_EventTagFrame(mdsPtr, index, i);
                array[i].type = pxMdsGetAnimation_EventTagType(mdsPtr, index, i);
                array[i].slot = pxMdsGetAnimation_EventTagSlot(mdsPtr, index, i).MarshalAsString();
                array[i].slot2 = pxMdsGetAnimation_EventTagSlot2(mdsPtr, index, i).MarshalAsString();
                array[i].item = pxMdsGetAnimation_EventTagItem(mdsPtr, index, i).MarshalAsString();
                array[i].fightMode = pxMdsGetAnimation_EventTagFightMode(mdsPtr, index, i);
                array[i].attached = pxMdsGetAnimation_EventTagAttached(mdsPtr, index, i);
                array[i].frames = pxMdsGetAnimation_EventTagFrames(mdsPtr, index, i, out uint size).MarshalAsArray<int>(size);
            }

            return array;
        }

        public static PxEventPfx[] GetAnimationPfx(IntPtr mdsPtr, uint index)
        {
            var count = pxMdsGetAnimation_EventPfxCount(mdsPtr, index);
            var array = new PxEventPfx[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].frame = pxMdsGetAnimation_EventPfxFrame(mdsPtr, index, i);
                array[i].index = pxMdsGetAnimation_EventPfxIndex(mdsPtr, index, i);
                array[i].name = pxMdsGetAnimation_EventPfxName(mdsPtr, index, i).MarshalAsString();
                array[i].position = pxMdsGetAnimation_EventPfxPosition(mdsPtr, index, i).MarshalAsString();
                array[i].attached = pxMdsGetAnimation_EventPfxAttached(mdsPtr, index, i);
            }

            return array;
        }

        public static PxEventPfxStop[] GetAnimationPfxStop(IntPtr mdsPtr, uint index)
        {
            var count = pxMdsGetAnimation_EventPfxStopCount(mdsPtr, index);
            var array = new PxEventPfxStop[count];

            for (var i = 0u; i < count; i++)
            {
                pxMdsGetAnimation_EventPfxStop(mdsPtr, index, i, out int frame, out int stopIndex);

                array[i].frame = frame;
                array[i].index = stopIndex;
            }

            return array;
        }

        public static PxEventSfx[] GetAnimationSfx(IntPtr mdsPtr, uint index)
        {
            var count = pxMdsGetAnimation_EventSfxCount(mdsPtr, index);
            var array = new PxEventSfx[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].frame = pxMdsGetAnimation_EventSfxFrame(mdsPtr, index, i);
                array[i].name = pxMdsGetAnimation_EventSfxName(mdsPtr, index, i).MarshalAsString();
                array[i].range = pxMdsGetAnimation_EventSfxRange(mdsPtr, index, i);
                array[i].emptySlot = pxMdsGetAnimation_EventSfxEmptySlot(mdsPtr, index, i);
            }

            return array;
        }

        public static PxEventSfxGround[] GetAnimationSfxGrounds(IntPtr mdsPtr, uint index)
        {
            var count = pxMdsGetAnimationEventSfxGroundCount(mdsPtr, index);
            var array = new PxEventSfxGround[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].frame = pxMdsGetAnimation_EventSfxGroundFrame(mdsPtr, index, i);
                array[i].name = pxMdsGetAnimation_EventSfxGroundName(mdsPtr, index, i).MarshalAsString();
                array[i].range = pxMdsGetAnimation_EventSfxGroundRange(mdsPtr, index, i);
                array[i].emptySlot = pxMdsGetAnimation_EventSfxGroundEmptySlot(mdsPtr, index, i);
            }

            return array;
        }

        public static PxEventMorphAnimate[] GetAnimationMorps(IntPtr mdsPtr, uint index)
        {
            var count = pxMdsGetAnimation_EventMorphAnimateCount(mdsPtr, index);
            var array = new PxEventMorphAnimate[count];

            for (var i = 0u; i < count; i++)
            {
                array[i].frame = pxMdsGetAnimation_EventMorphAnimateFrame(mdsPtr, index, i);
                array[i].animation = pxMdsGetAnimation_EventMorphAnimateAnimation(mdsPtr, index, i).MarshalAsString();
                array[i].node = pxMdsGetAnimation_EventMorphAnimateNode(mdsPtr, index, i).MarshalAsString();
    }

            return array;
        }

        public static PxEventCameraTremor[] GetAnimationTremors(IntPtr mdsPtr, uint index)
        {
            var count = pxMdsGetAnimation_EventCameraTremorCount(mdsPtr, index);
            var array = new PxEventCameraTremor[count];

            for (var i = 0u; i < count; i++)
            {
                pxMdsGetAnimation_EventCameraTremor(mdsPtr, index, i,
                    out int frame,
                    out int field1,
                    out int field2,
                    out int field3,
                    out int field4);

                array[i].frame = frame;
                array[i].field1 = field1;
                array[i].field2 = field2;
                array[i].field3 = field3;
                array[i].field4 = field4;
            }

            return array;
        }
    }
}
