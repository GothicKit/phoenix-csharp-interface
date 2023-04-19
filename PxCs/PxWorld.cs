using PxCs.Data;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs
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

                array[i] = new PxWayPointData() {
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
    }
}