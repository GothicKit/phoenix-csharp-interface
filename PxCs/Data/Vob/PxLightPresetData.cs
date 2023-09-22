using PxCs.Data.Struct;
using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    public class PxLightPresetData
    {
        public string preset = default!;
        public PxLightMode lightType;
        public float range;
        public Vector4Byte color;
        public float coneAngle;
        public bool isStatic;
        public PxLightQuality quality;
        public string lensflareFx = default!;

        public bool on;
        public float[]? rangeAnimationScale;
        public float rangeAnimationFps;
        public bool rangeAnimationSmooth;
        public Vector4Byte[]? colorAnimationList;
        public float colorAnimationFps;
        public bool colorAnimationSmooth;
        public bool canMove;
    }
}