using static PxCs.Interface.PxModelScript;

namespace PxCs.Data.Event
{
    public class PxEventTagData
    {
        public int frame;
        public PxEventTagType type;
        public string slot = default!;
        public string slot2 = default!;
        public string item = default!;
        public int[] frames = default!;
        public PxEventFightMode fightMode;
        public bool attached;
    }
}
