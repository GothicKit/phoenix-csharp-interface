using System;
using System.Runtime.InteropServices;

namespace Phoenix.Csharp.Interface.Marshaller
{
    /// <summary>
    /// This marshaller is used for heap allocated char* from unmanaged code.
    /// A custom marshaller is required, as the default will delete the char* on unmanaged side,
    /// which we want to overcome in this scenario.
    /// Reference: https://stackoverflow.com/questions/18498452/how-do-i-write-a-custom-marshaler-which-allows-data-to-flow-from-native-to-manag/18713398#18713398
    /// </summary>
    public class HeapStringMarshaller : ICustomMarshaler
    {
        public void CleanUpManagedData(object ManagedObj) { }

        // Empty by design: As the received string is from unmanaged heap, we won't clean it up here.
        public void CleanUpNativeData(IntPtr pNativeData) { }

        public int GetNativeDataSize() { return -1; }

        public IntPtr MarshalManagedToNative(object ManagedObj) { throw new NotImplementedException(); }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            string? data = Marshal.PtrToStringAnsi(pNativeData);

            if (data == null)
                return string.Empty;
            else
                return data;
        }

        public static ICustomMarshaler GetInstance(string v) { return new HeapStringMarshaller(); }
    }
}
