using System;

namespace PxCs.Data.Vob
{
	[Serializable]
    public class PxVobSoundDaytimeData : PxVobSoundData
    {
		public float startTime;
		public float endTime;
		public string soundName2 = default!;
	}
}
