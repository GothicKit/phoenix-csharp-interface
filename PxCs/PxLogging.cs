using System.Runtime.InteropServices;
using System;

namespace PxCs
{
    public static class PxLogging
    {
        private const string DLLNAME = PxPhoenix.DLLNAME;


        public enum Level
        {
            error, warn, info, debug
        };


        public delegate void PxLogCallback(Level level, string message);

        [DllImport(DLLNAME)] public static extern void pxLoggerSet(PxLogCallback cb);
        [DllImport(DLLNAME)] public static extern void pxLoggerSetDefault();
    }
}
