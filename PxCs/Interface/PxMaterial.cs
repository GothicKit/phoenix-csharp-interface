using System;
using System.Numerics;
using System.Runtime.InteropServices;
using PxCs.Data.Mesh;
using PxCs.Extensions;

namespace PxCs.Interface
{
    public static class PxMaterial
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        public enum PxMaterialGroup
        {
            PxMaterialGroup_Undefined = 0,
            PxMaterialGroup_Metal = 1,
            PxMaterialGroup_Stone = 2,
            PxMaterialGroup_Wood = 3,
            PxMaterialGroup_Earth = 4,
            PxMaterialGroup_Water = 5,
            PxMaterialGroup_Snow = 6,

            // The material group is explicitly not set. Added for [OpenGothic](https://github.com/Try/OpenGothic)
            // compatibility. It does not exist in real Gothic or Gothic 2 materials.
            PxMaterialGroup_None = 0xFF
        };
        
        public enum PxMaterialAnimationMappingMode
        {
            PxMaterialAnimMap_None = 0,
            PxMaterialAnimMap_Linear = 1
        };

        public enum PxMaterialWaveModeType
        {
            PxMaterialWaveMode_None = 0,
            PxMaterialWaveMode_AmbientGround = 1,
            PxMaterialWaveMode_Ground = 2,
            PxMaterialWaveMode_AmbientWall = 3,
            PxMaterialWaveMode_Wall = 4,
            PxMaterialWaveMode_Env = 5,
            PxMaterialWaveMode_AmbientWind = 6,
            PxMaterialWaveMode_Wind = 7
        }

        public enum PxMaterialWaveSpeedType
        {
            PxMaterialWaveSpeed_None = 0,
            PxMaterialWaveSpeed_Slow = 1,
            PxMaterialWaveSpeed_Normal = 2,
            PxMaterialWaveSpeed_Fast = 3
        }

        public enum PxMaterialAlphaFunction
        {
            PxMaterialAlpha_Default = 0,
            PxMaterialAlpha_None = 1,
            PxMaterialAlpha_Blend = 2,
            PxMaterialAlpha_Add = 3,
            PxMaterialAlpha_Sub = 4,
            PxMaterialAlpha_Mul = 5,
            PxMaterialAlpha_Mul2 = 6
        }

        [DllImport(DLLNAME)] public static extern IntPtr pxMatGetName(IntPtr mat);
        [DllImport(DLLNAME)] public static extern PxMaterialGroup pxMatGetGroup(IntPtr mat);
        [DllImport(DLLNAME)] public static extern uint pxMatGetColor(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetSmoothAngle(IntPtr mat);

        [DllImport(DLLNAME)] public static extern IntPtr pxMatGetTexture(IntPtr mat);
        [DllImport(DLLNAME)] public static extern Vector2 pxMatGetTextureScale(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetTextureAnimFps(IntPtr mat);
        [DllImport(DLLNAME)] public static extern PxMaterialAnimationMappingMode pxMatGetTextureAnimMapMode(IntPtr mat);
        [DllImport(DLLNAME)] public static extern Vector2 pxMatGetTextureAnimMapDir(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetDisableCollision(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetDisableLightmap(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetDontCollapse(IntPtr mat);

        [DllImport(DLLNAME)] public static extern IntPtr pxMatGetDetailObject(IntPtr mat);
        [DllImport(DLLNAME)] public static extern Vector2 pxMatGetDetailTextureScale(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetForceOccluder(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetEnvironmentMapping(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetEnvironmentMappingStrength(IntPtr mat);
        [DllImport(DLLNAME)] public static extern PxMaterialWaveModeType pxMatGetWaveMode(IntPtr mat);
        [DllImport(DLLNAME)] public static extern PxMaterialWaveSpeedType pxMatGetWaveSpeed(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetWaveMaxAmplitude(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetWaveGridSize(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetIgnoreSun(IntPtr mat);
        [DllImport(DLLNAME)] public static extern PxMaterialAlphaFunction pxMatGetAlphaFunc(IntPtr mat);
        [DllImport(DLLNAME)] public static extern Vector2 pxMatGetDefaultMapping(IntPtr mat);


        public static PxMaterialData GetMaterial(IntPtr mat)
        {
            return new PxMaterialData()
            {
                name = pxMatGetName(mat).MarshalAsString(),
                group = pxMatGetGroup(mat),
                color = pxMatGetColor(mat),
                smoothAngle = pxMatGetSmoothAngle(mat),
                texture = pxMatGetTexture(mat).MarshalAsString(),
                textureScale = pxMatGetTextureScale(mat),
                animFps = pxMatGetTextureAnimFps(mat),
                animMapMode = pxMatGetTextureAnimMapMode(mat),
                animMapDir = pxMatGetTextureAnimMapDir(mat),
                disableCollision = pxMatGetDisableCollision(mat),
                disableLightmap = pxMatGetDisableLightmap(mat),
                dontCollapse = pxMatGetDontCollapse(mat),
                detailObject = pxMatGetDetailObject(mat).MarshalAsString(),
                detailTextureScale = pxMatGetDetailTextureScale(mat),
                forceOccluder = pxMatGetForceOccluder(mat),
                environmentMapping = pxMatGetEnvironmentMapping(mat),
                environmentMappingStrength = pxMatGetEnvironmentMappingStrength(mat),
                waveMode = pxMatGetWaveMode(mat),
                waveSpeed = pxMatGetWaveSpeed(mat),
                waveMaxAmplitude = pxMatGetWaveMaxAmplitude(mat),
                waveGridSize = pxMatGetWaveGridSize(mat),
                ignoreSun = pxMatGetIgnoreSun(mat),
                alphaFunction = pxMatGetAlphaFunc(mat),
                defaultMapping = pxMatGetDefaultMapping(mat)
            };
        }
    }
}
