using static PxCs.PxModelScript;

namespace PxCs.Data.Event
{
    public class PxEventTagData
    {
        public int frame;
        public PxEventTagType type;
        public string? slot;
        public string? slot2;
        public string? item;
        public int[]? frames;
        public PxEventFightMode fightMode;
        public bool attached;
    }
}
