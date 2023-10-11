using System;

namespace PxCs.Data.Vm
{
	[Serializable]
	public abstract class PxVmData
	{
		public IntPtr instancePtr;
		public uint symbolIndex;
	}
}
