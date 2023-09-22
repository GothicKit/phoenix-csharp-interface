using PxCs.Data.Animation;
using PxCs.Data.Struct;
using PxCs.Data.Vob;
using PxCs.Data.WayNet;
using PxCs.Extensions;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using static PxCs.Data.Vob.PxVobNpcData;

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

        public enum PxVobMessageFilterAction
        {
            PxVobMessageFilterActionNone = 0,
            PxVobMessageFilterActionTrigger = 1,
            PxVobMessageFilterActionUntrigger = 2,
            PxVobMessageFilterActionEnable = 3,
            PxVobMessageFilterActionDisable = 4,
            PxVobMessageFilterActionToggle = 5,
        }

        public enum PxVobMoverMessageType : uint
        {
            PxVobMoverMessageTypeFixedDirect = 0,
            PxVobMoverMessageTypeFixedOrder = 1,
            PxVobMoverMessageTypeNext = 2,
            PxVobMoverMessageTypePrevious = 3,
        }

        public enum PxVobCollisionType : uint
        {
            PxVobCollisionTypeNone = 0,
            PxVobCollisionTypeBox = 1,
            PxVobCollisionTypePoint = 2,
        }

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

        public enum PxLightMode : uint
        {
            PxLightModePoint = 0,
            PxLightModeSpot = 1,
        }

        public enum PxLightQuality : uint
        {
            PxLightQualityHigh = 0,
            PxLightQualityMedium = 1,
            PxLightQualityLow = 2
        }


        [DllImport(DLLNAME)] public static extern IntPtr pxWorldLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxWorldLoadFromVfs(IntPtr vfs, string name);
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
        
        // Light Preset
        [DllImport(DLLNAME)] public static extern IntPtr pxLightPresetGetPreset(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern uint pxLightPresetGetLightType(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern float pxLightPresetGetRange(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern Vector4Byte pxLightPresetGetColor(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern float pxLightPresetGetConeAngle(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern bool pxLightPresetGetIsStatic(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern uint pxLightPresetGetQuality(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern IntPtr pxLightPresetGetLensFlareFx(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern bool pxLightPresetGetOn(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern uint pxLightPresetGetRangeAnimationScaleCount(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern float pxLightPresetGetRangeAnimationScale(IntPtr lightPreset, uint i);
        [DllImport(DLLNAME)] public static extern float pxLightPresetGetRangeAnimationFps(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern bool pxLightPresetGetRangeAnimationSmooth(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern uint pxLightPresetGetColorAnimationListCount(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern Vector4Byte pxLightPresetGetColorAnimationList(IntPtr lightPreset, uint i);
        [DllImport(DLLNAME)] public static extern float pxLightPresetGetColorAnimationFps(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern bool pxLightPresetGetColorAnimationSmooth(IntPtr lightPreset);
        [DllImport(DLLNAME)] public static extern bool pxLightPresetGetCanMove(IntPtr lightPreset);

        // Vob - Animate
        [DllImport(DLLNAME)] public static extern bool pxVobAnimateGetStartOn(IntPtr animate);

        // Vob - Item
        [DllImport(DLLNAME)] public static extern IntPtr pxVobItemGetInstance(IntPtr item);

        // Vob - Lens Flare
        [DllImport(DLLNAME)] public static extern IntPtr pxVobLensFlareGetFx(IntPtr lensFlare);

        // Vob - Pfx Controller
        [DllImport(DLLNAME)] public static extern IntPtr pxVobPfxControllerGetPfxName(IntPtr pfxController);
        [DllImport(DLLNAME)] public static extern bool pxVobPfxControllerGetKillWhenDone(IntPtr pfxController);
        [DllImport(DLLNAME)] public static extern bool pxVobPfxControllerGetInitiallyRunning(IntPtr pfxController);

        // Vob - Message Filter
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMessageFilterGetTarget(IntPtr messageFilter);
        [DllImport(DLLNAME)] public static extern uint pxVobMessageFilterGetOnTrigger(IntPtr messageFilter);
        [DllImport(DLLNAME)] public static extern uint pxVobMessageFilterGetOnUntrigger(IntPtr messageFilter);

        // Vob - Code Master
        [DllImport(DLLNAME)] public static extern IntPtr pxVobCodeMasterGetTarget(IntPtr codeMaster);
        [DllImport(DLLNAME)] public static extern bool pxVobCodeMasterGetOrdered(IntPtr codeMaster);
        [DllImport(DLLNAME)] public static extern bool pxVobCodeMasterGetFirstFalseIsFailure(IntPtr codeMaster);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobCodeMasterGetFailureTarget(IntPtr codeMaster);
        [DllImport(DLLNAME)] public static extern bool pxVobCodeMasterGetUntriggeredCancels(IntPtr codeMaster);
        [DllImport(DLLNAME)] public static extern uint pxVobCodeMasterGetSlavesCount(IntPtr codeMaster);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobCodeMasterGetSlaves(IntPtr codeMaster, uint i);

        // Vob - Mover Controller
        [DllImport(DLLNAME)] public static extern IntPtr pxVobMoverControllerGetTarget(IntPtr moverController);
        [DllImport(DLLNAME)] public static extern uint pxVobMoverControllerGetMessage(IntPtr moverController);
        [DllImport(DLLNAME)] public static extern int pxVobMoverControllerGetKey(IntPtr moverController);

        // Vob - Touch Damage
        [DllImport(DLLNAME)] public static extern float pxVobTouchDamageGetDamage(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetBarrier(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetBlunt(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetEdge(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetFire(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetFly(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetMagic(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetPoint(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern bool pxVobTouchDamageGetFall(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern float pxVobTouchDamageGetRepearDelaySec(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern float pxVobTouchDamageGetVolumeScale(IntPtr touchDamage);
        [DllImport(DLLNAME)] public static extern uint pxVobTouchDamageGetCollision(IntPtr touchDamage);

        // Vob - Earthquake
        [DllImport(DLLNAME)] public static extern float pxVobEarthquakeGetRadius(IntPtr earthquake);
        [DllImport(DLLNAME)] public static extern float pxVobEarthquakeGetDuration(IntPtr earthquake);
        [DllImport(DLLNAME)] public static extern Vector3 pxVobEarthquakeGetAmplitude(IntPtr earthquake);

        // Vob - Npc
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetNpcInstance(IntPtr npc);
        [DllImport(DLLNAME)] public static extern Vector3 pxVobNpcGetModelScale(IntPtr npc);
        [DllImport(DLLNAME)] public static extern float pxVobNpcGetModelFatness(IntPtr npc);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetOverlaysCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetOverlays(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetFlags(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetGuild(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetGuildTrue(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetLevel(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetXp(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetXpNextLevel(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetLp(IntPtr npc);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetTalentsCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern void pxVobNpcGetTalents(IntPtr npc, uint i, out int talent, out int value, out int skill);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetFightTactic(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetFightMode(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetWounded(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetMad(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetMadTime(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetPlayer(IntPtr npc);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetAttributesCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetAttributes(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetHcsCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetHcs(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetMissionsCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetMissions(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetStartAiState(IntPtr npc);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetAivarCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetAivar(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetScriptWaypoint(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetAttitude(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetAttitudeTemp(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetNameNr(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetMoveLock(IntPtr npc);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetPackedCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetPacked(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetItemsCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetItems(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetSlotsCount(IntPtr npc);
        [DllImport(DLLNAME)]
        public static extern void pxVobNpcGetSlots(IntPtr npc, uint i, out bool used, out IntPtr name,
                    out int itemIndex, out bool inInventory);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetCurrentStateValid(IntPtr npc);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetCurrentStateName(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetCurrentStateIndex(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetCurrentStateIsRoutine(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetNextStateValid(IntPtr npc);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetNextStateName(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetNextStateIndex(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetNextStateIsRoutine(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetLastAiState(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetHasRoutine(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetRoutineChanged(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetRoutineOverlay(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetRoutineOverlayCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetWalkmodeRoutine(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetWeaponmodeRoutine(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetStartNewRoutine(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetAiStateDriven(IntPtr npc);
        [DllImport(DLLNAME)] public static extern Vector3 pxVobNpcGetAiStatePos(IntPtr npc);
        [DllImport(DLLNAME)] public static extern IntPtr pxVobNpcGetCurrentRoutine(IntPtr npc);
        [DllImport(DLLNAME)] public static extern bool pxVobNpcGetRespawn(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetRespawnTime(IntPtr npc);
        [DllImport(DLLNAME)] public static extern uint pxVobNpcGetProtectionCount(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetProtection(IntPtr npc, uint i);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetBsInterruptableOverride(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetNpcType(IntPtr npc);
        [DllImport(DLLNAME)] public static extern int pxVobNpcGetSpellMana(IntPtr npc);

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
                case PxVobType.PxVob_oCNpc:
                    vob = new PxVobNpcData();
                    SetVobNpcData(vobPtr, (PxVobNpcData)vob);
                    break;
                case PxVobType.PxVob_zCMoverController:
                    vob = new PxVobMoverControllerData();
                    SetVobMoverControllerData(vobPtr, (PxVobMoverControllerData)vob);
                    break;
                case PxVobType.PxVob_zCPFXController:
                    vob = new PxVobPfxControllerData();
                    SetVobPfxControllerData(vobPtr, (PxVobPfxControllerData)vob);
                    break;
                case PxVobType.PxVob_zCVobAnimate:
                    vob = new PxVobAnimateData();
                    SetVobAnimateData(vobPtr, (PxVobAnimateData)vob);
                    break;
                case PxVobType.PxVob_zCVobLensFlare:
                    vob = new PxVobLensFlareData();
                    SetVobLensFlareData(vobPtr, (PxVobLensFlareData)vob);
                    break;
                case PxVobType.PxVob_zCVobLight:
                    vob = new PxVobLightData();
                    SetVobLightData(vobPtr, (PxVobLightData)vob);
                    break;
                case PxVobType.PxVob_zCMessageFilter:
                    vob = new PxVobMessageFilterData();
                    SetVobMessageFilterData(vobPtr, (PxVobMessageFilterData)vob);
                    break;
                case PxVobType.PxVob_zCCodeMaster:
                    vob = new PxVobCodeMasterData();
                    SetVobCodeMasterData(vobPtr, (PxVobCodeMasterData)vob);
                    break;
                case PxVobType.PxVob_zCTriggerWorldStart:
                    vob = new PxVobTriggerWorldStartData();
                    SetVobTriggerWorldStart(vobPtr, (PxVobTriggerWorldStartData)vob);
                    break;
                case PxVobType.PxVob_oCTouchDamage:
                    vob = new PxVobTouchDamageData();
                    SetVobTouchDamageData(vobPtr, (PxVobTouchDamageData)vob);
                    break;
                case PxVobType.PxVob_zCTriggerUntouch:
                    vob = new PxVobTriggerUntouchData();
                    SetVobTriggerUntouch(vobPtr, (PxVobTriggerUntouchData)vob);
                    break;
                case PxVobType.PxVob_zCEarthquake:
                    vob = new PxVobEarthQuakeData();
                    SetVobEarthquakeData(vobPtr, (PxVobEarthQuakeData)vob);
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
                ignoreDaylight = pxVobGetDecalIgnoreDaylight(vobPtr)
            };

            vob.vobDecal = decalData;
        }

        private static void SetVobItemData(IntPtr vobItemPtr, PxVobItemData vobItem)
        {
            SetVobData(vobItemPtr, vobItem);

            vobItem.instance = pxVobItemGetInstance(vobItemPtr).MarshalAsString();
        }

        private static void SetVobNpcData(IntPtr vobNpcPtr, PxVobNpcData vobNpc)
        {
            var overlaysCount = pxVobNpcGetOverlaysCount(vobNpcPtr);
            var overlays = new string[overlaysCount];
            for (uint i = 0; i < overlaysCount; i++) overlays[i] = pxVobNpcGetOverlays(vobNpcPtr, i).MarshalAsString();

            var talentsCount = pxVobNpcGetTalentsCount(vobNpcPtr);
            var talents = new PxVobNpcTalentData[talentsCount];
            for (uint i = 0; i < talentsCount; i++)
                pxVobNpcGetTalents(vobNpcPtr, i, out talents[i].talent, out talents[i].value, out talents[i].skill);

            var attributesCount = pxVobNpcGetAttributesCount(vobNpcPtr);
            var attributes = new int[attributesCount];
            for (uint i = 0; i < attributesCount; i++) attributes[i] = pxVobNpcGetAttributes(vobNpcPtr, i);

            var hcsCount = pxVobNpcGetHcsCount(vobNpcPtr);
            var hcs = new int[hcsCount];
            for (uint i = 0; i < hcsCount; i++) hcs[i] = pxVobNpcGetHcs(vobNpcPtr, i);

            var missionsCount = pxVobNpcGetMissionsCount(vobNpcPtr);
            var missions = new int[missionsCount];
            for (uint i = 0; i < missionsCount; i++) missions[i] = pxVobNpcGetMissions(vobNpcPtr, i);

            var aivarCount = pxVobNpcGetAivarCount(vobNpcPtr);
            var aivar = new int[aivarCount];
            for (uint i = 0; i < aivarCount; i++) aivar[i] = pxVobNpcGetAivar(vobNpcPtr, i);

            var packedCount = pxVobNpcGetPackedCount(vobNpcPtr);
            var packed = new string[packedCount];
            for (uint i = 0; i < packedCount; i++) packed[i] = pxVobNpcGetPacked(vobNpcPtr, i).MarshalAsString();

            var itemsCount = pxVobNpcGetItemsCount(vobNpcPtr);
            var items = new PxVobItemData[itemsCount];
            for (uint i = 0; i < itemsCount; i++)
            {
                var itemPtr = pxVobNpcGetItems(vobNpcPtr, i);
                SetVobItemData(itemPtr, items[i]);
            }

            var slotsCount = pxVobNpcGetSlotsCount(vobNpcPtr);
            var slots = new PxVobNpcSlotData[slotsCount];
            for (uint i = 0; i < slotsCount; i++)
            {
                IntPtr name;
                pxVobNpcGetSlots(vobNpcPtr, i, out slots[i].used, out name, out slots[i].itemIndex,
                    out slots[i].inInventory);
                slots[i].name = name.MarshalAsString();
            }

            var protectionCount = pxVobNpcGetProtectionCount(vobNpcPtr);
            var protection = new int[protectionCount];
            for (uint i = 0; i < protectionCount; i++) protection[i] = pxVobNpcGetProtection(vobNpcPtr, i);

            SetVobData(vobNpcPtr, vobNpc);

            vobNpc.npcInstance = pxVobNpcGetNpcInstance(vobNpcPtr).MarshalAsString();
            vobNpc.modelScale = pxVobNpcGetModelScale(vobNpcPtr);
            vobNpc.modelFatness = pxVobNpcGetModelFatness(vobNpcPtr);
            vobNpc.overlays = overlays;
            vobNpc.flags = pxVobNpcGetFlags(vobNpcPtr);
            vobNpc.guild = pxVobNpcGetGuild(vobNpcPtr);
            vobNpc.guildTrue = pxVobNpcGetGuildTrue(vobNpcPtr);
            vobNpc.level = pxVobNpcGetLevel(vobNpcPtr);
            vobNpc.xp = pxVobNpcGetXp(vobNpcPtr);
            vobNpc.xpNextLevel = pxVobNpcGetXpNextLevel(vobNpcPtr);
            vobNpc.lp = pxVobNpcGetLp(vobNpcPtr);
            vobNpc.talents = talents;
            vobNpc.fightTactic = pxVobNpcGetFightTactic(vobNpcPtr);
            vobNpc.fightMode = pxVobNpcGetFightMode(vobNpcPtr);
            vobNpc.wounded = pxVobNpcGetWounded(vobNpcPtr);
            vobNpc.mad = pxVobNpcGetMad(vobNpcPtr);
            vobNpc.madTime = pxVobNpcGetMadTime(vobNpcPtr);
            vobNpc.player = pxVobNpcGetPlayer(vobNpcPtr);
            vobNpc.attributes = attributes;
            vobNpc.hcs = hcs;
            vobNpc.missions = missions;
            vobNpc.startAiState = pxVobNpcGetStartAiState(vobNpcPtr).MarshalAsString();
            vobNpc.aivar = aivar;
            vobNpc.scriptWaypoint = pxVobNpcGetScriptWaypoint(vobNpcPtr).MarshalAsString();
            vobNpc.attitude = pxVobNpcGetAttitude(vobNpcPtr);
            vobNpc.attitudeTemp = pxVobNpcGetAttitudeTemp(vobNpcPtr);
            vobNpc.nameNr = pxVobNpcGetNameNr(vobNpcPtr);
            vobNpc.moveLock = pxVobNpcGetMoveLock(vobNpcPtr);
            vobNpc.packed = packed;
            vobNpc.items = items;
            vobNpc.slots = slots;
            vobNpc.currentStateValid = pxVobNpcGetCurrentStateValid(vobNpcPtr);
            vobNpc.currentStateName = pxVobNpcGetCurrentStateName(vobNpcPtr).MarshalAsString();
            vobNpc.currentStateIndex = pxVobNpcGetCurrentStateIndex(vobNpcPtr);
            vobNpc.currentStateIsRoutine = pxVobNpcGetCurrentStateIsRoutine(vobNpcPtr);
            vobNpc.nextStateValid = pxVobNpcGetNextStateValid(vobNpcPtr);
            vobNpc.nextStateName = pxVobNpcGetNextStateName(vobNpcPtr).MarshalAsString();
            vobNpc.nextStateIndex = pxVobNpcGetNextStateIndex(vobNpcPtr);
            vobNpc.nextStateIsRoutine = pxVobNpcGetNextStateIsRoutine(vobNpcPtr);
            vobNpc.lastAiState = pxVobNpcGetLastAiState(vobNpcPtr);
            vobNpc.hasRoutine = pxVobNpcGetHasRoutine(vobNpcPtr);
            vobNpc.routineChanged = pxVobNpcGetRoutineChanged(vobNpcPtr);
            vobNpc.routineOverlay = pxVobNpcGetRoutineOverlay(vobNpcPtr);
            vobNpc.routineOverlayCount = pxVobNpcGetRoutineOverlayCount(vobNpcPtr);
            vobNpc.walkmodeRoutine = pxVobNpcGetWalkmodeRoutine(vobNpcPtr);
            vobNpc.weaponmodeRoutine = pxVobNpcGetWeaponmodeRoutine(vobNpcPtr);
            vobNpc.startNewRoutine = pxVobNpcGetStartNewRoutine(vobNpcPtr);
            vobNpc.aiStateDriven = pxVobNpcGetAiStateDriven(vobNpcPtr);
            vobNpc.aiStatePos = pxVobNpcGetAiStatePos(vobNpcPtr);
            vobNpc.currentRoutine = pxVobNpcGetCurrentRoutine(vobNpcPtr).MarshalAsString();
            vobNpc.respawn = pxVobNpcGetRespawn(vobNpcPtr);
            vobNpc.respawnTime = pxVobNpcGetRespawnTime(vobNpcPtr);
            vobNpc.protection = protection;
            vobNpc.bsInterruptableOverride = pxVobNpcGetBsInterruptableOverride(vobNpcPtr);
            vobNpc.npcType = pxVobNpcGetNpcType(vobNpcPtr);
            vobNpc.spellMana = pxVobNpcGetSpellMana(vobNpcPtr);
        }

        private static void SetVobMoverControllerData(IntPtr vobMoverControllerPtr,
            PxVobMoverControllerData vobMoverController)
        {
            SetVobData(vobMoverControllerPtr, vobMoverController);

            vobMoverController.target = pxVobMoverControllerGetTarget(vobMoverControllerPtr).MarshalAsString();
            vobMoverController.message = pxVobMoverControllerGetMessage(vobMoverControllerPtr);
            vobMoverController.key = pxVobMoverControllerGetKey(vobMoverControllerPtr);
        }

        private static void SetVobPfxControllerData(IntPtr vobPfxControllerPtr, PxVobPfxControllerData vobPfxController)
        {
            SetVobData(vobPfxControllerPtr, vobPfxController);

            vobPfxController.pfxName = pxVobPfxControllerGetPfxName(vobPfxControllerPtr).MarshalAsString();
            vobPfxController.killWhenDone = pxVobPfxControllerGetKillWhenDone(vobPfxControllerPtr);
            vobPfxController.initiallyRunning = pxVobPfxControllerGetInitiallyRunning(vobPfxControllerPtr);
        }

        private static void SetVobAnimateData(IntPtr vobAnimateDataPtr, PxVobAnimateData vobAnimateData)
        {
            SetVobData(vobAnimateDataPtr, vobAnimateData);

            vobAnimateData.startOn = pxVobAnimateGetStartOn(vobAnimateDataPtr);
        }

        private static void SetVobLensFlareData(IntPtr vobLensFlarePtr, PxVobLensFlareData vobLensFlare)
        {
            SetVobData(vobLensFlarePtr, vobLensFlare);

            vobLensFlare.fx = pxVobLensFlareGetFx(vobLensFlarePtr).MarshalAsString();
        }

        private static void SetVobLightData(IntPtr vobLightPtr, PxVobLightData vobLight)
        {
            SetVobData(vobLightPtr, vobLight);

            vobLight.lightPreset = new PxLightPresetData();

            SetLightPresetData(vobLightPtr, vobLight.lightPreset);
        }

        private static void SetVobMessageFilterData(IntPtr vobMessageFilterPtr, PxVobMessageFilterData vobMessageFilter)
        {
            SetVobData(vobMessageFilterPtr, vobMessageFilter);

            vobMessageFilter.target = pxVobMessageFilterGetTarget(vobMessageFilterPtr).MarshalAsString();
            vobMessageFilter.onTrigger = pxVobMessageFilterGetOnTrigger(vobMessageFilterPtr);
            vobMessageFilter.onUntrigger = pxVobMessageFilterGetOnUntrigger(vobMessageFilterPtr);
        }

        private static void SetVobCodeMasterData(IntPtr vobCodeMasterPtr, PxVobCodeMasterData vobCodeMaster)
        {
            var slaveCount = pxVobCodeMasterGetSlavesCount(vobCodeMasterPtr);
            var slaves = new string[slaveCount];
            for (uint i = 0; i < slaveCount; i++)
                slaves[i] = pxVobCodeMasterGetSlaves(vobCodeMasterPtr, i).MarshalAsString();

            SetVobData(vobCodeMasterPtr, vobCodeMaster);

            vobCodeMaster.target = pxVobCodeMasterGetTarget(vobCodeMasterPtr).MarshalAsString();
            vobCodeMaster.ordered = pxVobCodeMasterGetOrdered(vobCodeMasterPtr);
            vobCodeMaster.firstFalseIsFailure = pxVobCodeMasterGetFirstFalseIsFailure(vobCodeMasterPtr);
            vobCodeMaster.failureTarget = pxVobCodeMasterGetFailureTarget(vobCodeMasterPtr).MarshalAsString();
            vobCodeMaster.untriggeredCancels = pxVobCodeMasterGetUntriggeredCancels(vobCodeMasterPtr);
            vobCodeMaster.slaves = slaves;
        }

        private static void SetVobTriggerWorldStart(IntPtr vobTriggerWorldStartPtr,
            PxVobTriggerWorldStartData vobTriggerWorldStart)
        {
            SetVobData(vobTriggerWorldStartPtr, vobTriggerWorldStart);

            vobTriggerWorldStart.target = pxVobTriggerWorldStartGetTarget(vobTriggerWorldStartPtr).MarshalAsString();
            vobTriggerWorldStart.fireOnce = pxVobTriggerWorldStartGetFireOnce(vobTriggerWorldStartPtr);
            vobTriggerWorldStart.sHasFired = pxVobTriggerWorldStartGetSHasFired(vobTriggerWorldStartPtr);
        }

        private static void SetVobTouchDamageData(IntPtr vobTouchDamagePtr, PxVobTouchDamageData vobTouchDamage)
        {
            SetVobData(vobTouchDamagePtr, vobTouchDamage);

            vobTouchDamage.damage = pxVobTouchDamageGetDamage(vobTouchDamagePtr);
            vobTouchDamage.barrier = pxVobTouchDamageGetBarrier(vobTouchDamagePtr);
            vobTouchDamage.blunt = pxVobTouchDamageGetBlunt(vobTouchDamagePtr);
            vobTouchDamage.edge = pxVobTouchDamageGetEdge(vobTouchDamagePtr);
            vobTouchDamage.fire = pxVobTouchDamageGetFire(vobTouchDamagePtr);
            vobTouchDamage.fly = pxVobTouchDamageGetFly(vobTouchDamagePtr);
            vobTouchDamage.magic = pxVobTouchDamageGetMagic(vobTouchDamagePtr);
            vobTouchDamage.point = pxVobTouchDamageGetPoint(vobTouchDamagePtr);
            vobTouchDamage.fall = pxVobTouchDamageGetFall(vobTouchDamagePtr);
            vobTouchDamage.repeatDelaySec = pxVobTouchDamageGetRepearDelaySec(vobTouchDamagePtr);
            vobTouchDamage.volumeScale = pxVobTouchDamageGetVolumeScale(vobTouchDamagePtr);
            vobTouchDamage.collision = pxVobTouchDamageGetCollision(vobTouchDamagePtr);
        }

        private static void SetVobTriggerUntouch(IntPtr vobTriggerUntouchPtr, PxVobTriggerUntouchData vobTriggerUntouch)
        {
            SetVobData(vobTriggerUntouchPtr, vobTriggerUntouch);
            vobTriggerUntouch.target = pxVobTriggerUntouchGetTarget(vobTriggerUntouchPtr).MarshalAsString();
        }

        private static void SetVobEarthquakeData(IntPtr vobEarthquakePtr, PxVobEarthQuakeData vobEarthquake)
        {
            SetVobData(vobEarthquakePtr, vobEarthquake);

            vobEarthquake.radius = pxVobEarthquakeGetRadius(vobEarthquakePtr);
            vobEarthquake.duration = pxVobEarthquakeGetDuration(vobEarthquakePtr);
            vobEarthquake.amplitude = pxVobEarthquakeGetAmplitude(vobEarthquakePtr);
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


        private static void SetVobTriggerChangeLevelData(IntPtr vobTriggerChangeLevelPtr,
            PxVobTriggerChangeLevelData vobTriggerChangeLevel)
        {
            SetVobTriggerData(vobTriggerChangeLevelPtr, vobTriggerChangeLevel);

            vobTriggerChangeLevel.levelName =
                pxVobTriggerChangeLevelGetLevelName(vobTriggerChangeLevelPtr).MarshalAsString();
            vobTriggerChangeLevel.startVob =
                pxVobTriggerChangeLevelGetStartVob(vobTriggerChangeLevelPtr).MarshalAsString();
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
                pxVobTriggerMoverGetKeyframe(vobTriggerMoverPtr, i, out var position, out var rotation);
                array[i] = new PxAnimationSampleData()
                {
                    position = position,
                    rotation = rotation
                };
            }

            vobTriggerMover.keyframes = array;

            vobTriggerMover.sfxOpenStart = pxVobTriggerMoverGetSfxOpenStart(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxOpenEnd = pxVobTriggerMoverGetSfxOpenEnd(vobTriggerMoverPtr).MarshalAsString();
            vobTriggerMover.sfxTransitioning =
                pxVobTriggerMoverGetSfxTransitioning(vobTriggerMoverPtr).MarshalAsString();
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

            pxWorldVobGetZoneMusic(vobZoneMusicPtr, out var enabled, out var priority, out var ellipsoid,
                out var reverb, out var volume, out var loop);
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

            pxWorldVobGetZoneFarPlane(vobZoneFarPlanePtr, out var vobFarPlaneZ, out var innerRangePercentage);

            vobZoneFarPlane.vobFarPlaneZ = vobFarPlaneZ;
            vobZoneFarPlane.innerRangePercentage = innerRangePercentage;
        }

        private static void SetVobZoneFogData(IntPtr vobZoneFogPtr, PxVobZoneFogData vobZoneFog)
        {
            SetVobData(vobZoneFogPtr, vobZoneFog);

            pxWorldVobGetZoneFog(
                vobZoneFogPtr,
                out var rangeCenter,
                out var innerRangePercentage,
                out var color,
                out var fadeOutSky,
                out var overrideColor);

            vobZoneFog.rangeCenter = rangeCenter;
            vobZoneFog.innerRangePercentage = innerRangePercentage;
            vobZoneFog.color = color;
            vobZoneFog.fadeOutSky = fadeOutSky;
            vobZoneFog.overrideColor = overrideColor;
        }

        private static void SetLightPresetData(IntPtr lightPresetPtr, PxLightPresetData lightPreset)
        {
            uint rangeAnimationScaleCount = 0;
            float[] rangeAnimationScale = new float[rangeAnimationScaleCount];
            for (var i = 0u; i < rangeAnimationScaleCount; i++)
            {
                rangeAnimationScale[i] = pxLightPresetGetRangeAnimationScale(lightPresetPtr, i);
            }

            uint colorAnimationListCount = 0;
            Vector4Byte[] colorAnimationList = new Vector4Byte[colorAnimationListCount];
            for (var i = 0u; i < colorAnimationListCount; i++)
            {
                colorAnimationList[i] = pxLightPresetGetColorAnimationList(lightPresetPtr, i);
            }

            var preset = pxLightPresetGetPreset(lightPresetPtr);
            var lensflareFx = pxLightPresetGetLensFlareFx(lightPresetPtr);
            lightPreset.preset = preset != IntPtr.Zero ? preset.MarshalAsString() : "";
            // lightPreset.preset = pxLightPresetGetPreset(lightPresetPtr).MarshalAsString();
            lightPreset.lightType = (PxLightMode)pxLightPresetGetLightType(lightPresetPtr);
            lightPreset.range = pxLightPresetGetRange(lightPresetPtr);
            lightPreset.color = pxLightPresetGetColor(lightPresetPtr);
            lightPreset.coneAngle = pxLightPresetGetConeAngle(lightPresetPtr);
            lightPreset.isStatic = pxLightPresetGetIsStatic(lightPresetPtr);
            lightPreset.quality = (PxLightQuality)pxLightPresetGetQuality(lightPresetPtr);
            // lightPreset.lensflareFx = lensflareFx != IntPtr.Zero ? lensflareFx.MarshalAsString() : "";
            lightPreset.on = pxLightPresetGetOn(lightPresetPtr);
            lightPreset.rangeAnimationScale = rangeAnimationScale;
            lightPreset.rangeAnimationFps = pxLightPresetGetRangeAnimationFps(lightPresetPtr);
            lightPreset.rangeAnimationSmooth = pxLightPresetGetRangeAnimationSmooth(lightPresetPtr);
            lightPreset.colorAnimationList = colorAnimationList;
            lightPreset.colorAnimationFps = pxLightPresetGetColorAnimationFps(lightPresetPtr);
            lightPreset.colorAnimationSmooth = pxLightPresetGetColorAnimationSmooth(lightPresetPtr);
            lightPreset.canMove = pxLightPresetGetCanMove(lightPresetPtr);
        }
    }
}