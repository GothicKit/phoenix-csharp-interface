using System;
using System.Collections.Generic;

namespace PxCs.Data.Sound
{
    public class PxSoundData<T>
    {
        public Type arrayType;
        public List<T> sound;

        public PxSoundData()
        {
            arrayType = typeof(T);
        }

    }
}
