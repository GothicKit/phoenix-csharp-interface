using System;
using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobMobData : PxVobData
    {
        public string name = default!;
        public int hp;
        public int damage;
        public bool movable;
        public bool takable;
        public bool focusOverride;
        public PxVobSoundMaterial material;
        public string visualDestroyed = default!;
        public string owner = default!;
        public string ownerGuild = default!;
        public bool destroyed;
    }
}
