using System;

namespace PxCs.Data.Sound
{
    [Serializable]
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
