﻿using System;
using System.Collections.Generic;
using System.Linq;
using PxCs.Data.Vm;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxVmTest : PxDaedalusScriptTest
    {
        public static void PxVmExternalDefaultCallbackFunction(IntPtr vmPtr, string missingCallbackName)
        {

        }

        public static void Wld_InsertNpc(IntPtr vmPtr)
        {
            // As the values are added to a Stack, we need to pop them in reversed order.
            var waypointName = PxVm.VmStackPopString(vmPtr);
            var instance = PxVm.pxVmStackPopInt(vmPtr);

            Assert.True(waypointName != string.Empty, "Empty waypoint. Maybe string parsing for pxVmStackPopString() is broken?");
            Assert.True(instance != 0, "Instance is >0<. Maybe pxVmStackPopInt() is broken?");
        }

        [Fact]
        public void Test_call_External_callback()
        {
            var vmPtr = LoadVm(VmGothicPath);

            PxVm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            PxVm.pxVmRegisterExternal(vmPtr, "Wld_InsertNpc", Wld_InsertNpc);

            var called = PxVm.CallFunction(vmPtr, "STARTUP_OLDCAMP");

            Assert.True(called, "Function wasn't called successfully.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_get_all_C_Item_Instances()
        {
            var vmPtr = LoadVm(VmGothicPath);

            var elements = PxVm.GetInstancesByClassName(vmPtr, "C_Item");
            var exampleItem = elements.FirstOrDefault(i => i.ToUpper() == "ITARSCROLLHEAL");

            Assert.NotNull(exampleItem);
            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Npc_by_name()
        {
            var vmPtr = LoadVm("_work/DATA/scripts/_compiled/GOTHIC.DAT");

            var hero = PxVm.InitializeNpc(vmPtr, "hero");

            Assert.NotEqual(hero!.instancePtr, IntPtr.Zero);

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_enumerate_Items()
        {
            var vmPtr = LoadVm(VmGothicPath);

            var itemNames = new List<string>();
            PxVm.pxVmEnumerateInstancesByClassName(vmPtr, "C_Item", (string name) => itemNames.Add(name));

            Assert.True(itemNames.Count > 0, "No items found.");
            Assert.True(itemNames.Contains("ITARSCROLLHEAL"), "No scroll found.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Item_by_name()
        {
            var vmPtr = LoadVm(VmGothicPath);

            var lockpick = PxVm.InitializeItem(vmPtr, "ITKELOCKPICK");

            Assert.NotEqual(lockpick!.instancePtr, IntPtr.Zero);
            Assert.True(lockpick.visual!.ToLower() == "ItKe_Lockpick_01.3ds".ToLower(), "Lockpick has wrong visual name.");
            Assert.True(lockpick.mainFlag == PxVm.PxVmItemFlags.ITEM_KAT_NONE, "Lockpick has wrong mainFlag");
            Assert.True(lockpick.flags.HasFlag(PxVm.PxVmItemFlags.ITEM_MULTI), "Lockpick needs to have flag >multi<.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Item_by_index()
        {
            var vmPtr = LoadVm(VmGothicPath);

            var grdArmorH = PxVm.InitializeItem(vmPtr, 3644); // Should be GRD_ARMOR_H

            Assert.NotEqual(grdArmorH!.instancePtr, IntPtr.Zero);
            Assert.True(grdArmorH.visual!.ToLower() == "grdh.3ds".ToLower(), "Guard armor has wrong visual name.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Menu_by_name()
        {
            var vmPtr = LoadVm(VmMenuPath);

            var menu = PxVm.InitializeMenu(vmPtr, "MENU_MAIN");

            Assert.NotNull(menu);
            Assert.True(menu!.musicTheme!.ToLower() == "SYS_Menu".ToLower(), "Menu music is not default music.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Menu_Item_by_name()
        {
            var vmPtr = LoadVm(VmMenuPath);

            var menuItem = PxVm.InitializeMenuItem(vmPtr, "MENUITEM_MAIN_HEADLINE");

            Assert.NotNull(menuItem);
            Assert.True(menuItem.backpic!.ToLower() == "menu_gothicshadow.tga".ToLower(), "Menu item has wrong backpic name.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Sfx_by_name()
        {
            var vmPtr = LoadVm(VmSfxPath);

            var fireSfx = PxVm.InitializeSfx(vmPtr, "FIRE_LARGE");

            Assert.NotEqual(fireSfx!.instancePtr, IntPtr.Zero);
            Assert.True(fireSfx.file!.ToLower() == "fire_large01.wav".ToLower(), "FireLarge has wrong file name.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Pfx_by_name()
        {
            var vmPtr = LoadVm(VmPfxPath);

            var firePfx = PxVm.InitializePfx(vmPtr, "FIRE");

            Assert.NotEqual(firePfx!.instancePtr, IntPtr.Zero);
            Assert.True(firePfx.shpType.ToLower() == "sphere", "Fire has wrong shpType.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_instantiate_Npc_by_index()
        {
            var vmPtr = LoadVm(VmGothicPath);
            PxVm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);

            var npc = PxVm.InitializeNpc(vmPtr, 7656);

            Assert.NotEqual(npc!.instancePtr, IntPtr.Zero);
            Assert.True(npc.level != 0, "NPC has no level set.");

            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_change_Npc_value()
        {
            var vmPtr = LoadVm(VmGothicPath);
            PxVm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);

            var npc = PxVm.InitializeNpc(vmPtr, 7656);
            var valueToTest = "SomeDebugValue";

            Assert.True(npc.wp != valueToTest);
            PxVm.pxVmInstanceNpcSetWP(npc.instancePtr, valueToTest);

            // There is no way to get the information back as of now. We can therefore check for an exception only.

            PxVm.pxVmDestroy(vmPtr);
        }

        public static void TA_MIN(IntPtr vmPtr)
        {
            var waypoint = PxVm.VmStackPopString(vmPtr); // OCR_HUT_33
            var action = PxVm.pxVmStackPopInt(vmPtr); // 5903
            var stop_m = PxVm.pxVmStackPopInt(vmPtr);
            var stop_h = PxVm.pxVmStackPopInt(vmPtr);
            var start_m = PxVm.pxVmStackPopInt(vmPtr);  // 30
            var start_h = PxVm.pxVmStackPopInt(vmPtr);  // 22
            var npc = PxVm.pxVmStackPopInstance(vmPtr);

            Assert.True(Array.Exists(new[] { "OCR_HUT_33", "OCR_OUTSIDE_HUT_29" }, el => el == waypoint), $"Waypoint >{waypoint}< is wrong.");
            Assert.True(Array.Exists(new[] { 22, 8 }, el => el == start_h), $"Start_h >{start_h}< is wrong.");
            Assert.True(npc != IntPtr.Zero, "Npc is IntPtr.Zero.");
        }

        [Fact]
        public void Test_call_routine_on_Npc()
        {
            var vmPtr = LoadVm(VmGothicPath);
            PxVm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            PxVm.pxVmRegisterExternal(vmPtr, "TA_MIN", TA_MIN);

            var pxNpc = PxVm.InitializeNpc(vmPtr, 7774); // Buddler

            var success = PxVm.CallFunction(vmPtr, (uint)pxNpc.routine, pxNpc.instancePtr);

            PxVm.pxVmDestroy(vmPtr);
        }

        // The below test has two purposes:
        // 1. Show how to return a value from an External. ConcatStrings() --> PushString()
        // 2. Show how variables can be sent from C# into VM. pushString()+pushInt() --> pxVmCallFunction()
        // Right now I didn't find a small method to call with these TODOs in mind. But I will keep it as reference.
        public static void ConcatStrings(IntPtr vmPtr)
        {
            var str2 = PxVm.VmStackPopString(vmPtr);
            var str1 = PxVm.VmStackPopString(vmPtr);

            Assert.True(str1 == "preText", "String with wrong value of >" + str1 + "<");
            Assert.True(str2 == "text", "String with wrong value of >" + str2 + "<");

            PxVm.pxVmStackPushString(vmPtr, str1 + str2);
        }

        // Test would be unsuccessfull because of strings put into VM.
        // Nevertheless this example can be used as reference for real implementations.
        //[Fact]
        public void Test_call_method_with_parameter()
        {
            var vmPtr = LoadVm(VmGothicPath);

            PxVm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            PxVm.pxVmRegisterExternal(vmPtr, "ConcatStrings", ConcatStrings);

            PxVm.pxVmStackPushInt(vmPtr, 2001); // int channel
            PxVm.pxVmStackPushString(vmPtr, "preText"); // string preText
            PxVm.pxVmStackPushString(vmPtr, "text"); // string text

            // Calling method: func void PrintDebugString (var int channel, var string preText, var string text)
            // Hint: This method will not work as we can't call Methods in VM with multiple strings (not implemented).
            // But this example can be used as reference for real implementations.
            var called = PxVm.pxVmCallFunction(vmPtr, "PrintDebugString", IntPtr.Zero);
            Assert.True(called, "Function wasn't called successfully.");

            PxVm.pxVmDestroy(vmPtr);
        }
    }
}
