namespace PxCs.Data.Animation
{
    public class PxAnimationEventData
    {
        public PxAnimationEventType type;
        public uint no;
        public string tag = default!;
        public string[] content = new string[4];
        public float[] values = new float[4];
        public float probability;
    }
}
