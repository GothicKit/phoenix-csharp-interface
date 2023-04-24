using PxCs.Data.Misc;

namespace PxCs.Data.Animation
{
    public class PxAnimationData
    {
        public string? name;
        public string? next;
        public uint layer;

        public uint frameCount;
        public uint nodeCount;

        public float fps;
        public float fpsSource;

        public float samplePositionRangeMin;
        public float samplePositionScalar;

        public PxAABBData bbox;
        public uint checksum;
        public string? sourcePath;
        public string? sourceScript;

        public PxAnimationSampleData[]? samples;
        public PxAnimationEventData[]? events;
        public uint[]? node_indices;
    }
}
