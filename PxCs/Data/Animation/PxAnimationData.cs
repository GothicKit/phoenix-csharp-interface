using PxCs.Data.Struct;
using System;

namespace PxCs.Data.Animation
{
    public class PxAnimationData
    {
        public string name = default!;
        public string next = default!;
        public uint layer;

        public uint frameCount;
        public uint nodeCount;

        public float fps;

        public PxAABBData bbox;
        public uint checksum;

        public PxAnimationSampleData[] samples = default!;
        public uint[] node_indices = default!;


        [Obsolete("Not yet delivered by phoenix.")]
        public PxAnimationEventData[] events = default!;
        [Obsolete("Not yet delivered by phoenix.")]
        public string sourcePath = default!;
        [Obsolete("Not yet delivered by phoenix.")]
        public string sourceScript = default!;
        [Obsolete("Not yet delivered by phoenix.")]
        public float fpsSource;
        [Obsolete("Not yet delivered by phoenix.")]
        public float samplePositionRangeMin;
        [Obsolete("Not yet delivered by phoenix.")]
        public float samplePositionScalar;
    }
}
