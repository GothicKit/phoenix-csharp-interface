using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

// DEPRECATED: This marshalling didn't work on Android. The CustomMarshaler annotation got called by Unity,
//             but instantly died with segfault without any error etc.
//             Now we Marshal the IntPtr via PxPhoenix.MarshalString() manually.

namespace PxCs.Marshaller
{
    /// <summary>
    /// This marshaller is used for heap allocated char* from unmanaged code.
    /// A custom marshaller is required, as the default will delete the char* on unmanaged side,
    /// which we want to overcome in this scenario.
    /// Reference: https://stackoverflow.com/questions/18498452/how-do-i-write-a-custom-marshaler-which-allows-data-to-flow-from-native-to-manag/18713398#18713398
    /// </summary>
    //public class PxHeapStringMarshaller : ICustomMarshaler
    //{
    //    private static bool isEncodingProviderRegistered = false;


    //    public void CleanUpManagedData(object ManagedObj) { }

    //    // Empty by design: As the received string is from unmanaged heap, we won't clean it up here.
    //    public void CleanUpNativeData(IntPtr pNativeData) { }

    //    public int GetNativeDataSize() { return -1; }

    //    public IntPtr MarshalManagedToNative(object ManagedObj) { throw new NotImplementedException(); }

    //    /// <summary>
    //    /// Gothic comes with Windows-1252 encoding. We need to ensure this codepage is used.
    //    /// Unfortunately Marshal has no option to define codepage. Therefore we need to marshal byte-by-byte.
    //    /// </summary>
    //    public object MarshalNativeToManaged(IntPtr pNativeData)
    //    {
    //        // As PxCs is with .netstandard2.1 we need to register the coding provider once.
    //        if (!isEncodingProviderRegistered)
    //        {
    //            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    //            isEncodingProviderRegistered = true;
    //        }

    //        if (pNativeData == IntPtr.Zero)
    //            throw new ArgumentNullException("String parameter is zero.");

    //        var byteSize = sizeof(byte);
    //        var byteArray = new List<byte>();
            
    //        while (true)
    //        {
    //            var curPtr = IntPtr.Add(pNativeData, byteSize * byteArray.Count);
    //            var curByte = Marshal.ReadByte(curPtr);

    //            if (curByte == 0)
    //                break;
    //            else
    //                byteArray.Add(curByte);
    //        }

    //        if (byteArray.Count == 0)
    //            return string.Empty;
    //        else
    //            return Encoding.GetEncoding(1252).GetString(byteArray.ToArray(), 0, byteArray.Count);
    //    }

    //    public static ICustomMarshaler GetInstance(string v) { return new PxHeapStringMarshaller(); }
    //}
}
