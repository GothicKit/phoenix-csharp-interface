using System;

namespace PxCs.Data.Vm
{
	[Serializable]
	public class PxVmNpcData : PxVmData
    {
		public int id;
        public string slot = default!;
        public uint npcType;
        public uint flags;
        public int startAiState;
        public string spawnPoint = default!;
        public int spawnDelay;
        public int damageType;
        public int guild;
        public int level;
        public int fightTactic;
        public int weapon;
        public int voice;
        public int voicePitch;
        public int bodyMass;
        public int routine;
        public int aiState;
        public int senses;
        public int sensesRange;
        public string wp = default!;
        public int exp;
        public int expNext;
        public int lp;

        public string[] names = default!;
        public int[] attribute = default!;
        public int[] protection = default!;
        public int[] damage = default!;
        public int[] mission = default!;
        public int[] aiVar = default!;
    }
}
