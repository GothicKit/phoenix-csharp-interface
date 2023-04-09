using System.Runtime.InteropServices;
using System;

namespace Phoenix.Csharp.Interface
{
    public class Logging
    {
        private const string DLLNAME = Phoenix.DLLNAME;


        public enum Level
        {
            error, warn, info, debug
        };


        public delegate void PxLogCallback(Level level, string message);

        [DllImport(DLLNAME)] public static extern void pxLoggerSet(PxLogCallback cb);
        [DllImport(DLLNAME)] public static extern void pxLoggerSetDefault();
    }
}
