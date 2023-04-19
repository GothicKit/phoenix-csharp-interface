﻿using PxCs.Data.Vm;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace PxCs
{
    public static class PxVm
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;


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

        [DllImport(DLLNAME)] public static extern IntPtr pxVmStackPopString(IntPtr vm);
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
        // Hint: We need to send a nullptr as the vararg parameter to tell Phoenix "this method isn't sending you varargs".
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVmCallFunction(IntPtr vm, string functionName, IntPtr zero /*==IntPtr.Zero*/);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport(DLLNAME)] public static extern bool pxVmCallFunctionByIndex(IntPtr vm, uint index, IntPtr zero /*==IntPtr.Zero*/);

        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceAllocateByIndex(IntPtr vm, uint index, PxVmInstanceType type);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceAllocateByName(IntPtr vm, string name, PxVmInstanceType type);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceInitializeByIndex(IntPtr vm, uint index, PxVmInstanceType type, IntPtr existing);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceInitializeByName(IntPtr vm, string name, PxVmInstanceType type, IntPtr existing);

        // HINT: Won't work as it will print to std::cerr which isn't shared with the managed C# side.
        // [DllImport(DLLNAME)] public static extern void pxVmPrintStackTrace(IntPtr vm);

        [DllImport(DLLNAME)] public static extern uint pxVmInstanceNpcGetNameLength(IntPtr instance);

        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceNpcGetName(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceNpcGetRoutine(IntPtr instance);


        public static bool CallFunction(IntPtr vmPtr, string methodName, params object[] parameters)
        {
            StackPushParameters(vmPtr, parameters);
            return pxVmCallFunction(vmPtr, methodName, IntPtr.Zero);
        }

        public static bool CallFunction(IntPtr vmPtr, uint index, params object[] parameters)
        {
            StackPushParameters(vmPtr, parameters);
            return pxVmCallFunctionByIndex(vmPtr, index, IntPtr.Zero);
        }

        public static bool CallFunction(IntPtr vmPtr, string methodName, IntPtr self, params object[] parameters)
        {
            var prevSelf = pxVmSetGlobalSelf(vmPtr, self);

            StackPushParameters(vmPtr, parameters);
            var success = pxVmCallFunction(vmPtr, methodName, IntPtr.Zero);

            pxVmSetGlobalSelf(vmPtr, prevSelf);

            return success;
        }

        public static bool CallFunction(IntPtr vmPtr, uint index, IntPtr self, params object[] parameters)
        {
            var prevSelf = pxVmSetGlobalSelf(vmPtr, self);

            StackPushParameters(vmPtr, parameters);
            var success = pxVmCallFunctionByIndex(vmPtr, index, IntPtr.Zero);

            pxVmSetGlobalSelf(vmPtr, prevSelf);

            return success;
        }

        public static string VmStackPopString(IntPtr vmPtr)
        {
            var strPtr = pxVmStackPopString(vmPtr);
            return PxPhoenix.MarshalString(strPtr);
        }

        public static PxVmNpcData InitializeNpc(IntPtr vmPtr, string name)
        {
            var npcPtr = pxVmInstanceInitializeByName(vmPtr, name, PxVmInstanceType.PxVmInstanceTypeNpc, IntPtr.Zero);

            return GetNpcByInstancePtr(npcPtr);
        }

        public static PxVmNpcData InitializeNpc(IntPtr vmPtr, uint index)
        {
            var npcPtr = pxVmInstanceInitializeByIndex(vmPtr, index, PxVmInstanceType.PxVmInstanceTypeNpc, IntPtr.Zero);

            return GetNpcByInstancePtr(npcPtr);
        }

        private static PxVmNpcData GetNpcByInstancePtr(IntPtr instancePtr)
        {
            var nameCount = pxVmInstanceNpcGetNameLength(instancePtr);
            string[] names = new string[nameCount];
            for (var i = 0u; i < nameCount; i++)
                names[i] = PxPhoenix.MarshalString(pxVmInstanceNpcGetName(instancePtr, i));

            return new PxVmNpcData()
            {
                npcPtr = instancePtr,
                names = names,
                routine = pxVmInstanceNpcGetRoutine(instancePtr)
            };
        }


        private static void StackPushParameters(IntPtr vmPtr, params object[] parameters)
        {
            // As we're working with a stack, we need to add parameters in reverse order.
            foreach (var param in parameters.Reverse())
            {
                switch (param)
                {
                    case string _:
                        pxVmStackPushString(vmPtr, (string)param);
                        break;
                    case float _:
                        pxVmStackPushFloat(vmPtr, (float)param);
                        break;
                    case int _:
                        pxVmStackPushInt(vmPtr, (int)param);
                        break;
                    case IntPtr _:
                        pxVmStackPushInstance(vmPtr, (IntPtr)param);
                        break;
                    default:
                        throw new ArgumentException($"VM doesn't support argument of type {param.GetType()}");
                }
            }
        }
    }
}
