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
        /// Convenient method to marshal heap string IntPtr to C#-String.
        /// </summary>
        public static string MarshalAsString(this IntPtr strPtr)
        {
            return PxPhoenix.MarshalString(strPtr);
        }

        /// <summary>
        /// Convenient method to marshal heap array IntPtr to C#-array values.
        /// The types are generic and will be handled by C# during runtime.
        /// </summary>
        public static T[] MarshalAsArray<T>(this IntPtr arrPtr, uint length)
        {
            if (arrPtr == IntPtr.Zero)
                return new T[0];

            var array = new T[length];

            if (length > int.MaxValue)
                throw new ArgumentOutOfRangeException($"We can only handle int.MaxValue of elements but >{length}< was given.");

            // Marshal
            switch (array)
            {
                case byte[] _:
                case sbyte[] _:
                    Marshal.Copy(arrPtr, (byte[])(object)array, 0, (int)length);
                    break;
                case short[] _:
                case ushort[] _:
                    Marshal.Copy(arrPtr, (short[])(object)array, 0, (int)length);
                    break;
                case int[] _:
                case uint[] _:
                    Marshal.Copy(arrPtr, (int[])(object)array, 0, (int)length);
                    break;
                case long[] _:
                case ulong[] _:
                    Marshal.Copy(arrPtr, (long[])(object)array, 0, (int)length);
                    break;
                case float[] _:
                    Marshal.Copy(arrPtr, (float[])(object)array, 0, (int)length);
                    break;
                    case double[] _:
                    Marshal.Copy(arrPtr, (double[])(object)array, 0, (int)length);
                    break;
                default:
                    throw new ArgumentException($"Marhaling for arguments of type >{array.GetType()}< isn't implemented (yet).");
            }

            // Hints about e.g. short -> ushort:
            // If there's any negative value inside the array, at runtime it will just be "wrapped around" and no binary change will happen on the data.
            // https://www.c-sharpcorner.com/uploadfile/b942f9/how-to-convert-unsigned-integer-arrays-to-signed-arrays-and-vice-versa/
            return array;
        }

    }
}
