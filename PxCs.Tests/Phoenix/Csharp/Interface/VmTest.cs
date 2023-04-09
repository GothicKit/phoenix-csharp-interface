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
            var waypointName = Vm.pxVmStackPopString(vmPtr);
            var instance = Vm.pxVmStackPopInt(vmPtr);
        }

        public static void Hlp_GetNpc(IntPtr vmPtr)
        {
            var instance = Vm.pxVmStackPopInstance(vmPtr);
        }

        [Fact]
        public void Test_call_Npc_Routine()
        {
            var bufferPtr = LoadBuffer("_work/DATA/scripts/_compiled/GOTHIC.DAT");

            var vmPtr = Vm.pxVmLoad(bufferPtr);

            DestroyBuffer(bufferPtr); // No need any longer

            Vm.pxVmRegisterExternalDefault(vmPtr, PxVmExternalDefaultCallbackFunction);
            Vm.pxVmRegisterExternal(vmPtr, "Wld_InsertNpc", Wld_InsertNpc);
            Vm.pxVmRegisterExternal(vmPtr, "Hlp_GetNpc", Hlp_GetNpc);

            var called = Vm.pxVmCallFunction(vmPtr, "STARTUP_SUB_SURFACE"); // Try to call Hlp_GetNpc() for Nek.

            Assert.True(called, "Function wasn't called successfully.");

            Vm.pxVmDestroy(vmPtr);
        }
    }
}
