using System;
using System.Numerics;
using static PxCs.Interface.PxMaterial;

namespace PxCs.Data.Mesh
{
    [Serializable]
    public class PxMaterialData
    {
        public string name = default!;
        public PxMaterialGroup group;
        public uint color;
        public float smoothAngle;
        public string texture = default!;
        public Vector2 textureScale;
        public float animFps;
        public PxMaterialAnimationMappingMode animMapMode;
        public Vector2 animMapDir;
        public bool disableCollision;
        public bool disableLightmap;
        public bool dontCollapse;
        public string detailObject = default!;
        public Vector2 detailTextureScale;
        public bool forceOccluder;
        public bool environmentMapping;
        public float environmentMappingStrength;
        public PxMaterialWaveModeType waveMode;
        public PxMaterialWaveSpeedType waveSpeed;
        public float waveMaxAmplitude;
        public float waveGridSize;
        public bool ignoreSun;
        public PxMaterialAlphaFunction alphaFunction;
        public Vector2 defaultMapping;

    }
}
