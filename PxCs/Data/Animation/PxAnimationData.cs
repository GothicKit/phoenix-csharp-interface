using PxCs.Data.Struct;
using System;

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

        public PxAABBData bbox;
        public uint checksum;

        public PxAnimationSampleData[]? samples;
        public uint[]? node_indices;


        [Obsolete("Not yet delivered by phoenix.")]
        public PxAnimationEventData[]? events;
        [Obsolete("Not yet delivered by phoenix.")]
        public string? sourcePath;
        [Obsolete("Not yet delivered by phoenix.")]
        public string? sourceScript;
        [Obsolete("Not yet delivered by phoenix.")]
        public float fpsSource;
        [Obsolete("Not yet delivered by phoenix.")]
        public float samplePositionRangeMin;
        [Obsolete("Not yet delivered by phoenix.")]
        public float samplePositionScalar;
    }
}
