using PxCs.Data.Vm;
using PxCs.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace PxCs.Interface
{
	public static class PxVm
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;


        public enum PxVmInstanceType
        {
            PxVmInstanceTypeNpc = 1,
            PxVmInstanceTypeItem = 2,
			PxVmInstanceTypeSfx = 3,
            PxVmInstanceTypeMusic = 4
		};


        public delegate void PxVmExternalDefaultCallback(IntPtr vmPtr, string missingCallbackName);
        public delegate void PxVmExternalCallback(IntPtr vmPtr);

        [DllImport(DLLNAME)] public static extern IntPtr pxVmLoad(IntPtr buffer);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmLoadFromVfs(IntPtr vfs, string name);
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

        [DllImport(DLLNAME)] public static extern IntPtr pxVmGetSymbolByIndex(IntPtr vm, uint index);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmGetSymbolByName(IntPtr vm, string name);
        [DllImport(DLLNAME)] public static extern uint pxVmSymbolGetId(IntPtr symbol);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmSymbolGetName(IntPtr symbol);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceAllocateByIndex(IntPtr vm, uint index, PxVmInstanceType type);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceAllocateByName(IntPtr vm, string name, PxVmInstanceType type);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceInitializeByIndex(IntPtr vm, uint index, PxVmInstanceType type, IntPtr existing);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceInitializeByName(IntPtr vm, string name, PxVmInstanceType type, IntPtr existing);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceGetSymbolIndex(IntPtr instance);

        // HINT: Won't work as it will print to std::cerr which isn't shared with the managed C# side.
        // [DllImport(DLLNAME)] public static extern void pxVmPrintStackTrace(IntPtr vm);

        // C_Npc
        public delegate void PxVmEnumerateInstancesCallback(string itemName);
        [DllImport(DLLNAME)] public static extern void pxVmEnumerateInstancesByClassName(IntPtr vm, string name, PxVmEnumerateInstancesCallback cb);

        [DllImport(DLLNAME)] public static extern int pxVmInstanceNpcGetId(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceNpcGetNameLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceNpcGetName(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceNpcGetRoutine(IntPtr instance);

		// C_Item
		[DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetName(IntPtr instance);
		[DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetDescription(IntPtr instance);
		[DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetVisual(IntPtr instance);

		// C_Sfx
		[DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceSfxGetFile(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceSfxGetPitchOff(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceSfxGetPitchVar(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceSfxGetVol(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceSfxGetLoop(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceSfxGetLoopStartOffset(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceSfxGetLoopEndOffset(IntPtr instance);
		[DllImport(DLLNAME)] public static extern float pxVmInstanceSfxGetReverbLevel(IntPtr instance);
		[DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceSfxGetPfxName(IntPtr instance);

		// C_Music_theme
		[DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMusicGetFile(IntPtr instance);
		[DllImport(DLLNAME)] public static extern float pxVmInstanceMusicGetVol(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceMusicGetLoop(IntPtr instance);
		[DllImport(DLLNAME)] public static extern float pxVmInstanceMusicGetReverbMix(IntPtr instance);
		[DllImport(DLLNAME)] public static extern float pxVmInstanceMusicGetReverbTime(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceMusicGetTransitionType(IntPtr instance);
		[DllImport(DLLNAME)] public static extern int pxVmInstanceMusicGetTransitionSubType(IntPtr instance);

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
            return strPtr.MarshalAsString();
        }

        public static PxVmNpcData? InitializeNpc(IntPtr vmPtr, string name)
        {
            var npcPtr = pxVmInstanceInitializeByName(vmPtr, name, PxVmInstanceType.PxVmInstanceTypeNpc, IntPtr.Zero);

			if (npcPtr == IntPtr.Zero)
				return null;

			return GetNpcByInstancePtr(npcPtr);
        }

        public static PxVmNpcData? InitializeNpc(IntPtr vmPtr, uint index)
        {
            var npcPtr = pxVmInstanceInitializeByIndex(vmPtr, index, PxVmInstanceType.PxVmInstanceTypeNpc, IntPtr.Zero);

			if (npcPtr == IntPtr.Zero)
				return null;

			return GetNpcByInstancePtr(npcPtr);
        }

		public static PxVmItemData? InitializeItem(IntPtr vmPtr, string name)
		{
			var itemPtr = pxVmInstanceInitializeByName(vmPtr, name, PxVmInstanceType.PxVmInstanceTypeItem, IntPtr.Zero);

            if (itemPtr == IntPtr.Zero)
                return null;

			return GetItemByInstancePtr(itemPtr);
		}
		
		public static PxVmItemData? InitializeItem(IntPtr vmPtr, uint index)
		{
			var itemPtr = pxVmInstanceInitializeByIndex(vmPtr, index, PxVmInstanceType.PxVmInstanceTypeItem, IntPtr.Zero);

			if (itemPtr == IntPtr.Zero)
				return null;

			return GetItemByInstancePtr(itemPtr);
		}

		public static PxVmSfxData? InitializeSfx(IntPtr vmPtr, string name)
		{
			var sfxPtr = pxVmInstanceInitializeByName(vmPtr, name, PxVmInstanceType.PxVmInstanceTypeSfx, IntPtr.Zero);

			if (sfxPtr == IntPtr.Zero)
				return null;

			return GetSfxByInstancePtr(sfxPtr);
		}
        
		public static PxVmMusicData? InitializeMusic(IntPtr vmPtr, string name)
		{
			var sfxPtr = pxVmInstanceInitializeByName(vmPtr, name, PxVmInstanceType.PxVmInstanceTypeMusic, IntPtr.Zero);

			if (sfxPtr == IntPtr.Zero)
				return null;

			return GetMusicByInstancePtr(sfxPtr);
		}

		public static PxVmSymbolData? GetSymbol(IntPtr vm, uint index)
		{
			var symbolPtr = pxVmGetSymbolByIndex(vm, index);

			return GetSymbolData(symbolPtr);
		}
		
		public static PxVmSymbolData? GetSymbol(IntPtr vm, string name)
		{
			var symbolPtr = pxVmGetSymbolByName(vm, name);

			return GetSymbolData(symbolPtr);
		}

		private static PxVmSymbolData? GetSymbolData(IntPtr symbolPtr)
		{
			if (symbolPtr == IntPtr.Zero)
				return null;

			return new PxVmSymbolData()
			{
				id = pxVmSymbolGetId(symbolPtr),
				name = pxVmSymbolGetName(symbolPtr).MarshalAsString()
			};
		}
		
        public static string[] GetInstancesByClassName(IntPtr vmPtr, string name)
        {
            var names = new List<string>();

            PxVmEnumerateInstancesCallback callback = (string name) =>
			{
				names.Add(name);
            };

            pxVmEnumerateInstancesByClassName(vmPtr, name, callback);

            return names.ToArray();
		}

        private static PxVmNpcData GetNpcByInstancePtr(IntPtr instancePtr)
        {
            var npc = new PxVmNpcData();
            AddInstanceData(npc, instancePtr);

            var nameCount = pxVmInstanceNpcGetNameLength(instancePtr);
            string[] names = new string[nameCount];
            for (var i = 0u; i < nameCount; i++)
                names[i] = pxVmInstanceNpcGetName(instancePtr, i).MarshalAsString();

            npc.id = pxVmInstanceNpcGetId(instancePtr);
			npc.symbolIndex = pxVmInstanceGetSymbolIndex(instancePtr);
			npc.names = names;
			npc.routine = pxVmInstanceNpcGetRoutine(instancePtr);

            return npc;
		}

		private static PxVmItemData GetItemByInstancePtr(IntPtr instancePtr)
		{
            var item = new PxVmItemData();
            AddInstanceData(item, instancePtr);

			item.name = pxVmInstanceItemGetName(instancePtr).MarshalAsString();
			item.description = pxVmInstanceItemGetDescription(instancePtr).MarshalAsString();
			item.visual = pxVmInstanceItemGetVisual(instancePtr).MarshalAsString();

			return item;
		}

		private static PxVmSfxData GetSfxByInstancePtr(IntPtr instancePtr)
		{
			var sfx = new PxVmSfxData();
			AddInstanceData(sfx, instancePtr);

			sfx.file = pxVmInstanceSfxGetFile(instancePtr).MarshalAsString();
		    sfx.pitchOff = pxVmInstanceSfxGetPitchOff(instancePtr);
		    sfx.pitchVar = pxVmInstanceSfxGetPitchVar(instancePtr);
		    sfx.vol = pxVmInstanceSfxGetVol(instancePtr);
		    sfx.loop = pxVmInstanceSfxGetLoop(instancePtr);
		    sfx.loopStartOffset = pxVmInstanceSfxGetLoopStartOffset(instancePtr);
		    sfx.loopEndOffset = pxVmInstanceSfxGetLoopEndOffset(instancePtr);
		    sfx.reverbLevel = pxVmInstanceSfxGetReverbLevel(instancePtr);
		    sfx.pfxName = pxVmInstanceSfxGetPfxName(instancePtr).MarshalAsString();

			return sfx;
		}
        private static PxVmMusicData GetMusicByInstancePtr(IntPtr instancePtr)
        {
            var music = new PxVmMusicData();

            music.file = pxVmInstanceMusicGetFile(instancePtr).MarshalAsString();
            music.vol = pxVmInstanceMusicGetVol(instancePtr);
            music.loop = pxVmInstanceMusicGetLoop(instancePtr);
            music.reverbMix = pxVmInstanceMusicGetReverbMix(instancePtr);
            music.reverbTime = pxVmInstanceMusicGetReverbTime(instancePtr);
            music.transitionSubType = pxVmInstanceMusicGetTransitionSubType(instancePtr);
            music.transitionType = pxVmInstanceMusicGetTransitionType(instancePtr);

            return music;
        }

		private static void AddInstanceData(PxVmData instanceData, IntPtr instancePtr)
        {
            instanceData.instancePtr = instancePtr;
			instanceData.symbolIndex = pxVmInstanceGetSymbolIndex(instancePtr);
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
