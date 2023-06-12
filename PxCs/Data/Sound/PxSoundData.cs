using System;

namespace PxCs.Data.Sound
{
    public class PxSoundData<T>
    {
        public Type arrayType;
        public T[] sound;

        public PxSoundData()
        {
            arrayType = typeof(T);
        }

    }
}
