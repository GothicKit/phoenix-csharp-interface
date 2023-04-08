using System;
using System.Runtime.InteropServices;

namespace Phoenix.Csharp.Interface
{
    public class Texture
    {
        private const string DLLNAME = Phoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxTexLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxTexLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxTexDestroy(IntPtr tex);

        [DllImport(DLLNAME)] public static extern void pxTexGetMeta(IntPtr tex, out uint format, out uint width, out uint height, out uint mipmapCount, out uint averageColor);
        /*[DllImport(DLLNAME)]*/ public static void /*extern uint8_t const**/ pxTexGetMipmap(IntPtr tex, uint level, out uint width, out uint height) { throw new NotImplementedException("FIXME returns an array (aka uint8_t const* pointers) - We need to check if this is possible..."); }
    }
}
