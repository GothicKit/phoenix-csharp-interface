using System;
using System.Runtime.InteropServices;
using PxCs.Data;
using PxCs.Extensions;

namespace PxCs.Interface
{
    public static class PxDaedalusScript
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;

        [DllImport(DLLNAME)] public static extern IntPtr pxScriptLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxScriptLoadFromVfs(IntPtr vfs, string name);
        [DllImport(DLLNAME)] public static extern void pxScriptDestroy(IntPtr scr);

        [DllImport(DLLNAME)] public static extern IntPtr pxScriptGetSymbolById(IntPtr scr, uint id);
        [DllImport(DLLNAME)] public static extern IntPtr pxScriptGetSymbolByAddress(IntPtr scr, uint address);
        [DllImport(DLLNAME)] public static extern IntPtr pxScriptGetSymbolByName(IntPtr scr, string name);

        [DllImport(DLLNAME)] public static extern byte pxScriptGetInstruction(IntPtr scr, uint ip, IntPtr info);

        [DllImport(DLLNAME)] public static extern uint pxScriptGetSymbolCount(IntPtr scr);
        
        
        public delegate bool PxVmEnumerateScriptInstancesCallback(IntPtr script, IntPtr symbol);
        [DllImport(DLLNAME)] public static extern void pxScriptEnumerateSymbols(IntPtr scr, PxVmEnumerateScriptInstancesCallback cb);

        [DllImport(DLLNAME)] public static extern uint pxScriptSymbolGetId(IntPtr sym);
        [DllImport(DLLNAME)] public static extern IntPtr pxScriptSymbolGetName(IntPtr sym);
        [DllImport(DLLNAME)] public static extern void pxScriptSymbolGetInfo(IntPtr sym, IntPtr info);

        [DllImport(DLLNAME)] public static extern int pxScriptSymbolGetInt(IntPtr sym, uint index);
        [DllImport(DLLNAME)] public static extern float pxScriptSymbolGetFloat(IntPtr sym, uint index);
        [DllImport(DLLNAME)] public static extern IntPtr pxScriptSymbolGetString(IntPtr sym, uint index);

        
        public static PxSymbolData? GetSymbol(IntPtr scr, uint index)
        {
            var symbolPtr = pxScriptGetSymbolById(scr, index);
            return GetSymbol(symbolPtr);
        }

        public static PxSymbolData? GetSymbol(IntPtr scr, string name)
        {
            var symbolPtr = pxScriptGetSymbolByName(scr, name);

            return GetSymbol(symbolPtr);
        }

        public static PxSymbolData? GetSymbol(IntPtr symbolPtr)
        {
            if (symbolPtr == IntPtr.Zero)
                return null;
            
            return new PxSymbolData()
            {
                name = pxScriptSymbolGetName(symbolPtr).MarshalAsString(),
                id = pxScriptSymbolGetId(symbolPtr)
            };
        }
    }
}