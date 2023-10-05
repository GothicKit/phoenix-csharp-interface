using System;

namespace PxCs.Data.Vm
{
    [Serializable]
    public class PxVmMenuItemData : PxVmData
    {

        public string? fontname;
        public string[]? text;
        public string? backpic;
        public string? alphamode;
        public int alpha;
        public uint type;
        public int[]? onSelAction;
        public string[]? onSelActionS;
        public string? onChgSetOption;
        public string? onChgSetOptionSection;
        public int[]? onEventAction;
        public int posX;
        public int posY;
        public int dimX;
        public int dimY;
        public float sizeStartScale;
        public uint flags;
        public float openDelayTime;
        public float openDuration;
        public float[]? userFloat;
        public string[]? userString;
        public int frameSizex;
        public int frameSizey;
        public string? hideIfOptionSectionSet;
        public string? hideIfOptionSet;
        public int hideOnValue;
    }
}
