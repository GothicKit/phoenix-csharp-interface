using System;
using System.Runtime.InteropServices;

namespace Phoenix.Csharp.Interface
{
    public static class Vm
    {
        private const string DLLNAME = Phoenix.DLLNAME;


        public enum PxVmInstanceType
        {
            PxVmInstanceTypeNpc = 1,
        };


        public delegate void PxVmExternalDefaultCallback(IntPtr vmPtr, string missingCallbackName);
        public delegate void PxVmExternalCallback(IntPtr vmPtr);

        [DllImport(DLLNAME)] public static extern IntPtr pxVmLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmLoadFromVdf(IntPtr vdf, string name);
        [DllImport(DLLNAME)] public static extern void pxVmDestroy(IntPtr vm);

        [DllImport(DLLNAME)] public static extern IntPtr pxVmStackPopInstance(IntPtr vm);
        [DllImport(DLLNAME)] public static extern string pxVmStackPopString(IntPtr vm);
        [DllImport(DLLNAME)] public static extern float pxVmStackPopFloat(IntPtr vm);
        [DllImport(DLLNAME)] public static extern int pxVmStackPopInt(IntPtr vm);

        [DllImport(DLLNAME)] public static extern void pxVmStackPushInstance(IntPtr vm, IntPtr instance);
        [DllImport(DLLNAME)] public static extern void pxVmStackPushString(IntPtr vm, string str);
        [DllImport(DLLNAME)] public static extern void pxVmStackPushFloat(IntPtr vm, float f);
        [DllImport(DLLNAME)] public static extern void pxVmStackPushInt(IntPtr vm, int i);

        [DllImport(DLLNAME)] public static extern void pxVmRegisterExternal(IntPtr vm, string name, PxVmExternalCallback cb);
        [DllImport(DLLNAME)] public static extern void pxVmRegisterExternalDefault(IntPtr vm, PxVmExternalDefaultCallback cb);

        [DllImport(DLLNAME)] public static extern IntPtr pxVmGetGlobalSelf(IntPtr vm);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmGetGlobalOther(IntPtr vm);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmGetGlobalVictim(IntPtr vm);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmGetGlobalHero(IntPtr vm);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmGetGlobalItem(IntPtr vm);

        [DllImport(DLLNAME)] public static extern IntPtr pxVmSetGlobalSelf(IntPtr vm, IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmSetGlobalOther(IntPtr vm, IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmSetGlobalVictim(IntPtr vm, IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmSetGlobalHero(IntPtr vm, IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmSetGlobalItem(IntPtr vm, IntPtr instance);

        // Hint: Varargs aren't possible from C# -> C. We therefore need to push stack entries manually before calling the method (e.g. pxVmPushString())
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVmCallFunction(IntPtr vm, string functionName /*, ...*/);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVmCallFunctionByIndex(IntPtr vm, uint index /*, ...*/ );

        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceAllocate(IntPtr vm, string name, PxVmInstanceType type);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceInitialize(IntPtr vm, string name, PxVmInstanceType type, IntPtr existing);

        [DllImport(DLLNAME)] public static extern uint pxVmInstanceNpcGetNameLength(IntPtr instance);
        // FIXME - will it work to get a string back from C? At least it's on heap there...
        [DllImport(DLLNAME)] public static extern string pxVmInstanceNpcGetName(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceNpcGetRoutine(IntPtr instance);
    }
}
