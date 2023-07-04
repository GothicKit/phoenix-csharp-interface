using System.Numerics;
using PxCs.Interface;

namespace PxCs.Data.Struct
{
    public struct VobDecalData
    {
        public string name;
        public Vector2 dimension;
        public Vector2 offset;
        public bool twoSided;
        public PxMaterial.PxMaterialAlphaFunction alphaFunc;
        public float textureAnimFps;
        public byte alphaWeight;
        public bool ignoreDaylight;
    }
}