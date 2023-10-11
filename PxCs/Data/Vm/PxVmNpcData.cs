using System;

namespace PxCs.Data.Vm
{
	[Serializable]
	public class PxVmNpcData : PxVmData
    {
		public int id;
		public string[] names = new string[0];
        public int routine;
    }
}
