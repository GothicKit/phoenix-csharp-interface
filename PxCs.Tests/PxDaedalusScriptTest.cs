using System;
using PxCs.Interface;
using Xunit;

namespace PxCs.Tests
{
    public class PxDaedalusScriptTest : PxPhoenixTest
    {
        protected const string VmGothicPath = "_work/DATA/scripts/_compiled/GOTHIC.DAT";
        protected const string VmSfxPath = "_work/DATA/scripts/_compiled/SFX.DAT";
        protected const string VmMenuPath = "_work/DATA/scripts/_compiled/MENU.DAT";
        
        
        /// <summary>
        /// Shows 2 ways of loading Symbols.
        /// </summary>
        [Fact]
        public void Test_load_Symbol()
        {
            var vmPtr = LoadVm(VmGothicPath);

            var nameSymbol = PxDaedalusScript.GetSymbol(vmPtr, "ZS_Boss");
            Assert.NotNull(nameSymbol);
            Assert.True(nameSymbol.name == "ZS_BOSS"); // Phoenix delivers uppercased
            Assert.True(nameSymbol.id != 0);

            var idSymbol = PxDaedalusScript.GetSymbol(vmPtr, nameSymbol.id);
            Assert.NotNull(idSymbol);
            Assert.True(idSymbol.name == "ZS_BOSS");
            Assert.True(idSymbol.id != 0);
            
            PxVm.pxVmDestroy(vmPtr);
        }

        [Fact]
        public void Test_load_invalid_Symbol()
        {
            var vmPtr = LoadVm(VmGothicPath);

            var symbol = PxDaedalusScript.GetSymbol(vmPtr, "Invalid_function");
            Assert.Null(symbol);
            
            PxVm.pxVmDestroy(vmPtr);
        }
        
        protected IntPtr LoadVm(string relativeFilePath)
        {
            var bufferPtr = LoadBuffer(relativeFilePath);
            var vmPtr = PxVm.pxVmLoad(bufferPtr);
            DestroyBuffer(bufferPtr); // No need any longer

            return vmPtr;
        }
    }
}