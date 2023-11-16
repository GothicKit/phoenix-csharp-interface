using System;
using System.Numerics;

namespace PxCs.Data.Vob
{
    [Serializable]
    public class PxVobNpcData : PxVobData
    {
        public class PxVobNpcTalentData
        {
            public int talent;
            public int value;
            public int skill;
        }

        public class PxVobNpcSlotData
        {
            public bool used;
            public string name = default!;
            public int itemIndex;
            public bool inInventory;
        }

        public string npcInstance = default!;
        public Vector3 modelScale;
        public float modelFatness;
        public string[] overlays = default!;
        public int flags;
        public int guild;
        public int guildTrue;
        public int level;
        public int xp;
        public int xpNextLevel;
        public int lp;
        public PxVobNpcTalentData[] talents = default!;
        public int fightTactic;
        public int fightMode;
        public bool wounded;
        public bool mad;
        public int madTime;
        public bool player;
        public int[] attributes = default!;
        public int[] hcs = default!;
        public int[] missions = default!;
        public string startAiState = default!;
        public int[] aivar = default!;
        public string scriptWaypoint = default!;
        public int attitude;
        public int attitudeTemp;
        public int nameNr;
        public bool moveLock;
        public string[] packed = default!;
        public PxVobItemData[] items = default!;
        public PxVobNpcSlotData[] slots = default!;
        public bool currentStateValid;
        public string currentStateName = default!;
        public int currentStateIndex;
        public bool currentStateIsRoutine;
        public bool nextStateValid;
        public string nextStateName = default!;
        public int nextStateIndex;
        public bool nextStateIsRoutine;
        public int lastAiState;
        public bool hasRoutine;
        public bool routineChanged;
        public bool routineOverlay;
        public int routineOverlayCount;
        public int walkmodeRoutine;
        public bool weaponmodeRoutine;
        public bool startNewRoutine;
        public int aiStateDriven;
        public Vector3 aiStatePos;
        public string currentRoutine = default!;
        public bool respawn;
        public int respawnTime;
        public int[] protection = default!;
        public int bsInterruptableOverride;
        public int npcType;
        public int spellMana;
    }
}