using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    public class PxVobSoundData : PxVobData
    {
		public float volume;
		public PxVobSoundMode mode;
		public float randomDelay;
		public float randomDelayVar;
		public bool initiallyPlaying;
		public bool ambient3d;
		public bool obstruction;
		public float coneAngle;
		public PxVobSoundTriggerVolume volumeType;
		public float radius;
		public string? soundName;
	}
}
