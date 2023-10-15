using PxCs.Data.Animation;
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

        public enum PxVobSoundMode
        {
			PxVobSoundModeLoop = 0,   // The sound should be player forever until the player exits the trigger volume.
			PxVobSoundModeOnce = 1,   // The sound should be played once when the player enters the trigger volume.
			PxVobSoundModeRandom = 2, // While the player is in the trigger volume, the should should play randomly.
		};

        public enum PxVobSoundTriggerVolume
        {
			PxVobSoundTriggerVolumeSpherical = 0, // The sound is triggered when the player enters a spherical area around the
												  // VOb indicated by its radius setting.
			PxVobSoundTriggerVolumeEllipsoidal = 1, // The sound is triggered when the player enters a ellipsoidal area around
													// the VOb indicated by its radius setting.
		};

        public enum PxVobTriggerMoverBehaviour
        {
            PxVobTriggerMoverBehaviourToggle = 0,
            PxVobTriggerMoverBehaviourTriggerControl = 1,
            PxVobTriggerMoverBehaviourOpenTimed = 2,
            PxVobTriggerMoverBehaviourLoop = 3,
            PxVobTriggerMoverBehaviourSingleKeys = 4
        }

        public enum PxVobTriggerMoverLerpMode
        {
            PxVobTriggerMoverLerpModeCurve = 0,
            PxVobTriggerMoverLerpModeLinear = 1
        }

        public enum PxVobTriggerMoverSpeedMode
        {
            PxVobTriggerMoverSpeedModeSegConstant = 0,
            PxVobTriggerMoverSpeedModeSlowStartEnd = 1,
            PxVobTriggerMoverSpeedModeSlowStart = 2,
            PxVobTriggerMoverSpeedModeSlowEnd = 3,
            PxVobTriggerMoverSpeedModeSegSlowStarEnd = 4,
            PxVobTriggerMoverSpeedModeSegSlowStart = 5,
            PxVobTriggerMoverSpeedModeSegSlowEnd = 6
        }

        public enum PxVobTriggerBatchMode
        {
            PxVobTriggerBatchModeAll = 0,
            PxVobTriggerBatchModeNext = 1,
            PxVobTriggerBatchModeRandom = 2
        }


        [DllImport(DLLNAME)] public static extern IntPtr pxWorldLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxWorldLoadFromVfs(IntPtr vfs, string name);
        [DllImport(DLLNAME)] public static extern void pxWorldDestroy(IntPtr world);

        [DllImport(DLLNAME)] public static extern IntPtr pxWorldGetBspTree(IntPtr world);
        [DllImport(DLLNAME)] public static extern IntPtr pxWorldGetMesh(IntPtr world);
        [DllImport(DLLNAME)] public static extern uint pxWorldGetWayPointCount(IntPtr world);
        [DllImport(DLLNAME)]
        public static extern void pxWorldGetWayPoint(
            IntPtr world,
            uint i,
            out IntPtr namePtr,
            out Vector3 position,
            out Vector3 direction,
            out bool freePoint,
            out bool underwater,
            out int waterDepth);

        [DllImport(DLLNAME)] public static extern uint pxWorldGetWayEdgeCount(IntPtr world);
        [DllImport(DLLNAME)] public static extern void pxWorldGetWayEdge(IntPtr world, uint i, out uint a, out uint b);

        [DllImport(DLLNAME)] public static extern uint pxWorldGetRootVobCount(IntPtr world);
        [DllImport(DLLNAME)] public static extern IntPtr pxWorldGetRootVob(IntPtr world, uint i);

        [DllImport(DLLNAME)] public static extern PxVobType pxVobGetType(IntPtr vob);
        [DllImport(DLLNAME)] public static extern uint pxVobGetId(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxAABBData pxVobGetBbox(IntPtr vob);

        [DllImport(DLLNAME)] public static extern Vector3 pxVobGetPosition(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxMatrix3x3Data pxVobGetRotation(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetShowVisual(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobSpriteAlignment pxVobGetSpriteAlignment(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetCdStatic(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetCdDynamic(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetVobStatic(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobShadowMode pxVobGetShadowMode(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetPhysicsEnabled(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobAnimationMode pxVobGetAnimationMode(IntPtr vob);
        [DllImport(DLLNAME)] public static extern int pxVobGetBias(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetAmbient(IntPtr vob);
        [DllImport(DLLNAME)] public static extern float pxVobGetAnimationStrength(IntPtr vob);
        [DllImport(DLLNAME)] public static extern float pxVobGetFarClipScale(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetPresetName(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetVobName(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetVisualName(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxVobVisualType pxVobGetVisualType(IntPtr vob);

        [DllImport(DLLNAME)] public static extern uint pxVobGetChildCount(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetChild(IntPtr vob, uint i);

        // Decal
        [DllImport(DLLNAME)] public static extern bool pxVobGetGetHasDecal(IntPtr vob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobGetDecalName(IntPtr vob);
        [DllImport(DLLNAME)] public static extern Vector2 pxVobGetDecalDimension(IntPtr vob);
        [DllImport(DLLNAME)] public static extern Vector2 pxVobGetDecalOffset(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetDecalTwoSided(IntPtr vob);
        [DllImport(DLLNAME)] public static extern PxMaterial.PxMaterialAlphaFunction pxVobGetDecalAlphaFunc(IntPtr vob);
        [DllImport(DLLNAME)] public static extern float pxVobGetDecalTextureAnimFps(IntPtr vob);
        [DllImport(DLLNAME)] public static extern byte pxVobGetDecalAlphaWeight(IntPtr vob);
        [DllImport(DLLNAME)] public static extern bool pxVobGetDecalIgnoreDaylight(IntPtr vob);
        
        // Vob - Item
        [DllImport(DLLNAME)] public static extern IntPtr pxVobItemGetInstance(IntPtr vobItem);
        // Vob - Mob
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetName(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern int pxVobMobGetHp(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern int pxVobMobGetDamage(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetMovable(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetTakable(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetFocusOverride(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern PxVobSoundMaterial pxVobMobGetMaterial(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetVisualDestroyed(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetOwner(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobGetOwnerGuild(IntPtr vobMob);
        [DllImport(DLLNAME)] public static extern bool pxVobMobGetDestroyed(IntPtr vobMob);
        //Vob - MobInter
        [DllImport(DLLNAME)] public static extern int pxVobMobInterGetState(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetTarget(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetItem(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetConditionFunction(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobInterGetOnStateChangeFunction(IntPtr vobMobInter);
        [DllImport(DLLNAME)] public static extern bool pxVobMobInterGetRewind(IntPtr vobMobInter);
        // Vob - MobFire
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobFireGetSlot(IntPtr vobMobFire);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobFireGetVobTree(IntPtr vobMobFire);
        // Vob - MobContainer
        [DllImport(DLLNAME)] public static extern bool pxVobMobContainerGetLocked(IntPtr vobMobContainer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobContainerGetKey(IntPtr vobMobContainer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobContainerGetPickString(IntPtr vobMobContainer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobContainerGetContents(IntPtr vobMobContainer);
        // Vob - MobDoor
        [DllImport(DLLNAME)] public static extern bool pxVobMobDoorGetLocked(IntPtr vobMobDoor);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobDoorGetKey(IntPtr vobMobDoor);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMobDoorGetPickString(IntPtr vobMobDoor);

        // Trigger
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerGetTarget(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern uint pxVobTriggerGetFlags(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern uint pxVobTriggerGetFilterFlags(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerGetVobTarget(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern int pxVobTriggerGetMaxActivationCount(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerGetRetriggerDelaySec(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerGetDamageThreshold(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerGetFireDelaySec(IntPtr trigger);
        // Trigger save-game only variables
        [DllImport(DLLNAME)] public static extern float pxVobTriggerGetSNextTimeTriggerable(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern int pxVobTriggerGetSCountCanBeActivated(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern bool pxVobTriggerGetSIsEnabled(IntPtr trigger);
        // Trigger - Mover
        [DllImport(DLLNAME)] public static extern PxVobTriggerMoverBehaviour pxVobTriggerMoverGetBehaviour(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerMoverGetTouchBlockerDamage(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerMoverGetStayOpenTimeSec(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern bool pxVobTriggerMoverGetLocked(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern bool pxVobTriggerMoverGetAutoLink(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern bool pxVobTriggerMoverGetAutoRotate(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerMoverGetSpeed(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern PxVobTriggerMoverLerpMode pxVobTriggerMoverGetLerpMode(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern PxVobTriggerMoverSpeedMode pxVobTriggerMoverGetSpeedMode(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern uint pxVobTriggerMoverGetKeyframeCount(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern void pxVobTriggerMoverGetKeyframe(IntPtr trigger, uint i, out Vector3 position, out PxQuaternionData rotation);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxOpenStart(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxOpenEnd(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxTransitioning(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxCloseStart(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxCloseEnd(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxLock(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxUnlock(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerMoverGetSfxUseLocked(IntPtr trigger);
        // Trigger - Mover save-game only variables
        [DllImport(DLLNAME)] public static extern Vector3 pxVobTriggerMoverGetSActKeyPosDelta(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerMoverGetSActKeyframeF(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern int pxVobTriggerMoverGetSActKeyframe(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern int pxVobTriggerMoverGetSNextKeyFrame(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerMoverGetMoveSpeedUnit(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerMoverGetSAdvanceDir(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern uint pxVobTriggerMoverGetSMoverState(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern int pxVobTriggerMoverGetSTriggerEventCount(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerMoverGetSStayOpenTimeDest(IntPtr trigger);
        // Trigger - List
        [DllImport(DLLNAME)] public static extern PxVobTriggerBatchMode pxVobTriggerListGetTriggerBatchMode(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern uint pxVobTriggerListGetTargetsCount(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerListGetTargetName(IntPtr trigger, uint i);
        [DllImport(DLLNAME)] public static extern float pxVobTriggerListGetTargetDelay(IntPtr trigger, uint i);
        // Trigger - List save-game only variables
        [DllImport(DLLNAME)] public static extern uint pxVobTriggerListGetSActTarget(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern bool pxVobTriggerListGetSSendOnTrigger(IntPtr trigger);
        // Trigger - Script
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerScriptGetFunction(IntPtr trigger);
        // Trigger - Change Level
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerChangeLevelGetLevelName(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerChangeLevelGetStartVob(IntPtr trigger);
        // Trigger - World Start
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerWorldStartGetTarget(IntPtr trigger);
        [DllImport(DLLNAME)] public static extern bool pxVobTriggerWorldStartGetFireOnce(IntPtr trigger);
        // Trigger - World Start save-game only variables
        [DllImport(DLLNAME)] public static extern bool pxVobTriggerWorldStartGetSHasFired(IntPtr trigger);
        // Trigger - Untouch
        [DllImport(DLLNAME)] public static extern IntPtr pxVobTriggerUntouchGetTarget(IntPtr trigger);

        // Vob - Sound
        [DllImport(DLLNAME)] public static extern float pxVobSoundGetVolume(IntPtr sound);
		[DllImport(DLLNAME)] public static extern PxVobSoundMode pxVobSoundGetSoundMode(IntPtr sound);
		[DllImport(DLLNAME)] public static extern float pxVobSoundGetRandomDelay(IntPtr sound);
		[DllImport(DLLNAME)] public static extern float pxVobSoundGetRandomDelayVar(IntPtr sound);
		[DllImport(DLLNAME)] public static extern bool pxVobSoundGetInitiallyPlaying(IntPtr sound);
		[DllImport(DLLNAME)] public static extern bool pxVobSoundGetAmbient3d(IntPtr sound);
        [DllImport(DLLNAME)] public static extern bool pxVobSoundGetObstruction(IntPtr sound);
		[DllImport(DLLNAME)] public static extern float pxVobSoundGetConeAngle(IntPtr sound);
		[DllImport(DLLNAME)] public static extern PxVobSoundTriggerVolume pxVobSoundGetSoundTriggerVolume(IntPtr sound);
		[DllImport(DLLNAME)] public static extern float pxVobSoundGetRadius(IntPtr sound);
		[DllImport(DLLNAME)] public static extern IntPtr pxVobSoundGetSoundName(IntPtr sound);
		// Vob - SoundDaytime
		[DllImport(DLLNAME)] public static extern float pxVobSoundDaytimeStartTime(IntPtr soundDaytime);
		[DllImport(DLLNAME)] public static extern float pxVobSoundDaytimeEndTime(IntPtr soundDaytime);
		[DllImport(DLLNAME)] public static extern IntPtr pxVobSoundDaytimeSoundName2(IntPtr soundDaytime);

		// Vob - ZoneMusic
		[DllImport(DLLNAME)] public static extern void pxWorldVobGetZoneMusic(
            IntPtr zoneMusic,
            out bool enabled,
            out int priority,
            out bool ellipsoid,
            out float reverb,
            out float volume,
            out bool loop);
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
            out bool fadeOutSky,
            out bool overrideColor);



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
                case PxVobType.PxVob_oCItem:
                    vob = new PxVobItemData();
                    SetVobItemData(vobPtr, (PxVobItemData)vob);
                    break;
                case PxVobType.PxVob_zCTriggerWorldStart:
                    vob = new PxVobTriggerWorldStartData();
                    SetVobTriggerWorldStart(vobPtr, (PxVobTriggerWorldStartData)vob);
                    break;
                case PxVobType.PxVob_zCTriggerUntouch:
                    vob = new PxVobTriggerUntouchData();
                    SetVobTriggerUntouch(vobPtr, (PxVobTriggerUntouchData)vob);
                    break;
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
                case PxVobType.PxVob_zCTrigger:
                    vob = new PxVobTriggerData();
                    SetVobTriggerData(vobPtr, (PxVobTriggerData)vob);
                    break;
                case PxVobType.PxVob_zCTriggerList:
                    vob = new PxVobTriggerListData();
                    SetVobTriggerListData(vobPtr, (PxVobTriggerListData)vob);
                    break;
                case PxVobType.PxVob_oCTriggerScript:
                    vob = new PxVobTriggerScriptData();
                    SetVobTriggerScriptData(vobPtr, (PxVobTriggerScriptData)vob);
                    break;
                case PxVobType.PxVob_oCTriggerChangeLevel:
                    vob = new PxVobTriggerChangeLevelData();
                    SetVobTriggerChangeLevelData(vobPtr, (PxVobTriggerChangeLevelData)vob);
                    break;
                case PxVobType.PxVob_zCMover:
                    vob = new PxVobTriggerMoverData();
                    SetVobTriggerMoverData(vobPtr, (PxVobTriggerMoverData)vob);
                    break;
                case PxVobType.PxVob_zCVobSound:
                    vob = new PxVobSoundData();
                    SetVobSoundData(vobPtr, (PxVobSoundData)vob);
                    break;
                case PxVobType.PxVob_zCVobSoundDaytime:
                    vob = new PxVobSoundDaytimeData();
                    SetVobSoundDaytimeData(vobPtr, (PxVobSoundDaytimeData)vob);
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

            vob.boundingBox = pxVobGetBbox(vobPtr);

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

            if (!pxVobGetGetHasDecal(vobPtr))
                return;
            
            var decalData = new VobDecalData()
            {
                name = pxVobGetDecalName(vobPtr).MarshalAsString(),
                dimension = pxVobGetDecalDimension(vobPtr),
                offset = pxVobGetDecalOffset(vobPtr),
                twoSided = pxVobGetDecalTwoSided(vobPtr),
                alphaFunc = pxVobGetDecalAlphaFunc(vobPtr),
                textureAnimFps = pxVobGetDecalTextureAnimFps(vobPtr),
                alphaWeight = pxVobGetDecalAlphaWeight(vobPtr),
                ignoreDaylight =pxVobGetDecalIgnoreDaylight(vobPtr) 
            };

            vob.vobDecal = decalData;
        }

        private static void SetVobItemData(IntPtr vobItemPtr, PxVobItemData vobItem)
        {
            SetVobData(vobItemPtr, vobItem);

            vobItem.instance = pxVobItemGetInstance(vobItemPtr).MarshalAsString();
        }

        private static void SetVobTriggerWorldStart(IntPtr vobTriggerWorldStartPtr, PxVobTriggerWorldStartData vobTriggerWorldStart)
        {
            SetVobData(vobTriggerWorldStartPtr, vobTriggerWorldStart);

            vobTriggerWorldStart.target = pxVobTriggerWorldStartGetTarget(vobTriggerWorldStartPtr).MarshalAsString();
            vobTriggerWorldStart.fireOnce = pxVobTriggerWorldStartGetFireOnce(vobTriggerWorldStartPtr);
            vobTriggerWorldStart.sHasFired = pxVobTriggerWorldStartGetSHasFired(vobTriggerWorldStartPtr);
        }

        private static void SetVobTriggerUntouch(IntPtr vobTriggerUntouchPtr, PxVobTriggerUntouchData vobTriggerUntouch)
        {
            SetVobData(vobTriggerUntouchPtr, vobTriggerUntouch);
            vobTriggerUntouch.target = pxVobTriggerUntouchGetTarget(vobTriggerUntouchPtr).MarshalAsString();
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

        private static void SetVobTriggerData(IntPtr vobTriggerPtr, PxVobTriggerData vobTrigger)
        {
            SetVobData(vobTriggerPtr, vobTrigger);

            vobTrigger.target = pxVobTriggerGetTarget(vobTriggerPtr).MarshalAsString();
            vobTrigger.flags = pxVobTriggerGetFlags(vobTriggerPtr);
            vobTrigger.filterFlags = pxVobTriggerGetFilterFlags(vobTriggerPtr);
            vobTrigger.maxActivationCount = pxVobTriggerGetMaxActivationCount(vobTriggerPtr);
            vobTrigger.retriggerDelaySec = pxVobTriggerGetRetriggerDelaySec(vobTriggerPtr);
            vobTrigger.damageThreshold = pxVobTriggerGetDamageThreshold(vobTriggerPtr);
            vobTrigger.fireDelaySec = pxVobTriggerGetFireDelaySec(vobTriggerPtr);
        }

        private static void SetVobTriggerListData(IntPtr vobTriggerListPtr, PxVobTriggerListData vobTriggerList)
        {
            SetVobTriggerData(vobTriggerListPtr, vobTriggerList);

            vobTriggerList.mode = pxVobTriggerListGetTriggerBatchMode(vobTriggerListPtr);
            var count = pxVobTriggerListGetTargetsCount(vobTriggerListPtr);
            var array = new PxVobTriggerListData.PxTarget[count];
            for (var i = 0u; i < count; i++)
            {
                var name = pxVobTriggerListGetTargetName(vobTriggerListPtr, i);
                var delay = pxVobTriggerListGetTargetDelay(vobTriggerListPtr, i);
                array[i] = new PxVobTriggerListData.PxTarget()
                {
                    name = name.MarshalAsString(),
                    delay = delay
                };
            }
            vobTriggerList.targets = array;
        }

        private static void SetVobTriggerScriptData(IntPtr vobTriggerScriptPtr, PxVobTriggerScriptData vobTriggerScript)
        {
            SetVobTriggerData(vobTriggerScriptPtr, vobTriggerScript);

            vobTriggerScript.function = pxVobTriggerScriptGetFunction(vobTriggerScriptPtr).MarshalAsString();
        }


        private static void SetVobTriggerChangeLevelData(IntPtr vobTriggerChangeLevelPtr, PxVobTriggerChangeLevelData vobTriggerChangeLevel)
        {
            SetVobTriggerData(vobTriggerChangeLevelPtr, vobTriggerChangeLevel);

            vobTriggerChangeLevel.levelName = pxVobTriggerChangeLevelGetLevelName(vobTriggerChangeLevelPtr).MarshalAsString();
            vobTriggerChangeLevel.startVob = pxVobTriggerChangeLevelGetStartVob(vobTriggerChangeLevelPtr).MarshalAsString();
        }

        private static void SetVobTriggerMoverData(IntPtr vobTriggerMoverPtr, PxVobTriggerMoverData vobTriggerMover)
        {
            SetVobTriggerData(vobTriggerMoverPtr, vobTriggerMover);

            vobTriggerMover.behaviour = pxVobTriggerMoverGetBehaviour(vobTriggerMoverPtr);
            vobTriggerMover.touchBlockerDamage = pxVobTriggerMoverGetTouchBlockerDamage(vobTriggerMoverPtr);
            vobTriggerMover.stayOpenTimeSec = pxVobTriggerMoverGetStayOpenTimeSec(vobTriggerMoverPtr);
            vobTriggerMover.locked = pxVobTriggerMoverGetLocked(vobTriggerMoverPtr);
            vobTriggerMover.autoLink = pxVobTriggerMoverGetAutoLink(vobTriggerMoverPtr);
            vobTriggerMover.autoRotate = pxVobTriggerMoverGetAutoRotate(vobTriggerMoverPtr);
            vobTriggerMover.speed = pxVobTriggerMoverGetSpeed(vobTriggerMoverPtr);
            vobTriggerMover.lerpMode = pxVobTriggerMoverGetLerpMode(vobTriggerMoverPtr);
            vobTriggerMover.speedMode = pxVobTriggerMoverGetSpeedMode(vobTriggerMoverPtr);

            var count = pxVobTriggerMoverGetKeyframeCount(vobTriggerMoverPtr);
            var array = new PxAnimationSampleData[count];
            for (var i = 0u; i < count; i++)
            {
                pxVobTriggerMoverGetKeyframe(vobTriggerMoverPtr, i, out Vector3 position, out PxQuaternionData rotation);
                array[i] = new PxAnimationSampleData()
                {
                    position = position,
                    rotation = rotation
                };
            }
            vobTriggerMover.keyframes = array;

            vobTriggerMover.sfxOpenStart = pxVobTriggerMoverGetSfxOpenStart(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxOpenEnd = pxVobTriggerMoverGetSfxOpenEnd(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxTransitioning = pxVobTriggerMoverGetSfxTransitioning(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxCloseStart = pxVobTriggerMoverGetSfxCloseStart(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxCloseEnd = pxVobTriggerMoverGetSfxCloseEnd(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxLock = pxVobTriggerMoverGetSfxLock(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxUnlock = pxVobTriggerMoverGetSfxUnlock(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxUseLocked = pxVobTriggerMoverGetSfxUseLocked(vobTriggerMoverPtr).MarshalAsString();
        }

        private static void SetVobSoundData(IntPtr vobSoundPtr, PxVobSoundData vobSound)
        {
            SetVobData(vobSoundPtr, vobSound);

			vobSound.volume = pxVobSoundGetVolume(vobSoundPtr);
			vobSound.mode = pxVobSoundGetSoundMode(vobSoundPtr);
			vobSound.randomDelay = pxVobSoundGetRandomDelay(vobSoundPtr);
			vobSound.randomDelayVar = pxVobSoundGetRandomDelayVar(vobSoundPtr);
			vobSound.initiallyPlaying = pxVobSoundGetInitiallyPlaying(vobSoundPtr);
			vobSound.ambient3d = pxVobSoundGetAmbient3d(vobSoundPtr);
			vobSound.obstruction = pxVobSoundGetObstruction(vobSoundPtr);
			vobSound.coneAngle = pxVobSoundGetConeAngle(vobSoundPtr);
			vobSound.volumeType = pxVobSoundGetSoundTriggerVolume(vobSoundPtr);
			vobSound.radius = pxVobSoundGetRadius(vobSoundPtr);
			vobSound.soundName = pxVobSoundGetSoundName(vobSoundPtr).MarshalAsString();
		}

		private static void SetVobSoundDaytimeData(IntPtr vobSoundDaytimePtr, PxVobSoundDaytimeData vobSoundDaytime)
		{
			SetVobSoundData(vobSoundDaytimePtr, vobSoundDaytime);

			vobSoundDaytime.startTime = pxVobSoundDaytimeStartTime(vobSoundDaytimePtr);
			vobSoundDaytime.endTime = pxVobSoundDaytimeEndTime(vobSoundDaytimePtr);
			vobSoundDaytime.soundName2 = pxVobSoundDaytimeSoundName2(vobSoundDaytimePtr).MarshalAsString();
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