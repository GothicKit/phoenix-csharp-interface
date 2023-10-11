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
            PxVmInstanceTypeMusic = 4,
            PxVmInstanceTypeMenu = 5,
            PxVmInstanceTypeMenuItem = 6
        };

        [Flags]
        public enum PxVmItemFlags
        {
            // Item categories
            ITEM_KAT_NONE    = 1 <<  0, // misc
            ITEM_KAT_NF      = 1 <<  1, // melee weapons
            ITEM_KAT_FF      = 1 <<  2, // distant-combat weapons
            ITEM_KAT_MUN     = 1 <<  3, // munition (->MultiSlot)
            ITEM_KAT_ARMOR   = 1 <<  4, // armor and helmets
            ITEM_KAT_FOOD    = 1 <<  5, // food (->MultiSlot)
            ITEM_KAT_DOCS    = 1 <<  6, // documents
            ITEM_KAT_POTIONS = 1 <<  7, // potions
            ITEM_KAT_LIGHT   = 1 <<  8, // light sources
            ITEM_KAT_RUNE    = 1 <<  9, // runes and scrolls
            ITEM_KAT_MAGIC   = 1 << 31, // rings and amulets
            ITEM_KAT_KEYS    = ITEM_KAT_NONE,
            
            // Item flags
            ITEM_BURN    = 1 << 10, // can be burnt (i.e. torch)
            ITEM_MISSION = 1 << 12, // mission item
            ITEM_MULTI   = 1 << 21, // is multi
            ITEM_TORCH   = 1 << 28, // use like a torch
            ITEM_THROW   = 1 << 29, // item can be thrown

            // Item weapon flags
            ITEM_SWD      = 1 << 14, // use like sword
            ITEM_AXE      = 1 << 15, // use like axe
            ITEM_2HD_SWD  = 1 << 16, // use like two handed weapon
            ITEM_2HD_AXE  = 1 << 17, // use like two handed axe
            ITEM_BOW      = 1 << 19, // use like bow
            ITEM_CROSSBOW = 1 << 20, // use like crossbow
            ITEM_AMULET   = 1 << 22, // use like amulet
            ITEM_RING     = 1 << 11  // use like ring
        };

        [Flags]
        public enum PxVmCMenuItemFlags
        {
            Chromakeyed = 1 << 0,
            Transparent = 1 << 1,
            Selectable = 1 << 2,
            Movable = 1 << 3,
            Centered = 1 << 4,
            Disabled = 1 << 5,
            Fade = 1 << 6,
            Effects = 1 << 7,
            OnlyOutgame = 1 << 8,
            OnlyIngame = 1 << 9,
            PerfOption = 1 << 10,
            Multiline = 1 << 11,
            NeedsApply = 1 << 12,
            NeedsRestart = 1 << 13,
            ExtendedMenu = 1 << 14,
        }

        public enum PxVmCMenuItemType
        {
            PxVmCMenuItemTypeUnknown = 0,
            PxVmCMenuItemTypeText = 1,
            PxVmCMenuItemTypeSlider = 2,
            PxVmCMenuItemTypeInput = 3,
            PxVmCMenuItemTypeCursor = 4,
            PxVmCMenuItemTypeChoicebox = 5,
            PxVmCMenuItemTypeButton = 6,
            PxVmCMenuItemTypeListbox = 7,
        }

        public enum PxVmCMenuItemSelectEvent
        {
            PxVmCMenuItemSelectEventExecute = 1,
            PxVmCMenuItemSelectEventChanged = 2,
            PxVmCMenuItemSelectEventLeave = 3,
            PxVmCMenuItemSelectEventTimer = 4,
            PxVmCMenuItemSelectEventClose = 5,
            PxVmCMenuItemSelectEventInit = 6,
            PxVmCMenuItemSelectEventSelectPrevious = 7,
            PxVmCMenuItemSelectEventSelectNext = 8,
        }

        public enum PxVmCMenuItemSelectAction
        {
            PxVmCMenuItemSelectActionUnknown = 0,
            PxVmCMenuItemSelectActionBack = 1,
            PxVmCMenuItemSelectActionStartMenu = 2,
            PxVmCMenuItemSelectActionStartItem = 3,
            PxVmCMenuItemSelectActionClose = 4,
            PxVmCMenuItemSelectActionConCommands = 5,
            PxVmCMenuItemSelectActionPlaySound = 6,
            PxVmCMenuItemSelectActionExecuteCommands = 7,
        }

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
        [DllImport(DLLNAME)] public static extern bool pxVmCallFunction(IntPtr vm, string functionName, IntPtr zero /*==IntPtr.Zero*/);
        [DllImport(DLLNAME)] public static extern bool pxVmCallFunctionByIndex(IntPtr vm, uint index, IntPtr zero /*==IntPtr.Zero*/);

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
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetId(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetName(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetNameId(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetHp(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetHpMax(IntPtr instance);
        [DllImport(DLLNAME)] public static extern PxVmItemFlags pxVmInstanceItemGetMainFlag(IntPtr instance);
        [DllImport(DLLNAME)] public static extern PxVmItemFlags pxVmInstanceItemGetFlags(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetWeight(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetValue(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetDamageType(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetDamageTotal(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetDamageLength();
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetDamage(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetWear(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetProtectionLength();
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetProtection(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetNutrition(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetCondAtrLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetCondAtr(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetCondValueLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetCondValue(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetChangeAtrLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetChangeAtr(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetChangeValueLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetChangeValue(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetMagic(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetOnEquip(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetOnUnequip(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetOnStateLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetOnState(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetOwner(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetOwnerGuild(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetDisguiseGuild(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetVisual(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetVisualChange(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetEffect(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetVisualSkin(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetSchemeName(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetMaterial(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetMunition(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetSpell(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetRange(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetMagCircle(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetDescription(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetTextLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceItemGetText(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceItemGetCountLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetCount(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetInvZbias(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetInvRotX(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetInvRotY(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetInvRotZ(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceItemGetInvAnimate(IntPtr instance);

        // C_Menu
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuGetBackPic(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuGetBackWorld(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetPosX(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetPosY(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetDimX(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetDimY(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetAlpha(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuGetMusicTheme(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetEventTimerMsec(IntPtr instance);
        [DllImport(DLLNAME)] public static extern byte pxVmInstanceMenuGetItemsLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuGetItems(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuGetFlags(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetDefaultOutgame(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuGetDefaultIngame(IntPtr instance);

        // C_MENU_ITEM
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetFontname(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetTextLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetText(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetBackpic(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetAlphamode(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetAlpha(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetType(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetOnSelActionLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetOnSelAction(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetOnSelActionSLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetOnSelActionS(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetOnChgSetOption(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetOnChgSetOptionSection(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetOnEventActionLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetOnEventAction(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetPosX(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetPosY(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetDimX(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetDimY(IntPtr instance);
        [DllImport(DLLNAME)] public static extern float pxVmInstanceMenuItemGetSizeStartScale(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetFlags(IntPtr instance);
        [DllImport(DLLNAME)] public static extern float pxVmInstanceMenuItemGetOpenDelayTime(IntPtr instance);
        [DllImport(DLLNAME)] public static extern float pxVmInstanceMenuItemGetOpenDuration(IntPtr instance);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetUserFloatLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern float pxVmInstanceMenuItemGetUserFloat(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern uint pxVmInstanceMenuItemGetUserStringLength(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetUserString(IntPtr instance, uint i);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetFrameSizex(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetFrameSizey(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetHideIfOptionSectionSet(IntPtr instance);
        [DllImport(DLLNAME)] public static extern IntPtr pxVmInstanceMenuItemGetHideIfOptionSet(IntPtr instance);
        [DllImport(DLLNAME)] public static extern int pxVmInstanceMenuItemGetHideOnValue(IntPtr instance);

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

        public static PxVmMenuData? InitializeMenu(IntPtr vmPtr, string name)
        {
            var menuPtr = pxVmInstanceInitializeByName(vmPtr, name, PxVmInstanceType.PxVmInstanceTypeMenu, IntPtr.Zero);

            if (menuPtr == IntPtr.Zero)
            {
                return null;
            }
            return GetMenuByInstancePtr(menuPtr);
        }

        public static PxVmMenuData? InitializeMenu(IntPtr vmPtr, uint index)
        {
            var menuPtr = pxVmInstanceInitializeByIndex(vmPtr, index, PxVmInstanceType.PxVmInstanceTypeMenu, IntPtr.Zero);

            if (menuPtr == IntPtr.Zero)
            {
                return null;
            }
            return GetMenuByInstancePtr(menuPtr);
        }

        public static PxVmMenuItemData? InitializeMenuItem(IntPtr vmPtr, string name)
        {
            var menuItem = pxVmInstanceInitializeByName(vmPtr, name, PxVmInstanceType.PxVmInstanceTypeMenuItem, IntPtr.Zero);

            if (menuItem == IntPtr.Zero)
            {
                return null;
            }
            return GetMenuItemByInstancePtr(menuItem);
        }

        public static PxVmMenuItemData? InitializeMenuItem(IntPtr vmPtr, uint index)
        {
            var menuItemPtr = pxVmInstanceInitializeByIndex(vmPtr, index, PxVmInstanceType.PxVmInstanceTypeMenuItem, IntPtr.Zero);

            if (menuItemPtr == IntPtr.Zero)
            {
                return null;
            }
            return GetMenuItemByInstancePtr(menuItemPtr);
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

            var damageCount = pxVmInstanceItemGetDamageLength();
            int[] damage = new int[damageCount];
            for (var i = 0u; i < damageCount; i++)
                damage[i] = pxVmInstanceItemGetDamage(instancePtr, i);

            var protectionCount = pxVmInstanceItemGetProtectionLength();
            int[] protection = new int[protectionCount];
            for (var i = 0u; i < protectionCount; i++)
                protection[i] = pxVmInstanceItemGetProtection(instancePtr, i);

            var conditionAtrCount = pxVmInstanceItemGetCondAtrLength(instancePtr);
            int[] condAtr = new int[conditionAtrCount];
            for (var i = 0u; i < conditionAtrCount; i++)
                condAtr[i] = pxVmInstanceItemGetCondAtr(instancePtr, i);

            var conditionValueCount = pxVmInstanceItemGetCondValueLength(instancePtr);
            int[] condValue = new int[conditionValueCount];
            for (var i = 0u; i < conditionValueCount; i++)
                condValue[i] = pxVmInstanceItemGetCondValue(instancePtr, i);

            var changeAtrCount = pxVmInstanceItemGetChangeAtrLength(instancePtr);
            int[] changeAtr = new int[changeAtrCount];
            for (var i = 0u; i < changeAtrCount; i++)
                changeAtr[i] = pxVmInstanceItemGetChangeAtr(instancePtr, i);

            var changeValueCount = pxVmInstanceItemGetChangeValueLength(instancePtr);
            int[] changeValue = new int[changeValueCount];
            for (var i = 0u; i < changeValueCount; i++)
                changeValue[i] = pxVmInstanceItemGetChangeValue(instancePtr, i);

            var onStateCount = pxVmInstanceItemGetOnStateLength(instancePtr);
            int[] onState = new int[onStateCount];
            for (var i = 0u; i < onStateCount; i++)
                onState[i] = pxVmInstanceItemGetOnState(instancePtr, i);

            var textCount = pxVmInstanceItemGetTextLength(instancePtr);
            string[] text = new string[textCount];
            for (var i = 0u; i < textCount; i++)
                text[i] = pxVmInstanceItemGetText(instancePtr, i).MarshalAsString();

            var countLength = pxVmInstanceItemGetCountLength(instancePtr);
            int[] count = new int[countLength];
            for (var i = 0u; i < countLength; i++)
                count[i] = pxVmInstanceItemGetCount(instancePtr, i);


            item.name = pxVmInstanceItemGetName(instancePtr).MarshalAsString();
            item.description = pxVmInstanceItemGetDescription(instancePtr).MarshalAsString();
            item.visual = pxVmInstanceItemGetVisual(instancePtr).MarshalAsString();
            item.visualChange = pxVmInstanceItemGetVisualChange(instancePtr).MarshalAsString();
            item.id = pxVmInstanceItemGetId(instancePtr);
            item.name = pxVmInstanceItemGetName(instancePtr).MarshalAsString();
            item.nameId = pxVmInstanceItemGetNameId(instancePtr).MarshalAsString();
            item.hp = pxVmInstanceItemGetHp(instancePtr);
            item.hpMax = pxVmInstanceItemGetHpMax(instancePtr);
            item.mainFlag = pxVmInstanceItemGetMainFlag(instancePtr);
            item.flags = pxVmInstanceItemGetFlags(instancePtr);
            item.weight = pxVmInstanceItemGetWeight(instancePtr);
            item.value = pxVmInstanceItemGetValue(instancePtr);
            item.damageType = pxVmInstanceItemGetDamageType(instancePtr);
            item.damageTotal = pxVmInstanceItemGetDamageTotal(instancePtr);
            item.damage = damage;
            item.wear = pxVmInstanceItemGetWear(instancePtr);
            item.protection = protection;
            item.nutrition = pxVmInstanceItemGetNutrition(instancePtr);
            item.condAtr = condAtr;
            item.condValue = condValue;
            item.changeAtr = changeAtr;
            item.changeValue = changeValue;
            item.magic = pxVmInstanceItemGetMagic(instancePtr);
            item.onEquip = pxVmInstanceItemGetOnEquip(instancePtr);
            item.onUnequip = pxVmInstanceItemGetOnUnequip(instancePtr);
            item.onState = onState;
            item.owner = pxVmInstanceItemGetOwner(instancePtr);
            item.ownerGuild = pxVmInstanceItemGetOwnerGuild(instancePtr);
            item.disguiseGuild = pxVmInstanceItemGetDisguiseGuild(instancePtr);
            item.visual = pxVmInstanceItemGetVisual(instancePtr).MarshalAsString();
            item.visualChange = pxVmInstanceItemGetVisualChange(instancePtr).MarshalAsString();
            item.effect = pxVmInstanceItemGetEffect(instancePtr).MarshalAsString();
            item.visualSkin = pxVmInstanceItemGetVisualSkin(instancePtr);
            item.schemeName = pxVmInstanceItemGetSchemeName(instancePtr).MarshalAsString();
            item.material = pxVmInstanceItemGetMaterial(instancePtr);
            item.munition = pxVmInstanceItemGetMunition(instancePtr);
            item.spell = pxVmInstanceItemGetSpell(instancePtr);
            item.range = pxVmInstanceItemGetRange(instancePtr);
            item.magCircle = pxVmInstanceItemGetMagCircle(instancePtr);
            item.description = pxVmInstanceItemGetDescription(instancePtr).MarshalAsString();
            item.text = text;
            item.count = count;
            item.invZbias = pxVmInstanceItemGetInvZbias(instancePtr);
            item.invRotX = pxVmInstanceItemGetInvRotX(instancePtr);
            item.invRotY = pxVmInstanceItemGetInvRotY(instancePtr);
            item.invRotZ = pxVmInstanceItemGetInvRotZ(instancePtr);
            item.invAnimate = pxVmInstanceItemGetInvAnimate(instancePtr);

            return item;
        }

        private static PxVmMenuData GetMenuByInstancePtr(IntPtr instancePtr)
        {
            var menu = new PxVmMenuData();

            // on phoenix side this is of type uint8_t, and uint is 32 bits in c# so we get the byte (8bit integer) and cast it to uint
            var itemCount = (uint)pxVmInstanceMenuGetItemsLength(instancePtr);
            string[]? items = new string[itemCount];
            for (var i = 0u; i < itemCount; i++)
            {
                var itemPtr = pxVmInstanceMenuGetItems(instancePtr, i);
                items[i] = itemPtr.MarshalAsString();
            }

            menu.backPic = pxVmInstanceMenuGetBackPic(instancePtr).MarshalAsString();
            menu.backWorld = pxVmInstanceMenuGetBackWorld(instancePtr).MarshalAsString();
            menu.posX = pxVmInstanceMenuGetPosX(instancePtr);
            menu.posY = pxVmInstanceMenuGetPosY(instancePtr);
            menu.dimX = pxVmInstanceMenuGetDimX(instancePtr);
            menu.dimY = pxVmInstanceMenuGetDimY(instancePtr);
            menu.alpha = pxVmInstanceMenuGetAlpha(instancePtr);
            menu.musicTheme = pxVmInstanceMenuGetMusicTheme(instancePtr).MarshalAsString();
            menu.eventTimerMsec = pxVmInstanceMenuGetEventTimerMsec(instancePtr);
            menu.items = items;
            menu.flags = pxVmInstanceMenuGetFlags(instancePtr);
            menu.defaultOutgame = pxVmInstanceMenuGetDefaultOutgame(instancePtr);
            menu.defaultIngame = pxVmInstanceMenuGetDefaultIngame(instancePtr);

            return menu;
        }

        private static PxVmMenuItemData GetMenuItemByInstancePtr(IntPtr instancePtr)
        {
            var menuItem = new PxVmMenuItemData();

            var textCount = pxVmInstanceMenuItemGetTextLength(instancePtr);
            string[] text = new string[textCount];
            for (int i = 0; i < textCount; i++)
                text[i] = pxVmInstanceMenuItemGetText(instancePtr, (uint)i).MarshalAsString();

            var onSelActionLength = pxVmInstanceMenuItemGetOnSelActionLength(instancePtr);
            int[] onSelAction = new int[onSelActionLength];
            for (int i = 0; i < onSelActionLength; i++)
                onSelAction[i] = pxVmInstanceMenuItemGetOnSelAction(instancePtr, (uint)i);

            var onSelActionSLength = pxVmInstanceMenuItemGetOnSelActionSLength(instancePtr);
            string[] onSelActionS = new string[onSelActionSLength];
            for (int i = 0; i < onSelActionSLength; i++)
                onSelActionS[i] = pxVmInstanceMenuItemGetOnSelActionS(instancePtr, (uint)i).MarshalAsString();

            var onEventActionLength = pxVmInstanceMenuItemGetOnEventActionLength(instancePtr);
            int[] onEventAction = new int[onEventActionLength];
            for (int i = 0; i < onEventActionLength; i++)
                onEventAction[i] = pxVmInstanceMenuItemGetOnEventAction(instancePtr, (uint)i);

            var userFloatLength = pxVmInstanceMenuItemGetUserFloatLength(instancePtr);
            float[] userFloat = new float[userFloatLength];
            for (int i = 0; i < userFloatLength; i++)
                userFloat[i] = pxVmInstanceMenuItemGetUserFloat(instancePtr, (uint)i);

            var userStringLength = pxVmInstanceMenuItemGetUserStringLength(instancePtr);
            string[] userString = new string[userStringLength];
            for (int i = 0; i < userStringLength; i++)
                userString[i] = pxVmInstanceMenuItemGetUserString(instancePtr, (uint)i).MarshalAsString();

            menuItem.fontname = pxVmInstanceMenuItemGetFontname(instancePtr).MarshalAsString();
            menuItem.text = text;
            menuItem.backpic = pxVmInstanceMenuItemGetBackpic(instancePtr).MarshalAsString();
            menuItem.alphamode = pxVmInstanceMenuItemGetAlphamode(instancePtr).MarshalAsString();
            menuItem.alpha = pxVmInstanceMenuItemGetAlpha(instancePtr);
            menuItem.type = pxVmInstanceMenuItemGetType(instancePtr);
            menuItem.onSelAction = onSelAction;
            menuItem.onSelActionS = onSelActionS;
            menuItem.onChgSetOption = pxVmInstanceMenuItemGetOnChgSetOption(instancePtr).MarshalAsString();
            menuItem.onChgSetOptionSection = pxVmInstanceMenuItemGetOnChgSetOptionSection(instancePtr).MarshalAsString();
            menuItem.onEventAction = onEventAction;
            menuItem.posX = pxVmInstanceMenuItemGetPosX(instancePtr);
            menuItem.posY = pxVmInstanceMenuItemGetPosY(instancePtr);
            menuItem.dimX = pxVmInstanceMenuItemGetDimX(instancePtr);
            menuItem.dimY = pxVmInstanceMenuItemGetDimY(instancePtr);
            menuItem.sizeStartScale = pxVmInstanceMenuItemGetSizeStartScale(instancePtr);
            menuItem.flags = pxVmInstanceMenuItemGetFlags(instancePtr);
            menuItem.openDelayTime = pxVmInstanceMenuItemGetOpenDelayTime(instancePtr);
            menuItem.openDuration = pxVmInstanceMenuItemGetOpenDuration(instancePtr);
            menuItem.userFloat = userFloat;
            menuItem.userString = userString;
            menuItem.frameSizex = pxVmInstanceMenuItemGetFrameSizex(instancePtr);
            menuItem.frameSizey = pxVmInstanceMenuItemGetFrameSizey(instancePtr);
            menuItem.hideIfOptionSectionSet = pxVmInstanceMenuItemGetHideIfOptionSectionSet(instancePtr).MarshalAsString();
            menuItem.hideIfOptionSet = pxVmInstanceMenuItemGetHideIfOptionSet(instancePtr).MarshalAsString();
            menuItem.hideOnValue = pxVmInstanceMenuItemGetHideOnValue(instancePtr);

            return menuItem;
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
