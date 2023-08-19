using System;
using PxCs.Data.Struct;
using System.Numerics;
using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    public class PxVobData
    {
        public uint id;
        public PxVobType type;

        public VobDecalData? vobDecal; // Optional by design
        
        public PxAABBData boundingBox;

        public PxVobData[] childVobs = default!;

        public Vector3 position;
        public PxMatrix3x3Data rotation = default!;

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
