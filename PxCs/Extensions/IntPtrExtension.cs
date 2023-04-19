using System;
using System.Runtime.InteropServices;

namespace PxCs.Extensions
{
    /// <summary>
    /// ATTENTION: These methods should be called ASAP after fetching the IntPtr from extern method before the pointer gets lost.
    /// </summary>
    public static class IntPtrExtension
    {
        /// <summary>
        /// Convenient method to marshal the heap string IntPtr to C#-String.
        /// </summary>
        public static string MarshalAsString(this IntPtr strPtr)
        {
            return PxPhoenix.MarshalString(strPtr);
        }

        public static int[] MarshalAsIntArray(this IntPtr intArrPtr, uint length)
        {
            var intArray = new int[length];

            if (length > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{length}< was given.");

            Marshal.Copy(intArrPtr, intArray, 0, (int)length);

            return intArray;
        }

    }
}
