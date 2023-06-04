using static PxCs.Interface.PxWorld;

namespace PxCs.Data.Vob
{
    public class PxVobMobData : PxVobData
    {
        public string? name;
        public int hp;
        public int damage;
        public bool movable;
        public bool takable;
        public bool focusOverride;
        public PxVobSoundMaterial material;
        public string? visualDestroyed;
        public string? owner;
        public string? ownerGuild;
        public bool destroyed;
    }
}
