using System;
using Xunit;

namespace PxCs.Tests
{
    public class MiscOverflowTest
    {

        /// <summary>
        /// When doing PInvoke, we often need to marshal unsigned values like uint, ushort, and ulong.
        /// This method briefly checks of e.g. Marshal.Copy(int[], ...) and copying to a new uint[] array would result in right values.
        /// </summary>
        [Fact]
        public void Test_overflow_unsigned()
        {
            int int0 = 0;
            int int1 = 1;
            int intMinus1 = -1;
            int maxInt = int.MaxValue;
            int minInt = int.MinValue;

            uint uintFromInt0 = (uint)int0;
            uint uintFromInt1 = (uint)int1;
            uint UintFromIntMinus1 = (uint)intMinus1;
            uint uintFromMaxInt = (uint)maxInt;
            uint uintFromMinInt = (uint)minInt;


            Assert.Equal(0u, uintFromInt0);
            Assert.Equal(1u, uintFromInt1);
            Assert.Equal(4294967295u, UintFromIntMinus1);
            Assert.Equal(2147483647u, uintFromMaxInt);
            Assert.Equal(2147483648u, uintFromMinInt);

            // Bonus checks: Check if binary representation isn't changed!
            // Whatever the representation is: We can say: It won't change the binary data when casted!
            string int0Str = Convert.ToString(int0, 2);
            string int1Str = Convert.ToString(int1, 2);
            string intMinus1Str = Convert.ToString(intMinus1, 2);
            string maxIntStr = Convert.ToString(maxInt, 2);
            string minIntStr = Convert.ToString(minInt, 2);

            string ubyteFromSbyte0Str = Convert.ToString(uintFromInt0, 2);
            string ubyteFromSbyte1Str = Convert.ToString(uintFromInt1, 2);
            string ubyteFromSbyteMinus1Str = Convert.ToString(UintFromIntMinus1, 2);
            string ubyteFromMaxSbyteStr = Convert.ToString(uintFromMaxInt, 2);
            string ubyteFromMinSbyteStr = Convert.ToString(uintFromMinInt, 2);

            Assert.Equal("0", int0Str);
            Assert.Equal("1", int1Str);
            Assert.Equal("11111111111111111111111111111111", intMinus1Str);
            Assert.Equal( "1111111111111111111111111111111", maxIntStr); // maxInt == 011... --> Right: MSB is negative inverter.
            Assert.Equal("10000000000000000000000000000000", minIntStr);
            // ---
            Assert.Equal("0", ubyteFromSbyte0Str);
            Assert.Equal("1", ubyteFromSbyte1Str);
            Assert.Equal("11111111111111111111111111111111", ubyteFromSbyteMinus1Str);
            Assert.Equal( "1111111111111111111111111111111", ubyteFromMaxSbyteStr);
            Assert.Equal("10000000000000000000000000000000", ubyteFromMinSbyteStr);
        }
    }
}
