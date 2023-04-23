using PxCs.Data.Misc;
using System.Numerics;
using static PxCs.PxWorld;

namespace PxCs.Data
{
    public class PxVobData
    {
        public uint id;
        public PxVobType type;

        public PxVobData[]? childVobs;

        public Vector3 position;
        public PxMatrix3x3Data? rotation;

        public string presetName = "";
        public string vobName = "";
        public string visualName = "";

        public PxVobAnimationMode animationMode;
        public PxVobShadowMode shadowMode;
        public PxVobSpriteAlignment spriteAlignment;
        public PxVobVisualType visualType;

        public bool ambient;
        public bool cdDynamic;
        public bool cdStatic;
        public bool vobStatic;
        public bool showVisual;
        public bool physicsEnabled;

        public int bias;

        public float animationStrength;
        public float farClipScale;
    }
}
