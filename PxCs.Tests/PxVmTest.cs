using System;
using PxCs;
using Xunit;

namespace PxCs.Tests
{
    public class PxVmTest : PxPhoenixTest
    {
        public static void PxVmExternalDefaultCallbackFunction(IntPtr vmPtr, string missingCallbackName)
        {

        }

        public static void Wld_InsertNpc(IntPtr vmPtr)
        {
            // As the values are added to a Stack, we need to pop them in reversed order.
            var waypointName = PxVm.pxVmStackPopString(vmPtr);
            var instance = PxVm.pxVmStackPopInt(vmPtr);

            Assert.True(waypointName != string.Empty, "Empty waypoint. Maybe string parsing for pxVmStackPopString() is broken?");
            Assert.True(instance != 0, "Instance is >0<. Maybe pxVmStackPopInt() is broken?");
        }

        private IntPtr LoadVm(string relativeFilePath)
        {
            var bufferPtr = LoadBuffer(relativeFilePath);
            var vmPtr = PxVm.pxVmLoad(bufferPtr);
            DestroyBuffer(bufferPtr); // No need any longer

            return vmPtr;
        }

        [Fact]
        public void Test_call_External_callback()
        {
            var vmPtr = LoadVm("_work/DATA/scripts/_compiled/GOTHIC.DAT");

            PxVm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            PxVm.pxVmRegisterExternal(vmPtr, "Wld_InsertNpc", Wld_InsertNpc);

            var called = PxVm.pxVmCallFunction(vmPtr, "STARTUP_OLDCAMP");

            Assert.True(called, "Function wasn't called successfully.");

            PxVm.pxVmDestroy(vmPtr);
        }


        // The below test has two purposes:
        // 1. Show how to return a value from an External. ConcatStrings() --> PushString()
        // 2. Show how variables can be sent from C# into VM. pushString()+pushInt() --> pxVmCallFunction()
        // Right now I didn't find a small method to call with these TODOs in mind. But I will keep it as reference.
        public static void ConcatStrings(IntPtr vmPtr)
        {
            var str2 = PxVm.pxVmStackPopString(vmPtr);
            var str1 = PxVm.pxVmStackPopString(vmPtr);

            Assert.True(str1 == "preText", "String with wrong value of >" + str1 + "<");
            Assert.True(str2 == "text", "String with wrong value of >" + str2 + "<");

            PxVm.pxVmStackPushString(vmPtr, str1 + str2);
        }

        // Test would be unsuccessfull because of strings put into VM.
        // Nevertheless this example can be used as reference for real implementations.
        //[Fact]
        public void Test_call_method_with_parameter()
        {
            var vmPtr = LoadVm("_work/DATA/scripts/_compiled/GOTHIC.DAT");

            PxVm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            PxVm.pxVmRegisterExternal(vmPtr, "ConcatStrings", ConcatStrings);


            PxVm.pxVmStackPushInt(vmPtr, 2001); // int channel
            PxVm.pxVmStackPushString(vmPtr, "preText"); // string preText
            PxVm.pxVmStackPushString(vmPtr, "text"); // string text

            // Calling method: func void PrintDebugString (var int channel, var string preText, var string text)
            // Hint: This method will not work as we can't call Methods in VM with multiple strings (not implemented).
            // But this example can be used as reference for real implementations.
            var called = PxVm.pxVmCallFunction(vmPtr, "PrintDebugString");
            Assert.True(called, "Function wasn't called successfully.");

            PxVm.pxVmDestroy(vmPtr);
        }
    }
}
