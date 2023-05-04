using System;

namespace PxCs.Data.Vm
{
    public class PxVmNpcData
    {
        public IntPtr npcPtr;
        public uint id;
        public uint symbolIndex;
        public string[] names = new string[0];

        public int routine;
    }
}
