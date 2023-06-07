using PxCs.Data.Struct;
using PxCs.Data.Vob;
using PxCs.Data.WayNet;
using PxCs.Extensions;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
    public static class PxWorld
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        public enum PxVobType
        {
            PxVob_zCVob = 0,
            PxVob_zCVobLevelCompo = 1,
            PxVob_oCItem = 2,
            PxVob_oCNpc = 3,
            PxVob_zCMoverController = 4,
            PxVob_zCVobScreenFX = 5,
            PxVob_zCVobStair = 6,
            PxVob_zCPFXController = 7,
            PxVob_zCVobAnimate = 8,
            PxVob_zCVobLensFlare = 9,
            PxVob_zCVobLight = 10,
            PxVob_zCVobSpot = 11,
            PxVob_zCVobStartpoint = 12,
            PxVob_zCMessageFilter = 13,
            PxVob_zCCodeMaster = 14,
            PxVob_zCTriggerWorldStart = 15,
            PxVob_zCCSCamera = 16,
            PxVob_zCCamTrj_KeyFrame = 17,
            PxVob_oCTouchDamage = 18,
            PxVob_zCTriggerUntouch = 19,
            PxVob_zCEarthquake = 20,
            PxVob_oCMOB = 21,
            PxVob_oCMobInter = 22,
            PxVob_oCMobBed = 23,
            PxVob_oCMobFire = 24,
            PxVob_oCMobLadder = 25,
            PxVob_oCMobSwitch = 26,
            PxVob_oCMobWheel = 27,
            PxVob_oCMobContainer = 28,
            PxVob_oCMobDoor = 29,
            PxVob_zCTrigger = 30,
            PxVob_zCTriggerList = 31,
            PxVob_oCTriggerScript = 32,
            PxVob_oCTriggerChangeLevel = 33,
            PxVob_oCCSTrigger = 34,
            PxVob_zCMover = 35,
            PxVob_zCVobSound = 36,
            PxVob_zCVobSoundDaytime = 37,
            PxVob_oCZoneMusic = 38,
            PxVob_oCZoneMusicDefault = 39,
            PxVob_zCZoneZFog = 40,
            PxVob_zCZoneZFogDefault = 41,
            PxVob_zCZoneVobFarPlane = 42,
            PxVob_zCZoneVobFarPlaneDefault = 43,
            PxVob_ignored = 44,
            PxVob_unknown = 45,
        };

        public enum PxVobShadowMode
        {
            PxVobShadowNone = 0,
            PxVobShadowBlob = 1,
        };

        public enum PxVobAnimationMode
        {
            PxVobAnimationNone = 0,
            PxVobAnimationWind = 1,
            PxVobAnimationWind2 = 2,
        };

        public enum PxVobSpriteAlignment
        {
            PxVobSpriteAlignNone = 0,
            PxVobSpriteAlignYaw = 1,
            PxVobSpriteAlignFull = 2,
        };

        public enum PxVobVisualType
        {
            PxVobVisualDecal = 0,               // The VOb presents as a decal.
            PxVobVisualMesh = 1,                // The VOb presents a PxMesh.
            PxVobVisualMultiResolutionMesh = 2, // The VOb presents a PxMultiResolutionMesh.
            PxVobVisualParticleSystem = 3,      // The VOb presents as a particle system.
            PxVobVisualAiCamera = 4,            // The VOb is a game-controlled camera.
            PxVobVisualModel = 5,               // The VOb presents a PxModel.
            PxVobVisualMorphMesh = 6,           // The VOb presents a PxMorphMesh.
            PxVobVisualUnknown = 7,             // The VOb presents an unknown visual or no visual at all.
        };

        public enum PxVobSoundMaterial {
            PxVobMobSoundWood = 0,
            PxVobMobSoundStone = 1,
            PxVobMobSoundMetal = 2,
            PxVobMobSoundLeather = 3,
            PxVobMobSoundClay = 4,
            PxVobMobSoundGlass = 5,
        };


        [DllImport(DLLNAME)] public static extern IntPtr pxWorldLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxWorldLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxWorldDestroy(IntPtr world);

        [DllImport(DLLNAME)] public static extern IntPtr pxWorldGetMesh(IntPtr world);
        [DllImport(DLLNAME)] public static extern uint pxWorldGetWayPointCount(IntPtr world);
        [DllImport(DLLNAME)]
        public static extern void pxWorldGetWayPoint(
            IntPtr world,
            uint i,
            out IntPtr namePtr,
            out Vector3 position,
            out Vector3 direction,
            [MarshalAs(UnmanagedType.U1)] out bool freePoint,
            [MarshalAs(UnmanagedType.U1)] out bool underwater,
            out int waterDepth);

        [DllImport(DLLNAME)] public static extern uint pxWorldGetWayEdgeCount(IntPtr world);
        [DllImport(DLLNAME)] public static extern void pxWorldGetWayEdge(IntPtr world, uint i, out uint a, out uint b);

        [DllImport(DLLNAME)] public static extern uint pxWorldGetRootVobCount(IntPtr world);
        [DllImport(DLLNAME)] public static extern IntPtr pxWorldGetRootVob(IntPtr world, uint i);

        [DllImport(DLLNAME)] public static extern PxVobType pxVobGetType(IntPtr vob);
        [DllImport(DLLNAME)] public static extern uint pxVobGetId(IntPtr vob);

        [DllImport(DLLNAME)] public static extern Vector3 pxVobGetPosition(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxMatrix3x3Data pxVobGetRotation(IntPtr vob);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobGetShowVisual(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobSpriteAlignment pxVobGetSpriteAlignment(IntPtr vob);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobGetCdStatic(IntPtr vob);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobGetCdDynamic(IntPtr vob);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobGetVobStatic(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobShadowMode pxVobGetShadowMode(IntPtr vob);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobGetPhysicsEnabled(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobAnimationMode pxVobGetAnimationMode(IntPtr vob);
        [DllImport(DLLNAME)] public static extern int pxVobGetBias(IntPtr vob);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobGetAmbient(IntPtr vob);
        [DllImport(DLLNAME)] public static extern float pxVobGetAnimationStrength(IntPtr vob);
        [DllImport(DLLNAME)] public static extern float pxVobGetFarClipScale(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetPresetName(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetVobName(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetVisualName(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobVisualType pxVobGetVisualType(IntPtr vob);

        [DllImport(DLLNAME)] public static extern uint pxVobGetChildCount(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetChild(IntPtr vob, uint i);


        // Vob - Mob
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetName(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern int pxVobMobGetHp(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern int pxVobMobGetDamage(IntPtr vobMob);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetMovable(IntPtr vobMob);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetTakable(IntPtr vobMob);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetFocusOverride(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern PxVobSoundMaterial pxVobMobGetMaterial(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetVisualDestroyed(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetOwner(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetOwnerGuild(IntPtr vobMob);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetDestroyed(IntPtr vobMob);
        //Vob - MobInter
        [DllImport(DLLNAME)] public static extern int pxVobMobInterGetState(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetTarget(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetItem(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetConditionFunction(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetOnStateChangeFunction(IntPtr vobMobInter);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobMobInterGetRewind(IntPtr vobMobInter);
        // Vob - MobFire
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobFireGetSlot(IntPtr vobMobFire);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobFireGetVobTree(IntPtr vobMobFire);
        // Vob - MobContainer
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobMobContainerGetLocked(IntPtr vobMobContainer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobContainerGetKey(IntPtr vobMobContainer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobContainerGetPickString(IntPtr vobMobContainer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobContainerGetContents(IntPtr vobMobContainer);
        // Vob - MobDoor
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVobMobDoorGetLocked(IntPtr vobMobDoor);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobDoorGetKey(IntPtr vobMobDoor);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobDoorGetPickString(IntPtr vobMobDoor);

        // Vob - ZoneMusic
        [DllImport(DLLNAME)] public static extern void pxWorldVobGetZoneMusic(
            IntPtr zoneMusic,
            [MarshalAs(UnmanagedType.U1)] out bool enabled,
            out int priority,
            [MarshalAs(UnmanagedType.U1)] out bool ellipsoid,
            out float reverb,
            out float volume,
            [MarshalAs(UnmanagedType.U1)] out bool loop);
        // Vob - ZoneFarPlane
        [DllImport(DLLNAME)] public static extern void pxWorldVobGetZoneFarPlane(
            IntPtr zoneFarPlane,
            out float vobFarPlaneZ,
            out float innerRangePercentage);
        // Vob - ZoneFog
        [DllImport(DLLNAME)] public static extern void pxWorldVobGetZoneFog(
            IntPtr zoneFog,
            out float rangeCenter,
            out float innerRangePercentage,
            out Vector4Byte color,
            [MarshalAs(UnmanagedType.U1)] out bool fadeOutSky,
            [MarshalAs(UnmanagedType.U1)] out bool overrideColor);



        public static PxWayPointData[] GetWayPoints(IntPtr worldPtr)
        {
            var count = pxWorldGetWayPointCount(worldPtr);
            var array = new PxWayPointData[count];

            for (var i = 0u; i < count; i++)
            {
                pxWorldGetWayPoint(worldPtr, i,
                    out IntPtr namePtr,
                    out Vector3 position,
                    out Vector3 direction,
                    out bool freePoint,
                    out bool underwater,
                    out int waterDepth
                );

                array[i] = new PxWayPointData()
                {
                    name = namePtr.MarshalAsString(),
                    position = position,
                    direction = direction,
                    freePoint = freePoint,
                    underwater = underwater,
                    waterDepth = waterDepth
                };
            }

            return array;
        }

        public static PxWayEdgeData[] GetWayEdges(IntPtr worldPtr)
        {
            var count = pxWorldGetWayEdgeCount(worldPtr);
            var array = new PxWayEdgeData[count];

            for (var i = 0u; i < count; i++)
            {
                pxWorldGetWayEdge(worldPtr, i, out uint a, out uint b);

                array[i] = new PxWayEdgeData()
                {
                    a = a,
                    b = b
                };
            }

            return array;
        }


        public static PxVobData[] GetVobs(IntPtr worldPtr)
        {
            var count = pxWorldGetRootVobCount(worldPtr);
            var vobs = new PxVobData[count];

            for (var i = 0u; i < count; i++)
            {
                var vobPtr = pxWorldGetRootVob(worldPtr, i);
                vobs[i] = GetVobTypeData(vobPtr);
            }

            return vobs;
        }

        private static PxVobData GetVobTypeData(IntPtr vobPtr)
        {
            var vobType = pxVobGetType(vobPtr);

            PxVobData vob;

            // Instanciate right class object.
            switch (vobType)
            {
                case PxVobType.PxVob_oCMOB:
                    vob = new PxVobMobData();
                    SetVobMobData(vobPtr, (PxVobMobData)vob);
                    break;
                case PxVobType.PxVob_oCMobInter:
                    vob = new PxVobMobInterData();
                    SetVobMobInterData(vobPtr, (PxVobMobInterData)vob);
                    break;
                case PxVobType.PxVob_oCMobFire:
                    vob = new PxVobMobFireData();
                    SetVobMobFireData(vobPtr, (PxVobMobFireData)vob);
                    break;
                case PxVobType.PxVob_oCMobContainer:
                    vob = new PxVobMobContainerData();
                    SetVobMobContainerData(vobPtr, (PxVobMobContainerData)vob);
                    break;
                case PxVobType.PxVob_oCMobDoor:
                    vob = new PxVobMobDoorData();
                    SetVobMobDoorData(vobPtr, (PxVobMobDoorData)vob);
                    break;
                case PxVobType.PxVob_oCZoneMusic:
                case PxVobType.PxVob_oCZoneMusicDefault:
                    vob = new PxVobZoneMusicData();
                    SetVobZoneMusicData(vobPtr, (PxVobZoneMusicData)vob);
                    break;
                case PxVobType.PxVob_zCZoneVobFarPlane:
                case PxVobType.PxVob_zCZoneVobFarPlaneDefault:
                    vob = new PxVobZoneFarPlaneData();
                    SetVobZoneFarPlaneData(vobPtr, (PxVobZoneFarPlaneData)vob);
                    break;
                case PxVobType.PxVob_zCZoneZFog:
                case PxVobType.PxVob_zCZoneZFogDefault:
                    vob = new PxVobZoneFogData();
                    SetVobZoneFogData(vobPtr, (PxVobZoneFogData)vob);
                    break;
                default:
                    // FIXME - As we will handle all the types, this will throw an exception in future.
                    vob = new PxVobData();
                    SetVobData(vobPtr, vob);
                    break;
            }

            var childCount = pxVobGetChildCount(vobPtr);
            vob.childVobs = new PxVobData[childCount];
            for (var ii = 0u; ii < childCount; ii++)
            {
                var childVobPtr = pxVobGetChild(vobPtr, ii);
                vob.childVobs[ii] = GetVobTypeData(childVobPtr);
            }

            return vob;
        }

        private static void SetVobData(IntPtr vobPtr, PxVobData vob)
        {
            vob.id = pxVobGetId(vobPtr);
            vob.type = pxVobGetType(vobPtr);

            vob.position = pxVobGetPosition(vobPtr);
            vob.rotation = pxVobGetRotation(vobPtr);

            vob.presetName = pxVobGetPresetName(vobPtr).MarshalAsString();
            vob.vobName = pxVobGetVobName(vobPtr).MarshalAsString();
            vob.visualName = pxVobGetVisualName(vobPtr).MarshalAsString();

            vob.animationMode = pxVobGetAnimationMode(vobPtr);
            vob.shadowMode = pxVobGetShadowMode(vobPtr);
            vob.spriteAlignment = pxVobGetSpriteAlignment(vobPtr);
            vob.visualType = pxVobGetVisualType(vobPtr);

            vob.ambient = pxVobGetAmbient(vobPtr);
            vob.cdDynamic = pxVobGetCdDynamic(vobPtr);
            vob.cdStatic = pxVobGetCdStatic(vobPtr);
            vob.vobStatic = pxVobGetVobStatic(vobPtr);
            vob.showVisual = pxVobGetShowVisual(vobPtr);
            vob.physicsEnabled = pxVobGetPhysicsEnabled(vobPtr);

            vob.bias = pxVobGetBias(vobPtr);

            vob.animationStrength = pxVobGetAnimationStrength(vobPtr);
            vob.farClipScale = pxVobGetFarClipScale(vobPtr);
        }

        private static void SetVobMobData(IntPtr vobMobPtr, PxVobMobData vobMob)
        {
            SetVobData(vobMobPtr, vobMob);

            vobMob.name = pxVobMobGetName(vobMobPtr).MarshalAsString();
            vobMob.hp = pxVobMobGetHp(vobMobPtr);
            vobMob.damage = pxVobMobGetDamage(vobMobPtr);
            vobMob.movable = pxVobMobGetMovable(vobMobPtr);
            vobMob.takable = pxVobMobGetTakable(vobMobPtr);
            vobMob.focusOverride = pxVobMobGetFocusOverride(vobMobPtr);
            vobMob.material = pxVobMobGetMaterial(vobMobPtr);
            vobMob.visualDestroyed = pxVobMobGetVisualDestroyed(vobMobPtr).MarshalAsString();
            vobMob.owner = pxVobMobGetOwner(vobMobPtr).MarshalAsString();
            vobMob.ownerGuild = pxVobMobGetOwnerGuild(vobMobPtr).MarshalAsString();
            vobMob.destroyed = pxVobMobGetDestroyed(vobMobPtr);
        }

        private static void SetVobMobInterData(IntPtr vobMobInterPtr, PxVobMobInterData vobMobInter)
        {
            SetVobMobData(vobMobInterPtr, vobMobInter);

            vobMobInter.state = pxVobMobInterGetState(vobMobInterPtr);
            vobMobInter.target = pxVobMobInterGetTarget(vobMobInterPtr).MarshalAsString();
            vobMobInter.item = pxVobMobInterGetItem(vobMobInterPtr).MarshalAsString();
            vobMobInter.conditionFunction = pxVobMobInterGetConditionFunction(vobMobInterPtr).MarshalAsString();
            vobMobInter.onStateChangeFunction = pxVobMobInterGetOnStateChangeFunction(vobMobInterPtr).MarshalAsString();
            vobMobInter.rewind = pxVobMobInterGetRewind(vobMobInterPtr);
        }

        private static void SetVobMobFireData(IntPtr vobMobFirePtr, PxVobMobFireData vobMobFire)
        {
            SetVobMobInterData(vobMobFirePtr, vobMobFire);

            vobMobFire.slot = pxVobMobFireGetSlot(vobMobFirePtr).MarshalAsString();
            vobMobFire.vobTree = pxVobMobFireGetVobTree(vobMobFirePtr).MarshalAsString();
        }

        private static void SetVobMobContainerData(IntPtr vobMobContainerPtr, PxVobMobContainerData vobMobContainer)
        {
            SetVobMobInterData(vobMobContainerPtr, vobMobContainer);

            vobMobContainer.locked = pxVobMobContainerGetLocked(vobMobContainerPtr);
            vobMobContainer.key = pxVobMobContainerGetKey(vobMobContainerPtr).MarshalAsString();
            vobMobContainer.pickString = pxVobMobContainerGetPickString(vobMobContainerPtr).MarshalAsString();
            vobMobContainer.contents = pxVobMobContainerGetContents(vobMobContainerPtr).MarshalAsString();
        }

        private static void SetVobMobDoorData(IntPtr vobMobDoorPtr, PxVobMobDoorData vobMobDoor)
        {
            SetVobMobInterData(vobMobDoorPtr, vobMobDoor);
        
            vobMobDoor.locked = pxVobMobDoorGetLocked(vobMobDoorPtr);
            vobMobDoor.key = pxVobMobDoorGetKey(vobMobDoorPtr).MarshalAsString();
            vobMobDoor.pickString = pxVobMobDoorGetPickString(vobMobDoorPtr).MarshalAsString();
        }

        private static void SetVobZoneMusicData(IntPtr vobZoneMusicPtr, PxVobZoneMusicData vobZoneMusic)
        {
            SetVobData(vobZoneMusicPtr, vobZoneMusic);

            pxWorldVobGetZoneMusic(vobZoneMusicPtr, out bool enabled, out int priority, out bool ellipsoid, out float reverb, out float volume, out bool loop);
            vobZoneMusic.enabled = enabled;
            vobZoneMusic.priority = priority;
            vobZoneMusic.ellipsoid = ellipsoid;
            vobZoneMusic.reverb = reverb;
            vobZoneMusic.volume = volume;
            vobZoneMusic.loop = loop;
        }

        private static void SetVobZoneFarPlaneData(IntPtr vobZoneFarPlanePtr, PxVobZoneFarPlaneData vobZoneFarPlane)
        {
            SetVobData(vobZoneFarPlanePtr, vobZoneFarPlane);

            pxWorldVobGetZoneFarPlane(vobZoneFarPlanePtr, out float vobFarPlaneZ, out float innerRangePercentage);

            vobZoneFarPlane.vobFarPlaneZ = vobFarPlaneZ;
            vobZoneFarPlane.innerRangePercentage = innerRangePercentage;
        }

        private static void SetVobZoneFogData(IntPtr vobZoneFogPtr, PxVobZoneFogData vobZoneFog)
        {
            SetVobData(vobZoneFogPtr, vobZoneFog);

            pxWorldVobGetZoneFog(
                vobZoneFogPtr,
                out float rangeCenter,
                out float innerRangePercentage,
                out Vector4Byte color,
                out bool fadeOutSky,
                out bool overrideColor);

            vobZoneFog.rangeCenter = rangeCenter;
            vobZoneFog.innerRangePercentage = innerRangePercentage;
            vobZoneFog.color = color;
            vobZoneFog.fadeOutSky = fadeOutSky;
            vobZoneFog.overrideColor = overrideColor;
        }
    }
}