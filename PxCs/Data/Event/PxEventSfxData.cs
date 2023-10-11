﻿using System;

namespace PxCs.Data.Event
{
    [Serializable]
    public class PxEventSfxData
    {
        public int frame;
        public string name = default!;
        public float range;
        public bool emptySlot;
    }
}
