using System;

namespace PxCs.Data.Vm
{
    [Serializable]
    public class PxVmMenuData : PxVmData
    {
        public string? backPic;
        public string? backWorld;
        public int posX;
        public int posY;
        public int dimX;
        public int dimY;
        public int alpha;
        public string? musicTheme;
        public int eventTimerMsec;
        public string[]? items;
        public uint flags;
        public int defaultOutgame;
        public int defaultIngame;
    }
}
