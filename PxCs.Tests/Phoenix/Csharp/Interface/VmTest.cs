using System;
using Xunit;

namespace Phoenix.Csharp.Interface
{
    public class VmTest : PhoenixTest
    {
        public static void PxVmExternalDefaultCallbackFunction(IntPtr vmPtr, string missingCallbackName)
        {

        }


        [Fact]
        public void Test_call_Npc_Routine()
        {
            var bufferPtr = LoadBuffer("_work/DATA/scripts/_compiled/GOTHIC.DAT");

            var vmPtr = Vm.pxVmLoad(bufferPtr);

            DestroyBuffer(bufferPtr); // No need any longer

            Vm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);

            var called = Vm.pxVmCallFunctionJax(vmPtr, "STARTUP_SUB_OLDCAMP");
            Assert.True(called, "Function wasn't called successfully.");

            Vm.pxVmDestroy(vmPtr);
        }
    }
}
