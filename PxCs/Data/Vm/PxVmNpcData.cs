using System;

namespace PxCs.Data.Vm
{
    public class PxVmNpcData
    {
        public IntPtr npcPtr;
        public int id;
        public uint symbolIndex;
        public string[] names = new string[0];

        public int routine;
    }
}
