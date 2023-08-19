namespace PxCs.Data.Vob
{
    public class PxVobMobInterData : PxVobMobData
    {
        public int state;
        public string target = default!;
        public string item = default!;
        public string conditionFunction = default!;
        public string onStateChangeFunction = default!;
        public bool rewind;
    }
}
