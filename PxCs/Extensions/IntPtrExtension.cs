using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PxCs.Extensions
{
    public static class IntPtrExtension
    {
        /// <summary>
        /// Convenient method to marshal the heap string IntPtr to C#-String.
        /// ATTENTION: Should be done ASAP after fetching the IntPtr from extern method before the pointer gets lost.
        /// </summary>
        public static string MarshalAsString(this IntPtr strPtr)
        {
            return PxPhoenix.MarshalString(strPtr);
        }
    }
}
