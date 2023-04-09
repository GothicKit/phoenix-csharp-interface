using System;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class VmTest : PhoenixTest
    {
        public static void PxVmExternalDefaultCallbackFunction(IntPtr vmPtr, string missingCallbackName)
        {

        }

        public static void Wld_InsertNpc(IntPtr vmPtr)
        {
            // As the values are added to a Stack, we need to pop them in reversed order.
            var waypointName = Vm.pxVmStackPopString(vmPtr);
            var instance = Vm.pxVmStackPopInt(vmPtr);

            Assert.True(waypointName != string.Empty, "Empty waypoint. Maybe string parsing for pxVmStackPopString() is broken?");
            Assert.True(instance != 0, "Instance is >0<. Maybe pxVmStackPopInt() is broken?");
        }

        private IntPtr LoadVm(string relativeFilePath)
        {
            var bufferPtr = LoadBuffer(relativeFilePath);
            var vmPtr = Vm.pxVmLoad(bufferPtr);
            DestroyBuffer(bufferPtr); // No need any longer

            return vmPtr;
        }

        [Fact]
        public void Test_call_External_callback()
        {
            var vmPtr = LoadVm("_work/DATA/scripts/_compiled/GOTHIC.DAT");

            Vm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            Vm.pxVmRegisterExternal(vmPtr, "Wld_InsertNpc", Wld_InsertNpc);

            var called = Vm.pxVmCallFunction(vmPtr, "STARTUP_OLDCAMP");

            Assert.True(called, "Function wasn't called successfully.");

            Vm.pxVmDestroy(vmPtr);
        }


        // The below test has two purposes:
        // 1. Show how to return a value from an External. ConcatStrings() --> PushString()
        // 2. Show how variables can be sent from C# into VM. pushString()+pushInt() --> pxVmCallFunction()
        // Right now I didn't find a small method to call with these TODOs in mind. But I will keep it as reference.
        public static void ConcatStrings(IntPtr vmPtr)
        {
            var str2 = Vm.pxVmStackPopString(vmPtr);
            var str1 = Vm.pxVmStackPopString(vmPtr);

            Assert.True(str1 == "preText", "String with wrong value of >" + str1 + "<");
            Assert.True(str2 == "text", "String with wrong value of >" + str2 + "<");

            Vm.pxVmStackPushString(vmPtr, str1 + str2);
        }

        // Test would be unsuccessfull because of strings put into VM.
        // Nevertheless this example can be used as reference for real implementations.
        //[Fact]
        public void Test_call_method_with_parameter()
        {
            var vmPtr = LoadVm("_work/DATA/scripts/_compiled/GOTHIC.DAT");

            Vm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            Vm.pxVmRegisterExternal(vmPtr, "ConcatStrings", ConcatStrings);


            Vm.pxVmStackPushInt(vmPtr, 2001); // int channel
            Vm.pxVmStackPushString(vmPtr, "preText"); // string preText
            Vm.pxVmStackPushString(vmPtr, "text"); // string text

            // Calling method: func void PrintDebugString (var int channel, var string preText, var string text)
            // Hint: This method will not work as we can't call Methods in VM with multiple strings (not implemented).
            // But this example can be used as reference for real implementations.
            var called = Vm.pxVmCallFunction(vmPtr, "PrintDebugString");
            Assert.True(called, "Function wasn't called successfully.");

            Vm.pxVmDestroy(vmPtr);
        }
    }
}
