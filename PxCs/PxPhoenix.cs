using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PxCs
{
    public static class PxPhoenix
    {
        public const string DLLNAME = "libphoenix-shared";

        private static bool isEncodingProviderRegistered = false;


        /// <summary>
        /// Important: This method handles heap strings by byte-copying values until (char)'\0' is found.
        /// Important: No memory clean up done.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static string MarshalString(IntPtr strPtr)
        {
            // As PxCs is with .netstandard2.1 we need to register the coding provider once.
            if (!isEncodingProviderRegistered)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                isEncodingProviderRegistered = true;
            }

            if (strPtr == IntPtr.Zero)
                throw new ArgumentNullException("String parameter is zero.");

            var byteSize = sizeof(byte);
            var byteArray = new List<byte>();

            while (true)
            {
                var curPtr = IntPtr.Add(strPtr, byteSize * byteArray.Count);
                var curByte = Marshal.ReadByte(curPtr);

                if (curByte == 0)
                    break;
                else
                    byteArray.Add(curByte);
            }

            if (byteArray.Count == 0)
                return string.Empty;
            else
                return Encoding.GetEncoding(1252).GetString(byteArray.ToArray(), 0, byteArray.Count);
        }
    }
}
