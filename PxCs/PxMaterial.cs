using PxCs.Data;
using PxCs.Extensions;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace PxCs
{
    public static class PxMaterial
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;


        [DllImport(DLLNAME)] public static extern IntPtr pxMatGetName(IntPtr mat);
        [DllImport(DLLNAME)] public static extern byte pxMatGetGroup(IntPtr mat);
        [DllImport(DLLNAME)] public static extern uint pxMatGetColor(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetSmoothAngle(IntPtr mat);

        [DllImport(DLLNAME)] public static extern IntPtr pxMatGetTexture(IntPtr mat);
        [DllImport(DLLNAME)] public static extern Vector2 pxMatGetTextureScale(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetTextureAnimFps(IntPtr mat);
        [DllImport(DLLNAME)] public static extern byte pxMatGetTextureAnimMapMode(IntPtr mat);
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
        [DllImport(DLLNAME)] public static extern bool pxMatGetForceOcculuder(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetEnvironmentMapping(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetEnvironmentMappingStrength(IntPtr mat);
        [DllImport(DLLNAME)] public static extern byte pxMatGetWaveMode(IntPtr mat);
        [DllImport(DLLNAME)] public static extern byte pxMatGetWaveSpeed(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetWaveMaxAmplitude(IntPtr mat);
        [DllImport(DLLNAME)] public static extern float pxMatGetWaveGridSize(IntPtr mat);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxMatGetIgnoreSun(IntPtr mat);
        [DllImport(DLLNAME)] public static extern byte pxMatGetAlphaFunc(IntPtr mat);
        [DllImport(DLLNAME)] public static extern Vector2 pxMatGetDefaultMapping(IntPtr mat);


        public static PxMaterialData GetMaterial(IntPtr mat)
        {
            return new PxMaterialData() {
                name = pxMatGetName(mat).MarshalAsString(),
                texture = pxMatGetTexture(mat).MarshalAsString(),
                color = pxMatGetColor(mat)
            };
        }
    }
}
