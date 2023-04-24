namespace PxCs.Data.Animation
{
    public class PxAnimationEventData
    {
        PxAnimationEventType type;
        uint no;
        string? tag;
        string[] content = new string[4];
        float[] values = new float[4];
        float probability;
    }
}
