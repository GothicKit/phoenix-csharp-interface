using static PxCs.PxWorld;
using System;

namespace PxCs.Data
{
    public class PxVobData
    {
        public uint id;
        public PxVobType type;

        public PxVobData[]? childVobs;

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
