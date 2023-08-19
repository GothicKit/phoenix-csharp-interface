namespace PxCs.Data.Vm
{
	public class PxVmSfxData : PxVmData
	{
		public string file = default!;
        public int pitchOff;			// pitchoffset in semitones
        public int pitchVar;			// semitone-variance
        public int vol;				    // 0..1
        public int loop;				// 0/1
        public int loopStartOffset;
        public int loopEndOffset;
        public float reverbLevel;		// 0..1
        public string pfxName = default!;
	}
}
